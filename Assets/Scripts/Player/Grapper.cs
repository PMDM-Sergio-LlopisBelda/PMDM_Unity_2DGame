using System.Collections;
using UnityEngine;

public class Grapper : MonoBehaviour
{

    [Header("Hook Parameters")]
    public float maxDistance = 14f;
    public float grappleSpeed = 10f;
    public float grappleShootSpeed = 20f;
    private bool isGrappling = false;
    private bool connectedHook = false;
    public float timeBetweemHook = 3f;
    public float timeNextHook = 0f;

    public GameObject player;
    public LayerMask grapplableMask;
    private LineRenderer line;
    private Vector2 target;

    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isGrappling)
        {
            StarGrapple();
        }

        if (connectedHook) 
        {
            Vector2 grapplePos = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);
            line.SetPosition(0, transform.position);

            transform.position = grapplePos;

            line.SetPosition(0, transform.position);
            
            if (Vector2.Distance(transform.position, target) < 1.5f)
            {
                DisconectHook();
            }
        }

    }

    private void StarGrapple() {

        // Se guarda el punto donde se ha cliclado.
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grapplableMask);

        if (hit.collider != null) {
            isGrappling = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;

            StartCoroutine(Grapple());
        }
    }

    IEnumerator Grapple() 
    {
        float currentTimeGrappling = 0;
        float maxTimeGrappling = 2f;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);

        Vector2 newPosition;

        for(; currentTimeGrappling < maxTimeGrappling; currentTimeGrappling += grappleShootSpeed * Time.deltaTime)
        {
            newPosition = Vector2.Lerp(transform.position, target, currentTimeGrappling/maxTimeGrappling);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, newPosition);
            yield return null;
        }

        connectedHook = true; 
        line.SetPosition(1, target);
    }

    private void DisconectHook()
    {
        connectedHook = false;
        isGrappling = false;
        line.enabled = false;
    }   

}
