using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PhoneUIController : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip buttonPress;
    public UIDocument document;
    public VisualElement root;
    public List<AudioClip> buttonTones;
    public Label dialedNumber;
    public List<string> activeNumbers; //if we have time, we can have easter eggs here!
    public AudioClip callEffect;
    public AudioClip busyEffect;
    public AudioClip ringer;

    private void Awake()
    {
        document = GetComponent<UIDocument>();
    }

    void Start()
    {
        root = document.rootVisualElement;
        dialedNumber = root.Q<Label>("DialedNumber");

        Dictionary<string, string> phoneKeys = new Dictionary<string, string>()
        {
            {"Key0", "0" },
            {"Key1", "1" },
            {"Key2", "2" },
            {"Key3", "3" },
            {"Key4", "4" },
            {"Key5", "5" },
            {"Key6", "6" },
            {"Key7", "7" },
            {"Key8", "8" },
            {"Key9", "9" },
            {"KeyPound", "#" },
            {"KeyStar", "*" }
        };

        foreach(KeyValuePair<string, string> keys in phoneKeys)
        {
            VisualElement k = root.Q<VisualElement>(keys.Key);
            k.RegisterCallback<ClickEvent>((ev) => { 
                string value = keys.Value;
                string key = keys.Key;
                PlayButtonTone(key);

                //clear the keypad in case of mistake
                if (key == "KeyPound")
                {
                    dialedNumber.text = "";
                    return;
                }

                AddNumberToKeypad(value);
                CallNumber();
            });
        }

    }

    private void CallNumber()
    {
        if (dialedNumber.text == "911" || dialedNumber.text == "999") //someone will try and call 911, we should handle that! (999 is UK emergency number)
        {
            OutgoingCall();
            //do something here to frighten the player.
            return;
        }

        if (dialedNumber.text.Length == 7) 
        {
            if (activeNumbers.Contains(dialedNumber.text))
            {
                //handle valid call here
                OutgoingCall();
            } else
            {
                //send busy signal here / invalid number handling
                dialedNumber.text = "";
                IsBusyTone();
            }
        }
    }

    public void AddNumberToKeypad(string number)
    {
        if (dialedNumber.text.Length < 7)
        {
            dialedNumber.text = dialedNumber.text + number;
        }
    }

    public void PlayButtonTone(string tone)
    {
        AudioClip clip = buttonTones[0];

        switch (tone)
        {
            case "Key0":
                clip = buttonTones[0];
                break;
            case "Key1":
                clip = buttonTones[1];
                break;
            case "Key2":
                clip = buttonTones[2];
                break;
            case "Key3":
                clip = buttonTones[3];
                break;
            case "Key4":
                clip = buttonTones[4];
                break;
            case "Key5":
                clip = buttonTones[5];
                break;
            case "Key6":
                clip = buttonTones[6];
                break;
            case "Key7":
                clip = buttonTones[7];
                break;
            case "Key8":
                clip = buttonTones[8];
                break;
            case "Key9":
                clip = buttonTones[9];
                break;

        }

        AudioSource.PlayClipAtPoint(clip, transform.position, 0.5f);
    }

    public void IsBusyTone()
    {
        AudioSource.PlayClipAtPoint(busyEffect, transform.position, 0.5f);

    }

    public void IncomingCall()
    {
        AudioSource.PlayClipAtPoint(ringer, transform.position, 0.5f);
    }

    public void OutgoingCall()
    {
        AudioSource.PlayClipAtPoint(callEffect, transform.position, 1f);
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
