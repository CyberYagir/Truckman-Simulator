using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;

public class Escape : MonoBehaviour
{
    public GameObject panel;
    public Transform holder, item;
    public TMP_InputField field;
    public GameObject saveLoad, isNot;
    private void Start()
    {
        GetSaves();
    }

    public void GetSaves() {
        string path = Path.GetFullPath(Path.Combine(Application.dataPath, @"..\"));
        var files = Directory.GetFiles(path + @"\Files\Saves\", "*.dat");
        foreach (Transform item in holder)
        {
            Destroy(item.gameObject);
        }
        for (int i = 0; i < files.Length; i++)
        {
            var p = Instantiate(item, holder);
            p.transform.GetChild(0).GetComponent<TMP_Text>().text = File.GetLastWriteTime(files[i]).ToString("dd.MM.yy HH:ss");
            p.transform.GetChild(1).GetComponent<TMP_Text>().text = Path.GetFileNameWithoutExtension(files[i]);
        }
    }
    public void SaveButton()
    {
        if (field.text.Trim() != "")
        {
            FindObjectOfType<SaveLoad>().Save(field.text);
            GetSaves();
        }
    }
    public void LoadButton()
    {
        var sel = FindObjectsOfType<LoadSelect>().ToList().Find(x => x.selected);
        if (sel != null)
        {
            FindObjectOfType<SaveLoad>().Load(sel.transform.GetChild(1).GetComponent<TMP_Text>().text.Trim());
        }
    }

    public void ToMenu()
    {
        Destroy(FindObjectOfType<GlobalScript>().gameObject);
        Application.LoadLevel(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panel.SetActive(!panel.active);
            Time.timeScale = panel.active ? 0 : 1;
            if (panel.active)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                isNot.SetActive(!FindObjectOfType<TruckManager>().isIn);
                saveLoad.SetActive(FindObjectOfType<TruckManager>().isIn);
            }
        }
    }
}
