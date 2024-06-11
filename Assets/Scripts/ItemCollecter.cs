using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int potionCount = 0;

    [SerializeField] private int totalPotionsNeeded = 10;
    [SerializeField] private Text potionText;
    [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Potion"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            potionCount++;
            potionText.text = "Potion: " + potionCount;
        }
    }

    public bool HasEnoughPotions()
    {
        return potionCount >= totalPotionsNeeded;
    }
}
