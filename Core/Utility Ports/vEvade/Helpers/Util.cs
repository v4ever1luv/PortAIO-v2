using EloBuddy; 
using LeagueSharp.Common; 
namespace vEvade.Helpers
{
    #region

    using System;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Text.RegularExpressions;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;

    using Color = System.Drawing.Color;
    using Version = System.Version;

    #endregion

    public static class Util
    {
        #region Static Fields

        public static readonly Random Random = new Random(Utils.GameTimeTickCount);

        #endregion

        #region Public Properties

        public static bool CommonCheck
            =>
                ObjectManager.Player.IsDead || ObjectManager.Player.IsInvulnerable || !ObjectManager.Player.IsTargetable
                || ShieldCheck || ObjectManager.Player.IsCastingInterruptableSpell(true)
                || ObjectManager.Player.IsDashing() || ImmobileCheck;

        public static bool ShieldCheck
        {
            get
            {
                if (ObjectManager.Player.HasBuffOfType(BuffType.SpellShield)
                    || ObjectManager.Player.HasBuffOfType(BuffType.SpellImmunity))
                {
                    return true;
                }

                switch (ObjectManager.Player.ChampionName)
                {
                    case "Olaf":
                        if (ObjectManager.Player.HasBuff("OlafRagnarok"))
                        {
                            return true;
                        }
                        break;
                    case "Sion":
                        if (ObjectManager.Player.HasBuff("SionR"))
                        {
                            return true;
                        }
                        break;
                }

                if (ObjectManager.Player.LastCastedSpellName() == "SivirE"
                    && Utils.TickCount - ObjectManager.Player.LastCastedSpellT() < 300)
                {
                    return true;
                }

                if (ObjectManager.Player.LastCastedSpellName() == "BlackShield"
                    && Utils.TickCount - ObjectManager.Player.LastCastedSpellT() < 300)
                {
                    return true;
                }

                if (ObjectManager.Player.LastCastedSpellName() == "NocturneShit"
                    && Utils.TickCount - ObjectManager.Player.LastCastedSpellT() < 300)
                {
                    return true;
                }

                return false;
            }
        }

        #endregion

        #region Properties

        private static bool ImmobileCheck
        {
            get
            {
                var result = (from buff in ObjectManager.Player.Buffs
                              where
                                  buff.IsValid
                                  && (buff.Type == BuffType.Charm || buff.Type == BuffType.Knockup
                                      || buff.Type == BuffType.Stun || buff.Type == BuffType.Suppression
                                      || buff.Type == BuffType.Snare)
                              select buff.EndTime).Concat(new[] { 0f }).Max();
                var time = result.Equals(0f) ? -1 : (int)(Utils.GameTimeTickCount + (result - Game.Time) * 1000);
                return time != -1 && time - Utils.GameTimeTickCount > Game.Ping / 2 + 70;
            }
        }

        #endregion

        #region Public Methods and Operators

        public static bool CompareId(this GameObject obj1, GameObject obj2)
        {
            return obj1.NetworkId == obj2.NetworkId;
        }

        public static void CheckVersion()
        {
        }

        public static void DrawLine(Vector2 start, Vector2 end, Color color)
        {
            Drawing.DrawLine(Drawing.WorldToScreen(start.To3D()), Drawing.WorldToScreen(end.To3D()), 1, color);
        }

        public static void Move(this Vector2 pos)
        {
            EloBuddy.Player.IssueOrder(GameObjectOrder.MoveTo, pos.To3D(), false);
        }

        #endregion
    }
}