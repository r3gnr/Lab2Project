using NUnit.Framework;
using System.Collections.Generic;
using System;

[TestFixture]
public class RankerTests
{
    [Test]
    public void CalculateRelevance_OneMatchingSkill_ReturnsHalf()
    {
        var vacancy = new Vacancy { Skills = new List<string> { "Python", "SQL" } };
        var ranker = new Ranker(new List<string> { "Python" });
        var result = ranker.CalculateRelevance(vacancy);
        Assert.That(result, Is.EqualTo(0.5));
    }

    [Test]
    public void CalculateRelevance_NoMatchingSkills_ReturnsZero()
    {
        var vacancy = new Vacancy { Skills = new List<string> { "Java" } };
        var ranker = new Ranker(new List<string> { "Python" });
        var result = ranker.CalculateRelevance(vacancy);
        Assert.That(result, Is.EqualTo(0));
    }

    [Test]
    public void CalculateRelevance_NullVacancy_ThrowsException()
    {
        var ranker = new Ranker(new List<string> { "Python" });
        Assert.Throws<ArgumentNullException>(() => ranker.CalculateRelevance(null));
    }

    [Test]
    public void RankList_WithMockData_ReturnsSortedList()
    {
        var mockCollector = new MockDataCollector();
        var vacancies = mockCollector.GetVacancies();
        var ranker = new Ranker(new List<string> { "C#" });
        var result = ranker.RankList(vacancies);
        Assert.That(result[0].Skills[0], Is.EqualTo("C#"));
    }

    [TestCase(new[] { "Python" }, new[] { "Python", "SQL" }, 0.5)]
    [TestCase(new[] { "Java" }, new[] { "C#" }, 0)]
    public void CalculateRelevance_ParameterizedTests(string[] userSkills, string[] vacancySkills, double expected)
    {
        var vacancy = new Vacancy { Skills = new List<string>(vacancySkills) };
        var ranker = new Ranker(new List<string>(userSkills));
        var result = ranker.CalculateRelevance(vacancy);
        Assert.That(result, Is.EqualTo(expected));
    }
}