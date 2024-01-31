using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpManager : MonoBehaviour
{
    public float maxHp = 20;
    public float actualHp;
    private Animator animator;
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
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage) {
        if (actualHp > 0f) {
            animator.SetTrigger("isDamaged");
            print(actualHp);
            actualHp -= damage;
            print(actualHp);
        }
    }
}
