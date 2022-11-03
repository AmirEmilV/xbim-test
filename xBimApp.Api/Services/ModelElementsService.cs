using Microsoft.Extensions.Options;
using Xbim.Ifc;
using Xbim.Ifc4.ProductExtension;
using xBimApp.Api.Helpers;
using xBimApp.Api.Models;
using XBimApp.Api;

namespace xBimApp.Api.Services
{
  /// <summary>
  /// IModel Elements Service
  /// </summary>
  public interface IModelElementsService
  {
    /// <summary>
    /// Gets the model elements.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    Task<ModelElementsResponse> GetModelElements(string fileName);
  }
  /// <summary>
  /// Model Elements Service
  /// </summary>
  /// <seealso cref="xBimApp.Api.Services.IModelElementsService" />
  public class ModelElementsService : IModelElementsService
  {
    #region Private Members

    /// <summary>
    /// The application settings
    /// </summary>
    private readonly AppSettings _appSettings;
    /// <summary>
    /// The aws service
    /// </summary>
    private readonly IAWSService _awsService;
    /// <summary>
    /// The enviroment setting
    /// </summary>
    private readonly EnviromentSetting _enviromentSetting;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelElementsService"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <param name="enviromentSetting">The enviroment setting.</param>
    /// <param name="awsService">The aws service.</param>
    public ModelElementsService(IOptions<AppSettings> options, EnviromentSetting enviromentSetting, IAWSService awsService)
    {
      _appSettings = options.Value;
      _awsService = awsService;
      _enviromentSetting = enviromentSetting;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets the model elements.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns></returns>
    public async Task<ModelElementsResponse> GetModelElements(string fileName)
    {
      var response = new ModelElementsResponse();


      var stream = await _awsService.GetFile(_enviromentSetting.AwsS3Settings.BucketName, fileName);

      string filePath = FileHandler.SaveFileTemp(stream, fileName);

      using (var model = IfcStore.Open(filePath))
      {
        var grps = model.Instances.OfType<IfcElement>().ToList().GroupBy(x => x.GetPropertySingleValue("Other", "Category"), x => x);
        foreach (var grp in grps)
        {
          if(grp.Key != null)
          {
            response.ModelCategories.Add(new ModelCategory(grp.Key.NominalValue.ToString()??"Undefined", grp.Count()));
          }
        }
      }
      return response;

    }

    #endregion

  }
}
