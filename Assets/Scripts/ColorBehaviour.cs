﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBehaviour : MonoBehaviour
{

    public bool triggerColor = false;
    public Animator colorAnim;

    private GameController gmCtrl;
    private Material colorMaterial;
    private float timeOfShine = 0f;

    // Start is called before the first frame update
    void Start()
    {
        colorAnim = gameObject.GetComponent<Animator>();
        colorMaterial = gameObject.GetComponent<Renderer>().material;
        gmCtrl = GameObject.Find("GameController").GetComponent<GameController>();
        gameObject.transform.GetChild(0).GetComponent<LightBehaviour>().OnMouseExit(); //To prevent the light to be blocked at the beginning of the game
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerColor) //Prevents blocking the lighting of a color by a previous mouse action (It can happen, when clicking so fast, that the code is not going into OnMouseUp())
        {

            timeOfShine += Time.deltaTime;

            if (timeOfShine < colorAnim.GetCurrentAnimatorStateInfo(0).length) //We shine during the duration of the animation
            {
                colorMaterial.EnableKeyword("_EMISSION");
            }
            else
            {
                colorMaterial.DisableKeyword("_EMISSION");
                timeOfShine = 0f;
                triggerColor = false;
            }
        }
    }

    public void OnMouseDown()
    {
        if (!gmCtrl.isShowing && gmCtrl.clickCount < gmCtrl.selection.Count) //We can not click when the level is showing the colors
            {
                triggerColor = true;
                colorAnim.SetTrigger(gameObject.name);
                gmCtrl.selectedColors.Add(gameObject); //We send the clicked object
                gmCtrl.colorListener = true; //We say that the user has clicked
                gmCtrl.clickCount++;
            }
    }

}
