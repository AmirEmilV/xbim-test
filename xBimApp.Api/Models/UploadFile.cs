namespace xBimApp.Api.Services
{
  /// <summary>
  /// Upload File Request
  /// </summary>
  public class UploadFileRequest
  {

    /// <summary>
    /// Gets or sets the ifc file path.
    /// </summary>
    /// <value>
    /// The ifc file path.
    /// </value>
    public string IFCFilePath { get; set; }
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
  public class UploadFileResponse
  {

    /// <summary>
    /// Gets or sets a value indicating whether this instance is succes.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is succes; otherwise, <c>false</c>.
    /// </value>
    public bool IsSucces { get; set; }
    /// <summary>
    /// Gets or sets the name of the file.
    /// </summary>
    /// <value>
    /// The name of the file.
    /// </value>
    public string FileName { get; set; }

  }
}
