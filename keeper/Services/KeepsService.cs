using System;
using System.Collections.Generic;
using keeper.Models;
using keeper.Repositories;

namespace keeper.Services
{
  public class KeepsService
  {
    private readonly KeepsRepository _kr;
    private readonly VaultsRepository _vr;

    public KeepsService(KeepsRepository kr, VaultsRepository vr)
    {
      _kr = kr;
      _vr = vr;
    }

    internal List<Keep> GetAll()
    {
      return _kr.GetAll();
    }

    internal Keep Create(Keep keepData)
    {
      return _kr.Create(keepData);
    }

    internal Keep GetById(int id)
    {
      Keep found = _kr.getById(id);
      if (found == null)
      {
        throw new Exception("no keep by that id");
      }
      found.Views++;
      _kr.increaseCount(found);
      return found;
    }

    internal List<Keep> GetProfileKeeps(string id)
    {
      return _kr.GetProfileKeeps(id);
    }

    internal Keep update(Keep updateData)
    {
      Keep original = GetById(updateData.Id);
      ValidateUser(updateData.CreatorId, original);
      original.Name = updateData.Name ?? original.Name;
      original.Description = updateData.Description ?? original.Description;
      original.Img = updateData.Img ?? original.Img;
      _kr.update(original);
      return original;
    }



    private void ValidateUser(string creatorId, Keep original)
    {
      if (creatorId != original.CreatorId)
      {
        throw new Exception("you cant edit a keep you didnt create");
      }
    }



    internal string Remove(int id, string userId)
    {
      Keep original = GetById(id);
      if (userId != original.CreatorId)
      {
        throw new Exception("cant remove a keep that isn't yours");
      }
      return _kr.Remove(id);
    }


    // Get Keeps by Vault id
    internal List<VKViewModel> GetByVaultId(int vaultId)
    {
      // TODO verify user somehow
      Vault vault = _vr.getById(vaultId);
      if (vault.IsPrivate == true)
      {
        throw new Exception("cant view private vault");
      }
      return _kr.GetByVaultId(vaultId);

    }
  }
}