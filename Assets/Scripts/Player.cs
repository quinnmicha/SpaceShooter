using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Prefabs
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _trippleShotPrefab;
    private UIManager _uIManager;

    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _fireRate = 0.15f;
    [SerializeField]
    private int _lives = 3;
    private float _nextFire = 0f;
    private SpawnManager _spawnManager;
    [SerializeField]
    private int _score;

    //PowerUps
    private bool _trippleShot = false;
    private bool _speedBoost = false;
    private float _speedIncreaseBy = 5f;
    private bool _shield = false;
    private float _powerupTime = 5f;
    private float _trippleShotDeactivateTime;
    private float _speedDeactivateTime;
    private Renderer shieldRend;

    // Start is called before the first frame update
    void Start()
    {
        _uIManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        //Take the current position and make it the starting position ( 0, 0, 0)
        transform.position = new Vector3(0f, 0f, 0f);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();//How to grab the gameobject to use the script and null check
        if (_spawnManager == null) {
            Debug.LogError("SpawnManager is null");//Errors out and logs error
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _nextFire)
        {
            if (_trippleShot && Time.time > _trippleShotDeactivateTime) {
                _trippleShot = false;
            }
            FireLaser();
        }

        if (_speedBoost && Time.time > _speedDeactivateTime) {
            _speed -= _speedIncreaseBy;
            _speedBoost = false;
        }

    }
        

    void CalculateMovement() {
        float horizontalInput = Input.GetAxis("Horizontal"); //This pulls back a 0, -1, or 1 
        float verticalInput = Input.GetAxis("Vertical");//The strings 'Vertical' and 'Horizontal are found within the input manager in the project settings

        //for every frame, move the object 1 unit to the right 0 units on the y axis and 0 units on the z axis
        //transform.Translate(new Vector3(1,0,0));

        //Multiplying the vector3 by Time.deltaTime allows for movement based on realworld seconds instead of frames
        //transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);

        //You can also multiply by a variable to dynamically set the speed of the translate
        //transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime);



        //You can also multiply by a variable to react to user Input
        //multiplying the vector3 moving right by one by the horizontal input will move it right or left depending on the user input
        //transform.Translate(new Vector3(1, 0, 0) * horizontalInput * speed * Time.deltaTime);

        //Doing the same thing for vertical movement is similar but the x axis becomes 0 and the y axis becomes 1
        //transform.Translate(new Vector3(0, 1, 0) * verticalInput * speed * Time.deltaTime);


        //The most optimal way to do this is to merge both the above codes together
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * _speed * Time.deltaTime);

        //This above line takes the transform part of the object this script is on, translate(manipulate) it with a new vector 3 
        //on the x axis we will input the horizontal input data, on the y axis we will input the vertical input data, then 0 for the z axis
        //multiply that vector by the speed, then multiply that by Time.deltaTime for real world meters per second


        //This if else restricts the player from moving past -5 to 0 on the y axis
        if (transform.position.y >= 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y <= -5f)
        {
            transform.position = new Vector3(transform.position.x, -5f, 0);
        }

        //this if else restricts the player from moving pas -10 to 10 on the x axis
        if (transform.position.x >= 10)
        {
            transform.position = new Vector3(-10f, transform.position.y, 0);
        }
        else if (transform.position.x <= -10)
        {
            transform.position = new Vector3(10f, transform.position.y, 0);
        }
    }

    void FireLaser() {

        //Quarternion.identity just means to keep the gameobject's rotation, the same as the player
        //this line bellow creates the gameobject and places it at the location of the player with the same rotation
        if (_trippleShot)
        {
            Instantiate(_trippleShotPrefab, transform.position + new Vector3(0, 1.06f, 0), Quaternion.identity);
            _nextFire = Time.time + _fireRate;
        }
        else {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.06f, 0), Quaternion.identity);
            _nextFire = Time.time + _fireRate;
        }
        
        
    }

    public void Damage()
    {
        if (_shield)
        {
            _shield = false;
            if (shieldRend != null) {
                shieldRend.enabled = false;
            }
        }
        else {
            _lives -= 1;
            if (_lives < 0)
            {
                _spawnManager.transform.GetComponent<SpawnManager>().onPlayerDeath();

                Destroy(this.gameObject);
            }
        }
        
    }

    public void addScore(int points) {
        _score += points;
        _uIManager.setScore(_score);
    }

    public void activateTrippleShot() {
        _trippleShot = true;
        _trippleShotDeactivateTime = Time.time + _powerupTime;
    }

    public void activateSpeed() {
        _speedBoost = true;
        _speedDeactivateTime = Time.time + _powerupTime;
        _speed += _speedIncreaseBy;
    }

    public void activateShield() {
        _shield = true;
        shieldRend = transform.GetChild(0).GetComponent<Renderer>();
        if (shieldRend != null) {
            shieldRend.enabled = true;
        }
    }
}
