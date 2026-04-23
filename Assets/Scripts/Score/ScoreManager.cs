using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public GameObject Background_Score;
    public TextMeshProUGUI scoreText;
    public GameObject Background_Multi;    
    public TextMeshProUGUI multiplierText;
    public GameObject Background_Books;
    public TextMeshProUGUI booksText;


    public float score = 0f;

    public float intervaloMultiplier = 10f; // iontervalo para aumentar o multiplicador
    public int multiplierMaximo = 5;
    private int multiplier = 1;
    private float tempoDecorrido = 0f;

    private bool jogoAtivo = true;
    private int scoreFinal = 0;

    void Update()
    {
        if (!jogoAtivo) return;

        tempoDecorrido += Time.deltaTime;

        if (tempoDecorrido >= intervaloMultiplier && multiplier < multiplierMaximo)
        {
            multiplier++;
            tempoDecorrido = 0f;
        }

        score += multiplier * Time.deltaTime * 10; // 10 é só para a pontucao ser mais rapide
        scoreText.text = (Mathf.FloorToInt(score)).ToString();
        multiplierText.text = multiplier.ToString();
    }

    public void PararScore()
    {
        scoreFinal = Mathf.FloorToInt(score);
        jogoAtivo = false;

        PlayerPrefs.SetInt("UltimaPontuacao", scoreFinal);

        if (scoreFinal > PlayerPrefs.GetInt("Recorde", 0))
        {
            PlayerPrefs.SetInt("Recorde", scoreFinal);
        }

        PlayerPrefs.Save();

        StartCoroutine(EsconderTexto());
    }

    private System.Collections.IEnumerator EsconderTexto()
    {
        yield return new WaitForSeconds(3.3f);
        Background_Score.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        Background_Multi.gameObject.SetActive(false);
        multiplierText.gameObject.SetActive(false);
        Background_Books.gameObject.SetActive(false);
        booksText.gameObject.SetActive(false);
    }

}