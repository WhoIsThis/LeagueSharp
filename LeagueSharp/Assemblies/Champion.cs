﻿using System;
using LeagueSharp;
using LeagueSharp.Common;
using LX_Orbwalker;

namespace Assemblies {
    internal class Champion : ChampionUtils {
        protected readonly Obj_AI_Hero player = ObjectManager.Player;
        private readonly WardJumper wardJumper;
        protected Spell E;
        protected Spell Q;
        protected Spell R;
        protected Spell W;
        protected Menu menu;
        protected Orbwalking.Orbwalker orbwalker;

        public Champion() {
            addBasicMenu();
            wardJumper = new WardJumper();
            Game.PrintChat("SkinChanger loaded - Credits to Screeder.");

            Game.OnGameUpdate += onUpdate;
        }

        private void addBasicMenu() {
            menu = new Menu("Assemblies - " + player.ChampionName, "Assemblies - " + player.ChampionName,
                true);

            var targetSelectorMenu = new Menu("Target Selector", "Target Selector");
            SimpleTs.AddToMenu(targetSelectorMenu);
            menu.AddSubMenu(targetSelectorMenu);

            //Orbwalker submenu
            var orbwalkerMenu = new Menu("LX-Orbwalker", "orbwalker");
            LXOrbwalker.AddToMenu(orbwalkerMenu);
            menu.AddSubMenu(orbwalkerMenu);

            var skinChangerMenu = new Menu("Skin Changer", "skinChanger");
            skinChangerMenu.AddItem(
                new MenuItem("skinName", "Skin").SetValue(new StringList(SkinChanger.getSkinList(player.ChampionName))));
            menu.AddSubMenu(skinChangerMenu);

            if (wardJumper.isCompatibleChampion(player))
                wardJumper.AddToMenu(menu);

            menu.AddToMainMenu();
        }

        private void onUpdate(EventArgs args) {
            SkinChanger.update(menu);
        }
    }
}