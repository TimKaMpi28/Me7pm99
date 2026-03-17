using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : EnemyScript
{
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }
    

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
    }

    protected override void Die()
    {
        base.Die();
        FPSceneController.instance.EndMissionLevel();
    }
}
