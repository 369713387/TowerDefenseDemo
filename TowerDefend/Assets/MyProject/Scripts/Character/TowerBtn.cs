using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerBtn : MonoBehaviour {

    /// <summary>
    /// 对应按钮生成的塔的Prefab
    /// </summary>
    public GameObject TowerPrefab;

    /// <summary>
    /// 塔的图标
    /// </summary>
    public Sprite TowerIcon;

    /// <summary>
    /// 购买所花费的金钱数
    /// </summary>
    public float Coins;

    /// <summary>
    /// 按钮名字
    /// </summary>
    public string BtnName;

    /// <summary>
    /// 选择一个塔
    /// </summary>
    public abstract void PickTower();

    /// <summary>
    /// 取消选择的塔
    /// </summary>
    public abstract void RemovePick();

    /// <summary>
    /// 购买一座塔
    /// </summary>
    public abstract void BuyTower();
}
