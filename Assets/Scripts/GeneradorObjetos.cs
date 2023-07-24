using System.Collections;
using UnityEngine;

public class GeneradorObjetos : MonoBehaviour
{
    [SerializeField] private GameObject prefabTorreOEnemigo;
    [SerializeField] private float tiempoEntreGeneraciones;
    [SerializeField] private int anchoGrid;
    [SerializeField] private int altoGrid;
    [SerializeField] private Vector2 tamañoCelda;

    private void Start()
    {
        StartCoroutine(GenerarObjetosPeriodicamente());
    }

    private IEnumerator GenerarObjetosPeriodicamente()
    {
        while (true)
        {
            // Obtener las esquinas del área jugable
            Vector2 esquinaInferiorIzquierda = new Vector2(transform.position.x - (anchoGrid * tamañoCelda.x) / 2f,
                                                           transform.position.y - (altoGrid * tamañoCelda.y) / 2f);
            Vector2 esquinaSuperiorDerecha = new Vector2(transform.position.x + (anchoGrid * tamañoCelda.x) / 2f,
                                                         transform.position.y + (altoGrid * tamañoCelda.y) / 2f);

            // Generar la posición aleatoria dentro del área jugable y asegurarnos de que no esté en un muro
            Vector2 posicionGenerada = new Vector2(Random.Range(esquinaInferiorIzquierda.x, esquinaSuperiorDerecha.x),
                                                   Random.Range(esquinaInferiorIzquierda.y, esquinaSuperiorDerecha.y));

            while (!EsEspacioJugable(posicionGenerada))
            {
                // Si la posición generada está en un muro, intentar nuevamente
                posicionGenerada = new Vector2(Random.Range(esquinaInferiorIzquierda.x, esquinaSuperiorDerecha.x),
                                               Random.Range(esquinaInferiorIzquierda.y, esquinaSuperiorDerecha.y));
            }

            // Instanciar el prefab en la posición generada
            Instantiate(prefabTorreOEnemigo, posicionGenerada, Quaternion.identity);

            // Esperar el tiempo especificado antes de generar el siguiente objeto
            yield return new WaitForSeconds(tiempoEntreGeneraciones);
        }
    }

    private bool EsEspacioJugable(Vector2 posicion)
    {
        Vector2 posicionLocal = posicion - (Vector2)transform.position;

        // Lanzar un Raycast hacia abajo desde la posición para verificar si hay un muro en esa celda.
        RaycastHit2D hit = Physics2D.Raycast(posicion, Vector2.down, 1f);

        // Si el Raycast golpea un collider con la capa de muros, la celda no es jugable.
        // Si el Raycast no golpea ningún collider, la celda es jugable.
        return hit.collider == null;
    }
}

