using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    private Vector2 direccion;
    private Rigidbody2D rb2D;
    private float movimientoX;
    private float movimientoY;
    private Animator animator;
    public ControladorPausa controladorPausa;
    public Disparo scriptDisparo;
    private Vector2 ultimaDireccionDisparo;
    [SerializeField] private float cadenciaDisparo = 0.3f;
    private float tiempoUltimoDisparo = 0f;



    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public Vector2 Direccion
    {
        get { return direccion; }
    }

    private void Update()
    {
        // Verificar si el juego est� pausado, si lo est�, no procesar entradas del jugador
        if (controladorPausa != null && controladorPausa.EstaPausado())
        {
            return;
        }

        movimientoX = Input.GetAxisRaw("Horizontal");
        movimientoY = Input.GetAxisRaw("Vertical");
        animator.SetFloat("MovimientoX", movimientoX);
        animator.SetFloat("MovimientoY", movimientoY);

        if (movimientoX != 0 || movimientoY != 0)
        {
            animator.SetFloat("UltimoX", movimientoX);
            animator.SetFloat("UltimoY", movimientoY);
        }
        direccion = new Vector2(movimientoX, movimientoY).normalized;

        // Actualizar la �ltima direcci�n de disparo cuando el jugador se mueva
        if (movimientoX != 0 || movimientoY != 0)
        {
            ultimaDireccionDisparo = direccion;
        }

        // Detectar la tecla de disparo (por ejemplo, la tecla "K")
        if (Input.GetKeyUp(KeyCode.K))
        {
            // Verificar si ha pasado suficiente tiempo desde el �ltimo disparo
            if (Time.time - tiempoUltimoDisparo >= cadenciaDisparo)
            {
                // Obtener la direcci�n en la que est� mirando el jugador
                Vector2 direccionDisparo = (ultimaDireccionDisparo != Vector2.zero) ? ultimaDireccionDisparo : direccion;

                // Llamar a la funci�n de disparo del script de Disparo
                if (scriptDisparo != null)
                {
                    scriptDisparo.Disparar(direccionDisparo);
                }

                // Actualizar el tiempo del �ltimo disparo
                tiempoUltimoDisparo = Time.time;
            }
        }
    }



    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + Time.fixedDeltaTime * velocidadMovimiento * direccion);
    }
}

