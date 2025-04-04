using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponWheelButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,ISelectHandler
{
    public int ID;
    private Animator anim;
    public string itemName;
    public Image itemImage;
    private bool isSelected = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoverOver();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverExit();
    }

    private void HoverOver()
    {
        isSelected = true;
        anim.SetBool("isSelected", true);
        WeaponWheelController.Instance.itemNameText.text = itemName;
    }
    private void HoverExit()
    {
        isSelected = false;
        anim.SetBool("isSelected", false);
        WeaponWheelController.Instance.itemNameText.text = "";       
    }
    private void Select()
    {
        Debug.Log("Selected item with ID: " + ID);
        WeaponWheelController.Instance.itemNameText.text = itemName;
        WeaponWheelController.Instance.itemImage.sprite = itemImage.sprite;
    }

    public void OnSelect(BaseEventData eventData)
    {
        Select();
    }
   
}
