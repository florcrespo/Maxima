using UnityEngine;

public class Generador : MonoBehaviour
{
    public GameObject prefabToro;
    public float tiempoEntreToros = 3f;

    private bool generando = true;

    void Start()
    {
        InvokeRepeating("CrearToro", 1f, tiempoEntreToros);
    }

    void CrearToro()
    {
        if (!generando) return;

        Instantiate(prefabToro, transform.position, Quaternion.identity);
    }

    public void DetenerGenerador()
    {
        generando = false;
        CancelInvoke("CrearToro");
    }
}