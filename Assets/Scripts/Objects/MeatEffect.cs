using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeatEffect : Item
{
    private void Start()
    {
        renderer.sprite = sprite;
    }

    public override void Pickup()
    {
        owner.GetComponent<CharacterStats>().speed++;

        base.Pickup();
    }
}
