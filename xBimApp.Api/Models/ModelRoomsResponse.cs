namespace xBimApp.Api.Models
{
  /// <summary>
  /// Model Rooms Response
  /// </summary>
  public class ModelRoomsResponse
  {
    /// <summary>
    /// Gets or sets the model rooms.
    /// </summary>
    /// <value>
    /// The model rooms.
    /// </value>
    public List<ModelRoom> ModelRooms { get; set; } = new List<ModelRoom>();
  }
}
