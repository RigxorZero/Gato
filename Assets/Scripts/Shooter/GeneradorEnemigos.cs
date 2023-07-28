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
            // Obtener una posici�n aleatoria dentro de los l�mites del mundo
            Vector2 posicionAleatoria = GetRandomPositionWithoutCollision();

            // Instanciar el enemigo en la posici�n generada
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
            // Generar una posici�n aleatoria dentro de los l�mites del mundo (ajusta estos valores seg�n tus necesidades)
            float x = Random.Range(-7.66f, 61.71f);
            float y = Random.Range(-35.34f, 3.39f);
            position = new Vector2(x, y);
        }
        while (HasCollisionWithSolidSurface(position));

        return position;
    }

    private bool HasCollisionWithSolidSurface(Vector2 position)
    {
        // Lanzar un Raycast hacia abajo desde la posici�n para verificar si hay un muro en esa posici�n.
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 1f, capaMuros);

        // Si el Raycast golpea un collider con la capa de muros, hay una colisi�n.
        // Si el Raycast no golpea ning�n collider, no hay colisi�n.
        return hit.collider != null;
    }
}
