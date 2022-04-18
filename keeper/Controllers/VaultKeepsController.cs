using System;
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
  [Authorize]
  public class VaultKeepsController : ControllerBase
  {
    private readonly VaultKeepsService _vks;
    private readonly VaultsService _vs;

    public VaultKeepsController(VaultKeepsService vks, VaultsService vs)
    {
      _vks = vks;
      _vs = vs;
    }

    [HttpPost]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep data)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        Vault vault = _vs.GetById(data.VaultId, user.Id);
        if (vault.CreatorId != user.Id)
        {
          throw new Exception("you can't add to a vault that doesn't belong to you");
        }
        data.CreatorId = user.Id;
        VaultKeep created = _vks.Create(data);
        return Ok(created);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _vks.Remove(id, userInfo.Id);
        return Ok("delorted vaultkeep");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}