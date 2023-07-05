namespace Practice.DataAccess;

public interface IDbRepository
{
    public string Create(DbEntity entity);
    public string Update(DbEntity dataForUpdate, int idForDeleteOrder);
    public string Delete(int idOrder);
    public IEnumerable<LogicalEntity> ReadAll();
    public LogicalEntity ReadOne(int IdEntity);
}