namespace xBimApp.Api.Models
{
  /// <summary>
  /// Model Elements esponse
  /// </summary>
  public class ModelElementsResponse
  {
    /// <summary>
    /// Gets or sets the model categories.
    /// </summary>
    /// <value>
    /// The model categories.
    /// </value>
    public List<ModelCategory> ModelCategories { get; set; } = new List<ModelCategory>();
  }
}
