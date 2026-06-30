using UnityEngine;

public class LanguageManager : MonoBehaviour {
    public void SetPortuguese() {
        SetLanguage("PT");
    }

    public void SetEnglish() {
        SetLanguage("EN");
    }

    private void SetLanguage(string lang) {
        PlayerPrefs.SetString(PlayerPrefsKeys.Language, lang);
        PlayerPrefs.Save();

        foreach (TranslatedText text in FindObjectsByType<TranslatedText>(FindObjectsSortMode.None))
            text.Refresh();
    }
}