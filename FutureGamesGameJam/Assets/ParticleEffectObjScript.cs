using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectObjScript : MonoBehaviour
{

	float lifetime;

	// Use this for initialization
	void Start()
	{
		StartCoroutine("KillMe");
	}

	// Update is called once per frame
	void Update()
	{

	}

	IEnumerator KillMe()
	{
		yield return new WaitForSeconds(transform.GetChild(0).GetComponent<ParticleSystem>().startLifetime);
		Destroy(gameObject);
	}
}
