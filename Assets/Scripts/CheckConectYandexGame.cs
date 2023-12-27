using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class CheckConectYandexGame : MonoBehaviour
{
    private void OnEnable() => YandexGame.GetDataEvent += CheckSDK;

    private void OnDisable() => YandexGame.GetDataEvent -= CheckSDK;

    // Start is called before the first frame update
    void Start()
    {
        CheckSDK();
    }

    public void CheckSDK()
    {
        if (YandexGame.SDKEnabled)
        {
            if (!YandexGame.auth)
                YandexGame.AuthDialog();
        }
    }
}
