using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuUI : MonoBehaviour
{
    public static OptionsMenuUI Instance { get; private set; }
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicEffectsButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicEffectsText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI altInteractText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI interactControllerText;
    [SerializeField] private TextMeshProUGUI altInteractControllerText;
    [SerializeField] private TextMeshProUGUI pauseControllerText;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button altInteractButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button interactControllerButton;
    [SerializeField] private Button altInteractControllerButton;
    [SerializeField] private Button pauseControllerButton;
    [SerializeField] private GameObject rebindScreenOverlay;

    private Action OnCloseButtonAction;
    private void Awake()
    {
        Instance = this;

        soundEffectsButton.onClick.AddListener(() =>
        {
            SoundEffectsManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicEffectsButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        closeButton.onClick.AddListener(() =>
        {
            Hide();
            OnCloseButtonAction();
        });


        moveUpButton.onClick.AddListener(() => {RebindBinding(PlayerInput.Binding.Move_Up);});
        moveDownButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.Move_Right); });
        interactButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.Interact); });
        altInteractButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.AltIntract); });
        pauseButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.Pause); });
        interactControllerButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.Controller_Interact); });
        altInteractControllerButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.Controller_AltIntract); });
        pauseControllerButton.onClick.AddListener(() => { RebindBinding(PlayerInput.Binding.Controller_Pause); });
    }

    private void Start()
    {
        Hide();
        HideRebindScreenOverlay();
        UpdateVisual();
    }

    private void UpdateVisual()
    {

        soundEffectsText.text = "Sound Effects : " + Math.Round(SoundEffectsManager.Instance.GetVolume() * 10);
        musicEffectsText.text = "Music Sound : " + Math.Round(MusicManager.Instance.GetVolume() * 10);

        moveUpText.text = PlayerInput.Instance.GetKeyBinding(PlayerInput.Binding.Move_Up);
        moveDownText.text = PlayerInput.Instance.GetKeyBinding(PlayerInput.Binding.Move_Down);
        moveLeftText.text = PlayerInput.Instance.GetKeyBinding(PlayerInput.Binding.Move_Left);
        moveRightText.text = PlayerInput.Instance.GetKeyBinding(PlayerInput.Binding.Move_Right);
        interactText.text = PlayerInput.Instance.GetKeyBinding(PlayerInput.Binding.Interact);
        altInteractText.text = PlayerInput.Instance.GetKeyBinding(PlayerInput.Binding.AltIntract);
        pauseText.text = PlayerInput.Instance.GetKeyBinding(PlayerInput.Binding.Pause);
        interactControllerText.text = PlayerInput.Instance.GetKeyBinding(PlayerInput.Binding.Controller_Interact);
        altInteractControllerText.text = PlayerInput.Instance.GetKeyBinding(PlayerInput.Binding.Controller_AltIntract);
        pauseControllerText.text = PlayerInput.Instance.GetKeyBinding(PlayerInput.Binding.Controller_Pause);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(Action OnCloseButtonAction)
    {
        this.OnCloseButtonAction = OnCloseButtonAction;
        gameObject.SetActive(true);
    }

    public void HideRebindScreenOverlay()
    {
        rebindScreenOverlay.SetActive(false);
    }

    public void ShowRebindScreenOverlay()
    {
        rebindScreenOverlay.SetActive(true);
    }

    public void RebindBinding(PlayerInput.Binding binding)
    {
        ShowRebindScreenOverlay();
        PlayerInput.Instance.SetKeyBinding(binding, () =>
        {
            HideRebindScreenOverlay();
            UpdateVisual();
        });
    }

}
