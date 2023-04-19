using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class buttons : MonoBehaviour
{
    public bool click = false;
    public Sprite clickedbutton;
    public Sprite unclickedbutton;
    public Button button;
    public void ButtonSelected()
    {
        if (!click)
        {
            button.image.sprite = clickedbutton;
            click = true;
        }
        else
        {
            button.image.sprite = unclickedbutton;
            click = false;
        }
    }
}
