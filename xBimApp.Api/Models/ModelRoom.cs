namespace xBimApp.Api.Models
{

  /// <summary>
  /// Model Room
  /// </summary>
  public class ModelRoom
  {

    #region Properties

    /// <summary>
    /// Gets or sets the gross area.
    /// </summary>
    /// <value>
    /// The gross area.
    /// </value>
    public double GrossArea { get; set; }
    /// <summary>
    /// Gets or sets the net area.
    /// </summary>
    /// <value>
    /// The net area.
    /// </value>
    public double NetArea { get; set; }
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name { get; set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="ModelRoom"/> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="grossArea">The gross area.</param>
    /// <param name="netArea">The net area.</param>
    public ModelRoom(string name , double grossArea, double netArea)
    {
      GrossArea = grossArea;
      NetArea = netArea;
      Name = name;
    }

    #endregion

  }

}