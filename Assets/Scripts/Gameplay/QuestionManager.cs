using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestionManager : MonoBehaviour
{
    public string jsonFilePath = "Assets/Data/questions.json";
    private QuestionData _questionData;
    private List<Question> _usedQuestions;
    private Question _currentQuestion;

    void Awake()
    {
        _usedQuestions = new List<Question>();
    }

    public List<Question> LoadQuestions()
    {
        string fullPath = Path.GetFullPath(jsonFilePath);
        Debug.Log("Tentando carregar o arquivo JSON em: " + fullPath);

        if (File.Exists(fullPath))
        {
            string dataAsJson = File.ReadAllText(fullPath);
            _questionData = JsonUtility.FromJson<QuestionData>(dataAsJson);
            Debug.Log("Arquivo JSON carregado com sucesso");

            return _questionData.Questions;
        }
        else
        {
            Debug.LogError("Não foi possível encontrar o arquivo");
            return null;
        }
    }

    public Question GetNextQuestion()
    {
        if (_questionData.Questions.Count > 0)
        {
            int randomIndex = Random.Range(0, _questionData.Questions.Count);
            _currentQuestion = _questionData.Questions[randomIndex];
            _questionData.Questions.RemoveAt(randomIndex);
            _usedQuestions.Add(_currentQuestion);
            return _currentQuestion;
        }
        else
        {
            return null;
        }
    }

    public Question GetCurrentQuestion()
    {
        return _currentQuestion;
    }

    public List<Question> GetUsedQuestions()
    {
        return _usedQuestions;
    }

    public void ResetUsedQuestions()
    {
        _usedQuestions = new List<Question>();
    }
    public bool CheckAnswer(bool playerResponse)
    {
        return _currentQuestion.Answer == playerResponse;
    }
}
