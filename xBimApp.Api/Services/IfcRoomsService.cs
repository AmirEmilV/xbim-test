using Xbim.Ifc;
using Xbim.Ifc4.ProductExtension;
using xBimApp.Api.Helpers;
using xBimApp.Api.Models;

namespace xBimApp.Api.Services
{
  /// <summary>
  /// IIfc Rooms Service
  /// </summary>
  public interface IIfcRoomsService
  {
    /// <summary>
    /// Gets the model rooms.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    Task<ModelRoomsResponse> GetModelRooms(string fileName);
  }
  /// <summary>
  /// Ifc Rooms Service
  /// </summary>
  /// <seealso cref="xBimApp.Api.Services.IIfcRoomsService" />
  public class IfcRoomsService : IIfcRoomsService
  {
    #region Private Members

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
    /// Initializes a new instance of the <see cref="IfcRoomsService"/> class.
    /// </summary>
    /// <param name="enviromentSetting">The enviroment setting.</param>
    /// <param name="awsService">The aws service.</param>
    public IfcRoomsService( EnviromentSetting enviromentSetting, IAWSService awsService)
    {
      _awsService = awsService;
      _enviromentSetting = enviromentSetting;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Gets the model rooms.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns></returns>
    public async Task<ModelRoomsResponse> GetModelRooms(string fileName)
    {
      var response = new ModelRoomsResponse();

      var stream = await _awsService.GetFile(_enviromentSetting.AwsS3Settings.BucketName, fileName);

      string filePath = FileHandler.SaveFileTemp(stream, fileName);

      using (var model = IfcStore.Open(filePath))
      {
        var rooms = model.Instances.OfType<IfcSpace>().Select(x => new ModelRoom(x.Name, x.GrossFloorArea.Value, x.NetFloorArea.Value)).ToList();
        response.ModelRooms.AddRange(rooms);
      }

      //using (var model = IfcStore.Open(/*isDevelopment ? _appSettings.IFCFilePath:*/stream, Xbim.IO.StorageType.))

      return response;
    }

    #endregion

  }
}
