using UnityEngine;
using System.Collections.Generic;

public class InimigoController : MonoBehaviour {
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int RollHash = Animator.StringToHash("Roll");
    private static readonly int StartRunHash = Animator.StringToHash("StartRun");
    private static readonly int IsGroundedHash = Animator.StringToHash("isGrounded");
    private static readonly int RirHash = Animator.StringToHash("rir");

    [SerializeField] private Player player;
    [SerializeField] private float intervalo = 0.075f;

    public float delay = 1f;
    public float zAproximacao = 2f;
    public float velocidadeAproximacao = 5f;

    private struct EstadoPlayer {
        public Vector3 posicao;
        public bool noChao;

        public EstadoPlayer(Vector3 posicao, bool noChao) {
            this.posicao = posicao;
            this.noChao = noChao;
        }
    }

    private readonly Queue<EstadoPlayer> estados = new Queue<EstadoPlayer>();
    private Animator animator;
    private Animator playerAnimator;
    private SkinnedMeshRenderer meshRenderer;
    private bool podeCorrer = false;
    private bool aRir = false;
    private float timer = 0f;

    private void OnEnable() {
        if (player == null) return;

        player.Jumped += ReplicarJump;
        player.Rolled += ReplicarRoll;
        player.StartedRunning += ComecarCorrida;
        player.Died += Rir;
    }

    private void OnDisable() {
        if (player == null) return;

        player.Jumped -= ReplicarJump;
        player.Rolled -= ReplicarRoll;
        player.StartedRunning -= ComecarCorrida;
        player.Died -= Rir;
    }

    private void Start() {
        animator = GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
        meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        meshRenderer.enabled = false;
    }

    private void FixedUpdate() {
        if (!podeCorrer) return;

        if (aRir) {
            AproximarDoPlayer();
            return;
        }

        if (!player.estaVivo) return;

        timer += Time.fixedDeltaTime;
        if (timer < intervalo) return;

        GuardarEstadoPlayer();
        ReproduzirComDelay();
        timer = 0f;
    }

    private void GuardarEstadoPlayer() {
        estados.Enqueue(new EstadoPlayer(
            player.transform.position,
            playerAnimator.GetBool(IsGroundedHash)
        ));
    }

    private void ReproduzirComDelay() {
        int estadosNecessarios = Mathf.RoundToInt(delay / intervalo);
        if (estados.Count <= estadosNecessarios) return;

        EstadoPlayer estado = estados.Dequeue();
        transform.position = estado.posicao;
        animator.SetBool(IsGroundedHash, estado.noChao);
    }

    private void AproximarDoPlayer() {
        Vector3 destino = new Vector3(
            transform.position.x,
            transform.position.y,
            player.transform.position.z - zAproximacao
        );

        transform.position = Vector3.MoveTowards(
            transform.position,
            destino,
            velocidadeAproximacao * Time.fixedDeltaTime
        );
    }

    private void ComecarCorrida() {
        meshRenderer.enabled = true;
        podeCorrer = true;
        StartCoroutine(TriggerComDelay(StartRunHash));
    }

    private void ReplicarJump() {
        StartCoroutine(TriggerComDelay(JumpHash));
    }

    private void ReplicarRoll() {
        StartCoroutine(TriggerComDelay(RollHash));
    }

    private System.Collections.IEnumerator TriggerComDelay(int triggerHash) {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger(triggerHash);
    }

    private void Rir() {
        aRir = true;
        estados.Clear();
        transform.position = new Vector3(transform.position.x, player.groundY, transform.position.z);
        animator.SetTrigger(RirHash);
    }
}
