using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string _name { get; set; }
    private int _health { get; set; }

    private bool _isAttacking { get; set; }
    private bool _responseLocked { get; set; }

    public string Name
    {
        get => _name;
        set => _name = value;
    }
    public int Health
    {
        get => _health;
        set => _health = value;
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


    void Awake()
    {
        _health = 10;
    }

    public void Attack(Player otherPlayer)
    {
        otherPlayer.Health -= 1;
        Debug.Log(otherPlayer.Health);
    }
    public void Defense(Player otherPlayer)
    {
        otherPlayer.Health -= 1;
        Debug.Log(otherPlayer.Health);
    }
}
