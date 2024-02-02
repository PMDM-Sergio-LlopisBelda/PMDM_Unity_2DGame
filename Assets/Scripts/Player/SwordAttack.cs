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
    private HpManager hpManager;
    public float timeBetweenAttacks = 1f;
    public float timeLastAttack = 0f;
    private PlayerController playerController;
    private bool attackingWithButtons = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        enemyLayer = LayerMask.GetMask("Enemy");
        hpManager = GetComponent<HpManager>();
        playerController = GetComponent<PlayerController>();
        damage = GameManager.playerDmg;
    }

    // Update is called once per frame
    void Update()
    {
        damage = GameManager.playerDmg;
        if (Input.GetMouseButton(0) && canAttack && !attackingWithButtons)
        {
            StartCoroutine(PlayerAttacks());
        }
    }

    public void AttackByButton() {
        attackingWithButtons = true;
        if (canAttack || ! attackingWithButtons) {
            StartCoroutine(PlayerAttacks());
        }
    }

    IEnumerator PlayerAttacks()
    {
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
            HpManager enemyHpManager = enemy.GetComponent<HpManager>();

            if (enemyHpManager != null) {
                enemyHpManager.TakeDamage(damage);
            }

        }
    }

    /*private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(damageArea.bounds.center, damageArea.bounds.size);
    }*/
}
