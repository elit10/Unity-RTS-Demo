using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngineInternal;

public class KameraKontrol : MonoBehaviour
{
    public Vector3 Hedef;
    public int Hız;
    public birlikAI SeçiliBirlik;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Hedef = new Vector3(Input.GetAxis("Vertical"), -Input.GetAxis("Mouse ScrollWheel")*30, -Input.GetAxis("Horizontal"));


        gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, gameObject.transform.position + Hedef,Time.deltaTime * Hız);
    
        if(Input.GetButtonDown("Shift"))
        {
            Hız = Hız * 2;
        }
        if(Input.GetButtonUp("Shift"))
        {
            Hız = Hız / 2;
        }

        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if(hit.collider.GetComponent<birlikAI>() != null)
                {
                    if(!hit.collider.GetComponent<birlikAI>().DüşmanMı)
                    {
                        SeçiliBirlik = hit.collider.GetComponent<birlikAI>();
                    }

                    
                }
                if (hit.collider.GetComponent<birlikAI>() == null)
                {
                    SeçiliBirlik =null;

                }


            }



        }
        if (Input.GetMouseButtonDown(1))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if(hit.collider.tag != "Siper")
                {
                    SeçiliBirlik.gameObject.transform.position = hit.point;
                }
                if (hit.collider.tag == "Siper")
                {
                    hit.collider.GetComponent<SiperKontrol>().İçeridekiBirlik = SeçiliBirlik;
                    SeçiliBirlik.gameObject.transform.position = hit.collider.transform.position;
                }
            }
        }



    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if(SeçiliBirlik != null)
        {
            Gizmos.DrawWireSphere(SeçiliBirlik.gameObject.transform.position, 5);
        }
    }


}