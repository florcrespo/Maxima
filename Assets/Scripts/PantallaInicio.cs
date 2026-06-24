using UnityEngine;
using UnityEngine.InputSystem;

public class PantallaInicio : MonoBehaviour
{
    public GameObject pantallaInicio;
    public SecuenciaInicio secuenciaInicio;
    public GameObject corazon1;
    public GameObject corazon2;
    public GameObject corazon3;
    public GameObject indicadorMetros;
    public GameObject inventario;

    void Start()
    {
        corazon1.SetActive(false);
        corazon2.SetActive(false);
        corazon3.SetActive(false);
        indicadorMetros.SetActive(false);
        inventario.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            pantallaInicio.SetActive(false);
            secuenciaInicio.IniciarIntro();
            corazon1.SetActive(true);
            corazon2.SetActive(true);
            corazon3.SetActive(true);
            indicadorMetros.SetActive(true);
            inventario.SetActive(true);
        }
    }
}