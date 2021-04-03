using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class popcanBehaviour : MonoBehaviour
{
    public float maxVelocity = 3.0f;
    public Material initialMat;
    public Material popcanExplodeMat;

    public bool inHand { get; set; }

    private Rigidbody rb;
    private float fadeAmount;
    
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (inHand)
        {
            ChangeMaterial(rb.velocity.magnitude);
        }
    }

    public void ChangeMaterial(float currVelocity)
    {
        if (currVelocity < maxVelocity)
        {
            MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer r in renderers)
            {
                r.material.Lerp(initialMat, popcanExplodeMat, currVelocity / maxVelocity);
            }
        }
    }

    public void ResetMaterial()
    {
        MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in renderers)
        {
            r.material = new Material(initialMat);
        }
    }
}
