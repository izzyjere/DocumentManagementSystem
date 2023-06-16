public class Status
{
    // This class provides constants for different statuses used in the system.

    public const string CREATE = "CREATE";
    // The status indicating creation.

    public const string ARCHIVE = "ARCHIVE";
    // The status indicating archiving.

    public const string DELETE = "DELETE";
    // The status indicating deletion.

    public const string EXPORT = "EXPORT";
    // The status indicating export.

    public const string IMPORT = "IMPORT";
    // The status indicating import.

    public const string INVALID = "INVALID";
    // The status indicating invalid.

    public const string CORRUPT = "CORRUPT";
    // The status indicating corrupt.

    /// <summary>
    /// Gets all statuses as an enumerable collection.
    /// </summary>
    /// <returns>An enumerable collection of strings representing the statuses.</returns>
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
