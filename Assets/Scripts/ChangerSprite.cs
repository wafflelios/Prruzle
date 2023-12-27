using System.Collections;
using System.Timers;
using UnityEngine;

public class ChangerSprite : MonoBehaviour
{ 
    public void Start()
    {
        InvokeRepeating("ChangeImage", 1f, 0.5f);
    }

    void ChangeImage()
    {
        transform.Rotate(0, 0, 180);
    }
}
