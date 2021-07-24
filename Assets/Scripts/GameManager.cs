using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Nationality[] nationalities;
    public Characteristic[] characteristics;
    public List<ICheckable> conditions = new List<ICheckable>();
    public IDCardUI IDCardUI;
    public static ID currentId;
    public ID[] passengers;
    public Vector3[] positions;
    private int kaçKereGeçildi = 0;
    public GameObject[] particles;
    public GameObject[] characters;
    public Animator cardAnimator;
    public GameObject winPanel;
    public GameObject losePanel;
    private static string[] surnames;
    private static string[] maleNames;
    private static string[] femaleNames;
    private Animator currentYolcuAnimator;
    public TextMeshProUGUI winPanelMoneyText;
    public Slider progressionBar;
    public PointSpawner pointSpawner;
    PlayerController controller;
    public AudioManager audioManager;
    public List<System.Action> actions = new List<System.Action>();
    

    private void Awake()
    {
        Application.targetFrameRate = 60;
        //IDCardUI.personID = CreateRandomID();
        actions.Add(CreateColorCondition);
        actions.Add(CreateAgeCondition);
        actions.Add(CreateGenderCondition);
        controller = GetComponent<PlayerController>();

        actions[Random.Range(0, actions.Count)].Invoke();
        //
        //CreateColorCondition();
        //CreateGenderCondition();
        //CreateAgeCondition();
        //currentId = passengers[0];
        
        LoadNamesAndSurnames();
        //currentYolcuAnimator = currentId.gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        SetUpScene(5, 8);
        SetGenders();
        Application.targetFrameRate = 60;
        SetCurrentID(currentId);

        positions = new Vector3[passengers.Length];
        for (int i = 0; i < passengers.Length; i++)
        {
            positions[i] = passengers[i].gameObject.transform.position;
        }
    }



    public ID CreateRandomID()
    {
        ID myId = new ID();
        myId.bodyTemperature = Random.Range(15, 41); //Random Temperature
        myId.income = Random.Range(600, 5000);
        myId.personAge = Random.Range(17,45);
        myId.nationality = nationalities[Random.Range(0, nationalities.Length)];
        myId.characteristics.Add(characteristics[Random.Range(0,characteristics.Length)]);
        myId.personName = "Taha Sökmen"; //TODO: Make it random
        return myId;
    }

    public void PlaySound(AudioManager.Sounds sound)
    {
        audioManager.PlaySound(sound);
    }

    public void CreateColorCondition()
    {
        ColorCondition colorCondition = new ColorCondition();
        conditions.Add(colorCondition);
        Debug.Log(colorCondition.conditionColor);
    }

    public void CreateGenderCondition()
    {
        GenderCondition genderCondition = new GenderCondition();
        conditions.Add(genderCondition);

    }

    public void CreateAgeCondition()
    {
        AgeCondition ageCondition = new AgeCondition(25, 50, 1);
        conditions.Add(ageCondition);

    }

    public void SetCurrentID(ID id)
    {
        currentId = id;
        IDCardUI.personID = currentId;
        IDCardUI.FillIDCard();
        
    }

    public bool CheckConditions()
    {
        return currentId.CheckConditions(conditions);
    }

    public void LoadNamesAndSurnames()
    {
        TextAsset surnameTextFile = Resources.Load<TextAsset>("american-names-master/surnames");
        TextAsset maleNamesTextFile = Resources.Load<TextAsset>("american-names-master/firstnames_m");
        TextAsset femaleNamesTextFile = Resources.Load<TextAsset>("american-names-master/firstnames_f");
        surnames = JsonHelper.getJsonArray<string>(surnameTextFile.text);
        maleNames = JsonHelper.getJsonArray<string>(maleNamesTextFile.text);
        femaleNames = JsonHelper.getJsonArray<string>(femaleNamesTextFile.text);

    }

    public static string RandomMaleName()
    {
        return maleNames[Random.Range(0,maleNames.Length)] + " " +surnames[Random.Range(0, surnames.Length)];
    }

    public static string RandomFemaleName()
    {
        return femaleNames[Random.Range(0, maleNames.Length)] +" "+ surnames[Random.Range(0, surnames.Length)];
    }

    public bool NextPassenger()
    {
        for (int i = 0; i < passengers.Length; i++)
        {
            if(currentId == passengers[i] && i != passengers.Length-1)
            {
                currentId = passengers[i + 1];
                SetCurrentID(passengers[i+1]);
                currentYolcuAnimator = currentId.gameObject.GetComponent<Animator>();
                return true;
            }
            else if(i == passengers.Length - 1)
            {
                GameFinished();
                return false;
            }
        }
        return false;
    }

    private void GameFinished()
    {
        
        foreach (var item in particles)
        {
            item.SetActive(true);
        }
        winPanelMoneyText.text = UnityEngine.PlayerPrefs.GetInt("_money").ToString() + "$";

        if(progressionBar.value < 75)
        {
            losePanel.SetActive(true);
            audioManager.PlaySound(AudioManager.Sounds.Lose);
        }
        else
        {
            winPanel.SetActive(true);
            audioManager.PlaySound(AudioManager.Sounds.Win);
        }
        controller.isScreenActive = false;
    }

    public void CurrentYolcuKabul()
    {
        currentYolcuAnimator.SetTrigger("Thankful");
        controller.isScreenActive = false;
        //YolcularıHareketEttir();
    }

    public void CurrentYolcuRet()
    {
        currentYolcuAnimator.SetTrigger("Defeated");
        controller.isScreenActive = false;
        //YolcularıHareketEttir();
    }

    public void CurrentYolcuExcited()
    {
        currentYolcuAnimator.SetTrigger("Excited");
    }

    public void CurrentYolcuSad()
    {
        currentYolcuAnimator.SetTrigger("Sad");
    }

    public void CurrentYolcuClearAnimTrigers()
    {
        currentYolcuAnimator.ResetTrigger("Sad");
        currentYolcuAnimator.ResetTrigger("Excited");
    }

    public void YolcularıHareketEttir()
    {
        int currentIndex = 0;

        for (int i = 0; i < passengers.Length; i++)
        {
            if(currentId == passengers[i])
            {
                currentIndex = i;
                break;
            }
        }

        

        for (int i = currentIndex; i < passengers.Length - 1; i++)
        {
            passengers[i + 1].gameObject.GetComponent<ID>().StartCoroutine("OneGec",positions[i + kaçKereGeçildi]);
        }
        kaçKereGeçildi -= 1;
    }

    public void CardDecisionMade()
    {
        cardAnimator.SetTrigger("DecisionMade");
    }

    public void CardGameStarted()
    {
        cardAnimator.SetTrigger("GameStarted");
    }

    public void SetGenders()
    {
        for (int i = 0; i < passengers.Length; i++)
        {
            if(passengers[i].gameObject.CompareTag("Male"))
            {
                passengers[i].personGender = Genders.Male;
            }
            else
            {
                passengers[i].personGender = Genders.Female;
            }
        }
    }

    public void SetUpScene(int lowestPassenger,int highestPassenger)
    {
        int randomPassengerCount = Random.Range(lowestPassenger, highestPassenger);
        positions = pointSpawner.GetPositions(randomPassengerCount);
        passengers = new ID[positions.Length];

        for (int i = 0; i < positions.Length; i++)
        {
            int randomIndex = Random.Range(0, characters.Length);
            Quaternion rotation = Quaternion.Euler(0, 180, 0);
            GameObject character = Instantiate(characters[randomIndex], positions[i], rotation);
            character.SetActive(true);
            passengers[i] = character.GetComponent<ID>();

            if(i == 0)
            {
                currentId = character.GetComponent<ID>();
                currentYolcuAnimator = character.GetComponent<Animator>();
            }
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
