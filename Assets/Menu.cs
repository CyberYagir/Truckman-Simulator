using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Animator[] menus;
    public Animator set;
    public TMP_Text version;
    public Button bindKeys;
    public Animator load;
    private void Start()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        version.text = "Version:\n" + Application.version;
        RenderSettings.skybox.SetFloat("_Exposure", 1f);
    }

    public void Update(){ if (Input.GetKeyDown(KeyCode.F5)){Application.LoadLevel(0);}}
    public void SetAnimator(Animator an){
        set = an;
    }
    public void PlayAnim(string anim){
        set.Play(anim);
    }
    public void CloseOptions(){
        for(int i =0; i < menus.Length; i++){
            menus[i].Play("Hide");
        }
    }
    public void Load(int scene){
        bool all = true;
        for (int i = 0; i < InputManager.m.cfg.inputs.Count; i++)
        {
            if (InputManager.m.cfg.inputs[i].key == KeyCode.None)
            {
                bindKeys.onClick.Invoke();
                all = false;
                return;
            }
        }
        if (all)
        {
            StartCoroutine(wait(scene));
        }
    }
    IEnumerator wait(int scene){
        load.Play("Show");
        yield return new WaitForSeconds(1f);
        if (!Application.isLoadingLevel)
        Application.LoadLevel(scene);
    }
    
}
