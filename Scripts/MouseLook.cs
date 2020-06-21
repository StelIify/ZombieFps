using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField]
    private float mouseSensetiviy = 100f;

    public float MouseSensetivity
    {
        get { return mouseSensetiviy; }

        set {
                if (value > 0)
                mouseSensetiviy = value;
        }
    }

    [SerializeField] Transform playerBody;
    float xRotation = 0f;

    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensetiviy * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensetiviy * Time.deltaTime;

        xRotation  -=mouseY;
        xRotation = Mathf.Clamp(xRotation, -70f, 70f);

        playerBody.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
       
        
        
    }
}
