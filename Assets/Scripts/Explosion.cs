using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("Daño")]
    public float tiempoVida = 0.4f;

    private bool hizoDaño = false;

    void Start()
    {
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