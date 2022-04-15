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

    public VaultKeepsController(VaultKeepsService vks)
    {
      _vks = vks;
    }

    [HttpPost]
    public async Task<ActionResult<VaultKeep>> Create([FromBody] VaultKeep data)
    {
      try
      {
        Account user = await HttpContext.GetUserInfoAsync<Account>();
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
        Account user = await HttpContext.GetUserInfoAsync<Account>();
        _vks.Remove(id, user.Id);
        return Ok("delorted vaultkeep");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}