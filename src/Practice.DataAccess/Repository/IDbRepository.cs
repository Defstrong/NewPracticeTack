namespace Practice.DataAccess;

public interface IDbRepository
{
    public string Create(DbEntity entity);
    public string Update(DbEntity dataForUpdate, int idForDeleteOrder);
    public string Delete(string clientOrOrder, int idOrder);
    public string Read(int idEntity);
}