using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour 
{
    #region Fields/Properties

    public float Force = 1;

    private Rigidbody _rigidbody;

	#endregion
	#region Events
	
	void Awake()
	{
        _rigidbody = GetComponent<Rigidbody>();
	}
	
	void Start() 
	{
        _rigidbody.AddRelativeForce(Vector3.forward * Force, ForceMode.Impulse);
	}

	void Update() 
	{
		
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
	
	
	
	#endregion
}
