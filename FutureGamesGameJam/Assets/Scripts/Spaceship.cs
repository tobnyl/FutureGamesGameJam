using UnityEngine;
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
    public float RecoilForce = 1;
    public float RecoilTime = 1;

    [Header("Torque")]
    public bool InvertedPitch = true;
    public float PitchTorque = 1;
    public float YawTorque = 1;
    public float RollTorque = 1;

    [Header("Max Velocity")]
    public float MaxVelocity = 20;
    public float MaxAngularVelocity = 10f;

    //public float MaxChargeTime = 1;    

    private Rigidbody _rigidbody;
    private PlayerHealth _playerHealth;
    //private bool _isFiring;
    private float _sqrMaxVelocity;
    private float _sqrMaxAngularVelocity;
    private float _force;
    private float _fireButtonDownTimer;
    private bool _isFired;
    private bool _isRecoil;


    private Vector2 AxisLeft
    {
        get { return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); }
    }

    private Vector2 AxisRight
    {
        get { return new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2")); }
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
        _sqrMaxAngularVelocity = MaxAngularVelocity * MaxAngularVelocity;

        _playerHealth = GetComponent<PlayerHealth>();
    }
	
	void Start() 
	{
		
	}

	void Update() 
	{
        //Debug.LogFormat("Avel: {0}", _rigidbody.angularVelocity.magnitude);

        if (!_isFired)
        {
            if (FireButton)
            {
                _fireButtonDownTimer += Time.deltaTime;

                if (_fireButtonDownTimer >= GameManager.Instance.MaxLaserChargeTime)
                {
                    StartCoroutine(RecoilCoroutine());

                    //Debug.Log("Time's up");
                    InstantiateLaser();
                }
            }
            else if (FireButtonUp)
            {
                //Debug.Log("Fire button up");
                InstantiateLaser();
                _isFired = false;
            }
        }
        else if (_isFired && FireButtonUp)
        {
            //Debug.Log("Is Fired false");
            _isFired = false;
        }
    }

    void FixedUpdate()
    {
        if (!_isRecoil)
        {
            if (_rigidbody.velocity.sqrMagnitude > _sqrMaxVelocity)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * MaxVelocity;
            }

            if (_rigidbody.angularVelocity.sqrMagnitude > _sqrMaxAngularVelocity)
            {
                _rigidbody.angularVelocity = _rigidbody.angularVelocity.normalized * MaxAngularVelocity;
            }

            _force = IdleForce;

            if (Trigger > 0)
            {
                _force *= BoostMultiplier;
            }

            _rigidbody.AddRelativeForce(Vector3.forward * _force);


            _rigidbody.AddRelativeTorque(Vector3.right * (InvertedPitch ? AxisLeft.y : -AxisLeft.y) * PitchTorque);
            _rigidbody.AddRelativeTorque(Vector3.up * AxisLeft.x * YawTorque);
            _rigidbody.AddTorque(transform.forward * (-AxisRight.x) * RollTorque);
        }
    }

    private void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("Moon"))
        {
            Debug.Log("Yes");
            _playerHealth.TakeDamage(1);
        }
    }

    #endregion
    #region Methods

    private void InstantiateLaser()
    {
        var go = Instantiate(LaserPrefab, FireSpawnPoint.position, FireSpawnPoint.rotation);
        var laser = go.GetComponent<Laser>();
        laser.Initialize(_fireButtonDownTimer);


        _isFired = true;
        _fireButtonDownTimer = 0;
    }

    #endregion
    #region Coroutines

    private IEnumerator RecoilCoroutine()
    {
        _isRecoil = true;

        _rigidbody.AddRelativeForce(-Vector3.forward * RecoilForce, ForceMode.Impulse);

        yield return new WaitForSeconds(RecoilTime);

        _isRecoil = false;
    }

    #endregion
}
