using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private Cutscene introScene, fincancierScene, pseudoScene, modderScene, desireScene, conclusionScene;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    //Sequence based comic book style art
    public void TriggerCutscene()
    {

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
}
