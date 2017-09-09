﻿using UnityEngine;
using System.Collections;

public class Spaceship : MonoBehaviour 
{
    #region Fields/Properties

    [Header("Objects")]
    public Transform FireSpawnPoint;
    public GameObject LaserPrefab;

    [Header("Force")]
    public float BoostMultiplier = 1;
    public float IdleForce = 1;

    [Header("Torque")]
    public bool InvertedPitch = true;
    public float PitchTorque = 1;
    public float YawTorque = 1;
    public float RollTorque = 1;

    [Header("Max")]
    public float MaxVelocity = 20;

    private Rigidbody _rigidbody;
    private bool _isFiring;
    private float _sqrMaxVelocity;
    private float _force;
    private float _fireButtonDownTimer;


    private Vector2 AxisLeft
    {
        get { return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); }
    }

    private float Trigger
    {
        get { return Input.GetAxis("Boost"); }
    }

    private bool FireButtonDown
    {
        get { return Input.GetButtonDown("Fire1"); }
    }

    private bool FireButtonUp
    {
        get { return Input.GetButtonUp("Fire1"); }
    }

    private bool FireButton
    {
        get { return Input.GetButton("Fire1"); }
    }

    #endregion
    #region Events

    void Awake()
	{
        _rigidbody = GetComponent<Rigidbody>();

        _sqrMaxVelocity = MaxVelocity * MaxVelocity;        
	}
	
	void Start() 
	{
		
	}

	void Update() 
	{
        if (FireButtonDown)
        {
            Instantiate(LaserPrefab, FireSpawnPoint.position, FireSpawnPoint.rotation);
        }

        if (FireButton)
        {
            _fireButtonDownTimer += Time.deltaTime;
        }
        else if (FireButtonUp)
        {
            Debug.LogFormat("Timer: {0}", _fireButtonDownTimer);

            _fireButtonDownTimer = 0;
        }
	}

    void FixedUpdate()
    {
        if (_rigidbody.velocity.sqrMagnitude > _sqrMaxVelocity)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * MaxVelocity;
        }

        _force = IdleForce;

        if (Trigger > 0)
        {
            _force *= BoostMultiplier;
        }

        _rigidbody.AddRelativeForce(Vector3.forward * _force);


        _rigidbody.AddRelativeTorque(Vector3.right * (InvertedPitch ? AxisLeft.y : -AxisLeft.y) * PitchTorque);
        _rigidbody.AddRelativeTorque(Vector3.up * AxisLeft.x * YawTorque);
    }

    #endregion
    #region Methods



    #endregion
}
