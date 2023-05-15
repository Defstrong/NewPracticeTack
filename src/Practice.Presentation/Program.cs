using Practice.DataAccess;
var csv = new CsvFileStorage<DbClient>(".\\text1.csv");
var xml = new XmlFileStorage<DbClient>(".\\text.xml");
var file2 = new BaseRepository<DbClient>(xml);
var client = new DbClient {FirstName = "Bob", LastName = "Lee", Address = "Another", PhoneNumber = "Another"};
// file2.Create(client);
// file.Delete(Guid.Parse("33f66fa2-0630-4c91-89c4-d9b299a2dff1"));
// file2.Create(client2);
// file2.Delete(Guid.Parse("56014dcc-a446-49d3-bc51-6a9b323b2cf0"));
