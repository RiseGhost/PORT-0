using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalBootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void CreateGlobalManager()
    {
        GameObject pause = new GameObject();
        pause.AddComponent<PauseMenu>();
        GameObject storage = new GameObject();
        storage.AddComponent<StorageManager>();
        new MoneyBank().Start();
        GameObject sound = new GameObject();
        sound.AddComponent<SoundServer>();
        Application.targetFrameRate = 60;
        Application.quitting += () => { MoneyBank.Exit(); };
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private static void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.Equals("Game"))
        {
            GameObject AttackServer = new GameObject();
            AttackServer.AddComponent<AttackServer>();
        }
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CreateGlobalManagerBefore()
    {
        GameObject notifications = new GameObject();
        notifications.AddComponent<NotificationServer>();
    }
}