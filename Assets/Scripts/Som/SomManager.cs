using UnityEngine;

public class SomManager : MonoBehaviour
{
    public static SomManager Instance;

    [SerializeField] private AudioClip somSalto;
    [SerializeField] private AudioClip somRoll;
    [SerializeField] private AudioClip somBook;
    [SerializeField] private AudioClip musicaAmbiente;

    [SerializeField] private AudioSource audioSourceMusica;
    [SerializeField] private AudioSource audioSourceEfeitos;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        AplicarVolumes(
            PlayerPrefs.GetFloat(PlayerPrefsKeys.VolumeMusica, 1f),
            PlayerPrefs.GetFloat(PlayerPrefsKeys.VolumeSons, 1f)
        );

        audioSourceMusica.clip = musicaAmbiente;
        audioSourceMusica.loop = true;
        audioSourceMusica.Play();
    }

    public void AplicarVolumes(float volumeMusica, float volumeEfeitos)
    {
        audioSourceMusica.volume = volumeMusica;
        audioSourceEfeitos.volume = volumeEfeitos;
    }

    public void TocarSalto() => audioSourceEfeitos.PlayOneShot(somSalto);
    public void TocarRoll() => audioSourceEfeitos.PlayOneShot(somRoll);
    public void TocarBook() => audioSourceEfeitos.PlayOneShot(somBook);
}

