using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemCollection : MonoBehaviour
{
    [SerializeField] private Text gemText;
    [SerializeField] private AudioSource collectAudio;

    public int gemCounter = 0;

    void Awake()
    {
        gemText.text = gemCounter.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Gem"))
        {
            collectAudio.Play();
            gemCounter++;
            gemText.text = gemCounter.ToString();
            Destroy(other.gameObject);
        }
    }
}
