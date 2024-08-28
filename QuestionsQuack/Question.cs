using System;

public class Question : QuestionAbstract
{
    public override int Id { get; set; }
    public override string Text { get; set; }
    public override bool Answer { get; set; }
    public override string Hint { get; set; }
    public override int Points { get; set; }
    public override string Category { get; set; }
    public override int Level { get; set; }
    public override string Language { get; set; }
}