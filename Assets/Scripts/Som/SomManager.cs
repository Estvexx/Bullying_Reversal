using UnityEngine;

public class SomManager : MonoBehaviour
{
    public static SomManager Instance;

    public AudioClip somSalto;
    public AudioClip somRoll;
    public AudioClip somBook;
    public AudioClip musicaAmbiente;

    public AudioSource audioSource;

    public AudioSource audioSourceMusica;
    public AudioSource audioSourceEfeitos;

    void Awake()
    {
        Instance = this;
        AudioSource[] sources = GetComponents<AudioSource>();
        audioSourceMusica = sources[0];
        audioSourceEfeitos = sources[1];
    }

    void Start()
    {
        audioSourceMusica.volume = PlayerPrefs.GetFloat("VolumeMusica", 1f);
        audioSourceEfeitos.volume = PlayerPrefs.GetFloat("VolumeSons", 1f);

        audioSourceMusica.clip = musicaAmbiente;
        audioSourceMusica.loop = true;
        audioSourceMusica.Play();
    }

    public void TocarSalto() => audioSourceEfeitos.PlayOneShot(somSalto);
    public void TocarRoll() => audioSourceEfeitos.PlayOneShot(somRoll);
    public void TocarBook() => audioSourceEfeitos.PlayOneShot(somBook);
}

