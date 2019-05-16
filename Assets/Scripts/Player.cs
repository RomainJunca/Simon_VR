using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simon_VR.Assets.Scripts;

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
        if (!gmCtrl.launch)
        {
            PLAYER_NAME = gmCtrl.PLAYER_NAME;

            if (PLAYER_NAME == "")
            {
                PLAYER_NAME = "Anonymous";
                SimonLogger.logger.NAME = PLAYER_NAME;
            }

            name.text = "Player :\n\n\n" + PLAYER_NAME + "\n\n\n";

            if (Input.GetKeyDown(KeyCode.L))
            {
                SimonLogger.logger.NAME = PLAYER_NAME;
                SimonLogger.logger.write("===== Starting Game =====");
            }
        }        
    }

}
