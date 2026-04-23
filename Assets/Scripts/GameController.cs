using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject GameOver;
    public GameObject Button_Play;
    public GameObject Button_Back_to_Menu;
    public GameObject Tittle;
    public GameObject Character;
    public GameObject Background_Current_Score;
    public TextMeshProUGUI scoreFinalText;
    public GameObject Background_Current_Books;
    public TextMeshProUGUI booksFinalText;

    void Start()
    {
    }

     public void GameOverScreen()
    {
        GameOver.SetActive(true);
        scoreFinalText.gameObject.SetActive(true);
        booksFinalText.gameObject.SetActive(true);
        Button_Play.SetActive(true);
        Button_Back_to_Menu.SetActive(true);
        Tittle.SetActive(true);
        Character.SetActive(true);
        Background_Current_Score.SetActive(true);
        Background_Current_Books.SetActive(true);

        scoreFinalText.text = "" + PlayerPrefs.GetInt("UltimaPontuacao", 0);
        booksFinalText.text = "" + PlayerPrefs.GetInt("UltimaBooks", 0);
        
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Jogo");
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}