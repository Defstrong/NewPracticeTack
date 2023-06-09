namespace Practice.DataAccess;
///<summary>
///     Represent an order factory
///</summary>
///<typeparam name = "T"> Entity type </typeparam>
public static class FileStorageFactory<T>
    where T : DbEntity 
{
    ///<summary>
    ///     Represent CsvFileStorage relization for IFileStorage interface
    ///</summary>
    ///<peparam name = "filePath"> Path to csc file </peparam>
    ///<returns>
    ///     CsvFileStorage implementation
    ///</returns>
    public static IFileStorage<T> GetCsvFileStorage(string filePath)
    {
        IFileStorage<T> fileStorage= new CsvFileStorage<T>(filePath);
        return fileStorage;
    }
    ///<summary>
    ///     Represent XmlFileStorage relization for IFileStorage interface
    ///</summary>
    ///<peparam name = "filePath"> Path to xml file </peparam>
    ///<returns>
    ///     XmlFileStorage implementation
    ///</returns>
    public static IFileStorage<T> GetXmlFileStorageAsync(string filePath)
    {
        IFileStorage<T> fileStorage = new XmlFileStorage<T>(filePath);
        return fileStorage;
    }
}