using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelIndicatorText : MonoBehaviour
{
    TextMeshProUGUI levelText;

    private void Awake()
    {
        levelText = GetComponent<TextMeshProUGUI>();
        if (UnityEngine.PlayerPrefs.HasKey("level"))
        {
            UnityEngine.PlayerPrefs.SetInt("level", (UnityEngine.PlayerPrefs.GetInt("level") + 1));
        }
        else
        {
            UnityEngine.PlayerPrefs.SetInt("level", 1);
        }
    }

    void Start()
    {
        levelText.text = "LEVEL " + UnityEngine.PlayerPrefs.GetInt("level").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
