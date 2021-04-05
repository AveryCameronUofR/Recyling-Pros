using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class bombBehaviour : MonoBehaviour
{
    public float explosionDelay;
    public Material initialMat;
    public Material bombExplodeMat;
    public GameObject explosion;
    public GameObject explosionRadius;
    public Renderer r;

    public Hand hand { get; set; }
    public bool inHand { get; set; }
    public bool triggerPressed { get; set; }

    private float currDelayTime;
    private bool isArmed = false;
    private explodeInRadius eInRadius;

    private void Start()
    {
        currDelayTime = explosionDelay;
        eInRadius = explosionRadius.GetComponent<explodeInRadius>();
    }

    private void Update()
    {
        if (inHand && triggerPressed)
            isArmed = true;

        if (isArmed)
        {
            currDelayTime -= Time.deltaTime;
            ChangeMaterial();
            
            if (currDelayTime <= 0)
            {
                hand.DetachObject(gameObject);
                DestroyNow();
            }
        }
    }

    public void ChangeMaterial()
    {
        r.material.Lerp(initialMat, bombExplodeMat, CalcLerpValue());
    }

    private void DestroyNow()
    {
        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
        eInRadius.DestroyItems();
        Destroy(gameObject);
    }

    public float CalcLerpValue()
    {
        float multiplier = Mathf.Pow(2 * (explosionDelay - currDelayTime), 2); 
        return Mathf.Sin(Mathf.PI*multiplier);
    }
}
