using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int puntaje = 0;
    public Text puntajeText;
    public Image hpImage; // Imagen con el texto "HP"
    public Image[] hearts; // Array de corazones.

    public Sprite spriteCorazonLleno; // Asigna el sprite del corazón lleno en el inspector de Unity.
    public Sprite spriteCorazonGris; // Asigna el sprite del corazón gris en el inspector de Unity.

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
}


