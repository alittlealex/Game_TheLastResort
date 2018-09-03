using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuNew : MonoBehaviour {

	public Animator CameraObject;

    bool isPlayClick = false;
    bool isMusic;

	[Header("Scean")]
	public string sceneName; 

    //面板
	[Header("Panels")]
	public GameObject PanelControls;
	public GameObject PanelGame;
    public GameObject SecondClassPanel;
	public GameObject PanelConfirm;
    public GameObject PanelName;

    //声音
    [Header("Sound")]
    public GameObject bgmAudio;
    public GameObject hoverAudio;
    public GameObject clickAudio;

    //按钮修饰底纹
    [Header("Highlight Effects")]
	public GameObject lineGame;
	public GameObject lineControls;
    public GameObject lineMusicOn;
    public GameObject lineMusicOff;

    [Header("Buttons")]
    public GameObject PlayBtn;
    public GameObject OptionBtn;
    public GameObject ExitBtn;
    public GameObject ReadyButton;

    [Header("Cursor")]
    public Texture2D cursorTexture;

    //public AudioClip btnClick;
    //public AudioClip btnHover;

    private void Awake()
    {
        isMusic = PlayerPrefs.GetInt("isMusic") == 1? true : false;
        if (isMusic)
        {
            lineMusicOn.SetActive(true);
            lineMusicOff.SetActive(false);
        }
        else
        {
            lineMusicOn.SetActive(false);
            lineMusicOff.SetActive(true);
        }
    }

    private void Update()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        if(isMusic)
        {
            bgmAudio.GetComponent<AudioSource>().enabled = true;
        }
        else
        {
            bgmAudio.GetComponent<AudioSource>().enabled = false;
        }
    }

    //点击PLAY
    public void OnClickPlay ()
    {
        isPlayClick = !isPlayClick;
        PanelConfirm.SetActive(false);
        if (isPlayClick)
            SecondClassPanel.SetActive(true);
        else
            SecondClassPanel.SetActive(false);
    }

    //移上PLAY
    public void OnHoverPlay()
    {
        if(PlayBtn.GetComponent<Button>().IsInteractable() == true)
        {
            hoverAudio.GetComponent<AudioSource>().Play();
        }
    }

    //点击SEARCH GAME
	public void OnClickSearchGame()
    {
        DisableSecondClassPanel();
        isPlayClick = !isPlayClick;
        PlayBtn.GetComponent<Button>().interactable = false;
        OptionBtn.GetComponent<Button>().interactable = false;
        ExitBtn.GetComponent<Button>().interactable = false;
        PanelName.SetActive(true);
    }

    public void OnClickCancle()
    {
        PanelName.SetActive(false);
        PlayBtn.GetComponent<Button>().interactable = true;
        OptionBtn.GetComponent<Button>().interactable = true;
        ExitBtn.GetComponent<Button>().interactable = true;
    }
    
    //禁用SEARCH GAME 面板
	public void  DisableSecondClassPanel ()
    {
        SecondClassPanel.SetActive(false);
    }

    //点击option
	public void  OnClickOption ()
    {
        DisableSecondClassPanel();
		CameraObject.SetFloat("Animate",1);
	}

    //移上option
    public void OnHoverOption()
    {
        if (OptionBtn.GetComponent<Button>().IsInteractable() == true)
        {
            hoverAudio.GetComponent<AudioSource>().Play();
        }
    }

    //点击RETURN
    public void  OnClickReturn ()
    {
        CameraObject.SetFloat("Animate",0);
        Invoke("GamePanel", 0.4f);
    }

    //点击option GAME
	public void  GamePanel ()
    {
		PanelControls.SetActive(false);
		PanelGame.SetActive(true);

		lineGame.SetActive(true);
		lineControls.SetActive(false);
	}

    //点击option，game，on
    public void OnClickMusicOn()
    {
        isMusic = true;
        PlayerPrefs.SetInt("isMusic",1);
        lineMusicOn.SetActive(true);
        lineMusicOff.SetActive(false);
    }

    //移上on
    public void OnHoverMusicOn()
    {
        lineMusicOn.SetActive(true);
    }

    //移出on
    public void OnExitMusicOn()
    {
        if(isMusic)
            lineMusicOn.SetActive(true);
        else
            lineMusicOn.SetActive(false);
    }

    //点击option，game，off
    public void OnClickMusicOff()
    {
        isMusic = false;
        PlayerPrefs.SetInt("isMusic", 0);
        lineMusicOn.SetActive(false);
        lineMusicOff.SetActive(true);
    }

    //移上off
    public void OnHoverMusicOff()
    {
        lineMusicOff.SetActive(true);
    }

    //移出off
    public void OnExitMusicOff()
    {
        if (isMusic)
            lineMusicOff.SetActive(false);
        else
            lineMusicOff.SetActive(true);
    }

    //点击option control
    public void  ControlsPanel ()
    {
		PanelControls.SetActive(true);
		PanelGame.SetActive(false);

		lineGame.SetActive(false);
		lineControls.SetActive(true);
	}

    //播放hover
	public void  PlayHover ()
    {
        hoverAudio.GetComponent<AudioSource>().Play();
    }

    //播放click
	public void  PlayClick (){
        clickAudio.GetComponent<AudioSource>().Play();
    }

    //点击exit
	public void  OnclickExit ()
    {
        PanelConfirm.SetActive(true);
		DisableSecondClassPanel();
        PlayBtn.GetComponent<Button>().interactable = false;
        OptionBtn.GetComponent<Button>().interactable = false;
        ExitBtn.GetComponent<Button>().interactable = false;
    }

    //移上exit
    public void OnHoverExit()
    {
        if (ExitBtn.GetComponent<Button>().IsInteractable() == true)
        {
            hoverAudio.GetComponent<AudioSource>().Play();
        }
    }

    //点击exit，no
    public void  No ()
    {
		PanelConfirm.SetActive(false);
        PlayBtn.GetComponent<Button>().interactable = true;
        OptionBtn.GetComponent<Button>().interactable = true;
        ExitBtn.GetComponent<Button>().interactable = true;
    }

    //点击exit，yes
	public void  Yes ()
    {
		Application.Quit();
	}

    //以下为房间系统内
    public void OnClickRoomReturn()
    {
        CameraObject.SetBool("Room", false);
    }

    public void OnClickRoomOK()
    {
        if(Network.m_Actor.isConnected)
            CameraObject.SetBool("Room", true);
    }

    public void OnReadyHover()
    {
        if(ReadyButton.GetComponent<Button>().IsInteractable() == true)
        {
            PlayHover();
        }
    }

    public void OnReadyClick()
    {
        if (ReadyButton.GetComponent<Button>().IsInteractable() == true)
        {
            PlayClick();
        }
    }
}