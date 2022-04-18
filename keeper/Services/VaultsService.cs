using System;
using System.Collections.Generic;
using keeper.Models;
using keeper.Repositories;

namespace keeper.Services
{
  public class VaultsService
  {
    private readonly VaultsRepository _vr;

    public VaultsService(VaultsRepository vr)
    {
      _vr = vr;
    }

    internal List<Vault> GetAll()
    {
      return _vr.GetAll();
    }

    internal Vault Create(Vault vaultData)
    {
      return _vr.Create(vaultData);
    }

    internal Vault GetById(int id, string userId)
    {
      Vault found = _vr.getById(id);
      if (found == null)
      {
        throw new Exception("no vault by that id");
      }
      else if (found.IsPrivate == true && found.Creator?.Id != userId)
      {
        throw new Exception("that vault is private");
      }
      return found;
    }

    internal Vault update(Vault updateData)
    {
      Vault original = GetById(updateData.Id, updateData.CreatorId);
      ValidateUser(updateData.CreatorId, original.CreatorId);
      original.Name = updateData.Name ?? original.Name;
      original.Description = updateData.Description ?? original.Description;
      original.IsPrivate = updateData.IsPrivate != null ? updateData.IsPrivate : original.IsPrivate;
      _vr.update(original);
      return original;
    }

    private void ValidateUser(string creatorId, string vaultCreatorId)
    {
      if (creatorId != vaultCreatorId)
      {
        throw new Exception("you cant edit a vault you didnt create");
      }
    }

    internal List<Vault> GetProfileVaults(string id)
    {
      return _vr.GetProfileVaults(id);
    }

    internal List<Vault> GetAccountVaults(string id)
    {
      return _vr.GetAccountVaults(id);
    }

    internal string Remove(int id, string userId)
    {
      Vault original = GetById(id, userId);
      if (userId != original.CreatorId)
      {
        throw new Exception("cant remove a vault that isn't yours");
      }
      return _vr.Remove(id);
    }
  }
}