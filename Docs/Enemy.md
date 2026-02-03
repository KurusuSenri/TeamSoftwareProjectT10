# Enemy

Enemy should be tagged with "Enemy". Enemy projectile should be tagged with "EnemyProjectile".

See [`EnemyAI.cs`](../Assets/Scripts/Enemies/EnemyAI.cs)

`EnemyAI` is a abstract class, providing two main functionalities for all enemy types in the game:

1. `TakeDamage(int damage)`
2. `KnockBack(Vector3 sourcePosition, float force, float duration)`

Beside `EnemyAI`, each enemy also have a animator and a motor.