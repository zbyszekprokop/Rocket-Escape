using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandlerer : MonoBehaviour
{
    [SerializeField] float invokeTime = 1f;
    [SerializeField]AudioClip crashSound;
    [SerializeField]AudioClip successSound;
    [SerializeField]ParticleSystem crashParticles;
    [SerializeField]ParticleSystem successParticles;

    AudioSource audioSource;
    bool isTransitioning = false;
    bool collisionDisable = false;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update() 
    {
        SkipLevel();
        TurnOffCollisions();
    }
    void OnCollisionEnter(Collision other) 
    {

        if(isTransitioning) // same as isTransitioning == true;
        {
            return;
        }
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Bumped into a friendly thing!");
                break;
            case "Finish":
                ChangeLevelSequence();
                break;
            default:
                CrashSequence();
                break;
            
        }
    }

    void ChangeLevelSequence()
    {
        isTransitioning = true;
        successParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(successSound);
        GetComponent<Mover>().enabled = false;
        Invoke("NextLevel", invokeTime);
    }
    void CrashSequence()
    {
        isTransitioning = true;
        crashParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);
        GetComponent<Mover>().enabled = false;
        Invoke("RestartLevel", invokeTime);
    }
    void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex +1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void SkipLevel()
    {
        if(Input.GetKey(KeyCode.L))
        {
            NextLevel();
        }
    }
    void TurnOffCollisions()
    {
        if(Input.GetKey(KeyCode.C) && !collisionDisable)
        {
            GetComponent<BoxCollider>().enabled = false;
            collisionDisable = true;
        }
        else if (Input.GetKey(KeyCode.C) && collisionDisable)
        {
            GetComponent<BoxCollider>().enabled = true;
            collisionDisable = false;
        }
    }
}
