using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using keeper.Models;
using keeper.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace keeper.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class VaultsController : ControllerBase
  {
    private readonly VaultsService _vs;
    private readonly VaultKeepsService _vks;
    private readonly KeepsService _ks;

    public VaultsController(VaultsService vs, VaultKeepsService vks, KeepsService ks)
    {
      _vs = vs;
      _vks = vks;
      _ks = ks;
    }

    [HttpGet]
    public ActionResult<List<Vault>> GetAll()
    {
      try
      {
        List<Vault> vaults = _vs.GetAll();
        return Ok(vaults);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Vault>> GetByIdAsync(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        Vault vault = _vs.GetById(id, userInfo.Id);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Vault>> Create([FromBody] Vault vaultData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        vaultData.CreatorId = userInfo.Id;
        Vault vault = _vs.Create(vaultData);
        vaultData.Creator = userInfo;
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpPut("{id}")]
    [Authorize]
    public async Task<ActionResult<Vault>> Update([FromBody] Vault updateData, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        updateData.Id = id;
        updateData.CreatorId = userInfo.Id;
        Vault vault = _vs.update(updateData);
        return Ok(vault);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<string>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return _vs.Remove(id, userInfo.Id);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // Get keeps by vault id
    [HttpGet("{id}/keeps")]

    public async Task<ActionResult<List<VKViewModel>>> GetVaultKeeps(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_ks.GetByVaultId(id, userInfo.Id));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    // FIXME if a user isn't logged in they can't get vault keeps...

  }
}