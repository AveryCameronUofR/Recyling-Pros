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
        Debug.Log("IM A DIRTY JUG");
        contaminationAura.SetActive(true);
        contaminationAura.GetComponent<Animation>().enabled = false;
        contaminationAura.GetComponent<Animator>().enabled = true;
    }

    public void CleanMe()
    {
        Debug.Log("IM A CLEAN JUG");
        contaminationAura.GetComponent<Animator>().enabled = false;
        contaminationAura.GetComponent<Animation>().enabled = false;
        contaminationAura.SetActive(false);
    }    
}
