﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
  void OnTriggerEnter2D(Collider2D col)  {
    if (col.gameObject.CompareTag("Player"))
      Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
  }

}
