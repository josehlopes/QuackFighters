using System.Collections;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    private bool _currentTurn;
    private int _turnCount;
    private int _turnTimer;
    private int _maxTurnTimer = 10;

    public bool CurrentTurn
    {
        get => _currentTurn;
        set => _currentTurn = value;
    }

    public int TurnCount
    {
        get => _turnCount;
        set => _turnCount = value;
    }

    public int TurnTimer
    {
        get => _turnTimer;
        set => _turnTimer = value;
    }

    public int MaxTurnTimer
    {
        get => _maxTurnTimer;
        set => _maxTurnTimer = value;
    }

    void Awake()
    {
        _turnCount = 0;
        _turnTimer = 0;
    }

    void Start()
    {
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        if (_turnTimer < _maxTurnTimer)
        {
            _turnTimer++;
        }
        else
        {
            _turnTimer = 0;
            SwapTurn();
        }
    }

    public void ResetTimer()
    {
        _turnTimer = 0;
    }
    public void SwapTurn()
    {
        _currentTurn = !_currentTurn;
        _turnCount++;
        Debug.Log("Turno: " + _turnCount);
    }
}
