using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private static LevelManager levelManager;
    private GameManager manager;
    private TapToPlayButton playButton;

    private void Awake()
    {
        
        CheckSingleton();
    }
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void CheckSingleton()
    {
        if (levelManager == null)
        {
            levelManager = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            SetUpScene();
            Destroy(this.gameObject);
        }
    }

    public void SetUpScene()
    {
        manager = GameObject.FindObjectOfType<GameManager>();
        playButton = GameObject.FindObjectOfType<TapToPlayButton>();
        GameObject checklist = GameObject.FindGameObjectWithTag("Checklist");
        manager.CardGameStarted();
        playButton.Started();
        checklist.SetActive(true);
        checklist.transform.GetChild(0).gameObject.SetActive(true);
    }
}
