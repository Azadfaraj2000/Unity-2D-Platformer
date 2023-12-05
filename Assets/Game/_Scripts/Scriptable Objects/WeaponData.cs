using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Weapon")]
public class WeaponData : ScriptableObject
{
    [field: SerializeField] public int NumberOfAttacks { get; protected set; }

    public float[] MovementSpeed { get; protected set; }

    [field: SerializeReference] public List<ComponentData> ComponentData { get; private set; }

    public T GetData<T>()
    {
        return ComponentData.OfType<T>().FirstOrDefault();
    }

    [ContextMenu(itemName: "Add Sprite Data")]
    private void AddSpriteData() => ComponentData.Add(item: new WeaponSpriteData());

    [ContextMenu(itemName: "Add Movement Data")]
    private void AddMovementData() => ComponentData.Add(item: new MovementData());
}
