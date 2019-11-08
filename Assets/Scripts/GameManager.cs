using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject Painel;

    public bool ispaused = false; //logica do pause

    AudioSource  som;

	
	void Start () {

        som = GetComponent<AudioSource>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Pause() {

        if (ispaused)
        {
            Painel.SetActive(false);
            ispaused = false;
            Time.timeScale = 1;
            som.mute = false;


        }

        else {

            Painel.SetActive(true);
            ispaused = true;
            Time.timeScale = 0;
            som.mute = true;

        }

    }
}
