using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class DeckController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] cards;

    [SerializeField]
    private GameObject storageItemPrefab;

    [SerializeField]
    private GameObject storage;

    [SerializeField]
    private CardManagementOverlay cardManagementOverlay;

    private int[] deck = new int[8];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < cards.Length; i++) 
        {
            GameObject storageItem = Instantiate(storageItemPrefab, storage.transform);
            storageItem.GetComponent<Button>().clicked += () => { StorageItemOnClick(i); };
            storageItem.GetComponent<Image>().image = new Texture2D(100, 100);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StorageItemOnClick(int item) 
    {
        cardManagementOverlay.DisplayButtonUse = !deck.Contains(item);
        DisplayButtonInfo();
    }

    public void DisplayButtonUse()
    {

    }

    public void DisplayButtonInfo()
    {

    }

    public void DeckItemOnClick(int item)
    {

    }

    public void ButtonUseOnClick(int item)
    {

    }

    public void ButtonInfoOnClick(int item)
    {

    }

    public void ButtonCancelOnClick()
    {

    }

    public void ButtonCloseOnClick()
    {

    }

    public void DisplayInfoWindow()
    {

    }
}
