using System.Data;
using System.Linq;
using Dapper;
using keeper.Models;

namespace keeper.Repositories
{
  public class ProfilesRepository
  {
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Profile GetById(string id)
    {
      string sql = @"
      SELECT * FROM accounts WHERE id = @id;
      ";
      return _db.Query<Profile>(sql, new { id }).FirstOrDefault();
    }
  }
}