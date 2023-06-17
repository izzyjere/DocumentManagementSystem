namespace RTSADocs.Shared.Constants
{
    public class Status
    {
        public const string CREATE = "CREATE";
        public const string ARCHIVE = "ARCHIVE";
        public const string DELETE = "DELETE";
        public const string EXPORT = "EXPORT";
        public const string IMPORT = "IMPORT";
        public const string INVALID = "INVALID";
        public const string CORRUPT = "CORRUPT";

        /// <summary>
        /// Gets all statuses an enumerable collection;
        /// </summary>
        /// <returns>An Enumerable collection of strings.</returns>
        public static IEnumerable<string> Enumerate()
        {
            yield return CREATE;
            yield return EXPORT;
            yield return IMPORT;
            yield return INVALID;
            yield return CORRUPT;
            yield return ARCHIVE;
            yield return DELETE;
        }
    }
}