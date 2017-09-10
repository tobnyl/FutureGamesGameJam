using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    #region Fields/Properties

    [Header("Spaceship")]
    public float MaxLaserChargeTime;
    public float ImmuneTime;

    [Header("Laser")]
    public float LaserMaxDistance;

    [Header("Asteroids")]
    public float DestroyMeshCollidersTime;
    public float DestroyChunksTime;
    public float AsteroidExplodeForce;
    public GameObject ChunksParent;

    [Header("Moon")]
    public float MoonExplodeForce;
    public bool IsMoonDestroyed;

    #endregion
    #region Events

    private static GameManager _instance;
    public static GameManager Instance
    {
        get { return _instance; }
    }
    void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    #endregion
    #region Methods



    #endregion
}
