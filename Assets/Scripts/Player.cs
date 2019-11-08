using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;// biblioteca do text
using UnityEngine.SceneManagement; //controle de cenas



public class Player : MonoBehaviour {

    public float forcaPulo;
    public float velocidadeMaxima;
    public int vidas;
    public int diamonds;

    public AudioSource diamante;    // som pegar diamante;
    public AudioSource hit;
    public AudioSource dano;
    public AudioSource musicafase;
    public AudioSource musica_morrendo;
    public AudioSource musicaBoss;
    public AudioSource bossSound;
    public AudioSource michael;


    public float tempo_destruicao = 1; // tempo que inimigo é destruido após contato onTrigger

    public Text TextLives;
    public Text TextDiamante;

    public bool isGrounded = true;
    public bool vivo = true;
    public bool vitoria = false;

    public GameObject Painel;
    public GameObject imagemVitoria;

    public bool ispaused = false; //logica do pause



    void Start() {

        TextLives.text = vidas.ToString();
        TextDiamante.text = diamonds.ToString();
        musicafase.Play();
        vitoria = false;
    }



    void Update() {


        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();







        if (vivo == true)
        {

            float movimento = Input.GetAxis("Horizontal"); // cria variavel movimento, controle horizontal
            rigidbody.velocity = new Vector2(movimento * velocidadeMaxima, rigidbody.velocity.y); // usado somente no eixo x

            if (movimento < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true; // controle flip player esquerda
            }

            else if (movimento > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false; // controle flip player direita
            }

            if (movimento > 0 || movimento < 0) // controle de animaçao se player esta andando ou nao
            {
                GetComponent<Animator>().SetBool("walking", true);
            }

            else
            {
                GetComponent<Animator>().SetBool("walking", false);
            }


            if (Input.GetKeyDown(KeyCode.Space) && isGrounded) // espaço para pular e controle de pulo

            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forcaPulo)); // Adiciona força para player pular
                GetComponent<AudioSource>().Play(); // som do pulo
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Animator>().SetBool("pulando", true);
            }
            else
            {
                GetComponent<Animator>().SetBool("pulando", false);

            }


            if (Input.GetKeyDown(KeyCode.DownArrow) && isGrounded)
            {
                GetComponent<Animator>().SetBool("Abaixando", true);
            }

            if ((Input.GetKeyDown(KeyCode.LeftArrow) && isGrounded))
            {
                GetComponent<Animator>().SetBool("walking", true);
                GetComponent<Animator>().SetBool("Abaixando", false);
            }


            else if ((Input.GetKeyDown(KeyCode.RightArrow) && isGrounded))
            {
                GetComponent<Animator>().SetBool("walking", true);
                GetComponent<Animator>().SetBool("Abaixando", false);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {

                Invoke("reload", 0f);
            }





            if (Input.GetKeyDown(KeyCode.P))
            {

                if (ispaused)
                {
                    Painel.SetActive(false);
                    ispaused = false;
                    Time.timeScale = 1;
                    musicafase.mute = false;


                }

                else
                {

                    Painel.SetActive(true);
                    ispaused = true;
                    Time.timeScale = 0;
                    musicafase.mute = true;
                }
            }


            if (diamonds == 30)
            {



                musicafase.Pause();
                musicaBoss.Pause();
                bossSound.Pause();
                michael.Play();
                imagemVitoria.SetActive(true);
                Invoke("reload", 6f);

                vivo = false;
                GetComponent<Animator>().SetBool("walking", false);

                GetComponent<Animator>().SetBool("vitoria", true);
            }

        }

    }// fecha validacao vivo








    /* public void caixa(Collision2D collision2D)
     {
         if (collision2D.gameObject.CompareTag("caixa"))
         {
             isGrounded = true;
         }

         else
         {
             isGrounded = false;
         }
     }*/


    void OnCollisionEnter2D(Collision2D collision2D) //funções de colisoes entre player e elementos do cenario
    {

        


            if (collision2D.gameObject.CompareTag("caixa"))
        {

            //caixa(collision2D);
            isGrounded = true;
        }


        if (collision2D.gameObject.CompareTag("diamonds"))
        {
            diamante.Play();
            Destroy(collision2D.gameObject);
            diamonds++;
            TextDiamante.text = diamonds.ToString();
        }

        if (collision2D.gameObject.CompareTag("cherry"))
        {
            diamante.Play();
            Destroy(collision2D.gameObject);
            vidas++;
            TextLives.text = vidas.ToString();

        }


        /* if (collision2D.gameObject.CompareTag("caixa"))
         {
             isGrounded = true;
         }*/



        if (collision2D.gameObject.CompareTag("inimigos"))
        {

           
            vidas--;
            TextLives.text = vidas.ToString();
            GetComponent<Animator>().SetBool("tomandoDano", true);

            hit.Play();
            damage();
        }

        else
        {
            GetComponent<Animator>().SetBool("tomandoDano", false);

        }

        if (collision2D.gameObject.CompareTag("KingPig"))
        {
            vidas--;
            TextLives.text = vidas.ToString();
            GetComponent<Animator>().SetBool("Morrendo", true);
            hit.Play();
            damage();
        }

        else
        {
            GetComponent<Animator>().SetBool("Morrendo", false);

        }

        if (collision2D.gameObject.CompareTag("plataformas")) // controlar o pulo

        {
            isGrounded = true;
        }
        // Debug.Log("colidiu"+ collision2D.gameObject.tag); // Detecta colisoes e mostra no console

    }




    void OnCollisionExit2D(Collision2D collision2D)
    {

        // Debug.Log("parou de colidir" + collision2D.gameObject.tag);

        if (collision2D.gameObject.CompareTag("plataformas")) // controlar o pulo
        {
            isGrounded = false;
        }

        if (collision2D.gameObject.CompareTag("caixa"))
        {

            //caixa(collision2D);
            isGrounded = false;
        }


    }


    void OnTriggerEnter2D(Collider2D collision2D) // destruir inimigos
    {


       



        if (collision2D.gameObject.CompareTag("plataformas"))
        {

            isGrounded = true;
        }



        if (collision2D.gameObject.CompareTag("inimigos"))
        {
            dano.Play();

            BoxCollider2D[] boxes = collision2D.gameObject.GetComponents<BoxCollider2D>();

            foreach (BoxCollider2D box in boxes)
            {
                box.enabled = false;
            }

            Destroy(collision2D.gameObject, tempo_destruicao);

        }


        if (collision2D.gameObject.CompareTag("KingPig"))
        {
            dano.Play();

            BoxCollider2D[] boxes = collision2D.gameObject.GetComponents<BoxCollider2D>();

            foreach (BoxCollider2D box in boxes)
            {
                box.enabled = false;
            }

            bossSound.Pause();
            Destroy(collision2D.gameObject, tempo_destruicao);
            vitoria = true;



        }



        if (collision2D.gameObject.CompareTag("musicaBoss"))
        {
            musicafase.Pause();
            musicaBoss.Play();
        }

        if (collision2D.gameObject.CompareTag("bossSound"))
        {
            bossSound.Play();
        }



    }


    void OnTriggerExit2D(Collider2D collision2D)
    {




    }



    public void damage()
    {
        if (vidas == 0)
        {
            GetComponent<Animator>().SetBool("morto", true);
            vivo = false;
            musicafase.Pause();
            musicaBoss.Pause();
            musica_morrendo.Play();
            Invoke("reload", 6f);
        }

    }

    void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //funcao reiniciar a cena ao morrer
    }








    public void Pause() //funcao pause
    {

        if (ispaused)
        {
            Painel.SetActive(false);
            ispaused = false;
            Time.timeScale = 1;
            musicafase.mute = false;


        }

        else
        {

            Painel.SetActive(true);
            ispaused = true;
            Time.timeScale = 0;
            musicafase.mute = true;

        }

    }


    public void QuitGame()
    {

        Application.Quit();
    }





}
