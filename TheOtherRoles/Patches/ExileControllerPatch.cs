using HarmonyLib;
using Hazel;
using System.Collections.Generic;
using System.Linq;
using static TheOtherRoles.TheOtherRoles;
using TheOtherRoles.Objects;
using System;
using TheOtherRoles.Players;
using TheOtherRoles.Utilities;
using UnityEngine;

namespace TheOtherRoles.Patches {
    [HarmonyPatch(typeof(ExileController), nameof(ExileController.BeginForGameplay))]
    [HarmonyPriority(Priority.First)]
    class ExileControllerBeginPatch {
        public static NetworkedPlayerInfo lastExiled;
        public static void Prefix(ExileController __instance, [HarmonyArgument(0)]ref NetworkedPlayerInfo exiled, [HarmonyArgument(1)]bool tie) {
            lastExiled = exiled;

            // Medic shield
            if (Medic.medic != null && AmongUsClient.Instance.AmHost && Medic.futureShielded != null && !Medic.medic.Data.IsDead) { // We need to send the RPC from the host here, to make sure that the order of shifting and setting the shield is correct(for that reason the futureShifted and futureShielded are being synced)
                MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.MedicSetShielded, Hazel.SendOption.Reliable, -1);
                writer.Write(Medic.futureShielded.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
                RPCProcedure.medicSetShielded(Medic.futureShielded.PlayerId);
            }
            if (Medic.usedShield) Medic.meetingAfterShielding = true;  // Has to be after the setting of the shield

            // Shifter shift
            if (Shifter.shifter != null && AmongUsClient.Instance.AmHost && Shifter.futureShift != null) { // We need to send the RPC from the host here, to make sure that the order of shifting and erasing is correct (for that reason the futureShifted and futureErased are being synced)
                MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.ShifterShift, Hazel.SendOption.Reliable, -1);
                writer.Write(Shifter.futureShift.PlayerId);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
                RPCProcedure.shifterShift(Shifter.futureShift.PlayerId);
            }
            Shifter.futureShift = null;

            // Eraser erase
            if (Eraser.eraser != null && AmongUsClient.Instance.AmHost && Eraser.futureErased != null) {  // We need to send the RPC from the host here, to make sure that the order of shifting and erasing is correct (for that reason the futureShifted and futureErased are being synced)
                foreach (PlayerControl target in Eraser.futureErased) {
                    if (target != null && target.canBeErased()) {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.ErasePlayerRoles, Hazel.SendOption.Reliable, -1);
                    writer.Write(target.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.erasePlayerRoles(target.PlayerId);
                    Eraser.alreadyErased.Add(target.PlayerId);
                    }
                }
            }
            Eraser.futureErased = new List<PlayerControl>();

            // Trickster boxes
            if (Trickster.trickster != null && JackInTheBox.hasJackInTheBoxLimitReached()) {
                JackInTheBox.convertToVents();
            }

            // Activate portals.
            Portal.meetingEndsUpdate();

            if (Doomsayer.doomsayer != null && AmongUsClient.Instance.AmHost && !Doomsayer.canGuess)
            {
                Doomsayer.canGuess = true;
            }

            // Witch execute casted spells
            if (Witch.witch != null && Witch.futureSpelled != null && AmongUsClient.Instance.AmHost) {
                bool exiledIsWitch = exiled != null && exiled.PlayerId == Witch.witch.PlayerId;
                bool witchDiesWithExiledLover = exiled != null && Lovers.existing() && Lovers.bothDie && (Lovers.lover1.PlayerId == Witch.witch.PlayerId || Lovers.lover2.PlayerId == Witch.witch.PlayerId) && (exiled.PlayerId == Lovers.lover1.PlayerId || exiled.PlayerId == Lovers.lover2.PlayerId);

                if ((witchDiesWithExiledLover || exiledIsWitch) && Witch.witchVoteSavesTargets) Witch.futureSpelled = new List<PlayerControl>();
                foreach (PlayerControl target in Witch.futureSpelled) {
                    if (target != null && !target.Data.IsDead && Helpers.checkMuderAttempt(Witch.witch, target, true) == MurderAttemptResult.PerformKill){
                        if (exiled != null && Lawyer.lawyer != null && (target == Lawyer.lawyer || target == Lovers.otherLover(Lawyer.lawyer)) && Lawyer.target != null && Lawyer.isProsecutor && Lawyer.target.PlayerId == exiled.PlayerId)
                            continue;
                        if (target == Lawyer.target && Lawyer.lawyer != null) {
                            MessageWriter writer2 = AmongUsClient.Instance.StartRpcImmediately(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.LawyerPromotesToPursuer, Hazel.SendOption.Reliable, -1);
                            AmongUsClient.Instance.FinishRpcImmediately(writer2);
                            RPCProcedure.lawyerPromotesToPursuer();
                        }
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.UncheckedExilePlayer, Hazel.SendOption.Reliable, -1);
                        writer.Write(target.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.uncheckedExilePlayer(target.PlayerId);

                        MessageWriter writer3 = AmongUsClient.Instance.StartRpcImmediately(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.ShareGhostInfo, Hazel.SendOption.Reliable, -1);
                        writer3.Write(CachedPlayer.LocalPlayer.PlayerId);
                        writer3.Write((byte)RPCProcedure.GhostInfoTypes.DeathReasonAndKiller);
                        writer3.Write(target.PlayerId);
                        writer3.Write((byte)DeadPlayer.CustomDeathReason.WitchExile);
                        writer3.Write(Witch.witch.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer3);

                        GameHistory.overrideDeathReasonAndKiller(target, DeadPlayer.CustomDeathReason.WitchExile, killer: Witch.witch);
                    }
                }
            }
            Witch.futureSpelled = new List<PlayerControl>();

            // SecurityGuard vents and cameras
            var allCameras = MapUtilities.CachedShipStatus.AllCameras.ToList();
            TORMapOptions.camerasToAdd.ForEach(camera => {
                camera.gameObject.SetActive(true);
                camera.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                allCameras.Add(camera);
            });
            MapUtilities.CachedShipStatus.AllCameras = allCameras.ToArray();
            TORMapOptions.camerasToAdd = new List<SurvCamera>();

            foreach (Vent vent in TORMapOptions.ventsToSeal) {
                PowerTools.SpriteAnim animator = vent.GetComponent<PowerTools.SpriteAnim>();
                vent.EnterVentAnim = vent.ExitVentAnim = null;
                Sprite newSprite = animator == null ? SecurityGuard.getStaticVentSealedSprite() : SecurityGuard.getAnimatedVentSealedSprite();
                SpriteRenderer rend = vent.myRend;
                if (Helpers.isFungle()) {
                    newSprite = SecurityGuard.getFungleVentSealedSprite();
                    rend = vent.transform.GetChild(3).GetComponent<SpriteRenderer>();
                    animator = vent.transform.GetChild(3).GetComponent<PowerTools.SpriteAnim>();
                }
                animator?.Stop();
                rend.sprite = newSprite;
                if (SubmergedCompatibility.IsSubmerged && vent.Id == 0) vent.myRend.sprite = SecurityGuard.getSubmergedCentralUpperSealedSprite();
                if (SubmergedCompatibility.IsSubmerged && vent.Id == 14) vent.myRend.sprite = SecurityGuard.getSubmergedCentralLowerSealedSprite();
                rend.color = Color.white;
                vent.name = "SealedVent_" + vent.name;
            }
            TORMapOptions.ventsToSeal = new List<Vent>();

            EventUtility.meetingEndsUpdate();
        }
    }

    [HarmonyPatch]
    class ExileControllerWrapUpPatch {

        [HarmonyPatch(typeof(ExileController), nameof(ExileController.WrapUp))]
        class BaseExileControllerPatch {
            public static void Postfix(ExileController __instance) {
                WrapUpPostfix(__instance.initData.networkedPlayer);
            }
        }

        [HarmonyPatch(typeof(AirshipExileController), nameof(AirshipExileController.WrapUpAndSpawn))]
        class AirshipExileControllerPatch {
            public static void Postfix(AirshipExileController __instance) {
                WrapUpPostfix(__instance.initData.networkedPlayer);
            }
        }

        // Workaround to add a "postfix" to the destroying of the exile controller (i.e. cutscene) and SpwanInMinigame of submerged
        [HarmonyPatch(typeof(UnityEngine.Object), nameof(UnityEngine.Object.Destroy), new Type[] { typeof(GameObject) })]
        public static void Prefix(GameObject obj) {
            // Nightvision:
            if (obj != null && obj.name != null && obj.name.Contains("FungleSecurity")) {
                SurveillanceMinigamePatch.resetNightVision();
                return;
            }

            // submerged
            if (!SubmergedCompatibility.IsSubmerged) return;
            if (obj.name.Contains("ExileCutscene")) {
                WrapUpPostfix(ExileControllerBeginPatch.lastExiled);
            } else if (obj.name.Contains("SpawnInMinigame")) {
                AntiTeleport.setPosition();
                Chameleon.lastMoved.Clear();
            }
        }

        static void WrapUpPostfix(NetworkedPlayerInfo exiled) {
            // Prosecutor win condition
            if (exiled != null && Lawyer.lawyer != null && Lawyer.target != null && Lawyer.isProsecutor && Lawyer.target.PlayerId == exiled.PlayerId && !Lawyer.lawyer.Data.IsDead)
                Lawyer.triggerProsecutorWin = true;

            // Mini exile lose condition
            else if (exiled != null && Mini.mini != null && Mini.mini.PlayerId == exiled.PlayerId && !Mini.isGrownUp() && !Mini.mini.Data.Role.IsImpostor && !RoleInfo.getRoleInfoForPlayer(Mini.mini).Any(x => x.isNeutral)) {
                Mini.triggerMiniLose = true;
            }
            // Jester win condition
            else if (exiled != null && Jester.jester != null && Jester.jester.PlayerId == exiled.PlayerId) {
                Jester.triggerJesterWin = true;
            }


            // Reset custom button timers where necessary
            CustomButton.MeetingEndedUpdate();

            // Mini set adapted cooldown
            if (Mini.mini != null && CachedPlayer.LocalPlayer.PlayerControl == Mini.mini && Mini.mini.Data.Role.IsImpostor) {
                var multiplier = Mini.isGrownUp() ? 0.66f : 2f;
                Mini.mini.SetKillTimer(GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown * multiplier);
            }

            // Seer spawn souls
            if (Seer.deadBodyPositions != null && Seer.seer != null && CachedPlayer.LocalPlayer.PlayerControl == Seer.seer && (Seer.mode == 0 || Seer.mode == 2)) {
                foreach (Vector3 pos in Seer.deadBodyPositions) {
                    GameObject soul = new GameObject();
                    //soul.transform.position = pos;
                    soul.transform.position = new Vector3(pos.x, pos.y, pos.y / 1000 - 1f);
                    soul.layer = 5;
                    var rend = soul.AddComponent<SpriteRenderer>();
                    soul.AddSubmergedComponent(SubmergedCompatibility.Classes.ElevatorMover);
                    rend.sprite = Seer.getSoulSprite();
                    
                    if(Seer.limitSoulDuration) {
                        FastDestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(Seer.soulDuration, new Action<float>((p) => {
                            if (rend != null) {
                                var tmp = rend.color;
                                tmp.a = Mathf.Clamp01(1 - p);
                                rend.color = tmp;
                            }    
                            if (p == 1f && rend != null && rend.gameObject != null) UnityEngine.Object.Destroy(rend.gameObject);
                        })));
                    }
                }
                Seer.deadBodyPositions = new List<Vector3>();
            }

            // Tracker reset deadBodyPositions
            Tracker.deadBodyPositions = new List<Vector3>();

            // Reset Arbiter settings.
            if (Arbiter.arbiter != null)
                Arbiter.specialVoteTargetPlayerId = byte.MaxValue;
            

            // Arsonist deactivate dead poolable players
            if (Arsonist.arsonist != null && Arsonist.arsonist == CachedPlayer.LocalPlayer.PlayerControl) {
                int visibleCounter = 0;
                Vector3 newBottomLeft = IntroCutsceneOnDestroyPatch.bottomLeft;
                var BottomLeft = newBottomLeft + new Vector3(-0.25f, -0.25f, 0);
                foreach (PlayerControl p in CachedPlayer.AllPlayers) {
                    if (!TORMapOptions.playerIcons.ContainsKey(p.PlayerId)) continue;
                    if (p.Data.IsDead || p.Data.Disconnected) {
                        TORMapOptions.playerIcons[p.PlayerId].gameObject.SetActive(false);
                    } else {
                        TORMapOptions.playerIcons[p.PlayerId].transform.localPosition = newBottomLeft + Vector3.right * visibleCounter * 0.35f;
                        visibleCounter++;
                    }
                }
            }

            // Deputy check Promotion, see if the sheriff still exists. The promotion will be after the meeting.
            if (Deputy.deputy != null)
            {
                PlayerControlFixedUpdatePatch.deputyCheckPromotion(isMeeting: true);
            }

            // Force Bounty Hunter Bounty Update
            if (BountyHunter.bountyHunter != null && BountyHunter.bountyHunter == CachedPlayer.LocalPlayer.PlayerControl)
                BountyHunter.bountyUpdateTimer = 0f;

            // Medium spawn souls
            if (Medium.medium != null && CachedPlayer.LocalPlayer.PlayerControl == Medium.medium) {
                if (Medium.souls != null) {
                    foreach (SpriteRenderer sr in Medium.souls) UnityEngine.Object.Destroy(sr.gameObject);
                    Medium.souls = new List<SpriteRenderer>();
                }

                if (Medium.futureDeadBodies != null) {
                    foreach ((DeadPlayer db, Vector3 ps) in Medium.futureDeadBodies) {
                        GameObject s = new GameObject();
                        //s.transform.position = ps;
                        s.transform.position = new Vector3(ps.x, ps.y, ps.y / 1000 - 1f);
                        s.layer = 5;
                        var rend = s.AddComponent<SpriteRenderer>();
                        s.AddSubmergedComponent(SubmergedCompatibility.Classes.ElevatorMover);
                        rend.sprite = Medium.getSoulSprite();
                        Medium.souls.Add(rend);
                    }
                    Medium.deadBodies = Medium.futureDeadBodies;
                    Medium.futureDeadBodies = new List<Tuple<DeadPlayer, Vector3>>();
                }
            }
            
             // Clear all snares
            Snare.clearAllSnares();
            Snarer.meetingFlag = false;


            // AntiTeleport set position
            AntiTeleport.setPosition();
             if (CustomOptionHolder.randomGameStartPosition.getBool() &&  (AntiTeleport.antiTeleport.FindAll(x => x.PlayerId == CachedPlayer.LocalPlayer.PlayerControl.PlayerId).Count == 0)) { //Random spawn on round start

                List<Vector3> skeldSpawn = new List<Vector3>() {
                new Vector3(-2.2f, 2.2f, 0.0f), //cafeteria. botton. top left.
                new Vector3(0.7f, 2.2f, 0.0f), //caffeteria. button. top right.
                new Vector3(-2.2f, -0.2f, 0.0f), //caffeteria. button. bottom left.
                new Vector3(0.7f, -0.2f, 0.0f), //caffeteria. button. bottom right.
                new Vector3(10.0f, 3.0f, 0.0f), //weapons top
                new Vector3(9.0f, 1.0f, 0.0f), //weapons bottom
                new Vector3(6.5f, -3.5f, 0.0f), //O2
                new Vector3(11.5f, -3.5f, 0.0f), //O2-nav hall
                new Vector3(17.0f, -3.5f, 0.0f), //navigation top
                new Vector3(18.2f, -5.7f, 0.0f), //navigation bottom
                new Vector3(11.5f, -6.5f, 0.0f), //nav-shields top
                new Vector3(9.5f, -8.5f, 0.0f), //nav-shields bottom
                new Vector3(9.2f, -12.2f, 0.0f), //shields top
                new Vector3(8.0f, -14.3f, 0.0f), //shields bottom
                new Vector3(2.5f, -16f, 0.0f), //coms left
                new Vector3(4.2f, -16.4f, 0.0f), //coms middle
                new Vector3(5.5f, -16f, 0.0f), //coms right
                new Vector3(-1.5f, -10.0f, 0.0f), //storage top
                new Vector3(-1.5f, -15.5f, 0.0f), //storage bottom
                new Vector3(-4.5f, -12.5f, 0.0f), //storrage left
                new Vector3(0.3f, -12.5f, 0.0f), //storrage right
                new Vector3(4.5f, -7.5f, 0.0f), //admin top
                new Vector3(4.5f, -9.5f, 0.0f), //admin bottom
                new Vector3(-9.0f, -8.0f, 0.0f), //elec top left
                new Vector3(-6.0f, -8.0f, 0.0f), //elec top right
                new Vector3(-8.0f, -11.0f, 0.0f), //elec bottom
                new Vector3(-12.0f, -13.0f, 0.0f), //elec-lower hall
                new Vector3(-17f, -10f, 0.0f), //lower engine top
                new Vector3(-17.0f, -13.0f, 0.0f), //lower engine bottom
                new Vector3(-21.5f, -3.0f, 0.0f), //reactor top
                new Vector3(-21.5f, -8.0f, 0.0f), //reactor bottom
                new Vector3(-13.0f, -3.0f, 0.0f), //security top
                new Vector3(-12.6f, -5.6f, 0.0f), // security bottom
                new Vector3(-17.0f, 2.5f, 0.0f), //upper engibe top
                new Vector3(-17.0f, -1.0f, 0.0f), //upper engine bottom
                new Vector3(-10.5f, 1.0f, 0.0f), //upper-mad hall
                new Vector3(-10.5f, -2.0f, 0.0f), //medbay top
                new Vector3(-6.5f, -4.5f, 0.0f), //medbay bottom
                new Vector3(-8.75f,-8.5f,0f),//elec---------------------------------
                new Vector3(-9.15f,-4.75f,0f),//medbey
                new Vector3(6f,-3.5f,0f),//o2
                new Vector3(6.25f,-8.5f,0f),//admin
                new Vector3(-17.75f,2.5f,0f),//upperengine
                new Vector3(2.75f,-15.25f,0f),//comms
                new Vector3(-17.75f,-13.25f,0f),//lowerengine
                new Vector3(9.75f,2.75f,0f),//weapons
                new Vector3(-13.5f,-6.75f,0f),//seguridad
                new Vector3(9.5f,-12.25f,0f),//shields
                new Vector3(-21.5f,-2.5f,0f),//reactor
                new Vector3(16.5f,-3.5f,0f),//nav
                new Vector3(-0.75f,5.25f,0f),//caftereriaupper
                new Vector3(-1.75f,-16f,0f),//stoage
                new Vector3(-0.75f,-2.75f,0f),//caftererialower
                };

                List<Vector3> miraSpawn = new List<Vector3>() {
                new Vector3(-4.5f, 3.5f, 0.0f), //launchpad top
                new Vector3(-4.5f, -1.4f, 0.0f), //launchpad bottom
                new Vector3(8.5f, -1f, 0.0f), //launchpad- med hall
                new Vector3(14f, -1.5f, 0.0f), //medbay
                new Vector3(16.5f, 3f, 0.0f), // comms
                new Vector3(10f, 5f, 0.0f), //lockers
                new Vector3(6f, 1.5f, 0.0f), //locker room
                new Vector3(2.5f, 13.6f, 0.0f), //reactor
                new Vector3(6f, 12f, 0.0f), //reactor middle
                new Vector3(9.5f, 13f, 0.0f), //lab
                new Vector3(15f, 9f, 0.0f), //bottom left cross
                new Vector3(17.9f, 11.5f, 0.0f), //middle cross
                new Vector3(14f, 17.3f, 0.0f), //office
                new Vector3(19.5f, 21f, 0.0f), //admin
                new Vector3(14f, 24f, 0.0f), //greenhouse left
                new Vector3(22f, 24f, 0.0f), //greenhouse right
                new Vector3(21f, 8.5f, 0.0f), //bottom right cross
                new Vector3(28f, 3f, 0.0f), //caf right
                new Vector3(22f, 3f, 0.0f), //caf left
                new Vector3(19f, 4f, 0.0f), //storage
                new Vector3(22f, -2f, 0.0f), //balcony
                new Vector3(19.5f, 4.65f, 0f), // storage--------------------------
                new Vector3(11.25f, 10.5f, 0f), // lab
                new Vector3(14.75f, 20.5f, 0f), // office
                new Vector3(15.5f, -0.5f, 0f), // medbey
                new Vector3(27.5f, -1.75f, 0f), // balcony
                new Vector3(2.5f, 13.15f, 0f), // reactor
                new Vector3(15.5f, 4f, 0f), // comms
                new Vector3(21f, 20.5f, 0f), // admin
                new Vector3(27, 4.75f, 0f), // cafeteria
                new Vector3(6.15f, 6.5f, 0f), // decom
                new Vector3(5f, -1.25f, 0f), // long hallway
                new Vector3(16.15f, 24.25f, 0f), // greenhouse
                new Vector3(-4.35f, 3.25f, 0f), // launch pad
                new Vector3(9.5f, 1.25f, 0f), // lockroom
                new Vector3(18f, 11.5f, 0f), // midway
                new Vector3(-4.5f, 2.75f, 0f), //-----------------------------
                new Vector3(9.15f, 4.75f, 0f), 
                new Vector3(11.5f, 10.3f, 0f), 
                new Vector3(2.5f, 13f, 0f), 
                new Vector3(14.25f, 2.85f, 0f), 
                new Vector3(14.25f, -1.5f, 0f), 
                new Vector3(19.5f, -1.75f, 0f), 
                new Vector3(19.5f, 4.35f, 0f), 
                new Vector3(28.25f, 0f, 0f),
                new Vector3(22.45f, 18.75f, 0f), 
                new Vector3(13.75f, 18.75f, 0f), 
                new Vector3(19.25f, 24.25f, 0f), 
                };

                List<Vector3> polusSpawn = new List<Vector3>() {
                new Vector3(16.6f, -1f, 0.0f), //dropship top
                new Vector3(16.6f, -5f, 0.0f), //dropship bottom
                new Vector3(20f, -9f, 0.0f), //above storrage
                new Vector3(22f, -7f, 0.0f), //right fuel
                new Vector3(25.5f, -6.9f, 0.0f), //drill
                new Vector3(29f, -9.5f, 0.0f), //lab lockers
                new Vector3(29.5f, -8f, 0.0f), //lab weather notes
                new Vector3(35f, -7.6f, 0.0f), //lab table
                new Vector3(40.4f, -8f, 0.0f), //lab scan
                new Vector3(33f, -10f, 0.0f), //lab toilet
                new Vector3(39f, -15f, 0.0f), //specimen hall top
                new Vector3(36.5f, -19.5f, 0.0f), //specimen top
                new Vector3(36.5f, -21f, 0.0f), //specimen bottom
                new Vector3(28f, -21f, 0.0f), //specimen hall bottom
                new Vector3(24f, -20.5f, 0.0f), //admin tv
                new Vector3(22f, -25f, 0.0f), //admin books
                new Vector3(16.6f, -17.5f, 0.0f), //office coffe
                new Vector3(22.5f, -16.5f, 0.0f), //office projector
                new Vector3(24f, -17f, 0.0f), //office figure
                new Vector3(27f, -16.5f, 0.0f), //office lifelines
                new Vector3(32.7f, -15.7f, 0.0f), //lavapool
                new Vector3(31.5f, -12f, 0.0f), //snowmad below lab
                new Vector3(25.9f, -12.9f, 0.0f), //behind rock
                new Vector3(10f, -14f, 0.0f), //below storrage
                new Vector3(21.5f, -12.5f, 0.0f), //storrage vent
                new Vector3(19f, -11f, 0.0f), //storrage toolrack
                new Vector3(12f, -7f, 0.0f), //left fuel
                new Vector3(5f, -7.5f, 0.0f), //above elec
                new Vector3(10f, -12f, 0.0f), //elec fence
                new Vector3(9f, -9f, 0.0f), //elec lockers
                new Vector3(5f, -9f, 0.0f), //elec window
                new Vector3(4f, -11.2f, 0.0f), //elec tapes
                new Vector3(5.5f, -16f, 0.0f), //elec-O2 hall
                new Vector3(1f, -17.5f, 0.0f), //O2 tree hayball
                new Vector3(3f, -21f, 0.0f), //O2 middle
                new Vector3(2f, -19f, 0.0f), //O2 gas
                new Vector3(1f, -24f, 0.0f), //O2 water
                new Vector3(7f, -24f, 0.0f), //under O2
                new Vector3(9f, -20f, 0.0f), //right outside of O2
                new Vector3(7f, -15.8f, 0.0f), //snowman under elec
                new Vector3(11f, -17f, 0.0f), //comms table
                new Vector3(12.7f, -15.5f, 0.0f), //coms antenna pult
                new Vector3(13f, -24.5f, 0.0f), //weapons window
                new Vector3(11f, -23.5f, 0.0f), //weapons generator
                new Vector3(15f, -17f, 0.0f), //between coms-office
                new Vector3(17.5f, -25.7f, 0.0f), //snowman under office
                new Vector3(9.75f, -12.15f, 0f), // elec---------------------------------------
                new Vector3(40.5f, -7.75f, 0f), // right lab
                new Vector3(11f, -23f, 0f), // weapons
                new Vector3(36.5f, -21.5f, 0f), // specifmen
                new Vector3(1f, -16.5f, 0f), // up o2
                new Vector3(27.75f, -7.5f, 0f), // left lab
                new Vector3(26.5f, -17f, 0f), // right office
                new Vector3(12.5f, -16.5f, 0f), // comms
                new Vector3(16.75f, -1f, 0f), // launch pad
                new Vector3(22f, -25.15f, 0f), // admin
                new Vector3(1.75f, -23.75f, 0f), // low o2
                new Vector3(17.15f, -17f, 0f), // left office
                new Vector3(3.75f, -12f, 0f), // secutiry
                new Vector3(20.75f, -12f, 0f), // storage
                new Vector3(1.5f, -20f, 0f), // mid o2
                };

                List<Vector3> dleksSpawn = new List<Vector3>() {
                new Vector3(2.2f, 2.2f, 0.0f), //cafeteria. botton. top left.
                new Vector3(-0.7f, 2.2f, 0.0f), //caffeteria. button. top right.
                new Vector3(2.2f, -0.2f, 0.0f), //caffeteria. button. bottom left.
                new Vector3(-0.7f, -0.2f, 0.0f), //caffeteria. button. bottom right.
                new Vector3(-10.0f, 3.0f, 0.0f), //weapons top
                new Vector3(-9.0f, 1.0f, 0.0f), //weapons bottom
                new Vector3(-6.5f, -3.5f, 0.0f), //O2
                new Vector3(-11.5f, -3.5f, 0.0f), //O2-nav hall
                new Vector3(-17.0f, -3.5f, 0.0f), //navigation top
                new Vector3(-18.2f, -5.7f, 0.0f), //navigation bottom
                new Vector3(-11.5f, -6.5f, 0.0f), //nav-shields top
                new Vector3(-9.5f, -8.5f, 0.0f), //nav-shields bottom
                new Vector3(-9.2f, -12.2f, 0.0f), //shields top
                new Vector3(-8.0f, -14.3f, 0.0f), //shields bottom
                new Vector3(-2.5f, -16f, 0.0f), //coms left
                new Vector3(-4.2f, -16.4f, 0.0f), //coms middle
                new Vector3(-5.5f, -16f, 0.0f), //coms right
                new Vector3(1.5f, -10.0f, 0.0f), //storage top
                new Vector3(1.5f, -15.5f, 0.0f), //storage bottom
                new Vector3(4.5f, -12.5f, 0.0f), //storrage left
                new Vector3(-0.3f, -12.5f, 0.0f), //storrage right
                new Vector3(-4.5f, -7.5f, 0.0f), //admin top
                new Vector3(-4.5f, -9.5f, 0.0f), //admin bottom
                new Vector3(9.0f, -8.0f, 0.0f), //elec top left
                new Vector3(6.0f, -8.0f, 0.0f), //elec top right
                new Vector3(8.0f, -11.0f, 0.0f), //elec bottom
                new Vector3(12.0f, -13.0f, 0.0f), //elec-lower hall
                new Vector3(17f, -10f, 0.0f), //lower engine top
                new Vector3(17.0f, -13.0f, 0.0f), //lower engine bottom
                new Vector3(21.5f, -3.0f, 0.0f), //reactor top
                new Vector3(21.5f, -8.0f, 0.0f), //reactor bottom
                new Vector3(13.0f, -3.0f, 0.0f), //security top
                new Vector3(12.6f, -5.6f, 0.0f), // security bottom
                new Vector3(17.0f, 2.5f, 0.0f), //upper engibe top
                new Vector3(17.0f, -1.0f, 0.0f), //upper engine bottom
                new Vector3(10.5f, 1.0f, 0.0f), //upper-mad hall
                new Vector3(10.5f, -2.0f, 0.0f), //medbay top
                new Vector3(6.5f, -4.5f, 0.0f) //medbay bottom
                };

                List<Vector3> fungleSpawn = new List<Vector3>() {
                new Vector3(-9.81f, 0.6f, 0.0f), //campfire.
                new Vector3(-8f, 10.5f, 0.0f), //dropship.
                new Vector3(-16.16f, 7.25f, 0.0f), //cafe.
                new Vector3(-15.5f, -7.5f, 0.0f), //kitchen.
                new Vector3(9.25f, -12f, 0.0f), //warmhouse
                new Vector3(14.75f, 0f, 0.0f), //middle
                new Vector3(21.65f, 13.75f, 0.0f), //communication
                new Vector3(-11f, 12.75f, 0.0f),//-------------------------
                new Vector3(-11.5f, 8.5f, 0.0f),
                new Vector3(-17.35f, 7.15f, 0.0f),
                new Vector3(-22.15f, -2.25f, 0.0f),
                new Vector3(-15.5f, -2.575f, 0.0f),
                new Vector3(-22.75f, -7.25f, 0.0f),
                new Vector3(-8.85f, -14.25f, 0.0f),
                new Vector3(-3.3f, -10.5f, 0.0f),
                new Vector3(10.75f, -15f, 0.0f),
                new Vector3(7.7f, -10f, 0.0f),
                new Vector3(0.55f, -1.75f, 0.0f),
                new Vector3(2.25f, 4.25f, 0.0f),
                new Vector3(2.175f, 1.5f, 0.0f),
                new Vector3(23.25f, -7.75f, 0.0f),
                new Vector3(10f, 1.15f, 0.0f),
                new Vector3(15.25f, 4.75f, 0.0f),
                new Vector3(13.25f, 10.25f, 0.0f),
                new Vector3(20.25f, 7.5f, 0.0f),
                new Vector3(24.4f, 14.45f, 0.0f),
                new Vector3(-18.25f, 5f, 0.0f),//----------------------------
                new Vector3(-22.65f, -7.15f, 0.0f),
                new Vector3(2, 4.35f, 0.0f),
                new Vector3(-3.15f, -10.5f, 0.0f),
                new Vector3(23.7f, -7.8f, 0.0f),
                new Vector3(-4.75f, -1.75f, 0.0f),
                new Vector3(8f, -10f, 0.0f),
                new Vector3(22.3f, 3.3f, 0.0f),
                new Vector3(20.5f, 7.35f, 0.0f),
                new Vector3(24.15f, 14.45f, 0.0f),
                new Vector3(-16.12f, 0.7f, 0.0f),
                new Vector3(1.65f, -1.5f, 0.0f),
                new Vector3(10.5f, -12f, 0.0f),
                new Vector3(7f, 1.75f, 0.0f),
                new Vector3(13.25f, 10f, 0.0f),
                };

                List<Vector3> airshipSpawn = new List<Vector3>() { }; //no spawns since it already has random spawns

                if (GameOptionsManager.Instance.currentGameOptions.MapId == 0) PlayerControl.LocalPlayer.transform.position = skeldSpawn[rnd.Next(skeldSpawn.Count)];
                if (GameOptionsManager.Instance.currentGameOptions.MapId == 1) PlayerControl.LocalPlayer.transform.position = miraSpawn[rnd.Next(miraSpawn.Count)];
                if (GameOptionsManager.Instance.currentGameOptions.MapId == 2) PlayerControl.LocalPlayer.transform.position = polusSpawn[rnd.Next(polusSpawn.Count)];
                if (GameOptionsManager.Instance.currentGameOptions.MapId == 3) PlayerControl.LocalPlayer.transform.position = dleksSpawn[rnd.Next(dleksSpawn.Count)];
                if (GameOptionsManager.Instance.currentGameOptions.MapId == 4) PlayerControl.LocalPlayer.transform.position = airshipSpawn[rnd.Next(airshipSpawn.Count)];
                if (GameOptionsManager.Instance.currentGameOptions.MapId == 5) PlayerControl.LocalPlayer.transform.position = fungleSpawn[rnd.Next(fungleSpawn.Count)];

            }


            // Invert add meeting
            if (Invert.meetings > 0) Invert.meetings--;

            Chameleon.lastMoved.Clear();

            foreach (Trap trap in Trap.traps) trap.triggerable = false;
            FastDestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown / 2 + 2, new Action<float>((p) => {
            if (p == 1f) foreach (Trap trap in Trap.traps) trap.triggerable = true;
            })));

            if (!Yoyo.markStaysOverMeeting)
                Silhouette.clearSilhouettes();
        }
    }

    [HarmonyPatch(typeof(SpawnInMinigame), nameof(SpawnInMinigame.Close))]  // Set position of AntiTp players AFTER they have selected a spawn.
    class AirshipSpawnInPatch {
        static void Postfix() {
            AntiTeleport.setPosition();
            Chameleon.lastMoved.Clear();
        }
    }

    [HarmonyPatch(typeof(TranslationController), nameof(TranslationController.GetString), new Type[] { typeof(StringNames), typeof(Il2CppReferenceArray<Il2CppSystem.Object>) })]
    class ExileControllerMessagePatch {
        static void Postfix(ref string __result, [HarmonyArgument(0)]StringNames id) {
            try {
                if (ExileController.Instance != null && ExileController.Instance.initData.networkedPlayer != null) {
                    PlayerControl player = Helpers.playerById(ExileController.Instance.initData.networkedPlayer.Object.PlayerId);
                    if (player == null) return;
                    // Exile role text
                    if (id == StringNames.ExileTextPN || id == StringNames.ExileTextSN || id == StringNames.ExileTextPP || id == StringNames.ExileTextSP) {
                        __result = player.Data.PlayerName + " 是 " + String.Join(" ", RoleInfo.getRoleInfoForPlayer(player, false).Select(x => x.name).ToArray());
                    }
                    // Hide number of remaining impostors on Jester win
                    if (id == StringNames.ImpostorsRemainP || id == StringNames.ImpostorsRemainS) {
                        if (Jester.jester != null && player.PlayerId == Jester.jester.PlayerId) __result = "";
                    }
                    if (Arbiter.specialVoteTargetPlayerId != byte.MaxValue)
                    {
                        if (CustomOptionHolder.arbiterSpecificMessageMode.getBool()) __result += "被裁决";
                        Tiebreaker.isTiebreak = false;
                        Arbiter.specialVoteTargetPlayerId = byte.MaxValue;
                    }
                    if (Tiebreaker.isTiebreak) __result += " (被破平)";
                    Tiebreaker.isTiebreak = false;
                }
            } catch {
                // pass - Hopefully prevent leaving while exiling to softlock game
            }
        }
    }
}
