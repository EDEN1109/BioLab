using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    protected static SceneLoader instance;

    [SerializeField] private CanvasGroup sceneLoaderCanvasGroup;
    [SerializeField] private Image progressBar;
    [SerializeField] private VideoPlayer vp;
    [SerializeField] private RawImage screen;
    [SerializeField] private VideoClip[] videoClip = new VideoClip[2];
    private string loadSceneName;
    private Transform player;

    public static SceneLoader Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<SceneLoader>();

                if (obj != null)
                {
                    instance = obj;
                }
                else
                {
                    instance = Create();
                }
            }
            return instance;
        }

        private set
        {
            instance = value;
        }
    }

    void Awake()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y, player.position.z);
        transform.rotation = player.rotation;
    }

    public static SceneLoader Create()
    {
        var SceneLoaderPrefab = Resources.Load<SceneLoader>("SceneLoader");
        return Instantiate(SceneLoaderPrefab);
    }

    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        vp.clip = videoClip[Random.Range(0, 2)];
        vp.Prepare();
        SceneManager.sceneLoaded += LoadSceneEnd;
        loadSceneName = sceneName;
        StartCoroutine(Load(sceneName));
    }

    private IEnumerator Load(string sceneName)
    {
        progressBar.fillAmount = 0f;

        yield return StartCoroutine(Fade(true));

        while(!vp.isPrepared)
        {
            yield return new WaitForSeconds(0.5f);
        }

        screen.texture = vp.texture;
        vp.Play();

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;
        float timer = 0.0f;

        while (!op.isDone)
        {
            yield return null;

            timer += Time.unscaledDeltaTime;

            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);

                if (progressBar.fillAmount >= op.progress)
                {
                    timer = 0f;
                }
            }
            else
            {
                progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);

                if (progressBar.fillAmount == 1.0f)
                {
                    vp.Stop();
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }
       
    private void LoadSceneEnd(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.name == loadSceneName)
        {
            StartCoroutine(Fade(false));
            SceneManager.sceneLoaded -= LoadSceneEnd;
        }
    }

    private IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;

        while (timer <= 1f)
        {
            yield return null;

            timer += Time.unscaledDeltaTime * 2f;
            sceneLoaderCanvasGroup.alpha = Mathf.Lerp(isFadeIn ? 0 : 1, isFadeIn ? 1 : 0, timer);
        }
               
        if (!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }
}