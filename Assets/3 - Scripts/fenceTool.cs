using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
public class fenceTool : MonoBehaviour
{
    public bool inHand { get; set; }
    public float timer = 10f;
    public GameObject destorying;
    public AudioSource audioSource;

    private bool placed = false;
    private bool placable = false;
    private bool destroying = false;
    private GameObject fenceLoc;
    void Start()
    {
        inHand = false;
    }

    void Update()
    {
        if (placed)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 1 && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if (timer <= 0 && !destroying)
        {
            destroying = true;
            DestroyNow();
        }
        if (!placed && placable && !inHand)
        {
            placed = true;
            placable = false;
            this.gameObject.transform.position = fenceLoc.transform.position;
            this.gameObject.transform.rotation = fenceLoc.transform.rotation;
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            Destroy(this.gameObject.GetComponent<Interactable>());
            Destroy(this.gameObject.GetComponent<Throwable>());
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FencePlacement" && inHand)
        {
            Debug.Log("Placable");
            placable = true;
            fenceLoc = other.gameObject;
        } 
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FencePlacement" && placable)
        {
            Debug.Log("NotPlacable");
            placable = false;
        }
    }

    private void DestroyNow()
    {
        //Instantiate(destroying, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
