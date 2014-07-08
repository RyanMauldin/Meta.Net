using System;

namespace Meta.Net.Common
{
    /// <summary>
    /// A Predicate is an expression that can be true of something. Provided are
    /// basic predicate methods to pass in to generic list methods such as
    /// List.FindAll(). MatchQualifer can be used as follows...
    /// var predicate = new StringPredicate(matchQualifier);
    /// var rows = list.FindAll(predicate.StartsWith); ...
    /// where each element in the list is checked to see if it starts with the
    /// MatchQualifier string and you get back a subset of that List.
    /// </summary>
    public class StringPredicate
    {
        # region members (1)

        /// <summary>
        /// All comparisons are made based on this string. The methods are implemented
        /// where match contains, starts with, etc... the MatchQualifier.
        /// </summary>
        public string MatchQualifier { get; set; }

        public StringComparison StringComparison { get; set; }

        # endregion members

        # region constructors (2)

        /// <summary>
        /// Default Constructor sets MatchQualifier to an empty string and sets
        /// the StringComparison mode to Ordinal.
        /// </summary>
        public StringPredicate()
        {
            MatchQualifier = "";
            StringComparison = StringComparison.Ordinal;
        }

        /// <summary>
        /// Constructor sets MatchQualifier to value of matchQualifier and sets
        /// the StringComparison mode to Ordinal
        /// </summary>
        public StringPredicate(string matchQualifier)
        {
            MatchQualifier = matchQualifier;
            StringComparison = StringComparison.Ordinal;
        }

        /// <summary>
        /// Constructor sets MatchQualifier to an empty string and sets
        /// the StringComparison type to comparisonType.
        /// </summary>
        public StringPredicate(StringComparison comparisonType)
        {
            MatchQualifier = "";
            StringComparison = comparisonType;
        }

        /// <summary>
        /// Constructor sets MatchQualifier to value of matchQualifier and sets
        /// the StringComparison type to comparisonType.
        /// </summary>
        public StringPredicate(string matchQualifier, StringComparison comparisonType)
        {
            MatchQualifier = matchQualifier;
            StringComparison = comparisonType;
        }

        # endregion constructors

        # region methods (4)

        /// <summary>
        /// Method to pass in as a predicate function in List objects to determine
        /// whether or not each element contains the MatchQualifier.
        /// </summary>
        /// <param name="match">the string to verify</param>
        /// <returns>
        /// True - match contains MatchQualifier
        /// False - match does not contain MatchQualifier
        /// </returns>
        public bool Contains(string match)
        {
            return !string.IsNullOrEmpty(match)
                && !string.IsNullOrEmpty(MatchQualifier)
                && match.IndexOf(MatchQualifier, 0, StringComparison) != -1;
        }

        /// <summary>
        /// Method to pass in as a predicate function in List objects to determine
        /// whether or not each element ends with the MatchQualifier.
        /// </summary>
        /// <param name="match">the string to verify</param>
        /// <returns>
        /// True - match ends with MatchQualifier
        /// False - match does not end with MatchQualifier
        /// </returns>
        public bool EndsWith(string match)
        {
            return !string.IsNullOrEmpty(match)
                && !string.IsNullOrEmpty(MatchQualifier)
                && match.EndsWith(MatchQualifier, StringComparison);
        }

        /// <summary>
        /// Method to pass in as a predicate function in List objects to determine
        /// whether or not each element equals the MatchQualifier.
        /// </summary>
        /// <param name="match">the string to verify</param>
        /// <returns>
        /// True - match equals MatchQualifier
        /// False - match does not equal MatchQualifier
        /// </returns>
        public bool Equals(string match)
        {
            return !string.IsNullOrEmpty(match)
                && !string.IsNullOrEmpty(MatchQualifier)
                && match.Equals(MatchQualifier, StringComparison);
        }

        /// <summary>
        /// Method to pass in as a predicate function in List objects to determine
        /// whether or not each element starts with the MatchQualifier.
        /// </summary>
        /// <param name="match">the string to verify</param>
        /// <returns>
        /// True - match starts with MatchQualifier
        /// False - match does not with MatchQualifier
        /// </returns>
        public bool StartsWith(string match)
        {
            return !string.IsNullOrEmpty(match)
                && !string.IsNullOrEmpty(MatchQualifier)
                && match.StartsWith(MatchQualifier, StringComparison);
        }

        # endregion methods
    }
}
