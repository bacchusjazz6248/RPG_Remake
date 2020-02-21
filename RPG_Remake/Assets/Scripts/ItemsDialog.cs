using UnityEngine;

public class ItemsDialog : MonoBehaviour
{
    [SerializeField] private int buttonNumber = 15;
    [SerializeField] private ItemButton itemButton;

    private ItemButton[] _itemButtons;

    private void Start()
    {
        gameObject.SetActive(false);

        for (var i = 0; i < buttonNumber - 1; i++)
        {
            Instantiate(itemButton, transform);
        }

        _itemButtons = GetComponentsInChildren<ItemButton>();
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if (gameObject.activeSelf)
        {
            for (var i = 0; i < buttonNumber; i++)
            {
                _itemButtons[i].OwnedItem = OwnedItemsData.Instance.OwnedItems.Length > i 
                    ? OwnedItemsData.Instance.OwnedItems[i]
                    : null;
            }
        }
    }
}