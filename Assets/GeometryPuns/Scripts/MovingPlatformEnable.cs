﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformEnable : MonoBehaviour {

    public List<GameObject> movingPlatforms;
    public Color mainColor;
    public bool enable;
    private GameObject player;
    private SpriteRenderer sr;


    // Use this for initialization
    void Start()
    {
        sr = this.GetComponent<SpriteRenderer>();
        sr.color = mainColor;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        player = other.gameObject;

        // check if collider is player 
        if (player.tag == "Player")
        {
            sr.color = Color.gray;
            foreach (GameObject element in movingPlatforms)
            {
                element.GetComponent<MovingPlatform>().enabled = enable;
            
            }
        }

    }


}


