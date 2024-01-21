using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Buffs/Health Buff")]
public class HealthBuffTemplate : Buffs
{
    [SerializeField]
    private float _additionalHealth;

    public override void Apply(GameObject player) {
        player.GetComponent<HealthController>().AddHealth(_additionalHealth);
    }
}
