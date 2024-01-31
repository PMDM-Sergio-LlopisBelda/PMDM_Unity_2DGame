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

    // Start is called before the first frame update
    void Start()
    {
        actualHp = maxHp;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (actualHp <= 0) {
            if (parentObject != null) {
                Destroy(parentObject);
            } else {
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage(float damage) {
        if (actualHp > 0f) {
            animator.SetTrigger("isDamaged");
            Destroy(Instantiate(bloodParticles, transform.position, Quaternion.identity), 1.0f);
            actualHp -= damage;
            //PushWhenDamaged();
        }
    }

    private void PushWhenDamaged() {
        transform.Translate(-transform.forward * distanceToMove);
    }

}
