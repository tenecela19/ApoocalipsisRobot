using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons_Controller : MonoBehaviour
{
    public Weapons weapons;
    public float nextDamageEvent = 0.1f;

    public void Start()
    {

    }
    // Start is called before the first frame update
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            if (Time.time >= nextDamageEvent)
            {

                nextDamageEvent = Time.time + weapons.attackDelay;
                collision.gameObject.GetComponent<EnemyAI>().TakeLife(weapons.damage);
                if (Control.Instance.right == true)
                {
                    collision.transform.Translate(Vector2.right * Random.Range(1f,3f));
                }
                if (Control.Instance.right == false)
                {
                    collision.transform.Translate(-Vector2.right * Random.Range(1f, 3f));

                }
            }
        }
        else
        {
            nextDamageEvent = Time.time + weapons.attackDelay;
        }
    }
}

