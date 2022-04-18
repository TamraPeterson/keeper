using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using keeper.Models;

namespace keeper.Repositories
{
  public class VaultsRepository
  {
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Vault> GetAll()
    {
      string sql = @"
      SELECT 
      v.*,
      a.*
       FROM vaults v
       JOIN accounts a ON v.creatorId = a.id;
      ";
      return _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
      {
        vault.Creator = account;
        return vault;
      }).ToList();
    }

    internal Vault Create(Vault data)
    {
      string sql = @"
      INSERT INTO vaults
      (name, description, isPrivate, creatorId)
      VALUES
      (@Name, @Description, @IsPrivate, @CreatorId);
      SELECT LAST_INSERT_ID();";
      int id = _db.ExecuteScalar<int>(sql, data);
      data.Id = id;
      return data;
    }

    internal Vault getById(int id)
    {
      string sql = @"
      SELECT v.*,
      a.*
       FROM vaults v
       JOIN accounts a ON v.creatorId = a.id
       WHERE v.id = @id;
      ";
      return _db.Query<Vault, Account, Vault>(sql, (vault, account) =>
      {
        vault.Creator = account;
        return vault;
      }, new { id }).FirstOrDefault();
    }

    internal void update(Vault original)
    {
      string sql = @"
      UPDATE vaults
      SET
      name = @Name,
      description = @Description,
      isPrivate = @IsPrivate
      WHERE id = @Id;";
      _db.ExecuteScalar(sql, original);
    }

    internal List<Vault> GetProfileVaults(string id)
    {
      string sql = @"
      SELECT 
      a.*,
      v.*
      FROM vaults v
      JOIN accounts a ON a.id = v.creatorId
      WHERE v.creatorId = @id;
      ";
      return _db.Query<Account, Vault, Vault>(sql, (a, v) =>
      {
        v.Creator = a;
        return v;
      }, new { id }).ToList();
    }

    internal List<Vault> GetAccountVaults(string id)
    {
      string sql = @"
     SELECT * FROM vaults
      WHERE creatorId = @id;
     ";
      return _db.Query<Vault>(sql, new { id }).ToList();
    }

    internal string Remove(int id)
    {
      string sql = @"
     DELETE FROM vaults
     WHERE id = @id;
    ";
      int rows = _db.Execute(sql, new { id });
      if (rows > 0)
      {
        return "vault delorted";
      }
      throw new Exception("error on remove vaults");
    }
  }
}