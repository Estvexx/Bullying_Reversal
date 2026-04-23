using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;
    public Transform groundCheck; // objeto nos pés do meco
    private Coroutine rollCoroutine;
    public float laneWidth = 2.5f;
    public float laneSpeed = 10f;

    public float gravity = 20f;
    public float gravityRoll = 80f;
    public float jumpHeight = 10f;

    public float velocidadeBase = 10f;
    public float velocidadeMaxima = 25f;
    public float fatorAumento = 0.1f;

    private float velocidadeAtual;

    private float tempoDeJogo = 0f;

    private int lane = 0;
    private float centerX;
    private float groundY;
    private float currentX;
    private float jumpVelocity = 0f;

    public bool estaVivo = true;
    private bool pausa_iniciarJogo = true;
    private bool estaARolar = false;
    public float tempoAnimacaoEntrada = 20f;



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
            animator.SetTrigger("Jump");
        }

        if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.LeftShift)) && !estaARolar)
        {
            rollCoroutine = StartCoroutine(Rolar());
            animator.SetTrigger("Roll");
        }

        animator.SetBool("isGrounded", IsGrounded());

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
        return Physics.Raycast(groundCheck.position, Vector3.down, 0.4f);
    }
    public void Morrer()
    {
        estaVivo = false;
        jumpVelocity = 0f;
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
    }

    private System.Collections.IEnumerator AnimacaoEntrada()
    {
        yield return new WaitForSeconds(5);
        animator.SetTrigger("StartRun");
        pausa_iniciarJogo = false;

        FindObjectOfType<ScoreManager>().IniciarScore();
    }

    private System.Collections.IEnumerator Rolar()
    {
        estaARolar = true;
        yield return new WaitForSeconds(0.72f);
        estaARolar = false;
    }
    void JumpDebug()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key pressed");
            if (IsGrounded())
            {
                Debug.Log("Player is grounded");
            }
            else
            {
                Debug.Log("Player is in the air");
            }
        }
    }
}   