using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    private Player _player1 { get; set; }
    private Player _player2 { get; set; }

    private ButtonManager _trueButton { get; set; }
    private ButtonManager _falseButton { get; set; }
    private Coin _coin { get; set; }
    private TurnManager _turnManager { get; set; }

    private QuestionManager _questionManager { get; set; }

    private List<Question> _questions { get; set; }
    private List<Question> _usedQuestions { get; set; }

    private Question _currentQuestion { get; set; }
    private bool _currentTurn { get; set; }
    private TextMeshProUGUI _statement { get; set; }

    void Awake()
    {
        _player1 = GameObject.Find("Player1").GetComponent<Player>();
        _player2 = GameObject.Find("Player2").GetComponent<Player>();
        _trueButton = GameObject.Find("TrueButton").GetComponent<ButtonManager>();
        _falseButton = GameObject.Find("FalseButton").GetComponent<ButtonManager>();
        _questionManager = GameObject.Find("Questions").GetComponent<QuestionManager>();
        _usedQuestions = new List<Question>();
        _coin = GameObject.Find("Coin").GetComponent<Coin>();
        _statement = GameObject.Find("Statement").GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        _coin.FlipCoin();
        _currentTurn = _coin.CoinValue != 0;
        _questions = _questionManager.LoadQuestions();

        NextQuestion();
    }

    void Update()
    {
        if (_player1.Health <= 0 || _player2.Health <= 0)
        {
            if (_player1.Health <= 0)
            {
                Debug.Log("Player 2 venceu!");
            }
            else
            {
                Debug.Log("Player 1 venceu!");
            }
            CheckButtonClick();
        }
    }
    private bool CheckAnswer(bool playerResponse)
    {
        bool isCorrect = (_currentQuestion.Answer == playerResponse);
        if (isCorrect)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void NextQuestion()
    {
        if (_questions.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, _questions.Count);
            _currentQuestion = _questions[randomIndex];
            _questions.RemoveAt(randomIndex);
            _usedQuestions.Add(_currentQuestion);

            if (_statement != null)
            {
                _statement.text = _currentQuestion.Text;
            }
            else
            {
                Debug.LogWarning("TextMeshPro component not found on 'Statement' GameObject.");
            }
        }
        else
        {
            if (_statement != null)
            {
                _statement.text = "No questions available.";
            }
            else
            {
                Debug.LogWarning("TextMeshPro component not found on 'Statement' GameObject.");
            }
        }
    }

    public void CheckButtonClick()
    {
        if (_trueButton.IsPressed)
        {
            bool isCorrect = CheckAnswer(true);
            _trueButton.Release();
            if (isCorrect)
            {
                if (_currentTurn)
                {
                    _player1.Attack(_player2);
                }
                else
                {
                    _player2.Attack(_player1);
                }
            }
            StartCoroutine(WaitAndLoadNextQuestion(1f));
        }
        else if (_falseButton.IsPressed)
        {
            bool isCorrect = CheckAnswer(false);
            _falseButton.Release();
            if (isCorrect)
            {
                if (_currentTurn)
                {
                    _player1.Attack(_player2);
                }
                else
                {
                    _player2.Attack(_player1);
                }
            }
            StartCoroutine(WaitAndLoadNextQuestion(1f));
        }
    }

    private IEnumerator WaitAndLoadNextQuestion(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NextQuestion();
    }
}
