using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class LevelUnlocker : MonoBehaviour
{
    public Button[] buttons;

    // Update is called once per frame
    void Update()
    {
        var levelAt = PlayerPrefs.GetInt("levelAt", YandexGame.savesData.complitedLevels);
        for (var i = 0; i < buttons.Length; i++)
        {
            if (i + 1 > levelAt)
                buttons[i].interactable = false;
        }
    }
}
