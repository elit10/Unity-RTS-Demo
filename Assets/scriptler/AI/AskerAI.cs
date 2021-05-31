using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.AI;
public class AskerAI : MonoBehaviour
{
    public int SıraYatay;
    public int SıraDikey;
    public birlikAI Kaynak;
    public NavMeshAgent Agent;
    public bool HayattaMı = true;
    private RaycastHit Ateşhit;
    private RaycastHit Yakınhit;
    public float Can;
    public Animator Anim;

    private void Start()
    {
        if(gameObject.GetComponent<Animator>() != null)
        {
            Anim = gameObject.GetComponent<Animator>();
        }
    }

    void Awake()
    {
        Agent = gameObject.GetComponent<NavMeshAgent>();
        
        InvokeRepeating("Tekrar", 1,0.5f);
        
        
        Invoke("Başlat", 0.5f);



    }

    public void Tekrar()
    {
        if(HayattaMı)
        {
            Agent.SetDestination(Kaynak.gameObject.transform.GetChild(0).transform.position + new Vector3(SıraDikey * 1.5f, 0, SıraYatay * 1.5f));
            Agent.speed = Kaynak.BirlikHızı;
            Agent.acceleration = Kaynak.BirlikHızı * 2;

            if (Can <= 0)
            {
                Ölüm();
            }


        }
    }

    public void Başlat()
    {
        Can = Kaynak.BirlikCan;
        InvokeRepeating("Ateş", 1, 60 / Kaynak.DBA);


        InvokeRepeating("Yakın", 1, 30 / Kaynak.DBA);
    }


    public void Yakın()
    {
        if (Yakınhit.collider != null)
        {
            if (Yakınhit.collider.GetComponent<AskerAI>() != null)
            {
                if (Yakınhit.collider.GetComponent<AskerAI>().Kaynak != Kaynak)
                {


                    if (Kaynak.DüşmanMı == !Yakınhit.collider.GetComponent<AskerAI>().Kaynak.DüşmanMı)
                    {
                        if(Yakınhit.distance <= 2)
                        {
                            Yakınhit.collider.GetComponent<AskerAI>().Can -= Kaynak.BirlikHasar + Kaynak.BirlikHasar * Agent.velocity.magnitude;
                        }
                    }
                }
            }
        }
    }

    public void Ateş()
    {
        if(Ateşhit.collider != null)
        {
            if (Ateşhit.collider.GetComponent<AskerAI>() != null)
            {
                if (Ateşhit.collider.GetComponent<AskerAI>().Kaynak != Kaynak)
                {
                    

                    if(Kaynak.DüşmanMı)
                    {
                        if(!Ateşhit.collider.GetComponent<AskerAI>().Kaynak.DüşmanMı)
                        {
                            Ateşhit.collider.GetComponent<AskerAI>().Can -= Kaynak.BirlikHasar;
                            Debug.Log(gameObject.name + " " + Ateşhit.collider.name + "'i vurdu.");
                        }
                    }

                    if (!Kaynak.DüşmanMı)
                    {
                        if (Ateşhit.collider.GetComponent<AskerAI>().Kaynak.DüşmanMı)
                        {
                            Ateşhit.collider.GetComponent<AskerAI>().Can -= Kaynak.BirlikHasar;
                            Debug.Log(gameObject.name + " " + Ateşhit.collider.name + "'i vurdu.");
                        }
                    }

                }
            }
        }








    }





    public void Ölüm()
    {
        Agent.enabled = false;
        HayattaMı = false;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        
        
        if (HayattaMı)
        {
            if(Anim != null)
            {
                Anim.SetFloat("Hız", Agent.velocity.magnitude);
            }
            
            
            
            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward*Kaynak.Menzil, out Ateşhit))
            {
                Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * Kaynak.Menzil, Color.red);

            }

            if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward * 2, out Yakınhit))
            {
                Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * 2, Color.blue);

            }
        }
    }
}
