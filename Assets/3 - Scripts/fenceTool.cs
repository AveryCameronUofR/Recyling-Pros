using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
using System.Linq;
public class fenceTool : MonoBehaviour
{
    public bool inHand { get; set; }
    public float timer = 10f;
    public int itemsToCatch = 5;
    public GameObject destorying;
    public AudioSource audioSource;
    public GameObject[] fencePlacements;

    private bool placed = false;
    private bool placable = false;
    private bool soundPlayed = false;
    private GameObject fenceLoc;
    private List<GameObject> recyclingCaught = new List<GameObject>();
    void Start()
    {
        inHand = false;
        fencePlacements = GameObject.FindGameObjectsWithTag("FencePlacement");
    }

    void Update()
    {
        if (inHand)
        {
            AddHighlight();
        } else
        {
            RemoveHighlight();
        }
        if (placed)
        {
            timer -= Time.deltaTime;
        }
        if ((timer <= 1 || recyclingCaught.Count >= itemsToCatch) && !audioSource.isPlaying && !soundPlayed)
        {
            audioSource.Play();
            soundPlayed = true;
            Invoke("DestroyNow", 0.75f);
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
    #region Placement Triggers & Highlights
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

    public void AddHighlight()
    {
        float min = Mathf.Abs(Vector3.Distance(this.transform.position, fencePlacements[0].transform.position));
        GameObject minObject = fencePlacements[0];
        minObject.GetComponent<MeshRenderer>().enabled = false;
        for (int i = 1; i < fencePlacements.Length; i++)
        {
            fencePlacements[i].GetComponent<MeshRenderer>().enabled = false;
            float dist = Mathf.Abs(Vector3.Distance(this.transform.position, fencePlacements[i].transform.position));
            if (dist < min)
            {
                minObject = fencePlacements[i];
            }
        }
        minObject.GetComponent<MeshRenderer>().enabled = true;
    }

    public void RemoveHighlight()
    {
        foreach (GameObject place in fencePlacements)
        {
            place.GetComponent<MeshRenderer>().enabled = false;
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
