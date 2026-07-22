using UnityEngine;

public class SensorViento : MonoBehaviour
{
    private AudioSource audioSourceViento;

    void Start()
    {
        // Buscamos o agregamos el AudioSource en este mismo objeto hijo
        audioSourceViento = GetComponent<AudioSource>();
        if (audioSourceViento == null)
        {
            audioSourceViento = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Maxima"))
        {
            // Carga y reproduce el sonido del viento cuando entra al radio grande
            AudioClip clipViento = Resources.Load<AudioClip>("viento");
            if (clipViento != null && audioSourceViento != null && !audioSourceViento.isPlaying)
            {
                audioSourceViento.clip = clipViento;
                audioSourceViento.loop = true;
                audioSourceViento.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Maxima"))
        {
            // Apaga el viento cuando sale del radio grande
            if (audioSourceViento != null)
            {
                audioSourceViento.Stop();
            }
        }
    }
}