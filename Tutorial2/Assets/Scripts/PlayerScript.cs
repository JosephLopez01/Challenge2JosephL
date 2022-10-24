using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI LivesText;

    public GameObject winTextObject;
    public GameObject loseTextObject;

    private int score;
    private int lives;

    public AudioClip gameAudio;

    public AudioClip winMusic;

    public AudioSource musicSource;

    private bool facingRight = true;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();

        lives = 3;
        score = 0;

        SetScoreText();
        SetLivesText();

        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        musicSource.clip = gameAudio;
        musicSource.Play();
        musicSource.loop = true;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
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
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.collider.tag == "Coin"){
            score += 1;
            SetScoreText();
            Destroy(collision.collider.gameObject);
        }
        if(collision.collider.tag == "Enemy"){
            lives -= 1;
            SetLivesText();
            Destroy(collision.collider.gameObject);
        }
        if(score == 4){
            transform.position = new Vector2(0.0f, -31.59f);
            lives = 3;
            SetLivesText();
        }
    }

    private void OnCollisionStay2D(Collision2D collision){
        if(collision.collider.tag == "Ground"){
            if(Input.GetKey(KeyCode.W)){
                rd2d.AddForce(new Vector2(0,2), ForceMode2D.Impulse);
                anim.SetInteger("State", 0);
            }
        }
    }

    void SetScoreText()
    {
        ScoreText.text = "Score: " + score.ToString();
        if(score >= 9){
            winTextObject.SetActive(true);
            speed = 0;
            musicSource.loop = false;
            musicSource.clip = winMusic;
            musicSource.Play();
        }
    }

    void SetLivesText()
    {
        LivesText.text = "Lives: " + lives.ToString();
        if(lives < 1){
            loseTextObject.SetActive(true);
            speed = 0;
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
