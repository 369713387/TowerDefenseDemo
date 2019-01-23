using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData{

    static private PlayerData m_instance;
    static public PlayerData Instance
    {
        get
        {
            return m_instance;
        }
    }

    /// <summary>
    /// 拥有的金币数量
    /// </summary>
    public float coins;

    /// <summary>
    /// 关卡等级
    /// </summary>
    public int wave;

    /// <summary>
    /// 血量
    /// </summary>
    public int hp;

    /// <summary>
    /// 新建一个用例
    /// </summary>
    public static void Create()
    {
        if (m_instance == null)
        {
            m_instance = new PlayerData();
        }
        
        if (PlayerPrefs.GetInt("NewGame", 0) == 0)
        {
            //无存档
            PlayerPrefs.SetInt("NewGame", 1);
            m_instance.NewSave();
            m_instance.Save();
        }
        else
        {
            //有存档
            m_instance.Read();
        }
    }

    /// <summary>
    /// 新建一份存档
    /// </summary>
    public void NewSave()
    {
        //读取金币
        m_instance.coins = PlayerPrefs.GetFloat("coins", 1000000);
        //读取关卡等级
        m_instance.wave = PlayerPrefs.GetInt("wave", 1);
        //读取血量
        m_instance.hp = PlayerPrefs.GetInt("hp", 10);
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    public void Read()
    {
        //读取金币
        m_instance.coins = PlayerPrefs.GetFloat("coins", 1000000);
        //读取关卡等级
        m_instance.wave = PlayerPrefs.GetInt("wave", 1);
        //读取血量
        m_instance.hp = PlayerPrefs.GetInt("hp", 10);
    }

    /// <summary>
    /// 存储数据
    /// </summary>
    public void Save()
    {
        //存储金币
        PlayerPrefs.SetFloat("coins", coins);
        //存储关卡等级
        PlayerPrefs.SetInt("wave", wave);
        //存储血量
        PlayerPrefs.SetInt("hp", hp);
    }

    /// <summary>
    /// 是否够钱买东西
    /// </summary>
    public bool ifCanBuy(float num)
    {
        if (num <= coins)
        {
            coins -= num;
            Save();
            //够钱
            return true;
        }
        return false;
    }
    /// <summary>
    /// 进入下一关
    /// </summary>
    public void NextWave(int num = 1)
    {
        wave += num;
        Save();
    }

    public void Clear()
    {
        PlayerPrefs.DeleteAll();
    }
}
