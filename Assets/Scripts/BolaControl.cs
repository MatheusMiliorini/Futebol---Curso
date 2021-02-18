using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BolaControl : MonoBehaviour
{
    // Angulo
    public float zRotate;
    // Morte Bola
    public GameObject morteBolaAnim;
    // Libera Rotação
    public bool liberaRot = false;
    public bool liberaTiro = false;
    public GameObject setaGO;

    private Rigidbody2D bola;
    private float forca = 0;
    public GameObject seta2;
    private Rigidbody2D rb;
    private bool atirou;

    private Transform paredeE, paredeD;


    private void Awake()
    {
        setaGO = GameObject.Find("Seta");
        seta2 = setaGO.transform.GetChild(0).gameObject;
        AlteraStatusSeta(false);

        paredeE = GameObject.Find("ParedeE").transform;
        paredeD = GameObject.Find("ParedeD").transform;

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        bola = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputRotacao();
        RotacaoSeta();
        PosicionaSeta();

        ControlaForca();
        AplicaForca();
        ValidaParedes();

        DestroiBolaParada();
    }

    void AlteraStatusSeta(bool ativo)
    {
        setaGO.GetComponent<Image>().enabled = ativo;
        seta2.GetComponent<Image>().enabled = ativo;
    }

    void PosicionaSeta()
    {
        setaGO.GetComponent<Image>().rectTransform.position = transform.position;
    }

    void RotacaoSeta()
    {
        setaGO.GetComponent<Image>().rectTransform.eulerAngles = new Vector3(0, 0, zRotate);
    }

    void InputRotacao()
    {
        if (liberaRot)
        {
            float movY = Input.GetAxis("Mouse Y");

            if (movY > 0 && zRotate < 90)
            {
                zRotate += 2.5f;
            }
            else if (movY < 0 && zRotate > 0)
            {
                zRotate -= 2.5f;
            }
        }
    }

    private void OnMouseDown()
    {
        if (GameManager.instance.tiro == 0)
        {
            liberaRot = true;
            AlteraStatusSeta(true);
        }
    }

    private void OnMouseUp()
    {
        liberaRot = false;
        AlteraStatusSeta(false);

        if (GameManager.instance.tiro == 0 && forca > 0)
        {
            liberaTiro = true;
            AudioManager.instance.SonsFXToca(1);
            GameManager.instance.tiro = 1;
        }
    }

    void AplicaForca()
    {
        float x = forca * Mathf.Cos(zRotate * Mathf.Deg2Rad);
        float y = forca * Mathf.Sin(zRotate * Mathf.Deg2Rad);

        if (liberaTiro)
        {
            bola.AddForce(new Vector2(x, y));
            liberaTiro = false;
            // Reseta força
            zRotate = 0;
            seta2.GetComponent<Image>().fillAmount = 0;
            // Marca atirou como true
            StartCoroutine(AtirouTrue());
        }
    }

    IEnumerator AtirouTrue()
    {
        yield return new WaitForSeconds(0.5f);
        atirou = true;
    }

    void ControlaForca()
    {
        if (liberaRot)
        {
            float movX = Input.GetAxis("Mouse X");
            if (movX < 0)
            {
                seta2.GetComponent<Image>().fillAmount += 0.8f * Time.deltaTime;
            }
            else if (movX > 0)
            {
                seta2.GetComponent<Image>().fillAmount -= 0.8f * Time.deltaTime;
            }

            forca = seta2.GetComponent<Image>().fillAmount * 1000;
        }
    }

    // Acionado pela animação
    public void BolaDinamica()
    {
        this.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }

    void ValidaParedes()
    {
        if (transform.position.x > paredeD.position.x)
        {
            DestroiBola();
        }
        else if (transform.position.x < paredeE.position.x)
        {
            DestroiBola();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("morte"))
        {
            DestroiBola();
        }
        else if (collision.CompareTag("win"))
        {
            GameManager.instance.win = true;
            int aux = OndeEstou.instance.fase;
            PlayerPrefs.SetInt("FASE_" + (aux - OndeEstou.instance.primeiraFase + 2), 1);
        }
    }

    void DestroiBola()
    {
        Instantiate(morteBolaAnim, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
        GameManager.instance.bolasEmCena -= 1;
        GameManager.instance.bolasNum -= 1;
    }

    void DestroiBolaParada()
    {
        float threshold = .1f;

        if (atirou)
        {
            if (rb.velocity.x <= threshold && rb.velocity.y <= threshold)
            {
                DestroiBola();
            }
        }
    }
}
