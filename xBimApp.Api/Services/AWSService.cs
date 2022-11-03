using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace xBimApp.Api.Services
{
  /// <summary>
  /// IAWS Service
  /// </summary>
  public interface IAWSService
  {
    /// <summary>
    /// Uploads the file asynchronous.
    /// </summary>
    /// <param name="bucketName">Name of the bucket.</param>
    /// <param name="filePath">The file path.</param>
    /// <param name="fileName">Name of the file.</param>
    /// <returns></returns>
    Task UploadFileAsync(string bucketName, string filePath, string fileName);
    /// <summary>
    /// Gets the file.
    /// </summary>
    /// <param name="bucketName">Name of the bucket.</param>
    /// <param name="fileKey">The file key.</param>
    /// <returns></returns>
    Task<byte[]> GetFile(string bucketName, string fileKey);
    /// <summary>
    /// Determines whether [is object exists] [the specified bucket name].
    /// </summary>
    /// <param name="bucketName">Name of the bucket.</param>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    Task<bool> IsObjectExists(string bucketName, string key);
  }
  /// <summary>
  /// AWS Service
  /// </summary>
  /// <seealso cref="xBimApp.Api.Services.IAWSService" />
  public class AWSService : IAWSService
  {
    #region Private Members

    /// <summary>
    /// The client
    /// </summary>
    private readonly IAmazonS3 _client;
    /// <summary>
    /// The env settings
    /// </summary>
    private readonly EnviromentSetting? _envSettings;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="AWSService"/> class.
    /// </summary>
    /// <param name="envSettings">The env settings.</param>
    public AWSService(EnviromentSetting envSettings)
    {
      _envSettings = envSettings;
      _client = new AmazonS3Client(_envSettings?.AwsS3Settings?.AccessKeyId,
        _envSettings?.AwsS3Settings?.SecretAccessKey, RegionEndpoint.USEast1);
    }

    #endregion

    #region Methods

    /// <summary>
    /// Uploads the file asynchronous.
    /// </summary>
    /// <param name="bucketName">Name of the bucket.</param>
    /// <param name="filePath">The file path.</param>
    /// <param name="fileName">Name of the file.</param>
    public async Task UploadFileAsync(string bucketName, string filePath, string fileName)
    {
      try
      {
        using (var newMemoryStream = new MemoryStream())
        {
          FileStream stream = new FileStream(filePath, FileMode.Open);


          stream.CopyTo(newMemoryStream);
          var uploadRequest = new Amazon.S3.Transfer.TransferUtilityUploadRequest
          {
            InputStream = newMemoryStream,
            BucketName = bucketName,
            Key = fileName,
            ContentType = "ifc/ifc"
          };
          var fileTransferUtility = new Amazon.S3.Transfer.TransferUtility(_client);

          await fileTransferUtility.UploadAsync(uploadRequest);
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex);
      }
    }

    /// <summary>
    /// Gets the file.
    /// </summary>
    /// <param name="bucketName">Name of the bucket.</param>
    /// <param name="fileKey">The file key.</param>
    /// <returns></returns>
    /// <exception cref="System.IO.FileNotFoundException"></exception>
    public async Task<byte[]> GetFile(string bucketName, string fileKey)
    {
      GetObjectResponse response = await _client.GetObjectAsync(bucketName, fileKey);


     if ((int)response.HttpStatusCode == 404)
        throw new FileNotFoundException();


      MemoryStream memoryStream = new MemoryStream();

      using (Stream responseStream = response.ResponseStream)
      {
        responseStream.CopyTo(memoryStream);
      }

      return memoryStream.ToArray();
    }

    /// <summary>
    /// Determines whether [is object exists] [the specified bucket name].
    /// </summary>
    /// <param name="bucketName">Name of the bucket.</param>
    /// <param name="key">The key.</param>
    /// <returns>
    ///   <c>true</c> if [is object exists] [the specified bucket name]; otherwise, <c>false</c>.
    /// </returns>
    public async Task<bool> IsObjectExists(string bucketName, string key)
    {
      try
      {
        await _client.GetObjectMetadataAsync(bucketName, key);

        return true;
      }
      catch (AmazonS3Exception ex)
      {
        if (string.Equals(ex.ErrorCode, "NotFound"))
          return false;

        throw ex;
      }
    }

    #endregion
  }
}
