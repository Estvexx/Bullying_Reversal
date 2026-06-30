using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour {
    public GameObject Background_Score;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI scoreText;
    public GameObject Background_Multi;
    public TextMeshProUGUI multiplierText;
    public GameObject Background_Books;

    public TextMeshProUGUI booksLabel;
    public TextMeshProUGUI booksText;


    public float score = 0f;

    public float intervaloMultiplier = 10f; // iontervalo para aumentar o multiplicador
    public int multiplierMaximo = 5;
    [SerializeField] private float pontosPorSegundo = 10f;
    [SerializeField] private float tempoParaEsconderTexto = 3.3f;
    private int multiplier = 1;
    private float tempoDecorrido = 0f;

    private bool jogoAtivo = true;
    private int scoreFinal = 0;

    private bool jogoIniciado = false;

    private void OnEnable() {
        Player.RunStarted += IniciarScore;
        PlayerHealth.AnyPlayerDied += PararScore;
    }

    private void OnDisable() {
        Player.RunStarted -= IniciarScore;
        PlayerHealth.AnyPlayerDied -= PararScore;
    }

    public void IniciarScore() {
        jogoIniciado = true;
    }

    void Update() {
        if (!jogoAtivo || !jogoIniciado) return;

        tempoDecorrido += Time.deltaTime;

        if (tempoDecorrido >= intervaloMultiplier && multiplier < multiplierMaximo) {
            multiplier++;
            tempoDecorrido = 0f;
        }

        score += multiplier * Time.deltaTime * pontosPorSegundo;
        scoreText.text = (Mathf.FloorToInt(score)).ToString();
        multiplierText.text = multiplier.ToString();
    }

    public void PararScore() {
        if (!jogoAtivo) return;

        scoreFinal = Mathf.FloorToInt(score);
        jogoAtivo = false;

        PlayerPrefs.SetInt(PlayerPrefsKeys.UltimaPontuacao, scoreFinal);

        if (scoreFinal > PlayerPrefs.GetInt(PlayerPrefsKeys.Recorde, 0)) {
            PlayerPrefs.SetInt(PlayerPrefsKeys.Recorde, scoreFinal);
        }

        PlayerPrefs.Save();

        StartCoroutine(EsconderTexto());
    }

    private System.Collections.IEnumerator EsconderTexto() {
        yield return new WaitForSeconds(tempoParaEsconderTexto);
        Background_Score.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        scoreLabel.gameObject.SetActive(false);
        Background_Multi.gameObject.SetActive(false);
        multiplierText.gameObject.SetActive(false);
        Background_Books.gameObject.SetActive(false);
        booksLabel.gameObject.SetActive(false);
        booksText.gameObject.SetActive(false);
    }

}
