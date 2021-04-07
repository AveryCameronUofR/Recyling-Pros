using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static GameManager;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int trainingIndex = 0;
    public int traiingModules = 10;
    public GameObject trainingText;
    public GameObject trainingText2;
    public float baseWaitTime = 3;

    public GameObject teleportArea;
    public GameObject teleporting;

    public GameObject startPedestal;
    public GameObject blueBin;

    public GameObject redBin;

    public GameObject shop;

    private Text text;
    private Text text2;
    private string message;

    private int teleportCount = 0;
    private float waitTime;

    private bool completedLeft, completedRight = false;

    
    private bool triedTeleporting = false;
    private Vector3 originalPosition;

    private bool gameEnabled;
    void Start()
    {
        text = trainingText.GetComponent<Text>();
        text2 = trainingText2.GetComponent<Text>();
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
                    message = "Turn your head Right by pressing right on the thumb sticks";
                    UpdateText(message);
                    completedLeft = true;
                }
                else if (rightTurn && !completedRight)
                {
                    message = "Turn your head Left by pressing left on the thumb sticks";
                    UpdateText(message);
                    completedRight = true;
                }
                else if (completedLeft && completedRight)
                {
                    message = "Great job, now let's try movement";
                    UpdateText(message);
                    trainingIndex++;
                }
                else if (!completedLeft && !completedRight)
                {
                    message = "Turn your head using the thumb sticks";
                    UpdateText(message);
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
                        message = "Try Teleporting around the room by pressing the thumbstick forward";
                        UpdateText(message);
                    }
                    else if (teleportCount >= 0 && teleportCount < 3)
                    {
                        
                        message = "Try Teleporting " + (3 - teleportCount).ToString() + " times to get used to teleporting around the room";
                        UpdateText(message);
                    }
                    else if (teleportCount == 3)
                    {
                        message = "Great job, now let's try sorting recycling. Press the green button on the pedastal to begin once you are ready";
                        UpdateText(message);
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
                    if (gm.waveIndex == 1)
                    {
                        message = "Great job, now let's try sorting contaminants.";
                        UpdateText(message);
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
                    if (gm.waveIndex == 2)
                    {
                        message = "Great, ready for a special item?";
                        UpdateText(message);
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
                        redBin.SetActive(true);
                        gameEnabled = true;
                    }
                    if (gm.waveIndex == 3)
                    {
                        message = "Great job, now let's try tools";
                        UpdateText(message);
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
                    if (!gameEnabled)
                    {
                        SetActiveRecursively(shop, true);
                        gameEnabled = true;
                    }
                    if (gm.waveIndex == 4)
                    {
                        message = "Great job, now let's try a bomb";
                        UpdateText(message);
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
            case 6:
                if (waitTime <= 0)
                {
                    if (gm.waveIndex == 5)
                    {
                        message = "Great job, now let's try a disenfecting items";
                        UpdateText(message);
                        trainingIndex++;
                        waitTime = baseWaitTime;
                    }
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
                break;
            case 7:
                if (waitTime <= 0)
                {
                    if (gm.waveIndex == 6)
                    {
                        message = "Great job, now let's try a disenfecting items";
                        UpdateText(message);
                        trainingIndex++;
                        waitTime = baseWaitTime;
                    }
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
                break;
            case 8:
                if (waitTime <= 0)
                {
                    message = "Great job, the Tutorial is complete!";
                    UpdateText(message);
                    trainingIndex++;
                    waitTime = baseWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
                break;
            case 9:
                SceneManager.LoadScene("Menu");
                break;
            default:
                break;
        }
        Debug.Log(gm.waveIndex);
        
    }

    public static void SetActiveRecursively(GameObject rootObject, bool active)
    {
        rootObject.SetActive(active);
        foreach (Transform childTransform in rootObject.transform)
        {
            SetActiveRecursively(childTransform.gameObject, active);
        }
    }

    public void UpdateText(string textString)
    {
        text.text = textString;
        text2.text = textString;
    }
}
