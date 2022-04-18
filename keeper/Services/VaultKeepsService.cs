using System;
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
      if (vkdata.CreatorId == null)
      {
        throw new Exception("you must be logged in to create a vaultkeep");
      }
      return _vkr.Create(vkdata);
    }

    internal string Remove(int id, string userId)
    {
      VaultKeep found = Get(id);
      if (found.CreatorId != userId)
      {
        throw new Exception("not your vaultkeep to delete");
      }
      return _vkr.Remove(id);
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

    // internal List<VaultKeep> GetByVaultId(int id)
    // {
    //   return _vkr.GetKeepsByVaultId(id);

    // }

    // internal List<VaultKeep> GetByVaultId(int id)
    // {
    //   return _vkr.GetAll(id);
    // }
  }
}