using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class applyInfection : MonoBehaviour
{
    public Material infectedMaterial;
    public Material originalMaterial;
    public Material bottleLabel;

    private Renderer [] renderers;
    private Material currMaterial;

    void Start()
    {
        renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        currMaterial = new Material(originalMaterial);
    }

    public void UpdateInfectionState()
    {
        if (gameObject.layer == 10)
        {
            if (currMaterial != infectedMaterial)
            {
                ChangeMaterial(infectedMaterial);
                currMaterial = new Material(infectedMaterial);
                if (gameObject.CompareTag("popcan"))
                {
                    gameObject.GetComponent<popcanBehaviour>().ChangeMyMaterialRemote(currMaterial);
                }
                else
                {
                    gameObject.GetComponent<timedObjectDestroyer>().ChangeMyMaterialRemote(currMaterial);
                }
            }
        }
        else
        {
            if (currMaterial != originalMaterial)
            {
                ChangeMaterial(originalMaterial);
                currMaterial = new Material(originalMaterial);
                if (gameObject.CompareTag("popcan"))
                {
                    gameObject.GetComponent<popcanBehaviour>().ChangeMyMaterialRemote(currMaterial);
                }
                else
                {
                    gameObject.GetComponent<timedObjectDestroyer>().ChangeMyMaterialRemote(currMaterial);
                }
            }
        }
    }

    private void ChangeMaterial(Material mat)
    {
        if (gameObject.CompareTag("bottle"))
        {
            foreach (MeshRenderer r in renderers)
            {
                if (r.gameObject.name == "Cylinder.001")
                    continue;
                else
                {
                    r.material = new Material(originalMaterial);
                }
            }
        }
        else
        {
            foreach (MeshRenderer r in renderers)
            {
                r.material = new Material(mat);
            }
        }
    }
}
