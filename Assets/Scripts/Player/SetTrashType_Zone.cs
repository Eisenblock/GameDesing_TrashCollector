using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SetTrashType_Zone : MonoBehaviour
{
    public GameObject player;
    public bool blueTrash = false;
    public bool yellowTrash = false;
    public bool greenTrash = false;
    public bool normalShippiece = false;
    PlayerMovement playerObject;
    public bool gotHit = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (blueTrash)
        {
            this.GetComponent<SpriteRenderer>().color = Color.blue;
        }


        if (yellowTrash)
        {
            this.GetComponent<SpriteRenderer>().color = Color.yellow;
        }


        if (greenTrash)
        {
            this.GetComponent<SpriteRenderer>().color = Color.green;
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
        if (blueTrash && !gotHit)
        {
            Debug.Log("Stance 0: Metall");
            Debug.Log("Kollision mit: " + collision.gameObject.name + ", Tag: " + collision.gameObject.tag);
            if (collision.gameObject.CompareTag("Blue"))
            {
                Destroy(collision.gameObject);
                playerObject.score += 100;
                gotHit = true;
            }
            else
            {
                Destroy(collision.gameObject);
                playerObject.takeDamage();
                this.GetComponent<SpriteRenderer>().color = Color.red;
                Invoke("ResetColorBool", 0.2f);
                gotHit = true;
            }
        }

        if (greenTrash && !gotHit)
        {
            Debug.Log("Stance 1: Plastik");
            Debug.Log("Kollision mit: " + collision.gameObject.name + ", Tag: " + collision.gameObject.tag);
            if (collision.gameObject.CompareTag("Grün"))
            {
                Destroy(collision.gameObject);
                playerObject.score += 100;
                gotHit = true;
            }
            else
            {
                Destroy(collision.gameObject);
                playerObject.takeDamage();
                this.GetComponent<SpriteRenderer>().color = Color.red;
              //  gotHit = true;
                Invoke("ResetColorBool", 0.2f);
                gotHit = true;
            }
        }

        if (yellowTrash && !gotHit)
        {
            Debug.Log("Stance 2: Rest");
            Debug.Log("Kollision mit: " + collision.gameObject.name + ", Tag: " + collision.gameObject.tag);
            if (collision.gameObject.CompareTag("Yellow"))
            {
                Destroy(collision.gameObject);
              playerObject.score += 100;
                gotHit = true;
            }
            else
            {
                Destroy(collision.gameObject);
                playerObject.takeDamage();
                this.GetComponent<SpriteRenderer>().color = Color.red;
             //   gotHit = true;
                Invoke("ResetColorBool", 0.2f);
                gotHit = true;
            }
        }

        if (normalShippiece && !gotHit)
        {
            Destroy(collision.gameObject);
            playerObject.life -= 1;
            this.GetComponent<SpriteRenderer>().color = Color.red;
          //  gotHit = true;
            Invoke("ResetColorBool", 0.2f);
            gotHit = true;
        }

        Invoke("ResetGotHIt",0.2f);
    }

    void ResetGotHIt()
    {
        if (gotHit)
        {
            gotHit = false;
        }
    }
}
