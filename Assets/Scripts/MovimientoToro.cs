using UnityEngine;

public class MovimientoToro : MonoBehaviour
{
    public float velocidadToro = 12f;

    void Update()
    {
        // Se mueve siempre hacia la izquierda
        transform.Translate(Vector2.left * velocidadToro * Time.deltaTime);

        // Si el toro se sale de la pantalla (ej. posición X < -20), lo destruimos para limpiar memoria
        if (transform.position.x < -20f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si choca con Máxima, le podemos restar vida o reiniciar el nivel
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡El toro chocó a Máxima!");
            // Acá podrías llamar a una función de "GameOver"
        }
    }
}