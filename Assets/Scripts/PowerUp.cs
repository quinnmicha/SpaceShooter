using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private float _speed = -3f;
    [SerializeField]
    private int _powerupID;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            Player player = collision.transform.GetComponent<Player>();
            if (player != null) {

                switch (_powerupID) {
                    case 0:
                        player.activateTrippleShot();
                        break;
                    case 1:
                        player.activateSpeed();
                        break;
                    case 2:
                        player.activateShield();
                        break;
                }
            }
            Destroy(gameObject);
        }
    }

    private void Movement() {
        transform.Translate(new Vector3(0, 1f, 0) * _speed * Time.deltaTime);
        if (transform.position.y <= -6.84) {
            Destroy(gameObject);
        }
    }
}
