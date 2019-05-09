using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaHandler : MonoBehaviour
{

    private bool trig = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (trig)
        {
            gameObject.transform.parent.GetChild(0).GetComponent<LightBehaviour>().shiny = true;
        }
        else
        {
            gameObject.transform.parent.GetChild(0).GetComponent<LightBehaviour>().shiny = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Controller")
        {
            trig = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Controller")
        {
            trig = false;
        }

    }
}
