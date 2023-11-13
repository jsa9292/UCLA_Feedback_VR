using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {

            savePath = Application.persistentDataPath + "/" + pid.text + ".txt";
            savingTo.text = savePath;
        }
    }
    private StreamWriter writer;
    public void OnStartPressed() {
        SceneManager.LoadScene(1);
        Debug.Log(savePath);
        writer = new StreamWriter(savePath, true);
        writer.WriteLine(Time.realtimeSinceStartup.ToString() + " Start");
    }
    public void WriteToSave(string s) {

        if (writer != null) writer.WriteLine(Time.realtimeSinceStartup.ToString() + " " + s);
        else Debug.Log("no writer");
    }
    private void OnApplicationQuit()
    {
        if (writer != null) writer.Close();
        else Debug.Log("no writer");
    }
}
