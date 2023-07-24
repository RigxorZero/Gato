using UnityEngine;

public class Bala : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Obtener la capa del objeto con el que colisionó la bala
        int collisionLayer = collision.gameObject.layer;

        // Obtener las capas del jugador y del enemigo
        int playerLayer = LayerMask.NameToLayer("PlayerBullet");
        int enemyLayer = LayerMask.NameToLayer("EnemyBullet");
        int enemyLayerMask = LayerMask.NameToLayer("Enemy");

        // Verificar si la bala colisionó con un muro
        if (collisionLayer == LayerMask.NameToLayer("Wall"))
        {
            // Destruir la bala si colisiona con un muro
            Destroy(gameObject);
        }
        // Verificar si la bala colisionó con una bala del otro tipo (jugador o enemigo)
        else if ((gameObject.layer == playerLayer && collisionLayer == enemyLayer) || (gameObject.layer == enemyLayer && collisionLayer == playerLayer))
        {
            // Destruir ambas balas si colisionan entre sí
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        // Verificar si la bala colisionó con un enemigo (solo si la bala es del jugador)
        else if (gameObject.layer == playerLayer && collisionLayer == enemyLayerMask)
        {
            // Destruir al enemigo si es golpeado por la bala del jugador
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}


