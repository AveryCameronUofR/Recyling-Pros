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
	private bool dying;
	private float fadeAmount;
	private float fadeDuration;

	private void Start()
    {
		if (gameObject.CompareTag("bottle"))
        {
			initialMat = gameObject.transform.Find("Cylinder").gameObject.GetComponent<MeshRenderer>().material;
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

	public void ChangeColour()
	{
		if (fadeAmount < fadeDuration)
		{
			fadeAmount += Time.deltaTime * fadeDuration;

			MeshRenderer [] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
				
			foreach (MeshRenderer r in renderers)
			{
				r.material.color = Color.Lerp(initialColor, dyingColor, fadeAmount);
			}
		}
	}

	public void ChangeMaterial()
	{
		if (fadeAmount < fadeDuration)
		{
			fadeAmount += Time.deltaTime * fadeDuration;

			MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
			foreach (MeshRenderer r in renderers)
			{
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
		
		MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
		foreach (MeshRenderer r in renderers)
		{
			r.material = new Material(initialMat);
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
