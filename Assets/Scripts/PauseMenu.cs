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

            // Activar o desactivar el menú de pausa según el estado de pausa
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
        // Mostrar el menú de pausa y pausar el tiempo
        Time.timeScale = 0f;
        menuPausa.SetActive(true);
    }

    private void DesactivarMenuPausa()
    {
        // Ocultar el menú de pausa y reanudar el tiempo
        Time.timeScale = 1f;
        menuPausa.SetActive(false);

        // Reiniciar el estado de pausa
        estaPausado = false;
    }

    // Agregar aquí las funciones para los botones del menú de pausa
    // Por ejemplo, función para volver al juego, reiniciar, ir al menú principal, etc.
    // Estas funciones se llamarían desde los botones del menú.

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
        // Cargar la escena del menú principal (asegúrate de que el nombre de la escena sea correcto)
        SceneManager.LoadScene("StartScene");
    }

}
