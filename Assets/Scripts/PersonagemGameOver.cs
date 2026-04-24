using UnityEngine;

public class PersonagemGameOver : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void TocarDanca()
{
    int ultimoScore = PlayerPrefs.GetInt("UltimaPontuacao", 0);
    Debug.Log("Score: " + ultimoScore);
    Debug.Log("Animator ativo: " + animator.enabled);
    Debug.Log("GameObject ativo: " + gameObject.activeSelf);
    Debug.Log("Estado atual: " + animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);

    if (ultimoScore <= 1000)
    {
        Debug.Log("A disparar dance1");
        animator.Play("dance1");
        //animator.SetTrigger("dance1");
    }
    else if (ultimoScore > 1000 && ultimoScore <= 2000)
        animator.Play("dance2");
    else
        animator.Play("dance3");
}
}