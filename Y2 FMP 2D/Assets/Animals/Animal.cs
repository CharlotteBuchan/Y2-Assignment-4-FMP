using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Animal")]

public class Animal : ScriptableObject
{
    public GameObject prefab;
    public int purchasePrice;
    public Sprite icon;
    public string description;
}