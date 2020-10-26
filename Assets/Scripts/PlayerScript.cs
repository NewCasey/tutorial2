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
    public Text end;
    private int scoreValue = 0;
    private int livesValue = 3;
    public bool level = false;
    public AudioSource musicSource;
    public AudioClip music1;
    public bool playingMusic1 = false;
    public AudioClip victory;
    public bool playingVictory = false;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
        end.text = "";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * 0));
        if (Input.GetKey("escape"))
            {
            Application.Quit();
            }
    }

    void Update(){
        if (scoreValue < 8 && playingMusic1 == false){
            musicSource.clip = music1;
            musicSource.Play();
            musicSource.loop = true;
            playingMusic1 = true;
        }
        if (scoreValue == 8 && playingVictory == false){
            musicSource.clip = victory;
            musicSource.Play();
            musicSource.loop = false;
            playingVictory = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (scoreValue == 8)
        {
            end.text = "You win! (Game created by Casey Newdorf)";
        }
        if (livesValue == 0){
            end.text = "You lose! Try again.";
        }
        if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (scoreValue == 4 && level == false){
            transform.position = new Vector2(00.0f, 50.0f);
            livesValue = 3;
            lives.text = "Lives: " + livesValue.ToString();
            level = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
            }
        }
    }
}