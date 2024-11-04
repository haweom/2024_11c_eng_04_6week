using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "NewKey", menuName = "Key")]
public class KeyConfig : ScriptableObject
{
    public int value;
    public Sprite ChestKeySprite;
}
