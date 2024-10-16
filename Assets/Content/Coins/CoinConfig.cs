using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCoin", menuName = "Coin")]
public class CoinConfig : ScriptableObject
{
    public int value;
    public Sprite coinSprite;
}
