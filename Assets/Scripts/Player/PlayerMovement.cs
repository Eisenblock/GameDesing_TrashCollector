using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private float moveInput;
    private float Stance_pos = 0;
    public float life = 3;
    public float score = 0;
    public TMP_Text LifeValue;
    public TMP_Text ScoreValue;
    public float timer = 0.5f;
    private bool timerActive = false;
    private float rotateValue = 120;
    private bool gotHit = false;
    private SpriteRenderer sr;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = 0.5f;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.E) )
        {
            Stance_pos = (Stance_pos + 1) % 3;
            Debug.Log("Stance: " + Stance_pos);
            switch (Stance_pos)
            {
                case 1:
                    transform.DORotate(new Vector3(0, 0, -150f), 0.2f);  // Korrekt: schließende Klammer und Komma
                    break;

                case 2:
                    transform.DORotate(new Vector3(0, 0, -280f), 0.2f); // Korrekt: schließende Klammer und Komma
                    break;

                case 0:
                    transform.DORotate(new Vector3(0, 0, -30f), 0.2f);  // Korrekt: schließende Klammer und Komma
                    break;
            }
        }
   

        /*  if (!timerActive && !gotHit)
          {
              switch (Stance_pos)
              {
                  case 0:
                      this.GetComponent<SpriteRenderer>().color = Color.blue;
                      break;

                  case 1:
                      this.GetComponent<SpriteRenderer>().color = Color.green;
                      break;

                  case 2:
                      this.GetComponent<SpriteRenderer>().color = Color.yellow;
                      break;
              }
          }*/


        if (LifeValue != null)
        {
            LifeValue.text = life.ToString();
        }

        if (ScoreValue != null)
        {
            ScoreValue.text = score.ToString();
        }

        if (life <= 0)
        {
            Destroy(this);
            SceneManager.LoadScene("MainMenu");
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*switch (Stance_pos)
        {
            case 0:
                Debug.Log("Stance 0: Metall");
                Debug.Log("Kollision mit: " + collision.gameObject.name + ", Tag: " + collision.gameObject.tag);
                if (collision.gameObject.CompareTag("Blue"))
                {
                    Destroy(collision.gameObject);
                    score += 100;
                }
                else
                {
                    Destroy(collision.gameObject);
                    life -= 1;
                    this.GetComponent<SpriteRenderer>().color = Color.red;
                    gotHit = true;
                    Invoke("ResetColorBool", 0.2f);
                }
                break;

            case 1:
                Debug.Log("Stance 1: Plastik");
                Debug.Log("Kollision mit: " + collision.gameObject.name + ", Tag: " + collision.gameObject.tag);
                if (collision.gameObject.CompareTag("Grün"))
                {
                    Destroy(collision.gameObject);
                    score += 100;
                }
                else
                {
                    Destroy(collision.gameObject);
                    life -= 1;
                    this.GetComponent<SpriteRenderer>().color = Color.red;
                    gotHit = true;
                    Invoke("ResetColorBool", 0.2f);
                }
                break;
                

            case 2:
                Debug.Log("Stance 2: Rest");
                Debug.Log("Kollision mit: " + collision.gameObject.name + ", Tag: " + collision.gameObject.tag);
                if (collision.gameObject.CompareTag("Yellow"))
                {
                    Destroy(collision.gameObject);
                    score += 100;
                }
                else
                {
                    Destroy(collision.gameObject);
                    life -= 1;
                    this.GetComponent<SpriteRenderer>().color = Color.red;
                    gotHit = true;
                    Invoke("ResetColorBool", 0.2f);
                }
                break;
        }*/
    }


    void ResetColorBool()
    {
        gotHit = false;
    }

    public void takeDamage()
    {
        life -= 1;
        this.GetComponent<SpriteRenderer>().color = Color.red;
        sr.DOColor(Color.white, 0.2f);

    }
}
