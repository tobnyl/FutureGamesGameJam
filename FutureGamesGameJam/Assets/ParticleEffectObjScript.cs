using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectObjScript : MonoBehaviour
{
	float lifetime;
	ParticleSystem ps;

	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SetScale(Vector3 scale)
	{
		ps = transform.GetChild(0).GetComponent<ParticleSystem>();
		print(ps);
		StartCoroutine("KillMe");
		transform.GetChild(0).transform.localScale = scale * 2;
		ps.startSize = scale.x;
		ps.Play();

	}

	IEnumerator KillMe()
	{
		yield return new WaitForSeconds(ps.startLifetime);
		Destroy(gameObject);
	}
}
