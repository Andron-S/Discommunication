using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button _button;
    private Image _image;
    private RectTransform _rectTransform;
    private Color _startColor;
    private Vector3 _startScale;

    [SerializeField] private int _nextSceneNumber = 1;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();

        _startColor = _image.color;
        _startScale = _rectTransform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Sequence sequence = DOTween.Sequence()
            .Append(transform.DOScale(0.8f, 0.5f))
            .Join(_image.DOColor(Color.green, 0.2f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Sequence sequence = DOTween.Sequence()
            .Append(transform.DOScale(_startScale, 0.5f))
            .Join(_image.DOColor(_startColor, 0.2f));
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(_nextSceneNumber);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}