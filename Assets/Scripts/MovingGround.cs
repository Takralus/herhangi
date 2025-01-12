using System.Collections;
using UnityEngine;

public class MovingGround : MonoBehaviour
{
    public float moveSpeed = 2f; 
    public float moveDistance = 5f; 
    private Vector3 startPosition; 
    private bool movingRight = true; 

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(startPosition, transform.position) >= moveDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            transform.position -= Vector3.right * moveSpeed * Time.deltaTime;

            if (Vector3.Distance(startPosition, transform.position) <= 0f)
            {
                movingRight = true;
            }
        }
    }
}