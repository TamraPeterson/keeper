using System;
using System.Collections.Generic;
using keeper.Models;
using keeper.Repositories;

namespace keeper.Services
{
  public class VaultKeepsService
  {
    private readonly VaultKeepsRepository _vkr;

    public VaultKeepsService(VaultKeepsRepository vkr)
    {
      _vkr = vkr;
    }

    internal VaultKeep Create(VaultKeep vkdata)
    {
      VaultKeep exists = _vkr.getById(vkdata.KeepId, vkdata.CreatorId);
      if (exists != null)
      {
        return exists;
      }
      return _vkr.create(vkdata);
    }

    internal void Remove(int id, string userId)
    {
      VaultKeep found = Get(id);
      if (found.CreatorId != userId)
      {
        throw new Exception("not yours to delete");
      }
      _vkr.Remove(id);
    }

    private VaultKeep Get(int id)
    {
      VaultKeep found = _vkr.Get(id);
      if (found == null)
      {
        throw new Exception("invalid vaultkeep id");
      }
      return found;
    }

    internal List<VaultKeep> GetByVaultId(int id)
    {
     return _vkr.GetAll(id);
    }
  }
}