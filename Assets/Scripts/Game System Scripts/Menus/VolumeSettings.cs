using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class VolumeSettings : MonoBehaviour
{
    public static VolumeSettings instance;
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider backgroundSlider;
    [SerializeField] private Slider sfxSlider;


    [SerializeField] private Button masterMutebutton; 
    [SerializeField] private Button BGMutebutton; 
    [SerializeField] private Button SFXMutebutton;

    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;

    private bool isBGMuted = true;
    private bool isSFXMuted = true;
    private bool isMasterMuted = true;




    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);   
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        SetMasterMusic();
        SetBackgroundMusic();
        SetSFX();
        //optionsPanel = 
    }

    public void SetMasterMusic()
    {
        float masterVolumeSliderValue = masterSlider.value;

        audioMixer.SetFloat("MasterVolume", GetVolume(masterVolumeSliderValue));
    } 
    public void SetBackgroundMusic()
    {
        float BGVolumeSliderValue = backgroundSlider.value;

        audioMixer.SetFloat("BGVolume", GetBGVolume(BGVolumeSliderValue));
    }  
    public void SetSFX()
    {
        float sfxVolumeSliderValue = sfxSlider.value;

        audioMixer.SetFloat("SFXVolume", GetVolume(sfxVolumeSliderValue));
    }

    private float GetVolume(float sliderValue)
    {
        float mappedVolume = Mathf.Lerp(-56f, 0f, sliderValue / 10f);
        return mappedVolume;
    }

    private float GetBGVolume(float sliderValue)
    {
        float mappedVolume = Mathf.Lerp(-48f, 0f, sliderValue / 10f);
        return mappedVolume;
    }

    public void MasterMute()
    {
        if (audioMixer != null)
        {
            if (isMasterMuted)
            {
                audioMixer.SetFloat("MasterVolume", -80f);
                masterMutebutton.image.sprite = on;
                isMasterMuted = false;
            }
            else
            {
                 SetMasterMusic();
                masterMutebutton.image.sprite = off;
                isMasterMuted = true;
            }
        }
    }
    public void SFXMute()
    {
        
        if (audioMixer != null)
        {
            if (isSFXMuted)
            {   
                audioMixer.SetFloat("SFXVolume", -80f);
                SFXMutebutton.image.sprite = on;
                isSFXMuted = false; 


            }
            else
            {
               SetSFX();
                SFXMutebutton.image.sprite = off;
                isSFXMuted = true;
            }
        }
    }
    public void BGMute()
    {
        if (audioMixer != null)
        {
            if (isBGMuted)
            {
                audioMixer.SetFloat("BGVolume", -80f);
                BGMutebutton.image.sprite = on;
                isBGMuted = false;
            }
            else
            {
                SetBackgroundMusic();
                BGMutebutton.image.sprite = off;
                isBGMuted = true;
            }
        }
    }

}
