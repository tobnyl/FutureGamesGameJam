using UnityEngine;
using System.Collections;

public class Moon : MonoBehaviour 
{
    #region Fields/Properties

    // TODO: GameManager.Instance.MoonExplodeForce


    #endregion
    #region Events

    void Awake()
	{
	    	
	}
	
	void Start() 
	{
		
	}

	void Update() 
	{		
	}

    private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.layer == Layers.Asteroid.Index)
        {
            Debug.Log("ASteroid hit moon");
        }
    }
	
	#endregion
	#region Methods
	
	
	
	#endregion
}
