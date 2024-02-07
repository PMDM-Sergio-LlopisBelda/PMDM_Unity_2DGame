using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = GameManager.playerDmg;
    private Animator animator;
    private bool canAttack = true;
    public Collider2D damageArea;
    private LayerMask enemyLayer;
    private HpManagerPlayer hpManagerPlayer;
    public float timeBetweenAttacks = 1f;
    public float timeLastAttack = 0f;
    private PlayerController playerController;
    private bool attackingWithButtons = false;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyLayer = LayerMask.GetMask("Enemy");
        hpManagerPlayer = GetComponent<HpManagerPlayer>();
        playerController = GetComponent<PlayerController>();
        damage = GameManager.playerDmg;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.playingWithButtons) {
            if (Input.GetMouseButton(0) && canAttack && playerController.isGrounded)
            {
                StartCoroutine(PlayerAttacks());
            }
        }
    }

    public void AttackByButton() {
        if (playerController.playingWithButtons) {
            if (canAttack && playerController.isGrounded) {
                StartCoroutine(PlayerAttacks());
            }
        }
    }

    IEnumerator PlayerAttacks()
    {
        damage = GameManager.playerDmg;
        audioSource.Play();
        canAttack = false;
        playerController.canMove = false;
        DealDamage();
        //hpManager.TakeDamage(5);
        animator.SetTrigger("IsAttacking");
        yield return new WaitForSeconds(0.35f);
        playerController.canMove = true;
        yield return new WaitForSeconds(1f);
        canAttack = true;
    }

    private void DealDamage()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(damageArea.bounds.center, damageArea.bounds.size, 0, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            HpManagerEnemy1 hpManagerEnemy1 = enemy.GetComponent<HpManagerEnemy1>();

            if (hpManagerEnemy1 != null) {
                hpManagerEnemy1.TakeDamage(damage);
            }

        }
    }

}
