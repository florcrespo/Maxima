using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    [Header("Referencias UI")]
    public GameObject panelTutorial;
    public Image imagenPlaca;
    public Button botonContinuar;

    private bool tutorialActivo = false;

    void Awake()
    {
        // Singleton para llamarlo fácil desde cualquier lado
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (panelTutorial != null) panelTutorial.SetActive(false);

        // Hace que el botón cierre el tutorial al hacerle clic
        if (botonContinuar != null)
        {
            botonContinuar.onClick.AddListener(CerrarTutorial);
        }
    }

    void Update()
    {
        // Si el tutorial está activo y apretan Espacio o Enter, también se cierra
        if (tutorialActivo && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            CerrarTutorial();
        }
    }

    public void MostrarTutorial(Sprite placaSprite)
    {
        Time.timeScale = 0f; // Pausa el juego
        tutorialActivo = true;

        if (imagenPlaca != null) imagenPlaca.sprite = placaSprite;
        if (panelTutorial != null) panelTutorial.SetActive(true);
    }

    public void CerrarTutorial()
    {
        if (panelTutorial != null) panelTutorial.SetActive(false);
        tutorialActivo = false;
        Time.timeScale = 1f; // Reanuda el tiempo
    }
}
