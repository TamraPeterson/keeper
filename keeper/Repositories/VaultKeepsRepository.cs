using System;
using System.Data;
using Dapper;
using keeper.Models;

namespace keeper.Repositories
{
  public class VaultKeepsRepository
  {

    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal VaultKeep getById(int keepId, string accountId)
    {
      string sql = @"
      SELECT 
      v.*,
      a.*
       FROM vaultkeeps v
       JOIN accounts a ON v.CreatorId = a.id
      WHERE v.keepId = @keepId;
      ";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { keepId, accountId });
    }

    internal VaultKeep Create(VaultKeep data)
    {
      string sql = @"
      INSERT INTO vaultkeeps
      (creatorId, keepId, vaultId)
      VALUES
      (@CreatorId, @KeepId, @VaultId);
      SELECT LAST_INSERT_ID();
      ";
      int id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    internal string Remove(int id)
    {
      string sql = @"
      DELETE FROM vaultkeeps 
      WHERE id = @id LIMIT 1;
      ";
      int rows = _db.Execute(sql, new { id });
      if (rows > 0)
      {
        return "vaultkeep delorted";
      }
      throw new Exception("error on remove vaultkeep");
    }

    // internal List<VaultKeep> GetAll(int id)
    // {
    //   string sql = @"
    //   SELECT 
    //   vk.* ,
    //   a.*
    //   FROM vaultkeeps vk
    //   JOIN accounts a ON vk.creatorId = a.id;";
    //   return _db.Query<VaultKeep, Account, VaultKeep>(sql, new{id}).ToList();
    // }

    internal VaultKeep Get(int id)
    {
      string sql = @"
      SELECT * FROM vaultkeeps WHERE id = @id;
      ";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }
  }
}