using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Question
{
    public int Id;             // Adicione este campo se você estiver usando IDs
    public string Text;
    public bool Answer;        // Atualize o tipo para bool
    public string Hint;
    public int Points;
    public string Category;
    public int Level;
    public string Language;
}


public class QuestionData
{
    public List<Question> Questions;
}