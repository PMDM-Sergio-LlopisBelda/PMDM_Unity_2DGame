using System.Collections;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float Speed = 0.0f;
    public float lateralMovement = 2.0f;
    public float jumpMovement = 400.0f;
    [SerializeField] bool canHeal = false;
    public bool isGrounded = true;
    public bool canMove = true;
    public bool playingWithButtons = false;
    public float scale = 0.7f;
    private bool movesLeft = false;
    private bool movesRight = false;
    public bool playWithMobile;
    public Transform groundCheck;
    public Animator animator;
    public Rigidbody2D rigidbody2d;
    public ShopChestHandler shopChestHandler;
    private HpManagerPlayer HpManagerPlayer;
    public Collider2D[] bossDors;
    public AudioSource jumpSound;

    void Start () 
    {
        #if UNITY_EDITOR 
        {
            playingWithButtons = false;
        }
        #else
        {
            playingWithButtons = true;
        }
        #endif

        rigidbody2d = GetComponent<Rigidbody2D> ();
        HpManagerPlayer = GetComponent<HpManagerPlayer>();
    }

    void Update () 
    {
        isGrounded = Physics2D.Linecast (transform.position,
        groundCheck.position,
        LayerMask.GetMask("Ground"));

        if (!playingWithButtons) {
            if (isGrounded && Input.GetButtonDown("Jump")) {
                jumpSound.Play();
                rigidbody2d.AddForce (Vector2.up * jumpMovement);
                isGrounded = false;
            }

            if (canMove) {
                Speed = Input.GetAxis("Horizontal") * lateralMovement;
                transform.Translate (Vector2.right * Speed * Time.deltaTime);
                animator.SetFloat("Speed", Mathf.Abs(Speed));
            }
        }

        if (isGrounded) {
            animator.SetTrigger("Grounded");
        } else {
            animator.SetTrigger("Jump");
        }

        if (Speed < 0) {
            transform.localScale = new Vector3(-scale, scale, scale);
        } else if (Speed > 0){
            transform.localScale = new Vector3(scale, scale, scale);
        }

    }

    public void MoveLeftButtonDown() {
        if (playingWithButtons) {
            movesLeft = true;
            StartCoroutine(Left());
        }
    }

    public void MoveLeftButtonUp() {
        if (playingWithButtons) {
            Speed = 0;
            animator.SetFloat("Speed", 0);
            movesLeft = false;
        }
    }

    IEnumerator Left() {
        while (movesLeft) {
            if (canMove) {
                Speed = -lateralMovement;
                transform.Translate (Vector2.right * Speed * Time.deltaTime);
                animator.SetFloat("Speed", Mathf.Abs(Speed));
                playingWithButtons = true;
            }
            yield return null;
        }
    }

    public void MoveRightButtonDown() {
        if (playingWithButtons) {
            movesRight = true;
            StartCoroutine(Right());            
        }
    }
    public void MoveRightButtonUp() {
        if (playingWithButtons) {
            Speed = 0;
            animator.SetFloat("Speed", 0);
            movesRight = false;            
        }
    }

    IEnumerator Right() {
        while (movesRight) {
            if (canMove) {
                Speed = lateralMovement;
                transform.Translate (Vector2.right * Speed * Time.deltaTime);
                animator.SetFloat("Speed", Mathf.Abs(Speed));
                playingWithButtons = true;
            }
            yield return null;
        }
    }

    public void JumpButton() {
        if (playingWithButtons && isGrounded) {
            jumpSound.Play();
            rigidbody2d.AddForce (Vector2.up * jumpMovement);
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.CompareTag("Zoom")) {
            GameObject.Find("MainVirtual").GetComponent<CinemachineVirtualCamera>().enabled = false;
        }
            
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
            SceneManager.LoadScene("SceneMenu");
        }

        if (collider.gameObject.tag == "ShopLevelEnd") {
            SceneManager.LoadScene("CaveLevel");
        }

        if (collider.gameObject.tag == "FirstLevelEnd") {
            SceneManager.LoadScene("ShopScene");
        }

        if (collider.gameObject.tag == "KillArea") {
            GameManager.currentHP = 0;
        }

    }

    private void OnTriggerStay2D(Collider2D collider) {
        if (collider.gameObject.tag == "HealingFountain") {
            canHeal = true;
            StartCoroutine(FountainHealPlayer());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Zoom"))
            GameObject.Find("MainVirtual").GetComponent<CinemachineVirtualCamera>().enabled = true;
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
            HpManagerPlayer.Heal();
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

    private void EnableButtons(bool enable) {
        
    }

}