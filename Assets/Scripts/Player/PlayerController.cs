using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private HpManager hpManager;
    public Collider2D[] bossDors;

    private bool canHeal = false;

    void Start () 
    {
        animator = GetComponent<Animator> ();
        rigidbody2d = GetComponent<Rigidbody2D> ();
        hpManager = GetComponent<HpManager>();
    }
    void Update () 
    {
        if (!isGrounded) {
            isGrounded = Physics2D.Linecast (transform.position,
            groundCheck.position,
            LayerMask.GetMask("Ground"));
        }

        if (!isGrounded) {
            isGrounded = Physics2D.Linecast (transform.position,
            groundCheck.position,
            LayerMask.GetMask("Enemy"));
        }

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
            Destroy(collider.gameObject);
        }
        if (collider.gameObject.tag == "ShopChest") {
            StartCoroutine(OpenShopChest());
        }
        if (collider.gameObject.tag == "BossArenaDetector") {
            collider.enabled = false;
            
            for(int i = 0; i < bossDors.Length; i++) {
                bossDors[i].enabled = true;
            } 
        }

        if (collider.gameObject.tag == "CaveLevelEnd") {
            SceneManager.LoadScene("MainMenu");
        }

        if (collider.gameObject.tag == "KillArea") {
            hpManager.actualHp = 0;
        }

    }

    private void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.tag == "HealingFountain") {
            canHeal = true;
            StartCoroutine(FountainHealPlayer());
        }
    }

    IEnumerator OpenShopChest() {
        if (GameManager.canOpenShopChest) {
            shopChestHandler.HandleOpenChest();
            GameManager.canOpenShopChest = false;
            yield return new WaitForSeconds(10f);
        }
    }

    IEnumerator FountainHealPlayer() {
        while(canHeal) {
            hpManager.Heal();
            canHeal = false;
            yield return new WaitForSeconds(1);
        }
    }

}