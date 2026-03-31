using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public float score = 0f;

    public float velocidadeBase = 5f;
    public float fatorAumento = 0.1f;
    public float velocidadeMaxima = 20f;

    public float VelocidadeAtual { get; private set; }

    private bool jogoAtivo = true;

    void Start()
    {
        VelocidadeAtual = velocidadeBase;
    }

    void Update()
    {
        if (!jogoAtivo) return;

        VelocidadeAtual = Mathf.Min(
            velocidadeBase + (Time.time * fatorAumento),
            velocidadeMaxima
        );

        score += VelocidadeAtual * Time.deltaTime;
        Debug.Log("Score: " + Mathf.FloorToInt(score));
        Debug.Log("Velocidade: " + VelocidadeAtual.ToString("F2"));
    }

public void PararScore()
{
    jogoAtivo = false;
    VelocidadeAtual = 0f;
}
    public int GetScore() => Mathf.FloorToInt(score);

    
}