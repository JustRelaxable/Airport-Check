using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgeCondition : MonoBehaviour,ICheckable
{
    public int ageCondition = 0;
    bool isHigher; 
    public AgeCondition(int lowInt,int highInt,int büyüktürKüçüktür)
    {
        ageCondition = Random.Range(lowInt, highInt);
        if(büyüktürKüçüktür == 1)
        {
            isHigher = true;
        }
        else
        {
            isHigher = false;
        }
    }
    public bool CheckCondition(ID id)
    {
        if (isHigher)
        {
            if(id.personAge > ageCondition)
            {
                return true;
            }
            return false;
        }
        else
        {
            if(id.personAge < ageCondition)
            {
                return true;
            }
            return false;
        }
    }

    public override string ToString()
    {
        if (isHigher)
        {
            return "Age of greater than " + ageCondition.ToString();
        }
        else
        {
            return "Age of lower than " + ageCondition.ToString();
        }
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
