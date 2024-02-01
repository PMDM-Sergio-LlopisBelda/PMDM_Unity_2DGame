using System.Collections;
using UnityEngine;

public class Respwan : MonoBehaviour
{
    public GameObject spider;
    public float timeBetweenSpider = 2f;
    public float timeLastSpider = 0;
    public float minX = -6f, maxX = 0;
    public bool canRespawn = true;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RespawnSpider(canRespawn));
    }

    // Update is called once per frame
    void Update()
    {
    }

        IEnumerator RespawnSpider(bool state) 
    {
        while(state) 
        {
            GameObject enemy = Instantiate(spider);
            enemy.transform.position = new Vector3(Random.Range(minX, maxX), 2f, 0);
            yield return new WaitForSeconds(5f);
        }
    }


}
