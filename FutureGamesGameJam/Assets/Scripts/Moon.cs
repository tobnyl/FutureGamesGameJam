﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Moon : MonoBehaviour 
{
    #region Fields/Properties

    public int StartHealth = 100;
    public int DamageAtCollision = 10;

    [SerializeField]
    private int _currentHealth;

    private FracturedObject _fracturedObject;
    private List<Transform> _chunkList;

    #endregion
    #region Events

    void Awake()
	{
        _fracturedObject = GetComponentInChildren<FracturedObject>();
        _chunkList = GetComponentsInChildren<Transform>().ToList();
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

        if (_currentHealth < 0)
        {
            foreach (var chunk in _chunkList)
            {
                chunk.gameObject.layer = LayerMask.NameToLayer("Chunk");
                chunk.transform.parent = GameManager.Instance.ChunksParent.transform;
            }

            //GameManager.Instance.IsMoonDestroyed = true;
            _fracturedObject.Explode(transform.position, GameManager.Instance.MoonExplodeForce);
        }
    }
	
	#endregion
}
