using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPair
{
    public GameObject object1;
    public GameObject object2;

    public GameObjectPair(GameObject object1 = null, GameObject object2 = null)
    {
        this.object1 = object1;
        this.object2 = object2;
    }
}
