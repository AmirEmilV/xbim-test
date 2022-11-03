using Microsoft.AspNetCore.Mvc;
using xBimApp.Api.Services;

namespace xBimApp.Api.Controllers
{
  /// <summary>
  /// Ifc Rooms Controller
  /// </summary>
  /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
  [ApiController]
  [Route("/")]
  public class IfcRoomsController : ControllerBase
  {
    #region Private Fields

    /// <summary>
    /// The ifc rooms service
    /// </summary>
    private readonly IIfcRoomsService _ifcRoomsService;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="IfcRoomsController"/> class.
    /// </summary>
    /// <param name="ifcRoomsService">The ifc rooms service.</param>
    public IfcRoomsController( IIfcRoomsService ifcRoomsService)
    {
      _ifcRoomsService = ifcRoomsService;
    }

    #endregion

    #region Apis

    /// <summary>
    /// Gets the rooms.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    [HttpGet]
    [Route("rooms")]
    public async Task<IActionResult> GetRooms(string fileName)
    {
      var response = await _ifcRoomsService.GetModelRooms(fileName);
      return Ok(new
      {
        status = "Success",
        message = $"{response.ModelRooms.Count} rooms found!",
        data = response
      });
    }

    #endregion
  
  }

}
