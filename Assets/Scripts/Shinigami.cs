using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shinigami : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    [SerializeField]
    private float movementSpeed = 0.0f;
    private Animator myAnimator;
    private bool attack;
    private bool facingRight;
    private bool isDead;
    [SerializeField]
    private Transform[] groundPoints = null;
    [SerializeField]
    private float groundRadius = 0;
    private bool isGrounded;
    private bool jump;
    [SerializeField]
    private float jumpForce = 0.0f;
    [SerializeField]
    private bool airControl = false;
    [SerializeField]
    private GameObject projectile = null;
    [SerializeField]
    private Transform spawnPoint = null;
    [SerializeField]
    private LayerMask whatIsGround;
    public static bool isCutscene = false;
    public GameObject gameOverPanel;

    public static int ascensionPoints, descensionPoints;

  

    // Start is called before the first frame update
    void Start()
    {
        ascensionPoints = 0;
        descensionPoints = 0;
        facingRight = true;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();

    }
    void Update()
    {
        if (!isCutscene)
        {
            handleInput();
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isCutscene)
        {
            float horizontal = Input.GetAxis("Horizontal");
            isGrounded = isGroundedCheck();
            handleMovement(horizontal);
            flip(horizontal);
            handleAttacks();
            handleLayers();
            resetValues();
        }
    }
    private void handleMovement(float horizontal)
    {
        if (myRigidbody.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }
        if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && (isGrounded || airControl) && !isDead)
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y);
        }
        if (isGrounded && jump && !isDead)
        {
            isGrounded = false;
            myRigidbody.AddForce(new Vector2(0, jumpForce));
            myAnimator.SetTrigger("jump");
            myAnimator.SetBool("land", false);
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }
    private void handleAttacks()
    {
        if (attack && isGrounded && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myAnimator.SetTrigger("attack");
            myRigidbody.velocity = Vector2.zero;
            // Create the Bullet from the Bullet Prefab
            GameObject bullet = Instantiate(
                projectile,
                spawnPoint.position,
                spawnPoint.rotation) as GameObject;

            // Add velocity to the bullet
            Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
            if (facingRight)
            {
                rigidbody2D.velocity = new Vector2(15, 0);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(-15, 0);
            }
            // Destroy the bullet after 2 seconds
            Destroy(bullet, 1.0f);
        }
    }
    private void handleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }
    private void flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight && !isDead)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
    private void resetValues()
    {
        attack = false;
        jump = false;
        //  isDead = false;
    }
    private bool isGroundedCheck()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                foreach (Collider2D collider in colliders)
                {
                    if (collider.gameObject != gameObject)
                    {
                        myAnimator.ResetTrigger("jump");

                        return true;
                    }
                }
            }
        }
        return false;
    }
    private void handleLayers()
    {
        if (!isGrounded)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag == "DeathTrap" || collision.gameObject.tag == "Enemy") && !isDead)
        {
            isDead = true;
            myAnimator.SetTrigger("death");
            PlayerVariables.lives--;
            StartCoroutine(WaitAndRespawn(3.0f));
        }
       
    }
	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.gameObject.tag == "Soul")
        {
            PlayerVariables.totalAscensionPoints += ascensionPoints;
            PlayerVariables.totalDescensionPoints += descensionPoints;
            myAnimator.SetFloat("speed", 0.0f);
            isCutscene = true;
        }
        else if(collision.gameObject.tag == "Narration")
        {
            myAnimator.SetFloat("speed", 0.0f);
            isCutscene = true;
        }
    }
	// suspend execution for waitTime seconds
	IEnumerator WaitAndRespawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
       
        if (PlayerVariables.lives == 0)
        {
            gameOverPanel.SetActive(true);
        }
        else if (PlayerVariables.lives > 0)
        {
            myAnimator.Play("Idle");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            isDead = false;
        }
    }

   
}
 