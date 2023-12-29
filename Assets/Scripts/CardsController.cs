using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsController : MonoBehaviour
{
    [SerializeField]
    private Card[] cards;

    [SerializeField]
    private Image[] images;

    [SerializeField]
    private Team team;

    Queue<int> deck = new Queue<int>();
    int[] hand = new int[4] { -1, -1, -1, -1 };

    int selected = -1;

    [SerializeField]
    private EntitySpawner blueKingPosition;

    [SerializeField]
    private EntitySpawner redKingPosition;

    private void Awake()
    {
        GameController.Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {
        deck = new Queue<int>(new List<int>{ 0, 1, 2, 3, 4, 5 });
        for (int i = 0; i < 4; i++) SetHandItem(i);

        GameController.redDefaultTarget = blueKingPosition.SpawnEntity();
        GameController.blueDefaultTarget = redKingPosition.SpawnEntity();
    }

    void SetHandItem(int item)
    {
        hand[item] = deck.Dequeue();
        images[item].sprite = cards[hand[item]].sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (selected != -1 && Physics.Raycast(ray, out hit))
            {
                SpawnEntity(hand[selected], hit.point);
                int temp = hand[selected];
                hand[selected] = deck.Dequeue();
                images[selected].sprite = cards[hand[selected]].sprite;
                deck.Enqueue(temp);
                selected = -1;
            }
        }
    }

    public void OnHandItemClick(int item)
    {
        selected = item;
    }

    public GameObject SpawnEntity(int id, Vector3 location)
    {
        GameObject entity = Instantiate(cards[id].prefab, location, Quaternion.identity); // spawn in world
        ITargetable target = entity.GetComponent<ITargetable>();
        target.team = team;
        if (target.team == Team.Red)
            target.targetTeam = Team.Blue;
        else
            target.targetTeam = Team.Red;
        GameController.AddEntity(entity, team);
        return entity;
    }
}
