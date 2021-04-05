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
    private bool soundPlayed = false;
    private GameObject fenceLoc;
    private List<GameObject> recyclingCaught = new List<GameObject>();
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
        if (timer <= 0 && !destroying)
        {
            destroying = true;
            DestroyNow();
        }
        if (timer <= 1 && !audioSource.isPlaying && !soundPlayed)
        {
            audioSource.Play();
            soundPlayed = true;
        }
        if (!placed && placable && !inHand)
        {
            placed = true;
            placable = false;
            this.gameObject.transform.position = fenceLoc.transform.position;
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            transform.rotation = new Quaternion(0, 90, 0, 0);
            transform.Rotate(0, 90, 0);
            
            Destroy(this.gameObject.GetComponent<InteractableHoverEvents>());
            Destroy(this.gameObject.GetComponent<Throwable>());
            Destroy(this.gameObject.GetComponent<Interactable>());
        }
    }
    #region Placement Triggers
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
    #endregion
    #region Recycling Triggers
    public void OnCollisionEnter(Collision collision)
    {
        //Freeze all recycling that touches the net
        if (GameManager.gm.goodItems.Contains(collision.gameObject.tag) || GameManager.gm.badItems.Contains(collision.gameObject.tag))
        {
            recyclingCaught.Add(collision.gameObject);
            collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
    #endregion
    private void DestroyNow()
    {
        Debug.Log("DESTROYING");
        foreach (GameObject recyclingObject in recyclingCaught)
        {
            recyclingObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        //Instantiate(destroying, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
