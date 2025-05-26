using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WaveSpawnerManager : MonoBehaviour
{
    //a list of all the spawn points
    public List<GameObject> SpawnPointsList;

    
    
    //how many eneimes to spawn each wave
    [SerializeField] private int _totalEnemiesToSpawn;

    //the total amount of enemies in the current wave
    [SerializeField] private int _currentAmountOfEnemies;

    [Header("Enemy types to spawn")]

    //a list of enemy types
    [SerializeField] private List<GameObject> _enemyTypes;


    void OnEnable()
    {
        EnemyBehavior.onEnemyDeath += () => _currentAmountOfEnemies--;
        EnemyBehavior.onEnemyDeathFromDespawner += () => _currentAmountOfEnemies--;

       
    }

    void OnDisable()
    {
        EnemyBehavior.onEnemyDeath -= () => _currentAmountOfEnemies--;
        EnemyBehavior.onEnemyDeathFromDespawner -= () => _currentAmountOfEnemies--;

        
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    private void OnDestroy()
    {
        EnemyBehavior.onEnemyDeath -= () => _currentAmountOfEnemies--;
        EnemyBehavior.onEnemyDeathFromDespawner -= () => _currentAmountOfEnemies--;

        
    }


    void Awake()
    {
        FindSpawnpoints();
    }

    void Start()
    {

        

        StartCoroutine(StartWave());
       
    }





    public void FindSpawnpoints()
    {
        //find all the game objects with the tag "Spawnpoint" and add them to the list
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("Spawnpoint");
        foreach (GameObject spawnPoint in spawnPoints)
        {
            SpawnPointsList.Add(spawnPoint);
        }
    }


   
    public IEnumerator StartWave()
    {
        //set the wave to one
        GameManager.instance.currentWave++;

        //how many type a eneimes to spawn each wave
        _totalEnemiesToSpawn = 2 + GameManager.instance.currentWave;

        //set the speed of each enemy depending on the wave
        for (int i = 0; i < _enemyTypes.Count; i++)
        {
            _enemyTypes[i].GetComponent<EnemyBehavior>().moveSpeed = ((GameManager.instance.currentWave * 3) * 3);
        }

        //how many total enemies to spawn 
            _currentAmountOfEnemies = _totalEnemiesToSpawn;

        //iterate through the list of how many enemies to spawn each wave
        for (int i = 0; i < _currentAmountOfEnemies; i++)
        {
            //randomly select an enemy from the enemy list
            int enemyToPick = UnityEngine.Random.Range(0, _enemyTypes.Count);

            //spawn the enemy at the spawn point
            int randomSpawnPoint = UnityEngine.Random.Range(0, SpawnPointsList.Count);
            Instantiate(_enemyTypes[enemyToPick], SpawnPointsList[randomSpawnPoint].transform.position, Quaternion.identity);
            
        }

        //wait until the eniemies in the wave are defeated
        yield return new WaitUntil(() => _currentAmountOfEnemies == 0);

        Debug.Log("All enemies are gone!");

        yield return new WaitForSeconds(3f);

        StartCoroutine(StartWave());

        yield break;


    }
    
   
}
