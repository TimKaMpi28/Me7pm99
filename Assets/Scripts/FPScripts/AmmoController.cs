using System.Collections;
using System.Collections.Generic;
using Manoeuvre;
using TMPro;
using UnityEngine;

public class AmmoController : MonoBehaviour
{
public static AmmoController instance;
[SerializeField] TextMeshProUGUI text;

private int curAmmo;
private int maxAmmo;

 private void Start()
{
    instance = this;
    maxAmmo = ManoeuvreFPSController.Instance.gunController.GetMaxAmmo();
    curAmmo = maxAmmo;
    text.text = curAmmo + " / " + maxAmmo;
}
public void setAmmo(int ammo){
    text.text = ammo + " / " + maxAmmo;
}
}
