using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 0.0f;
    public float lateralMovement = 2.0f;
    public float jumpMovement = 400.0f;
    public Transform groundCheck;
    private Animator animator;
    private Rigidbody2D rigidbody2d;
    public bool isGrounded = true;
    public bool canMove = true;
    

    void Start () 
    {
        animator = GetComponent<Animator> ();
        rigidbody2d = GetComponent<Rigidbody2D> ();
    }

    void Update () 
    {
        isGrounded = Physics2D.Linecast (transform.position,
        groundCheck.position,
        LayerMask.GetMask ("Ground"));

        if (isGrounded && Input.GetButtonDown ("Jump")) {
            rigidbody2d.AddForce (Vector2.up * jumpMovement);
        }
        
        if (isGrounded) {
            animator.SetTrigger ("Grounded");
        } else {
            animator.SetTrigger ("Jump");
        }

        if (canMove) {
            Speed = Input.GetAxis("Horizontal") * lateralMovement;
            transform.Translate (Vector2.right * Speed * Time.deltaTime);
            animator.SetFloat("Speed", Mathf.Abs(Speed));
        }


        if (Speed < 0) {
            transform.localScale = new Vector3 (-1, 1, 1);
        } else if (Speed > 0){
            transform.localScale = new Vector3 (1, 1, 1);
        }
}

}