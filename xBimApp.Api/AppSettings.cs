namespace XBimApp.Api
{

  /// <summary>
  /// App Settings
  /// </summary>
  public class AppSettings
  {
    /// <summary>
    /// Gets or sets the connection string.
    /// </summary>
    /// <value>
    /// The connection string.
    /// </value>
    public string ConnectionString { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the ifc file path.
    /// </summary>
    /// <value>
    /// The ifc file path.
    /// </value>
    public string IFCFilePath { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the name of the database.
    /// </summary>
    /// <value>
    /// The name of the database.
    /// </value>
    public string DatabaseName { get; set; } = string.Empty;
    /// <summary>
    /// Gets or sets the token key.
    /// </summary>
    /// <value>
    /// The token key.
    /// </value>
    public string TokenKey { get; set; } = string.Empty;

  }

}
