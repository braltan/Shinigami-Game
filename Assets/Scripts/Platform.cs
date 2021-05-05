using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private Transform posA;
    [SerializeField]
    private Transform posB;

    private Vector3 orgPos;

    public float speed;

    private bool movingRight = true;

    private GameObject target = null;
    private Vector3 offset;

    private enum PlatformType
    {
        Moving,
        Falling,
        Static
    };
    [SerializeField]
    PlatformType platformType;
    // Start is called before the first frame update
    void Start()
    {
        target = null;
        orgPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (platformType == PlatformType.Moving)
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.
            collider.CompareTag("Player"))
        {
            if (platformType == PlatformType.Falling)
            {
                StartCoroutine(Fall());
            }
        }
    }
    IEnumerator Fall()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
        yield return 0;
    }
    void OnCollisionStay2D(Collision2D col)
    {
        target = col.gameObject;
        offset = target.transform.position - transform.position;
    }
    void OnCollisionExit2D(Collision2D col)
    {
        target = null;
    }
    void LateUpdate()
    {
        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }
    }
}

