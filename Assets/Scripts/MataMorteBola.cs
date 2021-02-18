using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MataMorteBola : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Mata());
    }

    IEnumerator Mata()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
}
