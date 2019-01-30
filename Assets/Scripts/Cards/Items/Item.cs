using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject {

    public string cardName;
    public string cardDescription;
    public string cardType;
    public Sprite art;
    public Color color;

    public int lifetime;

    public virtual void Init()
    {

    }
}
