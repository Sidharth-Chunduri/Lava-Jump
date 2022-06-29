using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementLVL3 : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody2D rb;
    public float smooth = 1f;
    private Quaternion targetRotation;
    //private bool facingRight = true;
    private float moveDirection;
    public float respawnTime;
    public float lives = 4;
    public float jumpShakeMagnitude;
    public float deathShakeMagnitude;
    public bool isMovable;
    public bool isPlayer1;
    public float playerStun;
    public AudioSource coinPickup;
    public AudioSource jump;
    public AudioSource hit;
    public GameManagerLVL3 gameManager;
    public CameraShake cameraShake;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        isMovable = true;
    }

    void Update()
    {
        if (isPlayer1)
        {
            moveDirection = Input.GetAxis("Vertical");
        }
        else
        {
            moveDirection = Input.GetAxis("Horizontal");
        }

        if (isMovable)
        {
            rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
            if(isPlayer1){
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.gravityScale = rb.gravityScale * -1;
                    jump.Play();
                    targetRotation = Quaternion.AngleAxis(180.0f, transform.forward) * transform.rotation;
                    transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation, smooth * Time.deltaTime);
                    StartCoroutine(cameraShake.Shake(.15f, jumpShakeMagnitude));
                }
            }
            else{
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    rb.gravityScale = rb.gravityScale * -1;
                    jump.Play();
                    targetRotation = Quaternion.AngleAxis(180.0f, transform.forward) * transform.rotation;
                    transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation, smooth * Time.deltaTime);
                    StartCoroutine(cameraShake.Shake(.15f, jumpShakeMagnitude));
                }
            }
        }
 
        if (rb.transform.position.x > 36.5)
        {
            rb.transform.position = new Vector2(-36.5f, rb.transform.position.y);
        }
        else if (rb.transform.position.x < -36.5)
        {
            rb.transform.position = new Vector2(36.5f, rb.transform.position.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            lives--;
            if(lives == 3)
            {
                /*ParticleSystem part = heart1.GetComponent<ParticleSystem>();
                part.Play();
                Destroy(heart1, part.main.duration);*/
                heart1.SetActive(false);
                hit.Play();
                StartCoroutine(Stun(playerStun));
                StartCoroutine(cameraShake.Shake(.15f, deathShakeMagnitude));
                collision.gameObject.SetActive(false);
            }
            else if (lives == 2)
            {
                heart1.SetActive(false);
                heart2.SetActive(false);
                hit.Play();
                StartCoroutine(Stun(playerStun));
                StartCoroutine(cameraShake.Shake(.15f, deathShakeMagnitude));
                collision.gameObject.SetActive(false);
            }
            else if (lives == 1)
            {
                heart1.SetActive(false);
                heart2.SetActive(false);
                heart3.SetActive(false);
                hit.Play();
                StartCoroutine(Stun(playerStun));
                StartCoroutine(cameraShake.Shake(.15f, deathShakeMagnitude));
                collision.gameObject.SetActive(false);
            }
            else if (lives == 0)
            {
                PlayerPrefs.SetFloat("FinalScore", 0);
                gameManager.GameOver();
            }
        }
        else if (collision.gameObject.tag == "coin")
        {
            gameManager.CoinCollected();
            coinPickup.Play();
            StartCoroutine(ShowAndHide(collision.gameObject, respawnTime));
        }
    }

    IEnumerator ShowAndHide(GameObject coin, float delay)
    {
        coin.SetActive(false);
        yield return new WaitForSeconds(delay);
        coin.SetActive(true);
    }

    IEnumerator Stun(float delay)
    {
        isMovable = false;
        yield return new WaitForSeconds(delay);
        isMovable = true;
    }

    /*public void OneUp()
    {
        if (lives <= 3)
        {
            lives += 1;
        }
    }*/

}