using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

	[SerializeField] Transform moon;
	[SerializeField] GameObject plainAsteroid;

	public GameObject[] AsteroidPrefabs;

	float elapsedTime;
	float timeUntilSpawn;
	float baseSpawnTime = 10;
	float spawnTime = 10;

	bool initialWait = false;

	[SerializeField] float baseSpawnDistance;

	float spawnDistance;

	[SerializeField] float elapsedSecPerWaveSec;

	// Use this for initialization
	void Start()
	{
		StartCoroutine("InitialWave");
		spawnDistance = baseSpawnDistance;
	}

	void SpawnLoop()
	{
		elapsedTime += Time.deltaTime;

		if (timeUntilSpawn <= 0)
		{
			spawnTime = baseSpawnTime - elapsedTime / elapsedSecPerWaveSec;

			switch (Random.Range(1, 4))
			{
				case 1:
					SpawnAsteroid(1);
					timeUntilSpawn = spawnTime;
					break;

				case 2:
					SpawnAsteroid(2);
					timeUntilSpawn = spawnTime * 1.33f;
					break;

				case 3:
					spawnDistance *= 1.3f;
					SpawnAsteroid(3);
					timeUntilSpawn = spawnTime * 2;
					break;
			}
		}

		else
		{
			timeUntilSpawn -= Time.deltaTime;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (initialWait)
			SpawnLoop();

		if (Input.GetKeyDown(KeyCode.Space))
		{
			SpawnAsteroid(1);
		}
	}

	IEnumerator InitialWave()
	{
		yield return new WaitForSeconds(7);
		initialWait = true;
		SpawnAsteroid(1);
	}

	public float ElapsedTime
	{
		get { return elapsedTime; }
	}

	public void SpawnAsteroid(int spawnAmount)
	{
		//Fulkod like a boss

		for (int i = 0; i < spawnAmount; i++)
		{
			var currentAsteroid = AsteroidPrefabs[Random.Range(0, AsteroidPrefabs.Length - 1)];
			var g = Instantiate(currentAsteroid);

			//GameObject g = (GameObject)Instantiate(plainAsteroid);
			g.transform.position = new Vector3(0, 0, spawnDistance);

			g.transform.RotateAround(moon.position, Vector3.up, Random.Range(0, 360));
			g.transform.RotateAround(moon.position, Vector3.forward, Random.Range(0, 360));
			g.transform.RotateAround(moon.position, Vector3.right, Random.Range(0, 360));

			g.GetComponent<AsteroidLogic>().SetUpAsteroid(moon);
		}

		spawnDistance = baseSpawnDistance;
	}
}
