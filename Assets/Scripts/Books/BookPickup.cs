    using UnityEngine;

    public class BookPickup : MonoBehaviour
    {
        private BookManager bookManager;

        void Start()
        {
            bookManager = FindFirstObjectByType<BookManager>();
        }

        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Book"))
        {
            bookManager.AdicionarLivro();
            other.gameObject.SetActive(false);
        }
    }
    }