using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLevelByTimer : MonoBehaviour
{
    public float delay = 3;
    public string levelName;

    public void Start()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(levelName);
    }
}