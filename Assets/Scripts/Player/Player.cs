using UnityEngine;

public class Player : MonoBehaviour
{
    private static readonly int JumpHash = Animator.StringToHash("Jump");
    private static readonly int RollHash = Animator.StringToHash("Roll");
    private static readonly int StartRunHash = Animator.StringToHash("StartRun");
    private static readonly int IsGroundedHash = Animator.StringToHash("isGrounded");

    private Rigidbody rb;
    private Animator animator;
    public Transform groundCheck; // objeto nos pés do meco
    public ParticleSystem poDosPassos;
    private Coroutine rollCoroutine;
    public float laneWidth = 2.5f;
    public float laneSpeed = 10f;

    public float gravity = 20f;
    public float gravityRoll = 80f;
    public float jumpHeight = 10f;

    public float velocidadeBase = 10f;
    public float velocidadeMaxima = 25f;
    public float fatorAumento = 0.1f;

    public float velocidadeAtual;

    private float tempoDeJogo = 0f;

    private int lane = 0;
    private float centerX;
    public float groundY;
    private float currentX;
    private float jumpVelocity = 0f;

    public bool estaVivo = true;
    private bool pausa_iniciarJogo = true;
    private bool estaARolar = false;

    [SerializeField] private float tempoEsperaEntrada = 5f;
    [SerializeField] private float tempoRolagem = 0.72f;
    [SerializeField] private float distanciaGroundCheck = 0.4f;
    [SerializeField] private InimigoController inimigo;
    [SerializeField] private ScoreManager scoreManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        centerX = rb.position.x;
        groundY = rb.position.y;
        currentX = centerX;
        velocidadeAtual = velocidadeBase;

        StartCoroutine(AnimacaoEntrada());

    }

    void Update()
    {
        AtualizarPoeira();

        if (!estaVivo || pausa_iniciarJogo) return;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            lane = Mathf.Max(lane - 1, -1);

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            lane = Mathf.Min(lane + 1, 1);

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)) && IsGrounded())
        {
            if (estaARolar)
            {
                StopCoroutine(rollCoroutine);
                estaARolar = false;
            }
            jumpVelocity = jumpHeight;
            animator.SetTrigger(JumpHash);
            inimigo.ReplicarJump();
            SomManager.Instance.TocarSalto();
        }

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift)) && !estaARolar)
        {
            rollCoroutine = StartCoroutine(Rolar());
            animator.SetTrigger(RollHash);
            SomManager.Instance.TocarRoll();
            inimigo.ReplicarRoll();
        }

        animator.SetBool(IsGroundedHash, IsGrounded());

    }

    void FixedUpdate()
    {
        if (!estaVivo || pausa_iniciarJogo) return;

        tempoDeJogo += Time.fixedDeltaTime;

        velocidadeAtual = Mathf.Min(
            velocidadeBase + (tempoDeJogo * fatorAumento),
            velocidadeMaxima
        );

        if (!IsGrounded())
        {
            if (estaARolar)
                jumpVelocity -= gravityRoll * Time.fixedDeltaTime;
            else
                jumpVelocity -= gravity * Time.fixedDeltaTime;
        }
        else if (jumpVelocity < 0f)
            jumpVelocity = 0f;

        float targetX = centerX + lane * laneWidth;
        currentX = Mathf.MoveTowards(currentX, targetX, laneSpeed * Time.fixedDeltaTime);

        Vector3 p = rb.position;
        p.z += velocidadeAtual * Time.fixedDeltaTime;
        p.x = currentX;
        p.y += jumpVelocity * Time.fixedDeltaTime;

        if (p.y < groundY)
        {
            p.y = groundY;
            jumpVelocity = 0f;
        }

        rb.MovePosition(p);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(groundCheck.position, Vector3.down, distanciaGroundCheck);
    }
    public void Morrer()
    {
        estaVivo = false;
        jumpVelocity = 0f;
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        Vector3 p = rb.position;
        p.y = groundY;
        rb.position = p;

        inimigo.ExecutarRir();


    }

    private System.Collections.IEnumerator AnimacaoEntrada()
    {
        yield return new WaitForSeconds(tempoEsperaEntrada);
        animator.SetTrigger(StartRunHash);
        inimigo.ReplicarStartRun();
        pausa_iniciarJogo = false;
        inimigo.IniciarPerseguicao();

        scoreManager.IniciarScore();
    }

    private System.Collections.IEnumerator Rolar()
    {
        estaARolar = true;
        yield return new WaitForSeconds(tempoRolagem);
        estaARolar = false;
    }

    void AtualizarPoeira()
    {
        if (PlayerPrefs.GetInt(PlayerPrefsKeys.Efeitos, 1) == 0)
        {
            poDosPassos.Stop();
            return;
        }

        if (!estaVivo || pausa_iniciarJogo)
        {
            poDosPassos.Stop();
            return;
        }

        if (IsGrounded() && !estaARolar)
        {
            if (!poDosPassos.isPlaying)
            {
                poDosPassos.Play();
            }
        }
        else
        {
            if (poDosPassos.isPlaying)
                poDosPassos.Stop();
        }
    }

}
