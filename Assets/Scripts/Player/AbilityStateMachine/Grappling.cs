﻿using ASK.Core;
using UnityEngine;

namespace Player
{
    public partial class AbilityStateMachine
    {
        public class Grappling : AbilityState
        {

            public override void Enter(AbilityStateInput i)
            {
                // _grappleTimer = GameTimer.StartNewTimer(core.GrappleWarmTime);
                smActor.StartGrapple(Input.currentGrapplePos);
            }

            public override void FixedUpdate()
            {
                smActor.GrappleUpdate(Input.currentGrapplePos, 0);
                // GameTimer.FixedUpdate(_grappleTimer);
            }

            public override void CollideHorizontal() {
                smActor.CollideHorizontalGrapple();
                MySM.Transition<Idle>();
            }

            public override void CollideVertical() {
                smActor.CollideVerticalGrapple();
                MySM.Transition<Idle>();
            }

            public override void GrappleFinished()
            {
                base.GrappleFinished();
                smActor.GrappleBoost(Input.currentGrapplePos);
                MySM.Transition<Idle>();
            }
        }
    }
}