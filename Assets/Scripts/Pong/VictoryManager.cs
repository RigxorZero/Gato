using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    [SerializeField] private TMP_Text winnerText;

    void Start()
    {
        int winner = PlayerPrefs.GetInt("Winner");

        if (winner == 1)
        {
            winnerText.text = "Gana el lado derecho";
        }
        else if (winner == 2)
        {
            winnerText.text = "Gana el lado izquierdo";
        }

        // Eliminamos el valor de Winner en PlayerPrefs para futuros juegos
        PlayerPrefs.DeleteKey("Winner");
    }
}
