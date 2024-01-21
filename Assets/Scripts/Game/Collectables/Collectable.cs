using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public Buffs buff;
    public float despawnDelay = 5f;
    private void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        var player = collision.GetComponent<PlayerMovement>();

        if (player != null)
        {
            buff.Apply(player.gameObject);
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(despawnDelay);
        Destroy(gameObject);
    }
}