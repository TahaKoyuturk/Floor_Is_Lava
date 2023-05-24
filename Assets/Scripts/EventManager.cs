using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void CoinCollect();
    public static CoinCollect CoinCollectEvent;

    public delegate void JumpPowerCollect();
    public static JumpPowerCollect JumpPowerCollectEvent;

    public delegate void PlayerDie();
    public static PlayerDie PlayerDieEvent;

}
