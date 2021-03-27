using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckSounds : MonoBehaviour
{
    public static TruckSounds sounds;
    public AudioSource startEngine;
    public AudioSource stopEngine;
    public AudioSource engineIdle, engineWork;
    public AudioSource outIn;
    public AudioSource paySound;
    private void Start()
    {
        sounds = this;
    }

    private void Update()
    {
        engineWork.gameObject.SetActive(engineIdle.gameObject.active);
        engineWork.volume = Input.GetAxis("Vertical1")/1.5f;
    }
    public static void OutIn()
    {
        sounds.outIn.PlayOneShot(sounds.outIn.clip);
    }
    public static void StartEngine()
    {
        sounds.startEngine.PlayOneShot(sounds.startEngine.clip);
        sounds.engineIdle.gameObject.SetActive(true);
    }
    public static void StopEngine()
    {
        sounds.stopEngine.PlayOneShot(sounds.stopEngine.clip);
        sounds.engineIdle.gameObject.SetActive(false);
    }
    public static void Pay()
    {
        sounds.paySound.transform.position = Player.p.controller.transform.position;
        sounds.paySound.PlayOneShot(sounds.paySound.clip);
    }
}
