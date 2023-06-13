using UnityEngine;

/// <summary>
/// ������� ����� ���� ������������� ������� �������� �� ����� 
/// </summary>
public abstract class Entity : MonoBehaviour
{
    /// <summary>
    /// �������� ������� ��� ������������
    /// </summary>
    [SerializeField] private string n_nickname;   
    public string Nickname => n_nickname;
}
