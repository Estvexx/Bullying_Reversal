using UnityEngine;

public class SomManager : MonoBehaviour
{
    public static SomManager Instance;

    public AudioClip somSalto;
    public AudioClip somRoll;
    public AudioClip somBook;
    public AudioClip musicaAmbiente;

    public AudioSource audioSource;

    void Awake() //tem de vir antes d o start
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("VolumeMusica", 1f);
        AudioListener.volume = PlayerPrefs.GetFloat("VolumeSons", 1f);
        audioSource.clip = musicaAmbiente;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void TocarSalto() => audioSource.PlayOneShot(somSalto);
    public void TocarRoll() => audioSource.PlayOneShot(somRoll);
    public void TocarBook() => audioSource.PlayOneShot(somBook);
}