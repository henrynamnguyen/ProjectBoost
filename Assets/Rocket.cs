using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;


    [SerializeField] float rcsThrust = 250f;
    [SerializeField] float mainThrust = 10f;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    enum State { Alive, Dying, Transcending };
    [SerializeField]State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; } //ignore collision when dead
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }

    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadFirstScene", levelLoadDelay);
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextScene", levelLoadDelay);
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(1);
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();

        }
        else if (!Input.GetKey(KeyCode.Space))
        {
            audioSource.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioSource.isPlaying) // so the audio frames do not layer on top of one another to create weird sound.
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();
    }

    private void RespondToRotateInput()
    {
        rigidBody.freezeRotation = true; //after this line, we assume manual control of the rotarion
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; //resume physics control of rotation
    }

}