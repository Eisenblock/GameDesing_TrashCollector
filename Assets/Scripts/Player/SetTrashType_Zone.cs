using Unity.VisualScripting;
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
    private Color current_Color = Color.white;
    private Color startColor = Color.white;
    private float resetTimeColor = 0.3f ;
    private bool Isimmun = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerObject = player.GetComponent<PlayerMovement>();
        
        /*if (blueTrash)
        {
            current_Color = Color.blue;
            startColor = Color.blue;
        }


        if (yellowTrash)
        {
            current_Color = Color.yellow;
            startColor = Color.yellow;
        }


        if (greenTrash)
        {
            current_Color = Color.green;
            startColor = Color.green;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        //DO Color
        //this.GetComponent<SpriteRenderer>().color = current_Color;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (blueTrash && !gotHit)
        {
            Debug.Log("Stance 0: Metall");
            Debug.Log("Kollision mit: " + collision.gameObject.name + ", Tag: " + collision.gameObject.tag);
            if (collision.gameObject.CompareTag("Blue"))
            {
                Destroy(collision.gameObject);
                playerObject.score += 100;
                playerObject.GetEnergy(1);
                gotHit = true;
            }
            if (collision.gameObject.CompareTag("Yellow") || collision.gameObject.CompareTag("Gr�n"))
            {
                Destroy(collision.gameObject);
                playerObject.takeDamage();
                current_Color = Color.red;
                Invoke("ResetColorBool", resetTimeColor);
                gotHit = true;
            }
            if (collision.gameObject.CompareTag("asteroid"))
            {
                Destroy(collision.gameObject);
                playerObject.takeDamage();
                current_Color = Color.red;
                Invoke("ResetColorBool", resetTimeColor);
                gotHit = true;
            }
            if (collision.gameObject.CompareTag("shield"))
            {
                playerObject.Shield();
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.CompareTag("speedUp"))
            {
                playerObject.Shield();
                playerObject.FastTravel();
                Destroy(collision.gameObject);
            }
        }

        if (greenTrash && !gotHit)
        {
            Debug.Log("Stance 1: Plastik");
            Debug.Log("Kollision mit: " + collision.gameObject.name + ", Tag: " + collision.gameObject.tag);
            if (collision.gameObject.CompareTag("Gr�n"))
            {
                Destroy(collision.gameObject);
                playerObject.score += 100;
                playerObject.GetEnergy(1);
                gotHit = true;
            }
            if (!Isimmun) 
            { 
                if (collision.gameObject.CompareTag("Yellow") || collision.gameObject.CompareTag("Blue"))
                {
                    Destroy(collision.gameObject);
                    playerObject.takeDamage();
                    current_Color = Color.red;
                    //  gotHit = true;
                    Invoke("ResetColorBool", resetTimeColor);
                    gotHit = true;
                }

                if (collision.gameObject.CompareTag("asteroid"))
                {
                    Destroy(collision.gameObject);
                    playerObject.takeDamage();
                    current_Color = Color.red;
                    Invoke("ResetColorBool", resetTimeColor);
                    gotHit = true;
                }
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
                playerObject.GetEnergy(1);
                gotHit = true;
            }

            if (!Isimmun)
            {

                if (collision.gameObject.CompareTag("Gr�n"))
                {
                    Destroy(collision.gameObject);
                    playerObject.takeDamage();
                    current_Color = Color.red;
                    Invoke("ResetColorBool", resetTimeColor);
                    gotHit = true;
                }
                else if (collision.gameObject.CompareTag("Blue"))
                {
                    Destroy(collision.gameObject);
                    playerObject.takeDamage();
                    current_Color = Color.red;
                    Invoke("ResetColorBool", resetTimeColor);
                    gotHit = true;
                }

                if (collision.gameObject.CompareTag("asteroid"))
                {
                    Destroy(collision.gameObject);
                    playerObject.takeDamage();
                    current_Color = Color.red;
                    Invoke("ResetColorBool", resetTimeColor);
                    gotHit = true;
                }
            }
        }

        if (normalShippiece && !gotHit && !Isimmun)
        {
            Destroy(collision.gameObject);
            playerObject.takeDamage();
            current_Color = Color.red;
            //  gotHit = true;
            Invoke("ResetColorBool", resetTimeColor);
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

    void ResetColorBool()
    {
        current_Color = startColor; 
    }
}
   