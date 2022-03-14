using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CharacterMove : MonoBehaviour
{
    [SerializeField] private Text gemText;
    [SerializeField] private AudioSource finishAudio;
    private bool isCompleted = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("WinPlatform") && !isCompleted)
        {
            if(gemText.text == "3")
            {
                isCompleted = true;
                finishAudio.Play();
                Invoke("OnLevelCompleted", 0.5f);
            }
        }
    }

    private void OnLevelCompleted()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
