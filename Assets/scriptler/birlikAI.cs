using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class birlikAI : MonoBehaviour
{
    public List<AskerAI> Askerler;
    public birlikAI YakınDüşman;
    private int SıraNumarası;

    [Header("Kişiselleştir")]

    public bool DüşmanMı;
    public int En;
    public int BirlikHızı;
    public int Menzil;
    public int DBA;
    public int BirlikCan;
    public int BirlikHasar;





    public void Start()
    {
        //AgentBirlik = GameObject.Find(gameObject.name + "Agent").GetComponent<NavMeshAgent>();


        //ASKERLERİN İÇİNDEKİ KAYNAKLARI ATAYAN KOD
        foreach (GameObject asker in GameObject.FindGameObjectsWithTag(gameObject.name))
        {
            AskerAI AskerAIkaynak = asker.GetComponent<AskerAI>();


            Askerler.Add(AskerAIkaynak);
            AskerAIkaynak.Kaynak = this;

        }
        

        //ASKERLERİ SIRAYA DİZİYORUZ
        for (int i = 0; i < En; i++)
        {

            Askerler[i + SıraNumarası * En].SıraYatay = i;
            Askerler[i + SıraNumarası * En].SıraDikey = SıraNumarası;
            if (i == (En - 1))
            {
                SıraNumarası++;
                i = 0;
                Askerler[i + SıraNumarası * En].SıraDikey = SıraNumarası;
            }

            
            
        }
    }
    void Update()
    {
    }
}
