/**
 * @file TowerModel.cs
 * @brief
 * @details
 * @author ddayin
 * @date 2016-03-14
 */

using UnityEngine;
using System.Collections;

public class TowerModel : MonoBehaviour {

    private int m_iID = 0;
    private float m_fAttackRange = 3.0f;
    private float m_fFireTerm = 1.0f;
    private int m_iBulletCount = 0;
    private int m_iEarnScore = 20;
    private int m_iBuyGold = 30;
    private int m_iSellGold = 10;
    private int m_iLevel = 1;
    private int m_iBulletDamage = 10;

    public int IID
    {
        get
        {
            return m_iID;
        }

        set
        {
            m_iID = value;
        }
    }

    public float FAttackRange
    {
        get
        {
            return m_fAttackRange;
        }

        set
        {
            m_fAttackRange = value;
        }
    }

    public float FFireTerm
    {
        get
        {
            return m_fFireTerm;
        }

        set
        {
            m_fFireTerm = value;
        }
    }

    public int IBulletCount
    {
        get
        {
            return m_iBulletCount;
        }

        set
        {
            m_iBulletCount = value;
        }
    }

    public int IEarnScore
    {
        get
        {
            return m_iEarnScore;
        }

        set
        {
            m_iEarnScore = value;
        }
    }

    public int IBuyGold
    {
        get
        {
            return m_iBuyGold;
        }

        set
        {
            m_iBuyGold = value;
        }
    }

    public int ISellGold
    {
        get
        {
            return m_iSellGold;
        }

        set
        {
            m_iSellGold = value;
        }
    }

    public int ILevel
    {
        get
        {
            return m_iLevel;
        }

        set
        {
            m_iLevel = value;
        }
    }

    public int IBulletDamage
    {
        get
        {
            return m_iBulletDamage;
        }

        set
        {
            m_iBulletDamage = value;
        }
    }
}
