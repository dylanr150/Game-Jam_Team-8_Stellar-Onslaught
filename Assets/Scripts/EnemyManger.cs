using System.Collections.Generic;
using UnityEngine;

public class EnemyManger : MonoBehaviour
{
    [SerializeField] private int enemyCount = 5;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Vector3 startingPos = new Vector3(-8, 6, 0);
    [SerializeField] private float stride = 2.5f;
    private List<GameObject> enemies = new List<GameObject>();

    public int GetEnemyCount() => enemyCount;

    void Start()
    {
        var formationXPos = new float[]{ 0, 2, -2 };
        for (int i = 0; i < enemyCount; ++i) 
        {
            Vector3 pos = startingPos;
            pos.x += stride*i;
            GameObject enemy = GameObject.Instantiate(prefab, pos, Quaternion.identity);
            EnemyAI ai = enemy.GetComponent<EnemyAI>();

            var formationPos = new Vector2(formationXPos[i%formationXPos.Length], Mathf.Floor(i/formationXPos.Length));
            ai.formationPosition = formationPos;

            enemies.Add(enemy);
            
        }
    }

    void Update()
    {
        
    }
}
