using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public TextMeshProUGUI ultimaPontuacaoText;
    public TextMeshProUGUI recordeText;
    public TextMeshProUGUI booksText;
    void Start()
    {
        ultimaPontuacaoText.text = "" + PlayerPrefs.GetInt("UltimaPontuacao", 0);
        recordeText.text = "" + PlayerPrefs.GetInt("Recorde", 0);
        booksText.text = "" + PlayerPrefs.GetInt("Books", 0);
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

    public void EliminarDados()
    {
        PlayerPrefs.DeleteAll();
        ultimaPontuacaoText.text = "0";
        recordeText.text = "0";
        booksText.text = "0";
    }
}