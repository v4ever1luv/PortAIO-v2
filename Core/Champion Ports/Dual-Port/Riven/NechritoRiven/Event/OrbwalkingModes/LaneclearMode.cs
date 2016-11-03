using EloBuddy; 
 using LeagueSharp.Common; 
 namespace NechritoRiven.Event.OrbwalkingModes
{
    #region

    using Core;

    using LeagueSharp;
    using LeagueSharp.Common;

    #endregion

    internal class LaneclearMode : Core
    {
        #region Public Methods and Operators

        public static void Laneclear()
        {
            var minions = MinionManager.GetMinions(Player.AttackRange + 380);

            if (minions == null || Player.Spellbook.IsAutoAttacking || (MenuConfig.LaneEnemy && ObjectManager.Player.CountEnemiesInRange(1350) > 0))
            {
                return;
            }

            if (minions.Count <= 2)
            {
                return;
            }

            foreach (var m in minions)
            {
                if (m.UnderTurret(true))
                {
                    return;
                }

                if (Spells.E.IsReady() && MenuConfig.LaneE)
                {
                    Spells.E.Cast(m);

                    LeagueSharp.Common.Utility.DelayAction.Add(10, Usables.CastHydra);
                }

                if (!MenuConfig.LaneQFast)
                {
                    return;
                }

                if (m.Health < Spells.Q.GetDamage(m) && Spells.Q.IsReady())
                {
                    BackgroundData.CastQ(m);
                }
                else if (!Spells.W.IsReady()
                         || !MenuConfig.LaneW
                         || Player.Spellbook.IsAutoAttacking
                         || m.Health > Spells.W.GetDamage(m))
                {
                    return;
                }

                BackgroundData.CastW(m);
            }
        }

        #endregion
    }
}
