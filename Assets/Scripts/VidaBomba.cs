using System.Collections;
using UnityEngine;

public class VidaBomba : MonoBehaviour
{

    public GameObject bomba;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Vida());
    }

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(Vida());
    }

    IEnumerator Vida()
    {
        Destroy(bomba);
        yield return new WaitForSeconds(0.5f);
        Destroy(this.gameObject);
    }
}
