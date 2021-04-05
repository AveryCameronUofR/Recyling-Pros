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

    void Update()
    {
        if (gameObject.layer == 10)
        {
            if (currMaterial != infectedMaterial)
            {
                ChangeMaterial(infectedMaterial);
                currMaterial = infectedMaterial;
            }
        }
        else
        {
            if (currMaterial != originalMaterial)
            {
                ChangeMaterial(originalMaterial);
                currMaterial = originalMaterial;
            }
        }
    }

    private void ChangeMaterial(Material mat)
    {
        if (gameObject.CompareTag("bottle") && mat == originalMaterial)
        {
            Debug.Log("IM A JUICY BOTTLE");
            foreach (MeshRenderer r in renderers)
            {
                if (r.gameObject.name == "Cylinder.001")
                {
                    Debug.Log("IM A LABEL");
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
