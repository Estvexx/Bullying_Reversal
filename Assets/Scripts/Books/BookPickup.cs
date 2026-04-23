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

            GameObject efeito = Instantiate(efeitoMoeda, other.transform.position, Quaternion.identity);
            Destroy(efeito, 1f);

            other.gameObject.SetActive(false);
        }
    }
}