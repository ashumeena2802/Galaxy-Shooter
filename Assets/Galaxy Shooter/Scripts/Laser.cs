using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;

    void Start()
    {
        
    }

    void Update()
    {
        // Moving the laser upwards
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

        // destroying the laser after its not on the screen 
        if (transform.position.y > 5f)
        {
            // destrying the parent object i.e. Triple shot Parent object
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
            
        }
        
    }
}
