using UnityEngine;

public class BookPickup : MonoBehaviour
{
    private BookManager bookManager;
    public GameObject efeitoMoeda;

    void Start()
    {
        bookManager = FindFirstObjectByType<BookManager>();
    }

    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Book"))
    {
        bookManager.AdicionarLivro();
        SomManager.Instance.TocarBook();

        if (PlayerPrefs.GetInt("Efeitos", 1) == 1)
        {
            GameObject efeito = Instantiate(efeitoMoeda, other.transform.position, Quaternion.identity);
            Destroy(efeito, 1f);
        }

        StartCoroutine(ReativarLivro(other.gameObject));
    }
}

private System.Collections.IEnumerator ReativarLivro(GameObject livro)
{
    livro.SetActive(false);
    yield return new WaitForSeconds(10f);
    livro.SetActive(true);
}
}