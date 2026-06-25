using UnityEngine;
using TMPro;

public class BookManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI booksText;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private float tempoParaEsconderTexto = 3.3f;

    private int livros = 0;
    private int Books_Coletados = 0;
    private int QntAtualBooks;
    private bool contagemParada = false;

    private void OnEnable() {
        if (playerHealth != null)
            playerHealth.PlayerDied += PararContagem;
    }

    private void OnDisable() {
        if (playerHealth != null)
            playerHealth.PlayerDied -= PararContagem;
    }

    public void AdicionarLivro() {
        livros++;
        booksText.text = "" + livros;
    }

    public void PararContagem() {
        if (!contagemParada) {
            Books_Coletados = Mathf.FloorToInt(livros);
            PlayerPrefs.SetInt(PlayerPrefsKeys.UltimaBooks, Books_Coletados);
            QntAtualBooks = PlayerPrefs.GetInt(PlayerPrefsKeys.TotalBooks, 0);
            QntAtualBooks += Books_Coletados;
            PlayerPrefs.SetInt(PlayerPrefsKeys.TotalBooks, QntAtualBooks);

            PlayerPrefs.Save();
            StartCoroutine(EsconderTexto());
            contagemParada = true;
        }
    }

    private System.Collections.IEnumerator EsconderTexto() {
        yield return new WaitForSeconds(tempoParaEsconderTexto);
        booksText.gameObject.SetActive(false);
    }
}
