using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Simon_VR.Assets.Scripts;

public class GameController : MonoBehaviour
{
    public List<GameObject> selectedColors;
    public List<GameObject> selection = new List<GameObject>();
    public List<GameObject> colors = new List<GameObject>();
    public string PLAYER_NAME = "";
    public bool colorListener = false;
    public int clickCount = 0;
    public bool isShowing;
    public bool launch = false;
    public bool reset = false;
    public string rstMess = "";
    public string rstStatMess = "";

    private List<GameObject> selectedColorsCache = new List<GameObject>();
    private float timer = 1f;
    private float timeBtwTriggers = 1f;
    private int starterLevelIterations = 3;
    private int iteration;
    private int levelStep = 0;
    private int index = 0;
    private bool hasFailed = false;
    private int level = 0;
    private bool onGame = false;
    private bool hasLost = false;

    // Time Stats
    private float timePlayed = 0f;
    private List<float> timeBetweenColorClickArray = new List<float>();
    private float timeBetweenColorClick = 0f;

    // Start is called before the first frame update
    void Start()
    {
 
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Colors")) //For each square on the screen
        {
            obj.AddComponent<ColorBehaviour>(); //We add a script for mouse and object behaviour
            
            obj.transform.GetChild(0).gameObject.AddComponent<LightBehaviour>();
            obj.transform.GetChild(1).gameObject.AddComponent<AreaHandler>();
            colors.Add(obj); //We add the object to a list of GameObject
        }

        isShowing = true;
        iteration = starterLevelIterations;

        SimonLogger.logger.write("===== Starting Game =====");
    }

    // Update is called once per frame
    void Update()
    {
        //The next line in here in order to adapt the game to the size of the user (the user will always be 1m85 in the game)
        GameObject.Find("Floor").transform.localPosition = new Vector3(GameObject.Find("Floor").transform.localPosition.x, GameObject.Find("[CameraRig]").transform.localPosition.y - 1.8f, GameObject.Find("Floor").transform.localPosition.z);

        if (launch)
        {
            if (!hasFailed) //While the player has not failed
            {
                if (!onGame)
                {
                    selection = selectColors(colors, selection);
                    onGame = true;
                    iteration++; //Harder at each level (one iteration more)
                    level++;
                }
                else
                {
                    launchLevel(selection);
                }
            }

            if(Input.GetKeyDown(KeyCode.Q) || hasLost)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    rstMess = "You left the game !";
                }

                if (hasLost)
                {
                    rstMess = "Game Over !";
                }

                rstStatMess = "" + //Game Data
                    "\nLevel reached : " + level + 
                    "\nColors found : "+clickCount+
                    "\nTime played : "+ System.Math.Round(timePlayed, 2)+
                    "s\nTime between clicks : "+ System.Math.Round(timeBetweenColorClick, 2)+
                    "s";
                
                SimonLogger.logger.write("===== Game Over =====");

                endGame();
            }
        }
    
    }

    private List<GameObject> selectColors(List<GameObject> objs, List<GameObject> chosenOnes)
    {
        if(iteration == starterLevelIterations)
        {
            for (int i = 0; i < iteration; i++)
            {
                chosenOnes.Add(objs[Random.Range(0, objs.Count)]);
            }
        }
        else
        {
            chosenOnes.Add(objs[Random.Range(0, objs.Count)]);
        }

        return chosenOnes;
    }

    private void launchLevel(List<GameObject> selection)
    {
        if(levelStep < selection.Count)
        {
            timer -= Time.deltaTime;
            if(timer <= 0f) //Every Second a color is selected
            {
                selection[levelStep].GetComponent<ColorBehaviour>().colorAnim.SetTrigger(selection[levelStep].name);
                selection[levelStep].GetComponent<ColorBehaviour>().triggerColor = true;
                levelStep++;
                timer = timeBtwTriggers;
            }
        }
        else
        {
            isShowing = false;
            timePlayed += Time.deltaTime;
            if (play(selection)) //We reinitiate all variables if the user has succeed in order to do it again
            {
                onGame = false;
                levelStep = 0;
                timer = 0f;
                index = 0;
                colorListener = false;
                isShowing = true;
                selectedColors.Clear();
                selectedColorsCache.Clear();
                clickCount = 0;
                timer = timeBtwTriggers;

                timePlayed = 0f;
                timeBetweenColorClick = 0f;
                timeBetweenColorClickArray.Clear();
            }
        }
    }

    private bool play(List<GameObject> currentSelection)
    {
        timeBetweenColorClick += Time.deltaTime;
        if (selectedColors.Count > selectedColorsCache.Count) //There was a color selected
        {
            // Print time between each click
            //print("Color clicked at " + timeBetweenColorClick);
            timeBetweenColorClickArray.Add(timeBetweenColorClick);
            timeBetweenColorClick = 0f;

            selectedColorsCache.Add(selectedColors[selectedColors.Count - 1]);
            if (currentSelection[index] == selectedColors[index]) //We test if it matches the corresponding color on the level selection
            {
                if (index < currentSelection.Count) //We test if we reached the end of the level selection
                {
                    index++;
                }

                if (index == currentSelection.Count) //The user found all the selection we can go to the next level
                {
                    print("VOUS AVEZ GAGNE CETTE ITERATION (" + iteration + " ITERATIONS, LEVEL : "+ level +") en "+timePlayed+" secondes");
                    timeBetweenColorClickArray.ForEach(delegate (float value) { print("Time between click: " + value + "s"); });
                    return true;
                }

                colorListener = false;
            }
            else
            {
                if (level > 0)
                {
                    level--;
                }
                
                if(iteration > 0)
                {
                    iteration--;
                }

                print("GAME OVER ! Level : " + level + ", Itérations : " + iteration + ", Temps: " + timePlayed + "s");
                timeBetweenColorClickArray.ForEach(delegate (float value) { print("Time between click: " + value + "s"); });
                hasLost = true;
                colorListener = false;
                hasFailed = true;
            }
        }

        return false;
    }

    private void endGame()
    {
        launch = false;
        reset = true;
        onGame = false;
        level = 0;
        levelStep = 0;
        timer = 0f;
        colorListener = false;
        isShowing = true;
        selectedColors.Clear();
        selectedColorsCache.Clear();
        clickCount = 0;
        index = 0;
        timer = timeBtwTriggers;
        selection.Clear();
        iteration = 3;
        hasLost = false;
        hasFailed = false;
    }
}
