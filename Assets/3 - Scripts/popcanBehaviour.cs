using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class popcanBehaviour : MonoBehaviour
{
    public float maxVelocity;
    public float safetyDelay;
    public float shakeSpeed;
    public Material initialMat;
    public Material popcanExplodeMat;
    public GameObject explosion;

    public bool inHand { get; set; }

    private Rigidbody rb;
    private float currDelayTime;
    private int shakeDirection = 1;
    private float shakeTime;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        currDelayTime = safetyDelay;
        shakeTime = shakeSpeed;
    }

    private void Update()
    {
        if (inHand)
        {
            // ShakeMe();
            ChangeMaterial(rb.velocity.magnitude);
            
            currDelayTime -= Time.deltaTime;
            if (currDelayTime <= 0)
            {
                CheckIfExploding();
            }
        }
        else
        {
            if (currDelayTime < safetyDelay)
                currDelayTime = safetyDelay;
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

    private void ShakeMe()
    {
        shakeTime -= Time.deltaTime;
        if (shakeTime <= 0)
        {
            float shakeAmount = rb.velocity.magnitude / maxVelocity;

            Quaternion currQuaternion = gameObject.transform.rotation;
            Quaternion newQuaternion = currQuaternion;

            shakeDirection *= -1;

            newQuaternion.x += shakeDirection * shakeAmount;
            newQuaternion.y += shakeDirection * shakeAmount;
            newQuaternion.z += shakeDirection * shakeAmount;

            gameObject.transform.rotation = newQuaternion;

            shakeTime = shakeSpeed;
        }
    }

    private void CheckIfExploding()
    {
        if (rb.velocity.magnitude > (maxVelocity * 0.75f))
        {
            Invoke("DestroyNow", 0.5f);
        }
    }

    private void DestroyNow()
    {
        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
