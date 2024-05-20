using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.UI;
using TMPro;

public class OpenBook : MonoBehaviour
{
    public int preload;
    public string ebookPath;
    public string folderName;
    public Transform pagesParent;
    public FileInfo[] fInfo;
    public GameObject pagePrefab;
    public Object[] pages;
    public Transform bookMenu;
    public GameObject menuButtonPrefab;
    public string[] books;
    public GameObject touchTracker;
    public Vector3 touchEnter;
    public float speed;
    // Start is called before the first frame update
    private void Start()
    {
        //FIXME
        //this was added after implementing page loading, can be improved
        string folderPath = ebookPath;
        string[] folders = Directory.GetDirectories(folderPath);
        if (folders.Length > 0)
        {
            GameObject currentButton;
            foreach (string s in folders)
            {
                currentButton = GameObject.Instantiate(menuButtonPrefab, bookMenu);
                currentButton.GetComponentInChildren<TextMeshProUGUI>().text = s.Split('/').Last();
                currentButton.SetActive(true);

            }
        }

    }
    public void ChangeFolderName(TMP_Text tmp) {
        folderName = tmp.text;
        string msg = folderName;
        Debug.Log(msg);
        try
        {
            DataManager.instance.WriteToSave(msg);
        }
        catch {
            Debug.LogError("No DataManager Instance");
        }
        StartCoroutine(FindPages());
    }
    public void OnBackButtonPressed() {
        string msg = "eBook Back";
        Debug.Log(msg);
        DataManager.instance.WriteToSave(msg);
    }
    public IEnumerator FindPages()
    {
        string folderPath = ebookPath + folderName;
        DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
        fInfo = dirInfo.GetFiles("*.jpg");
        Debug.Log(fInfo[0].FullName);
        if (TrialManager.instance != null)
        {
            yield return new WaitForSeconds(TrialManager.instance.loadingTime);
        }
        else {
            Debug.LogError("No TrialManager instance");
        }
        foreach (FileInfo file in fInfo)
        {

            StartCoroutine(InitPage("PDF books/" + folderName + "/" + file.Name.TrimEnd(".jpg")));

        }


    }
    IEnumerator InitPage(string path)
    {
        ResourceRequest rr = Resources.LoadAsync(path);
        GameObject page = Instantiate(pagePrefab, pagesParent);
        yield return rr;
        Debug.Log(rr.isDone);
        RawImage img = page.GetComponent<RawImage>(); 
        img.texture = rr.asset as Texture;
        page.SetActive(true);
    }
    public void ConstructPage(string path) { 
    
    
    }
    public void onTouchEntered(Vector3 fingerPos) {
        touchTracker.transform.position = fingerPos;
        touchEnter = touchTracker.transform.localPosition;
    }
    public void onTouchMoved(Vector3 fingerPos)
    {
        touchTracker.transform.position = fingerPos;
        transform.localPosition += Vector3.up * (touchEnter - touchTracker.transform.localPosition).y * speed;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
