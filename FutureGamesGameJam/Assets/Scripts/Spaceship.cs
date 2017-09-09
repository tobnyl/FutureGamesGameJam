using UnityEngine;
using System.Collections;

public class Spaceship : MonoBehaviour 
{
    #region Fields/Properties

    [Header("Force")]
    public float ForwardForce = 1;
    public float IdleForce = 1;

    [Header("Torque")]
    public bool InvertedPitch = true;
    public float PitchTorque = 1;
    public float YawTorque = 1;
    public float RollTorque = 1;

    [Header("Max")]
    public float MaxVelocity = 20;

    private Rigidbody _rigidbody;
	
    private Vector2 AxisLeft
    {
        get { return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); }
    }

	#endregion
	#region Events
	
	void Awake()
	{
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	void Start() 
	{
		
	}

	void Update() 
	{
		
	}

    void FixedUpdate()
    {
        if (_rigidbody.velocity.magnitude > MaxVelocity)
        {
            _rigidbody.velocity = _rigidbody.velocity.normalized * MaxVelocity;

        }

        //if (Trigger > 0)
        //{
        //    _rigidbody.AddForce(transform.forward * Trigger * ForwardForce);
        //}

        _rigidbody.AddRelativeForce(Vector3.forward * IdleForce);


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
