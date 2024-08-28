using System;

public class QuestionFactory
{

    public Question Create(int id, string text, bool answer, string hint, int points, string category, int level, string language)
    {
        return new Question() { Id = id, Text = text, Answer = answer, Hint = hint, Points = points, Category = category, Level = level, Language = language };
    }
}