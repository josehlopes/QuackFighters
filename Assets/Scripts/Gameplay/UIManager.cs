using UnityEngine;
using TMPro;

public class UIManager
{
    private TextMeshProUGUI _statement;
    private TextMeshProUGUI _timerText;
    private TextMeshProUGUI _currentPlayerTurnText;

    public UIManager(TextMeshProUGUI statement, TextMeshProUGUI timerText, TextMeshProUGUI currentPlayerTurnText)
    {
        _statement = statement;
        _timerText = timerText;
        _currentPlayerTurnText = currentPlayerTurnText;
    }

    #region Public Methods
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

    public void UpdateCurrentPlayerTurnText(string playerName)
    {
        if (_currentPlayerTurnText != null)
        {
            _currentPlayerTurnText.text = $"Turno de: {playerName}";
        }
        else
        {
            Debug.LogWarning("TextMeshPro component not found on 'CurrentPlayerTurn' GameObject.");
        }
    }
    #endregion
}
