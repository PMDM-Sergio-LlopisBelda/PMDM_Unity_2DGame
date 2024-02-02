using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impulse : MonoBehaviour
{

    [Header("Coordenates")]
    public float x;
    public float y;
    public float z;
    
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
            bomb.gameObject.GetComponent<Rigidbody2D>().transform.position = new Vector3(x, y, z);
            yield return new WaitForSeconds(timeBetweenBombs);
        }
    }
}
