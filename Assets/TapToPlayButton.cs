using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToPlayButton : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Started()
    {
        animator.SetTrigger("Started");
    }
}
