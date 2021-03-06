﻿using System;
using FluidHTN;
using FluidHTN.Operators;
using UnityEngine;

namespace Rioters.Operators
{
    public class TakeDamageOperator : IOperator
    {
        public TaskStatus Update(IContext ctx)
        {
            if (ctx is NpcHtnContext c)
            {
                if (c.GenericTimer <= 0f)
                {
                    c.NavAgent.isStopped = true;
                    c.anim.SetBool("IsTakingDamage", true);
                    var clipInfo = c.anim.GetCurrentAnimatorClipInfo(0);
                    if (clipInfo.Length > 0)
                    {
                        var clip = clipInfo[0].clip;
                        c.GenericTimer = c.Time + clip.length;
                    }

                    return TaskStatus.Continue;
                }

                if (c.Time < c.GenericTimer)
                {
                    return TaskStatus.Continue;
                }

                c.GenericTimer = -1f;
                c.NavAgent.isStopped = false;
                c.anim.SetBool("IsTakingDamage", false); //done taking damage
                return TaskStatus.Success;
            }
            return TaskStatus.Failure;
        }

        public void Stop(IContext ctx)
        {
            if (ctx is NpcHtnContext c)
            {
                c.NavAgent.isStopped = false;
                c.anim.SetBool("IsTakingDamage", false);
                c.GenericTimer = -1f;
            }
        }
    }
}