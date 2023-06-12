using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class DeckController : MonoBehaviour
{
    [SerializeField]
    private Card[] cards;

    [SerializeField]
    private Image[] deckItemsImages;

    [SerializeField]
    private GameObject storageItemPrefab;

    [SerializeField]
    private GameObject storage;

    [SerializeField]
    private CardManagementOverlay cardManagementOverlay;

    private int[] deck = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
    private bool full { get { return !deck.Contains(-1); } }
    private int selected = -1;
    private bool waitToChange = false;

    // Start is called before the first frame update
    void Start()
    {
        Vector2 anchorMin = new Vector2(0.04f, 0.96f);
        Vector2 anchorMax = new Vector2(0.96f, 0.04f);
        Vector2 itemSize = new Vector2(0.20f, 0.1f);
        Vector2 gapSize = new Vector2(0.04f, 0.04f);

        Vector2 currentPos = new Vector2(anchorMin.x, anchorMin.y - itemSize.y);

        for (int i = 0; i < cards.Length; i++) 
        {
            GameObject storageItem = Instantiate(storageItemPrefab, storage.transform);
            
            RectTransform itemTransform = storageItem.GetComponent<RectTransform>();
            itemTransform.anchorMin = currentPos;
            itemTransform.anchorMax = currentPos + itemSize;
            currentPos.x += itemSize.x + gapSize.x;
            if (currentPos.x > anchorMax.x)
            {
                currentPos.x = anchorMin.x;
                currentPos.y -= gapSize.y + itemSize.y;
            }
            itemTransform.offsetMin = new Vector2(0, 0);
            itemTransform.offsetMax = new Vector2(0, 0);

            string k = i.ToString();
            storageItem.GetComponent<Button>().onClick.AddListener(() => { StorageItemOnClick(itemTransform, System.Convert.ToInt32(k)); });
            storageItem.GetComponent<Image>().sprite = cards[i].sprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StorageItemOnClick(RectTransform itemTransform, int item) 
    {
        cardManagementOverlay.gameObject.SetActive(true);
        cardManagementOverlay.DisplayButtonUse = !deck.Contains(item);
        RectTransform overlayTransform = cardManagementOverlay.GetComponent<RectTransform>();
        overlayTransform.anchorMax = itemTransform.anchorMax;
        overlayTransform.anchorMin = itemTransform.anchorMin;
        overlayTransform.offsetMin = new Vector2(0, 0);
        overlayTransform.offsetMax = new Vector2(0, 0);
        selected = item;
        Debug.Log(selected);
    }

    public void DeckItemOnClick(int item)
    {
        if (waitToChange)
        {
            deck[item] = selected;
            waitToChange = false;
            UpdateDeckImage(item);
        }
    }

    public void UpdateDeckImage(int item)
    {
        Debug.Log($"sel={selected}item={item}");
        deckItemsImages[item].sprite = cards[deck[item]].sprite;
    }

    public void ButtonUseOnClick()
    {
        if (!full)
        {
            for (int i = 0; i < deck.Length; i++)
            {
                if (deck[i] == -1)
                {
                    deck[i] = selected;
                    UpdateDeckImage(i);
                    break;
                }
            }
        } else
        {
            waitToChange = true;
        }
    }

    public void ButtonInfoOnClick()
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
