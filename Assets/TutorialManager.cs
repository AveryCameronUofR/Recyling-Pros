using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int trainingIndex = 0;
    public int traiingModules = 10;
    public GameObject trainingText;
    public float baseWaitTime = 3;

    public GameObject teleportArea;
    public GameObject teleporting;

    public GameObject startPedestal;
    public GameObject blueBin;

    public GameObject redBin;

    public GameObject shop;

    private Text text;
    private int teleportCount = 0;
    private float waitTime;

    private bool completedLeft, completedRight = false;

    
    private bool triedTeleporting = false;
    private Vector3 originalPosition;
    private GameManager gm;

    private bool gameEnabled;
    void Start()
    {
        text = trainingText.GetComponent<Text>();
        waitTime = baseWaitTime;
    }

    void Update()
    {
        switch (trainingIndex)
        {
            case 0:
                bool leftTurn = SteamVR_Input.GetState("SnapTurnLeft", SteamVR_Input_Sources.LeftHand) || SteamVR_Input.GetState("SnapTurnLeft", SteamVR_Input_Sources.RightHand);
                bool rightTurn = SteamVR_Input.GetState("SnapTurnRight", SteamVR_Input_Sources.LeftHand) || SteamVR_Input.GetState("SnapTurnRight", SteamVR_Input_Sources.RightHand);
                if (leftTurn && !completedLeft)
                {
                    text.text = "Turn your head Right by pressing right on the thumb sticks";
                    completedLeft = true;
                }
                else if (rightTurn && !completedRight)
                {
                    text.text = "Turn your head Left by pressing left on the thumb sticks";
                    completedRight = true;
                }
                else if (completedLeft && completedRight)
                {
                    text.text = "Great job, now let's try movement";
                    trainingIndex++;
                }
                else if (!completedLeft && !completedRight)
                {
                    text.text = "Turn your head using the thumb sticks";
                }
                break;
            case 1:
                if (waitTime <= 0)
                {
                    if (!gameEnabled)
                    {
                        teleportArea.SetActive(true);
                        SetActiveRecursively(teleportArea, true);
                        teleporting.SetActive(true);
                        gameEnabled = true;
                    }
                    bool teleported = SteamVR_Input.GetState("Teleport", SteamVR_Input_Sources.LeftHand) || SteamVR_Input.GetState("Teleport", SteamVR_Input_Sources.RightHand);
                    if (triedTeleporting && !teleported && originalPosition != this.gameObject.transform.position)
                    {
                        teleportCount++;
                        triedTeleporting = false;
                        waitTime = 0.5f;
                    }
                    if (teleported)
                    {
                        triedTeleporting = true;
                        originalPosition = this.gameObject.transform.position;
                        
                    }
                    
                    if (teleportCount == 0)
                    {
                        text.text = "Try Teleporting around the room by pressing the thumbstick forward";
                    }
                    else if (teleportCount >= 0 && teleportCount < 2)
                    {
                        
                        text.text = "Try Teleporting " + (2 - teleportCount).ToString() + " times to get used to teleporting around the room";
                    }
                    else if (teleportCount == 2)
                    {
                        text.text = "Great job, now let's try sorting recycling";
                        trainingIndex++;
                        waitTime = baseWaitTime;
                        gameEnabled = false;
                    }
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
                break;
            case 2:
                if (waitTime <= 0)
                {
                    if (!gameEnabled)
                    {
                        SetActiveRecursively(startPedestal, true);
                        blueBin.SetActive(true);
                        gameEnabled = true;
                    }
                    if (gm && gm.waveIndex == 2)
                    {
                        text.text = "Great job, now let's try sorting contaminants. Press the green button on the pedastal to begin once you are ready.";
                        trainingIndex++;
                        waitTime = baseWaitTime;
                        gameEnabled = false;
                    }
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
                break;
            case 3:
                if (waitTime <= 0)
                {
                    if (!gameEnabled)
                    {
                        redBin.SetActive(true);
                        gameEnabled = true;
                    }
                    if (gm.waveIndex == 3)
                    {
                        text.text = "Great job, now let's try tools";
                        trainingIndex++;
                        waitTime = baseWaitTime;
                        gameEnabled = false;
                    }
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
                break;
            case 4:
                if (waitTime <= 0)
                {
                    if (!gameEnabled)
                    {
                        SetActiveRecursively(shop, true);
                        gameEnabled = true;
                    }
                    if (gm.waveIndex == 4)
                    {
                        text.text = "Great job, now let's try a bomb";
                        trainingIndex++;
                        waitTime = baseWaitTime;
                        gameEnabled = false;
                    }
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
                break;
            case 5:
                if (waitTime <= 0)
                {
                    if (gm.waveIndex == 5)
                    {
                        text.text = "Great job, now let's try a disenfecting items";
                        trainingIndex++;
                        waitTime = baseWaitTime;
                    }
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
                break;
            case 6:
                if (waitTime <= 0)
                {
                    if (gm.waveIndex == 6)
                    {
                        text.text = "Great job, now let's try a disenfecting items";
                        trainingIndex++;
                        waitTime = baseWaitTime;
                    }
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
                break;
            default:
                break;
        }
        
    }

    public static void SetActiveRecursively(GameObject rootObject, bool active)
    {
        rootObject.SetActive(active);
        Debug.Log(rootObject.name.ToString());
        Debug.Log(rootObject.transform.ToString());
        foreach (Transform childTransform in rootObject.transform)
        {
            Debug.Log(childTransform.name.ToString());
            //if (childTransform.name != rootObject.name)
            //{
                SetActiveRecursively(childTransform.gameObject, active);
            //}
        }
    }
}
