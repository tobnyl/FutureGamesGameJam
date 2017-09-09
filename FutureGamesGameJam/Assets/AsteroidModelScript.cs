using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidModelScript : MonoBehaviour
{
    Vector3 modelRotDir;
    float rotationSpeed;
    // Use this for initialization
    void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
        transform.Rotate(modelRotDir * rotationSpeed);
    }

	public void SetUpMesh(Vector3 rot, float rotSpeed)
	{
		//Mesh mesh = Resources.Load<Mesh>("Peanut/asteroid" + Random.Range(1, 4).ToString()) as Mesh;
		//Debug.Log(mesh);
		//GetComponent<MeshFilter>().mesh = mesh;
		//GetComponent<MeshCollider>().sharedMesh = mesh;
        rotationSpeed = rotSpeed;
        modelRotDir = rot;
	}

	private void OnTriggerEnter(Collider other)
	{
		transform.parent.GetComponent<AsteroidLogic>().Collided(other.tag);
	}
}
