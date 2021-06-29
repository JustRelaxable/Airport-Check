using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Vector2 currentMousePos;
    GameManager manager;
    public Image redIndicator;
    public Image greenIndicator;
    public PlayerPrefs playerPrefs;
    public bool isScreenActive = false;
    private int yolcuSayısı = 0;
    public Slider levelCompletionSlider;
    AndroidVibrationHandler vibrationHandler = new AndroidVibrationHandler();

    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        yolcuSayısı = manager.passengers.Length;
    }

    void Update()
    {
        if (isScreenActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentMousePos = Input.mousePosition;
            }

            if (Input.GetMouseButton(0) && currentMousePos != Vector2.zero)
            {
                Vector2 deltaPos = (Vector2)Input.mousePosition - currentMousePos;
                float percentage = (deltaPos.x / Screen.width);
                if (deltaPos.x > 0)
                {
                    manager.CurrentYolcuClearAnimTrigers();
                    manager.CurrentYolcuExcited();
                    redIndicator.color = new Color(
                        redIndicator.color.r,
                        redIndicator.color.g,
                        redIndicator.color.b,
                        -0);

                    Debug.Log(percentage);
                    greenIndicator.color = new Color(
                        greenIndicator.color.r,
                        greenIndicator.color.g,
                        greenIndicator.color.b,
                        percentage);

                }
                else if (deltaPos.x < 0)
                {
                    manager.CurrentYolcuClearAnimTrigers();
                    manager.CurrentYolcuSad();
                    greenIndicator.color = new Color(
                        greenIndicator.color.r,
                        greenIndicator.color.g,
                        greenIndicator.color.b,
                        0);

                    redIndicator.color = new Color(
                        redIndicator.color.r,
                        redIndicator.color.g,
                        redIndicator.color.b,
                        -percentage);
                }
            }

            if (Input.GetMouseButtonUp(0) && currentMousePos != Vector2.zero)
            {
                ClearIndicators();
                StartCoroutine(FadeOutIndicators());
                Vector2 deltaVector = (Vector2)Input.mousePosition - currentMousePos;
                if (deltaVector.x > 0)
                {
                    manager.CurrentYolcuKabul();
                    if (manager.CheckConditions())
                    {
                        //Score artabilir
                        manager.PlaySound(AudioManager.Sounds.True);
                        playerPrefs.RaiseMoney(25);
                        FillSlider();
                        Debug.Log("Nice");
                    }
                    else
                    {
                        //Score azalabilir
                        manager.PlaySound(AudioManager.Sounds.False);
                        vibrationHandler.CreateVibration();
                        DecreaseSlider();
                        playerPrefs.RaiseMoney(-10);
                        Debug.Log("Not Nice");
                    }
                }
                else if (deltaVector.x < 0)
                {
                    manager.CurrentYolcuRet();
                    if (!manager.CheckConditions())
                    {
                        //Score artabilir
                        manager.PlaySound(AudioManager.Sounds.True);
                        playerPrefs.RaiseMoney(25);
                        FillSlider();
                        Debug.Log("Nice");
                    }
                    else
                    {
                        //Score azalabilir
                        manager.PlaySound(AudioManager.Sounds.False);
                        vibrationHandler.CreateVibration();
                        DecreaseSlider();
                        playerPrefs.RaiseMoney(-10);
                        Debug.Log("Not Nice");
                    }
                }

                //NextPassenger();
            }
        }

        else
        {
            currentMousePos = Vector2.zero;
        }
        
        
    }

    public void FillSlider()
    {
        float rate = (200 * (float)(1.0f / (float)yolcuSayısı));
        StartCoroutine(FillSlider(rate));
    }

    public void DecreaseSlider()
    {
        float rate = (100 * (float)(1.0f / (float)yolcuSayısı));
        StartCoroutine(FillSlider(-rate));
    }

    public IEnumerator FillSlider(float rate)
    {
        float currentTime = 0f;
        float timeToFade = 1f;
        float currentValue = levelCompletionSlider.value;
        float valueToGo = levelCompletionSlider.value + rate;

        while (currentTime < timeToFade)
        {
            levelCompletionSlider.value = Mathf.LerpAngle(currentValue, valueToGo, (currentTime / timeToFade));
            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    public void ClearIndicators()
    {
        /*
        
                        */
    }

    public IEnumerator FadeOutIndicators()
    {
        float currentTime = 0f;
        float timeToFade = 1f;

        Color currentRed = redIndicator.color;
        Color currentGreen = greenIndicator.color;
        Color redShould = new Color(currentRed.r, currentRed.g, currentRed.b, 0);
        Color greenShould = new Color(currentGreen.r, currentGreen.g, currentGreen.b, 0);

        while (currentTime < timeToFade)
        {
            redIndicator.color = Color.Lerp(currentRed, redShould, (currentTime / timeToFade));
            greenIndicator.color = Color.Lerp(currentGreen, greenShould, (currentTime / timeToFade));
            currentTime += Time.deltaTime;
            yield return null;
        }

        greenIndicator.color = new Color(
                        greenIndicator.color.r,
                        greenIndicator.color.g,
                        greenIndicator.color.b,
                        0);

        redIndicator.color = new Color(
                        redIndicator.color.r,
                        redIndicator.color.g,
                        redIndicator.color.b,
                        -0);
    }

    public bool NextPassenger()
    {
        return manager.NextPassenger();
    }

    public void GameStarted()
    {
        manager.PlaySound(AudioManager.Sounds.PassengerWalksIn);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.3f);
        isScreenActive = true;
        yield return null;
    }
}
