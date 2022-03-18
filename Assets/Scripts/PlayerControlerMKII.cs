using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerControlerMKII : MonoBehaviour
{
    [SerializeField] GameObject MoveByDircetion;
    DirectionByJoy dbj;
    Rigidbody2D rb;
    public Joystick joystick;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] float newMoveSpeed;
    [SerializeField] float veltext;
    int pointsMultiplier =1;
    [SerializeField] float timeToreset;
    bool resetPMultiplier=true;
    public int points;
    Vector3 gfxOrgin;
    [SerializeField] CapsuleCollider2D colliderplayer;
    public float NearMissDistance;
    //gfx
    [SerializeField] GameObject[] trails;
    [SerializeField] Animator anim;
    [SerializeField] Animator animt;
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator ui;
    [SerializeField] SpriteRenderer gfx;
    [SerializeField] SpriteRenderer gfxshadow;
    [SerializeField] TextMeshProUGUI Velocity;
    [SerializeField] TextMeshProUGUI comboMultiplier;

    //flag
   public bool canMove = true;
    [SerializeField] bool rampjump;
    private void Start()
    {
        if (dbj == null)
        {
            dbj = FindObjectOfType<DirectionByJoy>();
        }
        rb = GetComponent<Rigidbody2D>();
        gfxOrgin = gfx.gameObject.transform.position;
    }
    void Update()
    {
        #region Velocity stuff

        veltext = Mathf.FloorToInt(-rb.velocity.y * 10);//simplify velocity 

        Velocity.text = veltext.ToString();

        #region velocity color 
        ui.SetInteger("vel", (int)veltext);

        #endregion
        #endregion

        if (joystick == null)
        {
            joystick = FindObjectOfType<Joystick>();
        }//find joystick if = null
        else
        {
            if (canMove) {
                #region Clear movespeed
                if (veltext < 1)
                {
                        newMoveSpeed = 0;
                }

                if (joystick.Vertical == 0 && joystick.Horizontal == 0)
                {
                    rb.drag = 2f; 
                }
                
                #endregion
                if (dbj.saValue == -1)
                {
                    newMoveSpeed = Mathf.Lerp(newMoveSpeed, moveSpeed, 0.01f);
                    if (joystick.Vertical < 0)
                    {
                        rb.drag = 0.2f;
                        newMoveSpeed = Mathf.Lerp(newMoveSpeed, moveSpeed, 0.01f);
                        rb.velocity = -MoveByDircetion.transform.up * Mathf.Abs(Mathf.Sign(joystick.Vertical)) * newMoveSpeed;
                    }
                    else if (joystick.Vertical > 0.9f)
                    {
                        rb.drag = 2f;
                        rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.01f);
                    }
                 
                }
                else if (dbj.saValue == 0)
                {
                    rb.velocity = Vector3.Lerp(-MoveByDircetion.transform.up * Mathf.Abs(Mathf.Sign(joystick.Vertical)) * newMoveSpeed, Vector3.zero, 0.1f);
                    rb.drag = 2;
                }
            }
            if (rampjump == false)
            {
                #region player anim
                float anglevalue = joystick.Horizontal * 100;

                anim.SetInteger("Angle", (int)anglevalue);
                animt.SetInteger("Angle", (int)anglevalue);
                if (anglevalue < 0)
                {
                    gfx.flipX = true;
                    gfxshadow.flipX = true;
                }
                else if (anglevalue > 0)
                {
                    gfx.flipX = false;
                    gfxshadow.flipX = false;
                }
                #endregion
            }
            else
            {
                anim.SetInteger("Angle", 0);
                animt.SetInteger("Angle", 0);
            }
            
            }
        if (rampjump)//throw ramp
        {
            rb.velocity = new Vector3(0, -15);
        }
        #region ScoreGFX
        comboMultiplier.text ="x"+pointsMultiplier.ToString();
        comboMultiplier.alpha = Mathf.Lerp(comboMultiplier.alpha,Mathf.Sign(timeToreset),0.01f);
        #endregion
    }
    private void FixedUpdate()
    {
        #region ScoreGFX
       
        timeToreset -= Time.deltaTime;
        if (timeToreset <= 0)
        {
            pointsMultiplier = 1;
        }
        #endregion
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("tree")) //here we can put custom logic what is going to do when player hit obstacle.
        {//custom logic

            accident(collision);
            pointsMultiplier = 1;
        }
        else if (collision.CompareTag("rock"))//here we can put custom logic what is going to do when player hit obstacle.
        {//custom logic 

            accident(collision);
            pointsMultiplier = 1;

        }
        else if (collision.CompareTag("ramp"))//here we can put custom logic what is going to do when player hit obstacle.
        {
            //Ramp logic

            colliderplayer.enabled = false;

            foreach (var element in trails)
            {
                element.GetComponent<TrailRenderer>().emitting = false;
            }
            //disattatch joy
            //add velocityt 
            rampjump = true;
            StartCoroutine(ramp());
            deliverPoints(5, 0);
        }
    }
        #region Score
        /// <summary>
        /// type= 0-Ramp, 1-Tree,2-Rock
        /// </summary>
       public void deliverPoints(int pointsAmount,int type)
        {
            
            switch(type){
                case 0:
                    pointsMultiplier += 2;
            break;
                case 1:
                    pointsMultiplier += 1;
            break;
                case 2:
                    pointsMultiplier += 1;
            break;
            }

            points += 5*pointsAmount;
            if (resetPMultiplier)
            {
                resetPMultiplier = false;
                
            }
        timeToreset =5;
        }
       
        #endregion

    IEnumerator ramp()
    {
        //disattatch joy
        //add velocityt 
        yield return new WaitForSeconds(0.1f);
        playerAnimator.SetTrigger("jr");

        yield return new WaitForSeconds(0.6f);
        playerAnimator.SetTrigger("jro");
        yield return new WaitForSeconds(0.2f);
        foreach (var element in trails)
        {
            element.GetComponent<TrailRenderer>().emitting = true;
        }
       
        colliderplayer.enabled = true;
        rampjump = false;
    }
    void accident(Collider2D collision)
    {
        playerAnimator.SetTrigger("BlinkAlpha");
        anim.SetTrigger("accident");
        animt.SetTrigger("accident");
        rb.drag = 4;
        canMove = false;
        rb.MovePosition(transform.position - collision.transform.up * 2);
        foreach (var element in trails)
        {
            element.GetComponent<TrailRenderer>().emitting = false;
        }
        Handheld.Vibrate();
        StartCoroutine(trunMoveOn());
    }
    IEnumerator trunMoveOn()
    {
        yield return new WaitForSeconds(3); 
        canMove = true;
        foreach (var element in trails)
        {
            element.GetComponent<TrailRenderer>().emitting = true;
        }
    }
}
