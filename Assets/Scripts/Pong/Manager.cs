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
    public GameObject ballPrefab;
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
        ballTransform.position = new Vector2(0, 0);
    }
    public void Ganador()
    {
        if (paddle1score == 10)
        {
            PlayerPrefs.SetInt("Winner", 1);
            SceneManager.LoadScene("VictoryScene");
        }
        else if (paddle2score == 10)
        {
            PlayerPrefs.SetInt("Winner", 2);
            SceneManager.LoadScene("VictoryScene");
        }
    }


    public void GenerateExtraBalls()
    {
        for (int i = 0; i < 2; i++)
        {
            _ = Instantiate(ballPrefab, new Vector2(0, 0), Quaternion.identity);
        }
    }

    private void Update()
    {
        Ganador();
    }
}
