using System.Collections;
using UnityEngine;

public class PatrullaController : MonoBehaviour
{
    [SerializeField] private GameObject prefabBala;
    [SerializeField] private float velocidadBala;
    [SerializeField] private float distanciaActivacion; // Distancia de activación en cámaras.
    [SerializeField] private float velocidadMovimiento; // Velocidad de movimiento del enemigo.

    private Transform jugador;
    private Animator animator;
    private bool activado = false; // Bandera para controlar si el enemigo está activo.
    private Rigidbody2D rb2D;

    private bool patrullaHorizontal; // Variable para determinar si patrulla de forma horizontal o vertical
    private Vector2 direccionMovimiento; // Dirección de movimiento del enemigo

    private void Start()
    {
        // Encontrar la referencia al jugador (puedes hacerlo de diferentes maneras, por ejemplo, usando una etiqueta).
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

        // Determinar si patrulla de forma horizontal o vertical de forma aleatoria
        patrullaHorizontal = Random.Range(0, 2) == 0;

        // Definir la dirección de movimiento basada en si patrulla de forma horizontal o vertical
        direccionMovimiento = patrullaHorizontal ? Vector2.right : Vector2.up;

        // Iniciar la corutina de disparo solo si está activado
        if (activado)
        {
            StartCoroutine(DispararCadaUnSegundo());
        }
    }

    private void Update()
    {
        // Calcular la distancia entre el enemigo y el jugador en cámaras.
        float distanciaJugador = Vector3.Distance(transform.position, jugador.position) / Camera.main.orthographicSize;

        // Si la distancia es menor o igual a la distancia de activación en cámaras y el enemigo no ha sido activado, activar el enemigo y comenzar la corutina.
        if (distanciaJugador <= distanciaActivacion && !activado)
        {
            activado = true;
            StartCoroutine(DispararCadaUnSegundo());
        }
        else if (distanciaJugador > distanciaActivacion && activado) // Si la distancia es mayor a la distancia de activación en cámaras y el enemigo estaba activado, desactivar el enemigo.
        {
            activado = false;
            StopAllCoroutines();
        }

        // Si el enemigo está activado, moverlo en la dirección de patrulla.
        if (activado)
        {
            MoverEnemigo();
        }
    }

    private void MoverEnemigo()
    {
        // Calcular el desplazamiento en la dirección de movimiento y aplicarlo al enemigo.
        Vector2 desplazamiento = direccionMovimiento * velocidadMovimiento * new Vector2(Time.fixedDeltaTime, Time.fixedDeltaTime);
        rb2D.MovePosition(rb2D.position + desplazamiento);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detectar colisión con el muro y cambiar la dirección de movimiento.
        if (collision.gameObject.CompareTag("Wall"))
        {
            direccionMovimiento = -direccionMovimiento;
        }
    }

    private IEnumerator DispararCadaUnSegundo()
    {
        while (true)
        {
            // Calcular la dirección de disparo hacia el jugador.
            Vector2 direccionDisparo = (jugador.position - transform.position).normalized;

            // Actualizar el Animator con la dirección del disparo (puedes elegir una dirección específica o mezclarlas).
            animator.SetFloat("MovimientoX", direccionDisparo.x);
            animator.SetFloat("MovimientoY", direccionDisparo.y);

            // Llamar a la función de disparo hacia el jugador.
            Disparar(direccionDisparo);

            // Esperar 1 segundo antes de disparar nuevamente.
            yield return new WaitForSeconds(1f);
        }
    }

    private void Disparar(Vector2 direccionDisparo)
    {
        // Crear una instancia de la bala utilizando Instantiate
        GameObject bala = Instantiate(prefabBala, transform.position, Quaternion.identity);

        // Establecer la capa de la bala según el objeto que la disparó
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

        // Establecer la velocidad de la bala para que se mueva en la dirección de disparo
        rbBala.velocity = direccionDisparo.normalized * velocidadBala;
    }
}


