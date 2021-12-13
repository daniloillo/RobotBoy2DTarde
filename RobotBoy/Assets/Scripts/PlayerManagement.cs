using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    Variables variables;
    BoxCollider2D bc; 
        
    //Enfriamiento Voltereta
    bool cd = true;   

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        bc = gameObject.GetComponent<BoxCollider2D>();
        variables = GameObject.Find("Variables").GetComponent<Variables>();
        //El juego comienza con Correr = False
        animator.SetBool("Correr", false);
        


    }

    // Update is called once per frame
    void Update()
    {
        if (variables.alive == true)
        {
            Movimiento();
            Correr();           
            Saltar();
            Agacharse();
            Voltereta();
            DetectorSuelo();


        }
    }
    
    void Movimiento()
    {
        float desplX = Input.GetAxis("Horizontal");

        if (desplX != 0f)
        {   //Movimiento Estandar
            if (!animator.GetBool("Correr") && (!animator.GetBool("Agachado")))
            {
                transform.Translate(Vector3.right * Time.deltaTime * desplX * variables.speedX, Space.World);
            }
                      

            //Convertir Velocidad a Variable de Animator
            animator.SetFloat("VelocityX", Mathf.Abs(desplX));

            //Voltear Sprite
            Vector3 EscalaX = transform.localScale;
            if(desplX > 0f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            

        }
        else
        {

        }

    }
    void Correr()
    {   //Correr se activa pulsando Shift y se mantendrá activo hasta que el jugador baje la velocidad o se detenga
        float desplX = Input.GetAxis("Horizontal");
        if (Input.GetButton("Correr"))
        {
            animator.SetBool("Correr", true);

        }
        else
        {

        }
           
        
        if ((desplX >= 0.1f || desplX <= -0.1f) && animator.GetBool("Correr") && animator.GetBool("Grounded"))
        {
            transform.Translate(Vector3.right * Time.deltaTime * desplX * variables.speedX * 2, Space.World);
        }
        else
        {
            animator.SetBool("Correr", false);
        }

    }
    void Saltar()
    {   //Salto

        if(cd == true)
        {
            if (Input.GetButtonDown("Saltar") && animator.GetBool("Grounded"))
            {
                cd = false;

                animator.SetTrigger("Salto");

                rb.AddForce(new Vector2(0f, 1f) * variables.jumpForce, ForceMode2D.Impulse);

                Invoke("Cooldown", 0.5f);

            }
            else
            {

            }
        }
        

        //Caida Jugador
        if (rb.velocity.y < -0.1f)
        {
            animator.SetBool("Caida", true);

        }
        else
        {

        }
    }

    void Agacharse()
    {
        float desplX = Input.GetAxis("Horizontal");
        //Agacharse
        if (Input.GetButton("Agacharse") && animator.GetBool("Grounded"))
        {

            animator.SetBool("Agachado", true);

            //Cambia el tamaño del BoxCollider
            bc.offset = new Vector2(0.35f, 0.88f);
            bc.size = new Vector2(1.3f, 1.5f);
        }
        //Levantarse
        else
        {
            animator.SetBool("Agachado", false);

            //Devuelve el tamano del BoxCollider           
            bc.offset = new Vector2(0f, 1.23f);
            bc.size = new Vector2(0.5f, 2.2f);

        }
        //Velocidad Agachado.
        if (animator.GetBool("Agachado") && animator.GetBool("Grounded"))
        {
            transform.Translate(Vector3.right * Time.deltaTime * desplX * variables.speedX * 0.4f, Space.World);
        }
        else
        {
            
        }
    }
    void Voltereta()
    {   //Voltereta con tiempo de recarga (para evitar interacciones no deseadas)

        float desplX = Input.GetAxis("Horizontal");
        if (cd == true)
        {
            if (Input.GetButtonDown("Voltereta") && animator.GetBool("Grounded"))
            {
                cd = false;
                animator.SetTrigger("Voltereta");
                rb.AddForce(new Vector2(1f, 0f) * 15f * desplX, ForceMode2D.Impulse);

                
                //Cooldown
                Invoke("Cooldown", 0.5f);
                //Parada Voltereta
                Invoke("FrenoVoltereta", 0.4f);

            }
        }
        else
        {

        }
    }
    //Cooldown de Voltereta y Salto
    public void Cooldown()
    {
        cd = true;

    }
    public void FrenoVoltereta()
    {
        rb.velocity = new Vector2(0f,rb.velocity.y);
    }

    
    void DetectorSuelo()
    {   //LongitudRayCast
        float groundDistance = 0.02f;
        //RayCastIzquierda
        Debug.DrawRay(new Vector3 (transform.position.x-0.20f, transform.position.y+0.12f, transform.position.z), Vector2.down * groundDistance, Color.red);
        RaycastHit2D hitIzq = Physics2D.Raycast(new Vector2 (transform.position.x - 0.20f, transform.position.y + 0.12f), Vector2.down, groundDistance);       
        //RayCastDerecha
        Debug.DrawRay(new Vector3(transform.position.x+0.10f, transform.position.y + 0.12f, transform.position.z), Vector2.down * groundDistance, Color.red);
        RaycastHit2D hitDcha = Physics2D.Raycast(new Vector2 (transform.position.x + 0.20f, transform.position.y + 0.12f), Vector2.down, groundDistance);
        //NOTAS: En el RaycastHit si ponemos transform.position, aunque visualmente el rayo esté movido, realmente está colocado en el centro del objeto

        if ((hitIzq.collider != null)||( hitDcha.collider != null))
        {
            animator.SetBool("Caida", false);
            animator.SetBool("Grounded", true);
            
        }
        else
        {
            animator.SetBool("Grounded", false);
        }

    }

    
  
}
    



    
    


