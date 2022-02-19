using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSLook : MonoBehaviour
{

    public Transform head;
    public Transform body;

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
        y_look = -y_look * sensitivity * Time.deltaTime;
        y_look = Mathf.Clamp(y_look, -90f, 90f);
        
        body.transform.Rotate(Vector3.up * x_look);
        head.transform.Rotate(y_look, 0f, 0f);
     }
}
