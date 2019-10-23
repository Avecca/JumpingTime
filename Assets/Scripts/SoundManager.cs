using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public AudioSource backgroundSource;
    //AudioSource clickSource;
    public AudioSource audioSound;


   
    //List<AudioSource> sources;

   [SerializeField]
    private AudioClip buttonClick, powerClick;

    private bool soundEnabled = true;

    public static SoundManager Instance
    {
        get
        {

            if (_instance == null)
            {

                _instance = Instantiate(Resources.Load<GameObject>("SoundManager")).GetComponent<SoundManager>(); //new SoundManager();
                Debug.Log("SoundManager instantiated");
            }
            return _instance;
        }
    }


    private void Awake()
    {

        if (_instance != null && _instance != this) //!=this då finns det 2
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }


    private void Start()
    {
        //foreach (var source in GetComponents<AudioSource>())
        //{
        //    sources.Add(source);
        //}

        //AudioSource[] aS;
        //aS = GetComponents<AudioSource>();

        //Debug.Log("NR " +aS.Length);
        //backgroundSource = aS[0];
        ////clickSource = aS[1];
        //audioSound = aS[1];
        //audioSound.clip = buttonClick;
        //Debug.Log("hmm " + audioSound.clip.name);
        //PlaySound();


    }

    public void PlayBtnClick()
    {
       // Debug.Log("Setting audiosource");
       // Debug.Log("hmm " + buttonClick.name);
        // Debug.Log("hmm " + audioSound.clip.name);
        audioSound.clip = buttonClick;
        // audioSound.clip = buttonClick;
       // Debug.Log("Set audiosource " + audioSound.clip.name);
        if (audioSound.clip != null)
        {
            Debug.Log("PLaying bg sound");
            PlaySound();
        }

    }

    public void PlayPowerEffect()
    {
        //clickSource.clip = powerClick;
        PlaySound();
    }

    public void PlayBackground( AudioClip bgSound)
    {
        
        backgroundSource.clip = bgSound;
        if (soundEnabled)
        {
           
            backgroundSource.Play();
           
        }
    }
    public void PlayEffect(AudioClip effect)
    {

        audioSound.clip = effect;
        if (soundEnabled)
        {

            audioSound.Play();

        }
    }


    private void PlaySound()
    {
        if (soundEnabled)
        {

            Debug.Log("PLay SOUND!!!!!!");
            // clickSource.Play();
            audioSound.Play();
        }
        
    }

    public void TurnSoundToggle()
    {
        //soundEnabled = !soundEnabled;


        if (soundEnabled)
        {
            soundEnabled = false;
            backgroundSource.mute = true;
        }
        else
        {
            soundEnabled = true;
            backgroundSource.mute = false;
        }


    }

}
