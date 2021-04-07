using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timedObjectDestroyer : MonoBehaviour
{
	public float timeToDespawn;
	public float secondsOfColourChange;
	public bool detachChildren = false;
	public Color dyingColor;
	public Color initialColor;
	public Material dyingMat;
	
	private Material initialMat;
	private bool dying = false;
	private float fadeAmount;
	private float fadeDuration;

	private void Start()
    {
		if (gameObject.CompareTag("bottle"))
        {
			initialMat = gameObject.transform.Find("Cylinder").gameObject.GetComponent<Renderer>().material;
		}
		else if (gameObject.CompareTag("apple"))
        {
			initialMat = gameObject.transform.Find("default").gameObject.GetComponent<Renderer>().material;
		}
        else 
        {
			initialMat = gameObject.GetComponent<Renderer>().material;
		}

		if (timeToDespawn < secondsOfColourChange)
			timeToDespawn = secondsOfColourChange;

		fadeDuration = secondsOfColourChange;
	}

    private void Update()
    {
		if (dying)
			ChangeMaterial();
	}

	public void ChangeMaterial()
	{
		//Debug.Log("Lerp Material");
		if (fadeAmount < fadeDuration && dying)
		{
			fadeAmount += Time.deltaTime * fadeDuration;

			Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
			foreach (Renderer r in renderers)
			{
				if (r.gameObject.name == "Cylinder.001")
					continue;

				r.material.Lerp(initialMat, dyingMat, fadeAmount);
			}
		}
	}

	public void KillMe()
	{
		dying = true;
		Invoke("DestroyNow", timeToDespawn);
	}

	public void SaveMe()
    {
		dying = false;

		//Renderer r1 = gameObject.GetComponent<Renderer>();
		Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();

		if (gameObject.CompareTag("bottle"))
        {
			foreach (Renderer r in renderers)
			{
				if (r.gameObject.name == "Cylinder.001")
					continue;

				r.material = new Material(initialMat);
			}
		}
		else
        {
			foreach (Renderer r in renderers)
			{
				r.material = new Material(initialMat);
			}

			//r1.material = new Material(initialMat);
		}

		fadeAmount = 0;
		CancelInvoke();
	}		

	public bool isDying()
    {
		return dying;
    }

	public void ChangeMyMaterialRemote(Material mat)
    {
		initialMat = new Material(mat);

		MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer r in renderers)
		{
			if (r.gameObject.name == "Cylinder.001")
				continue;

			r.material = new Material(initialMat);
		}
	}

	private void DestroyNow()
	{
		if (detachChildren)
		{ // detach the children before destroying if specified
			transform.DetachChildren();
		}

		GameManager.gm.DecreaseScore(-4);
		// destory the game Object
		Destroy(gameObject);
	}
}
