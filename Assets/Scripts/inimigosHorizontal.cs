using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigosHorizontal : MonoBehaviour
{

    private bool collide = false;

    public float move = -2;

    public float time = 0.0f;
    public float timer;
    public float velocidade2;
    //private bool dano = false;
    public float jumpForce = 700;

   
    void Start()
    {
        
    }

   
    void Update()
    {


        time += Time.deltaTime;


        GetComponent<Rigidbody2D>().velocity = new Vector2(move*velocidade2, GetComponent<Rigidbody2D>().velocity.y); // velocity.y mantem o eixo y locked

        if ((collide)|| (time>=timer))
        {
            Flip();
            time = 0f;
        }
        
    }

    private void Flip()
    {

        move *= -1; 
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        collide = false; // nao fazer o flip, apenas quando colidir de novo

    }


    void OnCollisionEnter2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("plataformas"))
        {
            collide = true;

        }

      
    }


     void OnCollisionExit2D(Collision2D col)
    {

        if (col.gameObject.CompareTag("plataformas"))
        {
            collide = false;

        }
   

    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {

            BoxCollider2D[] boxes = gameObject.GetComponents<BoxCollider2D>();
            foreach(BoxCollider2D box in boxes)
            {
                box.enabled = false;
            }

            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));
           


        }
        
    }




}





