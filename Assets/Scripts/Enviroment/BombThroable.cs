using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombThroable : MonoBehaviour
{

    private float force = 12.0f;
    public float damage = 5f;
    private float radio = 2.5f;
    private float forceExplotion = 250f;
    private Rigidbody2D rb;
    //private HpManager hpManager;
    public GameObject explotion;
    private HpManager hpManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        //hpManager = new HpManager();
        ThrowBomb(); 
    }

    private void ThrowBomb()
	{
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);   
        StartCoroutine(Explote());     
	}

    IEnumerator Explote()
    {
        Destroy(gameObject, 1.6f);
        yield return new WaitForSeconds(1.5f);
        
        Destroy(Instantiate(explotion, transform.position, Quaternion.identity), 2.0f);
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, radio);
        foreach (Collider2D coll in objects)
        {
            Rigidbody2D rb2d = coll.GetComponent<Rigidbody2D>();
            if (rb2d != null) {
                Vector2 direction = coll.transform.position - transform.position; 
                float distance = 1 + direction.magnitude;
                float finalForce = forceExplotion/distance;
                rb2d.AddForce(direction*finalForce);
                if (coll.CompareTag("Player")) {
                    HpManager enemyHpManager = coll.GetComponent<HpManager>();
                    if (enemyHpManager != null) {
                        enemyHpManager.TakeDamage(damage);
                    }
                }
            }        
            
        }
        
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("DAÑO");
            //hpManager.TakeDamage(damage);
        }
    }

}
