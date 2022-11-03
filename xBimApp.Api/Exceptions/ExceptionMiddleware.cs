using System.Net;
using System.Text.Json;

namespace xBimApp.Api.Exceptions
{
  /// <summary>
  /// Exception Middleware
  /// </summary>
  public class ExceptionMiddleware
  {
    #region Private Fields

    /// <summary>
    /// The next
    /// </summary>
    private readonly RequestDelegate _next;
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<ExceptionMiddleware> _logger;
    /// <summary>
    /// The env
    /// </summary>
    private readonly IHostEnvironment _env;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="env">The env.</param>
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
      this._next = next;
      this._logger = logger;
      this._env = env;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Invokes the asynchronous.
    /// </summary>
    /// <param name="context">The context.</param>
    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, ex.Message);
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = _env.IsDevelopment() ?
          new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) :
          new ApiException(context.Response.StatusCode, "Internal Server Error");

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        var json = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(json);
      }
    }

    #endregion

  }
}
