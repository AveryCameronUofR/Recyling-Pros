using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timedObjectDestroyer : MonoBehaviour
{
	public float timeOut = 1.0f;
	public bool detachChildren = false;
	public Color despawnColor = Color.red;

	private Renderer rend;
	private Color baseColor;
	private bool dying;
	private float currTime = 0.0f;

    private void Start()
    {
		rend = gameObject.GetComponent<MeshRenderer>();
	}

    public void KillMe()
	{
		// invote the DestroyNow funtion to run after timeOut seconds
		StartCoroutine(LerpFunction(despawnColor, timeOut));
	}

	public void SaveMe()
    {
		CancelInvoke();
    }		

	public bool isDying()
    {
		return dying;
    }

	private void DestroyNow()
	{
		if (detachChildren)
		{ // detach the children before destroying if specified
			transform.DetachChildren();
		}

		// destory the game Object
		Destroy(gameObject);
	}

	IEnumerator LerpFunction(Color despawnColor, float duration)
	{
		float time = 0;

		baseColor = rend.material.color;

		while (time < duration)
		{
			rend.material.color = Color.Lerp(baseColor, despawnColor, time / duration);
			time += Time.deltaTime;
			yield return null;
		}

		rend.material.color = despawnColor;
		Invoke("DestroyNow", (timeOut / 4));
	}
}
