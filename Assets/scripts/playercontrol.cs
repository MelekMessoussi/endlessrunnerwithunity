using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playercontrol : MonoBehaviour
{
    private Vector2 startTouchPosition, endTouchPosition;
    private Touch touch;
    private bool coroutineAllowed;
    private Rigidbody rb;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float jumpForce;
    [SerializeField] private float speed;
    [SerializeField] private float tspeed;
    [SerializeField] private float gravityScale; 

    void Start()
    {
        coroutineAllowed = true;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; 
    }

    void Update()
    {
        rb.velocity = new Vector3(0, 0, 1 * Time.deltaTime * speed);
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
        }
        if (touch.phase == TouchPhase.Began)
        {
            startTouchPosition = touch.position;
        }
        if (Input.touchCount > 0 && touch.phase == TouchPhase.Ended && coroutineAllowed)
        {
            endTouchPosition = touch.position;
            Vector2 swipeDirection = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                
                if (swipeDirection.x > 0 && transform.position.x < maxX)
                {
                    StartCoroutine(MovePlayer(Vector3.right));
                }
                
                else if (swipeDirection.x < 0 && transform.position.x > minX)
                {
                    StartCoroutine(MovePlayer(Vector3.left));
                }
            }
            else
            {
                if (swipeDirection.y > 0)
                {
                    Jump();
                }
            }

        }
    }

    IEnumerator MovePlayer(Vector3 direction)
    {
        coroutineAllowed = false;
        float t = 0;
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + direction;

        while (t < 1)
        {
            t += Time.deltaTime * speed;
            
            Vector3 nextPosition = transform.position + direction * Time.deltaTime * tspeed;
            if (nextPosition.x >= minX && nextPosition.x <= maxX)
            {
                transform.Translate(direction * tspeed, Space.World);
            }
            yield return null;
        }

        coroutineAllowed = true;
    }

    void Jump()
    {
        if (Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        
        rb.AddForce(Vector3.down * gravityScale, ForceMode.Acceleration);
    }
}
