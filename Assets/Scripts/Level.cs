using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] protected AudioClip _clickSFX;
    [SerializeField] [Range(0, 1)] protected float _clickSFXVolume = 0.6f;
    [SerializeField] protected float _delayLoadingInSeconds = 2f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    public void PlayClickSound()
    {
        AudioSource.PlayClipAtPoint(_clickSFX, Camera.main.transform.position, _clickSFXVolume);
    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(_delayLoadingInSeconds);
        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
