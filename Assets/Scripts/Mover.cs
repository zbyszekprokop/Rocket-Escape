using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField]float rocketThrust =1300f;
    [SerializeField]float rotationSpeed = 100f;

    [SerializeField]AudioClip mainEngine;
    [SerializeField]ParticleSystem mainBooster;
    [SerializeField]ParticleSystem leftBooster;
    [SerializeField]ParticleSystem rightBooster;


    Rigidbody rb;
    AudioSource audioSource;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        ProcessThurst();
        ProcessRotation();
    }

    void ProcessThurst()
    {
        if(Input.GetKey(KeyCode.W))
        {
            StartThrusting();
        }
        else 
        {
            audioSource.Stop();
        }
    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.D))
        {
            RightBoosterThrust();
        }
        else if(Input.GetKey(KeyCode.A))
        {
            LeftBoosterThrust();
        }
    }
    void StartThrusting()
    {
        mainBooster.Play();
        rb.AddRelativeForce(Vector3.up * rocketThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }
    void RightBoosterThrust()
    {
        rightBooster.Play();
        ApplyRotation(rotationSpeed);
    }
    void LeftBoosterThrust()
    {
        leftBooster.Play();
        ApplyRotation(-rotationSpeed);
    }
    void ApplyRotation(float rotateAtFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.back * rotateAtFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }


}
