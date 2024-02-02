using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    
    public GameObject objectToMove;
    public Transform StartPoint;
    public Transform EndPoint;
    public float speed;
    private Vector3 moveTo;
    public float timeBetweenMoves = 2f;
    public float timeLastMove = 0f;

    void Start()
    {
        moveTo = EndPoint.position;
        StartCoroutine(MovePlatform());
    }

    IEnumerator MovePlatform()
    {
        while(true) {
        yield return new WaitForSeconds(5f);
        if (objectToMove.transform.position == EndPoint.position) 
            {
                moveTo = StartPoint.position;
            }
                    
            if (objectToMove.transform.position == StartPoint.position)
            {
                moveTo = EndPoint.position;
            }
            

        }
   
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLastMove > 0) {
            timeLastMove -= Time.deltaTime;
        }

        objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, moveTo, speed * Time.deltaTime);

    }
}
