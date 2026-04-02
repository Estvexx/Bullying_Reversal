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
        ultimaPontuacaoText.text = "Ult Pontuação: " + PlayerPrefs.GetInt("UltimaPontuacao", 0);
        recordeText.text = "Recorde: " + PlayerPrefs.GetInt("Recorde", 0);
        booksText.text = "Livros: " + PlayerPrefs.GetInt("Books", 0);
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
        ultimaPontuacaoText.text = "Última Pontuação: 0";
        recordeText.text = "Recorde: 0";
        booksText.text = "Livros: 0";
        Debug.Log("Dados deletados!");
    }
}