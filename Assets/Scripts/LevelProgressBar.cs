using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelProgressBar : MonoBehaviour
{
    public Slider slider;

    public void SetProgress(int progress)
    {
        slider.value = progress;
    }
}
