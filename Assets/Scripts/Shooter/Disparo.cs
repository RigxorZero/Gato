using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    [SerializeField] private GameObject prefabBala;
    [SerializeField] private float velocidadBala;

    public void Disparar(Vector2 direccionDisparo)
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
