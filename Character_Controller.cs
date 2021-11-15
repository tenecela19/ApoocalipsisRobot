using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller : MonoBehaviour
{
    #region Singleton
    private static Character_Controller _instance;
    public static Character_Controller Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Character_Controller>();
            }

            return _instance;
        }
    }
    #endregion
    public Rigidbody rg;
    public float speed = 10;
    private bool isGrounded;
    public int Health;
    public float jumpForce = 2.0f;
    public Vector3 jump;

    public bool Isdead;

    // Start is called before the first frame update
    void Start()
    {
        jump = new Vector3(0, 2f, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Walking();
        Jump();
    }

    void Walking()
    {

        float verticalInput = Input.GetAxis("Horizontal");
            rg.velocity = new Vector3(verticalInput * speed * Time.deltaTime, rg.velocity.y, 0);
        Vector3 movement = new Vector3(0.0f, 0, verticalInput);
        if (movement != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }

    }
    private void Update()
    {

       // RotatePlayer();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = true;
        }

    }
    public void TakeLife(int damage)
    {

        if (Health > 0)
        {
            Health -= damage;
        }
        if (Health <= 0)
        {
            Isdead = true;
            Destroy(gameObject);
        }
    }
    void Jump()
    {

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && isGrounded)
        {

            rg.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}
