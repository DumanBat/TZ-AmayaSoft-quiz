using UnityEngine;

[CreateAssetMenu(fileName = "ObjectSO_", menuName = "Object")]
public class CellObject : ScriptableObject
{
    public int id;
    public string objectName;
    public Sprite objectSprite;
}
