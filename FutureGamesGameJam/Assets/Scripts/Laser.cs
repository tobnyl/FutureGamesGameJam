using UnityEngine;
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

    void OnTriggerEnter(Collider c)
    {
        var otherLayer = c.gameObject.layer;

        if (otherLayer == LayerMask.NameToLayer("Moon"))
        {
            Destroy(gameObject);
        }
        else if (otherLayer == LayerMask.NameToLayer("Asteroid"))
        {
            Debug.Log("Hit asteroid");
            var asteroid = c.gameObject.GetComponent<AsteroidLogic>() ?? c.gameObject.GetComponentInParent<AsteroidLogic>(); ;
            asteroid.TakeDamage(10);
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
