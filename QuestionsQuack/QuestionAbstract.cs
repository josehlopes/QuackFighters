using System;

public abstract class QuestionAbstract
{
    public abstract int Id { get; set; }
    public abstract string Text { get; set; }
    public abstract bool Answer { get; set; }
    public abstract string Hint { get; set; }
    public abstract int Points { get; set; }
    public abstract string Category { get; set; }
    public abstract int Level { get; set; }
    public abstract string Language { get; set; }
}

