using System;
using System.Collections.Generic;
using UnityEngine;
// using Game; // <-- Uncomment and modify if ScoreManager is in a different namespace

public class ComboSystem : MonoBehaviour
{
    private List<string> bumperTags = new List<string>();   //lijst met geraakte tags
    private int scoreMultiplier = 1;
    public static event Action<int, int> OnScoreChange;    // Voeg een nieuw Action Event toe met de naam OnScoreChange
                                                          // Deze kan twee int waarden versturen: score en multiplier
    private void Start()
    {
        HitBumper.onHitBumper += CheckForCombo;
    }
    private void OnDisable()
    {
        HitBumper.onHitBumper -= CheckForCombo;
    }

    // Vervang de parameterlijst en implementatie
    private void CheckForCombo(Transform transform, int bumperValue)
    {
        bumperTags.Add(transform.gameObject.tag);                                //tag toevoegen aan lijst
        if (bumperTags.Count > 1)                           //check of er meer dan 1 tag is
        {                                                   //check of de laatste 2 tags gelijk zijn
            if (bumperTags[bumperTags.Count - 2] == bumperTags[bumperTags.Count - 1])
            {
                scoreMultiplier++;                          //verhoog de multiplier
            }
            else                                            //als ze niet gelijk zijn
            {
                scoreMultiplier = 1;                        //reset multiplier
                bumperTags.Clear();                         //leeg de lijst met tags
            }
        }                                                   //voeg score toe aan de ScoreManager
        Scoremanager.Instance.AddScore(bumperValue * scoreMultiplier);
        OnScoreChange?.Invoke(Scoremanager.Instance.score, scoreMultiplier); // Verstuur het Action Event met de score en multiplier
    }
}

