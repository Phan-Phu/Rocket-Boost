using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] float timeDestroyDelay = 0.3f;
    [SerializeField] AudioClip audioBreak;
    [SerializeField] AudioClip audioFinish;

    [SerializeField] ParticleSystem particleBreak;
    [SerializeField] ParticleSystem particleFinish;
    [SerializeField] ParticleSystem particleSavePoint;

    private AudioSource audioSource;
    private bool isTransitioning;
    private bool enableCollision;
    private SavePoint savePoint;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        isTransitioning = false;
        enableCollision = true;
    }

    private void Update()
    {
        RespondDebugKey();
    }

    private void RespondDebugKey()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            enableCollision = true;
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            enableCollision = !enableCollision;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning || !enableCollision) { return; }

        switch (collision.gameObject.tag)
        {
            case Tags.Factory:
                StartSavePointSequence();
                break;
            case Tags.Friendly:
                break;
            case Tags.Finish:
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSavePointSequence()
    {
        if (particleSavePoint != null)
        {
            PlayParticle(particleSavePoint);
        }
    }

    private void StartSuccessSequence()
    {
        GetComponent<Movement>().enabled = false;

        PlayAudio(audioFinish);
        PlayParticle(particleFinish);

        savePoint = FindObjectOfType<SavePoint>();
        if (savePoint != null)
        {
            savePoint.IsDestroy = true;
        }

        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void LoadNextLevel()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (currentIndex == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }

        SceneControllerManager.Instance.FadeAndLoadScene(NameOfSceneByBuildIndex(currentIndex));
    }

    private static string NameOfSceneByBuildIndex(int buildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(buildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }

    private void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        PlayAudio(audioBreak);
        PlayParticle(particleBreak);

        Transform child = transform.GetChild(0);
        Destroy(child.gameObject, timeDestroyDelay);

        Invoke("ReloadLevel", levelLoadDelay);
    }

    private void PlayAudio(AudioClip audioClip)
    {
        isTransitioning = true;
        audioSource.PlayOneShot(audioClip);
    }
    
    private void PlayParticle(ParticleSystem particle)
    {
        particle.Play();
    }

    private void ReloadLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        SceneControllerManager.Instance.FadeAndLoadScene(sceneName);
    }
}
