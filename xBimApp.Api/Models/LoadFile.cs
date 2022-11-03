namespace xBimApp.Api.Services
{
  /// <summary>
  /// Load File Request
  /// </summary>
  public class LoadFileRequest
  {
    /// <summary>
    /// Gets or sets the name of the file.
    /// </summary>
    /// <value>
    /// The name of the file.
    /// </value>
    public string FileName { get; set; }
  }
  /// <summary>
  /// 
  /// </summary>
  public class LoadFileResponse
  {
    /// <summary>
    /// Gets or sets the file.
    /// </summary>
    /// <value>
    /// The file.
    /// </value>
    public byte[] File { get; set; }
  }
}
