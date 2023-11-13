using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EditorButton : MonoBehaviour
{
    public Button button;
    public Image img;
    private void OnDrawGizmosSelected()
    {
        if (button == null) { 
            button= GetComponent<Button>();
            img = GetComponent<Image>();
            img.color = button.colors.normalColor;
        }
    }
    public void onButtonPressedChangeColor() {
        img.color = button.colors.pressedColor;
    }
    public void onButtonReleasedChangeColor()
    {
        img.color = button.colors.normalColor;
        button.onClick.Invoke();
    }

}
