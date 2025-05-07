using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicInfoManager : MonoBehaviour
{
    public bool crimsonFury;
    public bool danceOfTheShadows;
    public bool eyeOfTheFalcon;
    public bool handOfTheGunner;
    public bool ilusoryTrack;
    public bool roarOfTheThunder;
    public bool swordOfTheFallen;
    public bool titaniumHeart;
    public bool treeOfEternity;
    public bool wingsOfTheWind;

    public static RelicInfoManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
