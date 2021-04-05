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
        currMaterial = originalMaterial;
    }

    public void UpdateInfectionState()
    {
        if (gameObject.layer == 10)
        {
            if (currMaterial != infectedMaterial)
            {
                ChangeMaterial(infectedMaterial);
                currMaterial = infectedMaterial;
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
                currMaterial = originalMaterial;
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
        if (gameObject.CompareTag("bottle") && mat == originalMaterial)
        {
            foreach (MeshRenderer r in renderers)
            {
                if (r.gameObject.name == "Cylinder.001")
                {
                    r.material = bottleLabel;
                }
                else
                {
                    r.material = originalMaterial;
                }
            }
        }
        else
        {
            foreach (MeshRenderer r in renderers)
            {
                r.material = mat;
            }
        }
    }
}
