using UnityEngine;
using System.Collections;

public class Moon : MonoBehaviour 
{
    #region Fields/Properties

    public int StartHealth = 100;
    public int DamageAtCollision = 10;

    [SerializeField, ReadOnly]
    private int _currentHealth;

    #endregion
    #region Events

    void Awake()
	{
	    	
	}
	
	void Start() 
	{
        _currentHealth = StartHealth;
	}

	void Update() 
	{		
	}

    //private void OnTriggerEnter(Collider c)
    //{
    //    if (c.gameObject.layer == Layers.Asteroid.Index)
    //    {
            
            
    //    }
    //}
	
	#endregion
	#region Methods
	
	public void TakeDamage()
    {
        Debug.Log("ASteroid hit moon");
        _currentHealth -= DamageAtCollision;

        if (_currentHealth < 0)
        {
            Destroy(gameObject);
        }
    }
	
	#endregion
}
