using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private static readonly int DieHash = Animator.StringToHash("die");

    [SerializeField] private Animator anim;
    [SerializeField] private GameController gc;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private BookManager bookManager;
    [SerializeField] private float tempoAnimacaoMorte = 3.5f;

    public int lives = 2;

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

            if (lives <= 0)
            {
                GameOver();
            }
        }
    }

    private void GameOver()
    {
        GetComponent<Player>().Morrer();
        scoreManager.PararScore();
        bookManager.PararContagem();
        anim.SetTrigger(DieHash);

        StartCoroutine(WaitAndPause());
    }

    private System.Collections.IEnumerator WaitAndPause()
    {
        yield return new WaitForSeconds(tempoAnimacaoMorte);
        Time.timeScale = 0f;
        gc.GameOverScreen();
    }
}
