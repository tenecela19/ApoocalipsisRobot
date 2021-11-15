using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons_Controller : MonoBehaviour
{
    #region Singleton
    private static Weapons_Controller _instance;

    public static Weapons_Controller Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Weapons_Controller>();
            }

            return _instance;
        }
    }


    #endregion
    public Weapons weapons;
    public float nextDamageEvent = 0.1f;
    public MeshFilter _meshfilter;
    public MeshRenderer _meshrender;

    // Start is called before the first frame update
    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Quitar");
            if (Time.time >= nextDamageEvent)
            {
                if (Input.GetMouseButton(0))
                {
                    if (Control.Instance.right == true)
                    {
                        collision.transform.Translate(Vector2.right * Random.Range(1f, weapons.retroceso));
                    }
                    if (Control.Instance.right == false)
                    {
                        collision.transform.Translate(-Vector2.right * Random.Range(1f, weapons.retroceso));
                    }
                    nextDamageEvent = Time.time + weapons.attackDelay;
                    collision.gameObject.GetComponent<EnemyAI>().TakeLife(weapons.damage);
                }


            }
        }

        else
        {
            nextDamageEvent = Time.time + weapons.attackDelay;
        }
    }
    
}

