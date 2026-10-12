using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShadowMove : MonoBehaviour
{
    public PlayerMovment player;
    public Rigidbody2D Shadowrb;
    public Collider2D Shadowcollider;
    public Transform ShadowLocation;
    bool isAttach;
    [SerializeField] LayerMask ground;
    [SerializeField] Vector2 boxSize;
    [SerializeField] float Distance;

    [SerializeField] float horizontal;
    [SerializeField] float moveSpeed = 9.99f;
    [SerializeField] float jumpPower = 10f;
    bool facingRight;

    void Start()
    {
        Physics2D.IgnoreCollision(player.PlayerCollider, Shadowcollider);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            ShadowLocation.position = player.playerLocation.position;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || ShadowLocation.position == player.playerLocation.position)
        {
            isAttach = true;
        }
        if (Vector2.Distance(ShadowLocation.position, player.playerLocation.position) > .5f)
        {
            isAttach = false;
        }
        Shadowrb.velocity = new Vector2(horizontal * moveSpeed, Shadowrb.velocity.y);
       
        if (!facingRight && horizontal < 0)
        {
            flip();
        }
        else if (facingRight && horizontal > 0)
        {
            flip();
        }
        else if (isAttach)
        {
            ShadowLocation.localScale = player.playerLocation.localScale;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && isAttach)
        {
            Shadowrb.velocity = new Vector2(Shadowrb.velocity.x, jumpPower);
            isAttach = false;
        }
        Jump();
        NextJump();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isAttach && IsGrounded())
        {
            Shadowrb.velocity = new Vector2(Shadowrb.velocity.x, jumpPower);
        }
    }
    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, Distance, ground))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void NextJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && IsGrounded())
        {
            Shadowrb.velocity = new Vector2(Shadowrb.velocity.x, jumpPower);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundry"))
        {
            ShadowLocation.position = player.playerLocation.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            ShadowLocation.position = player.playerLocation.position;
        }
        else if (collision.CompareTag("end"))
        {
            player.playerLocation.position = new Vector3(-81.59f, -8.16f, 0);
            gameObject.SetActive(false);
        }
    }
    void flip()
    {
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = -1;
            
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = 1;
           
        }
        else { horizontal = 0; }
        if (isAttach)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                horizontal = -1;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                horizontal = 1;
            }
            else
            {
                horizontal = 0;
            }
        }
    }
}
