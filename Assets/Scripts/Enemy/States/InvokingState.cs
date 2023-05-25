using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class InvokingState : FSMState<EnemyController>
    {
        public InvokingState(EnemyController controller) : base(controller)
        {
            Transitions.Add(new FSMTransition<EnemyController>(
                isValid: () =>
                {
                    return mController.InvokingEnd;
                },
                getNextState: () =>
                {
                    return new IdleState(mController);
                }
            ));

            Transitions.Add(new FSMTransition<EnemyController>(
                isValid: () =>
                {
                    return mController.TpTime >= mController.TpingInterval && mController.IsBoss;
                },
                getNextState: () =>
                {
                    return new TpingState(mController);
                }
            ));

            Transitions.Add(new FSMTransition<EnemyController>(
                isValid: () =>
                {
                    return mController.IsBoss && mController.bossHealthBar.value <= 0;
                },
                getNextState: () =>
                {
                    return new DyingState(mController);
                }
            ));
        }

        public override void OnEnter()
        {
            Debug.Log("OnEnter InvokingState");
            mController.InvokerTime = 0f;
            mController.animator.SetTrigger("Invoke");
            mController.spawnObjects.SpawnEnemies();
        }

        public override void OnExit()
        {
            Debug.Log("OnExit InvokingState");
        }

        public override void OnUpdate(float deltaTime)
        {

            mController.TpTime += deltaTime;
        }
    }
}
