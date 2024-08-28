using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private int _coinValue { get; set; }

    public int CoinValue
    {
        get => _coinValue;
        set => _coinValue = value;
    }
    public int FlipCoin()
    {
        _coinValue = Random.Range(0, 2);
        Debug.Log("Coin Flipped:" + _coinValue);
        return _coinValue;
    }
}
