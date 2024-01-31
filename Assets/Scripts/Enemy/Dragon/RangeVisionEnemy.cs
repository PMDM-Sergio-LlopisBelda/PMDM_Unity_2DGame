using UnityEngine;

public class RangeVisionEnemy : MonoBehaviour
{

    public Animator enemyAnimator;
    public Enemy enemy;
    public float damage = 5;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null) {
            if (collider.CompareTag("Player"))
            {
                enemyAnimator.SetBool("Walk", false);
                enemyAnimator.SetBool("Run", false);
                enemyAnimator.SetBool("Attack", true);
                enemy.hitting = true;
                collider.GetComponent<HpManager>().TakeDamage(damage);
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
