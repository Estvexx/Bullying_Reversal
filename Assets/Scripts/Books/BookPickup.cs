using UnityEngine;

public class BookPickup : MonoBehaviour
{
    [SerializeField] private BookManager bookManager;
    [SerializeField] private GameObject efeitoMoeda;
    [SerializeField] private float tempoReativarLivro = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Book"))
        {
            bookManager.AdicionarLivro();
            SomManager.Instance.TocarBook();

            if (PlayerPrefs.GetInt(PlayerPrefsKeys.Efeitos, 1) == 1)
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
        yield return new WaitForSeconds(tempoReativarLivro);
        livro.SetActive(true);
    }
}
