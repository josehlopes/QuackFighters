using System.Collections;
using UnityEngine;
using TMPro;

public class Game : MonoBehaviour
{
    private Player _player1;
    private Player _player2;
    private ButtonManager _trueButton;
    private ButtonManager _falseButton;
    private Coin _coin;
    private TurnManager _turnManager;
    private QuestionManager _questionManager;
    private UIManager _uiManager;
    private bool _gameOver;

    void Awake()
    {
        InitializeComponents();
    }

    void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        _player1.Name = "Erik";
        _player2.Name = "Ícaro";
        _coin.FlipCoin();
        _turnManager.CurrentTurn = _coin.CoinValue != 0;
        _turnManager.LastTurn = !_turnManager.CurrentTurn;
        _questionManager.LoadQuestions();
        NextQuestion();
        HandleUIInformation();
    }

    void Update()
    {
        if (!_gameOver)
        {
            CheckButtonClick();
        }
        else
        {
            EndGame();
        }
    }

    void FixedUpdate()
    {
        _uiManager.UpdateTimer(_turnManager.TurnTimer);
        HandleUIInformation();
    }

    #region Private Methods
    private void InitializeComponents()
    {
        _player1 = GameObject.Find("Player1").GetComponent<Player>();
        _player2 = GameObject.Find("Player2").GetComponent<Player>();
        _trueButton = GameObject.Find("TrueButton").GetComponent<ButtonManager>();
        _falseButton = GameObject.Find("FalseButton").GetComponent<ButtonManager>();
        _questionManager = GameObject.Find("Questions").GetComponent<QuestionManager>();
        _coin = GameObject.Find("Coin").GetComponent<Coin>();
        _turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();

        TextMeshProUGUI statementText = GameObject.Find("Statement").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI turnManagerText = GameObject.Find("TurnManager").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI currentPlayerTurnText = GameObject.Find("CurrentTurn").GetComponent<TextMeshProUGUI>();

        _uiManager = new UIManager(statementText, turnManagerText, currentPlayerTurnText);

        _gameOver = false;

        if (_player1 == null || _player2 == null || _trueButton == null || _falseButton == null || _questionManager == null || _coin == null || _turnManager == null || _uiManager == null)
        {
            Debug.LogError("Um ou mais componentes não foram encontrados. Certifique-se de que todos os componentes necessários estão na cena e possuem os nomes e tags corretos.");
        }
    }

    public void NextQuestion()
    {
        _uiManager.UpdateStatement(_questionManager.GetNextQuestion());
    }

    public void CheckButtonClick()
    {
        bool? answer = null;

        if (_trueButton.IsPressed)
        {
            answer = true;
            _trueButton.Release();
        }
        else if (_falseButton.IsPressed)
        {
            answer = false;
            _falseButton.Release();
        }

        if (answer.HasValue)
        {
            bool isCorrect = _questionManager.CheckAnswer(answer.Value);
            HandleAttackOrDefense(isCorrect);
            StartCoroutine(WaitAndLoadNextQuestion(1f));
            _turnManager.SwapTurn();
            HandleUIInformation();
        }
    }

    private void HandleUIInformation()
    {
        if (_turnManager.LastTurn != _turnManager.CurrentTurn)
        {
            _turnManager.LastTurn = _turnManager.CurrentTurn;
            _uiManager.UpdateCurrentPlayerTurnText(_turnManager.CurrentTurn ? _player1.Name : _player2.Name);
        }
    }

    private void HandleAttackOrDefense(bool isCorrect)
    {
        if (isCorrect)
        {
            if (_turnManager.CurrentTurn)
            {
                _player1.Attack(_player2);
            }
            else
            {
                _player2.Defense(_player1);
            }
        }
        else
        {
            if (_turnManager.CurrentTurn)
            {
                _player2.Attack(_player1);
            }
            else
            {
                _player1.Defense(_player2);
            }
        }
        CheckWinCondition();
    }

    private IEnumerator WaitAndLoadNextQuestion(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        NextQuestion();
    }

    private void CheckWinCondition()
    {
        if (_player1.Health <= 0)
        {
            Debug.Log("Player 2 venceu!");
            _gameOver = true;
        }
        else if (_player2.Health <= 0)
        {
            Debug.Log("Player 1 venceu!");
            _gameOver = true;
        }
    }

    private void EndGame()
    {
        Debug.Log("Fim do jogo. Parabéns ao vencedor!");
    }
    #endregion
}
