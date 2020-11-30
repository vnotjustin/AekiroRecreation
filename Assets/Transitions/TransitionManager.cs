using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager main;

    

    public Transform transitionQuad;
    public Material transitionMat;

    private Texture2D _screenShot;
    [Space]
    public float transitionTime;
    public AnimationCurve transitionCurve;
    [Space]
    public TextMeshPro textbox;

    bool inChange;

    private void Awake()
    {

        if (main != null && main != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            main = this;
            DontDestroyOnLoad(main);
        }
    }


    // Update is called once per frame
    void Update()
    {
        UpdateTransitionSize();
        UpdateMaterial();

    }


    public void UpdateTransitionSize()
    { 
        float quadHeight = Camera.main.orthographicSize * 2f;
        float quadWidth = quadHeight * Screen.width / Screen.height;
        transitionQuad.localScale = new Vector3(quadWidth, quadHeight, 1);
        Vector3 tPos = Camera.main.transform.position;
        tPos.z = -9f;
        transitionQuad.position = tPos;
        tPos.z = -9.1f;
        textbox.transform.localPosition = transitionQuad.localPosition;
    }

    public void UpdateMaterial()
    {
        float aspectRatio = Screen.width / (float)Screen.height;
        transitionMat.SetVector("_NoiseTiling", new Vector4(2.5f * aspectRatio, 2.5f , 0, 0));
    }


    public IEnumerator StartTransition()
    {
        transitionMat.SetFloat("_TransitionValue", 0);
        inChange = true;
        int resWidth = Screen.width;
        int resHeight = Screen.height;
        RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
        Camera.main.targetTexture = rt;
        _screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
        Camera.main.Render();
        RenderTexture.active = rt;
        _screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
        _screenShot.Apply();
        Camera.main.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        transitionMat.SetTexture("_MainTexture", _screenShot);
        transitionMat.SetFloat("_TransitionValue", 1);

        float t = 0;
        float fadeVal = 1;
        while (t < transitionTime)
        {
            t += Time.deltaTime;
            fadeVal = Remap(t, 0, transitionTime, 1, 0);
            fadeVal = transitionCurve.Evaluate(fadeVal);
            transitionMat.SetFloat("_TransitionValue", fadeVal);
            yield return new WaitForEndOfFrame();
        }
        transitionMat.SetFloat("_TransitionValue", 0);
        inChange = false;
    }

    public IEnumerator StartTransitionToScene(int targetScene)
    {
        transitionMat.SetFloat("_TransitionValue", 0);
        inChange = true;
        int resWidth = Screen.width;
        int resHeight = Screen.height;
        float _t = 0;
        float _fadeVal = 0;
        while (_t < transitionTime)
        {
            _t += Time.deltaTime;
            _fadeVal = Remap(_t, 0, transitionTime, 0, 1);
            _fadeVal = transitionCurve.Evaluate(_fadeVal);
            transitionMat.SetFloat("_TransitionValue", _fadeVal);
            yield return new WaitForEndOfFrame();
        }
        transitionMat.SetFloat("_TransitionValue", 1);

        float loadT = 0;
        float loadVal = Random.Range(2, 4);
        while (loadT < loadVal)
        {
            int numOfDots = Mathf.FloorToInt(Remap(loadT % 1, 0, 1, 0, 4));
            string dots = "";
            for (int i = 0; i < numOfDots; i++)
            {
                dots += ".";
            }
            textbox.text = "Loading" + dots;
            loadT += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        SceneManager.LoadScene(targetScene);
        while (targetScene != SceneManager.GetActiveScene().buildIndex)
        {
            yield return new WaitForEndOfFrame();
        }
        textbox.text = "";
        float t = 0;
        float fadeVal = 1;
        while (t < transitionTime)
        {
            t += Time.deltaTime;
            fadeVal = Remap(t, 0, transitionTime, 1, 0);
            fadeVal = transitionCurve.Evaluate(fadeVal);
            transitionMat.SetFloat("_TransitionValue", fadeVal);
            yield return new WaitForEndOfFrame();
        }
        transitionMat.SetFloat("_TransitionValue", 0);
        inChange = false;
    }


    public void Transition()
    {
            if (!inChange)
            {
                print("CHANGE");
                StartCoroutine(StartTransition());
            }
            else
            {
                StopAllCoroutines();

                StartCoroutine(StartTransition());
            }
    }

    public void TransitionToScene(int sceneNo)
    {
        if (!inChange)
        {
            print("CHANGE");
            StartCoroutine(StartTransitionToScene(sceneNo));
        }
        else
        {
            StopAllCoroutines();

            StartCoroutine(StartTransitionToScene(sceneNo));
        }
    }

    public void TransitionToNextScene()
    {
        int targetScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (targetScene > 2)
        {
            GameManager.Main.beatGame = true;
            targetScene = 0;
        }
        if (!inChange)
        {
            print("CHANGE");
            StartCoroutine(StartTransitionToScene(targetScene));
        }
        else
        {
            StopAllCoroutines();

            StartCoroutine(StartTransitionToScene(targetScene));
        }
    }

    public float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
