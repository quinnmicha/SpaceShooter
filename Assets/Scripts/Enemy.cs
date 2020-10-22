using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Player _player;

    [SerializeField]
    private GameObject[] _powerupPrefab;
    [SerializeField]
    private float _speed = -2.5f;//negative because we want down on the y axis
    private float _randomNumber;
    private int _randomPowerup;
    private float _powerUpChance = 0.2f;//This will be the "percentage" that a powerup will spawn

    private Animator _death_Anim;
    

    // Start is called before the first frame update
    void Start()
    {
       //Finds and caches player object once
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null) {
            Debug.Log("Player is Null");
        }
        _death_Anim = gameObject.GetComponent<Animator>();
        if (_death_Anim == null) {
            Debug.Log("Enemy Death Animation is Null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 1, 0) * _speed * Time.deltaTime);

        if (transform.position.y <= -6f) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser"){
            Destroy(other.gameObject);
            if (_player != null) {
                _player.AddScore();
            }
            PowerupChance();
            _death_Anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(gameObject, 2f);
        }
        else if (other.tag == "Player") {
            //other.transform gets the root of the gameObject .GetComponent<> gets the added component on the gameObject, 
                //in this case the gameObject has a Player script so we can access it through the code bellow to then access the public method within that script
            //other.transform.GetComponent<Player>().Damage();

            //To null check, you can set the component to a variable and then check if it is null
            Player player = other.transform.GetComponent<Player>();
            if (player != null) {
                player.Damage();
            }
            _death_Anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(gameObject);
        }
        
    }

    private void PowerupChance() {
        _randomNumber = Random.Range(0f, 1f);
        _randomPowerup = Random.Range(0, 3);
        if (_randomNumber < _powerUpChance) {
            Instantiate(_powerupPrefab[_randomPowerup], transform.position, Quaternion.identity);
        }
    }

    
}
