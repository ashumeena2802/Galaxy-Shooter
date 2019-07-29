using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;

    [SerializeField]
    private GameObject Enemy_Explosion_Prefab;

    private UIManager _uiManager;

    [SerializeField]
    private AudioClip _clip;

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();


    }

    void Update()
    {
        Movement();

    }

    private void Movement()
    {
        // For moving the enemy down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //now we need to set the enemy on top when it gets out of bounds
        if(transform.position.y < -6.30)
        {
            // Also while randomizing the x position of the player
            float RandomX = Random.Range(-8.8f, 8.8f);
            transform.position = new Vector3(RandomX, 6.30f, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // if statements for checking the collision was with whom

        if(other.tag == "Laser")
        {
            // destrying the parent object i.e. Triple shot Parent object
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            // destroying the laser and enemy
            Destroy(other.gameObject);

            // For the enemy explosion animation
            Instantiate(Enemy_Explosion_Prefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);

            _uiManager.UpdateScore();
             Destroy(this.gameObject);

        }
        else if (other.tag == "Player")
        {
            // get the player component
            Player player = other.GetComponent<Player>();

            if(player != null)
            {
                // calling the damage system for the player
                player.Damage();

            }

            // For the enemy explosion animation
            Instantiate(Enemy_Explosion_Prefab, transform.position, Quaternion.identity);
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
            Destroy(this.gameObject);
            
        }

    }


}
