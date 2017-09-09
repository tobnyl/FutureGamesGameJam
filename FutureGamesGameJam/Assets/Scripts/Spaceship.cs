using UnityEngine;
using System.Collections;

public class Spaceship : MonoBehaviour 
{
    #region Fields/Properties

    private Rigidbody _rigidbody;
	
    private Vector2 AxisLeft
    {
        get { return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical") * -1); }
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

        Debug.LogFormat("Axis: {0}", AxisLeft);

        _rigidbody.AddForce(transform.forward * AxisLeft.x);
        _rigidbody.AddForce(transform.right * AxisLeft.y);

    }

    #endregion
    #region Methods



    #endregion
}
