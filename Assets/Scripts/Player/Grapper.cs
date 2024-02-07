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
    public AudioSource chairSound;
    private PlayerController playerController;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!playerController.playingWithButtons) {
            if (Input.GetMouseButtonDown(1) && !isGrappling)
            {
                StarGrapple();
            }
        } else {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    if (touch.phase == TouchPhase.Began)
                    {
                        if (!isGrappling)
                        {
                            StarGrapple();
                        }
                    }
                }
            }
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

    private void StarGrapple()
{
    // Se guarda el punto donde se ha clicado.
    Vector2 clickPosition;
    #if UNITY_EDITOR || UNITY_STANDALONE
        clickPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    #else
        clickPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
    #endif

    // Verifica si la posición de clic está por encima del jugador
    if (clickPosition.y > player.transform.position.y)
    {
        Vector2 direction = clickPosition - (Vector2)transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, maxDistance, grapplableMask);

        if (hit.collider != null)
        {
            isGrappling = true;
            target = hit.point;
            line.enabled = true;
            line.positionCount = 2;

            StartCoroutine(Grapple());
        }
    }
}

    IEnumerator Grapple()
    {
        float currentTimeGrappling = 0;
        float maxTimeGrappling = 2f;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);

        Vector2 newPosition;
        chairSound.Play();
        for (; currentTimeGrappling < maxTimeGrappling; currentTimeGrappling += grappleShootSpeed * Time.deltaTime)
        {
            newPosition = Vector2.Lerp(transform.position, target, currentTimeGrappling / maxTimeGrappling);
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
