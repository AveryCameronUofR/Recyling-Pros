using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStateController : MonoBehaviour
{
    public GameObject rightHand;

    private GameManager.GameStates playerState;
    private controllerInput controllerInput;
    private raycastPointer raycastPointer;

    void Start()
    {
        playerState = GameManager.gm.currState;
        controllerInput = rightHand.GetComponent<controllerInput>();
        raycastPointer = rightHand.GetComponent<raycastPointer>();
    }

    void Update()
    {
        playerState = GameManager.gm.currState;

        if (playerState.Equals(GameManager.GameStates.GameOver))
        {
            controllerInput.enabled = true;
            raycastPointer.enabled = true;
        }
        else
        {
            controllerInput.enabled = false;
            raycastPointer.enabled = false;
        }
    }
}
