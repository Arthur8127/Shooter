using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    public Transform cameraRoot;
    public Transform playerRoot;
    public float sensX, sensY;
    private float hor, ver;
    public float minVer = -90f, maxVer = 75f;

    

    private void Update()
    {
        hor += Input.GetAxis("Mouse X") * sensX * Time.deltaTime;
        ver -= Input.GetAxis("Mouse Y") * sensY * Time.deltaTime;
        ver = Mathf.Clamp(ver, minVer, maxVer);

        playerRoot.rotation = Quaternion.Euler(Vector3.up * hor);
        cameraRoot.localRotation = Quaternion.Euler(Vector3.right * ver);
    }


}
