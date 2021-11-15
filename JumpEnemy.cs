using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JumpEnemy : MonoBehaviour
{
    public Rigidbody rg;
    public NavMeshAgent Eagent;
    private bool hold;
    bool isGround = false;
    public int Jjumpforce;
    private void OnCollisionEnter(Collision collision)
    {



    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacule"))
        {

            Eagent.enabled = false;
            rg.isKinematic = false;
        }

    }
    private void OnCollisionExit(Collision collision)
    {


    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Eagent.enabled == false)
        {
            Debug.Log("stopperd");
            rg.AddForce(0, Jjumpforce, 0);
            if (hold)
                StartCoroutine(Hold());

        }
        CheckGround();
    }
    public IEnumerator Hold()
    {
        hold = false;
        yield return new WaitForSeconds(0.2f);
        isGround = false;
        hold = true;
    }
    private void CheckGround()
    {
        Vector3 origin = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y * .5f), transform.position.z);
        Vector3 direction = transform.TransformDirection(Vector3.down);
        Vector3 Xdirecion = transform.TransformDirection(Vector3.right);
        float distance = .75f;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            Debug.DrawRay(origin, direction * distance, Color.red);
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
}
