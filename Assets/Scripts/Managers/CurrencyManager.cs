using UnityEngine;
using Utilities;

public class CurrencyManager : LocalSingleton<CurrencyManager>
{
    [SerializeField] private string _currencyPrefName = "CURRENCY";
    [SerializeField] private CurrencyPanel _currencyPanel;

    private float _currency = 0;
        
    public void Init()
    {
        Load();
    }
    public float GetCurrency()
    {
        return _currency;
    }
    public void DealCurrency(float value)
    {
        _currency += value;
        UpdateCurrencyPanel();
        Save();
    }
    private void UpdateCurrencyPanel()
    {
        _currencyPanel.SetCurrencyAmount(_currency, false);
    }
    private void Load()
    {
        _currency = PlayerPrefs.GetFloat(_currencyPrefName);
        UpdateCurrencyPanel();
    }

    private void Save()
    {
        PlayerPrefs.SetFloat(_currencyPrefName, _currency);
    }
}