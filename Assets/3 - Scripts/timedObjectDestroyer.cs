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
	private Material bottleCapMat;
	private bool dying;
	private float fadeAmount;
	private float fadeDuration;

	private void Start()
    {
		if (gameObject.CompareTag("bottle"))
        {
			initialMat = gameObject.transform.Find("Cylinder").gameObject.GetComponent<MeshRenderer>().material;
			bottleCapMat = gameObject.transform.Find("Cylinder.001").gameObject.GetComponent<MeshRenderer>().material;
		}
		else if (gameObject.CompareTag("apple"))
        {
			initialMat = gameObject.transform.Find("default").gameObject.GetComponent<MeshRenderer>().material;
		}
        else 
        {
			initialMat = gameObject.GetComponent<MeshRenderer>().material;
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
		if (fadeAmount < fadeDuration)
		{
			fadeAmount += Time.deltaTime * fadeDuration;

			MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer r in renderers)
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

		//if (!initialMat)
		//	initialMat = gameObject.GetComponent<MeshRenderer>().material;

		MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();

		if (gameObject.CompareTag("bottle"))
        {
			foreach (MeshRenderer r in renderers)
			{
				if (r.gameObject.name == "Cylinder.001")
					continue;

				r.material = initialMat;
			}
		}
		else
        {
			foreach (MeshRenderer r in renderers)
			{
				r.material = initialMat;
			}
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
		initialMat = mat;

		MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer r in renderers)
		{
			if (r.gameObject.name == "Cylinder.001")
				continue;

			r.material = initialMat;
		}
	}

	private void DestroyNow()
	{
		if (detachChildren)
		{ // detach the children before destroying if specified
			transform.DetachChildren();
		}

		GameManager.gm.DecreaseScore();
		// destory the game Object
		Destroy(gameObject);
	}
}
