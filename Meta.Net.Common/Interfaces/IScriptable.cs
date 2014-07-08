using System.Collections.Specialized;

namespace Meta.Net.Common.Interfaces
{
    /// <summary>
    /// Interface for ensuring classes contain a Script() method that returns a string.
    /// </summary>
    public interface IScriptable
    {
        # region methods (1)

        /// <summary>
        /// Generates a set of script strings.
        /// </summary>
        /// <returns>collection of strings</returns>
        StringCollection Script();

        /// <summary>
        /// Generates a set of script strings delimited by a string.
        /// </summary>
        /// <returns>collection of strings</returns>
        StringCollection Script(string delimiter);

        # endregion methods
    }
}
