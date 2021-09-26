using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private float levelLoadDelay = 1f;

    PlayerController playerController;
    [SerializeField]
    ParticleSystem crashVFX2;

    private void Start() 
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter(Collision other) 
    {
       StartCrashSequence();
    }

    private void OnTriggerEnter(Collider other) 
    {
        Debug.Log($"{this.name} triggered {other.gameObject.name}");
    }

    private void StartCrashSequence()
    {
        playerController.enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        crashVFX2.Play();

        Invoke("ReloadLevel", levelLoadDelay);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
