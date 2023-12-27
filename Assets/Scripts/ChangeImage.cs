using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Sprite image1;
    public Sprite image2;
    private int counter;

    public void Start()
    {
        InvokeRepeating("Change", 1f, 0.5f);
    }

    void Change()
    {
        if (counter % 2 == 0)
            GetComponent<Image>().sprite = image1;
        else
            GetComponent<Image>().sprite = image2;
        counter += 1;
    }
}
