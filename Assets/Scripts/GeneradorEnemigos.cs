using System.Collections;
using UnityEngine;

public class GeneradorEnemigos : MonoBehaviour
{
    [SerializeField] private GameObject prefabEnemigoEstatico;
    [SerializeField] private LayerMask capaMuros;
    [SerializeField] private float intervaloEntreEnemigos = 5f;

    private void Start()
    {
        StartCoroutine(GenerarEnemigosPeriodicamente());
    }

    private IEnumerator GenerarEnemigosPeriodicamente()
    {
        while (true) // Bucle infinito para generar enemigos continuamente
        {
            // Obtener una posición aleatoria dentro de los límites del mundo
            Vector2 posicionAleatoria = GetRandomPositionWithoutCollision();

            // Instanciar el enemigo en la posición generada
            Instantiate(prefabEnemigoEstatico, posicionAleatoria, Quaternion.identity);

            // Esperar el intervalo de tiempo antes de generar el siguiente enemigo
            yield return new WaitForSeconds(intervaloEntreEnemigos);
        }
    }


    private Vector2 GetRandomPositionWithoutCollision()
    {
        Vector2 position;

        do
        {
            // Generar una posición aleatoria dentro de los límites del mundo (ajusta estos valores según tus necesidades)
            float x = Random.Range(-7.66f, 61.71f);
            float y = Random.Range(-35.34f, 3.39f);
            position = new Vector2(x, y);
        }
        while (HasCollisionWithSolidSurface(position));

        return position;
    }

    private bool HasCollisionWithSolidSurface(Vector2 position)
    {
        // Lanzar un Raycast hacia abajo desde la posición para verificar si hay un muro en esa posición.
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 1f, capaMuros);

        // Si el Raycast golpea un collider con la capa de muros, hay una colisión.
        // Si el Raycast no golpea ningún collider, no hay colisión.
        return hit.collider != null;
    }
}
