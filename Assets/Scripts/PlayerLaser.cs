using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
    [SerializeField]
    private float _speed =5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, 1 , 0) * _speed * Time.deltaTime);
        //destroy laser after it reaches 5.9f on the y

        if (transform.position.y >= 5.9f) {
            
            if (this.transform.parent != null) {
                Destroy(transform.parent.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

}
