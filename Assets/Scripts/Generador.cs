using UnityEngine;

public class Generador : MonoBehaviour
{
    public GameObject prefabToro; // Arrastrá tu Toro desde la carpeta a este campo
    public float tiempoEntreToros = 3f;

    void Start()
    {
        InvokeRepeating("CrearToro", 1f, tiempoEntreToros);
    }

    void CrearToro()
    {
        Instantiate(prefabToro, transform.position, Quaternion.identity);
    }
}