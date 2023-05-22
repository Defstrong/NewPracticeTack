using System.Xml.Serialization;
namespace Practice.DataAccess;
/// <summary>
///     Represent class for monipulation xml file
/// </summary>
public sealed class XmlFileStorage<T> : IFileStorage<T>
    where T : DbEntity 
{
    private readonly string _filePath = string.Empty;
    private string? filePath;
    /// <summary>
    ///     Constructor class for add in _filePath name file.
    ///     Create a file if it doesn't exist
    /// </summary>
    /// <param name="filePath">
    ///     Path file for save or read data
    /// </param>
    public XmlFileStorage(string filePath)
    {
        _filePath = filePath;
        if(!File.Exists(_filePath))
            Save(new List<T>()); 
    }
    /// <summary>
    ///     Represent method for read xml file
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
        XmlSerializer serializer = new XmlSerializer(typeof(T[]));
        using StreamReader fileStream = new StreamReader(_filePath);
        T[]? data = serializer.Deserialize(fileStream) as T[];
        return data ?? Array.Empty<T>();
    }
    /// <summary>
    ///     Represent method for save element xml file
    /// </summary>
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
    /// <param name="entity">
    ///     Entity for save in file
    /// </param>
    public void Save(T entity)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using StreamWriter fileStream = new StreamWriter(_filePath);
        serializer.Serialize(fileStream, entity);
        fileStream.Close();
    }
        
    /// <summary>
    ///     Represent method for save elements xml file
    /// </summary>
    /// <param name="entities">
    ///     Enumerable for save in file
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
        XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
        using StreamWriter fileStream = new StreamWriter(_filePath);
        serializer.Serialize(fileStream, entities);
        fileStream.Close();
    }
}

