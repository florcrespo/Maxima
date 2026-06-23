using UnityEngine;

public class AsadoRecogible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventarioManager.instancia.AgregarAsado();

            Destroy(gameObject);
        }
    }
}