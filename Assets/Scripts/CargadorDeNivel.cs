using UnityEngine;
using UnityEngine.SceneManagement;

public class CargadorDeNivel : MonoBehaviour
{
    [SerializeField] private string nombreDeEscena;

    public void CargarSiguienteNivel()
    {
        SceneManager.LoadScene(nombreDeEscena);
    }
}
