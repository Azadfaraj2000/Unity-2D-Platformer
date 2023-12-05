using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityEvents
{
    public static UnityAction<GameObject, int> entityDamaged;
    public static UnityAction<GameObject, int> entityHealed;
}
