using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpManager : MonoBehaviour
{
    public float maxHp = 20;
    public float actualHp;
    private Animator animator;
    private float distanceToMove = 10;
    public GameObject parentObject;
    public ParticleSystem bloodParticles;
    public GameObject loot;
    public Collider2D[] bossDors;
    private LevelInterface levelInterface;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag.Equals("Player")) {
            maxHp = GameManager.playerMaxHp;
        }
        actualHp = maxHp;
        animator = GetComponent<Animator>();
        levelInterface = FindAnyObjectByType<LevelInterface>();
    }

    // Update is called once per frame
    void Update()
    {
        if (actualHp <= 0) {
            if (parentObject != null) {
                Instantiate(loot, transform.position, Quaternion.identity);
                Destroy(parentObject);
            } else {
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(float damage) {
        if (actualHp > 0f) {
            animator.SetTrigger("isDamaged");
            levelInterface.SpawnParticles(transform, bloodParticles);
            actualHp -= damage;

            if (bossDors != null) {
                for(int i = 0; i < bossDors.Length; i++) {
                bossDors[i].enabled = false;
            } 
            }

            //PushWhenDamaged();
        }
    }

    private void PushWhenDamaged() {
        transform.Translate(-transform.forward * distanceToMove);
    }

    public void Heal() {
        if (actualHp < maxHp) {
            actualHp++;
        }
    }

}
