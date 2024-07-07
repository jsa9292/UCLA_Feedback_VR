using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DataManager : MonoBehaviour
{
    public string savePath;
    public static DataManager instance;
    public bool dataWrite = false;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {

        savePath = Application.persistentDataPath + "/" + TrialManager.instance.ParticipantID + ".txt";
        writer = new StreamWriter(savePath, true);
        writer.WriteLine(Time.realtimeSinceStartup.ToString() + " Start");
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            WriteToSave(Camera.main.transform.position.ToString() + ", " + Camera.main.transform.localEulerAngles.ToString());
        }
        catch {
            Debug.Log("no camera");
        }
    }
    private StreamWriter writer;
    public void WriteToSave(string s) {

        if (writer != null && dataWrite) writer.WriteLine(Time.realtimeSinceStartup.ToString() + " " + s);
        else
        {
            Debug.LogError("no writer");
            this.enabled = false;
        }
    }
    private void OnApplicationQuit()
    {
        if (writer != null) writer.Close();
        else Debug.Log("no writer");
    }
}
