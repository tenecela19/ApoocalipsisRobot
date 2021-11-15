using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
   public bool HasWeapon;
    public GameObject player;
    #region Singleton
    private static GameController _instance;

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameController>();
            }

            return _instance;
        }
    }


    #endregion
    public bool IsPlayerDead()
    {
        if (player.activeSelf == false)
        {
            return true;
        }
        else return false;
    }
    public HordeSystem _hordeSystem;

    // Start is called before the first frame update
    void Start()
    {
        _hordeSystem.enabled = false;
    }
  
    // Update is called once per frame
    void Update()
    {
        if (HasWeapon)
        {
            _hordeSystem.enabled = true;
        }
        else
        {
            _hordeSystem.enabled = false;
        }
        if (IsPlayerDead())
        {
            StartCoroutine(InstantiatePlayer());
        }
        else {
            StopAllCoroutines();
      
        };

    }
    IEnumerator InstantiatePlayer()
    {
        yield return new WaitForSeconds(2f);
        Control.Instance.gameObject.SetActive(true);
        Control.Instance.transform.position = _hordeSystem.TotalSpawnPoints[Random.Range(0,_hordeSystem.TotalSpawnPoints.Length)].position;
        Control.Instance.Health = 10;
        yield break;
    }
}
