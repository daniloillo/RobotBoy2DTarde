using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret_1_Management : MonoBehaviour
{
    Animator animator;   
    [SerializeField] GameObject target;      
    [SerializeField] GameObject bullet;
    [SerializeField] Collider2D detector;
    [SerializeField] Transform canon;
    [SerializeField] LayerMask layers;
    public float distance;   
    Vector2 targetPos;
    public Vector2 direccion;
    Variables variables;
    



    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        variables = GameObject.Find("Variables").GetComponent<Variables>();

    }

    // Update is called once per frame
    void Update()
    {
        if (variables.alive == true)
        {
            DetectorPlayer();
        }
        else
        {
            animator.SetBool("Shooting", false);
        }
                       
    }



    void DetectorPlayer()
    {   //Direccion Torreta/Jugador
        targetPos = target.transform.position;
        direccion = targetPos - (Vector2)transform.position;
        //Detector
        detector = Physics2D.OverlapCircle(transform.position, distance, layers);
        if(detector != null)
        {
            //Direccion Apuntado            
            transform.up = direccion;
            animator.SetBool("Shooting", true);
        }
        else
        {
            animator.SetBool("Shooting", false);

        }

    }
    //Radio de Torreta Visible
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, distance);
    }


    //Disparo 
    void shoot()
    {
        Instantiate(bullet, canon.position, canon.rotation);

    }

        
}
