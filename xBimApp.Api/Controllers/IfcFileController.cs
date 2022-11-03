using Microsoft.AspNetCore.Mvc;
using xBimApp.Api.Services;

namespace xBimApp.Api.Controllers
{
  /// <summary>
  /// Ifc File Controller
  /// </summary>
  /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
  [ApiController]
  [Route("/")]
  public class IfcFileController : ControllerBase
  {

    #region Private Fields

    /// <summary>
    /// The ifc file service
    /// </summary>
    private readonly IIfcFileService _ifcFileService;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="IfcFileController"/> class.
    /// </summary>
    /// <param name="ifcFileService">The ifc file service.</param>
    public IfcFileController(IIfcFileService ifcFileService)
    {
      _ifcFileService = ifcFileService;
    }

    #endregion

    #region Apis

    /// <summary>
    /// Uploads the i fc file.
    /// </summary>
    /// <param name="uploadFileRequest">The upload file request.</param>
    /// <returns></returns>
    [HttpPost]
    [Consumes("application/json", "multipart/form-data")]
    [Route("/ifc")]
    public async Task<IActionResult> UploadIFcFile([FromBody] UploadFileRequest uploadFileRequest)
    {
      try
      {
        var response = await _ifcFileService.UploadIfcFile(uploadFileRequest);

        if (response != null && response.IsSucces)
          return Ok(new
          {
            status = "Success",
            message = $"uploaded!",
            data = response
          });
        else
          return StatusCode(500, new { status = "Error", Message = "File with the same name already exist." });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new
        {
          status = "Error",
          ex.Message
        });
      }

    }


    /// <summary>
    /// Loads the i fc file.
    /// </summary>
    /// <param name="FileName">Name of the file.</param>
    /// <returns></returns>
    [HttpGet]
    [Route("/ifc")]
    public async Task<IActionResult> LoadIFcFile(string FileName)
    {
      try
      {
        
        var response = await _ifcFileService.LoadIfcFile(FileName);

        if (response != null)
        {
          return Ok(new
          {
            status = "Success",
            message = $"loaded!",
            data = response,
          });
        }
        else
          return StatusCode(500, new { status = "Error", Message = "File doesn't exist." });
      }
      catch (Exception ex)
      {
        return StatusCode(500, new
        {
          status = "Error",
          ex.Message
        });
      }

    }

    #endregion

  }
}
