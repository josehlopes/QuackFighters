using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class QuestionManager : MonoBehaviour
{
    public string jsonFilePath = "Assets/Data/questions.json";
    private QuestionData questionData;
    public List<Question> LoadQuestions()
    {
        string fullPath = Path.GetFullPath(jsonFilePath);
        Debug.Log("Tentando carregar o arquivo JSON em: " + fullPath);

        if (File.Exists(fullPath))
        {
            string dataAsJson = File.ReadAllText(fullPath);
            questionData = JsonUtility.FromJson<QuestionData>(dataAsJson);
            Debug.Log("Arquivo JSON carregado com sucesso");

            // foreach (var question in questionData.Questions)
            // {
            //     Debug.Log("Pergunta: " + question.Text + " - Resposta Correta: " + question.Answer);
            // }

            return questionData.Questions;
        }
        else
        {
            Debug.LogError("Não foi possível encontrar o arquivo");
            return null;
        }
    }

    public Question SelectQuestion()
    {
        return questionData.Questions[Random.Range(0, questionData.Questions.Count)];
    }
}