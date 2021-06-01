using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelScript : MonoBehaviour
{
    public int levelLoad;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    
    public void FadeToLevel()
    {
        anim.SetTrigger("fade");
    }
    public void ChangeLevel()
    {
        SceneManager.LoadScene(levelLoad);
    }
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelLoad);
    }
}
