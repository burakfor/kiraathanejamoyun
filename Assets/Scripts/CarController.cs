using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public float MoveSpeed;
    public float Drag = 0.98f; 
    public float MaxSpeed = 15;
    public float SteerAngle = 20;
    public float Traction = 1;
    public float fixRotateSpeed;

    private Vector3 MoveForce;   
                

    void Update()
    {
        MoveForce += transform.forward * MoveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
        transform.position += MoveForce * Time.deltaTime;

        float steerInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerInput * MoveForce.magnitude * SteerAngle * Time.deltaTime);

        
        MoveForce *= Drag;
        MoveForce = Vector3.ClampMagnitude(MoveForce, MaxSpeed);

        Debug.DrawRay(transform.position, MoveForce.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
        MoveForce = Vector3.Lerp(MoveForce.normalized, transform.forward, Traction *Time.deltaTime) * MoveForce.magnitude;
    }
    private void LateUpdate()
    {
        if(transform.rotation.y>90)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(new Vector3(0,0,0)),Time.deltaTime*fixRotateSpeed);
        }
    }
}
