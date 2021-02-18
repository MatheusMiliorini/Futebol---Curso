using UnityEngine;

public class BombaManager : MonoBehaviour
{
    [SerializeField]
    private GameObject bombaFx;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bola"))
        {
            GameObject _bombaFx = Instantiate(bombaFx, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
            _bombaFx.GetComponent<VidaBomba>().bomba = this.gameObject;
        }
    }
}
