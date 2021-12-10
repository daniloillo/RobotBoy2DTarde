using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagement : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    Variables variables;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        variables = GameObject.Find("Variables").GetComponent<Variables>();

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (variables.alive == true)
        {
            Movimiento();
            DetectorSuelo();
            Saltar();
        }
    }
    void Movimiento()
    {
        float desplX = Input.GetAxis("Horizontal");
        
        
        if(desplX != 0f  && animator.GetBool("Grounded"))
        {
            animator.SetBool("Movimiento", true);


            rb.AddForce(new Vector2 (1f, 0f) * desplX * variables.speedX,ForceMode2D.Force);

            animator.SetFloat("VelocityX", Mathf.Abs(desplX));
            
        }
        else
        {

        }

    }
    void Saltar()
    {   
        if (Input.GetKeyDown(KeyCode.W) && animator.GetBool("Grounded"))
        {

            animator.SetTrigger("Salto");

            rb.AddForce(new Vector2(0f, 1f) * variables.jumpForce, ForceMode2D.Impulse);

            animator.SetBool("Grounded", false);
            animator.SetBool("Aire", true);


            //Caida Jugador
            if (rb.velocity.y < -0.15f)
            {
                animator.SetBool("Caida", true);

            }
            else
            {

            }
        }
    }
    void DetectorSuelo()
    {
        float groundDistance = 0.15f;
        Debug.DrawRay(new Vector3 (transform.position.x, transform.position.y +0.15f, transform.position.z), Vector2.down * groundDistance, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance);
        if (hit.collider != null)
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
    



    
    


