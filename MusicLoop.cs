using UnityEngine;

public class MusicLoop : MonoBehaviour
{
    private static MusicLoop instance;
    private AudioSource audioSource;

    void Awake()
    {
        // 確保這個物件不會被銷毀
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // 創建 AudioSource 組件
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true; // 設置循環播放
    }

    // 播放音樂的公共方法
    public void PlayMusic(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
