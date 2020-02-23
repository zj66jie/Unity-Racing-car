using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class CarMove : MonoBehaviour {
    public WheelCollider flWheelCollider;
    public WheelCollider frWheelCollider;
    public WheelCollider rlWheelCollider;
    public WheelCollider rrWheelCollider;

    public Transform flWheelModel;//车轮旋转
    public Transform frWheelModel;
    public Transform rlWheelModel;
    public Transform rrWheelModel;

    public Transform flDiscBrake;//车轮转向
    public Transform frDiscBrake;

    public float brakeTorque = 10000;//刹车的力量
    public int[] speedArray;
    private bool isBreaking = false;
    private float currentSpeed;
    public float motorTorque = 20;//马力
    public float steerAngle = 10;//转向
    public float maxSpeed = 140;
    public float minSpeed = 30;
    public AudioSource daiji;
    public AudioSource jiasu;
    void Update()
    {
     
        currentSpeed = flWheelCollider.rpm * (flWheelCollider.radius * 2 * Mathf.PI) * 60 / 1000;
        if ((currentSpeed > maxSpeed && Input.GetAxis("Vertical") > 0) || (currentSpeed < -minSpeed && Input.GetAxis("Vertical") < 0))
        {
            flWheelCollider.motorTorque = 0;
            frWheelCollider.motorTorque = 0;
        }
        else
        {

            flWheelCollider.motorTorque = Input.GetAxis("Vertical") * motorTorque;
            frWheelCollider.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        }

     


        flWheelCollider.steerAngle = Input.GetAxis("Horizontal") * steerAngle;
        frWheelCollider.steerAngle = Input.GetAxis("Horizontal") * steerAngle;

        RotateWheel();// 转动车轮 
        SteerWheel();
        EngineSound();
       


        if (Input.GetKeyDown(KeyCode.Space))
        {
            isBreaking = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isBreaking = false;
        }  

        if (isBreaking)//刹车
        {
            flWheelCollider.motorTorque = 0;
            frWheelCollider.motorTorque = 0;
            rlWheelCollider.motorTorque = 0;
            rrWheelCollider.motorTorque = 0;

            flWheelCollider.brakeTorque = brakeTorque;
            frWheelCollider.brakeTorque = brakeTorque;
            rlWheelCollider.brakeTorque = brakeTorque;
            rrWheelCollider.brakeTorque = brakeTorque;
        }
        else
        {
            flWheelCollider.brakeTorque = 0;
            frWheelCollider.brakeTorque = 0;
            rlWheelCollider.brakeTorque = 0;
            rrWheelCollider.brakeTorque = 0;
            flWheelCollider.motorTorque = Input.GetAxis("Vertical") * motorTorque;
            frWheelCollider.motorTorque = Input.GetAxis("Vertical") * motorTorque;
            rlWheelCollider.motorTorque = Input.GetAxis("Vertical") * motorTorque;
            rrWheelCollider.motorTorque = Input.GetAxis("Vertical") * motorTorque;
        }
        RotateWheel();// 转动车轮 
        SteerWheel();//车轮转向
        EngineSound();
    }
    void RotateWheel()//车轮旋转
    {
        flDiscBrake.Rotate(flWheelCollider.rpm * 6 * Time.deltaTime * Vector3.right);
        frDiscBrake.Rotate(frWheelCollider.rpm * 6 * Time.deltaTime * Vector3.right);

        rlWheelModel.Rotate(rlWheelCollider.rpm * 6 * Time.deltaTime * Vector3.right);
        rrWheelModel.Rotate(rrWheelCollider.rpm * 6 * Time.deltaTime * Vector3.right);

    }
    void EngineSound()
    {
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            jiasu.Play();
        }
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    jiasu.Play();
        //}
        if (Input.GetKeyUp(KeyCode.W))
        {
            jiasu.Stop();
            daiji.Play();
        }
        //if (Input.GetKeyUp(KeyCode.S))
        //{
        //    jiasu.Stop();
        //    daiji.Play();
        //}



    }
    void SteerWheel()
    {//控制轮子的转向
        
        Vector3 localEulerAngles = flWheelModel.localEulerAngles;
        localEulerAngles.y = flWheelCollider.steerAngle;

        flWheelModel.localEulerAngles = localEulerAngles;
        frWheelModel.localEulerAngles = localEulerAngles;
    }
}

