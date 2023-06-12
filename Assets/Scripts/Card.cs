using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCard", menuName = "Card/New Card")]
public class Card : ScriptableObject
{
    public GameObject prefab;
    public float elexir;
    public Sprite sprite;
}
