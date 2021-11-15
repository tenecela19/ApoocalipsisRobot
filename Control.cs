using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    [SerializeField] private LayerMask platformlayerMask;
    [System.Serializable]
    public class Boundary
    {
        public float Xmin;
        public float Xmax;
        public float Ymin;
        public float Ymax;

    }

    #region Singleton
    private static Control _instance;
    public static Control Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<Control>();
            }

            return _instance;
        }
    }
    #endregion
    public float moveSpeed;
    public float jumpHeight;

    private float jumpRechargeTime = 0f;

    public int Health = 10;
    public bool right;
    Rigidbody2D rg;
    public Boundary boundary;
    private CapsuleCollider2D Collider2D;
    public Animator anim;
    public AudioSource pasosaudio;
    public AudioSource ataque;
    private void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        Collider2D = GetComponent<CapsuleCollider2D>();
    }
    void FixedUpdate()
    {
        PlayerMov();
    }
    private void Update()
    {
    }
    public void PlayerMov()
    {
        if (Input.GetMouseButton(0))
        {
            if (GameController.Instance.HasWeapon)
            {
                if (!ataque.isPlaying)
                {
                    ataque.Play();
                }
                anim.SetBool("atack", true);

            }

        }
        else
        {
            if (!ataque.isPlaying)
            {
                ataque.Stop();
            }
            anim.SetBool("atack", false);

        }
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W)) && IsGrounded())
        {

            if (Time.time >= jumpRechargeTime)
            {
                rg.velocity = new Vector2(rg.velocity.x, jumpHeight);

                jumpRechargeTime = Time.time + 1f; // One second until the next jump
            }
        }
        if(anim.GetBool("atack") == false)
        {
            if (Input.GetKey(KeyCode.D))
            {
                if (!pasosaudio.isPlaying)
                {
                    pasosaudio.Play();
                }
                rg.velocity = new Vector2(moveSpeed, rg.velocity.y);
                anim.SetBool("walk", true);
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                right = true;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                if (!pasosaudio.isPlaying)
                {
                    pasosaudio.Play();
                }
                rg.velocity = new Vector2(-moveSpeed, rg.velocity.y);
                anim.SetBool("walk", true);
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                right = false;
            }
            else
            {

                anim.SetBool("walk", false);
            }
            rg.position = new Vector3(Mathf.Clamp(rg.position.x, boundary.Xmin, boundary.Xmax), Mathf.Clamp(rg.position.y, boundary.Ymin, boundary.Ymax), 0f);

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
            gameObject.SetActive(false);
        }
    }
   


    bool IsGrounded()
    {
        float extraHeightText = 0.1f;
        RaycastHit2D raycast =  Physics2D.Raycast(Collider2D.bounds.center, Vector2.down, Collider2D.bounds.extents.y + extraHeightText, platformlayerMask);
        Debug.DrawRay(Collider2D.bounds.center, Vector2.down * (Collider2D.bounds.extents.y + extraHeightText));
        return raycast.collider != null;
    }
}
