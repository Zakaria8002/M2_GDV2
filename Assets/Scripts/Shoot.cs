using UnityEngine;
public class Shoot : MonoBehaviour
{
    //De waarden van deze variabelen kun je in de inspector editen dankzij [SerializeField]

    //in de inspector moet de prefab van de bal in dit veld (variabele) gesleept worden.
    [SerializeField] private GameObject prefab;
    //kracht die de bal krijgt per seconde dat we de knop inhouden
    [SerializeField] private float forceBuild = 20f;
    //maximale tijd om bij te houden hoe lang we de knop hebben ingedrukt
    [SerializeField] private float maximumHoldTime = 5f;

    //Deze variabelen zijn niet zichtbaar in de inspector

    //Bijhouden hoe lang we de knop hebben ingedrukt (seconden)
    private float _pressTimer = 0f;
    //Totale kracht waarmee de bal wordt afgevoord
    private float _launchForce = 0f;

    //Elk frame voeren we een functie HandleShot uit
    private void Update(){
        HandleShot();
    }
    //Die functie scrijven we zelf
    private void HandleShot() {
        //Check of de linkermuisknop word ingedrukt (alleen het eerste moment van indrukken)
        if (Input.GetMouseButtonDown(0))
        {
            _pressTimer = 0; //reset de timer weer op 0. Verderop gaan we de tijd hierin bijhouden hoe lang we de knop hebben ingehouden

        }
        //Check of je de linkermuisknop loslaat.
        if (Input.GetMouseButtonUp(0))
        {
            /*bepaal de kracht die je bal moet krijgen. hoe langer je de knop hebt vastgehouden hoe meer kracht. Met forcebuild kun je deze kracht tweaken in de inspector. Dit is de kracht per seconde.*/
            _launchForce = _pressTimer * forceBuild;

            /*Instantiate maakt van een prefab een gameonject in je scene.
            Er wordt dus een nieuwe bal in je scene aangemaakt.
            Om nog meer met deze bal te kunnen in ons script slaan we hem op in een variabele
            transform.parent verwijst naar de scene zodat de bal in de scene beland en niet in je kannon */
            GameObject ball = Instantiate(prefab, transform.parent);

            /*geef de bal dezelfde rotatie als het kanon zodat we heb de juiste richting op kunnen schieten.*/
            ball.transform.rotation = transform.rotation;

            /*Geef de Rigidbody van de bal een kracht (_launchForce) naar rechts mee op zijn eigen x-as. Doordat de bal goed geroteerd is gaat hij de goede kant op. ForceMode2D.Impulse zorgt dat alle kracht in 1 keer aan de bal gegeven wordt*/
            ball.GetComponent<Rigidbody2D>().AddForce(ball.transform.right * _launchForce, ForceMode2D.Impulse);

            /*Plaats de bal op dezelfde plek als het kanon zodat deze op die plek in de scene verschijnt*/
            ball.transform.position = transform.position;
        }
        /*Om te voorkomen dat we oneindige kracht mee kunnen geven beperken we de tijd die we maximaal bij gaan houden. Deze maximum tijd kunnen we in seconden instellen in de inspector (maximumHoldTime)*/
        if(_pressTimer < maximumHoldTime){
            /*Elk frame tellen we de duur van het frame op bij de verstreken tijd sinds we de knop in hebben gedrukt. Zodra we deze los laten weten we dus hoe lang dit duurde */
            _pressTimer += Time.deltaTime;
        }
    }
}