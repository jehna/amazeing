using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ender : MonoBehaviour
{
    public AudioClip winSound;

    bool ended = false;
    void OnTriggerEnter(Collider other)
    {
        if (ended)
        {
            return;
        }
        ended = true;
        GetComponent<AudioSource>().PlayOneShot(winSound);
        FindFirstObjectByType<ParticleSystem>().Play();
        PlayerPrefs.SetInt("level", (PlayerPrefs.GetInt("level", 0) + 1) % MazeGenerator.sizes.Length);
        FindFirstObjectByType<Rigidbody>().isKinematic = true;
        FindFirstObjectByType<Player>().enabled = false;
        FindFirstObjectByType<Player>().GetComponent<Renderer>().enabled = false;

        StartCoroutine(RestartLevel());
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
