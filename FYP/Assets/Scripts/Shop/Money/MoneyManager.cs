using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager instance;

    public float coins = 1000;

    public Text coinsText;

    private void Awake()
    {
        instance = this;
        //coins = 0;
        RefreshCoins();
    }

    public void Buy(float price)
    {
        if(coins - price >= 0)
            coins -= price;
        //RefreshCoins();
    }

    public void Sell(float price)
    {
        coins += price;
        //RefreshCoins();
    }

    public void RefreshCoins()
    {
        coinsText.text = "Coins: " + coins;
    }
}
