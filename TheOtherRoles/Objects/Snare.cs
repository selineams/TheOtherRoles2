using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using TheOtherRoles.Patches;
using TheOtherRoles.Players;
using TheOtherRoles.Utilities;
using UnityEngine;

namespace TheOtherRoles.Objects
{
    public class Snare
    {
        public GameObject snare;
        public static Sprite snareSprite;
        public static Sprite snareActiveSprite;
        /*public static AudioClip place;
        public static AudioClip activate;
        public static AudioClip disable;
        public static AudioClip countdown;
        public static AudioClip kill;
        public static AudioRolloffMode rollOffMode = AudioRolloffMode.Linear;*/
        private static byte maxId = 0;
        //public AudioSource audioSource;
        public static SortedDictionary<byte, Snare> snares = new();
        public bool isActive = false;
        public PlayerControl target;
        public DateTime placedTime;

        public static void loadSprite()
        {
            if (snareSprite == null)
                snareSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.Snare.png", 300f);
            if (snareActiveSprite == null)
                snareActiveSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.SnareActive.png", 300f);

        }

        private static byte getAvailableId()
        {
            byte ret = maxId;
            maxId++;
            return ret;
        }

        public Snare(Vector3 pos)
        {
            // 最初の罠を消す
            if (snares.Count == Snarer.numSnare)
            {

                foreach (var key in snares.Keys)
                {
                    var firstSnare = snares[key];
                    if (firstSnare.snare != null)
                        UnityEngine.Object.DestroyObject(firstSnare.snare);
                    snares.Remove(key);
                    break;
                }
            }

            // 罠を設置
            this.snare = new GameObject("Snare");
            var snareRenderer = snare.AddComponent<SpriteRenderer>();
            snare.AddSubmergedComponent(SubmergedCompatibility.Classes.ElevatorMover);
            snareRenderer.sprite = snareSprite;
            Vector3 position = new(pos.x, pos.y, pos.y / 1000 + 0.001f);
            this.snare.transform.position = position;
            // this.snare.transform.localPosition = pos;
            this.snare.SetActive(true);
            SoundEffectsManager.play("trapperTrap");

            // 音を鳴らす
           /* this.audioSource = trap.gameObject.AddComponent<AudioSource>();
            this.audioSource.priority = 0;
            this.audioSource.spatialBlend = 1;
            this.audioSource.clip = place;
            this.audioSource.loop = false;
            this.audioSource.playOnAwake = false;
            this.audioSource.maxDistance = 2 * EvilTrapper.maxDistance / 3;
            this.audioSource.minDistance = EvilTrapper.minDistance;
            this.audioSource.rolloffMode = rollOffMode;
            this.audioSource.PlayOneShot(place);*/

            // 設置時刻を設定
            this.placedTime = DateTime.UtcNow;

            snares.Add(getAvailableId(), this);

        }

        public static void activateSnare(byte snareId, PlayerControl snarer, PlayerControl target)
        {
            var snare = snares[snareId];

            // 有効にする
            snare.isActive = true;
            snare.target = target;
            var spriteRenderer = snare.snare.gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = snareActiveSprite;

            // 他のトラップを全て無効化する
            var newSnares = new SortedDictionary<byte, Snare>
            {
                { snareId, snare }
            };
            foreach (var t in snares.Values)
            {
                if (t.snare == null || t == snare) continue;
                t.snare.SetActive(false);
                UnityEngine.Object.Destroy(t.snare);
            }
            snares = newSnares;

            // 音を鳴らす
            /*trap.audioSource.Stop();
            trap.audioSource.loop = true;
            trap.audioSource.priority = 0;
            trap.audioSource.spatialBlend = 1;
            trap.audioSource.maxDistance = EvilTrapper.maxDistance;
            trap.audioSource.clip = countdown;
            trap.audioSource.Play();*/


            // ターゲットを動けなくする
            target.NetTransform.Halt();

            bool moveableFlag = false;
            FastDestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(Snarer.killTimer, new Action<float>((p) =>
            {
                try
                {
                    if (Snarer.meetingFlag) return;
                    if (snare == null || snare.snare == null || !snare.isActive) //　解除された場合の処理
                    {
                        if (!moveableFlag)
                        {
                            target.moveable = true;
                            moveableFlag = true;
                        }
                        return;
                    }
                    else if ((p == 1f) && !target.Data.IsDead)
                    { // 正常にキルが発生する場合の処理
                        target.moveable = true;
                        if (CachedPlayer.LocalPlayer.PlayerControl == Snarer.snarer)
                        {   
                            if (Helpers.checkAndDoVetKill(target)) return;
                            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.SnarerKill, Hazel.SendOption.Reliable, -1);
                            writer.Write(snareId);
                            writer.Write(CachedPlayer.LocalPlayer.PlayerControl.PlayerId);
                            writer.Write(target.PlayerId);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.snarerKill(snareId, CachedPlayer.LocalPlayer.PlayerControl.PlayerId, target.PlayerId);
                        }
                    }
                    else
                    { // カウントダウン中の処理
                        target.moveable = false;
                        //target.NetTransform.Halt();
                        target.transform.position = snare.snare.transform.position + new Vector3(0, 0.3f, 0);
                    }
                }
                catch (Exception e)
                {
                    TheOtherRolesPlugin.Logger.LogError("An error occured during the countdown");
                    TheOtherRolesPlugin.Logger.LogError(e.Message);
                }
            })));
        }

        public static void disableSnare(byte snareId)
        {
            var snare = snares[snareId];
            snare.isActive = false;
            //trap.audioSource.Stop();
            //trap.audioSource.PlayOneShot(disable);
            FastDestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(1/*disable.length*/, new Action<float>((p) =>
            {
                if (p == 1f)
                {
                    if (snare.snare != null)
                        snare.snare.SetActive(false);
                    UnityEngine.Object.Destroy(snare.snare);
                    snares.Remove(snareId);
                }
            })));

            if (CachedPlayer.LocalPlayer.PlayerControl == Snarer.snarer)
            {
                CachedPlayer.LocalPlayer.PlayerControl.killTimer = GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown + Snarer.penaltyTime;
                HudManagerStartPatch.snarerSetSnareButton.Timer = Snarer.cooldown + Snarer.penaltyTime;
            }
        }

        public static void onMeeting()
        {
            Snarer.meetingFlag = true;
            foreach (var snare in snares)
            {
                //trap.Value.audioSource.Stop();
                if (snare.Value.target != null)
                {

                    if (CachedPlayer.LocalPlayer.PlayerControl == Snarer.snarer)
                    {
                        if (!snare.Value.target.Data.IsDead)
                        {
                            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.SnarerKill, Hazel.SendOption.Reliable, -1);
                            writer.Write(snare.Key);
                            writer.Write(CachedPlayer.LocalPlayer.PlayerControl.PlayerId);
                            writer.Write(snare.Value.target.PlayerId);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.snarerKill(snare.Key, CachedPlayer.LocalPlayer.PlayerControl.PlayerId, snare.Value.target.PlayerId);
                        }
                    }

                }
            }
        }

        public static bool hasSnaredPlayer()
        {
            foreach (var snare in snares.Values)
            {
                if (snare.target != null) return true;
            }
            return false;
        }

        public static Snare getActiveSnare()
        {
            foreach (var snare in snares.Values)
            {
                if (snare.target != null) return snare;
            }
            return null;
        }

        public static bool isSnareped(PlayerControl p)
        {
            foreach (var snare in snares.Values)
            {
                if (snare.target == p) return true;
            }
            return false;
        }

        public static void snareKill(byte snareId, PlayerControl snarer, PlayerControl target)
        {
            var snare = snares[snareId];
           /* var audioSource = trap.audioSource;
            audioSource.Stop();
            audioSource.maxDistance = EvilTrapper.maxDistance;
            audioSource.PlayOneShot(kill);*/
            FastDestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(2/*kill.length*/, new Action<float>((p) =>
            {
                if (p == 1f)
                {
                    clearAllSnares();
                }
            })));
            Snarer.isSnareKill = true;
            KillAnimationCoPerformKillPatch.hideNextAnimation = true;
            snarer.MurderPlayer(target, MurderResultFlags.Succeeded);
        }

        public static void clearAllSnares()
        {
            loadSprite();
            foreach (var snare in snares.Values)
            {
                if (snare.snare != null)
                    UnityEngine.GameObject.DestroyObject(snare.snare);
            }
            snares = new SortedDictionary<byte, Snare>();
            maxId = 0;
        }

        [HarmonyPatch(typeof(PlayerPhysics), nameof(PlayerPhysics.FixedUpdate))]
        public static class PlayerPhysicsSnarePatch
        {
            public static void Postfix(PlayerPhysics __instance)
            {
                foreach (var snare in Snare.snares.Values)
                {
                    bool canSee =
                        snare.isActive ||
                        CachedPlayer.LocalPlayer.PlayerControl.Data.Role.IsImpostor ||
                        CachedPlayer.LocalPlayer.PlayerControl.Data.IsDead || 
                        CachedPlayer.LocalPlayer.PlayerControl == TheOtherRoles.Lighter.lighter;
                    var opacity = canSee ? 1.0f : 0.0f;
                    if (snare.snare != null)
                        snare.snare.GetComponent<SpriteRenderer>().material.color = Color.Lerp(Palette.ClearWhite, Palette.White, opacity);
                }
            }
        }
    }
}