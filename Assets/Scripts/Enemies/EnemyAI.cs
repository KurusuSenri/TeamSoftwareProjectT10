using UnityEngine;

public abstract class EnemyAI : MonoBehaviour
{
    public abstract void TakeDamage(int damage);

    public abstract void KnockBack(Vector3 sourcePosition, float force, float duration);
}