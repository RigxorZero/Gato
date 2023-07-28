using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float initialVelocity = 4f;
    private Rigidbody2D ballRB;
    private float minThreshold = 0.5f;

    void Start()
    {
        ballRB = GetComponent<Rigidbody2D>();
        Launch();
    }

    public void Launch()
    {
        Vector2 randomDirection;

        do
        {
            randomDirection = Random.insideUnitCircle.normalized;
        }
        while (Mathf.Abs(randomDirection.x) < minThreshold || Mathf.Abs(randomDirection.y) < minThreshold);

        ballRB.velocity = randomDirection * initialVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Point1"))
        {
            Manager.Instance.Paddle2score();
            Destroy(gameObject);
            Launch();

            // Llamamos al método para generar las pelotas adicionales
            Manager.Instance.GenerateExtraBalls();
        }
        else if (collision.gameObject.CompareTag("Point2"))
        {
            Manager.Instance.Paddle1score();
            Destroy(gameObject);
            Launch();

            // Llamamos al método para generar las pelotas adicionales
            Manager.Instance.GenerateExtraBalls();
        }

        // Aumentar la velocidad real de la pelota con cada rebote
        ballRB.velocity = ballRB.velocity.normalized * (initialVelocity += 0.2f);
    }
}

