namespace xBimApp.Api
{
  /// <summary>
  /// Enviroment Setting
  /// </summary>
  public class EnviromentSetting
  {
    /// <summary>
    /// Gets or sets the aws s3 settings.
    /// </summary>
    /// <value>
    /// The aws s3 settings.
    /// </value>
    public AwsS3Settings? AwsS3Settings { get; set; }
  }

  /// <summary>
  /// AwsS3 Settings
  /// </summary>
  public class AwsS3Settings
  {
    /// <summary>
    /// Gets or sets the access key identifier.
    /// </summary>
    /// <value>
    /// The access key identifier.
    /// </value>
    public string AccessKeyId { get; set; }
    /// <summary>
    /// Gets or sets the secret access key.
    /// </summary>
    /// <value>
    /// The secret access key.
    /// </value>
    public string SecretAccessKey { get; set; }
    /// <summary>
    /// Gets or sets the name of the bucket.
    /// </summary>
    /// <value>
    /// The name of the bucket.
    /// </value>
    public string BucketName { get; set; }

  }

}
