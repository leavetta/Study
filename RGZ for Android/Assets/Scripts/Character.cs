using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : Unit
{
    public float speed;
    public float normalSpeed;

    public float jumpForce;
    private float moveInput;
    public float health;
    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public float heal;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;



    private Rigidbody2D rig;
    private Animator anim;
    public VectorValue pos;
    private void Start()
    {
        transform.position = pos.initalValue;
        speed = 0f;
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(health > numOfHearts)
        {
            health = numOfHearts;
        }
        health += Time.deltaTime * heal;
        for(int i = 0; i < hearts.Length; i++)
        {
            if(i < Mathf.RoundToInt(health))
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
            if(health < 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        rig.velocity = new Vector2(speed, rig.velocity.y);
        if(speed != 0)
        {
            anim.SetBool("isRunning", true);
        }
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);

        

        if(isGrounded == true)
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }
    }
    public override void ReceiveDamage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnJumpButtonDown()
    {
        if (isGrounded == true)
        {
            rig.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("takeOf");
        }
    }
    public void OnLeftButtonDown()
    {
        if(speed >= 0f)
        {
            speed = -normalSpeed;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    public void OnRightButtonDown()
    {
        if (speed <= 0f)
        {
            speed = normalSpeed;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
    public void OnButtonUp()
    {
        speed = 0f;
        anim.SetBool("isRunning", false);
    }

    public void ChangeHealth(int healthValue)
    {
        health += healthValue;
    }
    /*void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

        /*if(moveInput < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (moveInput > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }*/
}

