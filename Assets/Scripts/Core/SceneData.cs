using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SceneData : MonoBehaviour
{
    [Header("Store stuff")]
    public Transform shopListing;

    [Header("Level Decorations")]
    public GameObject plane;
    public GameObject nodes;
    public Canvas greenArrow;
    public Canvas redArrow;
    public GameObject streakPopup;
    public GameObject powerActivatedPopup;

    [Header("On Screen Buttons")]
    public NextWaveButton nextWaveButton;
    public PausePlayButton playPauseButton;
    public SpeedButton speedButton;

    [Header("Audio Sources")]
    public AudioSource backgroundMusic;
    public FXManager soundEffectsPlayer;

    [Header("Screen Panels")]
    public GameObject menuPanel;
    public GameObject settingsPanel;

    [Header("Game Over")]
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public TextMeshProUGUI winGems;
    public GameObject losePanel;
    public Button loseButton;
    public TextMeshProUGUI loseReplayText;

    [Header("Upgrade Panels")]
    public GameObject turretUpgradePanel;
    public Transform turretButtonList;
    public GameObject heroUpgradePanel;

    [Header("Player Stats")]
    public TextMeshProUGUI playerLives;
    public TextMeshProUGUI playerGold;
    public TextMeshProUGUI playerGems;
    public TextMeshProUGUI currentWave;
}
