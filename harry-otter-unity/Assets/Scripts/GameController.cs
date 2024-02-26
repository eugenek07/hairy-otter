using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spells;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    bool spellProgress;

    void Start()
    {
        spellProgress = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (spellProgress) {
        //receive command through MQTT (arduino)
            Spells.cast("test");
        }
    }
}
