using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulse : MonoBehaviour
{

    public float timeBetweenBombs;
    public GameObject bomb;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ThorwBomb());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        IEnumerator ThorwBomb() 
    {
        while(true) 
        {
            Instantiate(bomb);
            bomb.gameObject.GetComponent<Rigidbody2D>().transform.position = transform.position;
            yield return new WaitForSeconds(timeBetweenBombs);
        }
    }
}
