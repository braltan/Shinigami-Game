using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    [SerializeField]
    private Transform posA;
    [SerializeField]
    private Transform posB;
    [SerializeField]
    private bool shootingUp;


    private bool movingRight = true;

    private bool shootingRight = false;

    public Transform spawnPoint = null;

    public float firingRate;

    public GameObject projectile = null;

    private enum EnemyType
    {
        Patrolling,
        Firing
    };
    [SerializeField]
    EnemyType enemyType;
    // Start is called before the first frame update
    void Start()
    {
        movingRight = true;
        if (enemyType == EnemyType.Firing)
        {
            InvokeRepeating("LaunchProjectile", 0.0f, firingRate);
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyType == EnemyType.Patrolling)
        {
            if (movingRight == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, posB.position, speed * Time.deltaTime);
                if (transform.position == posB.position)
                {
                    movingRight = false;
                }

            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, posA.position, speed * Time.deltaTime);

                if (transform.position == posA.position)
                {
                    movingRight = true;
                }
            }
        }
      
    }
    void LaunchProjectile()
    {     // Create the Bullet from the Bullet Prefab
        GameObject bullet = Instantiate(
            projectile,
            spawnPoint.position,
            spawnPoint.rotation) as GameObject;

        // Add velocity to the bullet
        Rigidbody2D rigidbody2D = bullet.GetComponent<Rigidbody2D>();
        if (shootingUp)
        {
            rigidbody2D.velocity = new Vector2(0, 10);
        }
        else
        {
            if (shootingRight)
            {
                rigidbody2D.velocity = new Vector2(10, 0);
            }
            else
            {
                rigidbody2D.velocity = new Vector2(-10, 0);
            }
        }
        // Destroy the bullet after 2 seconds
        Destroy(bullet, 1.0f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.tag == "PlayerProjectile")
        {
        
            Destroy(gameObject);
        }
    }
   
}
