using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HpManagerEnemy1 : MonoBehaviour
{
    public float maxHp = 20;
    public float actualHp;
    private Animator animator;
    public GameObject parentObject;
    public ParticleSystem bloodParticles;
    public GameObject loot;
    public Collider2D[] bossDors;
    private LevelInterface levelInterface;

    // Start is called before the first frame update
    void Start()
    {
        actualHp = maxHp;
        animator = GetComponent<Animator>();
        levelInterface = FindAnyObjectByType<LevelInterface>();
    }

    // Update is called once per frame
    void Update()
    {
        if (actualHp <= 0) {
            if (parentObject != null && gameObject.tag.Equals("Enemy")) {
                if (loot != null) {
                    Instantiate(loot, transform.position, Quaternion.identity);
                }
                
                Destroy(parentObject);
            }
        }
    }

    public void TakeDamage(float damage) {
        if (actualHp > 0f) {
            animator.SetTrigger("isDamaged");
            Destroy(Instantiate(bloodParticles, transform.position, Quaternion.identity), 1.0f);
            actualHp -= damage;
        }
    }

}
