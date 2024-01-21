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

    public override void Apply(GameObject player){
        player.GetComponent<PlayerShoot>().applyBuff(_ttfRate);
        bullet.GetComponent<Bullet>().SetBulletDamage(_bulletDamage);
    }
}
