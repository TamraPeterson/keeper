using keeper.Models;
using keeper.Services;
using Microsoft.AspNetCore.Mvc;

mespace keeper.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProfilesController : ControllerBase
{
  private readonly VaultsService _vs;
  private readonly KeepsService _ks;

  public ProfilesController(VaultsService vs, KeepsService ks)
  {
    _vs = vs;
    _ks = ks;
  }

  [HttpGet("{id}/keeps")]
  public ActionResult<List<Keep>> GetProfileKeeps(int id)
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
}
}