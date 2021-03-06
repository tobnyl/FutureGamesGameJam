﻿using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour 
{
    #region Fields/Properties

    public float BaseForce = 1;
    public float Force = 1;
    public float DamagePoints = 1;
    public float DamageMaxMultiplier = 10;
    public float MaxScale = 1;

    private Rigidbody _rigidbody;
    private Vector3 _startPosition;


	#endregion
	#region Events
	
	void Awake()
	{
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	void Start() 
	{
        _rigidbody.AddRelativeForce(Vector3.forward * Force, ForceMode.Impulse);

        _startPosition = transform.position;
	}

	void Update() 
	{
		if (Vector3.Distance(transform.position, _startPosition) > GameManager.Instance.LaserMaxDistance)
        {
            Destroy(gameObject);
        }
	}

    private bool _isDestroyed;
    void OnTriggerEnter(Collider c)
    {
        if (!_isDestroyed)
        {
            var otherLayer = c.gameObject.layer;

            if (otherLayer == Layers.Moon.Index)
            {
                Destroy(gameObject);
            }
            else if (otherLayer == Layers.Asteroid.Index)
            {
                _isDestroyed = true;
                Destroy(gameObject);
                var asteroid = c.gameObject.GetComponent<AsteroidLogic>() ?? c.gameObject.GetComponentInParent<AsteroidLogic>();

                if (asteroid != null)
                {
                    asteroid.TakeDamage(DamagePoints);
                }
            }
        }
    }
	
	#endregion
	#region Methods
	
	public void Initialize(float timeElapsed)
    {
        var deltaTime = timeElapsed / GameManager.Instance.MaxLaserChargeTime;

        Force = BaseForce + Force - Mathf.Lerp(0, Force, deltaTime);

        transform.localScale = Vector3.zero;
        transform.localScale += Vector3.one * Mathf.Lerp(1, MaxScale, deltaTime);
        transform.position += transform.forward * (transform.localScale.z);

        DamagePoints += Mathf.Lerp(0, DamageMaxMultiplier, deltaTime);


    }
	
	#endregion
}
