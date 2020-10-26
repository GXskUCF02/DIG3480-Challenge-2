using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public Text score;
    public Text lives;
    public Text winText;
    public Text loseText;
    private int livesValue =  3;
    private int scoreValue = 0;

    public AudioClip musicClipPlayer;

    public AudioSource musicSourcePlayer;
    
    public AudioClip musicClipPlayerWin;

    public AudioSource musicSourcePlayerWin;

    Animator anim;

    private bool facingRight = true;

    private bool isOnGround;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask allGround;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        score.text = "Coins Collected: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
        musicSourcePlayer.clip = musicClipPlayer;
        musicSourcePlayer.Play();
    }

    // Update is called once per frame
    
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        
        isOnGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, allGround);

        if (scoreValue == 5)
            {
                musicSourcePlayer.Stop();
                winText.text = "You won the Game! Game by Gage Schroeder";
                musicSourcePlayerWin.clip = musicClipPlayerWin;
                musicSourcePlayerWin.Play();
            }

        if (livesValue == 0)
        {
            Destroy(gameObject);
            loseText.text = "You lost the game...";
        }
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit(); 
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Coins Collected: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }

        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3),ForceMode2D.Impulse);
                anim.SetInteger("State", 0);
            }
        }
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
   

}
