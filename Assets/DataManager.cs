using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DataManager : MonoBehaviour
{
    
    public TMPro.TMP_InputField pid;
    public Button button;
    public TMPro.TextMeshProUGUI savingTo;
    private string savePath;
    public static DataManager instance;
    public bool dataWrite = false;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
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
    public void OnStartPressed(string id) {
        dataWrite = true;
        savePath = Application.persistentDataPath + "/" + id + ".txt";
        Debug.Log(savePath);
        writer = new StreamWriter(savePath, true);
        writer.WriteLine(Time.realtimeSinceStartup.ToString() + " Start");
    }
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
