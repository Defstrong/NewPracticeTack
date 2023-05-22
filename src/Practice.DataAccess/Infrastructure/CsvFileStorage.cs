using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
namespace Practice.DataAccess;
/// <summary>
///     Represent class for monipulation csv file
/// </summary>
/// <typeparam name="T">
///     Type entity
/// </typeparam>
public sealed class CsvFileStorage<T> : IFileStorage<T>
    where T : DbEntity
{
    private string _filePath = string.Empty;
    /// <summary>
    ///     Constructor class for add in _filePath name file.
    ///     Create a file if it doesn't exist
    /// </summary>
    /// <param name="filePath">
    ///     Path file for save or read data
    /// </param>
    public CsvFileStorage(string filePath)
    {
        _filePath = filePath;
        if(!File.Exists(_filePath))
            Save(new List<T>());
    }

    /// <summary>
    ///     Represent method for read csv file
    /// </summary>
    /// <exception cref="FileNotFoundException">
    ///     The file cannot be found
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     path is an empty string ("").
    /// </exception>
    /// <exception cref="DirectoryNotFoundException">
    ///     The exception that is thrown when part of a file or directory cannot be found.
    /// </exception>
    /// <exception cref="IOException">
    ///     path includes an incorrect or invalid syntax for file name, directory name, or
    ///     volume label.
    /// </exception>
    /// <exception cref="ArgumentNullException">
    ///     path is null.
    /// </exception>
    public IEnumerable<T> Read()
    {
        using var reader = new StreamReader(_filePath);
        var config = new CsvConfiguration(CultureInfo.CurrentCulture);
        using var csv = new CsvReader(reader, config);
        //Don't work with IEnumerable<T>
        return csv.GetRecords<T>().ToList<T>();
    }
    /// <summary>
    ///     Represent method for saver element csv file
    /// </summary>
    /// <param name="entity">
    ///     Entity for save in file
    /// </param>
    /// <exception cref="UnauthorizedAccessException">
    ///     Access is denied.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     path is an empty string (""). -or- path contains the name of a system device
    ///     (com1, com2, and so on).
    /// </exception>
    /// <exception cref="ArgumentNullException">
    ///     path is null.
    /// </exception>
    /// <exception cref="DirectoryNotFoundException">
    ///     The specified path is invalid (for example, it is on an unmapped drive).
    /// </exception>
    /// <exception cref="PathTooLongException">
    ///     The specified path, file name, or both exceed the system-defined maximum length.
    /// </exception>
    /// <exception cref="IOException">
    ///     path includes an incorrect or invalid syntax for file name, directory name, or
    ///     volume label syntax.
    /// </exception>
    /// <exception cref="SecurityException">
    ///     The caller does not have the required permission.
    /// </exception>
    public void Save(T entity)
    {
        using var writer = new StreamWriter(_filePath);
        var config = new CsvConfiguration(CultureInfo.CurrentCulture);
        using var csv = new CsvWriter(writer, config);
        csv.WriteRecord(entity);
    }
    /// <summary>
    ///     Represent method for save elements csv file
    /// </summary>
    /// <param name="entities">
    ///     Entities for save in file
    /// </param> 
    /// <exception cref="UnauthorizedAccessException">
    ///     Access is denied.
    /// </exception>
    /// <exception cref="ArgumentException">
    ///     path is an empty string (""). -or- path contains the name of a system device
    ///     (com1, com2, and so on).
    /// </exception>
    /// <exception cref="ArgumentNullException">
    ///     path is null.
    /// </exception>
    /// <exception cref="DirectoryNotFoundException">
    ///     The specified path is invalid (for example, it is on an unmapped drive).
    /// </exception>
    /// <exception cref="PathTooLongException">
    ///     The specified path, file name, or both exceed the system-defined maximum length.
    /// </exception>
    /// <exception cref="IOException">
    ///     path includes an incorrect or invalid syntax for file name, directory name, or
    ///     volume label syntax.
    /// </exception>
    /// <exception cref="SecurityException">
    ///     The caller does not have the required permission.
    /// </exception>
    public void Save(IEnumerable<T> entities)
    {
        using StreamWriter writer = new StreamWriter(_filePath);
        var config = new CsvConfiguration(CultureInfo.CurrentCulture);
        using CsvWriter csv = new CsvWriter(writer, config);
        csv.WriteRecords(entities);
    }
    
}