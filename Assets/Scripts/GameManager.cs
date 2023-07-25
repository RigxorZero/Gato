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

    public Sprite spriteCorazonLleno; // Asigna el sprite del coraz�n lleno en el inspector de Unity.
    public Sprite spriteCorazonGris; // Asigna el sprite del coraz�n gris en el inspector de Unity.
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
        // Otras partes del c�digo...

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

        // Verificar si el puntaje es un m�ltiplo de 10 para recuperar una vida
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
        // Asegurarse de que no exceda el m�ximo de 3 vidas
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
        // Mostrar la imagen con el texto "HP" junto al n�mero de vidas
        hpImage.enabled = vidas > 0; // Activar o desactivar la imagen seg�n el n�mero de vidas
    }

    private void ActualizarHearts()
    {
        // Asegurarse de que el �ndice de los corazones est� dentro del rango del array de corazones.
        int heartIndex = Mathf.Clamp(vidas, 0, hearts.Length);

        // Activar o desactivar los corazones seg�n el n�mero de vidas y cambiar el sprite al coraz�n gris si es necesario.
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < heartIndex)
            {
                // Coraz�n lleno
                hearts[i].sprite = spriteCorazonLleno; // Aqu� asigna el sprite del coraz�n lleno
                hearts[i].gameObject.SetActive(true);
            }
            else
            {
                // Coraz�n gris
                hearts[i].sprite = spriteCorazonGris; // Aqu� asigna el sprite del coraz�n gris
                hearts[i].gameObject.SetActive(true);
            }
        }
    }

    public void RalentizarTiempo(float duracionRalentizacion)
    {
        if (!tiempoRalentizado)
        {
            // Ralentizar el tiempo
            Time.timeScale = 0.5f; // Puedes ajustar el valor para cambiar la ralentizaci�n

            // Desactivar la f�sica de los objetos para evitar problemas con la ralentizaci�n
            Time.fixedDeltaTime = 0.02f * Time.timeScale;

            tiempoRalentizado = true;
            // Llamar a un m�todo para revertir la ralentizaci�n despu�s de la duraci�n especificada
            StartCoroutine(RevertirRalentizacion(duracionRalentizacion));
        }
    }

    private IEnumerator RevertirRalentizacion(float duracionRalentizacion)
    {
        yield return new WaitForSeconds(duracionRalentizacion);

        // Revertir la ralentizaci�n del tiempo
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



