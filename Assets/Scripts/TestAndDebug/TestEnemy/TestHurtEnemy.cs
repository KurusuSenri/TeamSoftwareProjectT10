using UnityEngine;

public class TestHurtEnemy : MonoBehaviour
{
    public GameObject enemyObject;
    public Transform player;
    public int knockbackSpeed = 5;
    public float knockbackDuration = 1.5f;
    public int hurtAmount = 9;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Button T Pressed, trying to hurt enemy");
            HumanFormEnemyAI ai = enemyObject.GetComponent<HumanFormEnemyAI>();
            ai.TakeDamage(hurtAmount);
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Button Y Pressed, trying to knockback enemy");
            HumanFormEnemyAI ai = enemyObject.GetComponent<HumanFormEnemyAI>();
            Vector3 dir = enemyObject.transform.position - player.position;
            ai.KnockBack(dir, knockbackSpeed, knockbackDuration);
        }
    }
}
