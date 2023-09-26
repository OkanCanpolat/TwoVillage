using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool ContinueSpawn = true;
    [SerializeField] private GameObject spawnObjectPrefab;
    [SerializeField] private List<Enemy> currentEnemies;
    [SerializeField] private List<GameObject> spawnPoints;
    [SerializeField] private float timeBetweenChecks;
    [SerializeField] private float spawnMaxRadius;
    private int currentEnemyCount;
    private int spawnPointCount;
    private void Awake()
    {
        currentEnemyCount = currentEnemies.Count;
        spawnPointCount = spawnPoints.Count;
    }

    private void Start()
    {
        foreach (Enemy enemy in currentEnemies)
        {
            enemy.Spawner = this;
        }

        StartCoroutine(Check());
    }

    private IEnumerator Check()
    {
        while (ContinueSpawn)
        {
            yield return new WaitForSeconds(timeBetweenChecks);

            if (currentEnemies.Count <= 0)
            {
                InstantiateNewEnemies();
            }
        }
    }
    public void RemoveFromList(Enemy enemy)
    {
        if (currentEnemies.Contains(enemy))
        {
            currentEnemies.Remove(enemy);
        }
    }
    private void InstantiateNewEnemies()
    {
       
        for (int i = 0; i < currentEnemyCount; i++)
        {
            Vector3 pos = SelectRandomSpawnPoint();
            GameObject o = Instantiate(spawnObjectPrefab, pos, Quaternion.identity);
            Enemy enemey = o.GetComponent<Enemy>();
            enemey.Spawner = this;
            currentEnemies.Add(enemey);
        }
    }

    private Vector3 SelectRandomSpawnPoint()
    {
        int randomPos = Random.Range(0, spawnPointCount);
        Vector3 pos = spawnPoints[randomPos].transform.position;
        return pos;
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.cyan;

        foreach(GameObject go in spawnPoints)
        {
            Gizmos.DrawWireSphere(go.transform.position, 1f);
        }
    }

}
