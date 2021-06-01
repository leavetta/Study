using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : Unit
{
    public float offset;
    public float speed;
    public float jumpForce;
    public float moveInput;
    private Rigidbody2D rig;
    private Color bulletColor = Color.gray;

    public Text scoreDisplay;
    public int allCoins;
    public int score = 0;
    public GameObject teleport;

    private Bullet bullet;
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private Animator anim;

    public GameObject sound;
    public GameObject effect;
    private SpriteRenderer sprite;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        bullet = Resources.Load<Bullet>("Bullet");
    }
    private void Update()
    {
        scoreDisplay.text = score.ToString() + "/" + allCoins.ToString();
        if (Input.GetButtonDown("Fire1")) Shoot();
        if (Input.GetButton("Horizontal")) Run();
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
        if (Input.GetKey("escape")) 
        {
            Application.Quit(); 
        }
        
        if (isGrounded == true)
        {
            anim.SetBool("isJumping", false);
        }
        else
        {
            anim.SetBool("isJumping", true);
        }
    }

    private void Run()
    {
        moveInput = Input.GetAxis("Horizontal");
        Vector3 direction = transform.right * moveInput;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0.0F;
        
        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }
    }

    private void Jump()
    {
        Instantiate(sound, transform.position, Quaternion.identity);
        Instantiate(effect, transform.position, Quaternion.identity);

        rig.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        anim.SetTrigger("takeOf");
    }

    private void Shoot()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        Vector3 position = transform.position;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation);

        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
        newBullet.Color = bulletColor;
    }
    public override void ReceiveDamage()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Coin")
        {
            Destroy(col.gameObject);
            score++;
        }
        if(score == allCoins)
        {
            teleport.SetActive(true);
        }
    }

    public void OnMenuButtonDown()
    {
        SceneManager.LoadScene(11);
    }
}
