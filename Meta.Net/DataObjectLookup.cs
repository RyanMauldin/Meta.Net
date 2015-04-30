using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Meta.Net.Abstract;

namespace Meta.Net
{
    /// <summary>
    /// This class is basically a wrapper for a dictionary instantiated as StringComparer.OrdinalIgnoreCase.
    /// This cleans up dictionary intensive code fairly well throughout the entire library and gives
    /// direct access to the last used data object, instead of performing a dictionary lookup first.
    /// It also ensures that all dictionaries are instantiated properly with StringComparer.OrdinalIgnoreCase
    /// comparer, without having that code everywhere such as: if(!dictionary.TryGetValue(...etc.)). 
    /// </summary>
    /// <typeparam name="TP">The parent data object that implements IDataObject</typeparam>
    /// <typeparam name="TC">The child data object that implements IDataObject</typeparam>
    public class DataObjectLookup<TP, TC>
        : IEnumerable<TC>
        where TP : BaseMetaObject
        where TC : BaseMetaObject
    {
        private const string InvalidParentTypeErrorMessage =
            "{0} {1} can not be assigned the parent {2} {3} as it is not an acceptable type.";

        private const string ObjectAlreadyExistsErrorMessage =
            "The {0} {1} already has the {2} {3}.";

        /// <summary>
        /// The default string comparer for this library.
        /// </summary>
        public readonly StringComparer StringComparer = StringComparer.OrdinalIgnoreCase;

        /// <summary>
        /// The last used data object requested from Lookup
        /// </summary>
        private TC _lastUsedDataObject;

        /// <summary>
        /// The underlying field for DataObjects.
        /// </summary>
        private ConcurrentDictionary<string, TC> _childDataObjects;

        /// <summary>
        /// The data object dictionary of type T
        /// </summary>
        private ConcurrentDictionary<string, TC> ChildDataObjects
        {
            get { return _childDataObjects; }
            set
            {
                if (value == null)
                {
                    _childDataObjects = new ConcurrentDictionary<string, TC>(StringComparer);
                    _lastUsedDataObject = null;
                    return;
                }

                _childDataObjects = value;
                _lastUsedDataObject = _childDataObjects.Count > 0
                    ? _childDataObjects.First().Value
                    : null;
            }
        }

        public TP ParentDataObject { get; private set; }

        /// <summary>
        /// Initializer for all constructors.
        /// </summary>
        /// <param name="parentDataObject">The parent data object to parent all child data objects on.</param>
        /// <param name="childDataObjects">The child data objects to add to the parent.</param>
        private void Init(TP parentDataObject, Dictionary<string, TC> childDataObjects)
        {
            if (parentDataObject == null)
                throw new Exception("The parent dataobject should never be initialized as a null object.");

            ParentDataObject = parentDataObject;
            ChildDataObjects = new ConcurrentDictionary<string, TC>(StringComparer);

            if (childDataObjects == null)
                return;

            foreach (var childDataObject in childDataObjects.Values)
                Add(childDataObject);
        }

        /// <summary>
        /// Default constructor that initializes the data objects dictionary to an empty dictionary
        /// with StringComparer.OrdinalIgnoreCase parameter.
        /// </summary>
        public DataObjectLookup(TP parentDataObject)
        {
            Init(parentDataObject, null);
        }

        /// <summary>
        /// Specific constructor to initialize with a data objects dictionary.
        /// </summary>
        /// <param name="parentDataObject">The parent data objects to initalize with.</param>
        /// <param name="childDataObjects">The chidl data objects dictionary to initalize with.</param>
        public DataObjectLookup(TP parentDataObject, Dictionary<string, TC> childDataObjects)
        {
            Init(parentDataObject, childDataObjects);
        }

        /// <summary>
        /// Specific constructor to initialize with a data object lookup or any IEnumerable collection of type T.
        /// </summary>
        /// <param name="parentDataObject">The parent data objects to initalize with.</param>
        /// <param name="childDataObjects">The chidl data objects dictionary to initalize with.</param>
        public DataObjectLookup(TP parentDataObject, IEnumerable<TC> childDataObjects)
        {
            Init(parentDataObject, null);
            foreach (var childDataObject in childDataObjects)
                if (!ChildDataObjects.TryAdd(childDataObject.Namespace, childDataObject))
                    throw new Exception(string.Format(ObjectAlreadyExistsErrorMessage,
                         ParentDataObject.Description, ParentDataObject.Namespace, childDataObject.Description, childDataObject.Namespace));

            _lastUsedDataObject = _childDataObjects.Count > 0
                ? _childDataObjects.First().Value
                : null;
        }

        /// <summary>
        /// The number of objects in the DataObjects dictionary.
        /// </summary>
        public int Count { get { return ChildDataObjects.Count; } }

        /// <summary>
        /// Looks up an object that implements IDataObject by Namespace.
        /// If the last used object is the same, no lookup occurs by the dictionary.
        /// </summary>
        /// <param name="key">The key in the dictionary (IDataObject.Namespace).</param>
        /// <returns>Returns the data object in the dictionary.</returns>
        public TC this[string key]
        {
            get
            {
                if (_lastUsedDataObject != null
                    && StringComparer.Compare(key, _lastUsedDataObject.Namespace) == 0)
                    return _lastUsedDataObject;

                _lastUsedDataObject = null;
                return ChildDataObjects.TryGetValue(key, out _lastUsedDataObject)
                    ? _lastUsedDataObject
                    : null;
            }
            set
            {
                ChildDataObjects[key] = value;
                _lastUsedDataObject = value;
            }
        }

        public IEnumerable<string> Keys
        {
            get { return ChildDataObjects.Keys; }
        }

        public IEnumerator<KeyValuePair<string, TC>> KeyValuePairs
        {
            get { return ChildDataObjects.GetEnumerator(); }
        }

        public IEnumerator<TC> GetEnumerator()
        {
            return ChildDataObjects.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Calls ContainsKey on underlying dictionary.
        /// </summary>
        /// <param name="key">The key to find.</param>
        /// <returns>true if the dictionary has the key, false if not.</returns>
        public bool ContainsKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new Exception("Key parameter should never be null.");

            return ChildDataObjects.ContainsKey(key);
        }

        ///// <summary>
        ///// Calls ContainsValue on underlying dictionary.
        ///// </summary>
        ///// <param name="value">The value to find.</param>
        ///// <returns>true if the dictionary has the value, false if not.</returns>
        //public bool ContainsValue(TC value)
        //{
        //    if (value == null)
        //        throw new Exception("Value parameter should never be null.");

        //    return ChildDataObjects.ContainsValue(value);
        //}

        /// <summary>
        /// Calls clear on the underlying dictionary.
        /// Sets _lastUsedDataObject to null;
        /// </summary>
        public void Clear()
        {
            _lastUsedDataObject = null;
            ChildDataObjects.Clear();
        }

        /// <summary>
        /// Adds a new object to the DataObjects dictionary using the Namespace property as the key.
        /// It also reassigns the parent data object if it has not been assigned.
        /// </summary>
        /// <param name="childDataObject">The child data object to add.</param>
        public void Add(TC childDataObject)
        {
            if (childDataObject == null)
                throw new Exception("The child data object should never be null.");
            
            if (!childDataObject.CanBeAssignedParentMetaObject(ParentDataObject))
                throw new Exception(string.Format(InvalidParentTypeErrorMessage,
                    childDataObject.Description, childDataObject.Namespace, ParentDataObject.Description, ParentDataObject.Namespace));

            var tempParentDataObject = childDataObject.ParentMetaObject;
            childDataObject.ParentMetaObject = ParentDataObject;
            if (ChildDataObjects.ContainsKey(childDataObject.Namespace))
            {
                var errorMessage = string.Format(ObjectAlreadyExistsErrorMessage,
                    ParentDataObject.Description, ParentDataObject.Namespace, childDataObject.Description, childDataObject.Namespace);
                childDataObject.ParentMetaObject = tempParentDataObject;
                throw new Exception(errorMessage);
            }

            if(!ChildDataObjects.TryAdd(childDataObject.Namespace, childDataObject))
                throw new Exception(string.Format(ObjectAlreadyExistsErrorMessage,
                    ParentDataObject.Description, ParentDataObject.Namespace, childDataObject.Description, childDataObject.Namespace));

        }

        /// <summary>
        /// Merges more data objects into the DataObjects dictionary using the same keys.
        /// It also reassigns the parent data object if it has not been assigned.
        /// </summary>
        /// <param name="childDataObjects">The child data objects dictionary to merge.</param>
        public void Add(Dictionary<string, TC> childDataObjects)
        {
            if (childDataObjects == null)
                return;

            foreach (var childDataObject in childDataObjects.Values)
                Add(childDataObject);
        }

        /// <summary>
        /// Merges more data objects into the DataObjects dictionary using the Namespace property as the key.
        /// It also reassigns the parent data object if it has not been assigned.
        /// </summary>
        /// <param name="childDataObjects">The child data objects collection to merge.</param>
        public void Add(ICollection<TC> childDataObjects)
        {
            if (childDataObjects == null)
                return;

            foreach (var childDataObject in childDataObjects)
                Add(childDataObject);
        }

        /// <summary>
        /// Merges more data objects into the DataObjects dictionary using the Namespace property as the key.
        /// It also reassigns the parent data object if it has not been assigned.
        /// </summary>
        /// <param name="childDataObjects">The child data objects enumeration to merge.</param>
        public void Add(IEnumerable<TC> childDataObjects)
        {
            if (childDataObjects == null)
                return;

            foreach (var childDataObject in childDataObjects)
                Add(childDataObject);
        }

        /// <summary>
        /// Merges more data objects into the dictionary for this lookup using the Namespace property as the key.
        /// It also reassigns the parent data object if it has not been assigned.
        /// </summary>
        /// <param name="childDataObjects">The child data objects list to merge.</param>
        public void Add(IList<TC> childDataObjects)
        {
            if (childDataObjects == null)
                return;

            foreach (var childDataObject in childDataObjects)
                Add(childDataObject);
        }

        /// <summary>
        /// Removes a data object from the internal dictionary by key.
        /// </summary>
        /// <param name="key">The key for the data object to remove.</param>
        public void Remove(string key)
        {
            if (!ChildDataObjects.ContainsKey(key))
                return;

            if (StringComparer.Compare(_lastUsedDataObject.Namespace, key) == 0)
                _lastUsedDataObject = null;

            TC childDataObject;
            ChildDataObjects.TryRemove(key, out childDataObject);
            childDataObject.ParentMetaObject = null;
        }

        /// <summary>
        /// Removes a data object from the internal dictionary by value.
        /// </summary>
        /// <param name="childDataObject">The child data object to remove.</param>
        public void Remove(TC childDataObject)
        {
            if (!ChildDataObjects.ContainsKey(childDataObject.Namespace))
                return;

            if (StringComparer.Compare(_lastUsedDataObject.Namespace, childDataObject.Namespace) == 0
                || _lastUsedDataObject.Equals(childDataObject))
                _lastUsedDataObject = null;

            TC childDataObjectToRemove;
            ChildDataObjects.TryRemove(childDataObject.Namespace, out childDataObjectToRemove);
            childDataObject.ParentMetaObject = null;
        }

        /// <summary>
        /// Renames the child data object and retains it as the last used data object if it was.
        /// This method will also add the child data object to this collection with the new name
        /// if it did not already exist in the dictionary for this lookup. It also reassigns the parent
        /// data object if it has not been assigned.
        /// </summary>
        /// <param name="childDataObject">The child data object to rename.</param>
        /// <param name="newObjectName">The new name of the child data object.</param>
        public void Rename(TC childDataObject, string newObjectName)
        {
            var tempLastUsedDataObject = _lastUsedDataObject;
            Remove(childDataObject);
            if (tempLastUsedDataObject != null && tempLastUsedDataObject.Equals(childDataObject))
                _lastUsedDataObject = tempLastUsedDataObject;
            childDataObject.ObjectName = newObjectName;
            Add(childDataObject);
        }

        public DataObjectLookup<TP, TC> DeepClone(TP parentDataObject)
        {
            var dataObjectLookup = new DataObjectLookup<TP, TC>(parentDataObject);
            foreach (var childDataObject in ChildDataObjects.Values.Select(p => p.DeepClone()))
                dataObjectLookup.Add((TC)childDataObject);
            return dataObjectLookup;
        }

        public DataObjectLookup<TP, TC> ShallowClone(TP parentDataObject)
        {
            var dataObjectLookup = new DataObjectLookup<TP, TC>(parentDataObject);
            foreach (var childDataObject in ChildDataObjects.Values.Select(p => p.ShallowClone()))
                dataObjectLookup.Add((TC)childDataObject);
            return dataObjectLookup;
        }
    }
}
