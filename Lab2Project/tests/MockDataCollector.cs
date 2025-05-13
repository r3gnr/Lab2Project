using System.Collections.Generic;

public class MockDataCollector
{
    public List<Vacancy> GetVacancies()
    {
        return new List<Vacancy>
        {
            new Vacancy { Name = "Intern Python", Skills = new List<string> { "Python", "SQL" } },
            new Vacancy { Name = "Intern C#", Skills = new List<string> { "C#", "OOP" } }
        };
    }
}