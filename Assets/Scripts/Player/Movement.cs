using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [SerializeField] private float mainThrust = 500f;
    [SerializeField] private float ZAngle = 100f;
    [SerializeField] private AudioClip audioThrust;
    [SerializeField] private ParticleSystem particleThrust;
    [SerializeField] private ParticleSystem particleThrustLeft;
    [SerializeField] private ParticleSystem particleThrustRight;

    private Touch theTouch;
    private Rigidbody rb;
    private BoxCollider boxCollider;
    private AudioSource audioSource;
    private bool isRouting = true;
    public float outTimeUseEnegy = 3f;

    private float _timeUseEnergy;
    public float TimeUseEnergy { get { return _timeUseEnergy; } set { _timeUseEnergy = value; } }


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloadFadeOutEvent += FadeOutScene;
        EventHandler.AfterSceneLoadFadeInEvent += FadeInScene;
    }
    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloadFadeOutEvent -= FadeOutScene;
        EventHandler.AfterSceneLoadFadeInEvent -= FadeInScene;
    }

    void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            ProccessInputAndroid();
        }
        else
        {
            ProccessInputPC();
        }
    }

    private void ProccessInputPC()
    {
        ProcessThurstPC();
        if (isRouting)
        {
            ProccessRotation();
        }
        // else
        // {
        //     RecoverEnegy();
        // }
    }

    private void ProcessThurstPC()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ProccessThurst();
            UseEnegy();
        }
        else
        {
            StopAudio();
        }
    }
    private void ProccessRotation()
    {
        if (!isRouting) { return; }

        rb.freezeRotation = true;

        Vector3 rotation = Vector3.forward * ZAngle;

        if (Input.GetKey(KeyCode.A))
        {
            RotationLeft(rotation);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotationRight(rotation);
        }
        rb.freezeRotation = false;
    }

    private void ProccessInputAndroid()
    {
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                theTouch = Input.GetTouch(i);
                if (theTouch.phase == TouchPhase.Stationary && theTouch.position.x > Screen.width / 2)
                {
                    ProccessThurst();
                    UseEnegy();
                }
            }
        }
        else
        {
            StopAudio();
        }

        if (isRouting)
        {
            RotationAndroid();
        }
        // else
        // {
        //     RecoverEnegy();
        // }

    }
    private void ProccessThurst()
    {
        isRouting = true;

        audioSource.clip = audioThrust;
        particleThrust.Play();

        float boost = mainThrust;
        rb.AddRelativeForce(Vector3.up * boost);

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    private void UseEnegy()
    {
        _timeUseEnergy += Time.deltaTime;
        if (_timeUseEnergy >= outTimeUseEnegy)
        {
            // if game object collision start point, check point => game not reset => error
            this.enabled = false;
            audioSource.Stop();
        }
    }
    private void StopAudio()
    {
        audioSource.Stop();
    }
    private void RotationAndroid()
    {
        Vector3 rotation = Vector3.forward * ZAngle;
        if (RotateRightConection.Instance.Connection == true)
        {
            RotationRight(rotation);
        }
        else if (RotateLeftConection.Instance.Connection == true)
        {
            RotationLeft(rotation);
        }
    }
    private void RotationLeft(Vector3 rotationAndroid)
    {
        transform.Rotate(rotationAndroid);
        particleThrustLeft.Play();
    }

    private void RotationRight(Vector3 rotationAndroid)
    {
        transform.Rotate(-rotationAndroid);
        particleThrustRight.Play();
    }

    // private void RecoverEnegy()
    // {
    //     if (_timeUseEnergy >= outTimeUseEnegy)
    //     {
    //         rb.Sleep();
    //     }
    //     else
    //     {
    //         rb.WakeUp();
    //     }
    // }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(Tags.Factory))
        {
            isRouting = false;
        }
    }

    // private void OnCollisionStay(Collision other)
    // {
    //     if (other.gameObject.CompareTag(Tags.Factory))
    //     {
    //         if (this.isActiveAndEnabled == false)
    //         {
    //             this.enabled = true;
    //         }
    //         if (_timeUseEnergy - Time.deltaTime < 0)
    //         {
    //             _timeUseEnergy = 0;
    //         }
    //         else
    //         {
    //             _timeUseEnergy -= Time.deltaTime;
    //         }
    //     }
    // }

    private void FadeInScene()
    {
        this.enabled = true;
    }
    private void FadeOutScene()
    {
        this.enabled = false;
    }
}