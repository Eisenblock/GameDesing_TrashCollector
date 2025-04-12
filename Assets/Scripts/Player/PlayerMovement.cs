using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private float moveInput;
    private float Stance_pos = 0;
    public float life = 3;
    private float score = 0;
    public TMP_Text LifeValue;
    public TMP_Text ScoreValue;
    public float timer = 0.5f;
    private bool timerActive = false;
    private float rotateValue = 120;
    private bool gotHit = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.E) && !timerActive)
        {
            Stance_pos = (Stance_pos + 1) % 3;
            Debug.Log("Stance: " + Stance_pos);
            timerActive = true;
        }

        if (!timerActive && !gotHit)
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
        }
        if (timerActive)
        {
            timer -= Time.deltaTime;
            Debug.Log("Timer: " + timer.ToString("F2"));
            transform.Rotate(0, 0, 240 * Time.deltaTime);

            if (timer <= 0)
            {
                timer = 0.5f;
                timerActive = false;
                Debug.Log("Timer abgelaufen!");
            }
        }

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
        switch (Stance_pos)
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
        }
    }


    void ResetColorBool()
    {
        gotHit = false;
    }
}
