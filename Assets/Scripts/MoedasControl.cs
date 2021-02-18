using UnityEngine;

public class MoedasControl : MonoBehaviour
{
    public int valor = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bola"))
        {
            ScoreManager.instance.ColetaMoedas(valor);
            AudioManager.instance.SonsFXToca(0);
            Destroy(this.gameObject);
        }
    }
}
