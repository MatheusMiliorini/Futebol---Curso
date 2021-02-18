using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSegue : MonoBehaviour
{
    [SerializeField] private Transform objE, objD, bola;
    private float t = 0;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.jogoComecou)
        {
            if (!bola && GameManager.instance.bolasEmCena > 0)
            {
                bola = GameObject.FindWithTag("bola").GetComponent<Transform>();
            }
            else if (GameManager.instance.bolasEmCena > 0)
            {
                Vector3 posCam = transform.position;
                posCam.x = bola.position.x;
                posCam.x = Mathf.Clamp(posCam.x, objE.position.x, objD.position.x);
                transform.position = posCam;
            }
            else if (GameManager.instance.bolasEmCena == 0 && transform.position.x != objE.position.x)
            {
                t += 0.08f * Time.deltaTime;
                float smoothedX = Mathf.SmoothStep(Camera.main.transform.position.x, objE.position.x, t);
                transform.position = new Vector3(smoothedX, transform.position.y, transform.position.z);

                if (t >= 1)
                    t = 0;
            }
        }
    }
}
