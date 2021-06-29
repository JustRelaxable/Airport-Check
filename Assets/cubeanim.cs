using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeanim : MonoBehaviour
{

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            anim.SetBool("isWalking", true);
        }
        else if (Input.GetMouseButton(1))
        {
            anim.SetBool("isWalking", false);
        }
    }
}
