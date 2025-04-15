using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable object/Item")]
public class Item : ScriptableObject
{
    public int id;
    public Sprite image;
    public ItemType type;
}

public enum ItemType
{
    Head,
    Body,
    Left,
    Right,
    Feet
}
