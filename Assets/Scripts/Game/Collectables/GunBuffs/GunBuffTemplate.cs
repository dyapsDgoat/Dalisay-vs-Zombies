using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buffs/Gun buff")]
public class GunBuffController : Buffs
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private float _bulletDamage;

    [SerializeField]
    private float _ttfRate;

    [SerializeField]
    private float _buffDuration = 5;

    public override void Apply(GameObject player){
        MonoBehaviour monoBehaviour = player.GetComponent<MonoBehaviour>();
        if (monoBehaviour != null)
        {
            monoBehaviour.StartCoroutine(BuffTimer(player));
        }
    }

    private void ApplyEffects(GameObject player) {
        player.GetComponent<PlayerShoot>().applyBuff(_ttfRate);
        bullet.GetComponent<Bullet>().SetBulletDamage(_bulletDamage);
    }

    private void RevertEffects(GameObject player) {
        player.GetComponent<PlayerShoot>().RevertBuff();
        bullet.GetComponent<Bullet>().RevertDamage();
    }

    private IEnumerator BuffTimer(GameObject player)
    {
        ApplyEffects(player);
        Debug.Log("buff applied");
        // Wait for the specified duration
        yield return new WaitForSeconds(_buffDuration);
        // Revert the stats after the buff duration
        Debug.Log("buff reverted");
        RevertEffects(player);
        // Optionally, you can add any additional logic here after the buff expires
    }
}
