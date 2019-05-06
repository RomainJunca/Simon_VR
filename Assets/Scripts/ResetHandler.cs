using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetHandler : MonoBehaviour
{
    public GameController gmCtrl;
    public StartHandler start;

    private bool clicked;

    // Start is called before the first frame update
    void Start()
    {
        gmCtrl.reset = false;
        clicked = false;
        GameObject.Find("EndMenu").transform.localPosition = new Vector3(GameObject.Find("EndMenu").transform.localPosition.x, 1000f, GameObject.Find("EndMenu").transform.localPosition.z);    //We kick the menu out
    }

    // Update is called once per frame
    void Update()
    {

        if(GameObject.Find("EndMenu").transform.localPosition.y != 1000f)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                clicked = true;
                gmCtrl.launch = true; //We launch the update() of every other scripts
            }

            if (clicked)
            {
                gmCtrl.launch = true;
                GameObject.Find("Simon").transform.localPosition = new Vector3(GameObject.Find("Simon").transform.localPosition.x, 3f, GameObject.Find("Simon").transform.localPosition.z);
                GameObject.Find("EndMenu").transform.localPosition = new Vector3(GameObject.Find("EndMenu").transform.localPosition.x, 1000f, GameObject.Find("EndMenu").transform.localPosition.z);    //We kick the menu out
            }
            else
            {
                GameObject.Find("Simon").transform.localPosition = new Vector3(GameObject.Find("Simon").transform.localPosition.x, 1000f, GameObject.Find("Simon").transform.localPosition.z);
            }

        }

        if (gmCtrl.reset) //The game is reset, we bring the menu in front of the user
        {
            GameObject.Find("Simon").transform.localPosition = new Vector3(GameObject.Find("Simon").transform.localPosition.x, 1000f, GameObject.Find("Simon").transform.localPosition.z);
            GameObject.Find("EndMenu").transform.localPosition = new Vector3(GameObject.Find("EndMenu").transform.localPosition.x, 0f, GameObject.Find("EndMenu").transform.localPosition.z);    //We kick the menu out
            gmCtrl.reset = false;
            start.clicked = false;
            clicked = false;
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
