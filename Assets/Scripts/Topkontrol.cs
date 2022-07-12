using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Topkontrol : MonoBehaviour
{
    public UnityEngine.UI.Button btn;
    public UnityEngine.UI.Text Zaman, Can,Durum;
    private Rigidbody rg;
    public float Hiz = 1.5f;
    float zamanSayaci = 30;
    int canSayaci = 3;
    bool oyunDevam = true;
    bool oyunTamam = false;
    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (oyunDevam && !oyunTamam) { 
        zamanSayaci -= Time.deltaTime; // zamanSayacı = zamanSayacı - time.deltaTime;
        Zaman.text = (int)zamanSayaci + "";
        }
        else if(!oyunTamam)
        {
            Durum.text = "Oyun Tamamlanamadı";
            btn.gameObject.SetActive(true);
        }
        if (zamanSayaci < 0) oyunDevam = false;
    }

    private void FixedUpdate()
    {
        if (oyunDevam && !oyunTamam) { 
        float yatay = Input.GetAxis("Horizontal");
        float dikey = Input.GetAxis("Vertical");
        Vector3 kuvvet = new Vector3(-dikey,0,yatay);
        rg.AddForce(kuvvet*Hiz);
        }
        else
        {
            rg.velocity = Vector3.zero;
            rg.angularVelocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
      string objname =  collision.gameObject.name;
        if (objname.Equals("Plane"))
        {
            oyunTamam = true;
            Durum.text = "Oyun Tamamlandı";
            btn.gameObject.SetActive(true);
            //print("Oyun Tamamlandı");   
        }
        else if(!objname.Equals("zemin") && !objname.Equals("labZemin"))
        {
            canSayaci -= 1;
            Can.text = canSayaci + "";
            if (canSayaci == 0) oyunDevam = false;
        }
    }
}
