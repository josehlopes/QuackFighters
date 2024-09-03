using UnityEngine;

public class Player : MonoBehaviour
{
    private string _name;
    private int _health;
    private int _points;
    private int _maxHealth;

    private bool _isAttacking;
    private bool _responseLocked;

    public string Name
    {
        get => _name;
        set => _name = value;
    }
    public int Health
    {
        get => _health;
        set
        {
            _health = Mathf.Clamp(value, 0, _maxHealth);
            OnHealthChanged?.Invoke();
        }
    }
    public int Points
    {
        get => _points;
        set => _points = value;
    }
    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }
    public bool IsAttacking
    {
        get => _isAttacking;
        set => _isAttacking = value;
    }
    public bool ResponseLocked
    {
        get => _responseLocked;
        set => _responseLocked = value;
    }

    public delegate void HealthChanged();
    public event HealthChanged OnHealthChanged;

    void Awake()
    {
        _health = 10;
        _maxHealth = _health;
    }

    public void Attack(Player otherPlayer)
    {
        if (otherPlayer != null)
        {
            otherPlayer.Health -= 1;
            Debug.Log("Vida do jogador " + otherPlayer.name + ": " + otherPlayer.Health);
        }
    }

    public void Defense(Player otherPlayer)
    {
        if (otherPlayer != null)
        {
            otherPlayer.Health -= 1;
            Debug.Log("Vida do jogador " + otherPlayer.name + ": " + otherPlayer.Health);
        }
    }
}
