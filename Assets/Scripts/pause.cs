using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pause : MonoBehaviour
{


    bool ispause;
    public GameObject Painel;

    public AudioSource audio;
    public bool mute;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }



    void Pause()
    {

        Time.timeScale = 0;
        
        Painel.SetActive(true);
        audio.mute = false;



    }

    void Unpause()
    {
        Painel.SetActive(false);
        Time.timeScale = 1;
        audio.mute = false;




    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {

            ispause = !ispause;

            if (ispause)
            {
               
                Pause();
            }
            else
            {

                Unpause();
            }


        }
        
    }
}
