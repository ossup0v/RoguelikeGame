﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkAsSpawned : MonoBehaviour
{
  private bool isSpawned = false;
  public bool IsSpawned => isSpawned;
  private static int index;
  public int Index;
  void Start()
  {
    Index = ++index;
    isSpawned = true;
  }
}
