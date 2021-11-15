using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyController : MonoBehaviour
{

    #region Singleton
    private static EnemyController _instance;
    public static EnemyController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<EnemyController>();
            }

            return _instance;
        }
    }


    #endregion

    public NavMeshAgent Eagent;
    Transform Player;

    [Header("Enemy Settings")]
    public float nextDamageEvent;
    public float attackDelay;
    public  int damage = 1;
    public  int health = 10;
    bool isAlive = true;
    bool isGround = false;
    public Rigidbody rg;
    private bool hold;


    // Start is called before the first frame update
    void Start() {

        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
   public IEnumerator Hold()
    {
        hold = false;
        yield return new WaitForSeconds(0.1f);
        isGround = false;
        hold = true;
    }
    private void Update()
    {


        CheckGround();
        if (!Character_Controller.Instance.Isdead)
        {
            if (isAlive){

            if (isGround && Eagent.enabled)
            {

                    if(Eagent.isStopped == false)
                    {
                        Eagent.SetDestination(Player.position);

                    }

                }

            }

        }

    }

    private void FixedUpdate()
    {
        
    }
    public void TakeLife(int damage )
    {

        if (health > 0)
        {
            health -= damage;
        }
        if (health <= 0)
        {
            isAlive = false;
            Destroy(gameObject);
        }
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player");
            Eagent.isStopped = true;
        }
    }
    private void CheckGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
        Vector3 direction = transform.TransformDirection(Vector3.down);
        float distance = .75f;

        if (Physics2D.Raycast(origin, direction))
        {
            Debug.DrawRay(origin, direction * distance, Color.red);
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Eagent.isStopped = false;
        }
        if (collision.gameObject.CompareTag("Ground") && !isGround)
        {
            Eagent.enabled = true;
            rg.isKinematic = true;
        }

    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(HordeSystem.Instance.GameOver == false)
            {
                if (Time.time >= nextDamageEvent)
                {
                    nextDamageEvent = Time.time + attackDelay;
                    Character_Controller.Instance.TakeLife(damage);
                }
            }
            else
            {
                nextDamageEvent = Time.time + attackDelay;
            }

        }

    }
}
