using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
   public Animator animator;
    public Animator door;
    public Collider2D doorCol;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shadow"))
        {
            animator.SetBool("IsPressed", true);
            door.SetBool("IsOpened", true);
            doorCol.enabled = false;
        }
    }
}
