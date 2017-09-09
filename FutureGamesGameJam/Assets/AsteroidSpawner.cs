using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

	[SerializeField] Transform moon;
	[SerializeField] GameObject plainAsteroid;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			SpawnAsteroid();
		}
	}

	public void SpawnAsteroid()
	{

		GameObject g = (GameObject)Instantiate(plainAsteroid);
		g.transform.position = new Vector3(0, 0, 200);

		g.transform.RotateAround(moon.position, Vector3.up, Random.Range(0, 360));
		g.transform.RotateAround(moon.position, Vector3.forward, Random.Range(0, 360));
		g.transform.RotateAround(moon.position, Vector3.right, Random.Range(0, 360));

		g.GetComponent<AsteroidLogic>().SetUpAsteroid(moon);
	}
}
