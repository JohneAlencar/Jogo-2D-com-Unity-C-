using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dano_recebido : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision2D)
    {


        if (collision2D.gameObject.CompareTag("Player"))

            //Debug.Log("recebeu_dano" + collision2D.gameObject.tag);

        
        {
            GetComponent<Animator>().SetBool("explosao", true);
            
        }

    }




    }
