using UnityEngine;

public class DesactivarAlTerminar : MonoBehaviour
{
    // Este evento lo llamamos desde un Animation Event al final de "Escudo_Desaparecer"
    public void OcultarObjeto()
    {
        gameObject.SetActive(false);
    }
}