using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenCloseBail : MonoBehaviour
{
    public bool click = false;
    public bool click2 = false;
    public Sprite clickedbutton;
    public Sprite unclickedbutton;
    public Button button;
    public Button button2;
    public TMP_Text bail;
    public GameObject hook;
    public void OpenorCloseBail()
    {
        if (!click)
        {
            button.image.sprite = clickedbutton;
            bail.text = "Close Bail";
            click = true;
        }
        else
        {
            bail.text = "Open Bail";
            button.image.sprite = unclickedbutton;
            click = false;
        }
    }
    public void HookView()
    {
        Camera camera = Camera.main;
        if (!click2)
        {
            camera.transform.position = new Vector3(0, hook.transform.position.y, Camera.main.transform.position.z);
            button2.image.sprite = clickedbutton;
            click2 = true;
            camera.transform.parent = hook.transform;
        }
        else
        {
            camera.transform.position = new Vector3(0, 0, Camera.main.transform.position.z);
            button2.image.sprite = unclickedbutton;
            click2 = false;
            camera.transform.parent = null;
        }
    }
    public void GoToTank()
    {
        SceneManager.LoadScene(0);
    }
}
