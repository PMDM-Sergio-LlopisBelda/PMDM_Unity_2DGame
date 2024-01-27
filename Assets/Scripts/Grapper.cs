using System.Collections;
using UnityEngine;

public class Grapper : MonoBehaviour
{

    [Header("Hook Parameters")]
    public float maxDistance = 10f;
    public float grappleSpeed = 10f;
    public float grappleShootSpeed = 20f;
    private bool isGrappling = false;
    private bool retracting = false;

    public GameObject player;
    public LayerMask grapplableMask;
    private PlayerController playerScript;
    private LineRenderer line;
    private Vector2 target;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        player = GetComponent<GameObject>();
        playerScript = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGrappling)
        {
            StarGrapple();
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 grapplePosition = Vector2.Lerp(transform.position, target, grappleSpeed * Time.deltaTime);
       
            transform.position = grapplePosition;

            line.SetPosition(0, transform.position);
        
            if (Vector2.Distance(transform.position, target) < 1.5f)
            {
                isGrappling = false;
                line.enabled = false;
                playerScript.canMove = true;
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            isGrappling = false;
            line.enabled = false;
            playerScript.canMove = true;
        }


    }
    private void StarGrapple() {
        playerScript.canMove = false;

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

    IEnumerator Grapple() {
        float currentTimeGrappling = 0;
        float maxTimeGrappling = 1.5f;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);

        Vector2 newPosition;

        for (; currentTimeGrappling < maxTimeGrappling; currentTimeGrappling += grappleShootSpeed * Time.deltaTime) 
        {
            newPosition = Vector2.Lerp(transform.position, target, currentTimeGrappling/maxTimeGrappling);
            line.SetPosition(0, transform.position);
            line.SetPosition(1, newPosition);
            yield return null;
        }

        line.SetPosition(1, target);
        retracting = true;
    }

}
