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
    public Rigidbody2D rigidbody2d;
    public bool isGrounded = true;
    public bool canMove = true;
    public float scale = 0.7f;
    public ShopChestHandler shopChestHandler;    

    void Start () 
    {
        animator = GetComponent<Animator> ();
        rigidbody2d = GetComponent<Rigidbody2D> ();
    }
    void Update () 
    {
        isGrounded = Physics2D.Linecast (transform.position,
        groundCheck.position,
        LayerMask.GetMask("Ground"));

        if (isGrounded && Input.GetButtonDown("Jump")) {
            rigidbody2d.AddForce (Vector2.up * jumpMovement);
        }
        
        if (isGrounded) {
            animator.SetTrigger("Grounded");
        } else {
            animator.SetTrigger("Jump");
        }

        if (canMove) {
            Speed = Input.GetAxis("Horizontal") * lateralMovement;
            transform.Translate (Vector2.right * Speed * Time.deltaTime);
            animator.SetFloat("Speed", Mathf.Abs(Speed));
        }


        if (Speed < 0) {
            transform.localScale = new Vector3(-scale, scale, scale);
        } else if (Speed > 0){
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Gold") {
            GameManager.coindsCollected++;
            Destroy (collider.gameObject);
        }
        if (collider.gameObject.tag == "ShopChest") {
            StartCoroutine(OpenShopChest());
        }
    }

    IEnumerator OpenShopChest() {
        if (GameManager.canOpenShopChest) {
            shopChestHandler.HandleOpenChest();
            GameManager.canOpenShopChest = false;
            yield return new WaitForSeconds(10f);
        }
    }

}