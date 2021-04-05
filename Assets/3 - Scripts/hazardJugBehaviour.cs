using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hazardJugBehaviour : MonoBehaviour
{
    public GameObject contaminationAura;

    void Start()
    {

    }

    void Update()
    {

    }

    public void InfectMe()
    {
        contaminationAura.SetActive(true);
    }

    public void CleanMe()
    {
        contaminationAura.SetActive(false);
    }    
}
