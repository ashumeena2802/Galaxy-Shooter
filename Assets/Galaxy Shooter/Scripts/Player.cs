using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool _canTripleShot = false;

    public bool _canSpeedUp = false;

    public bool shieldsActive = false;

    [SerializeField]
    private int lives = 3;

    [SerializeField]
    private float _speed = 5.0f;

    [SerializeField]
    private float _canFire = 0.00f;

    [SerializeField]
    private float _fireRate = 0.15f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleShotPrefab;

    [SerializeField]
    private GameObject Explosion_Prefab;

    [SerializeField]
    private GameObject _shieldsGameObject;

    [SerializeField]
    private GameObject[] _engines; 

    [SerializeField]
    private AudioClip _clip;

    private GameManager _gameManager;

    private UIManager _uiManager;

    private SpawnManager _spawnManager;

    private AudioSource _audioSource;

    private int hitcount = 0;
        
    void Start()
    {
        // Set the position of player at game start
        transform.position = new Vector3(0, 0, 0);

        _audioSource = GetComponent<AudioSource>();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
 

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        // Getting the component for the UIManager
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        // Null Check for the UI Manager

        if (_uiManager != null)
        {
            // updating the lives
            _uiManager.UpdateLives(lives);


        }


        if(_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }

        // setting the hitcount to zero
        hitcount = 0;
        
    }

    public void PowerUpSound()
    {
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
    }
         

    void Update()
    {
        // declaring the void for updating 
        Movement();

        


        // taking input from aplce key and mouse0 button and spawnin the laser 
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
           
        }       
        
    }

    private void Shoot()
    {
        // Now we need a cooldown system i.e. Laser should have a timegap pf 0.25 sec
        if (Time.time > _canFire)
        {
            // For  playing the laser sound
            _audioSource.Play();

            // need to impliment the triple shot behavior

            if (_canTripleShot == false)
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.75f, 0), Quaternion.identity);

            }

            else if (_canTripleShot == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);

            }
            _canFire = Time.time + _fireRate;
        }

    }

    public void Damage()
    {

        // Checking if shields is active

        if (shieldsActive == true)
        {
            shieldsActive = false;
            _shieldsGameObject.SetActive(false);


        }
        else 
        {
            // subtract 1 life from player 
            // if life < 1
            // destroy this gameObject

            lives--;

            // increasing the hitcount
            hitcount++;

            // updating the new lives
            _uiManager.UpdateLives(lives);


            if (lives < 1)
            {
            // instantiating the player explosion animation
            Instantiate(Explosion_Prefab, transform.position, Quaternion.identity);
                


                // destroying the player
                Destroy(this.gameObject);

                // set game over to true and show the title screen
                _gameManager.gameOver = true;
                _uiManager.ShowTitleScreen();


            }

            if(hitcount == 1)
            {
                _engines[0].SetActive(true);
            }
            else if (hitcount == 2)
            {
                _engines[1].SetActive(true);
            }

        }
        

    }



    private void Movement()
    {
        // Getting the keys for axis
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Implimenting the Speed Boost
        if(_canSpeedUp == true)
        {
            
            // Making the player move
            transform.Translate(Vector3.right * _speed * 2f * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * 2f * verticalInput * Time.deltaTime);

        }
        else {
           
        transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
        transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);


        }
        
        
        // Bounding the player movement in y
        if(transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if(transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        // Bounding the player movement in x
        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }

    }

    public void EnableShields()
    {
        // activating shields

        shieldsActive = true;
        _shieldsGameObject.SetActive(true);

    }

    public void TripleShotPowerUpOn()
    {

        _canTripleShot = true;
        // starting the couroutine
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        // waiting time
        yield return new WaitForSeconds(12);
        _canTripleShot = false;

    }

    public void CanSpeedUpOn()
    {
        _canSpeedUp = true;

        StartCoroutine(SpeedUpPowerDownRoutine());

    }
    public IEnumerator SpeedUpPowerDownRoutine()
    {
        yield return new WaitForSeconds(10);
        _canSpeedUp = false;
    }

    
}
