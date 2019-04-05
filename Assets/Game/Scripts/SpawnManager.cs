using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemeyShipPrefab;
    [SerializeField]
    private GameObject[] powerups;
    private GameManager _gameManager;
    private bool shouldStartSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void StartSpawning() 
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    //spawn the enmey every 5 seconds
    public IEnumerator SpawnEnemyRoutine()
    {
        while(_gameManager.gameOver == false) 
        {
            //spawn at top
            float randomX = Random.Range(-7.0f, 7.0f);
            Vector3 randomPositionAtTop = new Vector3(randomX, 7.0f, 0);
            Instantiate(_enemeyShipPrefab, randomPositionAtTop, Quaternion.identity);

            //wait
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator PowerupSpawnRoutine()
    {
        while (_gameManager.gameOver == false) 
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-7.0f, 7.0f), 7, 0), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
        }
        
    }
}
