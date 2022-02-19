using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Transform body;
    public float speed = 10f;
    
    int x_val = 1;
    int y_val = 1;
   
   void Awake() {
       rb = GetComponent<Rigidbody>();
   }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() {
        x_val = (int)Input.GetAxisRaw("Horizontal");
        y_val = (int)Input.GetAxisRaw("Vertical");    
        MoveCharacter(x_val, y_val);
    }

    void MoveCharacter(int x_val, int z_val) {
        
        Vector3 movement = Vector3.zero;
        if(x_val != 0) {
            movement += body.transform.right * x_val;
        }
        if(z_val != 0) {
            movement += body.transform.forward * z_val;
        }
        if(z_val != 0 && x_val != 0) {
            movement = movement.normalized;
        }

        movement = movement * speed * Time.deltaTime;
        rb.MovePosition(body.position + movement);
    }

    void JumpCharacter() {
        
    }

}
