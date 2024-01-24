using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private AudioClip gunSound;
    private AudioSource gunSoundSource;

    private void Awake()
    {
        // Initialize the AudioSource component for the gun sound
        gunSoundSource = gameObject.AddComponent<AudioSource>();
        gunSoundSource.playOnAwake = false;
        gunSoundSource.clip = gunSound;
    }

    public void Shoot()
    {
        // Play the gun sound when the gun is used (e.g., when shooting)
        if (gunSound != null)
        {
            gunSoundSource.PlayOneShot(gunSound);
        }

        // Add the logic for shooting here (e.g., instantiate bullets, apply force, etc.)
    }
}
