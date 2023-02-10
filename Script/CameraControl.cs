using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//相机视角控制
public class CameraControl : MonoBehaviour
{
    [SerializeField] private List<Transform> allCelestialBodies = new List<Transform>();
    [SerializeField] private Camera _camera;
    public float rotateSpeed = 10f;
    
    private void Update()
    {
        transform.position = CalculateCenter(allCelestialBodies); 
        CameraControlByMouse();
    }

    //鼠标操控相机
    private void CameraControlByMouse()
    {
        float xMouse = Input.GetAxis("Mouse X") * rotateSpeed;
        float yMouse = Input.GetAxis("Mouse Y") * rotateSpeed;

        _camera.transform.localRotation *= Quaternion.Euler(-yMouse, xMouse, 0);
    }
    private Vector3 CalculateCenter(List<Transform> stars)
    {
        float xTotal = 0f;
        float yTotal = 0f;
        float zTotal = 0f;
        foreach (Transform t in stars)
        {
            xTotal += t.position.x;
            yTotal += t.position.y;
            zTotal += t.position.z;
        }

        float x = xTotal / stars.Count;
        float y = yTotal / stars.Count;
        float z = zTotal / stars.Count;

        Vector3 result = new Vector3(x, y, z);

        return result;
    }
}
