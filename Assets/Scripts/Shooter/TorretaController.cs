using System.Collections;
using UnityEngine;

public class TorretaController : MonoBehaviour
{
    [SerializeField] private GameObject prefabBala;
    [SerializeField] private float velocidadBala;
    [SerializeField] private float distanciaActivacion; // Distancia de activaci�n en c�maras.

    private Transform jugador;
    private Animator animator;
    private bool activado = false; // Bandera para controlar si la torreta est� activa.

    private void Start()
    {
        // Encontrar la referencia al jugador (puedes hacerlo de diferentes maneras, por ejemplo, usando una etiqueta).
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Calcular la distancia entre el enemigo y el jugador en c�maras.
        float distanciaJugador = Vector3.Distance(transform.position, jugador.position) / Camera.main.orthographicSize;

        // Si la distancia es menor o igual a la distancia de activaci�n en c�maras y el enemigo no ha sido activado, activar el enemigo y comenzar la corutina.
        if (distanciaJugador <= distanciaActivacion && !activado)
        {
            activado = true;
            // Comenzar la corutina para disparar cada 1 segundo.
            StartCoroutine(DispararCadaUnSegundo());
        }
        else if (distanciaJugador > distanciaActivacion && activado) // Si la distancia es mayor a la distancia de activaci�n en c�maras y el enemigo estaba activado, desactivar el enemigo.
        {
            activado = false;
            StopAllCoroutines();
        }
    }

    private IEnumerator DispararCadaUnSegundo()
    {
        while (true)
        {
            // Calcular la direcci�n de disparo hacia el jugador.
            Vector2 direccionDisparo = (jugador.position - transform.position).normalized;
            // Calcular dos direcciones adicionales de disparo hacia el jugador, rotando la direcci�n principal.
            Vector2 direccionDisparo1 = Quaternion.Euler(0, 0, 45) * direccionDisparo;
            Vector2 direccionDisparo2 = Quaternion.Euler(0, 0, -45) * direccionDisparo;

            // Actualizar el Animator con la direcci�n del disparo (puedes elegir una direcci�n espec�fica o mezclarlas).
            animator.SetFloat("MovimientoX", direccionDisparo.x);
            animator.SetFloat("MovimientoY", direccionDisparo.y);

            // Llamar a la funci�n de disparo con las tres direcciones distintas.
            Disparar(direccionDisparo);
            Disparar(direccionDisparo1);
            Disparar(direccionDisparo2);

            // Esperar 1 segundo antes de disparar nuevamente.
            yield return new WaitForSeconds(1f);
        }
    }

    private void Disparar(Vector2 direccionDisparo)
    {
        // Crear una instancia de la bala utilizando Instantiate
        GameObject bala = Instantiate(prefabBala, transform.position, Quaternion.identity);

        // Establecer la capa de la bala seg�n el objeto que la dispar�
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            bala.layer = LayerMask.NameToLayer("PlayerBullet");
        }
        else if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            bala.layer = LayerMask.NameToLayer("EnemyBullet");
        }

        // Obtener el Rigidbody2D de la bala
        Rigidbody2D rbBala = bala.GetComponent<Rigidbody2D>();

        // Establecer la velocidad de la bala para que se mueva en la direcci�n de disparo
        rbBala.velocity = direccionDisparo.normalized * velocidadBala;
    }
}
