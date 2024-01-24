using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Buffs buff;
    public float despawnDelay = 5f;
    public AudioClip collectSound;  
    private AudioSource collectSoundSource; 

    private void Start()
    {
        // Initialize the AudioSource component for collect sound
        collectSoundSource = gameObject.AddComponent<AudioSource>();
        collectSoundSource.playOnAwake = false;
        collectSoundSource.clip = collectSound;

        StartCoroutine(DestroyAfterDelay());
    }

private void OnTriggerEnter2D(Collider2D collision)
{
    // Check if the collider has the PlayerMovement component
    var player = collision.GetComponent<PlayerMovement>();

    // If the collider has the PlayerMovement component
    if (player != null && player.CompareTag("Player"))
    {
        Debug.Log("Player with 'Player' tag detected");
        
        // Apply the buff to the player
        buff.Apply(player.gameObject);

        // Play the collect sound if it is assigned
        if (collectSound != null)
        {
            Debug.Log("Playing collect sound");
            collectSoundSource.PlayOneShot(collectSound);
        }

        // Destroy the Collectable GameObject
        Debug.Log("Destroying Collectable GameObject");
        Destroy(gameObject);
    }
    else
    {
        // For debugging purposes, log if a collision with another object occurs
        Debug.Log("Collider entered, but no player detected");
    }
}


    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(despawnDelay);
        Destroy(gameObject);
    }
}
