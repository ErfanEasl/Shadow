using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class PlayerMovment : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerrb;
    public Collider2D PlayerCollider;
    public Transform playerLocation;

    [SerializeField] float pushPower;
    [SerializeField] LayerMask ground;
    [SerializeField] Vector2 boxSize;
    [SerializeField] float Distance;

    public Vector3 checkPoint;

    private float horizontal;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpPower = 10f;
    bool facingRight;
    // Start is called before the first frame update
    void Start()
    {
        checkPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (playerLocation.position != new Vector3(-81.59f, -8.14496f, 0))
        {
            playerrb.velocity = new Vector2(horizontal * moveSpeed, playerrb.velocity.y);
        }
        if (!facingRight && horizontal < 0)
        {
            flip();
        }
        else if (facingRight && horizontal > 0)
        {
            flip();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && playerLocation.position != new Vector3(-81.59f, -8.14496f, 0))
        {
            playerrb.velocity = new Vector2(playerrb.velocity.x, jumpPower);
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * Distance, boxSize);
    }
    void flip()
    {
        if (playerLocation.position != new Vector3(-81.59f, -8.14496f, 0))
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            transform.position = checkPoint;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Save"))
        {
            checkPoint = collision.transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider other)
    {
        if (other.CompareTag("end"))
        {
            Debug.Log("end");
        }
    }
}
