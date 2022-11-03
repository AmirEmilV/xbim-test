using xBimApp.Api;
using xBimApp.Api.Services;

namespace XBimApp.Api.Extentions
{
  /// <summary>
  /// Application Service Extensions
  /// </summary>
  public static class ApplicationServiceExtensions
  {
    #region Extenstion Method

    /// <summary>
    /// Adds the services.
    /// </summary>
    /// <param name="services">The services.</param>
    public static void AddServices(this IServiceCollection services)
    {

      var envSettings = new EnviromentSetting
      {
     
        AwsS3Settings = new AwsS3Settings
        {
          AccessKeyId = Environment.GetEnvironmentVariable("AWS_S3_ACCESS_KEY_ID"),
          SecretAccessKey = Environment.GetEnvironmentVariable("AWS_S3_SECRET_ACCESS_KEY"),
          BucketName = Environment.GetEnvironmentVariable("AWS_S3_BUCKET_NAME")
        }
      };
      services.AddSingleton(envSettings);

      services.AddSingleton<IAWSService, AWSService>();
      services.AddScoped<IIfcFileService, IfcFileService>();

      services.AddScoped<IModelElementsService, ModelElementsService>();
      services.AddScoped<IIfcRoomsService, IfcRoomsService>();


    }

    #endregion
  
  }
}
