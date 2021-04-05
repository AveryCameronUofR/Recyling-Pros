using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class applyInfection : MonoBehaviour
{
    public Material infectedMaterial;
    public Material originalMaterial;

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
        foreach (MeshRenderer r in renderers)
        {
            r.material = mat;
        }
    }
}
