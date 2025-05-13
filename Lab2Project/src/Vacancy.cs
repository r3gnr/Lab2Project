using System.Collections.Generic;

public class Vacancy
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<string> Skills { get; set; } = new List<string>();
    public string Link { get; set; }
    public double Relevance { get; set; }
}