using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorPausa : MonoBehaviour
{
    public GameObject menuPausa;

    [SerializeField] private bool estaPausado = false;

    private void Update()
    {
        // Verificar si se presiona la tecla "ESC"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Alternar el estado de pausa
            estaPausado = !estaPausado;

            // Activar o desactivar el men� de pausa seg�n el estado de pausa
            if (estaPausado)
            {
                ActivarMenuPausa();
            }
            else
            {
                DesactivarMenuPausa();
            }
        }
    }

    // M�todo p�blico para consultar el estado de pausa desde otros scripts
    public bool EstaPausado()
    {
        return estaPausado;
    }

    private void ActivarMenuPausa()
    {
        // Mostrar el men� de pausa y pausar el tiempo
        Time.timeScale = 0f;
        menuPausa.SetActive(true);
    }

    private void DesactivarMenuPausa()
    {
        // Ocultar el men� de pausa y reanudar el tiempo
        Time.timeScale = 1f;
        menuPausa.SetActive(false);
    }

    // Agregar aqu� las funciones para los botones del men� de pausa
    // Por ejemplo, funci�n para volver al juego, reiniciar, ir al men� principal, etc.
    // Estas funciones se llamar�an desde los botones del men�.

    public void VolverAlJuego()
    {
        estaPausado = false;
        DesactivarMenuPausa();
    }

    public void ReiniciarJuego()
    {
        estaPausado = false; // Actualizar el estado de pausa cuando se presiona el bot�n
        // Recargar la escena actual
        DesactivarMenuPausa();
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(escenaActual);
    }

    public void IrAMenuPrincipal()
    {
        estaPausado = false; // Actualizar el estado de pausa cuando se presiona el bot�n
        DesactivarMenuPausa();
        // Cargar la escena del men� principal (aseg�rate de que el nombre de la escena sea correcto)
        SceneManager.LoadScene("StartScene");
    }
}

