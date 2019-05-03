using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviour : MonoBehaviour
{
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

    }

    private void OnMouseOver()
    {
        colorMaterial.EnableKeyword("_EMISSION");
    }

    public void OnMouseExit()
    {
        if (colorMaterial)
        {
            colorMaterial.DisableKeyword("_EMISSION");
        }
    }

    public void OnMouseDown()
    {
        clBhv.OnMouseDown();
    }
}
