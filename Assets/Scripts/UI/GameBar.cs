using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class GameBar : MonoBehaviour
{
    private enum Mode { Horizontal, Vertical }
    [SerializeField] private float _maxValue = 100;
    [SerializeField] private Color _minColor = Color.red;
    [SerializeField] private Color _middleColor = Color.yellow;
    [SerializeField] private Color _maxColor = Color.green;
    [SerializeField] private Image _image; // дочерний Image
    [SerializeField] private Mode _direction;
    private Color _color_min, _color_middle, _color_max;
    private RectTransform _curRect;
    private float _delta;
    public float percent { get; private set; }
    public float current { get; private set; }
    public float max { get; private set; }

    public void ResetBarColors()
    {
        _color_max = _maxColor;
        _color_middle = _middleColor;
        _color_min = _minColor;
        SetTargetColor();
    }

    public void Initialize()
    {
        _color_max = _maxColor;
        _color_middle = _middleColor;
        _color_min = _minColor;
        max = _maxValue;
        current = _maxValue;
        _curRect = GetComponent<RectTransform>();
        if (_direction == Mode.Horizontal)
            _delta = _curRect.sizeDelta.x;
        else
            _delta = _curRect.sizeDelta.y;
        _image.raycastTarget = false;
        percent = 1;
        SetTargetColor();
        SetTargetRect();
    }

    void OnValidate()
    {
        RectTransform t = GetComponent<RectTransform>();
        float x = t.sizeDelta.x, y = t.sizeDelta.y;
        if (_direction == Mode.Horizontal && y > x || _direction == Mode.Vertical && x > y)
            t.sizeDelta = new Vector2(y, x);

        if (_image != null)
        {
            _image.rectTransform.anchorMin = Vector2.zero;
            _image.rectTransform.anchorMax = Vector2.one;
            _image.rectTransform.offsetMax = Vector2.zero;
            _image.rectTransform.offsetMin = Vector2.zero;
        }
    }

    public void BarSetup(float currentValue, float maxValue)
    {
        current = currentValue;
        max = maxValue;
        percent = Round(currentValue / maxValue);
        SetTargetColor();
        SetTargetRect();
    }

    public void BarSetup(Color colorMax, Color colorMiddle, Color colorMin)
    {
        _color_max = colorMax;
        _color_middle = colorMiddle;
        _color_min = colorMin;
        SetTargetColor();
    }

    public void BarSetup(float currentValue, float maxValue, Color colorMax, Color colorMiddle, Color colorMin)
    {
        _color_max = colorMax;
        _color_middle = colorMiddle;
        _color_min = colorMin;
        current = currentValue;
        max = maxValue;
        percent = Round(currentValue / maxValue);
        SetTargetColor();
        SetTargetRect();
    }

    void SetTargetColor()
    {
        Color color = _color_max;

        if (percent < .3f)
            color = _color_min;
        else if (percent < .7f)
            color = _color_middle;

        _image.color = color;
    }

    void SetTargetRect()
    {
        float offset = -(_delta - (_delta * percent));
        if (offset > 0)
            offset = 0;
        if (_direction == Mode.Horizontal)
            _image.rectTransform.offsetMax = new Vector2(offset, 0);
        else
            _image.rectTransform.offsetMax = new Vector2(0, offset);
        _image.rectTransform.offsetMin = Vector2.zero;
    }

    public void Adjust(float value)
    {
        current += value;
        if (current < 0)
            current = 0;
        if (current > max)
            current = max;
        percent = Round(current / max);
        SetTargetColor();
        SetTargetRect();
    }

    float Round(float f)
    {
        return ((int)(f * 100f)) / 100f;
    }
}
