using UnityEngine;

[CreateAssetMenu(fileName = "NewObj", menuName = "Player/New Status")]
public class ObjectStats : ScriptableObject
{
    public int health;
    public float speed;

    public void UpdateHealth(int value)
    {
        health -= value;
    }

    public void Heal(int value)
    {
        health = value;
    }
}
