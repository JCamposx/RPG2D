using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class AttackingState : FSMState<EnemyController>
    {
        public AttackingState(EnemyController controller) : base(controller)
        {
            Transitions.Add(new FSMTransition<EnemyController>(
                isValid: () =>
                {
                    return mController.AttackingEnd;
                },
                getNextState: () =>
                {
                    return new IdleState(mController);
                }
            ));

            Transitions.Add(new FSMTransition<EnemyController>(
                isValid: () =>
                {
                    return mController.InvokerTime >= mController.InvokingInterval && mController.IsBoss;
                },
                getNextState: () =>
                {
                    return new InvokingState(mController);
                }
            ));
        }

        public override void OnEnter()
        {
            Debug.Log("OnEnter AttackingState");
            mController.TpTime = 0;
            if (!mController.IsTping)
            {
                mController.animator.SetTrigger("Attack");
                mController.hitBox.gameObject.SetActive(true);
            }
        }

        public override void OnExit()
        {
            Debug.Log("OnExit AttackingState");
            mController.hitBox.gameObject.SetActive(false);
        }

        public override void OnUpdate(float deltaTime)
        {
            mController.InvokerTime += deltaTime;
        }
    }
}
