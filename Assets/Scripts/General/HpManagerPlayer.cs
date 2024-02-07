using UnityEngine;
using UnityEngine.SceneManagement;

public class HpManagerPlayer : MonoBehaviour
{
    private Animator animator;
    public ParticleSystem bloodParticles;
    public Collider2D[] bossDors;
    private LevelInterface levelInterface;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        levelInterface = FindAnyObjectByType<LevelInterface>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.currentHP <= 0) 
        {
            SceneManager.LoadScene("DeathMenu");
        }
    }

    public void TakeDamage(float damage) {
        if (GameManager.currentHP > 0f) {
            animator.SetTrigger("isDamaged");
            Destroy(Instantiate(bloodParticles, transform.position, Quaternion.identity), 2.0f);
            GameManager.currentHP -= damage;
            if (bossDors != null) {
                for(int i = 0; i < bossDors.Length; i++) {
                bossDors[i].enabled = false;
                } 
            }
        }
    }

    public void Heal() {
        if (GameManager.currentHP < GameManager.playerMaxHp) {
            GameManager.currentHP++;
        }
    }

}
