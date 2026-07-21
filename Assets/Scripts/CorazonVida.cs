using UnityEngine;

public class CorazonVida : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GestorVidas.instancia.RecuperarVida();
            Destroy(gameObject);
        }
    }
}