using Microsoft.AspNetCore.Mvc;
using xBimApp.Api.Services;

namespace xBimApp.Api.Controllers
{
  /// <summary>
  /// Model Elements Controller
  /// </summary>
  /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
  [ApiController]
  [Route("/")]
  public class ModelElementsController : ControllerBase
  {
    #region Private Fields

    /// <summary>
    /// The model elements service
    /// </summary>
    private readonly IModelElementsService _modelElementsService;

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelElementsController"/> class.
    /// </summary>
    /// <param name="modelElementsService">The model elements service.</param>
    public ModelElementsController( IModelElementsService modelElementsService)
    {
      _modelElementsService = modelElementsService;
    }

    #endregion

    #region Apis

    /// <summary>
    /// Gets the elements.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    [HttpGet]
    [Route("elements")]
    public async Task<IActionResult> GetElements(string fileName)
    {

      var response =await _modelElementsService.GetModelElements(fileName);
      return Ok(new
      {
        status = "Success",
        message = $"{response.ModelCategories.Count} categories found!",
        data = response
      });

    }

    #endregion
  
  }
}
