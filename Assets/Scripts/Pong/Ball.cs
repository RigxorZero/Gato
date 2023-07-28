using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float inicialvelocity = 4f;
    private Rigidbody2D ballRB;
    void Start()
    {
        ballRB = GetComponent<Rigidbody2D>();
        Launch();
    }
    private void Launch()
    {
        float xVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        float yVelocity = Random.Range(0, 2) == 0 ? 1 : -1;
        ballRB.velocity = new Vector2(xVelocity, yVelocity) * inicialvelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Point1"))
        {
            Manager.Instance.Paddle2score();
            Manager.Instance.Restart();
            Launch();
        }
        else if(collision.gameObject.CompareTag("Point2"))
        {
            Manager.Instance.Paddle1score();
            Manager.Instance.Restart();
            Launch();
        }
    }

    void Update()
    {
        
    }
}