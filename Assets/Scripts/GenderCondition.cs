using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenderCondition : MonoBehaviour,ICheckable
{
    public Genders gender;

    public GenderCondition()
    {
        int i = Random.Range(0, 2);
        gender = (Genders)i;
    }
    public bool CheckCondition(ID id)
    {
        if(id.personGender == gender)
        {
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return "-Accept " + gender.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
