using System.Collections.Generic;

public class MockDataCollector : IDataCollector
{
    public List<Vacancy> GetVacancies()
    {
        return new List<Vacancy>
            {
                new Vacancy { Skills = new List<string> { "C#", "SQL" } },
                new Vacancy { Skills = new List<string> { "Python" } }
            };
    }
}