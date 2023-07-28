using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    [SerializeField] private TMP_Text paddle1scoreText;
    [SerializeField] private TMP_Text paddle2scoreText;
    [SerializeField] private Transform paddle1Transform;
    [SerializeField] private Transform paddle2Transform;
    [SerializeField] private Transform ballTransform;
    [SerializeField] private TMP_Text WIN;

    private int paddle1score;
    private int paddle2score;

    private static Manager instance;
    public static Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Manager>();
            }
            return instance;
        }
    }
    public void Paddle1score()
    {
        paddle1score++;
        paddle1scoreText.text =paddle1score.ToString();
    }
    public void Paddle2score()
    {
        paddle2score++;
        paddle2scoreText.text = paddle2score.ToString();
    }
    public void Restart()
    {
        paddle1Transform.position = new Vector2(paddle1Transform.position.x, 0);
        paddle2Transform.position = new Vector2(paddle2Transform.position.x, 0);
        ballTransform.position = new Vector2(0, 0);
    }
    public void Ganador()
    {
        if (paddle1score == 10)
        {
            SceneManager.LoadScene("Victory Screen");
            WIN.text = "Jugador 1 gana";
        }
        else if (paddle2score == 10) 
        {
            SceneManager.LoadScene("Victory Screen");
            WIN.text = "Jugador 2 gana";
        }
    }
}
