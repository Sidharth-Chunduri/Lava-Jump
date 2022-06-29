using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //instatiates variables that can be tweaked to adjust the game
    public float moveSpeed;
    private Rigidbody2D rb;
    public float smooth = 1f;
    private Quaternion targetRotation;
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
    public GameManager gameManager;
    public CameraShake cameraShake;
    public GameObject heart1;
    public GameObject heart2;
    public GameObject heart3;

    //initializes the physics engine of the player
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //sets the player to be able to move at the start of the game
    private void Start()
    {
        isMovable = true;
    }
    
    //runs every frame to execute repeated actions
    void Update()
    {
        //sets up controls for both players
        if (isPlayer1)
        {
            moveDirection = Input.GetAxis("Vertical");
        }
        else
        {
            moveDirection = Input.GetAxis("Horizontal");
        }

        //jumping for both players
        if (isMovable)
        {
            rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
            if(isPlayer1){
                //jumps using space if the player is the main player
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
            }
            else{
                //jumps using control if the player is the secondary player
                if (Input.GetKeyDown(KeyCode.RightControl))
                {
                    Jump();
                }
            }
        }

        //If the players exits the boundaries of the screen, this moves them to the other side of the screen
        if (rb.transform.position.x > 36.5)
        {
            rb.transform.position = new Vector2(-36.5f, rb.transform.position.y);
        }
        else if (rb.transform.position.x < -36.5)
        {
            rb.transform.position = new Vector2(36.5f, rb.transform.position.y);
        }
    }

    //collision detection for various objects with the players
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //when colliding with a fireball, one life is subtracted and the hearts are displayed accordingly
        if (collision.gameObject.tag == "bullet")
        {
            //subtracts a life
            lives--;
            //sets the number of hearts when lives are 3
            if(lives == 3)
            {
                heart1.SetActive(false);
                hit.Play();
                StartCoroutine(Stun(playerStun));
                StartCoroutine(cameraShake.Shake(.15f, deathShakeMagnitude));
                collision.gameObject.SetActive(false);
            }
            //sets the number of hearts when lives are 2
            else if (lives == 2)
            {
                heart1.SetActive(false);
                heart2.SetActive(false);
                hit.Play();
                StartCoroutine(Stun(playerStun));
                StartCoroutine(cameraShake.Shake(.15f, deathShakeMagnitude));
                collision.gameObject.SetActive(false);
            }
            //sets the number of hearts when lives are 1
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
            //ends the game and displays some text when the player is out of lives
            else if (lives == 0)
            {
                gameManager.GameOver();
            }
        }
        //when a coin is touched, score is added to the player
        else if (collision.gameObject.tag == "coin")
        {
            //adds score to the player
            gameManager.CoinCollected();
            coinPickup.Play();
            //hides the coin for some time so it doesn't get collected repeatedly
            StartCoroutine(ShowAndHide(collision.gameObject, respawnTime));
        }
        else if(collision.gameObject.tag == "oneup"){
            if(lives < 4){
            lives +=1;
            if(lives == 2){
                heart3.SetActive(true);
            }
            else if(lives == 3){
                heart3.SetActive(true);
                heart2.SetActive(true);
            }
            else if(lives == 4){
                heart3.SetActive(true);
                heart2.SetActive(true);
                heart1.SetActive(true);
            }
            Debug.Log(lives);
            }
            collision.gameObject.SetActive(false);
            
        }
    }

    //When a coin is picked up, it is made unable to be picked up again for a set interval
    IEnumerator ShowAndHide(GameObject coin, float delay)
    {
        coin.SetActive(false);
        yield return new WaitForSeconds(delay);
        coin.SetActive(true);
    }

    //Stuns the players when they are hit
    IEnumerator Stun(float delay)
    {
        isMovable = false;
        yield return new WaitForSeconds(delay);
        isMovable = true;
    }

    //As used in the update loop, this function makes the player jump when a user input is given
    private void Jump(){
        //locally reverses the gravity of the player
        rb.gravityScale = rb.gravityScale * -1;
        jump.Play();
        //uses quaternions to rotate the player sprite to make it seem like they are upside down
        targetRotation = Quaternion.AngleAxis(180.0f, transform.forward) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation,targetRotation, smooth * Time.deltaTime);
        //shakes the camera for visual effect
        StartCoroutine(cameraShake.Shake(.15f, jumpShakeMagnitude));
    }

}
