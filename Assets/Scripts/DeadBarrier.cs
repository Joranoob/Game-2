using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player touched the death barrier.");

            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}
