namespace xBimApp.Api.Models
{
  /// <summary>
  /// Model Category
  /// </summary>
  public class ModelCategory
  {
    /// <summary>
    /// Gets or sets the quantity.
    /// </summary>
    /// <value>
    /// The quantity.
    /// </value>
    public int Quantity { get; set; }
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelCategory"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="quantity">The quantity.</param>
    public ModelCategory(string name , int quantity)
    {
      Quantity = quantity;
      Name = name;
    }
  }
}