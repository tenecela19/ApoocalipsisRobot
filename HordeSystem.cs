using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Hordes
{
    public int enem_count;
    public Transform enemy;
    public int rate;
    public Transform[] SpawnPoint;
}
public class HordeSystem : MonoBehaviour
{
    #region Singleton
    private static HordeSystem _instance;

    public static HordeSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<HordeSystem>();
            }

            return _instance;
        }
    }


    #endregion
    public enum SpawnState { SPAWING, WAITING, COUNTING };
    public Hordes[] hordes;

    [Header("Enemy Horde Control")]
       private int nextHorde = 0;
       public int hordedelay;  
       public float timeBetweenHordes = 5f;
       public float hordesCountdown;
    [Header("Enemy Control")]


    
    
        private float searchCountdown = 1f;
        SpawnState state = SpawnState.COUNTING;

    public bool GameOver;

        void Start()
        {
            hordesCountdown = timeBetweenHordes;

        }

    void Update()
        {
        if(state == SpawnState.WAITING)
        {
            if (!EnemyIsAlive())
            {
                HordeCompleted();
            }
            else
            {
                return;
            }
        }
            if(hordesCountdown <= 0)
            {
                if(state != SpawnState.SPAWING)
            {
                StartCoroutine(SpawnEnemies(hordes[nextHorde]));
            }

            }
        else
        {
            hordesCountdown -= Time.deltaTime;
        }
    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }
    IEnumerator SpawnEnemies(Hordes _horde)
    {
        state = SpawnState.SPAWING;
        for (int i = 0; i < _horde.enem_count; i++)
        {
            yield return new WaitForSeconds(Random.Range(1,3));
            SpawnEnemy(_horde.enemy);
            yield return new WaitForSeconds(1 / hordedelay);
        }
        state = SpawnState.WAITING;
        yield break;
    }
    void SpawnEnemy(Transform enemy)
    {
        Transform randomSpawn = hordes[nextHorde].SpawnPoint[RandomRange()];
        Instantiate(enemy, randomSpawn.position, Quaternion.identity) ;
    }
    void HordeCompleted()
    {
        state = SpawnState.COUNTING;
        hordesCountdown = timeBetweenHordes;
        nextHorde++;
    }

    int RandomRange()
    {
        return Random.Range(0, hordes[nextHorde].SpawnPoint.Length);
    }
}

