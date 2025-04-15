using UnityEngine;

public class SettingButton : MonoBehaviour
{
    [SerializeField] private GameObject settingPanel;

    public void ToggleSettingPanel()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.successEffect);
        settingPanel.SetActive(!settingPanel.activeInHierarchy);
    }
}