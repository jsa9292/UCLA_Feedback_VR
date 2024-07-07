using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class TrialManager : MonoBehaviour
{
    [Header("Trial Setup")]
    public string ParticipantID;
    public int activateRoom = 0;
    public Vector3 feedbackDirection;
    public bool StartTrial;
    [Space]
    [Space]
    [Header("Accesorries")]
    public float loadingTime;
    public RoomSetup roomSetup;
    public VideoPlayer exercise_vp;
    public GameObject exercise_menu;
    public GameObject vidButtonPrefab;
    public Transform vidButtonParent;
    public static TrialManager instance;
    public PlayRandFeedback[] Feedbacks; //check is positive
    public VideoClip[] vcs;
    public GameObject[] games;
    [Header("Controller")]
    public Vector2 controllerPad;
    public float controllerTrigger;
    public bool fakeInput;
    public Vector2 fakeController;
    public float fakeTrigger;
    // Update is called once per frame
    private void Awake()
    {
        instance = this;
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
        if (StartTrial) {
            roomSetup.Activate = true;
            for(int i = 0; i< 3; i++)
            {
                PlayRandFeedback prf = Feedbacks[i];
                if (feedbackDirection[i] != 0)
                {
                    prf.positive = feedbackDirection[i] == 1;
                    prf.negative = feedbackDirection[i] == -1;
                    if (feedbackDirection[i] == 2)
                    {

                        prf.positive = true;
                        prf.negative = true;
                    }
                }

            }
        }
        
        if (fakeInput)
        {
            controllerPad = fakeController;
            controllerTrigger = fakeTrigger;
        }
        else
        {
            controllerTrigger = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
            controllerPad = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            //Debug.Log(controllerTrigger.ToString() + controllerPad.ToString());
        }
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

        exercise_menu.SetActive(false);
        yield return new WaitForSeconds(loadingTime);
        exercise_vp.clip = vcs[i];
        exercise_vp.transform.parent.gameObject.SetActive(true);
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
