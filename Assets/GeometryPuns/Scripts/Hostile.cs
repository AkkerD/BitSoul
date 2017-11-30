﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostile : MonoBehaviour {

	public LevelManager levelManager;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D ( Collider2D other){
		if(other.tag == "Player"){
            if (other.name == "Magenta")
                Destroy(this);
            else
			    levelManager.RespawnPlayer();
		}
	}
}
