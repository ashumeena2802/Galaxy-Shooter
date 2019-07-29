using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;
    
   [SerializeField]
   private GameObject[] powerups;

    private GameManager _gameManager;

    void Start()
    {
        // started the coroutine for enemy spawning
        StartCoroutine(EnemySpawnRoutine());
        // started the coroutine for Powerup spawning
        StartCoroutine(PowerupSpawnRoutine());

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    // starting the spawn routine as the while loop has ended
    public void StartSpawnRoutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    // enemy spawnin loop system
    IEnumerator EnemySpawnRoutine()
    {
        // Loop for spawning enemy
        while (_gameManager.gameOver == false)
        {
            Instantiate(enemyShipPrefab, new Vector3 (Random.Range(-8, 8), 6.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(8f);
        }
    }
    
    // Powerups Spawning system
    IEnumerator PowerupSpawnRoutine()
    {
        // Loop for spawning Powerup
        while (_gameManager.gameOver == false)
        {
            // random power up id for spawnin random powerup
            int randomPowerup = Random.Range(0,3);
            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-8, 8), 6.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(7);
        }
    }
}
