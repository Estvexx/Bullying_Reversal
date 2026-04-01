using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

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
        scoreText.text = Mathf.FloorToInt(score) + " x" + multiplier;
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
        scoreText.gameObject.SetActive(false);
    }

}