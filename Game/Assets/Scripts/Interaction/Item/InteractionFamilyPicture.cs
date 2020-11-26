
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InteractionFamilyPicture : InteractionCommon, IPointerClickHandler
{
    private PlayerInput input;
    private GameObject newFrame;
    public event Action<ScriptableItem> OnLeftClickEvent;
    public override string ToString() => "Examine Family's Picture";

    [SerializeField] private GameObject frame;
    [SerializeField] private Transform framePosition;
    [SerializeField] private GameObject frameCanvas;
    [SerializeField] private ScriptableItem frameItem;

    private void Start()
    {
        input = FindObjectOfType<PlayerInput>();
    }

    private void Update()
    {
        if (input.CurrentControl == TypeOfControl.InExamine)
        {
            if (input.RightClick)
            {
                Destroy(newFrame);
                frameCanvas.SetActive(false);
                input.ChangeTypeOfControl(TypeOfControl.InGameplay);
            }
        }
    }

    public override void Execute()
    {
        input.ChangeTypeOfControl(TypeOfControl.InExamine);
        frameCanvas.SetActive(true);
        OnLeftClickEvent(frameItem);
        newFrame = Instantiate(frame);
        newFrame.transform.position = framePosition.position;
        newFrame.transform.rotation = framePosition.rotation;

    }

    public void BackToGameplay()
    {
        Destroy(newFrame);
        frameCanvas.SetActive(false);
        input.ChangeTypeOfControl(TypeOfControl.InGameplay);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button ==
            PointerEventData.InputButton.Left)
        {
            if (frameItem != null && OnLeftClickEvent != null)
            {
                // Gets left click as a ScriptableItem
                OnLeftClickEvent(frameItem);
            }
        }
    }
}
