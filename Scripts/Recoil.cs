using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    void Update()
    {
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(Vector3.zero), 0.5f);
    }

    public void AddRecoil(Vector3 recoil)
    {
        transform.localRotation *= Quaternion.Euler(recoil);
    }
}
