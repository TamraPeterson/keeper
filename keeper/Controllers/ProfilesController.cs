using System;
using System.Collections.Generic;
using keeper.Models;
using keeper.Services;
using Microsoft.AspNetCore.Mvc;

namespace keeper.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly KeepsService _ks;
    private readonly ProfilesService _ps;


    public ProfilesController(VaultsService vs, KeepsService ks, ProfilesService ps)
    {
      _vs = vs;
      _ks = ks;
      _ps = ps;

    }


    [HttpGet("{id}")]
    public ActionResult<Profile> GetProfile(string id)
    {
      try
      {
        Profile profile = _ps.GetById(id);
        return Ok(profile);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // Get profile Keeps
    [HttpGet("{id}/keeps")]
    public ActionResult<List<Keep>> GetProfileKeeps(string id)
    {
      try
      {
        List<Keep> keeps = _ks.GetProfileKeeps(id);
        return Ok(keeps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // Get profile vaults
    [HttpGet("{id}/vaults")]
    public ActionResult<List<Vault>> GetProfileVaults(string id)
    {
      try
      {
        List<Vault> vaults = _vs.GetProfileVaults(id);
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}