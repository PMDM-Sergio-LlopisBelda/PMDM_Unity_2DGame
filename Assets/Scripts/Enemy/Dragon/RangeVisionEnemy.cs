using UnityEngine;

public class RangeVisionEnemy : MonoBehaviour
{
    public Animator enemyAnimator;
    public Enemy enemy;
    [SerializeField] float timeBetweenAttacks = 4f;
    [SerializeField] float timeLastAttack = 0f;
    private bool canAttack;

    void OnTriggerEnter2D(Collider2D other)
    {
        TryAttack(other);  
    }

      void OnTriggerStay2D(Collider2D other)
    {
        TryAttack(other);  
    }

    private void TryAttack(Collider2D collider) 
    {
        if (collider != null) 
        {
            if (collider.CompareTag("Player") && canAttack)
            {
                enemyAnimator.SetBool("Walk", false);
                enemyAnimator.SetBool("Run", false);
                enemy.audios.Play();
                enemyAnimator.SetBool("Attack", true);  
                enemy.hitting = true;
                GetComponent<BoxCollider2D>().enabled = false;
                canAttack = false;
                timeLastAttack = timeBetweenAttacks;
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
        if (!canAttack && timeLastAttack > 0) {
            canAttack = false;
            timeLastAttack -= Time.deltaTime;
        } else {
            canAttack = true;
        }
    }
}
