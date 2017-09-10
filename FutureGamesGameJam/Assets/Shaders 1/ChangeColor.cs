using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeColor : MonoBehaviour {

	public Material [] materials;
	public MeshRenderer Rend;

	private int index = 1;


	void Start () {
		//Rend = GetComponent<MeshRenderer> ();
		//Rend.enabled = true;
		Rend.sharedMaterial = materials[0];
		}

	void OnMouseDown()
	{

		if (materials.Length == 0)
			return;

		if (Input.GetMouseButtonDown (0)) {
			index += 1;

			if (index == materials.Length + 1)
				index = 1;

			print (index);

			Rend.sharedMaterial = materials [index - 1];
		}
	}
}