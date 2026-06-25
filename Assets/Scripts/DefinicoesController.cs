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
        sliderMusica.value = PlayerPrefs.GetFloat(PlayerPrefsKeys.VolumeMusica, 1f);
        sliderSons.value = PlayerPrefs.GetFloat(PlayerPrefsKeys.VolumeSons, 1f);
        toggleEfeitos.isOn = PlayerPrefs.GetInt(PlayerPrefsKeys.Efeitos, 1) == 1;


        AplicarVolumes();
    }

    public void OnMusicaChanged()
    {
        PlayerPrefs.SetFloat(PlayerPrefsKeys.VolumeMusica, sliderMusica.value);
        PlayerPrefs.Save();
        AplicarVolumes();
    }

    public void OnSonsChanged()
    {
        PlayerPrefs.SetFloat(PlayerPrefsKeys.VolumeSons, sliderSons.value);
        PlayerPrefs.Save();
        AplicarVolumes();
    }

    public void OnEfeitosChanged()
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.Efeitos, toggleEfeitos.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
    void AplicarVolumes()
    {
        if (SomManager.Instance != null)
        {
            SomManager.Instance.AplicarVolumes(sliderMusica.value, sliderSons.value);
        }
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
