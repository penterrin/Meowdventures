using UnityEngine;
using System.Collections.Generic;

public class DestroyEnemy : MonoBehaviour
{
    GameObject enemy;

    public void AddEnemy(GameObject enemy)
    {
        this.enemy = enemy;
    }

    public GameObject GetEnemy()
    {
        return enemy;
    }
}
