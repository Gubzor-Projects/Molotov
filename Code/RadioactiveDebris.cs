using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
public class RadioactiveDebris : MonoBehaviour
{
    float stuff;
    public void OnTriggerEnter2D(Collider2D coll)
    {
        coll.transform.SendMessage("Slice", SendMessageOptions.DontRequireReceiver);
        coll.transform.SendMessage("Damage", SendMessageOptions.DontRequireReceiver);
        coll.transform.SendMessage("Break", SendMessageOptions.DontRequireReceiver);
        coll.transform.SendMessage("OnEMPHit", SendMessageOptions.DontRequireReceiver);
    }
    void Update()
    {
        stuff += Time.fixedDeltaTime;
        if(stuff >= 3)
        {
            transform.localScale -= Vector3.one * 0.01f * Time.timeScale;
            if (transform.localScale.x <= 0) Destroy(gameObject);
        }
    }
}