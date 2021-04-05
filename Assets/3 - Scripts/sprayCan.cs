using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprayCan : MonoBehaviour
{
    public bool inHand { get; set; }

    private ParticleSystem sprayParticles;
    public AudioSource audioSource;
    private void Start()
    {
        sprayParticles = gameObject.GetComponent<ParticleSystem>();
        sprayParticles.Stop();
        inHand = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.layer == 10)
        {
            if (GameManager.gm.goodItems.Contains(other.tag) || GameManager.gm.badItems.Contains(other.tag))
            {
                CleanItem(other);
            }
            else if (other.CompareTag("jug"))
            {
                other.GetComponent<hazardJugBehaviour>().CleanMe();
            }
        }
    }

    private void CleanItem(GameObject objToClean)
    {
        objToClean.layer = 8;
    }

    public void TriggerState(bool triggered)
    {
        if (inHand && triggered)
        {
            sprayParticles.Play();
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            sprayParticles.Stop();
            audioSource.Stop();
        }
    }
}
