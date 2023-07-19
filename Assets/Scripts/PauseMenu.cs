using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorPausa : MonoBehaviour
{
    public GameObject menuPausa;

    private bool estaPausado = false;

    private void Update()
    {
        // Verificar si se presiona la tecla "ESC"
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Alternar el estado de pausa
            estaPausado = !estaPausado;
            Debug.Log("Tecla ESC presionada.");

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

        // Reiniciar el estado de pausa
        estaPausado = false;
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
        // Recargar la escena actual
        int escenaActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(escenaActual);
    }

    public void IrAMenuPrincipal()
    {
        // Cargar la escena del men� principal (aseg�rate de que el nombre de la escena sea correcto)
        SceneManager.LoadScene("StartScene");
    }

}
