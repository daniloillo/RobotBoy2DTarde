using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{   Turret_1_Management turret1;
    Rigidbody2D rb;
    Variables variables;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        variables = GameObject.Find("Variables").GetComponent<Variables>();
        Invoke("DeleteBullet", 4f);

    }

    // Update is called once per frame
    void Update()
    {
        if (variables.alive == true)
        {
            transform.Translate(Vector3.up * variables.bulletSpeed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        Destroy(gameObject);
        
    }
    void DeleteBullet()
    {
        Destroy(gameObject);
        
    }
}
