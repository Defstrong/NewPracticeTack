using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
namespace Practice.DataAccess;
/// <summary>
/// Represent class for monipulation csv file
/// </summary>
public sealed class CsvFileStorage<T> : IFileStorage<T>
    where T : IBaseDbEntity
{
    private string _filePath = string.Empty;
    //TODO : то что и у XmlFileStorage
    public CsvFileStorage(string filePath)
    {
        _filePath = filePath;
        if(!File.Exists(_filePath))
            Save(new List<T>());
    }

    /// <summary>
    /// Represent method for read csv file
    /// </summary>
    public IEnumerable<T> Read()
    {
        using var reader = new StreamReader(_filePath);
        var config = new CsvConfiguration(CultureInfo.CurrentCulture);
        using var csv = new CsvReader(reader, config);
        return csv.GetRecords<T>().ToList<T>();
    }
    /// <summary>
    /// Represent method for saver element csv file
    /// </summary>
    public void Save(T entity)
    {
        using var writer = new StreamWriter(_filePath);
        var config = new CsvConfiguration(CultureInfo.CurrentCulture);
        using var csv = new CsvWriter(writer, config);
        csv.WriteRecord(entity);
    }
    /// <summary>
    /// Represent method for save elements csv file
    /// </summary>
    public void Save(IEnumerable<T> entities)
    {
        using StreamWriter writer = new StreamWriter(_filePath);
        var config = new CsvConfiguration(CultureInfo.CurrentCulture);
        using CsvWriter csv = new CsvWriter(writer, config);
        csv.WriteRecords(entities);
    }
    
}