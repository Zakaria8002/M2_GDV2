using UnityEngine;

public class randomitems : MonoBehaviour
{
    [SerializeField]
    private string[] items = { "vla", "stokbrood", "kaas", "boter", "eieren", "appels", "bananen", "sinaasappels", "granaatappel", "worst" };

    private void Start()
    {
        Debug.Log("Druk op 1 willekeurig item | druk op 2 voor totaal aantal.");
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            printRandomItem();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            printTotalItems();
        }
    }

    private void printRandomItem()
    {
        Debug.Log("1 is ingedrukt");
        int randomIndex = Random.Range(0, items.Length);
        Debug.Log("Random item: " + items[randomIndex]);
    }

    private void printTotalItems()
    {
        Debug.Log("2 is ingedrukt");
        Debug.Log("Alle items:");
        foreach (var item in items)
        {
            Debug.Log(item);
        }
    }
}