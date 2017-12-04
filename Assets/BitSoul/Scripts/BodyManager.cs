﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyManager : MonoBehaviour{

    private BodyHashes bh;
    private GameObject[] bodyObjects;
    private bool[] activeBodies;
    private bool[] storedBodies;
    private GameObject currentBody;
    private int currentIndex;
    private CameraController camControl;

    public GameObject magenta;
    public GameObject cyan;
    public GameObject yellow;

    // Use this for initialization
    void Start()
    {
        bh = GetComponent<BodyHashes>();

        // TODO get and assign GameObjects 
        bodyObjects = new GameObject[4];
        activeBodies = new bool[4] { true, false, false, false };
        storedBodies = new bool[4] { true, false, false, false };

        currentIndex = 0;
        // Find player and set it as first active Body
        currentBody = GameObject.FindGameObjectWithTag("Player");

        cyan.SetActive(false);
        magenta.SetActive(false);
        yellow.SetActive(false);

        bodyObjects[0] = currentBody;
        bodyObjects[1] = cyan;
        bodyObjects[2] = magenta;
        bodyObjects[3] = yellow;


        camControl = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        //camControl.setCameraOnPlayer(currentBody);
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetButtonDown("NextBody"))
        {
            SwitchToNext();
        }
    }

    public void SwitchToNext()
    {
        /* Disable old body scripts and tags */
        setPlayerComponents(currentBody, false);
        currentBody.tag = "Untagged";

        for ( int i = currentIndex+1; i < currentIndex+4; i++  )
        {
            int ind = i % 4;
            if (activeBodies[ind])
            {
                currentBody = bodyObjects[ind];
                currentIndex = ind;
                break;
            }
        }
    
        camControl.setCameraOnPlayer(currentBody);                          // Change Camera Target

        /* Enable new body scripts and tags */
        setPlayerComponents(currentBody, true);
        currentBody.tag = "Player";
    }

    public void SwitchToIndex(int index)
    {
        if (activeBodies[index])
        {
            setPlayerComponents(currentBody, false);
            currentBody.tag = "Untagged";

            currentBody = bodyObjects[index];
            currentIndex = index;

            camControl.setCameraOnPlayer(currentBody);                          // Change Camera Target

            /* Enable new body scripts and tags */
            setPlayerComponents(currentBody, true);
            currentBody.tag = "Player";
        }
    }

    public void SetCurrentBody( GameObject current)
    {
        currentBody = current;
    }

    public GameObject GetCurrentBody()
    {
        return currentBody;
    }

    public bool SetActiveBody(int index)
    {
        if (activeBodies[index])
        {
            SwitchToIndex(index);
            return true;
        }
        else if (storedBodies[index])
        {
            Vector3 variance = new Vector3(3, 0);
            /*if (GetComponent<PlayerController>().isFacingRight())
                variance = new Vector3(3, 0);
            else
                variance = new Vector3(-3, 0);
                */
            Vector3 newpos = currentBody.transform.position + variance;

            Debug.Log(newpos + "  " + currentBody.transform.position + "  " + variance);
            bodyObjects[index].transform.SetPositionAndRotation(newpos, currentBody.transform.rotation);
            bodyObjects[index].SetActive(true);

            activeBodies[index] = true;
            SwitchToIndex(index);

            return true;
        }

        return false;
    }

    MonoBehaviour[] comps;
    private void setPlayerComponents( GameObject go, bool isEnabled )
    {
        comps = go.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour c in comps)
        {
            c.enabled = isEnabled;
        }

        if( !isEnabled )
        {
            go.GetComponent<BoxCollider2D>().enabled = true;
            go.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void StoreClone(int identifier)
    {
        storedBodies[identifier] = true;
    }

    public void setActiveBody(int identifier, bool b)
    {
        activeBodies[identifier] = b;
    }
}
