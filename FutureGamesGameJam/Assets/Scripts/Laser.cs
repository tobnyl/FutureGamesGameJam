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
	
	#endregion
	#region Methods
	
	
	
	#endregion
}
