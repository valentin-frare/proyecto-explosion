using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigMenu : MonoBehaviour
{
    [SerializeField]
    private Sprite[] spritesConfigMenu;
    [SerializeField]
    private Sprite[] spritesMusicOnOff;
    [SerializeField]
    private Sprite[] spritesVibrateOnOff;
    [SerializeField]
    private GameObject music;
    [SerializeField]
    private GameObject vibrate;

    public Image actualImageConfig;
    private bool configActive;
    public Image actualImageMusic;
    private bool musicActive;
    public Image actualImageVibrate;
    private bool vibrateActive;
    private float timer = 0;

    void Start()
    {
        //LeanTween.moveX( gameObject, 1f, 1f).setEase( LeanTweenType.easeInQuad ).setDelay(1f);
        actualImageConfig.sprite = spritesConfigMenu[1];
        configActive = false;
        actualImageMusic.sprite = spritesMusicOnOff[1];
        musicActive = true;
        actualImageVibrate.sprite = spritesVibrateOnOff[1];
        vibrateActive = true;
        music.SetActive(false);
        vibrate.SetActive(false);
    }

    public void ChangeSprite(int id)
    {
        timer = 0;
        switch (id)
        {
            case 0:
                if (configActive)
                {
                    actualImageConfig.sprite = spritesConfigMenu[1];
                    configActive = false;
                }
                else
                {
                    actualImageConfig.sprite = spritesConfigMenu[0];
                    configActive = true;
                }
                ShowOrNot();
                break;
            case 1:
                if (musicActive)
                {
                    actualImageMusic.sprite = spritesMusicOnOff[0];
                    musicActive = false;
                    MusicManager.instance.MuteMixer(true);
                    SoundManager.instance.MuteMixer(true);
                }
                else
                {
                    actualImageMusic.sprite = spritesMusicOnOff[1];
                    musicActive = true;
                    MusicManager.instance.MuteMixer(false);
                    SoundManager.instance.MuteMixer(false);
                }
                break;
            case 2:
                if (vibrateActive)
                {
                    actualImageVibrate.sprite = spritesVibrateOnOff[0];
                    vibrateActive = false;
                    GameManager.instance.vibration = false;
                }
                else
                {
                    actualImageVibrate.sprite = spritesVibrateOnOff[1];
                    vibrateActive = true;
                    GameManager.instance.vibration = true;
                }
                break;
            default:
                break;
        }
    }

    public void ShowOrNot()
    {
        if (music.activeSelf)
        {
            music.SetActive(false);
            vibrate.SetActive(false);
        }
        else
        {
            music.SetActive(true);
            vibrate.SetActive(true);
        }
    }

    void Update()
    {
        if (configActive)
        {
            timer += Time.deltaTime;
            if (timer >= 5){
                ChangeSprite(0);
            }
        }
    }
}
