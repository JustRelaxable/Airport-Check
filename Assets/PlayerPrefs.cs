using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerPrefs : MonoBehaviour
{
    TextMeshProUGUI moneyText;

    private void Awake()
    {
        moneyText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        moneyText.text = UnityEngine.PlayerPrefs.GetInt("_money").ToString() + "$";
    }

    public void UpdateMoney()
    {
        moneyText.text = UnityEngine.PlayerPrefs.GetInt("_money").ToString() + "$";
    }

    public void RaiseMoney(int amount)
    {
        int currentMoney = UnityEngine.PlayerPrefs.GetInt("_money");
        UnityEngine.PlayerPrefs.SetInt("_money", (currentMoney + amount));
        UpdateMoney();
    }

    
    void Update()
    {
        
    }
}
