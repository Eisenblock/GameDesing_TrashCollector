using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    public GameObject lifeIconPrefab;   // Dein Herz-Image Prefab
    public GameObject nolifeIconPrefab;
    public Transform lifeBox;
    GameManagerGlobal gm;
    public GameObject ship;
    private GameObject globalObject;
    private Color current_color = Color.white;
    public float moveSpeed = 5f;
    public float StartSpeed = 1.0f;
    public float increasedSpeed = 10f;
    public float timerMoreSpeed = 3f;
    private float speedTimer = 0f;
    private bool speedIncreased = true;
    private Rigidbody2D rb;
    private float moveInput;
    private float Stance_pos = 0;
    public float life = 15;
    public float score = 0;
    public TMP_Text LifeValue;
    public float timer = 0.5f;
    private bool timerActive = false;
    public float rotateValue = 5;
    private bool gotHit = false;
    private bool blockRotate = false;
    //SHieldValues
    private bool IsShielded = false;
    private float shield_duration = 0;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer = 0.5f;
        StartSpeed = moveSpeed;
        globalObject = GameObject.FindWithTag("GlobalManager");
        gm = globalObject.GetComponent<GameManagerGlobal>();
    }

    // Update is called once per frame
    void Update()
    {
        if (life >= 6)
        {
            life = 6;
        }
        UpdateLifeDisplay();
        if (ship != null) 
        { 
            ship.GetComponent<SpriteRenderer>().color = current_color;
        }

        if (IsShielded)
        {
            shield_duration -= Time.deltaTime;
            if (shield_duration < 0)
            {
                IsShielded=false;
            }
        }
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
      /*  if (moveInput == 0)
        {
            moveSpeed = StartSpeed;
            Debug.Log("Geschwindigkeit erhöht!" + moveSpeed);
        }
        else
        {
            speedIncreased = false;
            MoreSPeedOverTime();
        }*/

        if (Input.GetKeyDown(KeyCode.E) && !blockRotate)
        {
            Stance_pos = (Stance_pos + 1) % 3;
            Debug.Log("Stance (E): " + Stance_pos);
            transform.DORotate(new Vector3(0, 0, -120f), 0.2f, RotateMode.LocalAxisAdd);
            Invoke("ResetRotate", 0.2f);
            blockRotate = true;
        }

        if (Input.GetKeyDown(KeyCode.Q) && !blockRotate)
        {
            Stance_pos = (Stance_pos - 1 + 3) % 3;  // Wichtig: +3 verhindert negative Werte bei -1
            Debug.Log("Stance (Q): " + Stance_pos);
            transform.DORotate(new Vector3(0, 0, 120f), 0.2f, RotateMode.LocalAxisAdd);
            Invoke("ResetRotate", 0.2f);
            blockRotate = true;
        }/*
        if (Input.GetKey(KeyCode.E) )
        {
            Stance_pos = (Stance_pos + 1) % 3;
            transform.Rotate(0, 0, -rotateValue * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Q))
        {
            Stance_pos = (Stance_pos - 1 + 3) % 3;
            transform.Rotate(0, 0, rotateValue * Time.deltaTime);
        }*/

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

        /*if (ScoreValue != null)
        {
            if (gm.playerScore != 0) 
            {
                ScoreValue.text = gm.playerScore.ToString();
            }
        }*/


        if (life <= 0)
        {
            gm.gameOver = true;
            Destroy(this);
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        }

        if (!IsShielded) 
        {
            LostEnergyOverTime();
        }
        
    }

    public void  MoreSPeedOverTime()
    {
        
        speedTimer += Time.deltaTime;

       
        if (speedTimer <= timerMoreSpeed && !speedIncreased)
        {
            moveSpeed += increasedSpeed * Time.deltaTime;
            Debug.Log("Geschwindigkeit erhöht!" + moveSpeed);
            
        }
        if (speedTimer >= timerMoreSpeed && !speedIncreased)
        {
            speedIncreased = true;
            Debug.Log("Geschwindigkeit erhöht!" + moveSpeed);
            speedTimer = 0;
        }



    }

    void RotateStance()
    {
        switch (Stance_pos)
        {
            case 0:
                transform.DORotate(new Vector3(0, 0, -30f), 0.2f);
                break;
            case 1:
                transform.DORotate(new Vector3(0, 0, -150f), 0.2f);
                break;
            case 2:
                transform.DORotate(new Vector3(0, 0, -280f), 0.2f);
                break;
        }
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
        if (!IsShielded)
        {
            life -= 1;
            current_color = Color.red;
            Invoke("ResetColor", 0.3f);
        }
    }
    
    public void LostEnergyOverTime()
    {
        life -= 0.3f * Time.deltaTime;
    }

    public void GetEnergy(float energy_amount)
    {
        life += energy_amount;
    }

    private void ResetRotate()
    {
        blockRotate = false;
    }

    private void ResetColor()
    {
        current_color = Color.white;
    }

    public void UpdateLifeDisplay()
    {
        foreach (Transform child in lifeBox)
        {
            Destroy(child.gameObject);
        }

        // Neue Icons spawnen
        for (int i = 0; i < 6; i++)
        {
            if (i < life)
            {
                Instantiate(lifeIconPrefab, lifeBox).transform.localScale = Vector3.one;
            }
            else
            {
                Instantiate(nolifeIconPrefab, lifeBox).transform.localScale = Vector3.one;
            }
        }
    }

    public void Shield()
    {
        IsShielded = true;
        shield_duration = 5;
        current_color = Color.blue;
        Invoke("ResetColor", 5.0f);
    }

    public void FastTravel()
    {
        GameObject[] backgrounds = GameObject.FindGameObjectsWithTag("background");
        Debug.Log("FastTravel wird ausgeführt.");

        if (backgrounds == null || backgrounds.Length == 0)
        {
            Debug.LogWarning("Keine Objekte mit dem Tag 'Background' gefunden.");
            return;
        }

        foreach (GameObject obj in backgrounds)
        {
            moveDownBackground bgScript = obj.GetComponent<moveDownBackground>();
            if (bgScript != null)
            {
                bgScript.SpeedUp();
            }
            else
            {
                Debug.LogWarning($"Kein moveDownBackground-Script gefunden an Objekt: {obj.name}");
            }
        }
    }
}
