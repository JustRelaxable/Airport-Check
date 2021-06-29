using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMenu : MonoBehaviour
{
	public Animator backgroundAnimator;
	public Animator buttonAnimator;
    // Start is called before the first frame update
	void Awake()
	{
		
	}
	
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	
	public void FadeMenus()
	{
		backgroundAnimator.SetTrigger("Started");
		buttonAnimator.SetTrigger("Started");
		StartCoroutine(Disable());
	}
	
	IEnumerator Disable()
	{
		yield return new WaitForSeconds(1);
		this.gameObject.SetActive(false);
	}
}
