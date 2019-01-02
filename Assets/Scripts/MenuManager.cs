using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using GoogleMobileAds.Api;

public class MenuManager : MonoBehaviour
{
    public GameObject deathPanel;
    public GameObject canvasObj;
    public GameObject walls;
    public GameObject startPanel;

    public AudioSource source;

    public AudioClip jump;
    public AudioClip death;

    private BannerView bannerView;


    void Start()
    {
        //initializing ads
        string appId = "<< your app id here >>"; 
        // not mentioned my original AppId I used for registering my app on Playstore
        MobileAds.Initialize(appId);
        this.RequestBanner();

        deathPanel = GameObject.Find("DeathScreen");
        deathPanel.SetActive(false);
        source = GetComponent<AudioSource>();
        startPanel = GameObject.Find("StartScreen");
        startPanel.SetActive(true);
        canvasObj = GameObject.Find("Canvas");
    }

    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";
        //adUnitId to use for testing = "ca-app-pub-3940256099942544/6300978111"
        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
            //.AddTestDevice(AdRequest.TestDeviceSimulator)
            // .AddTestDevice(SystemInfo.deviceUniqueIdentifier)
            // .Build();// REMOVE ADDTESTDEVICE() AFTER TESTING
        // Debug.Log(request.TestDevices);
        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    public void jumpSound()
    {
        source.PlayOneShot(jump, 1);
    }
    public void deathSound()
    {
        source.PlayOneShot(death, 1);
    }

    public void setWallsOff()
    {
        walls.SetActive(false);
    }

    public void setWallsOn()
    {
        walls.SetActive(true);
    }

    public void switchToDeath()
    {
        setWallsOff();
        deathPanel.SetActive(true);

    }

    public void startTheGame()
    {
        startPanel.SetActive(false);
        setWallsOn();
    }
    
    public void restart()
    {
        SceneManager.LoadScene(0);
    }
}
