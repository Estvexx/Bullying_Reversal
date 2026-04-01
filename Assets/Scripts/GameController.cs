using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject GameOver;
    public GameObject Button_Play;
    public GameObject Button_Back_to_Menu;

    public TextMeshProUGUI scoreFinalText;

    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    public void GameOverScreen()
    {
        GameOver.SetActive(true);
        Button_Play.SetActive(true);
        Button_Back_to_Menu.SetActive(true);
        scoreFinalText.gameObject.SetActive(true); 

        scoreFinalText.text = "" + PlayerPrefs.GetInt("UltimaPontuacao", 0);
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