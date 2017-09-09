using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour 
{
    #region Fields/Properties

    public float BaseForce = 1;
    public float Force = 1;
    public float DamagePoints = 1;

    private Rigidbody _rigidbody;
    private float _force;
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

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.layer == LayerMask.NameToLayer("Moon"))
        {
            Destroy(gameObject);
        }
    }
	
	#endregion
	#region Methods
	
	public void Initialize(float timeElapsed)
    {
        Force = BaseForce + Force - Mathf.Lerp(0, Force, timeElapsed / GameManager.Instance.MaxLaserChargeTime);
        //Debug.LogFormat("Force: {0}", Force);
    }
	
	#endregion
}
