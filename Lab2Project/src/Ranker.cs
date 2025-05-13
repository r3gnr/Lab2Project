using System.Collections.Generic;
using System.Linq;
using System;

public class Ranker
{
    private readonly List<string> _userSkills;

    public Ranker(List<string> userSkills)
    {
        _userSkills = userSkills ?? new List<string>();
    }

    public double CalculateRelevance(Vacancy vacancy)
    {
        if (vacancy == null)
            throw new ArgumentNullException(nameof(vacancy));

        if (_userSkills.Count == 0 || vacancy.Skills.Count == 0)
            return 0;

        int matches = vacancy.Skills.Intersect(_userSkills).Count();
        return (double)matches / vacancy.Skills.Count;
    }

    public List<Vacancy> RankList(List<Vacancy> vacancies)
    {
        if (vacancies == null)
            throw new ArgumentNullException(nameof(vacancies));

        // Обновляем Relevance для каждой вакансии
        foreach (var vacancy in vacancies)
        {
            vacancy.Relevance = CalculateRelevance(vacancy);
        }

        // Затем сортируем по обновленному свойству
        return vacancies
            .OrderByDescending(v => v.Relevance)
            .ToList();
    }
}