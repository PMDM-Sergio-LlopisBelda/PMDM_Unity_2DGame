using UnityEngine;

public class RangeVisionEnemy : MonoBehaviour
{

    public Animator animator;
    public Enemy enemy;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider != null) {
            if (collider.CompareTag("Player"))
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Run", false);
                animator.SetBool("Attack", true);
                enemy.hitting = true;
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
