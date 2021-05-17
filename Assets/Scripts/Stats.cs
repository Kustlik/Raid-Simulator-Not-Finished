using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public event EventHandler OnStatsChanged;

    public static float STAT_MIN = 0;
    public static float STAT_MAX = 1;

    public enum Type
    {
        Hps,
        Survi,
        Stability,
        Range,
        Dps,
    }

    private SingleStat hpsStat;
    private SingleStat surviStat;
    private SingleStat stabilityStat;
    private SingleStat rangeStat;
    private SingleStat dpsStat;

    public Stats(float hpsStatAmount, float surviStatAmount, float stabilityStatAmount, float rangeStatAmount, float dpsStatAmount)
    {
        hpsStat = new SingleStat(hpsStatAmount);
        surviStat = new SingleStat(surviStatAmount);
        stabilityStat = new SingleStat(stabilityStatAmount);
        rangeStat = new SingleStat(rangeStatAmount);
        dpsStat = new SingleStat(dpsStatAmount);
    }

    private SingleStat GetSingleStat(Type statType)
    {
        switch (statType)
        {
            default:
            case Type.Hps:        return hpsStat;
            case Type.Survi:      return surviStat;
            case Type.Stability:  return stabilityStat;
            case Type.Range:      return rangeStat;
            case Type.Dps:        return dpsStat;
        }
    }

    public void SetStatAmount(Type statType, float statAmount)
    {
        GetSingleStat(statType).SetStatAmount(statAmount);
        if (OnStatsChanged != null) OnStatsChanged(this, EventArgs.Empty);
    }

    public float GetStatAmount(Type statType)
    {
        return GetSingleStat(statType).GetStatAmount();
    }

    public float GetStatAmountNormalized(Type statType)
    {
        return GetSingleStat(statType).GetStatAmountNormalized();
    }

    private class SingleStat
    {
        private float stat;

        public SingleStat(float statAmount)
        {
            SetStatAmount(statAmount);
        }

        public void SetStatAmount(float statAmount)
        {
            stat = Mathf.Clamp(statAmount, STAT_MIN, STAT_MAX); 
        }

        public float GetStatAmount()
        {
            return stat;
        }

        public float GetStatAmountNormalized()
        {
            return (float)stat / STAT_MAX;
        }
    }
}
