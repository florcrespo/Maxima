using UnityEngine;

public class BucleFondo : MonoBehaviour
{
    private float anchoFondo;
    private Vector3 inicioPosicion;

    void Start()
    {
        // Calculamos cuánto mide el fondo realmente
        anchoFondo = GetComponent<SpriteRenderer>().bounds.size.x;
        inicioPosicion = transform.position;
    }

    void Update()
    {
        // Si el fondo se movió más de lo que mide su propio ancho, lo reseteamos
        float distanciaMovida = transform.position.x - inicioPosicion.x;

        if (Mathf.Abs(distanciaMovida) >= anchoFondo)
        {
            // Lo movemos de vuelta al punto de inicio para que encaje perfecto
            transform.position = inicioPosicion;
        }
    }
}