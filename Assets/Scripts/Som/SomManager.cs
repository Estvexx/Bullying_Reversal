using UnityEngine;

public class SomManager : MonoBehaviour
{
    public static SomManager Instance;

    public AudioClip somSalto;
    public AudioClip somRoll;
    public AudioClip somBook;

    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void TocarSalto() => audioSource.PlayOneShot(somSalto);
    public void TocarRoll() => audioSource.PlayOneShot(somRoll);
    public void TocarBook() => audioSource.PlayOneShot(somBook);
}