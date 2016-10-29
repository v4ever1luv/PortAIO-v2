using EloBuddy; 
 using LeagueSharp.Common; 
 namespace ReformedAIO.Champions.Ashe
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Drawing;

    using LeagueSharp;
    using LeagueSharp.Common;

    using Core;
    using Drawings;
    using OrbwalkingMode.Combo;
    using OrbwalkingMode.JungleClear;
    using OrbwalkingMode.LaneClear;
    using OrbwalkingMode.Mixed;

    using RethoughtLib.Bootstraps.Abstract_Classes;
    using RethoughtLib.FeatureSystem.Abstract_Classes;
    using RethoughtLib.FeatureSystem.Implementations;
    using RethoughtLib.Orbwalker.Implementations;
    using RethoughtLib.Utility;

    using Color = SharpDX.Color;

    #endregion

    internal class AsheLoader : LoadableBase
    {
        #region Public Properties

        public override string DisplayName { get; set; } = "Reformed Ashe";

        public override string InternalName { get; set; } = "Ashe";

        public override IEnumerable<string> Tags { get; set; } = new[] { "Ashe" };

        #endregion

        #region Public Methods and Operators

        public override void Load()
        {
            var superParent = new SuperParent(DisplayName);
            superParent.Initialize();

            var orbwalkerModule = new OrbwalkerModule();
            orbwalkerModule.Load();

            var comboParent = new OrbwalkingParent("Combo", orbwalkerModule.OrbwalkerInstance, Orbwalking.OrbwalkingMode.Combo);
            var laneParent = new OrbwalkingParent("Lane", orbwalkerModule.OrbwalkerInstance, Orbwalking.OrbwalkingMode.LaneClear);
            var jungleParent = new OrbwalkingParent("Jungle", orbwalkerModule.OrbwalkerInstance, Orbwalking.OrbwalkingMode.LaneClear);
            var mixedParent = new OrbwalkingParent("Mixed", orbwalkerModule.OrbwalkerInstance, Orbwalking.OrbwalkingMode.Mixed);
            var drawingParent = new Parent("Drawings");

            var setSpell = new SetSpells();
            setSpell.Load();

            comboParent.Add(new ChildBase[]
            {
                new QCombo(),
                new WCombo(),
                new ECombo(),
                new RCombo()
            });

            mixedParent.Add(new ChildBase[]
            {
                new QMixed(),
                new WMixed() 
            });

            jungleParent.Add(new ChildBase[]
            {
                new QJungle(),
                new WJungle()
            });

           laneParent.Add(new ChildBase[]
           {
               new QLane(),
               new WLane()  
           });

            drawingParent.Add(new ChildBase[]
            {
               new WDraw(),
               new DmgDraw() 
            });
           
            superParent.Add(new Base[] {
                orbwalkerModule,
                comboParent,
                mixedParent,
                laneParent,
                jungleParent,
                drawingParent
            });

            superParent.Load();

            superParent.Menu.Style = FontStyle.Bold;
            superParent.Menu.Color = Color.Cyan;
        }
        #endregion
    }
}