using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Animator anim;
    public int lives = 2;

    private GameController gc;
    private ScoreManager scoreManager;

    void Start()
    {
        // so tenho este objeto com o script game controller
        gc = FindObjectOfType<GameController>();
        scoreManager = FindObjectOfType<ScoreManager>();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Obstacle")) return;

        // a seta do impacto
        Vector3 impactNormal = collision.contacts[0].normal;

        // quão de frente foi o impacto (0 = esquina, 1 = frente total)
        float dot = Mathf.Abs(Vector3.Dot(impactNormal, Vector3.forward));

        if (dot >= 0.7f)
        {
            // bateu mesmo de frente
            GameOver();
        }
        else
        {
            // bateu de esquina
            lives--;
            Debug.Log("Lives remaining: " + lives);
            Debug.Log("Batida de esquina! Cuidado!");

            if (lives <= 0)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        GetComponent<Player>().Morrer();
        FindFirstObjectByType<ScoreManager>().PararScore();
        anim.SetTrigger("die");

        StartCoroutine(WaitAndPause());
    }

    private System.Collections.IEnumerator WaitAndPause()
    {
        yield return new WaitForSeconds(3.5f); // tempo da animação
        Time.timeScale = 0f;
        gc.GameOverScreen();
    }
}