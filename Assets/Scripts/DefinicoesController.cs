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
        if (SomManager.Instance != null)
        {
            SomManager.Instance.audioSourceMusica.volume = sliderMusica.value;
            SomManager.Instance.audioSourceEfeitos.volume = sliderSons.value;
        }
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}