using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittingEnemy : MonoBehaviour
{
    public float damage = 5;


    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")) {
            collider.GetComponent<HpManager>().TakeDamage(damage);   
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
