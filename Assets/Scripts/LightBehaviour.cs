using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviour : MonoBehaviour
{
    public bool shiny = false;

    private GameController gmCtrl;
    private ColorBehaviour clBhv;
    private Material colorMaterial;

    // Start is called before the first frame update
    void Start()
    {
        colorMaterial = gameObject.GetComponent<Renderer>().material; //To deactivate emission before other Start() (deactivated at Start() of ColorBehaviour)
        clBhv = gameObject.GetComponentInParent<ColorBehaviour>();
        colorMaterial.DisableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if (shiny)
        {
            if (colorMaterial)
            {
                colorMaterial.EnableKeyword("_EMISSION");
            }
        }
        else
        {
            colorMaterial.DisableKeyword("_EMISSION");
        }
    }

    private void OnMouseOver()
    {
    }

    public void OnMouseExit()
    {
        
    }

    public void OnMouseDown()
    {
        clBhv.OnMouseDown();
    }
}
