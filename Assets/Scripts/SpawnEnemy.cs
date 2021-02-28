using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnEnemy
{
    public class SpawnEnemy : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _spawnPoints;
        [SerializeField] private GameObject _enemy;
        private GameObject _enemyReference;

        // Start is called before the first frame update
        void Awake()
        {
            SpawnEnemyAtRandomPosition();
            Events.SimpleEventSystem.AddListener("move-enemy-to-random-position", MoveEnemyToRandomPosition);
        }

        void MoveEnemyToRandomPosition()
        {
            GameObject randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
            _enemyReference.transform.position = randomSpawnPoint.transform.position;
            _enemyReference.transform.rotation = randomSpawnPoint.transform.rotation;
            RemoveSpawnPointFromList(randomSpawnPoint);
        }

        void SpawnEnemyAtRandomPosition()
        {
            GameObject randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];
            _enemyReference = Instantiate(_enemy, randomSpawnPoint.transform.position, Quaternion.identity);
            _enemyReference.transform.rotation = randomSpawnPoint.transform.rotation;
            _enemyReference.GetComponent<FloatingThing>().Init();
            RemoveSpawnPointFromList(randomSpawnPoint);
        }

        void RemoveSpawnPointFromList(GameObject randomSpawnPoint)
        {
            _spawnPoints.Remove(randomSpawnPoint);
        }
    }
}
