using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public static WeaponWheelController Instance { get; private set; }
    public TextMeshProUGUI itemNameText;
    public Image itemImage;
    private Animator wheelAnimator;
    [SerializeField] private ItemList itemList;
    [SerializeField] private GameObject[] wheelBtns;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        itemNameText.text = "";
        wheelAnimator = GetComponent<Animator>();
        SetInfo();
    }
    private void SetInfo()
    {
        for (int i = 0; i < wheelBtns.Length; i++)
        {
            wheelBtns[i].GetComponent<WeaponWheelButton>().ID = i;
            wheelBtns[i].GetComponent<WeaponWheelButton>().itemName = itemList.items[i].itemName;
            wheelBtns[i].GetComponent<WeaponWheelButton>().itemImage.sprite = itemList.items[i].image;
        }       
    }
    public void OpenWheel()
    {
        wheelAnimator.SetBool("openWheel", true);
    }

    public void CloseWheel()
    {
        wheelAnimator.SetBool("openWheel", false);
    }
}
