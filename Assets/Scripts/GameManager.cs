using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int puntaje = 0;
    public Text puntajeText;
    public Image hpImage; // Imagen con el texto "HP"
    public Image[] hearts; // Array de corazones.

    public Sprite spriteCorazonLleno; // Asigna el sprite del corazón lleno en el inspector de Unity.
    public Sprite spriteCorazonGris; // Asigna el sprite del corazón gris en el inspector de Unity.
    private bool tiempoRalentizado = false;
    public Movimiento jugador;
    private int vidas = 3; // Valor inicial de vidas.

    private void Awake()
    {
        // Asegurarse de que solo haya una instancia del GameManager en la escena
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Actualizar el puntaje inicial en el texto
        ActualizarPuntajeText();
        // Actualizar la imagen con el texto "HP"
        ActualizarHPImage();
        // Actualizar los corazones de vidas
        ActualizarHearts();
    }

    private void Update()
    {
        // Otras partes del código...

        // Verificar si el jugador ha perdido todas las vidas
        if (IsGameOver())
        {
            // Cambiar a la nueva escena cuando el jugador muere
            SceneManager.LoadScene("StartScene");
        }
    }

    public void IncrementarPuntaje(int cantidad)
    {
        // Incrementar el puntaje con la cantidad especificada
        puntaje += cantidad;

        // Verificar si el puntaje es un múltiplo de 10 para recuperar una vida
        if (puntaje % 10 == 0)
        {
            RecuperarVida();
        }

        // Actualizar el texto del puntaje
        ActualizarPuntajeText();
    }

    public void PerderVida(int cantidad)
    {
        // Restar la cantidad especificada de vidas
        vidas -= cantidad;

        // Asegurarse de que el valor de vidas no sea menor a 0
        if (vidas < 0)
        {
            vidas = 0;
        }

        // Actualizar la imagen con el texto "HP"
        ActualizarHPImage();
        // Actualizar los corazones de vidas
        ActualizarHearts();
    }

    private void RecuperarVida()
    {
        // Asegurarse de que no exceda el máximo de 3 vidas
        if (vidas < 3)
        {
            vidas++;
            ActualizarHearts();
        }
    }

    private void ActualizarPuntajeText()
    {
        // Mostrar el puntaje actualizado en el texto
        puntajeText.text = "Puntaje: " + puntaje.ToString();
    }

    private void ActualizarHPImage()
    {
        // Mostrar la imagen con el texto "HP" junto al número de vidas
        hpImage.enabled = vidas > 0; // Activar o desactivar la imagen según el número de vidas
    }

    private void ActualizarHearts()
    {
        // Asegurarse de que el índice de los corazones esté dentro del rango del array de corazones.
        int heartIndex = Mathf.Clamp(vidas, 0, hearts.Length);

        // Activar o desactivar los corazones según el número de vidas y cambiar el sprite al corazón gris si es necesario.
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < heartIndex)
            {
                // Corazón lleno
                hearts[i].sprite = spriteCorazonLleno; // Aquí asigna el sprite del corazón lleno
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                // Corazón gris
                hearts[i].sprite = spriteCorazonGris; // Aquí asigna el sprite del corazón gris
                hearts[i].gameObject.SetActive(true);
            }
        }
    }

    public void RalentizarTiempo(float duracionRalentizacion)
    {
        if (!tiempoRalentizado)
        {
            // Ralentizar el tiempo
            Time.timeScale = 0.5f; // Puedes ajustar el valor para cambiar la ralentización

            // Desactivar la física de los objetos para evitar problemas con la ralentización
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            tiempoRalentizado = true;
            // Llamar a un método para revertir la ralentización después de la duración especificada
            StartCoroutine(RevertirRalentizacion(duracionRalentizacion));
        }
    }

    private IEnumerator RevertirRalentizacion(float duracionRalentizacion)
    {
        yield return new WaitForSeconds(duracionRalentizacion);

        // Revertir la ralentización del tiempo
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        tiempoRalentizado = false;
    }

    public int ObtenerPuntaje()
    {
        return puntaje;
    }

    public bool IsGameOver()
    {
        return vidas <= 0;
    }
}



