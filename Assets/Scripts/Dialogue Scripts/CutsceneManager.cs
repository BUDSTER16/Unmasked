using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private Cutscene introScene, fincancierScene, pseudoScene, modderScene, desireScene, conclusionScene;

    [SerializeField] private GameObject comicScreen;
    private Image[] panels;

    private bool sceneActive = false;
    private Cutscene activeCut;

    private int currentPanel = 0;
    private GameObject activeComic;

    private static CutsceneManager instance;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Sequence based comic book style art
    public void TriggerCutscene(int index)
    {
        sceneActive = true;

        activeComic = Instantiate(comicScreen);
        panels = activeComic.GetComponentsInChildren<Image>(true);


        switch (index)
        {
            case 0:
                break;
            case 1:
                activeCut = introScene;
                break;
            case 2:
                activeCut = fincancierScene;
                break;
            case 3:
                activeCut = pseudoScene;
                break;
            case 4:
                activeCut = modderScene;
                break;
            case 5:
                activeCut = desireScene;
                break;
            case 6:
                activeCut = conclusionScene;
                break;
        }

        if(activeCut != null && activeCut.ContainsFrameAt(currentPanel))
        {
            panels[currentPanel].gameObject.SetActive(true);
            panels[currentPanel].sprite = activeCut.GetScene(currentPanel);
            currentPanel++;
        }
    }

    public void NextPanel()
    {
        if (activeCut != null && activeCut.ContainsFrameAt(currentPanel) && sceneActive)
        {
            panels[currentPanel].gameObject.SetActive(true);
            panels[currentPanel].sprite = activeCut.GetScene(currentPanel);
            currentPanel++;
        }
        else if(activeCut != null && sceneActive)
        {
            ResetFunction();
        }
    }

    private void ResetFunction()
    {
        Destroy(activeComic);
        currentPanel = 0;
        sceneActive = false;
        activeCut = null;
    }


}

[System.Serializable]
public class Cutscene
{
    [SerializeField] Sprite[] cutSprites;

    public Sprite GetScene(int index)
    {
        return cutSprites[index];
    }

    public bool ContainsFrameAt(int index)
    {
        return (index < cutSprites.Length);
    }
}
