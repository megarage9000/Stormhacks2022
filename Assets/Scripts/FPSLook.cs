using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLook : MonoBehaviour
{

    public Transform head;
    public Transform body;
    
    float rotationY = 0f;
    public float sensitivity = 1000f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x_look = Input.GetAxis("Mouse X");
        float y_look = Input.GetAxis("Mouse Y");

        GetLookAxis(x_look, y_look);
    }

    void GetLookAxis(float x_look, float y_look) {

        Vector3 x_axis_look = body.transform.up;
        Vector3 y_axis_look = head.transform.right;

        x_look = x_look * sensitivity * Time.deltaTime;
        body.transform.Rotate(Vector3.up * x_look);
        
        Vector3 headRot = head.transform.localRotation.eulerAngles;
        rotationY  -= y_look * sensitivity * Time.deltaTime;
        rotationY = Mathf.Clamp(rotationY, -90f, 90f);
        head.transform.localRotation = Quaternion.Euler(rotationY, 0f, 0f);
     }
}
