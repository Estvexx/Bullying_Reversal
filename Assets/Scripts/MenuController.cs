using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ultimaPontuacaoText;
    [SerializeField] private TextMeshProUGUI recordeText;
    [SerializeField] private TextMeshProUGUI booksText;

    void Start()
    {
        ultimaPontuacaoText.text = "" + PlayerPrefs.GetInt(PlayerPrefsKeys.UltimaPontuacao, 0);
        recordeText.text = "" + PlayerPrefs.GetInt(PlayerPrefsKeys.Recorde, 0);
        booksText.text = "" + PlayerPrefs.GetInt(PlayerPrefsKeys.TotalBooks, 0);
    }

    public void Jogar()
    {
        SceneManager.LoadScene("Jogo");
    }

    public void Sair()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void AbrirDefinicoes()
    {
        SceneManager.LoadScene("Definicoes");
    }

    public void EliminarDados()
    {
        PlayerPrefs.DeleteAll();
        ultimaPontuacaoText.text = "0";
        recordeText.text = "0";
        booksText.text = "0";
    }
}
