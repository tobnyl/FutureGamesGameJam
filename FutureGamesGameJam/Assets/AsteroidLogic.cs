using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidLogic : MonoBehaviour
{

	[SerializeField] Transform asteroidTarget;

	//This value is just visual to help setting up the object in the inspector
	[SerializeField] float movementSpeed;
	[SerializeField] float movementSpeedDifRange;
	[SerializeField] float baseMass;

	[SerializeField] float baseHp;
	float deathTime;

	float mass;
	float health;

	[SerializeField] GameObject ParticleObject;

	[Header("ModelStuff")]
	[SerializeField]
	float rotationSpeed;
	[SerializeField] float asteroidMinSize;
	[SerializeField] float asteroidMaxSize;

	Transform asteroidModel;

	Vector3 direction;
	Vector3 modelRotDir;

	//This is the valeu thats used by the code to move the asteroid
	float moveSpeed;

	// Use this for initialization
	void Start()
	{

	}

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

	public void SetUpAsteroid(Transform moon)
	{
		asteroidModel = transform.GetChild(0);
		asteroidModel.GetComponent<AsteroidModelScript>().SetUpMesh();
		modelRotDir = transform.rotation.eulerAngles * Random.Range(-0.2f, 0.2f);
		asteroidTarget = moon;
		moveSpeed = movementSpeed * (-0.15f + Random.Range(0, 0.3f));

		direction = -(transform.position - asteroidTarget.position).normalized;
		transform.rotation = Quaternion.Euler(0, 0, 0);

		deathTime = asteroidModel.GetComponent<TrailRenderer>().time;

		SetUpSize();

	}

	// Update is called once per frame
	void Update()
	{
		transform.Translate(direction * movementSpeed);
		RotateModel();
	}

	void SetUpSize()
	{

		float _size = Random.Range(asteroidMinSize, asteroidMaxSize);

		//size
		asteroidModel.localScale = asteroidModel.localScale * _size;

		//hp

		health = baseHp * _size;

		mass = baseMass * _size;

	}

	void RotateModel()
	{
		transform.GetChild(0).transform.Rotate(modelRotDir * rotationSpeed);
	}

	public void Collided(string typeHit)
	{
		//collision code here

		if (typeHit == "Moon")
		{
			//Play particle effect
			//Destroy moon
			Instantiate(ParticleObject, asteroidModel.transform.position, Quaternion.identity);
			asteroidModel.GetComponent<Collider>().enabled = false;
			asteroidModel.GetComponent<Renderer>().enabled = false;

			StartCoroutine("DestroySelf");
		}

		else if (typeHit == "Player")
		{

		}
	}

	IEnumerator DestroySelf()
	{
		yield return new WaitForSeconds(0.25f);
		movementSpeed = 0;
		yield return new WaitForSeconds(deathTime - 0.25f);
		Destroy(gameObject);
	}
}
