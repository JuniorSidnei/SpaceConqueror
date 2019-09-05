using UnityEngine;

[CreateAssetMenu(fileName = "New item", menuName = "Inventory/LootItem")]
public class LootItem : ScriptableObject
{
   public new string name = "New item";
   public Sprite Icon = null;
   public int Amount = 0;
}
