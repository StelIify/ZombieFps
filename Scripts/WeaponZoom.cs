using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField]
    private float zoomInView = 35f;
    [SerializeField]
    private float zoomInSensetivity = 70f;
    private float zoomOutView;
    private float zoomOutSensetiviry;

    Camera cam;
    Animator anim;
    MouseLook mouse;
    private void Awake()
    {
        anim = FindObjectOfType<Weapon>().GetComponent<Animator>();
        cam = FindObjectOfType<Camera>();
        mouse = FindObjectOfType<MouseLook>().GetComponent<MouseLook>();
        zoomOutView = cam.fieldOfView;
        zoomOutSensetiviry = mouse.MouseSensetivity;
    }
    void Update()
    {
        Zoom();
    }

    private void Zoom()
    {
        if (Input.GetMouseButtonDown(1))
        {
            anim.SetBool("Aim", true);
            cam.fieldOfView = zoomInView;
            mouse.MouseSensetivity = zoomInSensetivity;
        }
        if (Input.GetMouseButtonUp(1))
        {
            anim.SetBool("Aim", false);
            cam.fieldOfView = zoomOutView;
            mouse.MouseSensetivity = zoomOutSensetiviry;
        }
    }
}
