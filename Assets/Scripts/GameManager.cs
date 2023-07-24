using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int puntaje = 0;
    public Text puntajeText;
    public Image hpImage; // Imagen con el texto "HP"
    public Image[] hearts; // Array de corazones.

    public Sprite spriteCorazonLleno; // Asigna el sprite del coraz�n lleno en el inspector de Unity.
    public Sprite spriteCorazonGris; // Asigna el sprite del coraz�n gris en el inspector de Unity.

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

    public void IncrementarPuntaje(int cantidad)
    {
        // Incrementar el puntaje con la cantidad especificada
        puntaje += cantidad;

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
}


