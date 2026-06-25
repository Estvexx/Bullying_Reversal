using UnityEngine;

public class PersonagemGameOver : MonoBehaviour
{
    private static readonly int Dance1Hash = Animator.StringToHash("dance1");
    private static readonly int Dance2Hash = Animator.StringToHash("dance2");
    private static readonly int Dance3Hash = Animator.StringToHash("dance3");

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }

    public void TocarDanca()
    {
        int ultimoScore = PlayerPrefs.GetInt(PlayerPrefsKeys.UltimaPontuacao, 0);

        if (ultimoScore <= 1000)
        {
            animator.Play(Dance1Hash);
        }
        else if (ultimoScore > 1000 && ultimoScore <= 2000)
        {
            animator.Play(Dance2Hash);
        }
        else
        {
            animator.Play(Dance3Hash);
        }
    }
}
