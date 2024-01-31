using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Atributes")]
    public float routine;
    public float crono;
    public int direcction;
    public float speedWalk;
    public float speedRun;
    public bool hitting;
    public float scale = 0.85f; 
    public float timeBetweenAttack = 1.5f;
    public float timeNextAttack = 0f;
    private float maxRangeViewOnY = 1f; 
    //public RectTransform hpBar;

    [Header("Attack")]
    public float rangeVision;
    public float rangeAttack;
    public  GameObject range;
    public  GameObject Hit;
    public Animator enemiyAnimation;
    public GameObject target;

    public Rigidbody2D rb;


    void Start()
    {
        enemiyAnimation = GetComponent<Animator>();
        target = GameObject.Find("Player");
        transform.localScale.Set(scale, scale, scale);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (timeNextAttack > 0) {
            timeNextAttack -= Time.deltaTime;
        }

        Behaviors();
    }

    public void Behaviors()
    {

        if (Mathf.Abs(transform.position.x - target.transform.position.x) > rangeVision && !hitting)
        {
            enemiyAnimation.SetBool("Run", false);
            crono += 1 * Time.deltaTime;

            if (crono >= 2)
            {
                routine = Random.Range(0, 2);
                crono = 0;
            }

            switch(routine)
            {
                case 0:
                    enemiyAnimation.SetBool("Walk", false);
                    break;
                
                case 1:
                    direcction = Random.Range(0 ,2);
                    routine++;
                    break;
                
                case 2:
                    switch(direcction)
                    {
                        case 0:
                            transform.rotation = Quaternion.Euler(0,0,0);
                            transform.Translate(Vector3.right * speedWalk * Time.deltaTime); 
                            break;
                        
                        case 1:
                            transform.rotation = Quaternion.Euler(0,180,0);
                            transform.Translate(Vector3.right * speedWalk * Time.deltaTime); 
                            break;
                    }
                    enemiyAnimation.SetBool("Walk", true);
                    break;    
            }

        }
        else 
        {
            if (Mathf.Abs(transform.position.x - target.transform.position.x) > rangeAttack && !hitting)
            {

                if (transform.position.x < target.transform.position.x && 
                    transform.position.y - target.transform.position.y < maxRangeViewOnY)
                {
                    enemiyAnimation.SetBool("Walk", false);
                    enemiyAnimation.SetBool("Run", true);
                    transform.Translate(Vector3.right * speedRun * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,0,0);
                    enemiyAnimation.SetBool("Attack", false);
                    //hpBar.position = transform.position;
                }
                else if (transform.position.x > target.transform.position.x && 
                        transform.position.y - target.transform.position.y < maxRangeViewOnY)
                {
                    enemiyAnimation.SetBool("Walk", false);
                    enemiyAnimation.SetBool("Run", true);
                    transform.Translate(Vector3.right * speedRun * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,180,0);
                    enemiyAnimation.SetBool("Attack", false);
                    //hpBar.position = transform.position;
                }

            }
            else
            {
                if (!hitting)
                {
                    if (transform.rotation.x < target.transform.position.x)
                    {
                        transform.rotation = Quaternion.Euler(0,0,0);
                    }
                    else
                    {
                        transform.rotation = Quaternion.Euler(0,180,0);
                    }
                    enemiyAnimation.SetBool("Walk", false);
                    enemiyAnimation.SetBool("Run", false);
                }
            }
        }

        
    }

    public void FinalAnimation()
    {
        enemiyAnimation.SetBool("Attack", false);
        hitting = false;
        range.GetComponent<BoxCollider2D>().enabled = true;
    }

    public void SetColliderWeaponTrue()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = true;
    } 

        public void SetColliderWeaponFalse()
    {
        Hit.GetComponent<BoxCollider2D>().enabled = false;
    } 


}
