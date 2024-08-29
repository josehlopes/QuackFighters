using System.Collections;
using System.Collections.Generic;
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
        _player1 = GameObject.Find("Player1").GetComponent<Player>();
        _player2 = GameObject.Find("Player2").GetComponent<Player>();
        _trueButton = GameObject.Find("TrueButton").GetComponent<ButtonManager>();
        _falseButton = GameObject.Find("FalseButton").GetComponent<ButtonManager>();
        _questionManager = GameObject.Find("Questions").GetComponent<QuestionManager>();
        _coin = GameObject.Find("Coin").GetComponent<Coin>();
        _uiManager = new UIManager(GameObject.Find("Statement").GetComponent<TextMeshProUGUI>(), GameObject.Find("TurnManager").GetComponent<TextMeshProUGUI>());
        _turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        _gameOver = false;
    }

    void Start()
    {
        _player1.Name = "Player 1";
        _player2.Name = "Player 2";
        _coin.FlipCoin();
        _turnManager.CurrentTurn = _coin.CoinValue != 0;
        _questionManager.LoadQuestions();
        NextQuestion();
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
            _turnManager.ResetTimer();
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
                _player2.Attack(_player1);
            }
        }
        else
        {
            if (_turnManager.CurrentTurn)
            {
                _player2.Defense(_player1);
            }
            else
            {
                _player1.Defense(_player2);
            }
        }

        _turnManager.SwapTurn();
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
}
