using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class conveyorController : MonoBehaviour
{
    public GameObject[] conveyorChildren;

    private List<LinearConveyor> linearConvScripts = new List<LinearConveyor>();
    private List<RadialConveyor> radialConvScripts = new List<RadialConveyor>();

    private void Start()
    {
        foreach (GameObject conv in conveyorChildren)
        {
            if (conv.gameObject.CompareTag("straight_conv"))
            {
                linearConvScripts.Add(conv.GetComponent<LinearConveyor>());
            }
            else
            {
                radialConvScripts.Add(conv.GetComponent<RadialConveyor>());
            }
        }
    }

    public void UpdateSpeed(float speed)
    {
        foreach (LinearConveyor conv in linearConvScripts)
        {
            conv.ChangeSpeed(speed);
        }

        foreach (RadialConveyor conv in radialConvScripts)
        {
            conv.ChangeSpeed(speed);
        }
    }
}
