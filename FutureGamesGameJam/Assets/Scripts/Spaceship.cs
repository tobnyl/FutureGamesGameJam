using UnityEngine;
using System.Collections;

public class Spaceship : MonoBehaviour 
{
    #region Fields/Properties

    [Header("Fire Spawn Point")]
    public Transform FireSpawnPoint;

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


    private Vector2 AxisLeft
    {
        get { return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); }
    }

    private float Trigger
    {
        get { return Input.GetAxis("Boost"); }
    }

    private bool FireButton
    {
        get { return Input.GetButtonDown("Fire1"); }
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
        Debug.LogFormat("FirButton: {0}", FireButton);
        //Debug.LogFormat("Velocity: {0}", _rigidbody.velocity.magnitude);
	}

    void FixedUpdate()
    {
        //Debug.LogFormat("Magnitud: {0} | Max {1}", _rigidbody.velocity.magnitude, MaxVelocity);

        if (_rigidbody.velocity.sqrMagnitude > _sqrMaxVelocity)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * MaxVelocity;
        }

        _force = IdleForce;

        if (Trigger > 0)
        {
            _force *= BoostMultiplier;
        }

        //Debug.LogFormat("Force: {0}", _force);

        _rigidbody.AddRelativeForce(Vector3.forward * _force);


        _rigidbody.AddRelativeTorque(Vector3.right * (InvertedPitch ? AxisLeft.y : -AxisLeft.y) * PitchTorque);
        _rigidbody.AddRelativeTorque(Vector3.up * AxisLeft.x * YawTorque);
        //_rigidbody.AddRelativeTorque(transform.forward * (-AxisLeft.x) * RollTorque);

        //if (_isFiringRight)
        //{
        //    AudioManager.Instance.Play(LazerSfx, transform.position);

        //    InstantiateProjectile(SpawnLeft);
        //    InstantiateProjectile(SpawnRight);
        //    _isFiringRight = false;
        //}

    }

    #endregion
    #region Methods



    #endregion
}
