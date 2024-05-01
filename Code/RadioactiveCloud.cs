using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class RadioactiveCloud : MonoBehaviour
{
    float stuff;
    float stuff2;
    void Update()
    {
        stuff += Time.fixedDeltaTime;
        stuff2 += Time.fixedDeltaTime;
        if(stuff >= 1)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 15);
            foreach (Collider2D collider in colliders)
            {
                if(UnityEngine.Random.value >= 0.5f) collider.transform.SendMessage("Damage", 50f, SendMessageOptions.RequireReceiver);
                if (UnityEngine.Random.value >= 0.5f) collider.transform.SendMessage("OnEMPHit", SendMessageOptions.RequireReceiver);
            }
            stuff = 0;
        }
        if(stuff2 >= 0.2f)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 15);
            foreach (Collider2D collider in colliders)
            {
                LimbBehaviour limb = collider.GetComponent<LimbBehaviour>();
                if (limb)
                {
                    limb.SkinMaterialHandler.RottenProgress += 0.02f;
                    limb.SkinMaterialHandler.AcidProgress += 0.02f;
                    if (UnityEngine.Random.value >= 0.9f) limb.Slice();
                }
            }
            stuff2 = 0;
        }
        Destroy(gameObject, 60);
    }
}