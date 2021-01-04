using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy;
    private Transform[] spawnTrans;
    private List<int> spawnPoint = new List<int>();
    private int[] points;
    private const int limitEnemy = 2;
    private const float spawnTime = 30f;
    private float timer = spawnTime;

    // Start is called before the first frame update
    void Start()
    {
        SetSpawnTrans();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!IsFullEnemyNum() && timer > spawnTime)
        {
            SpawnEnemy();
        }
    }

    private void SetSpawnTrans()
    {
        int count = transform.childCount;
        spawnTrans = new Transform[count];

        for(int i = 0; i < count; i++)
        {
            spawnPoint.Add(i);
            spawnTrans[i] = transform.GetChild(i).transform;
        }
        points = spawnPoint.ToArray();
    }

    private bool IsFullEnemyNum()
    {
        int num = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if(num < limitEnemy)
        {
            return false;
        }
        return true;
    }

    private void SpawnEnemy()
    {
        int spawnNum = Random.Range(0, spawnPoint.Count);

        Instantiate(enemy, spawnTrans[spawnPoint[spawnNum]]);
        spawnPoint.RemoveAt(spawnNum);

        if(GameObject.FindGameObjectsWithTag("Enemy").Length == limitEnemy)
        {
            spawnPoint = new List<int>(points);
            timer = 0;
        }
    }
}
