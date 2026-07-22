using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("Daño")]
    public float tiempoVida = 0.4f;

    [Header("Sonido")]
    public float volumen = 2f;

    [Header("Radio de daño")]
    public float radioDaño = 2.5f;

    private bool hizoDaño = false;

    void Start()
    {
        AudioClip clip = Resources.Load<AudioClip>("explosion");

        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position, volumen);
        }

        // Comprueba inmediatamente si Máxima ya está dentro del radio
        Collider2D jugador = Physics2D.OverlapCircle(
            transform.position,
            radioDaño,
            LayerMask.GetMask("Player")
        );

        if (jugador != null)
        {
            hizoDaño = true;
            GestorVidas.instancia.PerderVida(jugador.gameObject);
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