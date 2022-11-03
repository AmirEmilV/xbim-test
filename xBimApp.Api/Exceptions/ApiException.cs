namespace xBimApp.Api.Exceptions
{
  /// <summary>
  /// Api Exception
  /// </summary>
  public class ApiException
  {

    #region Public Properties

    /// <summary>
    /// Gets or sets the status code.
    /// </summary>
    /// <value>
    /// The status code.
    /// </value>
    public int StatusCode { get; set; }
    /// <summary>
    /// Gets or sets the message.
    /// </summary>
    /// <value>
    /// The message.
    /// </value>
    public string Message { get; set; }
    /// <summary>
    /// Gets or sets the details.
    /// </summary>
    /// <value>
    /// The details.
    /// </value>
    public string Details { get; set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="ApiException"/> class.
    /// </summary>
    /// <param name="statusCode">The status code.</param>
    /// <param name="message">The message.</param>
    /// <param name="details">The details.</param>
    public ApiException(int statusCode, string message = null, string details = null)
    {
      StatusCode = statusCode;
      Message = message;
      Details = details;
    }

    #endregion
  
  }

}
