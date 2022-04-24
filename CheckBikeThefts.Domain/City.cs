namespace CheckBikeThefts.Domain;

/// <summary>
/// City domain entity
/// </summary>
public class City
{
    public City()
    {
        Name = string.Empty;
        Country = string.Empty;
    }

    /// <summary>
    /// Id of the city
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the city
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Country of the city
    /// </summary>
    public string Country { get; set; }

    /// <summary>
    /// If true, then we already operate in the city. Otherwise, false
    /// </summary>
    public bool CurrentlyOperate { get; set; }
}