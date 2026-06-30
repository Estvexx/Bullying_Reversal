using UnityEngine;
using System;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    private static readonly int DieHash = Animator.StringToHash("die");

    public event Action PlayerDied;
    public event Action DeathAnimationFinished;
    public static event Action AnyPlayerDied;
    public static event Action AnyDeathAnimationFinished;
    public static event Action AnyCornerHit;

    [SerializeField] private Animator anim;
    [SerializeField] private Player player;
    [SerializeField] private float tempoAnimacaoMorte = 3.5f;

    [SerializeField] private int maxBatidasSeguidas = 2;
    [SerializeField] private float tempoEntreBatidas = 10f;
    [SerializeField] private float limiteBatidaFrontal = 0.7f;

    private int batidasSeguidas;
    private float tempoUltimaBatida = -999f; // Este valor serve como garantia para primeira batida
    private bool gameOverAtivo;

    private void Awake() {
        if (player == null)
            player = GetComponent<Player>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (!collision.gameObject.CompareTag("Obstacle")) return;

        ContactPoint contact = collision.contacts[0];
        float dot = Mathf.Abs(Vector3.Dot(contact.normal, Vector3.forward));

        if (dot >= limiteBatidaFrontal) {
            GameOver();
            return;
        }

        RegistarBatidaDeEsquina();
    }

    private void RegistarBatidaDeEsquina() {
        if (Time.time - tempoUltimaBatida > tempoEntreBatidas)
            batidasSeguidas = 0;

        tempoUltimaBatida = Time.time;
        batidasSeguidas++;

        AnyCornerHit?.Invoke();

        if (batidasSeguidas >= maxBatidasSeguidas)
            GameOver();
    }

    private void GameOver() {
        if (gameOverAtivo) return;

        gameOverAtivo = true;
        player.Morrer();

        PlayerDied?.Invoke();
        AnyPlayerDied?.Invoke();

        anim.SetTrigger(DieHash);
        StartCoroutine(WaitAndPause());
    }

    private IEnumerator WaitAndPause() {
        yield return new WaitForSeconds(tempoAnimacaoMorte);

        DeathAnimationFinished?.Invoke();
        AnyDeathAnimationFinished?.Invoke();
    }
}