using UnityEngine;
using TMPro;

public class TranslatedText : MonoBehaviour {
    [SerializeField] private string key;

    private TextMeshProUGUI text;

    private void Awake() {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start() {
        Refresh();
    }

    public void Refresh() {
        text.text = Translation.Get(key);
    }
}