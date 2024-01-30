using UnityEngine;

public class RangeVisionEnemy : MonoBehaviour
{

    public Animator enemyAnimator;
    public Enemy enemy;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            enemyAnimator.SetBool("Walk", false);
            enemyAnimator.SetBool("Run", false);
            enemyAnimator.SetBool("Attack", true);
            enemy.hitting = true;            
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
