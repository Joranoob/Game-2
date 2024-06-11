using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;
    private ItemCollector itemCollector;

    private void Start()
    {
        finishSound = GetComponent<AudioSource>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            itemCollector = player.GetComponent<ItemCollector>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (itemCollector != null && itemCollector.HasEnoughPotions())
            {
                finishSound.Play();
                Invoke("CompleteLevel", 1f);
            }
            else
            {
                Debug.Log("Not enough potions collected!");
            }
        }
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
