using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject entityToFollow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        transform.position = new Vector3(entityToFollow.transform.position.x,
        (entityToFollow.transform.position.y + 0.5f),
        transform.position.z);
    }
}
