using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsteroidLogic : MonoBehaviour
{

    [SerializeField]
    Transform asteroidTarget;

    //This value is just visual to help setting up the object in the inspector
    [SerializeField]
    float movementSpeed;
    [SerializeField]
    float movementSpeedDifRange;
    [SerializeField]
    float baseMass;

    [SerializeField]
    float baseHp;
    float deathTime;

    float mass;
    float health;

    [SerializeField]
    GameObject ParticleObject;

    [Header("ModelStuff")]
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    float asteroidMinSize;
    [SerializeField]
    float asteroidMaxSize;

    Transform asteroidModel;

    Vector3 direction;
    Vector3 modelRotDir;

    private FracturedObject _fracturedObject;
    private List<FracturedChunk> _chunkList;

    //This is the valeu thats used by the code to move the asteroid
    float moveSpeed;

    // Use this for initialization
    void Start()
    {
        _fracturedObject = GetComponentInChildren<FracturedObject>();
        _chunkList = GetComponentsInChildren<FracturedChunk>().ToList();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            //Destroy(gameObject);
            foreach (var chunk in _chunkList)
            {
                chunk.gameObject.layer = LayerMask.NameToLayer("Chunk");
            }

            _fracturedObject.Explode(transform.position, 100f);
        }
    }

    public void SetUpAsteroid(Transform moon)
    {
        asteroidModel = transform.GetChild(0);
        //asteroidModel.GetComponent<AsteroidModelScript>().SetUpMesh();
        modelRotDir = transform.rotation.eulerAngles * 3;
        asteroidTarget = moon;
        moveSpeed = movementSpeed;// * (-0.15f + Random.Range(0, 0.3f));

        direction = -(transform.position - asteroidTarget.position).normalized;
        transform.rotation = Quaternion.Euler(0, 0, 0);

        deathTime = asteroidModel.GetComponent<TrailRenderer>().time;
        asteroidModel.GetComponent<AsteroidModelScript>().SetUpMesh(modelRotDir, rotationSpeed);
        SetUpSize();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * movementSpeed);

    }

    void SetUpSize()
    {

        //float _size = Random.Range(asteroidMinSize, asteroidMaxSize);

        float size_ = Random.Range(0, 1f);

        //size

        float sizeDifference = (asteroidMaxSize - asteroidMinSize) * size_;
        float _vecSize = asteroidMinSize + sizeDifference;

        print(_vecSize);

        asteroidModel.localScale = new Vector3(_vecSize, _vecSize, _vecSize);

        //hp

        health = baseHp * (size_ + 1);

        mass = baseMass * (size_ * 3);

        moveSpeed = movementSpeed - (size_ * 0.3f);

    }



    public void Collided(string typeHit)
    {
        //collision code here

        if (typeHit == "Moon")
        {
            //Play particle effect
            GameObject g = Instantiate(ParticleObject, asteroidModel.transform.position, Quaternion.identity) as GameObject;
            g.GetComponent<ParticleEffectObjScript>().SetScale(transform.localScale);
            asteroidModel.GetComponent<Collider>().enabled = false;
            asteroidModel.GetComponent<Renderer>().enabled = false;

            StartCoroutine("DestroySelf");
        }

        else if (typeHit == "Player")
        {

        }
    }

    IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(1f);
        movementSpeed = 0;
        yield return new WaitForSeconds(deathTime - 0.25f);
        Destroy(gameObject);
    }
}
