using UnityEngine;
using UnityEngine.UI;
using Unity.Cinemachine;

public class ModoPanico : MonoBehaviour
{
    [Header("Objetos a Monitorear")]
    public Transform jugador;
    public Transform reina;

    [Header("Cámara y UI")]
    public CinemachineCamera camaraVirtual; 
    public Image fondoRojoUI;             // Arrastrá 'FondoRojo'
    public Image marcoFuegoUI;            // Arrastrá 'MarcoPanico'
    public AudioSource musicaFondo;        

    [Header("Ajustes de Pánico")]
    public float distanciaPanico = 0.5f;   // Distancia donde empieza (50cm)
    public float distanciaCritica = 0.2f;  // Máximo pánico (20cm)

    [Header("Ajuste de Zoom (Diferencia)")]
    public float cuantoAcercarZoom = 1.2f; // Cuánto se acerca la cámara respecto a tu zoom original

    [Header("Valores de Música")]
    public float pitchNormal = 1f;
    public float pitchPanico = 1.2f;    

    private float alfaFondoBase = 0.25f; 
    private float zoomOriginalJuego;       // Lee automáticamente tu Zoom (ej: 9)
    private CinemachinePositionComposer composer;

    void Start()
    {
        // 1. Guardamos el Zoom que vos ya tenés configurado en tu cámara en el Inspector
        if (camaraVirtual != null)
        {
            zoomOriginalJuego = camaraVirtual.Lens.OrthographicSize;
            composer = camaraVirtual.GetCinemachineComponent(CinemachineCore.Stage.Body) as CinemachinePositionComposer;
        }

        // 2. Configuración de UI
        if (fondoRojoUI != null)
        {
            alfaFondoBase = fondoRojoUI.color.a; // Respetamos tu 25% de alfa
            SetAlfa(fondoRojoUI, 0f);
        }
        if (marcoFuegoUI != null) SetAlfa(marcoFuegoUI, 0f);
    }

    void Update()
    {
        if (jugador == null || reina == null) return;

        Vector2 posJugador = new Vector2(jugador.position.x, jugador.position.y);
        Vector2 posReina = new Vector2(reina.position.x, reina.position.y);
        float distancia = Vector2.Distance(posJugador, posReina);

        if (distancia <= distanciaPanico)
        {
            float factor = Mathf.InverseLerp(distanciaPanico, distanciaCritica, distancia);

            // 1. CÁMARA (Zoom relativo a TU zoom original)
            if (camaraVirtual != null)
            {
                float zoomObjetivoPanico = zoomOriginalJuego - cuantoAcercarZoom;
                camaraVirtual.Lens.OrthographicSize = Mathf.Lerp(zoomOriginalJuego, zoomObjetivoPanico, factor);

                // Bajar suavemente la visión para no cortar suelo
                if (composer != null)
                {
                    Vector3 currentOffset = composer.TargetOffset;
                    currentOffset.y = Mathf.Lerp(0f, 1f, factor);
                    composer.TargetOffset = currentOffset;
                }
            }

            // 2. MÚSICA
            if (musicaFondo != null)
            {
                musicaFondo.pitch = Mathf.Lerp(pitchNormal, pitchPanico, factor);
            }

            // 3. VISUALES (Se encienden y titilan rápido)
            float velocidadTitileo = Mathf.Lerp(8f, 20f, factor);
            float latido = (Mathf.Sin(Time.time * velocidadTitileo) + 1f) / 2f;

            if (fondoRojoUI != null)
            {
                SetAlfa(fondoRojoUI, alfaFondoBase);
            }

            if (marcoFuegoUI != null)
            {
                float alfaMinimo = Mathf.Lerp(0.4f, 0.7f, factor); 
                float alfaLatidoExtra = Mathf.Lerp(0.15f, 0.3f, factor) * latido; 
                SetAlfa(marcoFuegoUI, alfaMinimo + alfaLatidoExtra);
            }
        }
        else
        {
            // Volver al Zoom ORIGINAL de tu juego de forma fluida
            if (camaraVirtual != null)
            {
                camaraVirtual.Lens.OrthographicSize = Mathf.Lerp(camaraVirtual.Lens.OrthographicSize, zoomOriginalJuego, Time.deltaTime * 3f);
                
                if (composer != null)
                {
                    Vector3 currentOffset = composer.TargetOffset;
                    currentOffset.y = Mathf.Lerp(currentOffset.y, 0f, Time.deltaTime * 3f);
                    composer.TargetOffset = currentOffset;
                }
            }

            if (musicaFondo != null)
            {
                musicaFondo.pitch = Mathf.Lerp(musicaFondo.pitch, pitchNormal, Time.deltaTime * 3f);
            }

            if (fondoRojoUI != null) SetAlfa(fondoRojoUI, Mathf.Lerp(fondoRojoUI.color.a, 0f, Time.deltaTime * 4f));
            if (marcoFuegoUI != null) SetAlfa(marcoFuegoUI, Mathf.Lerp(marcoFuegoUI.color.a, 0f, Time.deltaTime * 4f));
        }
    }

    void SetAlfa(Image img, float alfa)
    {
        if (img == null) return;
        Color c = img.color;
        c.a = alfa;
        img.color = c;
    }
}