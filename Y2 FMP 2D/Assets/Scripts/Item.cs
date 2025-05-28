using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]

public class Item : ScriptableObject
{
    public enum ItemType
    {
        tool,
        material,
        playerFood,
        animalFood,
        seed,
        buildingBlock,
    }

    public enum ActionType
    {
        none,
        chop,
        hoe,
        dig,
        water,
        plant,
        touch,
        craft,
        feed,
        eat,
    }

    [Header("Only gameplay")]
    public TileBase tile;
    public ItemType type;
    public bool useUp = true;
    public int usesLeft;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);
    public int purchasePrice;
    public int sellPrice;
    public string description;

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;
}