using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MenuSettings")]
public class MenuSettings : ScriptableObject
{

    public float menuFadeTime = .5f;
    public Color sceneChangeFadeColor = Color.black;
    [Header("Leave this at zero to start game in same scene as menu, otherwise set to scene index")]
    public int nextSceneIndex = 0;

    [Header("Add your menu music here")]
    public AudioClip mainMenuMusicLoop;
    [Header("Level 0 Music")]
    public AudioClip musicLoopToChangeTo;
    [Header("Level 1 Music")]
    public AudioClip musicLevel1;
    [Header("Level 2 Music")]
    public AudioClip musicLevel2;
    [Header("Level 3 Music")]
    public AudioClip musicLevel3;
    [Header("Level 4 Music")]
    public AudioClip musicLevel4;
    [Header("Ascension Ending")]
    public AudioClip ascensionEnding;
    [Header("Descension Ending")]
    public AudioClip descensionEnding;

}
