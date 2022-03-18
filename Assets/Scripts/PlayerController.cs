using System.Collections;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public Joystick joystick;
    public Rigidbody2D rb;
    public float multiplier = 1;
    public float maxmulti;
    public float minmulti;
    public float rotSpeed = 10;
    [SerializeField] float maxPlayerSpeed = 15;

    public bool isCrash, isCooldown;
    [SerializeField] float cooldown = 0, joistickCooldown = 3;

    [SerializeField] Animator anim;
    [SerializeField] Animator playerAnimator;
    [SerializeField] SpriteRenderer gfx;



    // indicator
    [SerializeField] TextMeshProUGUI Velocity;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (joystick == null)
        {
            joystick = FindObjectOfType<Joystick>();
        }//find joystick if = null
        #region Velocity stuff

        float vel = Mathf.FloorToInt(-rb.velocity.y * 10);//simplify velocity 

        Velocity.text = vel.ToString();

        #region velocity color 
        playerAnimator.SetInteger("vel", (int)vel);

        #endregion
        #endregion
        if (isCooldown)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {

            isCooldown = false;
        }
        if (!isCrash) //called when player has been crashed
        {
            if (cooldown <= 0)
            {
                if (joystick != null)
                {
                    float vert = 0;
                    if (joystick.Vertical > 0.9f)
                    {
                        multiplier = Mathf.SmoothStep(minmulti, maxmulti, Time.deltaTime * 0.1f);//speed up smoothly 
                    }
                    else
                    {
                        multiplier = Mathf.SmoothStep(maxmulti, minmulti, Time.deltaTime * 0.1f);
                    }
                    if (joystick.Vertical > 0.5f)
                    {
                        vert = joystick.Vertical;
                        if (rb.velocity.y < 0)
                        {
                            rb.AddForce(new Vector2(joystick.Horizontal * rotSpeed, vert * multiplier));
                        }

                    }
                    else
                    {
                        vert = joystick.Vertical;
                        if (rb.velocity.y < 0)
                        {
                            rb.AddForce(new Vector2(joystick.Horizontal * rotSpeed, 0));
                        }
                        if (Mathf.Abs(rb.velocity.y) < maxPlayerSpeed)
                        {

                            rb.AddForce(new Vector2(0, vert * multiplier));
                        }
                    }
                    if (rb.velocity.y > 0)//prevent to players backslide
                    {
                        rb.velocity = new Vector2(rb.velocity.x, 0);
                    }

                }
            }
        }
        else//if crash
        {
            cooldown = 5;
            isCooldown = true;
            rb.velocity = Vector3.zero;
            anim.SetTrigger("accident");
            StartCoroutine(ResetPlayer());



        }
        if (joystick != null)
        {
            float anglevalue = joystick.Horizontal * 100;

            anim.SetInteger("Angle", (int)anglevalue);
            if (anglevalue < 0)
            {
                gfx.flipX = true;
            }
            else if (anglevalue > 0)
            {
                gfx.flipX = false;
            }

        }
    }
    IEnumerator blinkanim()
    {
        yield return new WaitForSeconds(3);
        playerAnimator.SetTrigger("BlinkAlpha");
    }
    IEnumerator ResetPlayer()
    {
        isCrash = false;
        StartCoroutine(blinkanim());
        yield return new WaitForSeconds(3);

        transform.position = new Vector2(0, transform.position.y - 0.6f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isCrash = true;

        if (collision.collider.tag == "tree")
        {
            Animator anim = collision.collider.GetComponent<Animator>();
            collision.collider.GetComponent<CircleCollider2D>().enabled = false;
            anim.SetTrigger("crash");

        }
        if (collision.collider.tag == "rock")
        {

            collision.collider.GetComponent<PolygonCollider2D>().enabled = false;

        }
    }
}
