using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuNav : MonoBehaviour
{
    public Button[] games;
    public Button backButton;
    private int selected = 0;
    public Color selectedColor;
    public Color normColor;
    // Start is called before the first frame update
    void Start()
    {
        setGraphics(selected);
    }

    float thumb;
    float trig;
    float hand;
    bool moved;
    float cooldown;
    // Update is called once per frame
    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime; 
            return; 
        }
        if (!TrialManager.instance.calibrated) return;
        thumb = TrialManager.instance.controllerPad.x;
        trig = TrialManager.instance.controllerTrigger;
        hand = TrialManager.instance.controllerSqueeze;
        if (games[selected].gameObject.activeInHierarchy)
        {
            if (Mathf.Abs(thumb) > 0.3f)
            {
                if (!moved)
                {
                    selected += (int)Mathf.Sign(thumb);
                    selected = Mathf.Clamp(selected, 0, games.Length - 1);
                    moved = true;
                    setGraphics(selected);
                }

            }
            else
            {
                moved = false;
            }
            if (trig > 0.5)
            {
                games[selected].onClick.Invoke();
                cooldown = 0.5f;
            }
        }
        if (backButton.gameObject.activeInHierarchy) {
            if (hand > 0.5f)
            {
                backButton.onClick.Invoke();
                cooldown = 0.5f;
            }
        }
    }
        
        

    void setGraphics(int selected)
    {
        for (int i = 0; i < games.Length; i++)
        {
            games[i].targetGraphic.color = normColor;
        }
        games[selected].targetGraphic.color = selectedColor;

    }
}
