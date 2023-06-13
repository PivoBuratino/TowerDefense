using UnityEngine;

/// <summary>
/// Базовый класс всех интерактивных игровых объектов на сцене 
/// </summary>
public abstract class Entity : MonoBehaviour
{
    /// <summary>
    /// Название объекта для пользователя
    /// </summary>
    [SerializeField] private string n_nickname;   
    public string Nickname => n_nickname;
}
