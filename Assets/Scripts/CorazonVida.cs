using UnityEngine;

public class CorazonVida : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reproducir sonido
            AudioClip clip = Resources.Load<AudioClip>("corazon_vida");

            if (clip != null)
            {
                AudioSource.PlayClipAtPoint(clip, transform.position, 3f);
            }

            // Recuperar vida
            GestorVidas.instancia.RecuperarVida();

            // Destruir el corazón
            Destroy(gameObject);
        }
    }
}