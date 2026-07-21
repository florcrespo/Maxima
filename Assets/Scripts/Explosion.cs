using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("Daño")]
    public float tiempoVida = 0.4f;

    [Header("Sonido")]
    public float volumen = 2f;

    private bool hizoDaño = false;

    void Start()
    {
        AudioClip clip = Resources.Load<AudioClip>("explosion");

        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, volumen);
        }

        Destroy(gameObject, tiempoVida);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hizoDaño)
            return;

        if (other.CompareTag("Player"))
        {
            hizoDaño = true;
            GestorVidas.instancia.PerderVida(other.gameObject);
        }
    }
}