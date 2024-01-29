using System.Collections;
using UnityEngine;

public class Grapper : MonoBehaviour
{

    [Header("Hook Parameters")]
    public float maxDistance = 10f;
    public float grappleSpeed = 10f;
    public float grappleShootSpeed = 20f;
    private bool isGrappling = false;
    private bool connectedHook = false;
    private bool clicked = false;
    private bool isImpulsed = false;

    public GameObject player;
    public LayerMask grapplableMask;
    private PlayerController playerScript;
    private LineRenderer line;
    private Vector2 target;
    private Vector2 direction;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        playerScript = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isGrappling)
        {
            clicked = true;
            StarGrapple();
        }

        if (Input.GetMouseButtonUp(1)) {
            clicked = false;
            DisconectHook();
        }

        if (connectedHook) 
        {
            line.SetPosition(0, transform.position);

            if (!isImpulsed) {
                playerScript.rigidbody2d.AddForce(direction * 1.5f, ForceMode2D.Impulse);
                isImpulsed = true;
                DisconectHook();
            }
            
            if (Vector2.Distance(transform.position, target) < 1.5f)
            {
                DisconectHook();
            }
        }

    }

    private void StarGrapple() {

        // Se guarda el punto donde se ha cliclado.
        direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grapplableMask);

        if (hit.collider != null) {
            isImpulsed = false;
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

        if (clicked) {
           connectedHook = true; 
           playerScript.canMove = false;
        }
        line.SetPosition(1, target);
    }


    private void DisconectHook()
    {
        playerScript.canMove = true;
        connectedHook = false;
        isGrappling = false;
        line.enabled = false;
    }   

}
