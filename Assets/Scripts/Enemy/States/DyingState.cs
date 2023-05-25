using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class DyingState : FSMState<EnemyController>
    {
        public DyingState(EnemyController controller) : base(controller)
        {
        }

        public override void OnEnter()
        {
            Debug.Log("OnEnter DyingState");
            mController.animator.SetTrigger("Die");
        }

        public override void OnExit()
        {
            Debug.Log("OnExit DyingState");
        }

        public override void OnUpdate(float deltaTime) { }
    }
}
