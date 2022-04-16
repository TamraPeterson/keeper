using System;
using keeper.Models;
using keeper.Repositories;

namespace keeper.Services
{
  public class ProfilesService
  {
    private readonly ProfilesRepository _pr;

    public ProfilesService(ProfilesRepository pr)
    {
      _pr = pr;
    }

    internal Profile GetById(string id)
    {
      Profile found = _pr.GetById(id);
      if (found == null)
      {
        throw new Exception("no profile with that id");
      }
      return found;
    }
  }
}