using UnityEngine;

public class BattleScene : MonoBehaviour
{
    [SerializeField] string sceneName;

    public string GetSceneName()
    {
        return sceneName;
    }
}
