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
        buildingBlock,
    }

    public enum ActionType
    {
        none,
        chop,
        hoe,
        water,
        feed,
        dig,
        touch,
        plant
    }

    [Header("Only gameplay")]
    public TileBase tile;
    public ItemType type;
    public ActionType actionType;
    public Vector2Int range = new Vector2Int(5, 4);

    [Header("Only UI")]
    public bool stackable = true;

    [Header("Both")]
    public Sprite image;

    public void Test()
    {
        if (type == ItemType.tool)
        {

        }
    }

}