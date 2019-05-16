using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameController gmCtrl;
    //public bool isWriting = false;

    private TextMesh name;
    private string PLAYER_NAME = "";

    // Start is called before the first frame update
    void Start()
    {
        name = gameObject.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
        PLAYER_NAME = gmCtrl.PLAYER_NAME;
    }

    // Update is called once per frame
    void Update()
    {
        PLAYER_NAME = gmCtrl.PLAYER_NAME;

        if (PLAYER_NAME == "")
        {
            PLAYER_NAME = "Anonymous";
        }

        name.text = "Player :\n\n\n" + PLAYER_NAME + "\n\n\n Press\n\"P\" to change";
    }

}
