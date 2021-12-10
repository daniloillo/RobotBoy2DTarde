using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    Variables variables;
    //Enfriamiento Voltereta
    bool cd = true;   

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        variables = GameObject.Find("Variables").GetComponent<Variables>();
        //El juego comienza con Correr = False
        animator.SetBool("Correr", false);


    }

    // Update is called once per frame
    void FixedUpdate()
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
            if (!animator.GetBool("Correr"))
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
        if (Input.GetButtonDown("Saltar") && animator.GetBool("Grounded") && animator.GetBool ("De Pie"))
        {

            animator.SetTrigger("Salto");           

            rb.AddForce(new Vector2(0f, 1f) * variables.jumpForce, ForceMode2D.Impulse);

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
    {   //Agacharse
        if (Input.GetButton("Agacharse") && animator.GetBool("Grounded"))
        {

            animator.SetBool("Agachado", true);
            animator.SetBool("De Pie", false);


        }
        //Levantarse
        else
        {
            animator.SetBool("Agachado", false);
            animator.SetBool("De Pie", true);
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
                rb.AddForce(new Vector2(desplX, 0f) * 10f, ForceMode2D.Impulse);
                Invoke("VolteretaCooldown", 1f);
            }
        }
        else
        {

        }
    }
    //Cooldown de Voltereta
    public void VolteretaCooldown()
    {
        print("Espera");
        cd = true;
    }

    
    void DetectorSuelo()
    {   //LongitudRayCast
        float groundDistance = 0.02f;
        //RayCastIzquierda
        Debug.DrawRay(new Vector3 (transform.position.x-0.24f, transform.position.y+0.12f, transform.position.z), Vector2.down * groundDistance, Color.red);
        RaycastHit2D hitIzq = Physics2D.Raycast(new Vector2 (transform.position.x - 0.24f, transform.position.y + 0.12f), Vector2.down, groundDistance);       
        //RayCastDerecha
        Debug.DrawRay(new Vector3(transform.position.x+0.24f, transform.position.y + 0.12f, transform.position.z), Vector2.down * groundDistance, Color.red);
        RaycastHit2D hitDcha = Physics2D.Raycast(new Vector2 (transform.position.x + 0.24f, transform.position.y + 0.12f), Vector2.down, groundDistance);
        //NOTAS: En el RaycastHit si ponemos transform.position, aunque visualmente el rayo esté movido, realmente está colocado en el centro del objeto
        if (hitIzq.collider != null || hitDcha.collider != null)
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
    



    
    


