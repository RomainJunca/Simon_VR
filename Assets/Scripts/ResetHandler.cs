using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simon_VR.Assets.Scripts;

public class ResetHandler : MonoBehaviour
{
    public GameController gmCtrl;
    public StartHandler start;

    private bool clicked;
    private TextMesh title;
    private TextMesh stats;

    // Start is called before the first frame update
    void Start()
    {
        gmCtrl.reset = false;
        clicked = false;
        GameObject.Find("EndMenu").transform.localPosition = new Vector3(GameObject.Find("EndMenu").transform.localPosition.x, 1000f, GameObject.Find("EndMenu").transform.localPosition.z);    //We kick the menu out
        title = GameObject.Find("EndMenu").transform.Find("Board").transform.Find("Title").GetComponent<TextMesh>();
        stats = GameObject.Find("EndMenu").transform.Find("Board").transform.Find("Stats").GetComponent<TextMesh>();
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
                SimonLogger.logger.write("===== Starting Game =====");
            }

            if (clicked)
            {
                gmCtrl.launch = true;
                GameObject.Find("Simon").transform.localPosition = new Vector3(GameObject.Find("Simon").transform.localPosition.x, 1.68f, GameObject.Find("Simon").transform.localPosition.z);
                GameObject.Find("EndMenu").transform.localPosition = new Vector3(GameObject.Find("EndMenu").transform.localPosition.x, 1000f, GameObject.Find("EndMenu").transform.localPosition.z);    //We kick the menu out
            }
            else
            {
                GameObject.Find("Simon").transform.localPosition = new Vector3(GameObject.Find("Simon").transform.localPosition.x, 1000f, GameObject.Find("Simon").transform.localPosition.z);
            }

        }

        if (gmCtrl.reset) //The game is reset, we bring the menu in front of the user
        {
            foreach (GameObject obj in gmCtrl.colors) //We clear the materials' shaders (sometimes it stays in emissive mode when we go back to the game)
            {
                if(obj.GetComponent<ColorBehaviour>().triggerColor)
                {
                    obj.GetComponent<ColorBehaviour>().timeOfShine = 0f;//obj.GetComponent<ColorBehaviour>().colorAnim.GetCurrentAnimatorStateInfo(0).length;
                    obj.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                    obj.GetComponent<ColorBehaviour>().triggerColor = false;
                }
            }

            GameObject.Find("Simon").transform.localPosition = new Vector3(GameObject.Find("Simon").transform.localPosition.x, 1000f, GameObject.Find("Simon").transform.localPosition.z);
            GameObject.Find("EndMenu").transform.localPosition = new Vector3(GameObject.Find("EndMenu").transform.localPosition.x, 1.85f, GameObject.Find("EndMenu").transform.localPosition.z);    //We kick the menu out
            title.text = gmCtrl.rstMess;
            stats.text = gmCtrl.rstStatMess;
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
