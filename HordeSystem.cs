using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Hordes
{
    public int enem_count;
    public Transform enemy;
    public int rate;
    public GameObject WeaponWave;
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
    public Transform[] TotalSpawnPoints;
    [Header("Enemy Control")] 
        private float searchCountdown = 1f;
        SpawnState state = SpawnState.COUNTING;
    public ChangeScene scene;
    bool HasWin()
    {
        if (nextHorde >= hordes.Length)
        {
            return true;
        }
        else return false;
    }


        void Start()
        {
        Debug.Log(nextHorde);
        RandomRange();
        hordesCountdown = timeBetweenHordes;
        }

    void Update()
        {

        if (!HasWin())
        {
            if (state == SpawnState.WAITING)
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
            if (hordesCountdown <= 0 && !HasWin())
            {
                if (state != SpawnState.SPAWING)
                {
                    StartCoroutine(SpawnEnemies(hordes[nextHorde]));

                    for (int i = 0; i < hordes[nextHorde].SpawnPoint.Length; i++)
                        if (hordes[nextHorde].SpawnPoint[i] != null)
                        {
                            hordes[nextHorde].SpawnPoint[i].GetChild(0).gameObject.SetActive(true);
                        }
                    {
                    }

                }

            }
            else
            {
                hordesCountdown -= Time.deltaTime;
            }
        }
        else
        {
            scene.Scene("TransicionFinal");
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
        Transform randomSpawn = hordes[nextHorde].SpawnPoint[Random.Range(0,hordes[nextHorde].SpawnPoint.Length)];
        Instantiate(enemy, randomSpawn.position, Quaternion.identity) ;

    }
    void HordeCompleted()
    {

        nextHorde++;
        
        if (!HasWin())
        {
       
            state = SpawnState.COUNTING;
            hordesCountdown = timeBetweenHordes;

            GameObject weapon = Instantiate(hordes[nextHorde].WeaponWave);
            weapon.transform.position = new Vector3(0, 10, 0);
            RandomRange(); 
        }
        foreach (GameObject portal in GameObject.FindGameObjectsWithTag("Portales"))
        {
            portal.SetActive(false);
        }
    }

    void RandomRange()
    {
        for (int i = 0; i < hordes[nextHorde].SpawnPoint.Length; i++)
        {
            hordes[nextHorde].SpawnPoint[i] = TotalSpawnPoints[Random.Range(0, TotalSpawnPoints.Length)];
            hordes[nextHorde].SpawnPoint[i].GetChild(0).gameObject.SetActive(true);
        }


    }
}

