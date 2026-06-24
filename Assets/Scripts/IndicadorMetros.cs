using UnityEngine;
using UnityEngine.UI;

public class IndicadorMetros : MonoBehaviour
{
    public Transform maxima;
    public Transform meta;
    public Sprite[] sprites;

    private Image imagen;
    private float posicionInicial;

    void Start()
    {
        imagen = GetComponent<Image>();
        posicionInicial = maxima.position.x;
    }

    void Update()
    {
        float distanciaTotal = meta.position.x - posicionInicial;
        float recorrido = maxima.position.x - posicionInicial;
        float progreso = Mathf.Clamp01(recorrido / distanciaTotal);
        int indice = Mathf.RoundToInt(progreso * 10);
        indice = Mathf.Clamp(indice, 0, sprites.Length - 1);
        imagen.sprite = sprites[indice];
    }
}