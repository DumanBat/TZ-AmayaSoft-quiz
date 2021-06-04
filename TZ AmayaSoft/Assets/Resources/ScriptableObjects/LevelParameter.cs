using UnityEngine;

[CreateAssetMenu(fileName = "Level_", menuName = "Level")]
public class LevelParameter : ScriptableObject
{
    // Номер категории уровня. Нумерация категорий в Level Manager -> Level Parameters начинается с 0
    public int id;

    // Диапазон значений для элементов, одна группа элементов должна быть в одном диапазоне. [0-25] - буквы, [26-34] - числа и т.д. при расширении пула
    public int objectsMinId;
    public int objectsMaxId;

    // Количество уровней в каждой из сложностей
    public int levelsInDifficulty;

    // Название уровня
    public string levelTypeName;

}
