using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerStateController : MonoBehaviour
{
    public GameObject rightHand;
    public GameObject leftHand;

    private controllerInput rhControllerInput;
    private raycastPointer rhRaycastPointer;
    private controllerInput lhControllerInput;
    private raycastPointer lhRaycastPointer;

    private controllerInput selectedControllerInput;
    private raycastPointer selectedRaycastPointer;

    private GameManager.GameStates playerState;
    private controllerInput controllerInput;
    private raycastPointer raycastPointer;

    void Start()
    {
        rhControllerInput = rightHand.GetComponent<controllerInput>();
        rhRaycastPointer = rightHand.GetComponent<raycastPointer>();

        lhControllerInput = leftHand.GetComponent<controllerInput>();
        lhRaycastPointer = leftHand.GetComponent<raycastPointer>();

        selectedControllerInput = rhControllerInput;
        selectedRaycastPointer = rhRaycastPointer;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("MainMenu"))
        {
            selectedControllerInput.enabled = true;
            selectedRaycastPointer.enabled = true;
        }    
        else
        {
            playerState = GameManager.gm.currState;

            if (playerState.Equals(GameManager.GameStates.GameOver))
            {
                selectedControllerInput.enabled = true;
                selectedRaycastPointer.enabled = true;
            }
            else
            {
                lhRaycastPointer.enabled = false;
                rhRaycastPointer.enabled = false;
            }
        }    
    }

    public void SelectHand(string hand)
    {
        if (hand == "right")
        {
            // enable right hand raycast to equal left
            rhControllerInput.enabled = lhControllerInput.enabled;
            rhRaycastPointer.enabled = lhRaycastPointer.enabled;

            lhRaycastPointer.DeleteLineRenderer();
            rhRaycastPointer.CreateLineRenderer();

            // disable left hand
            lhRaycastPointer.enabled = false;

            selectedControllerInput = rhControllerInput;
            selectedRaycastPointer = rhRaycastPointer;
        }
        else
        {
            // enable left hand raycast to equal right
            lhControllerInput.enabled = rhControllerInput.enabled;
            lhRaycastPointer.enabled = rhRaycastPointer.enabled;

            rhRaycastPointer.DeleteLineRenderer();
            lhRaycastPointer.CreateLineRenderer();

            // disable right hand
            rhRaycastPointer.enabled = false;

            selectedControllerInput = lhControllerInput;
            selectedRaycastPointer = lhRaycastPointer;
        }
    }
}
