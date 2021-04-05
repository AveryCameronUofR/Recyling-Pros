using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazardJugBehaviour : MonoBehaviour
{
    public GameObject contaminationAura;
    public GameObject hazardJug;
    public GameObject cleanJug;

    public void InfectMe()
    {
        contaminationAura.SetActive(true);
        hazardJug.SetActive(true);
        cleanJug.SetActive(false);
        gameObject.layer = 10;
    }

    public void CleanMe()
    {
        contaminationAura.SetActive(false);
        cleanJug.SetActive(true);
        hazardJug.SetActive(false);
        gameObject.layer = 8;
    }
}
