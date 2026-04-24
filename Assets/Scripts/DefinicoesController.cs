using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DefinicoesController : MonoBehaviour
{
    public Slider sliderMusica;
    public Slider sliderSons;
    public Toggle toggleEfeitos;

    void Start()
    {
        sliderMusica.value = PlayerPrefs.GetFloat("VolumeMusica", 1f);
        sliderSons.value = PlayerPrefs.GetFloat("VolumeSons", 1f);
        toggleEfeitos.isOn = PlayerPrefs.GetInt("Efeitos", 1) == 1;


        AplicarVolumes();
    }

    public void OnMusicaChanged()
    {
        PlayerPrefs.SetFloat("VolumeMusica", sliderMusica.value);
        PlayerPrefs.Save();
        AplicarVolumes();
    }

    public void OnSonsChanged()
    {
        PlayerPrefs.SetFloat("VolumeSons", sliderSons.value);
        PlayerPrefs.Save();
        AplicarVolumes();
    }

    public void OnEfeitosChanged()
    {
        PlayerPrefs.SetInt("Efeitos", toggleEfeitos.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
    void AplicarVolumes()
    {
        AudioListener.volume = sliderSons.value;

        if (SomManager.Instance != null)
            SomManager.Instance.audioSource.volume = sliderMusica.value;
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}