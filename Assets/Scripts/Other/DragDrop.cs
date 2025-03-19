using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler, IDragHandler
{
    public Canvas canvas;
    public int id;
    public string essential;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 initialPosition;
    [SerializeField]
    private GridLayoutGroup gridLayout;

    public GameObject textObj;
    private bool possibleSlot = false;
    private bool interactable = true;

    public GridLayoutGroup GridLayout { get => gridLayout; set => gridLayout = value; }

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        initialPosition = transform.localPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (GetInteractable())
        {
            AudioManager.am.PlaySFX(AudioManager.am.drag);
            rectTransform.localScale = new Vector3(1.15f, 1.15f, 1);
            canvasGroup.alpha = .6f;
        }
    }    

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GetInteractable())
            canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GetInteractable())
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (GetInteractable())
        {
            FlavorManager.fm.Puff(Camera.main.WorldToScreenPoint(transform.position));

            transform.localPosition = initialPosition;
            rectTransform.localScale = new Vector3(1, 1, 1);

            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            ResetGrid();
        }
    }

    public void ResetGrid()
    {
        if (GridLayout != null)
        {
            GridLayout.enabled = false;
            GridLayout.enabled = true;
        }
    }

    public bool GetInteractable()
    {
        return interactable;
    }

    public void SetInteractable(bool value)
    {
        interactable = value;
    }

    public bool GetPossibleSlot()
    {
        return possibleSlot;
    }

    public void SetPossibleSlot(bool value)
    {
        possibleSlot = value;
    }
}