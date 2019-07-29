using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed =  2.0f;
    [SerializeField]
    private int powerupId;// 0 = triple shot 1 = speed boost 2 = shields

    void Start()
    {
        
    }

    void Update()
    {

        // moving the power ups down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);


        if(transform.position.y < -8)
        {
            Destroy(this.gameObject);
        }

        
        
    }

    // collision of Powerup and Player
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collided with :" + other.name);

        if(other.tag == "Player")
        {
            // Access the Player and get component
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                //  plaing the power up sound
                player.PowerUpSound();
                
                if(powerupId == 0)
                {
                    // Enable the Triple shot
                    // reference for the public class for turning triple shot true
                player.TripleShotPowerUpOn();

                }
                                
                else if (powerupId == 1)
                {
                    // Enable the speed boost
                    player.CanSpeedUpOn();

                }

                else if (powerupId == 2)
                {
                    // // Enable the shields
                    player.EnableShields();
                    

                }

            }

            // Destroy the Triple SHot Powerup
            Destroy(this.gameObject);

        }        
        
    }
}
