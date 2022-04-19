using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keeper.Models;

namespace keeper.Repositories
{
  public class KeepsRepository
  {
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Keep> GetAll()
    {
      string sql = @"
      SELECT 
      k.*,
      a.*
       FROM keeps k
       JOIN accounts a ON k.creatorId = a.id;
      ";
      return _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
      {
        keep.Creator = account;
        return keep;
      }).ToList();
    }

    internal void increaseCount(Keep found)
    {
      string sql = @"
      UPDATE keeps
      SET
      views =  @Views
      WHERE id = @Id;";
      _db.ExecuteScalar(sql, found);
    }
    internal void increaseKept(Keep found)
    {
      string sql = @"
      UPDATE keeps
      SET
      kept =  @Kept
      WHERE id = @Id;";
      _db.ExecuteScalar(sql, found);
    }

    internal Keep Create(Keep data)
    {
      string sql = @"
      INSERT INTO keeps
      (name, description, img, views, kept, creatorId)
      VALUES
      (@Name, @Description, @Img, @Views, @Kept, @CreatorId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    internal List<Keep> GetProfileKeeps(string id)
    {
      string sql = @"
      SELECT 
     a.*,
     k.*
      FROM keeps k
      JOIN accounts a ON a.id = k.creatorId
      WHERE k.creatorId = @id
      ";
      return _db.Query<Account, Keep, Keep>(sql, (a, k) =>
      {
        k.Creator = a;
        return k;
      }, new { id }).ToList();
    }

    internal Keep getById(int id)
    {
      string sql = @"
      SELECT k.*,
      a.*
       FROM keeps k
       JOIN accounts a ON k.creatorId = a.id
       WHERE k.id = @id;
      ";
      return _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
      {
        keep.Creator = account;
        return keep;
      }, new { id }).FirstOrDefault();
    }


    internal List<VKViewModel> GetByVaultId(int vaultId)
    {
      string sql = @"
      SELECT 
        k.*,
        vk.id AS vaultkeepId,
        a.*
      FROM vaultkeeps vk
      JOIN keeps k ON k.id = vk.keepId
      JOIN accounts a ON a.id = vk.creatorId
      WHERE vk.vaultId = @vaultId
      ";
      return _db.Query< VKViewModel, Account, VKViewModel>(sql, (vkvm, a) =>
      {
        vkvm.Creator = a;
        return vkvm;
      }, new { vaultId }).ToList();
    }

    internal void update(Keep original)
    {
      string sql = @"
      UPDATE keeps
      SET
      name = @Name,
      description = @Description,
      img = @Img
      WHERE id = @Id;";
      _db.ExecuteScalar(sql, original);
    }

    internal string Remove(int id)
    {
      string sql = @"
     DELETE FROM keeps
     WHERE id = @id;
    ";
      int rows = _db.Execute(sql, new { id });
      if (rows > 0)
      {
        return "keep delorted";
      }
      throw new Exception("error on remove keeps");
    }
  }
}