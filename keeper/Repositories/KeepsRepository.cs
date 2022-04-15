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
      SELECT * FROM keeps;
      ";
      return _db.Query<Keep>(sql).ToList();
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

    internal Keep getById(int id)
    {
      string sql = @"
      SELECT * FROM keeps
      WHERE id = @id;
      ";
      return _db.Query<Keep>(sql, new { id }).FirstOrDefault();
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