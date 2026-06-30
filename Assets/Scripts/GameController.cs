using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public GameObject GameOver;
    public GameObject Button_Play;
    public GameObject Button_Back_to_Menu;
    public GameObject Tittle;
    public GameObject Character;
    public GameObject RawImage;
    public GameObject Background_Current_Score;
    public TextMeshProUGUI scoreFinalLabel;
    public TextMeshProUGUI scoreFinalText;
    public GameObject Background_Current_Books;
    public TextMeshProUGUI booksLabel;
    public TextMeshProUGUI booksFinalText;
    public GameObject Background_Recorde;
    public TextMeshProUGUI recordeFinalText;
    public TextMeshProUGUI recordeLabel;

    public PersonagemGameOver personagemGameOver;

    private void OnEnable() {
        PlayerHealth.AnyDeathAnimationFinished += GameOverScreen;
    }

    private void OnDisable() {
        PlayerHealth.AnyDeathAnimationFinished -= GameOverScreen;
    }

    public void GameOverScreen() {
        Time.timeScale = 0f;
        GameOver.SetActive(true);
        scoreFinalText.gameObject.SetActive(true);
        booksFinalText.gameObject.SetActive(true);
        Button_Play.SetActive(true);
        Button_Back_to_Menu.SetActive(true);
        Tittle.SetActive(true);
        Character.SetActive(true);
        RawImage.SetActive(true);
        Background_Current_Score.SetActive(true);
        Background_Current_Books.SetActive(true);
        scoreFinalLabel.gameObject.SetActive(true);
        booksLabel.gameObject.SetActive(true);
        recordeLabel.gameObject.SetActive(true);

        if (Background_Recorde != null)
            Background_Recorde.SetActive(true);

        scoreFinalText.text = "" + PlayerPrefs.GetInt(PlayerPrefsKeys.UltimaPontuacao, 0);
        booksFinalText.text = "" + PlayerPrefs.GetInt(PlayerPrefsKeys.UltimaBooks, 0);


        if (recordeFinalText != null) {
            recordeFinalText.gameObject.SetActive(true);
            recordeFinalText.text = "" + PlayerPrefs.GetInt(PlayerPrefsKeys.Recorde, 0);
        }


        personagemGameOver.TocarDanca();

    }

    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Jogo");
    }

    public void BackToMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuPrincipal");
    }
}
