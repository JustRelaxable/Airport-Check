using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDCardEventHandler : MonoBehaviour
{
    public PlayerController playerController;
    public Animator animator;
    public bool isFinished = false;
    // Start is called before the first frame update

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

    public void NextPassenger()
    {
        animator.SetBool("IsGameFinished", true);
        if (playerController.NextPassenger())
        {
            animator.SetBool("IsGameFinished", false);
        }
        else
        {
            isFinished = true;
        }
    }

    public void SetIsScreenActiveOn()
    {
        if (!isFinished)
        {
            playerController.isScreenActive = true;
        }

    }
}
