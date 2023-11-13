using HTC.UnityPlugin.Vive;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TrialManager : MonoBehaviour
{
    public int holding {get; set; }//0 = none; 1 = controller; 2 = weight; 3 = book;

    public VideoPlayer exercise_vp;
    public GameObject exercise_menu;
    public VideoClip[] vcs;
    public GameObject[] games;
    public float loadingTime;
    public static TrialManager instance;
    public GameObject vidButtonPrefab;
    public Transform vidButtonParent;
    public Vector2 controllerPad;
    public float controllerTrigger;
    // Update is called once per frame
    private void Awake()
    {
        instance = this;
        loadingTime = 1.5f;
        vcs = Resources.LoadAll<VideoClip>("Exercise Videos");
        GameObject bt;
        int clip_i = 0;
        foreach(VideoClip clip in vcs)
        {
            bt = Instantiate(vidButtonPrefab, vidButtonParent);
            bt.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = clip.name;
            bt.name = clip_i.ToString();
            clip_i++;
            bt.SetActive(true);


        }
    }

    private void Update()
    {
        controllerTrigger = (ViveInput.GetAxisEx(HandRole.RightHand, ControllerAxis.Trigger));
        controllerPad = new Vector2(ViveInput.GetAxisEx(HandRole.RightHand, ControllerAxis.PadX), ViveInput.GetAxisEx(HandRole.RightHand, ControllerAxis.PadY));
        //Debug.Log(controllerTrigger.ToString() + controllerPad.ToString());
    }
    public void ExerciseButtonPressed(GameObject self)
    {
        int i = Convert.ToInt32(self.name);
        StartCoroutine(PlayVideo(i));
        string msg = "Exercise" + i;
        Debug.Log(msg);
        if (DataManager.instance != null)
        {
            DataManager.instance.WriteToSave(msg);


        }
        else {
            Debug.LogError("No DataManager Instance");
        }
    }
    public void GameButtonPressed(int i)
    {
        StartCoroutine(SwitchGame(i));
        string msg = "Game" + i;
        if (i == 0) msg = "Game Back";
        Debug.Log(msg);
        if (DataManager.instance != null)
        {
            DataManager.instance.WriteToSave(msg);


        }
        else
        {
            Debug.LogError("No DataManager Instance");
        }

    }
    public void OnExerciseBackPressed()
    {
        string msg = "Exercise Back";
        Debug.Log(msg);
        if (DataManager.instance != null)
        {
            DataManager.instance.WriteToSave(msg);


        }
        else
        {
            Debug.LogError("No DataManager Instance");
        }

    }
    public IEnumerator PlayVideo(int i) {
        if(i == -1)
        {
            exercise_menu.SetActive(true);
            exercise_vp.Stop();
            yield break;
        }

        yield return new WaitForSeconds(loadingTime);
        exercise_menu.SetActive(false);
        exercise_vp.clip = vcs[i];
        exercise_vp.Play();
    }
    public IEnumerator SwitchGame(int gi)
    {

        foreach (GameObject game in games)
        {
            game.SetActive(false);
        }
        yield return new WaitForSeconds(loadingTime);
        games[gi].SetActive(true);

    }

}
