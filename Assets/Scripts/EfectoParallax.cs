using UnityEngine;

public class EfectoParallax : MonoBehaviour
{
    public Transform camara;
    [Range(0f, 1f)]
    public float factorParallax;

    private float posicionYInicial; // Guardamos la altura original
    private Vector3 posicionAnteriorCamara;

    void Start()
    {
        if (camara == null) camara = Camera.main.transform;
        posicionAnteriorCamara = camara.position;
        posicionYInicial = transform.position.y; // Guardamos dónde está ahora
    }

    void LateUpdate()
    {
        Vector3 movimientoCamara = camara.position - posicionAnteriorCamara;

        // Solo movemos el eje X
        transform.position += new Vector3(movimientoCamara.x * (1f - factorParallax), 0f, 0f);

        // ¡FORZAMOS a que el eje Y siempre vuelva a su lugar original!
        transform.position = new Vector3(transform.position.x, posicionYInicial, transform.position.z);

        posicionAnteriorCamara = camara.position;
    }
}