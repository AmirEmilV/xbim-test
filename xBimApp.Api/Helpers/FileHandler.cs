namespace xBimApp.Api.Helpers
{
  /// <summary>
  /// File Handler
  /// </summary>
  public static class FileHandler
  {

    #region Functions 

    /// <summary>
    /// Saves the file temporary.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="FileName">Name of the file.</param>
    /// <returns></returns>
    public static string SaveFileTemp(byte[] data,string FileName)
    {
      var path = Path.GetTempPath();

      var filePath = Path.Join(path, @$"/{FileName}");
      using (var fileStream = File.Create(filePath))
      {
        fileStream.Write(data,0, data.Length);
        fileStream.Close();
      }

      return filePath;
    }

    #endregion

  }
}
