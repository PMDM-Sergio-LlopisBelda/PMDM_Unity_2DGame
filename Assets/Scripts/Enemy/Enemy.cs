using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Atributes")]
    public float rutin;
    public float crono;
    public float speedWalk;
    public float speedRun;
    public int direcction;
    public bool hitting;

    [Header("Attack")]
    public float rangeVision;
    public float rangeAttack;
    public  GameObject range;
    public  GameObject Hit;
    public Animator animator;
    public GameObject target;


    void Start()
    {
        animator = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Behaviors();
    }

    public void Behaviors()
    {

        if (Mathf.Abs(transform.position.x - target.transform.position.x) > rangeVision && !hitting)
        {
            animator.SetBool("Run", false);
            crono += 1 * Time.deltaTime;

            if (crono >= 4)
            {
                rutin = Random.Range(0, 2);
                crono = 0;
            }

            switch(rutin)
            {
                case 0:
                    animator.SetBool("Walk", false);
                    break;
                
                case 1:
                    direcction = Random.Range(0 ,2);
                    rutin++;
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
                    animator.SetBool("Walk", true);
                    break;    
            }

        }
        else 
        {
            if (System.Math.Abs(transform.position.x - target.transform.position.x) > rangeAttack && !hitting)
            {
                if (transform.position.x < target.transform.position.x)
                {
                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", true);
                    transform.Translate(Vector3.right * speedRun * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,0,0);
                    animator.SetBool("Attack", false);
                }
                else 
                {
                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", true);
                    transform.Translate(Vector3.right * speedRun * Time.deltaTime);
                    transform.rotation = Quaternion.Euler(0,180,0);
                    animator.SetBool("Attack", false);
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
                    animator.SetBool("Walk", false);
                    animator.SetBool("Run", false);
                }
            }
        }

        
    }

    public void FinalAnimation()
    {
        animator.SetBool("Attack", false);
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
