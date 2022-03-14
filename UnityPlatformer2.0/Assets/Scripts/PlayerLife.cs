using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [SerializeField] private AudioSource deathAudio;
    [SerializeField] private GameObject soundController;

    private Animator animator;
    private Rigidbody2D rb;

    private bool isLife = true;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(rb.position.y < -20)
        {
            StartCoroutine(DieCoroutine());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(DieCoroutine());
        }
    }

    private void Die()
    {
        GetComponent<PlayerMovement>().enabled = false;
        animator.SetTrigger("Death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator DieCoroutine()
    {
        soundController.SetActive(false);
        if (isLife)
        {
            deathAudio.Play();
            isLife = false;
        }
        Die();

        yield return new WaitForSeconds(0.2f);

        deathAudio.Pause();

        yield return new WaitForSeconds(0.5f);

        RestartLevel();
    }
}
