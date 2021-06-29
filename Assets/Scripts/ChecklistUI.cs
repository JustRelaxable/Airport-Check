using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistUI : MonoBehaviour
{
    GameManager manager;
    public Text text;

    private void Awake()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        for (int i = 0; i < manager.conditions.Count; i++)
        {
            text.text += manager.conditions[i].ToString() + "\n";
        }
        
    }

    private void OnEnable()
    {
        
    }


}
