using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;
    private bool isOn = true;

    public bool IsOn => isOn;

    private void Start()
    {
        image.sprite = isOn ? onSprite : offSprite;
    }

    public void Turn()
    {
        isOn = !isOn;
        image.sprite = isOn ? onSprite : offSprite;
    }

    public void SetState(bool newState)
    {
        isOn = newState;
        image.sprite = isOn ? onSprite : offSprite;
    }
}
