﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHandler : MonoBehaviour
{
    public GameController gmCtrl;
    public bool clicked;

    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetKeyDown(KeyCode.S))
            {
                clicked = true;
                gmCtrl.launch = true; //We launch the update() of every other scripts
            }

            if (clicked) //If the user has clicked on the start button
            {
                GameObject.Find("Simon").transform.localPosition = new Vector3(GameObject.Find("Simon").transform.localPosition.x, 3f, GameObject.Find("Simon").transform.localPosition.z);
                GameObject.Find("StartMenu").transform.localPosition = new Vector3(GameObject.Find("StartMenu").transform.localPosition.x, 1000f, GameObject.Find("StartMenu").transform.localPosition.z);    //We kick the menu out
                GameObject.Find("EndMenu").transform.localPosition = new Vector3(GameObject.Find("EndMenu").transform.localPosition.x, 1000f, GameObject.Find("EndMenu").transform.localPosition.z);    //We kick the reste menu out (when reseting the game)
        }
        else
            {
                GameObject.Find("Simon").transform.localPosition = new Vector3(GameObject.Find("Simon").transform.localPosition.x, 1000f, GameObject.Find("Simon").transform.localPosition.z);
            }
    }

    private void OnMouseOver()
    {
        gameObject.GetComponent<Renderer>().material.EnableKeyword("_EMISSION");
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
    }

    private void OnMouseDown()
    {
        clicked = true;
    }
}