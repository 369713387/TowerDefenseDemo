using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleBar : MonoBehaviour {

    [SerializeField]
    private float hp_total;
    [SerializeField]
    private float hp_now;

    public Image hp_Bar;

    public float Hp_total
    {
        get
        {
            return hp_total;
        }

        set
        {
            hp_total = value;
        }
    }

    public float Hp_now
    {
        get
        {
            return hp_now;
        }

        set
        {
            hp_now = value;
        }
    }

    private void Update()
    {
        hp_Bar.fillAmount = Hp_now / Hp_total;
    }
}
