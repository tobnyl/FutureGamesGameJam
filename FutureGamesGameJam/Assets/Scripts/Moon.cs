﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class Moon : MonoBehaviour
{
	#region Fields/Properties

	public int StartHealth = 100;
	public int DamageAtCollision = 10;
    public GameObject Outline;

	[SerializeField]
	private int _currentHealth;

	private FracturedObject _fracturedObject;
	private List<FracturedChunk> _chunkList;

    private ChangeColor _changeColor;

	#endregion
	#region Events

	void Awake()
	{
		_fracturedObject = GetComponentInChildren<FracturedObject>();
		_chunkList = GetComponentsInChildren<FracturedChunk>().ToList();
        _changeColor = GetComponent<ChangeColor>();
	}

	void Start()
	{
		_currentHealth = StartHealth;
	}

	void Update()
	{
	}

	//private void OnTriggerEnter(Collider c)
	//{
	//    if (c.gameObject.layer == Layers.Asteroid.Index)
	//    {


	//    }
	//}

	#endregion
	#region Methods

	public void TakeDamage()
	{
		Debug.Log("ASteroid hit moon");
		_currentHealth -= DamageAtCollision;

        _changeColor.SetColor((float)_currentHealth / StartHealth);

		if (_currentHealth < 0)
		{
            Outline.SetActive(false);

			foreach (var chunk in _chunkList)
			{
				chunk.gameObject.layer = LayerMask.NameToLayer("Chunk");
				chunk.transform.parent = GameManager.Instance.ChunksParent.transform;
			}

			StartCoroutine("GameOver");

			//GameManager.Instance.IsMoonDestroyed = true;
			_fracturedObject.Explode(transform.position, GameManager.Instance.MoonExplodeForce);
		}
	}

	IEnumerator GameOver()
	{
		PlayerPrefs.SetFloat("Time", GameObject.Find("GameManager").GetComponent<AsteroidSpawner>().ElapsedTime);
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene(2);

	}

	#endregion
}
