using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astroid : MonoBehaviour
{

    private float _rotateSpeed = 0.5f;

    [SerializeField]
    private GameObject _explosion;
    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager == null) {
            Debug.Log("Spawn Manager was found null in android.cs");
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0,0,_rotateSpeed, Space.Self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Laser") {
            Destroy(collision.gameObject);
            GameObject explosion = Instantiate(_explosion, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            Destroy(this.gameObject, 0.15f);
            Destroy(explosion, 2f);
            _spawnManager.StartSpawning();
        }
        
    }
}
