using UnityEngine;

public class LanzadorBombas : MonoBehaviour
{
    [Header("Configuración")]
    public GameObject prefabBomba;
    public Transform puntoLanzamiento;

    [Header("Tiempo entre bombas")]
    public float tiempoInicial = 10f;
    public float tiempoRapido = 7f;
    public float tiempoCambio = 30f;

    [Header("Lanzamiento")]
    public float fuerzaHorizontal = 8f;
    public float fuerzaVertical = 5f;

    [Header("Distancia mínima")]
    public Transform maxima;
    public float distanciaMinimaParaLanzar = 2.5f;

    private float temporizador;

    void Update()
    {
        // Determina cada cuánto tiempo lanzar bombas según el tiempo transcurrido
        float tiempoActualEntreBombas = Time.time >= tiempoCambio ? tiempoRapido : tiempoInicial;

        temporizador += Time.deltaTime;

        if (temporizador >= tiempoActualEntreBombas)
        {
            // Calcula la distancia entre la reina y Máxima
            float distancia = Vector2.Distance(transform.position, maxima.position);

            // Solo lanza la bomba si Máxima está lo suficientemente lejos
            if (distancia > distanciaMinimaParaLanzar)
            {
                LanzarBomba();
                temporizador = 0f;
            }
        }
    }

    void LanzarBomba()
    {
        GameObject bomba = Instantiate(prefabBomba, puntoLanzamiento.position, Quaternion.identity);

        Rigidbody2D rb = bomba.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = new Vector2(fuerzaHorizontal, fuerzaVertical);
        }
    }
}