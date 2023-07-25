using System.Collections;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField] private GameObject prefabBala;
    [SerializeField] private float velocidadBala;
    [SerializeField] private float distanciaActivacion; // Distancia de activación en cámaras.
    [SerializeField] private float velocidadMovimiento; // Velocidad de movimiento del enemigo.
    [SerializeField] private float tiempoMoverseHaciaJugador = 2f;
    [SerializeField] private float tiempoAlejarseDelJugador = 1.5f;
    [SerializeField] private float tiempoDispararPrediccion = 0.5f;

    private Transform jugador;
    private Animator animator;
    private bool activado = false; // Bandera para controlar si el enemigo está activo.
    private Rigidbody2D rb2D;

    private void Start()
    {
        // Encontrar la referencia al jugador (puedes hacerlo de diferentes maneras, por ejemplo, usando una etiqueta).
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();

        // Iniciar la corutina de movimiento y disparo solo si está activado
        if (activado)
        {
            StartCoroutine(MoverseHaciaJugadorYDisparar());
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
            StartCoroutine(MoverseHaciaJugadorYDisparar());
        }
        else if (distanciaJugador > distanciaActivacion && activado) // Si la distancia es mayor a la distancia de activación en cámaras y el enemigo estaba activado, desactivar el enemigo.
        {
            activado = false;
            StopAllCoroutines();
        }
    }

    private void MoverEnemigo()
    {
        // Calcular la dirección hacia el jugador y mover al enemigo en esa dirección.
        Vector2 direccionMovimiento = (jugador.position - transform.position).normalized;
        rb2D.MovePosition(rb2D.position + direccionMovimiento * velocidadMovimiento * Time.fixedDeltaTime);

        // Actualizar el Animator con la dirección del movimiento
        animator.SetFloat("MovimientoX", direccionMovimiento.x);
        animator.SetFloat("MovimientoY", direccionMovimiento.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Detectar colisión con el muro y cambiar la dirección de movimiento.
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 direccionInversa = -(jugador.position - transform.position).normalized;
            rb2D.MovePosition(rb2D.position + direccionInversa * velocidadMovimiento * Time.fixedDeltaTime);
        }
    }

    private IEnumerator MoverseHaciaJugadorYDisparar()
    {
        while (true)
        {
            // Moverse hacia el jugador durante el tiempo establecido.
            float tiempoInicioMoverseHaciaJugador = Time.time;
            while (Time.time - tiempoInicioMoverseHaciaJugador < tiempoMoverseHaciaJugador)
            {
                MoverEnemigo();
                yield return null;
            }

            // Alejarse del jugador durante el tiempo establecido.
            float tiempoInicioAlejarseDelJugador = Time.time;
            while (Time.time - tiempoInicioAlejarseDelJugador < tiempoAlejarseDelJugador)
            {
                Vector2 direccionInversa = -(jugador.position - transform.position).normalized;
                rb2D.MovePosition(rb2D.position + direccionInversa * velocidadMovimiento * Time.fixedDeltaTime);

                // Actualizar el Animator con la dirección del movimiento
                animator.SetFloat("MovimientoX", direccionInversa.x);
                animator.SetFloat("MovimientoY", direccionInversa.y);

                yield return null;
            }

            // Disparar hacia la posición predicha del jugador.
            Vector2 posicionPredichaJugador = (Vector2)jugador.position + jugador.GetComponent<Rigidbody2D>().velocity * tiempoDispararPrediccion;
            Vector2 direccionDisparo = (posicionPredichaJugador - (Vector2)transform.position).normalized;
            Disparar(direccionDisparo);
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
