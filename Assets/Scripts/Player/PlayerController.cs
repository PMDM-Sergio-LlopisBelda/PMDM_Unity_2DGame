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
    public Animator animator;
    public Rigidbody2D rigidbody2d;
    public bool isGrounded = true;
    public bool canMove = true;
    public float scale = 0.7f;
    public ShopChestHandler shopChestHandler;
    private HpManager hpManager;
    public Collider2D[] bossDors;
    public bool playingWithButtons = false;

    private bool canHeal = false;

    void Start () 
    {
        //animator = GetComponent<Animator>();
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

        if (isGrounded && Input.GetButtonDown("Jump") && !playingWithButtons) {
            rigidbody2d.AddForce (Vector2.up * jumpMovement);
            isGrounded = false;
        }
        
        if (isGrounded) {
            animator.SetTrigger("Grounded");
        } else {
            animator.SetTrigger("Jump");
        }

        if (!playingWithButtons) {
            if (canMove) {
                Speed = Input.GetAxis("Horizontal") * lateralMovement;
                transform.Translate (Vector2.right * Speed * Time.deltaTime);
                animator.SetFloat("Speed", Mathf.Abs(Speed));
            }
        }


        if (Speed < 0) {
            transform.localScale = new Vector3(-scale, scale, scale);
        } else if (Speed > 0){
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }

    public void MoveLeft() {
        if (canMove) {
            Speed = -lateralMovement;
            transform.Translate (Vector2.right * Speed * Time.deltaTime);
            animator.SetFloat("Speed", Mathf.Abs(Speed));
            playingWithButtons = true;
        }
    }

    public void MoveRight() {
        if (canMove) {
            Speed = lateralMovement;
            transform.Translate (Vector2.right * Speed * Time.deltaTime);
            animator.SetFloat("Speed", Mathf.Abs(Speed));
            playingWithButtons = true;
        }
    }

    public void JumpByButton() {
        if (isGrounded) {
            isGrounded = false;
            animator.SetTrigger("Jump");
            rigidbody2d.AddForce (Vector2.up * jumpMovement);
            playingWithButtons = true;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Gold") {
            GameManager.coindsCollected++;
            GameManager.totalCoins++;
            SpriteRenderer spriteRenderer = collider.GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = false;
            AudioSource audioSource = collider.GetComponent<AudioSource>();
            audioSource.Play();
            collider.enabled = false;
            Destroy(collider.gameObject, 2f);
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

        if (collider.gameObject.tag == "ShopLevelEnd") {
            SceneManager.LoadScene("CaveLevel");
        }

        if (collider.gameObject.tag == "FirstLevelEnd") {
            SceneManager.LoadScene("ShopScene");
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

        void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("MovePlatform"))
        {
            transform.parent = collision.transform;
            
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        transform.parent = null;
    }

}