using AlienBloxUtility.Utilities.EntityManipulation;
using AlienBloxUtility.Utilities.UIUtilities.UIRenderers;
using Microsoft.Xna.Framework;
using System;
//using System.Collections;
using System.Collections.Concurrent;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace AlienBloxUtility.Utilities.Core
{
    public class UtilityPlayer : ModPlayer
    {
        private ConcurrentQueue<Action> threadQueue;

        public bool noClipHack;

        public bool Immortal;

        public bool HitboxTool;

        public bool toolSudo;

        public bool tileTool;

        public bool ForceDelTE;

        public bool GrabMode;

        public int ForceSyncTimer;

        public int ForcePlaceTile = -1;

        public int ForcePlaceWall = -1;

        public int ForcePlaceTE = -1;

        public int mouseIcon;

        public int GrabNPC;

        public Vector2 noClipHackPos;

        public override void Initialize()
        {
            threadQueue = [];
        }

        public override void PreUpdate()
        {
            if (Main.myPlayer == Player.whoAmI)
            {
                while (threadQueue.TryDequeue(out Action a))
                {
                    a();
                }

                AlienBloxIconRender.SetIcon(mouseIcon);
            }

            if (!noClipHack)
            {
                noClipHackPos = Player.position;
            }
        }

        public override void PostUpdate()
        {
            if (noClipHack)
            {
                Player.position = noClipHackPos;
            }

            if (Immortal)
            {
                Player.statLife = Player.statLifeMax2;
                
                for (int i = 0; i < Main.debuff.Length; i++)
                {
                    Player.buffImmune[i] = true;
                }
            }
        }

        public override void PreUpdateBuffs()
        {
            if (Immortal)
            {
                foreach (var buffs in Player.buffType)
                {
                    if (Main.debuff[buffs])
                    {
                        Player.ClearBuff(buffs);
                    }
                }
            }
        }

        public override void PostUpdateBuffs()
        {
            if (Immortal)
            {
                foreach (var buffs in Player.buffType)
                {
                    if (Main.debuff[buffs])
                    {
                        Player.ClearBuff(buffs);
                    }
                }
            }
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            //if (tileTool)
            //{
            //    DebugTool.UseTool("TileTool", Player.whoAmI, toolSudo);
            //}
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (Main.mouseLeft && GrabMode)
            {
                GrabNPC = (int)AlienBloxUtility.GetNPCAtMouse()?.whoAmI;
            }

            if (!Main.mouseLeft)
            {
                EntityHelper.GrabNPC(GrabNPC, false, Main.MouseWorld);

                GrabNPC = -1;
            }

            if (AlienBloxKeybinds.GrabNPC.JustReleased)
            {
                GrabMode = !GrabMode;
            }

            if (triggersSet.MouseLeft && ForcePlaceTile != -1)
            {
                if (!AlienBloxUtility.SmartTilePlace(Main.MouseWorld, ForcePlaceTile))
                {
                    AlienBloxUtility.SmartTileModify(Main.MouseWorld, ForcePlaceTile);
                }

                Main.NewText($"Forced tile placement at {(Main.MouseWorld / 16).ToPoint()}");

                ForcePlaceTile = -1;
            }

            if (triggersSet.MouseLeft && ForcePlaceWall != -1)
            {
                AlienBloxUtility.SmartWallModify(Main.MouseWorld, ForcePlaceWall);

                Main.NewText($"Forced Wall placement at {(Main.MouseWorld / 16).ToPoint()}, Wall Type: {(ushort)ForcePlaceWall}");

                ForcePlaceWall = -1;
            }
            
            if (triggersSet.MouseLeft && ForcePlaceTE != -1)
            {
                try
                {
                    AlienBloxUtility.SmartEditTE(Main.MouseWorld, ForcePlaceTE);
                }
                catch
                {

                }

                Main.NewText($"Forced TE placement at {(Main.MouseWorld / 16).ToPoint()}, TE Type: {(ushort)ForcePlaceTE}");

                ForcePlaceTE = -1;
            }

            if (triggersSet.MouseLeft && ForceDelTE)
            {
                try
                {
                    AlienBloxUtility.SmartEditTE(Main.MouseWorld, -1);
                }
                catch
                {

                }

                Main.NewText($"Forced TE destruction at {(Main.MouseWorld / 16).ToPoint()}");

                ForceDelTE = false;
            }
            

            if (AlienBloxKeybinds.SudoKeybind.JustPressed)
            {
                toolSudo = !toolSudo;
            }

            //if (AlienBloxKeybinds.UseTileTool.JustPressed)
            //{
            //    tileTool = !tileTool;
            //}

            if (noClipHack)
            {
                float secondaryModifier = 1f;
                Vector2 prevNoClipHack = noClipHackPos;

                if (AlienBloxKeybinds.SudoKeybind.Current)
                {
                    secondaryModifier = 2f;
                }

                if (triggersSet.Up)
                {
                    noClipHackPos.Y -= 40 * secondaryModifier;
                }

                if (triggersSet.Down)
                {
                    noClipHackPos.Y += 40 * secondaryModifier;
                }

                if (triggersSet.Left)
                {
                    noClipHackPos.X += Player.direction * 40 * secondaryModifier;
                }

                if (triggersSet.Right)
                {
                    noClipHackPos.X += Player.direction * 40 * secondaryModifier;
                }

                if (prevNoClipHack != noClipHackPos)
                {
                    AlienBloxUtility.SendNoclipHack(noClipHackPos, noClipHack);
                }
            }

            if (GrabNPC != -1)
            {
                if (Main.npc[GrabNPC].position != Main.MouseWorld)
                    EntityHelper.GrabNPC(GrabNPC, true, Main.MouseWorld);
            }
        }

        public override void SyncPlayer(int toWho, int fromWho, bool newPlayer)
        {
            if (!newPlayer)
                AlienBloxUtility.SendNoclipHack(noClipHackPos, noClipHack);
        }

        public override void OnEnterWorld()
        {
            noClipHack = false;
            AlienBloxUtility.SendNoclipHack(Player.position, noClipHack);

            //DebugSidebarRender.Instance.RegenUI();
            AlienBloxUtility.SendSteamID(Player);
            AlienBloxUtility.RetrieveWallhackData();
            AlienBloxUtility.RetrieveSteamID();
            AlienBloxUtility.RetrieveProjectileFreeze();
        }

        public override bool CanBeHitByNPC(NPC npc, ref int cooldownSlot)
        {
            return !Immortal;
        }

        public override bool CanBeHitByProjectile(Projectile proj)
        {
            return !Immortal;
        }

        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genDust, ref PlayerDeathReason damageSource)
        {
            noClipHackPos = Player.position;
            noClipHack = false;

            return !Immortal;
        }
    }
}