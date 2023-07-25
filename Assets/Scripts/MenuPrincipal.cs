using UnityEngine;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    public Text puntajeGuardadoText;

    private void Start()
    {
        // Recuperar el puntaje guardado desde PlayerPrefs
        int puntajeGuardado = PlayerPrefs.GetInt("PuntajeGuardado", 0); // El segundo parámetro es el valor por defecto en caso de que la clave no exista

        // Mostrar el puntaje guardado en el Text
        puntajeGuardadoText.text = "Puntaje guardado: " + puntajeGuardado.ToString();
    }
}
