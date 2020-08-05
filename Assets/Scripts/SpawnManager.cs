using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    private float _randomX_max = 9f;
    private float _randomX_min = -9f;
    [SerializeField]
    private float _enemySpawnTime = 2;
    [SerializeField]
    private GameObject _container;
    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(_enemySpawnTime));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator spawnEnemy(float spawnTime) {
        Debug.Log("Entered Coroutine");
        while (_stopSpawning == false) {
            GameObject newEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(_randomX_min, _randomX_max), 6f, 0), Quaternion.identity);
            newEnemy.transform.parent = _container.transform;
            yield return new WaitForSeconds(spawnTime);

        }
    }

    public void onPlayerDeath() {
        _stopSpawning = true;
    }
}
