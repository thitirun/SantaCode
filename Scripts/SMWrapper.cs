using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMWrapper : MonoBehaviour {

    public static SMWrapper Instance;
    
    // Use this for initialization
	void Awake () {
        if (Instance)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
	}

    public bool isFirstLevel(string levelid)
    {
        return SMGameEnvironment.Instance.SceneManager.IsFirstLevel(levelid);

    }

    public bool isLevelAvailable(string levelid)
    {
        SMSceneManager sm = SMGameEnvironment.Instance.SceneManager;
        SMLevelStatus status = sm.LevelProgress.GetLevelStatus(levelid);
        if (SMLevelStatus.New == status)
            return false;
        return true;

    }

    public void loadLevel(string levelid)
    {
        SMSceneManager sm = SMGameEnvironment.Instance.SceneManager;
        if (isLevelAvailable(levelid))
            sm.LoadLevel(levelid);
    }

    public void loadLevel()
    {
        SMSceneManager sm = SMGameEnvironment.Instance.SceneManager;
        if (isLevelAvailable(gameObject.name))
            sm.LoadLevel(gameObject.name);
    }

    public void loadScreen(string screen)
    {
        SMGameEnvironment.Instance.SceneManager.LoadScreen(screen);
    }

    public void loadNextLevel()
    {
        SMGameEnvironment.Instance.SceneManager.LoadNextLevel();
    }

    public void loadFirstLevel()
    {
        SMGameEnvironment.Instance.SceneManager.LoadFirstLevel();
    }

    public bool hasPlayed()
    {
        return SMGameEnvironment.Instance.SceneManager.LevelProgress.HasPlayed;
    }

    public void resetProgress()
    {
        SMSceneManager sceneManager = SMGameEnvironment.Instance.SceneManager;
        sceneManager.LevelProgress.ResetLastLevel();
        foreach (string levelId in sceneManager.Levels)
        {
            sceneManager.LevelProgress.SetLevelStatus(levelId, SMLevelStatus.New);
        }
        foreach (string groupId in sceneManager.Groups)
        {
            sceneManager.LevelProgress.SetGroupStatus(groupId, SMGroupStatus.New);
        }
        sceneManager.LevelProgress = sceneManager.UnmodifiableLevelProgress;
    }
    public void SetLevelAsFinished(string levelid)
    {
        SMGameEnvironment.Instance.SceneManager.SetLevelAsFinished(levelid);
    }

    public void setTransitionPrefab(string s)
    {
        SMGameEnvironment.Instance.SceneManager.TransitionPrefab = s;
    }
}
