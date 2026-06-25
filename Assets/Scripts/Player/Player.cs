using UnityEngine;
using System;
using System.Collections;

public class Player : MonoBehaviour {
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int RollHash = Animator.StringToHash("Roll");
    private static readonly int StartRunHash = Animator.StringToHash("StartRun");
    private static readonly int IsGroundedHash = Animator.StringToHash("isGrounded");

    public event Action Jumped;
    public event Action Rolled;
    public event Action StartedRunning;
    public event Action Died;
    public static event Action RunStarted;

    public Transform groundCheck;
    public ParticleSystem poDosPassos;

    public float laneWidth = 2.5f;
    public float laneSpeed = 10f;
    public float gravity = 20f;
    public float gravityRoll = 80f;
    public float jumpHeight = 10f;
    public float velocidadeBase = 10f;
    public float velocidadeMaxima = 25f;
    public float fatorAumento = 0.1f;
    public float velocidadeAtual;
    public float groundY;
    public bool estaVivo = true;

    private bool efeitosAtivos;
    private bool poeiraAtiva;

    [SerializeField] private float tempoEsperaEntrada = 5f;
    [SerializeField] private float tempoRolagem = 0.72f;
    [SerializeField] private float distanciaGroundCheck = 0.4f;

    private Rigidbody rb;
    private Animator animator;
    private Coroutine rollCoroutine;

    private bool jogoIniciado;
    private bool estaARolar;
    private float centerX;
    private float currentX;
    private float jumpVelocity;
    private float tempoDeJogo;
    private int lane;

    private bool PodeJogar => estaVivo && jogoIniciado;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        centerX = currentX = rb.position.x;
        groundY = rb.position.y;
        velocidadeAtual = velocidadeBase;

        efeitosAtivos = PlayerPrefs.GetInt(PlayerPrefsKeys.Efeitos, 1) == 1;

        if (!efeitosAtivos && poDosPassos != null)
            poDosPassos.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        StartCoroutine(AnimacaoEntrada());
    }

    private void Update() {
        bool grounded = IsGrounded();

        if (efeitosAtivos) AtualizarPoeira(grounded);


        if (!PodeJogar) return;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            lane = Mathf.Max(lane - 1, -1);

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            lane = Mathf.Min(lane + 1, 1);

        if (grounded && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
            Saltar();

        if (!estaARolar && (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift)))
            IniciarRolagem();

        animator.SetBool(IsGroundedHash, grounded);
    }

    private void FixedUpdate() {
        if (!PodeJogar) return;

        float dt = Time.fixedDeltaTime;
        bool grounded = IsGrounded();

        tempoDeJogo += dt;
        velocidadeAtual = Mathf.Min(velocidadeBase + tempoDeJogo * fatorAumento, velocidadeMaxima);

        if (!grounded) {
            jumpVelocity -= (estaARolar ? gravityRoll : gravity) * dt;
        }
        else if (jumpVelocity < 0f) {
            jumpVelocity = 0f;
        }

        float targetX = centerX + lane * laneWidth;
        currentX = Mathf.MoveTowards(currentX, targetX, laneSpeed * dt);

        Vector3 pos = rb.position;
        pos.x = currentX;
        pos.z += velocidadeAtual * dt;
        pos.y += jumpVelocity * dt;

        if (pos.y < groundY) {
            pos.y = groundY;
            jumpVelocity = 0f;
        }

        rb.MovePosition(pos);
    }

    public void Morrer() {
        estaVivo = false;
        jumpVelocity = 0f;

        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.position = new Vector3(rb.position.x, groundY, rb.position.z);

        Died?.Invoke();
    }

    public void AjustarOrigem(float deslocamentoZ) {
        Vector3 pos = rb.position;
        pos.z += deslocamentoZ;
        rb.position = pos;
    }

    private void Saltar() {
        if (estaARolar) {
            StopCoroutine(rollCoroutine);
            estaARolar = false;
        }

        jumpVelocity = jumpHeight;
        animator.SetTrigger(JumpHash);
        Jumped?.Invoke();
        SomManager.Instance.TocarSalto();
    }

    private void IniciarRolagem() {
        rollCoroutine = StartCoroutine(Rolar());
        animator.SetTrigger(RollHash);
        Rolled?.Invoke();
        SomManager.Instance.TocarRoll();
    }

    private bool IsGrounded() => Physics.Raycast(groundCheck.position, Vector3.down, distanciaGroundCheck);

    private void AtualizarPoeira(bool grounded) {
        bool deveTocar = PodeJogar && grounded && !estaARolar;
        if (deveTocar == poeiraAtiva) return;

        poeiraAtiva = deveTocar;
        if (poeiraAtiva) poDosPassos.Play(); else poDosPassos.Stop();
    }

    private IEnumerator Rolar() {
        estaARolar = true;
        yield return new WaitForSeconds(tempoRolagem);
        estaARolar = false;
    }

    private IEnumerator AnimacaoEntrada() {
        yield return new WaitForSeconds(tempoEsperaEntrada);

        animator.SetTrigger(StartRunHash);
        jogoIniciado = true;
        StartedRunning?.Invoke();
        RunStarted?.Invoke();
    }
}
