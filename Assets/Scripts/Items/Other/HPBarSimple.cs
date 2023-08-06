using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HPBarSimple : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private GameObject _deathScreen;
    [SerializeField] private int _hp = 10;

    private void Start()
    {
        UpdateHP(0);
    }

    public void UpdateHP(int value)
    {
        _hp += value;

        _hpText.text = _hp.ToString();
        if(_hp <= 0)
        {
            _Die();
        }
    }

    private void _Die()
    {
        _deathScreen.SetActive(true);
        Debug.Log($"{this} Is Dead!!!");
    }
}
