using UnityEngine;
using System.Collections.Generic;

public class InimigoController : MonoBehaviour
{
    private Player player;
    private Animator animator;

    public float delay = 1f;
    private float intervalo = 0.01f;
    private float timer = 0f;
    public float zAproximacao = 2f;
    public float velocidadeAproximacao = 5f;
    private bool aRir = false;

    private Queue<Vector3> posicoes = new Queue<Vector3>();
    private Queue<bool> filaGrounded = new Queue<bool>();
    private Queue<string> filaTriggers = new Queue<string>();

    private bool podeCorrer = false;

    void Start()
    {
        player = FindFirstObjectByType<Player>();
        animator = GetComponent<Animator>();
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
    }

    void Update()
    {
        if (!podeCorrer) return;

        if (aRir)
        {
            Vector3 destino = new Vector3(
                transform.position.x,
                transform.position.y,
                player.transform.position.z - zAproximacao
            );

            transform.position = Vector3.MoveTowards(
                transform.position,
                destino,
                velocidadeAproximacao * Time.deltaTime
            );
            return;
        }


        if (!player.estaVivo || !podeCorrer) return;

        int posicoesNecessarias = Mathf.RoundToInt(delay / intervalo);

        if (posicoes.Count >= posicoesNecessarias)
        {
            Vector3 alvo = posicoes.Dequeue();
            transform.position = alvo;

            if (filaGrounded.Count > 0)
                animator.SetBool("isGrounded", filaGrounded.Dequeue());
        }
    }

    void FixedUpdate()
    {
        if (!player.estaVivo || !podeCorrer) return;

        timer += Time.fixedDeltaTime;
        if (timer >= intervalo)
        {
            posicoes.Enqueue(player.transform.position);
            filaGrounded.Enqueue(player.GetComponent<Animator>().GetBool("isGrounded"));
            timer = 0f;
        }
    }

    public void IniciarPerseguicao()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        podeCorrer = true;
    }

    public void ReplicarJump()
    {
        StartCoroutine(TriggerComDelay("Jump"));
    }

    public void ReplicarRoll()
    {
        StartCoroutine(TriggerComDelay("Roll"));
    }

    public void ReplicarStartRun()
    {
        StartCoroutine(TriggerComDelay("StartRun"));
    }

    private System.Collections.IEnumerator TriggerComDelay(string trigger)
    {
        yield return new WaitForSeconds(delay);
        animator.SetTrigger(trigger);
    }

    public void ExecutarRir()
    {
        aRir = true;
        posicoes.Clear();
        filaGrounded.Clear();
        animator.SetTrigger("rir");
    }
}