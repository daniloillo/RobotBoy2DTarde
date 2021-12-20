using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretTier2_Management : MonoBehaviour
{
    Variables variables;
    [SerializeField] GameObject laser;
    [SerializeField] GameObject preLaser;
    [SerializeField] Transform canon;
    // Start is called before the first frame update
    void Start()
    {
        variables = GameObject.Find("Variables").GetComponent<Variables>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PreShoot()
    {
        Instantiate(preLaser, canon.position, Quaternion.identity);  
    }
    void Shoot()
    {
        Instantiate(laser, canon.position, Quaternion.identity);
    }
    
}
