using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript: MonoBehaviour 
{
  private Rigidbody2D rd2d;
  public float speed;
  public Text scoreText;
  private int scoreValue;
  public Text livesText;
  private int lives;
  public GameObject winTextObject;
  public GameObject youLoseObject;
  private bool isGameOver;
  private bool isGameReallyOver;
  private bool isGameActuallyOver;
  public AudioClip musicClipOne;
  public AudioClip musicClipTwo;
  public AudioSource musicSource;
  Animator anim;
  void Start() 
  {
    rd2d = GetComponent < Rigidbody2D > ();
    scoreValue = 0;
    lives = 3;
    winTextObject.SetActive(false);
    youLoseObject.SetActive(false);
    isGameOver = false;
    isGameReallyOver = false;
    isGameActuallyOver = false;
    anim = GetComponent<Animator>();
  }
  void FixedUpdate() 
  {
    float hozMovement = Input.GetAxis("Horizontal");
    float verMovement = Input.GetAxis("Vertical");
    rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
  }
  private void OnCollisionEnter2D(Collision2D collision) 
  {
    if (collision.collider.tag == "Coin") 
    {
      scoreValue += 1;
      Destroy(collision.collider.gameObject);
    } 
    if (collision.collider.tag == "Enemy") 
    {
      lives -=1;
      Destroy(collision.collider.gameObject);
    }

  }
  void Update() 
  {
    scoreText.text = "Score: " + scoreValue.ToString();
      if(scoreValue >=8)
      {
        winTextObject.SetActive(true);
      }
    livesText.text = "Lives: " + lives.ToString();
      if (lives <=0)
      {
      Destroy(gameObject);
      youLoseObject.SetActive(true);
      }
      if (scoreValue == 8 && !isGameReallyOver)
      {
        isGameReallyOver = true;
        musicSource.clip = musicClipOne;
        musicSource.Play();

      }
      if (lives <= 0 && !isGameActuallyOver)
      {
        isGameActuallyOver = true;
        musicSource.clip = musicClipTwo;
        musicSource.Play();
      }
  }
  private void OnCollisionStay2D(Collision2D collision) 
  {
    if (collision.collider.tag == "Ground") 
    {
      if (Input.GetKey(KeyCode.W)) 
      {
        rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
      }
    }
    if (Input.GetKey("escape")) 
    {
      Application.Quit();
    }
    if (scoreValue == 4 && !isGameOver)
    { 
    isGameOver = true;
    transform.position = new Vector2(80.0f, 1.0f);
    lives =3;
    }
}
}