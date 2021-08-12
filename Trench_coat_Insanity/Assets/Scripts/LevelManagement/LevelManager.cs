using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    #region Singleton
    private static LevelManager _instance;
    public static LevelManager Instance => _instance;
    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(gameObject);
        _instance = this;
    }
    #endregion

    #region Event Declaration
    
    public delegate void SwitchScene();
    public event SwitchScene InitSceneSwitch;
    
    #endregion
    
    //[SerializeField] private SceneDataObject sceneDataObject;
    
    public void LoadSceneFromIndex(in int sceneIndex, int timer = 0)
    {
        StartCoroutine(Transition(timer, sceneIndex));
    }

    private IEnumerator Transition(float timer, int sceneIndex)
    {
        yield return new WaitForSeconds(timer);
        InitSceneSwitch?.Invoke();
        yield return new WaitForSeconds(1.5f);
        LoadNewScene(sceneIndex);
    }

    private void LoadNewScene(in int sceneIndex)
    {
        //string sceneName = sceneDataObject.scenes[sceneIndex].name;
        SceneManager.LoadScene(sceneIndex);
    }
}
