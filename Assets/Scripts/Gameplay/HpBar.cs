using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Image _hpBarFill;
    [SerializeField] private Player _player;

    private void Start()
    {
        if (_hpBarFill == null || _player == null)
        {
            Debug.LogError("HPBar: Please assign the HPBarFill image and Player script in the inspector.");
            return;
        }

        UpdateHPBar();
    }

    #region Private Methods
    private void OnEnable()
    {
        _player.OnHealthChanged += UpdateHPBar;
    }

    private void OnDisable()
    {
        _player.OnHealthChanged -= UpdateHPBar;
    }

    private void UpdateHPBar()
    {
        if (_player == null) return;

        float hpPercentage = (float)_player.Health / _player.MaxHealth;
        _hpBarFill.fillAmount = Mathf.Clamp01(hpPercentage);
    }
    #endregion
}
