using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ID : MonoBehaviour
{
    public string personName;
    public int personAge;
    public Color bodyColor;
    public int bodyTemperature;
    private GameManager manager;
    public int income;
    public Nationality nationality;
    public Genders personGender;
    public List<Characteristic> characteristics = new List<Characteristic>();
    public Transform losePoint;
    public Transform winPoint;
    public Animator animator;
    

    private void Awake()
    {
        manager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        
        CreateRandomID();
        
        //CreateRandomID();
    }

    private void Start()
    {
        losePoint = GameObject.FindGameObjectWithTag("LosePoint").transform;
        winPoint = GameObject.FindGameObjectWithTag("WinPoint").transform;
        //manager.SetCurrentID(this);

    }

    public bool CheckConditions(List<ICheckable> conditions)
    {
        foreach (var item in conditions)
        {
            if (item.CheckCondition(this))
            {
                continue;
            }

            else
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        
    }
    public void CreateRandomID()
    {
        this.bodyTemperature = Random.Range(15, 41); //Random Temperature
        this.income = Random.Range(600, 5000);
        this.personAge = Random.Range(17, 45);
        this.nationality = manager.nationalities[Random.Range(0, manager.nationalities.Length)];
        this.characteristics.Add(manager.characteristics[Random.Range(0, manager.characteristics.Length)]);

        if (gameObject.CompareTag("Male"))
        {
            this.personName = GameManager.RandomMaleName();
        }

        else
        {
            this.personName = GameManager.RandomFemaleName();
        }
         //TODO: Make it random
        Material material = new Material(Shader.Find("Standard"));
        material.color = ColorCondition.currentColors[Random.Range(0, ColorCondition.currentColors.Length)];
        bodyColor = material.color;
        //GetComponent<MeshRenderer>().material = material;
    }

    public IEnumerator OneGec(Vector3 vector3)
    {
        animator.applyRootMotion = false;
        animator.SetTrigger("WalkingStarted");
        float currentTime = 0;
        float timer = 2f;
        Vector3 posNow = transform.position;
        StartCoroutine(TurnTo(vector3));
        Quaternion rotToTurn = new Quaternion(0, -180, 0, transform.rotation.w);
        bool isTurnToRotCalled = false;

        while (currentTime<timer)
        {
            //transform.LookAt(vector3);
            transform.position = Vector3.Lerp(posNow, vector3, (currentTime / timer));
            //Debug.Log(Vector3.Lerp(posNow, vector3, (currentTime / timer)));
            currentTime += Time.deltaTime;

            
            if(currentTime > 1 && !isTurnToRotCalled)
            {
                StopCoroutine("TurnTo");
                StartCoroutine(TurnToRotation(rotToTurn));
                isTurnToRotCalled = true;
            }
            

            yield return null;
        }

        animator.SetTrigger("WalkingFinished");
        animator.applyRootMotion = true;
        //StartCoroutine(TurnToRotation(rotToTurn));
        //transform.rotation = new Quaternion(0,-180,0,transform.rotation.w);

    }

    public void Ret()
    {
        //Debug.Log("C");
        StopAllCoroutines();
        StartCoroutine(TurnTo(losePoint.position));
    }

    public void Kabul()
    {
        StopAllCoroutines();
        StartCoroutine(TurnTo(winPoint.position));
    }

    public IEnumerator TurnTo(Vector3 pos)
    {
        float currentTime = 0;
        float timer = 1f;
        Vector3 posNow = transform.position;
        Quaternion rotNow = transform.rotation;

        while (currentTime < timer)
        {
            Vector3 direction = pos - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            currentTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(rotNow, toRotation, (currentTime/timer));

            yield return null;
        }
    }

    public IEnumerator TurnToRotation(Quaternion rot)
    {
        float currentTime = 0;
        float timer = 5f;
        Quaternion rotNow = transform.rotation;

        while (currentTime < timer)
        {
            currentTime += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(rotNow, rot, (currentTime / timer));
            
            yield return null;
        }
    }

    public void MovePassengers()
    {
        StartCoroutine(IMovePassengers());
    }

    public IEnumerator IMovePassengers()
    {
        manager.YolcularıHareketEttir();
        manager.CardDecisionMade();
        yield return null;
    }
}

public enum Genders
{
    Male,Female
}
