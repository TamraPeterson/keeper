using System.Collections.Generic;
using System.Data;
using System.Linq;
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

    internal VaultKeep create(VaultKeep data)
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

    internal void Remove(int id)
    {
      string sql = @"
      DELETE FROM vaultkeeps WHERE id = @id LIMIT 1;
      ";
      _db.ExecuteScalar(sql, new { id });
    }

    internal List<VaultKeep> GetAll(int vaultId)
    {
      string sql = @"
      SELECT 
      vk.* ,
      a.*
      FROM vaultkeeps vk
      JOIN accounts a ON vk.creatorId = a.id
      WHERE v.vaultId = @vaultId;";
      return _db.Query<VaultKeep, Account, VaultKeep>(sql, new { vaultId }).ToList();
    }

    internal VaultKeep Get(int id)
    {
      string sql = @"
      SELECT * FROM vaultkeeps WHERE id = @id;
      ";
      return _db.QueryFirstOrDefault<VaultKeep>(sql, new { id });
    }
  }
}