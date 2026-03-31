using UnityEngine;

public class Player : MonoBehaviour
{
    public float laneWidth = 2.5f;
    public float laneSpeed = 10f;

    public float gravity = 20f;
    public float jumpHeight = 15f;

    private Rigidbody rb;
    private ScoreManager scoreManager;

    private int lane = 0;
    private float centerX;
    private float groundY;
    private float currentX;
    private float jumpVelocity = 0f;

    public bool estaVivo = true;

    void Start()
    {
        if (!estaVivo) return;
        rb = GetComponent<Rigidbody>();
        scoreManager = FindObjectOfType<ScoreManager>();

        centerX = rb.position.x;
        groundY = rb.position.y;
        currentX = centerX;
    }

    void Update()
    {
        if (!estaVivo) return;
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            lane = Mathf.Max(lane - 1, -1);

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            lane = Mathf.Min(lane + 1, 1);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            jumpVelocity = jumpHeight;
    }

    void FixedUpdate()
    {
        if (!estaVivo) return;
        
        if (!IsGrounded())
            jumpVelocity -= gravity * Time.fixedDeltaTime;
        else if (jumpVelocity < 0f)
            jumpVelocity = 0f;

        float targetX = centerX + lane * laneWidth;
        currentX = Mathf.MoveTowards(currentX, targetX, laneSpeed * Time.fixedDeltaTime);

        Vector3 p = rb.position;
        p.z += scoreManager.VelocidadeAtual * Time.fixedDeltaTime;
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
    rb.linearVelocity = Vector3.zero; // zera qualquer velocidade residual do Rigidbody
    rb.isKinematic = true; // desativa a física completamente
}
}