using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DragDropItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private RectTransform image;

    [SerializeField]
    private GameObject bucksGameObject;

    [SerializeField]
    private TextMeshProUGUI priceText;

    private Options options;

    public Options Options
    {
        get { return options; }
        set { options = value; }
    }

    private void Start()
    {
        if (options == null)
        {
            Debug.LogError("The options is null");
            return;
        }

        if (text != null)
            text.text = options.Text;

        if (priceText != null)
            priceText.text = options.Cost.ToString();

        if (options.Image != null)
            Instantiate(options.Image, image);
    }

    public void SetActiveBucks(bool value)
    {
        bucksGameObject.SetActive(value);
    }
}
