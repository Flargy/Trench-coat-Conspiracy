using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Loads the next scene in the scene list
 */
public class SceneChange_Action : ActionType
{
    [SerializeField] private int delayTime = 1;
    private int nextSceneIndex = 0;
    private void Awake()
    {
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }
    public override void Activate()
    {
        LevelManager.Instance.LoadSceneFromIndex(nextSceneIndex, delayTime);
    }
}
