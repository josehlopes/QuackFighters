using UnityEngine;
using TMPro;

public class UIManager
{
    private TextMeshProUGUI _statement;
    private TextMeshProUGUI _timerText;


    public UIManager(TextMeshProUGUI statement, TextMeshProUGUI timerText)
    {
        _statement = statement;
        _timerText = timerText;
    }

    public void UpdateStatement(Question question)
    {
        if (_statement != null)
        {
            _statement.text = question != null ? question.Text : "No questions available.";
        }
        else
        {
            Debug.LogWarning("TextMeshPro component not found on 'Statement' GameObject.");
        }
    }

    public void UpdateTimer(int seconds)
    {
        _timerText.text = seconds.ToString();
    }
}
