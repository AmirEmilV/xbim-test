using Microsoft.Extensions.Options;
using XBimApp.Api;

namespace xBimApp.Api.Services
{
  /// <summary>
  /// IIfcFile Service
  /// </summary>
  public interface IIfcFileService
  {
    /// <summary>
    /// Uploads the ifc file.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    Task<UploadFileResponse> UploadIfcFile(UploadFileRequest filePath);
    /// <summary>
    /// Loads the ifc file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns></returns>
    Task<LoadFileResponse> LoadIfcFile(string fileName);
  }
  /// <summary>
  /// IfcFile Service
  /// </summary>
  /// <seealso cref="xBimApp.Api.Services.IIfcFileService" />
  public class IfcFileService : IIfcFileService
  {

    #region Private Members 

    /// <summary>
    /// The enviroment setting
    /// </summary>
    private readonly EnviromentSetting _enviromentSetting;
    /// <summary>
    /// The aws service
    /// </summary>
    private readonly IAWSService _awsService;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="IfcFileService"/> class.
    /// </summary>
    /// <param name="enviromentSetting">The enviroment setting.</param>
    /// <param name="awsService">The aws service.</param>
    public IfcFileService( EnviromentSetting enviromentSetting, IAWSService awsService)
    {
      _enviromentSetting = enviromentSetting;
      _awsService = awsService;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Uploads the ifc file.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    public async Task<UploadFileResponse> UploadIfcFile(UploadFileRequest request)
    {
      var exists = await _awsService.IsObjectExists(_enviromentSetting.AwsS3Settings.BucketName, request.FileName);

      if (!exists)
      {

        await _awsService.UploadFileAsync(_enviromentSetting.AwsS3Settings!.BucketName, request.IFCFilePath, request.FileName);
        return new UploadFileResponse
        {
          IsSucces = true,
          FileName = request.FileName
        };
      }
      else
        return new UploadFileResponse
        {
          IsSucces = false,
          FileName = request.FileName
        };
    }

    /// <summary>
    /// Loads the ifc file.
    /// </summary>
    /// <param name="fileName">Name of the file.</param>
    /// <returns></returns>
    public async Task<LoadFileResponse> LoadIfcFile(string fileName)
    {

      var exists = await _awsService.IsObjectExists(_enviromentSetting.AwsS3Settings.BucketName, fileName);
      if (exists)
      {
        var file = await _awsService.GetFile(_enviromentSetting.AwsS3Settings.BucketName, fileName);
        return new LoadFileResponse()
        {
          File = file,
        };
      }
      else
        return null;
    }

    #endregion
  }
}
