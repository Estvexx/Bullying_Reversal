using UnityEngine;
using TMPro;

public class BookManager : MonoBehaviour
{
    public TextMeshProUGUI booksText;
    private int livros = 0;
    private int Books_Coletados = 0;
    private int QntAtualBooks;

    
public void AdicionarLivro()
{
    livros++;
    booksText.text = "" + livros;
    Debug.Log("AdicionarLivro chamado! Stack: " + System.Environment.StackTrace);
}
    public void PararContagem()
    {
        Books_Coletados = Mathf.FloorToInt(livros);
        QntAtualBooks = PlayerPrefs.GetInt("Books", 0);
        QntAtualBooks += Books_Coletados;
        PlayerPrefs.SetInt("Books", QntAtualBooks);        
        
        PlayerPrefs.Save();
        StartCoroutine(EsconderTexto());
    }

    private System.Collections.IEnumerator EsconderTexto()
    {
        yield return new WaitForSeconds(3.3f);
        booksText.gameObject.SetActive(false);
    }
}