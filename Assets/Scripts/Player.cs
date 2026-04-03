using UnityEngine;

public class Player : MonoBehaviour
{
    public float laneWidth = 2.5f;
    public float laneSpeed = 10f;

    public float gravity = 20f;
    public float jumpHeight = 15f;

    public float velocidadeBase = 5f;
    public float fatorAumento = 0.1f;
    public float velocidadeMaxima = 20f;
    private float velocidadeAtual;

    private float tempoDeJogo = 0f;

    private Rigidbody rb;
    private Animator animator;

    private int lane = 0;
    private float centerX;
    private float groundY;
    private float currentX;
    private float jumpVelocity = 0f;

    public bool estaVivo = true;
    private bool estaIniciando = true;
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
        if (!estaVivo || estaIniciando) return;

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            lane = Mathf.Max(lane - 1, -1);

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            lane = Mathf.Min(lane + 1, 1);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            
            jumpVelocity = jumpHeight;
            animator.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.S) && IsGrounded() && !estaARolar)
        {
            StartCoroutine(Rolar());
            animator.SetTrigger("Roll");
        }

        animator.SetBool("isGrounded", IsGrounded());
    }

    void FixedUpdate()
    {
        if (!estaVivo || estaIniciando) return;

        tempoDeJogo += Time.fixedDeltaTime;

        velocidadeAtual = Mathf.Min(
            velocidadeBase + (tempoDeJogo * fatorAumento),
            velocidadeMaxima
        );

        if (!IsGrounded())
            jumpVelocity -= gravity * Time.fixedDeltaTime;
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

    bool IsGrounded() => rb.position.y <= groundY + 0.1f;

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

        // Depois transita para o Run
        animator.SetTrigger("StartRun");
        estaIniciando = false; // liberta o movimento
    }

    private System.Collections.IEnumerator Rolar()
    {
        estaARolar = true;
        yield return new WaitForSeconds(1.23f);
        estaARolar = false; 
    }
}