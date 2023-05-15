using System.Xml.Serialization;
using System.Xml;
using System.Xml.Linq;
namespace Practice.DataAccess;
/// <summary>
/// Represent class for monipulation xml file
/// </summary>
public sealed class XmlFileStorage<T> : IFileStorage<T>
    where T : IBaseDbEntity
{
    private readonly string _filePath = string.Empty;
    private string? filePath;
    //TODO: Add coment
    public XmlFileStorage(string filePath)
    {
        _filePath = filePath;
        if(!File.Exists(_filePath))
        {
            Save(new List<T>()); 
        }
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
    /// Represent method for save element xml file
    /// </summary>
    public void Save(T entity)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using StreamWriter fileStream = new StreamWriter(_filePath);
        serializer.Serialize(fileStream, entity);
        fileStream.Close();
    }
    /// <summary>
    /// Represent method for save elements xml file
    /// </summary>
    // TODO: 
    public void Save(IEnumerable<T> entities)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
        using StreamWriter fileStream = new StreamWriter(_filePath);
        serializer.Serialize(fileStream, entities);
        fileStream.Close();
    }
}

