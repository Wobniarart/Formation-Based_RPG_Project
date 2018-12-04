﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScript : MonoBehaviour 
{

public TransitionManager transition;
	// Use this for initialization
	void Start () {
		transition = GameObject.FindGameObjectWithTag("Manager").GetComponent<TransitionManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	public void GoToTitle()
	{
		//Debug.Log("Commencing GoToTitle");
		transition.FadeToSceneChange(false,1);//Fade to black to title screen.
		gameObject.SetActive(false);
	}
}
