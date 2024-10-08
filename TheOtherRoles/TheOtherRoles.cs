using System.Linq;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using TheOtherRoles.Objects;
using TheOtherRoles.Players;
using TheOtherRoles.Utilities;
using TheOtherRoles.CustomGameModes;
using static TheOtherRoles.TheOtherRoles;
using AmongUs.Data;
using Hazel;
using Reactor.Utilities.Extensions;

namespace TheOtherRoles
{
    [HarmonyPatch]
    public static class TheOtherRoles
    {
        public static System.Random rnd = new System.Random((int)DateTime.Now.Ticks);

        public static void clearAndReloadRoles() {
            Jester.clearAndReload();
			Swooper.clearAndReload();
            Poucher.clearAndReload();
			Mimic.clearAndReload();
            Escapist.clearAndReload();
            Snarer.clearAndReload();
            Mayor.clearAndReload();
            Portalmaker.clearAndReload();
            Engineer.clearAndReload();
            Sheriff.clearAndReload();
			Cursed.clearAndReload();
            Deputy.clearAndReload();
	        Amnisiac.clearAndReload();
            Lighter.clearAndReload();
            Godfather.clearAndReload();
            Mafioso.clearAndReload();
            Janitor.clearAndReload();
            Detective.clearAndReload();
            Werewolf.clearAndReload();
            Juggernaut.clearAndReload();
            Doomsayer.clearAndReload();
            Kataomoi.clearAndReload();
            Akujo.clearAndReload();
            Cerenovus.clearAndReload();
            TimeMaster.clearAndReload();
            BodyGuard.clearAndReload();
            Veteren.clearAndReload();
            Medic.clearAndReload();
            Shifter.clearAndReload();
            Swapper.clearAndReload();
            Lovers.clearAndReload();
            Seer.clearAndReload();
            Morphling.clearAndReload();
            Camouflager.clearAndReload();
            Hacker.clearAndReload();
            Tracker.clearAndReload();
            Vampire.clearAndReload();
            Snitch.clearAndReload();
            Jackal.clearAndReload();
            Sidekick.clearAndReload();
            Eraser.clearAndReload();
            Spy.clearAndReload();
            Trickster.clearAndReload();
            Cleaner.clearAndReload();
            Warlock.clearAndReload();
            SecurityGuard.clearAndReload();
            Arsonist.clearAndReload();
            BountyHunter.clearAndReload();
            Vulture.clearAndReload();
            Medium.clearAndReload();
            Lawyer.clearAndReload();
            Pursuer.clearAndReload();
            Witch.clearAndReload();
            Ninja.clearAndReload();
            Thief.clearAndReload();
            Trapper.clearAndReload();
            Bomber.clearAndReload();
            Yoyo.clearAndReload();
            Pitfaller.clearAndReload();
            Shinobi.clearAndReload();
            Transporter.clearAndReload();
            Transportist.clearAndReload();
            Teleporter.clearAndReload();
            Miner.clearAndReload();
            Arbiter.clearAndReload();

            // Modifier
            Bait.clearAndReload();
            Bloody.clearAndReload();
            AntiTeleport.clearAndReload();
            Tiebreaker.clearAndReload();
            Sunglasses.clearAndReload();
            Mini.clearAndReload();
            Paranoid.clearAndReload();
            Slueth.clearAndReload();
            Radar.clearAndReload();
            AllKnowing.clearAndReload();
            Viewer.clearAndReload();
            AntiReport.clearAndReload();
            Indomitable.clearAndReload();
            Vip.clearAndReload();
            Invert.clearAndReload();
            Chameleon.clearAndReload();
            Disperser.clearAndReload();
            Recruiter.clearAndReload();
            Flash.clearAndReload();
            Giant.clearAndReload();
            OneTimeKiller.clearAndReload();

            // Gamemodes
            HandleGuesser.clearAndReload();
            HideNSeek.clearAndReload();
            PropHunt.clearAndReload();
        }

        public static class Jester {
            public static PlayerControl jester;
            public static Color color = new Color32(236, 98, 165, byte.MaxValue);

            public static bool triggerJesterWin = false;
            public static bool canVent = false;
            public static bool canCallEmergency = true;
            public static bool hasImpostorVision = false;

            public static void clearAndReload() {
                jester = null;
                triggerJesterWin = false;
                canVent = CustomOptionHolder.jesterCanVent.getBool();
                canCallEmergency = CustomOptionHolder.jesterCanCallEmergency.getBool();
                hasImpostorVision = CustomOptionHolder.jesterHasImpostorVision.getBool();
            }
        }
        
       public static class Doomsayer
    {
        public static PlayerControl doomsayer;
        //public static PlayerControl evilGuesser;
        public static Color color = new Color(0f, 1f, 0.5f, 1f);
        public static PlayerControl currentTarget;
        public static List<PlayerControl> playerTargetinformation = new List<PlayerControl>();
        public static float cooldown = 30f;
        public static float formationNum = 1f;
        public static bool hasMultipleShotsPerMeeting = false;
        public static bool showInfoInGhostChat = true;
        public static bool canGuessNeutral = false;
        public static bool canGuessImpostor = false;
        public static bool triggerDoomsayerrWin = false;
        public static bool canGuess = true;
        public static bool onlineTarger = false;
        public static float killToWin = 3;
        public static float killedToWin = 0;


        private static Sprite buttonSprite;

        public static Sprite getButtonSprite()
        {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.Observe.png", 115f);
            return buttonSprite;

        }
        public static void clearAndReload()
        {
            doomsayer = null;
            currentTarget = null;
            killedToWin = 0;
            canGuess = true;
            triggerDoomsayerrWin = false;
            playerTargetinformation.Clear();
            cooldown = CustomOptionHolder.doomsayerCooldown.getFloat();
            hasMultipleShotsPerMeeting = CustomOptionHolder.doomsayerHasMultipleShotsPerMeeting.getBool();
            showInfoInGhostChat = CustomOptionHolder.doomsayerShowInfoInGhostChat.getBool();
            canGuessNeutral = CustomOptionHolder.doomsayerCanGuessNeutral.getBool();
            canGuessImpostor = CustomOptionHolder.doomsayerCanGuessImpostor.getBool();
            formationNum = CustomOptionHolder.doomsayerDormationNum.getFloat();
            killToWin = CustomOptionHolder.doomsayerKillToWin.getFloat();
            onlineTarger = CustomOptionHolder.doomsayerOnlineTarger.getBool();

        }
    }

    

        
        public static class BodyGuard {
            public static PlayerControl bodyguard;
            public static PlayerControl guarded = null;
            public static Color color = new Color32(145, 102, 64, byte.MaxValue);
            public static bool reset = true;
            public static bool usedGuard = false;
            private static Sprite guardButtonSprite;
            public static PlayerControl currentTarget;            

            public static void resetGuarded() {
                currentTarget = guarded = null;
                usedGuard = false;
            }


            public static Sprite getGuardButtonSprite() {
                if (guardButtonSprite) return guardButtonSprite;
                guardButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.Shield.png", 115f);
                return guardButtonSprite;
            }

            public static void clearAndReload() {
                bodyguard = null;
                reset = CustomOptionHolder.bodyGuardResetTargetAfterMeeting.getBool();
                guarded = null;
                usedGuard = false;
            }
        }
        
        public static class Portalmaker {
            public static PlayerControl portalmaker;
            public static Color color = new Color32(69, 69, 169, byte.MaxValue);

            public static float cooldown;
            public static float usePortalCooldown;
            public static bool logOnlyHasColors;
            public static bool logShowsTime;
            public static bool canPortalFromAnywhere;

            private static Sprite placePortalButtonSprite;
            private static Sprite usePortalButtonSprite;
            private static Sprite usePortalSpecialButtonSprite1;
            private static Sprite usePortalSpecialButtonSprite2;
            private static Sprite logSprite;

            public static Sprite getPlacePortalButtonSprite() {
                if (placePortalButtonSprite) return placePortalButtonSprite;
                placePortalButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.PlacePortalButton.png", 115f);
                return placePortalButtonSprite;
            }

            public static Sprite getUsePortalButtonSprite() {
                if (usePortalButtonSprite) return usePortalButtonSprite;
                usePortalButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.UsePortalButton.png", 115f);
                return usePortalButtonSprite;
            }

            public static Sprite getUsePortalSpecialButtonSprite(bool first) {
                if (first) {
                    if (usePortalSpecialButtonSprite1) return usePortalSpecialButtonSprite1;
                    usePortalSpecialButtonSprite1 = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.UsePortalSpecialButton1.png", 115f);
                    return usePortalSpecialButtonSprite1;
                } else {
                    if (usePortalSpecialButtonSprite2) return usePortalSpecialButtonSprite2;
                    usePortalSpecialButtonSprite2 = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.UsePortalSpecialButton2.png", 115f);
                    return usePortalSpecialButtonSprite2;
                }
            }

            public static Sprite getLogSprite() {
                if (logSprite) return logSprite;
                logSprite = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.DoorLogsButton].Image;
                return logSprite;
            }

            public static void clearAndReload() {
                portalmaker = null;
                cooldown = CustomOptionHolder.portalmakerCooldown.getFloat();
                usePortalCooldown = CustomOptionHolder.portalmakerUsePortalCooldown.getFloat();
                logOnlyHasColors = CustomOptionHolder.portalmakerLogOnlyColorType.getBool();
                logShowsTime = CustomOptionHolder.portalmakerLogHasTime.getBool();
                canPortalFromAnywhere = CustomOptionHolder.portalmakerCanPortalFromAnywhere.getBool();
            }


        }

        public static class Mayor {
            public static PlayerControl mayor;
            public static Color color = new Color32(32, 77, 66, byte.MaxValue);
            public static Minigame emergency = null;
            public static Sprite emergencySprite = null;
            public static int remoteMeetingsLeft = 1;

            public static bool canSeeVoteColors = false;
            public static int tasksNeededToSeeVoteColors;
            public static bool meetingButton = true;
            public static int mayorChooseSingleVote;

            public static bool voteTwice = true;

            public static Sprite getMeetingSprite()
            {
                if (emergencySprite) return emergencySprite;
                emergencySprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.EmergencyButton.png", 550f);
                return emergencySprite;
            }

            public static void clearAndReload() {
                mayor = null;
                emergency = null;
                emergencySprite = null;
		        remoteMeetingsLeft = Mathf.RoundToInt(CustomOptionHolder.mayorMaxRemoteMeetings.getFloat()); 
                canSeeVoteColors = CustomOptionHolder.mayorCanSeeVoteColors.getBool();
                tasksNeededToSeeVoteColors = (int)CustomOptionHolder.mayorTasksNeededToSeeVoteColors.getFloat();
                meetingButton = CustomOptionHolder.mayorMeetingButton.getBool();
                mayorChooseSingleVote = CustomOptionHolder.mayorChooseSingleVote.getSelection();
                voteTwice = true;
            }
        }

        public static class Engineer {
            public static PlayerControl engineer;
            public static Color color = new Color32(0, 40, 245, byte.MaxValue);
            private static Sprite buttonSprite;

            public static int remainingFixes = 1;           
            public static bool highlightForImpostors = true;
            public static bool highlightForTeamJackal = true; 

            public static Sprite getButtonSprite() {
                if (buttonSprite) return buttonSprite;
                buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.RepairButton.png", 115f);
                return buttonSprite;
            }

            public static void clearAndReload() {
                engineer = null;
                remainingFixes = Mathf.RoundToInt(CustomOptionHolder.engineerNumberOfFixes.getFloat());
                highlightForImpostors = CustomOptionHolder.engineerHighlightForImpostors.getBool();
                highlightForTeamJackal = CustomOptionHolder.engineerHighlightForTeamJackal.getBool();
            }
        }

        public static class Godfather {
            public static PlayerControl godfather;
            public static Color color = Palette.ImpostorRed;

            public static void clearAndReload() {
                godfather = null;
            }
        }

        public static class Mafioso {
            public static PlayerControl mafioso;
            public static Color color = Palette.ImpostorRed;

            public static void clearAndReload() {
                mafioso = null;
            }
        }


        public static class Janitor {
            public static PlayerControl janitor;
            public static Color color = Palette.ImpostorRed;

            public static float cooldown = 30f;

            private static Sprite buttonSprite;
            public static Sprite getButtonSprite() {
                if (buttonSprite) return buttonSprite;
                buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.CleanButton.png", 115f);
                return buttonSprite;
            }

            public static void clearAndReload() {
                janitor = null;
                cooldown = CustomOptionHolder.janitorCooldown.getFloat();
            }
        }

        public static class Sheriff {
            public static PlayerControl sheriff;
            public static Color color = new Color32(248, 205, 70, byte.MaxValue);

            public static float cooldown = 30f;
            public static bool canKillNeutrals = false;
            public static bool spyCanDieToSheriff = false;

            public static PlayerControl currentTarget;

            public static PlayerControl formerDeputy;  // Needed for keeping handcuffs + shifting
            public static PlayerControl formerSheriff;  // When deputy gets promoted...

            public static void replaceCurrentSheriff(PlayerControl deputy)
            {
                if (!formerSheriff) formerSheriff = sheriff;
                sheriff = deputy;
                currentTarget = null;
                cooldown = CustomOptionHolder.sheriffCooldown.getFloat();
            }

            public static void clearAndReload() {
                sheriff = null;
                currentTarget = null;
                formerDeputy = null;
                formerSheriff = null;
                cooldown = CustomOptionHolder.sheriffCooldown.getFloat();
                canKillNeutrals = CustomOptionHolder.sheriffCanKillNeutrals.getBool();
                spyCanDieToSheriff = CustomOptionHolder.spyCanDieToSheriff.getBool();
            }
        }

        public static class Deputy
        {
            public static PlayerControl deputy;
            public static Color color = Sheriff.color;

            public static PlayerControl currentTarget;
            public static List<byte> handcuffedPlayers = new List<byte>();
            public static int promotesToSheriff; // No: 0, Immediately: 1, After Meeting: 2
            public static bool keepsHandcuffsOnPromotion;
            public static float handcuffDuration;
            public static float remainingHandcuffs;
            public static float handcuffCooldown;
            public static bool knowsSheriff;
            public static Dictionary<byte, float> handcuffedKnows = new Dictionary<byte, float>();

            private static Sprite buttonSprite;
            private static Sprite handcuffedSprite;
            
            public static Sprite getButtonSprite()
            {
                if (buttonSprite) return buttonSprite;
                buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.DeputyHandcuffButton.png", 115f);
                return buttonSprite;
            }

            public static Sprite getHandcuffedButtonSprite()
            {
                if (handcuffedSprite) return handcuffedSprite;
                handcuffedSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.DeputyHandcuffed.png", 115f);
                return handcuffedSprite;
            }

            // Can be used to enable / disable the handcuff effect on the target's buttons
            public static void setHandcuffedKnows(bool active = true, byte playerId = Byte.MaxValue)
            {
                if (playerId == Byte.MaxValue)
                    playerId = CachedPlayer.LocalPlayer.PlayerId;

                if (active && playerId == CachedPlayer.LocalPlayer.PlayerId) {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.ShareGhostInfo, Hazel.SendOption.Reliable, -1);
                    writer.Write(CachedPlayer.LocalPlayer.PlayerId);
                    writer.Write((byte)RPCProcedure.GhostInfoTypes.HandcuffNoticed);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                }

                if (active) {
                    handcuffedKnows.Add(playerId, handcuffDuration);
                    handcuffedPlayers.RemoveAll(x => x == playerId);
               }

                if (playerId == CachedPlayer.LocalPlayer.PlayerId) {
                    HudManagerStartPatch.setAllButtonsHandcuffedStatus(active);
                    SoundEffectsManager.play("deputyHandcuff");
		}
 
	    }

            public static void clearAndReload()
            {
                deputy = null;
                currentTarget = null;
                handcuffedPlayers = new List<byte>();
                handcuffedKnows = new Dictionary<byte, float>();
                HudManagerStartPatch.setAllButtonsHandcuffedStatus(false, true);
                promotesToSheriff = CustomOptionHolder.deputyGetsPromoted.getSelection();
                remainingHandcuffs = CustomOptionHolder.deputyNumberOfHandcuffs.getFloat();
                handcuffCooldown = CustomOptionHolder.deputyHandcuffCooldown.getFloat();
                keepsHandcuffsOnPromotion = CustomOptionHolder.deputyKeepsHandcuffs.getBool();
                handcuffDuration = CustomOptionHolder.deputyHandcuffDuration.getFloat();
                knowsSheriff = CustomOptionHolder.deputyKnowsSheriff.getBool();
            }
        }

        public static class Lighter {
            public static PlayerControl lighter;
            public static Color color = new Color32(238, 229, 190, byte.MaxValue);
            
            public static float lighterModeLightsOnVision = 2f;
            public static float lighterModeLightsOffVision = 0.75f;
            public static float flashlightWidth = 0.75f;

            public static void clearAndReload() {
                lighter = null;
                flashlightWidth = CustomOptionHolder.lighterFlashlightWidth.getFloat();
                lighterModeLightsOnVision = CustomOptionHolder.lighterModeLightsOnVision.getFloat();
                lighterModeLightsOffVision = CustomOptionHolder.lighterModeLightsOffVision.getFloat();
            }
        }

        public static class Detective {
            public static PlayerControl detective;
            public static Color color = new Color32(45, 106, 165, byte.MaxValue);

            public static float footprintIntervall = 1f;
            public static float footprintDuration = 1f;
            public static bool anonymousFootprints = false;
            public static float reportNameDuration = 0f;
            public static float reportColorDuration = 20f;
            public static float timer = 6.2f;

            public static void clearAndReload() {
                detective = null;
                anonymousFootprints = CustomOptionHolder.detectiveAnonymousFootprints.getBool();
                footprintIntervall = CustomOptionHolder.detectiveFootprintIntervall.getFloat();
                footprintDuration = CustomOptionHolder.detectiveFootprintDuration.getFloat();
                reportNameDuration = CustomOptionHolder.detectiveReportNameDuration.getFloat();
                reportColorDuration = CustomOptionHolder.detectiveReportColorDuration.getFloat();
                timer = 6.2f;
            }
        }
    }

    public static class TimeMaster {
        public static PlayerControl timeMaster;
        public static Color color = new Color32(112, 142, 239, byte.MaxValue);

        public static bool reviveDuringRewind = false;
        public static float rewindTime = 3f;
        public static float shieldDuration = 3f;
        public static float cooldown = 30f;

        public static bool shieldActive = false;
        public static bool isRewinding = false;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.TimeShieldButton.png", 115f);
            return buttonSprite;
        }

        public static void clearAndReload() {
            timeMaster = null;
            isRewinding = false;
            shieldActive = false;
            rewindTime = CustomOptionHolder.timeMasterRewindTime.getFloat();
            shieldDuration = CustomOptionHolder.timeMasterShieldDuration.getFloat();
            cooldown = CustomOptionHolder.timeMasterCooldown.getFloat();
        }
    }

    public static class Amnisiac {
        public static PlayerControl amnisiac;
        public static List<Arrow> localArrows = new List<Arrow>();
        public static Color color = new Color(0.5f, 0.7f, 1f, 1f);
        public static List<PoolablePlayer> poolIcons = new List<PoolablePlayer>();

        public static bool showArrows = true;
        public static bool resetRole = false;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.Remember.png", 115f);
            return buttonSprite;
        }

        public static void clearAndReload() {
            amnisiac = null;
            showArrows = CustomOptionHolder.amnisiacShowArrows.getBool();
            resetRole = CustomOptionHolder.amnisiacResetRole.getBool();
            if (localArrows != null) {
                foreach (Arrow arrow in localArrows)
                    if (arrow?.arrow != null)
                        UnityEngine.Object.Destroy(arrow.arrow);
            }
            localArrows = new List<Arrow>();
        }
    }

    public static class Veteren {
        public static PlayerControl veteren;
        public static Color color = new Color32(255, 77, 0, byte.MaxValue);

        public static float alertDuration = 3f;
        public static float cooldown = 30f;

        public static bool alertActive = false;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.Alert.png", 115f);
            return buttonSprite;
        }

        public static void clearAndReload() {
            veteren = null;
            alertActive = false;
            alertDuration = CustomOptionHolder.veterenAlertDuration.getFloat();
            cooldown = CustomOptionHolder.veterenCooldown.getFloat();
        }
    }

     public static class Teleporter
    {
        public static PlayerControl teleporter;
        public static Color color = new Color32(164, 249, 255, byte.MaxValue);
        private static Sprite teleportButtonSprite;
        private static Sprite sampleSprite;
        public static float teleportCooldown = 30f;
        public static float sampleCooldown = 30f;
        public static int teleportNumber = 5;
        public static PlayerControl target1;
        public static PlayerControl target2;
        public static PlayerControl currentTarget;

        public static Sprite getButtonSprite()
        {
            if (teleportButtonSprite) return teleportButtonSprite;
            teleportButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.TeleporterButton.png", 115f);
            return teleportButtonSprite;
        }

        public static Sprite getTeleporterSampleSprite()
        {
            if (sampleSprite) return sampleSprite;
            sampleSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.SampleButton.png", 115f);
            return sampleSprite;
        }

        public static void clearAndReload()
        {
            teleporter = null;
            target1 = null;
            target2 = null;
            currentTarget = null;
            teleportCooldown = CustomOptionHolder.teleporterCooldown.getFloat();
            teleportNumber = (int)CustomOptionHolder.teleporterTeleportNumber.getFloat();
            sampleCooldown = CustomOptionHolder.teleporterSampleCooldown.getFloat();
        }
    }

    public static class Transporter {
    
        public static PlayerControl transporter;
        public static Color color = new Color32(23, 86, 92, byte.MaxValue);
        private static Sprite sampleSprite;
        private static Sprite morphSprite;

        public static float cooldown = 15f;
        public static float delaiAfterScan = 15f;

        public static bool haveArrow = true;
        public static float arrowUpdateInterval = 1f;
        public static Arrow localArrow = new Arrow(Color.blue);
        public static float timeUntilUpdate = 0f;

        public static PlayerControl currentTarget;
        public static PlayerControl sampledTarget;

        public static void TransportPlayers(PlayerControl TP2)
        {
            var TP1 = Transporter.transporter;
            var deadBodies = UnityEngine.Object.FindObjectsOfType<DeadBody>();
            DeadBody Player1Body = null;
            DeadBody Player2Body = null;
            if (TP1.Data.IsDead)
                foreach (var body in deadBodies)
                    if (body.ParentId == TP1.PlayerId)
                        Player1Body = body;
            if (TP2.Data.IsDead)
                foreach (var body in deadBodies)
                    if (body.ParentId == TP2.PlayerId)
                        Player2Body = body;

            if (TP1.inVent && PlayerControl.LocalPlayer.PlayerId == TP1.PlayerId)
            {
                TP1.MyPhysics.ExitAllVents();
            }
            if (TP2.inVent && PlayerControl.LocalPlayer.PlayerId == TP2.PlayerId)
            {
                TP2.MyPhysics.ExitAllVents();
            }

            if (Player1Body == null && Player2Body == null)
            {
                TP1.MyPhysics.ResetMoveState();
                TP2.MyPhysics.ResetMoveState();
                var TempPosition = TP1.GetTruePosition();
                //       var TempFacing = TP1.myRend.flipX;
                TP1.NetTransform.SnapTo(new Vector2(TP2.GetTruePosition().x, TP2.GetTruePosition().y + 0.3636f));
                //       TP1.myRend.flipX = TP2.myRend.flipX;
                TP2.NetTransform.SnapTo(new Vector2(TempPosition.x, TempPosition.y + 0.3636f));
                //        TP2.myRend.flipX = TempFacing;
            }

            TP1.moveable = true;
            TP2.moveable = true;
            TP1.Collider.enabled = true;
            TP2.Collider.enabled = true;
            TP1.NetTransform.enabled = true;
            TP2.NetTransform.enabled = true;
        }

        public static void clearAndReload()
        {
            transporter = null;
            currentTarget = null;
            sampledTarget = null;
            timeUntilUpdate = 0f;
            cooldown = CustomOptionHolder.transporterScanCooldown.getFloat();
            delaiAfterScan = CustomOptionHolder.transporterDelaiAfterScan.getFloat();
            haveArrow = CustomOptionHolder.transporterAddArrow.getBool();
            arrowUpdateInterval = CustomOptionHolder.transporterUpdateIntervall.getFloat();
            if (localArrow?.arrow != null) UnityEngine.Object.Destroy(localArrow.arrow);
            localArrow = new Arrow(Color.blue);
            if (localArrow.arrow != null) localArrow.arrow.SetActive(false);
        }

        public static Sprite getTransporterSampleSprite()
        {
            if (sampleSprite) return sampleSprite;
            sampleSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.SampleButton.png", 115f);
            return sampleSprite;
        }

        public static Sprite getTransporterMorphSprite()
        {
            if (morphSprite) return morphSprite;
            morphSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.TransporterButton.png", 115f);
            return morphSprite;
        }
    }

    public static class Transportist {
    
        public static PlayerControl transportist;
        public static Color color = Palette.ImpostorRed;
        private static Sprite sampleSprite;
        private static Sprite morphSprite;

        public static float cooldown = 15f;
        public static float delaiAfterScan = 15f;

        public static bool haveArrow = true;
        public static float arrowUpdateInterval = 1f;
        public static Arrow localArrow = new Arrow(Color.blue);
        public static float timeUntilUpdate = 0f;

        public static PlayerControl currentTarget;
        public static PlayerControl sampledTarget;

        public static void TransportistPlayers(PlayerControl TP2)
        {
            var TP1 = Transportist.transportist;
            var deadBodies = UnityEngine.Object.FindObjectsOfType<DeadBody>();
            DeadBody Player1Body = null;
            DeadBody Player2Body = null;
            if (TP1.Data.IsDead)
                foreach (var body in deadBodies)
                    if (body.ParentId == TP1.PlayerId)
                        Player1Body = body;
            if (TP2.Data.IsDead)
                foreach (var body in deadBodies)
                    if (body.ParentId == TP2.PlayerId)
                        Player2Body = body;

            if (TP1.inVent && PlayerControl.LocalPlayer.PlayerId == TP1.PlayerId)
            {
                TP1.MyPhysics.ExitAllVents();
            }
            if (TP2.inVent && PlayerControl.LocalPlayer.PlayerId == TP2.PlayerId)
            {
                TP2.MyPhysics.ExitAllVents();
            }

            if (Player1Body == null && Player2Body == null)
            {
                TP1.MyPhysics.ResetMoveState();
                TP2.MyPhysics.ResetMoveState();
                var TempPosition = TP1.GetTruePosition();
                //       var TempFacing = TP1.myRend.flipX;
                //TP1.NetTransform.SnapTo(new Vector2(TP2.GetTruePosition().x, TP2.GetTruePosition().y + 0.3636f));
                //       TP1.myRend.flipX = TP2.myRend.flipX;
                TP2.NetTransform.SnapTo(new Vector2(TempPosition.x, TempPosition.y + 0.3636f));
                //        TP2.myRend.flipX = TempFacing;
            }

            TP1.moveable = true;
            TP2.moveable = true;
            TP1.Collider.enabled = true;
            TP2.Collider.enabled = true;
            TP1.NetTransform.enabled = true;
            TP2.NetTransform.enabled = true;
        }

        public static void clearAndReload()
        {
            transportist = null;
            currentTarget = null;
            sampledTarget = null;
            timeUntilUpdate = 0f;
            cooldown = CustomOptionHolder.transportistScanCooldown.getFloat();
            delaiAfterScan = CustomOptionHolder.transportistDelaiAfterScan.getFloat();
            haveArrow = CustomOptionHolder.transportistAddArrow.getBool();
            arrowUpdateInterval = CustomOptionHolder.transportistUpdateIntervall.getFloat();
            if (localArrow?.arrow != null) UnityEngine.Object.Destroy(localArrow.arrow);
            localArrow = new Arrow(Color.blue);
            if (localArrow.arrow != null) localArrow.arrow.SetActive(false);
        }

        public static Sprite getTransportistSampleSprite()
        {
            if (sampleSprite) return sampleSprite;
            sampleSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.SampleButton.png", 115f);
            return sampleSprite;
        }

        public static Sprite getTransportistMorphSprite()
        {
            if (morphSprite) return morphSprite;
            morphSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.TransportistButton.png", 115f);
            return morphSprite;
        }
    }


    public static class Arbiter
    {
        public static PlayerControl arbiter;
        public static Color color = new Color32(179, 128, 0, byte.MaxValue);
        public static byte specialVoteTargetPlayerId = byte.MaxValue;
        private static int _remainingSpecialVotes = 1;
        private static Sprite targetSprite;

        public static void clearAndReload()
        {
            arbiter = null;
            _remainingSpecialVotes = Mathf.RoundToInt(CustomOptionHolder.arbiterNumberOfSpecialVotes.getFloat());
            specialVoteTargetPlayerId = byte.MaxValue;
        }

        public static Sprite getTargetSprite()
        {
            if (targetSprite) return targetSprite;
            targetSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.ArbiterTargetIcon.png", 150f);
            return targetSprite;
        }

        public static int remainingSpecialVotes(bool isVote = false)
        {
            if (arbiter == null)
                return 0;

            if (isVote)
                _remainingSpecialVotes = Mathf.Max(0, _remainingSpecialVotes - 1);
            return _remainingSpecialVotes;
        }

        public static bool isArbiter(byte playerId)
        {
            return arbiter != null && arbiter.PlayerId == playerId;
        }
    }


    public static class Medic {
        public static PlayerControl medic;
        public static PlayerControl shielded;
        public static PlayerControl futureShielded;
        
        public static Color color = new Color32(126, 251, 194, byte.MaxValue);
        public static bool usedShield;

        public static int showShielded = 0;
        public static bool showAttemptToShielded = false;
        public static bool showAttemptToMedic = false;
        public static bool setShieldAfterMeeting = false;
        public static bool showShieldAfterMeeting = false;
        public static bool meetingAfterShielding = false;

        public static Color shieldedColor = new Color32(0, 221, 255, byte.MaxValue);
        public static PlayerControl currentTarget;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.ShieldButton.png", 115f);
            return buttonSprite;
        }

        public static bool shieldVisible(PlayerControl target) {
            bool hasVisibleShield = false;

            bool isMorphedMorphling = target == Morphling.morphling && Morphling.morphTarget != null && Morphling.morphTimer > 0f;
            if (Medic.shielded != null && ((target == Medic.shielded && !isMorphedMorphling) || (isMorphedMorphling && Morphling.morphTarget == Medic.shielded))) {
                hasVisibleShield = Medic.showShielded == 0 || Helpers.shouldShowGhostInfo() // Everyone or Ghost info
                    || (Medic.showShielded == 1 && (CachedPlayer.LocalPlayer.PlayerControl == Medic.shielded || CachedPlayer.LocalPlayer.PlayerControl == Medic.medic)) // Shielded + Medic
                    || (Medic.showShielded == 2 && CachedPlayer.LocalPlayer.PlayerControl == Medic.medic); // Medic only
                // Make shield invisible till after the next meeting if the option is set (the medic can already see the shield)
                hasVisibleShield = hasVisibleShield && (Medic.meetingAfterShielding || !Medic.showShieldAfterMeeting || CachedPlayer.LocalPlayer.PlayerControl == Medic.medic || Helpers.shouldShowGhostInfo());
            }
            return hasVisibleShield;            
        }

        public static void clearAndReload() {
            medic = null;
            shielded = null;
            futureShielded = null;
            currentTarget = null;
            usedShield = false;
            showShielded = CustomOptionHolder.medicShowShielded.getSelection();
            showAttemptToShielded = CustomOptionHolder.medicShowAttemptToShielded.getBool();
            showAttemptToMedic = CustomOptionHolder.medicShowAttemptToMedic.getBool();
            setShieldAfterMeeting = CustomOptionHolder.medicSetOrShowShieldAfterMeeting.getSelection() == 2;
            showShieldAfterMeeting = CustomOptionHolder.medicSetOrShowShieldAfterMeeting.getSelection() == 1;
            meetingAfterShielding = false;
        }
    }

    public static class Swapper {
        public static PlayerControl swapper;
        public static Color color = new Color32(134, 55, 86, byte.MaxValue);
        private static Sprite spriteCheck;
        public static bool canCallEmergency = false;
        public static bool canOnlySwapOthers = false;
        public static int charges;
        public static float rechargeTasksNumber;
        public static float rechargedTasks;
 
        public static byte playerId1 = Byte.MaxValue;
        public static byte playerId2 = Byte.MaxValue;

        public static Sprite getCheckSprite() {
            if (spriteCheck) return spriteCheck;
            spriteCheck = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.SwapperCheck.png", 150f);
            return spriteCheck;
        }

        public static void clearAndReload() {
            swapper = null;
            playerId1 = Byte.MaxValue;
            playerId2 = Byte.MaxValue;
            canCallEmergency = CustomOptionHolder.swapperCanCallEmergency.getBool();
            canOnlySwapOthers = CustomOptionHolder.swapperCanOnlySwapOthers.getBool();
            charges = Mathf.RoundToInt(CustomOptionHolder.swapperSwapsNumber.getFloat());
            rechargeTasksNumber = Mathf.RoundToInt(CustomOptionHolder.swapperRechargeTasksNumber.getFloat());
            rechargedTasks = Mathf.RoundToInt(CustomOptionHolder.swapperRechargeTasksNumber.getFloat());
        }
    }

    public static class Lovers {
        public static PlayerControl lover1;
        public static PlayerControl lover2;
        public static Color color = new Color32(232, 57, 185, byte.MaxValue);

        public static bool bothDie = true;
        public static bool enableChat = true;
        // Lovers save if next to be exiled is a lover, because RPC of ending game comes before RPC of exiled
        public static bool notAckedExiledIsLover = false;

        public static bool existing() {
            return lover1 != null && lover2 != null && !lover1.Data.Disconnected && !lover2.Data.Disconnected;
        }

        public static bool existingAndAlive() {
            return existing() && !lover1.Data.IsDead && !lover2.Data.IsDead && !notAckedExiledIsLover; // ADD NOT ACKED IS LOVER
        }

        public static PlayerControl otherLover(PlayerControl oneLover) {
            if (!existingAndAlive()) return null;
            if (oneLover == lover1) return lover2;
            if (oneLover == lover2) return lover1;
            return null;
        }

        public static bool existingWithKiller() {
            return existing() && (lover1 == Jackal.jackal     || lover2 == Jackal.jackal
                               || lover1 == Sidekick.sidekick || lover2 == Sidekick.sidekick
                               || lover1 == Swooper.swooper || lover2 == Swooper.swooper
                               || lover1 == Werewolf.werewolf || lover2 == Werewolf.werewolf
                               || lover1 == Juggernaut.juggernaut || lover2 == Juggernaut.juggernaut
                               || lover1.Data.Role.IsImpostor      || lover2.Data.Role.IsImpostor);
        }

        public static bool hasAliveKillingLover(this PlayerControl player) {
            if (!Lovers.existingAndAlive() || !existingWithKiller())
                return false;
            return (player != null && (player == lover1 || player == lover2));
        }

        public static void clearAndReload() {
            lover1 = null;
            lover2 = null;
            notAckedExiledIsLover = false;
            bothDie = CustomOptionHolder.modifierLoverBothDie.getBool();
            enableChat = CustomOptionHolder.modifierLoverEnableChat.getBool();
        }

        public static PlayerControl getPartner(this PlayerControl player) {
            if (player == null)
                return null;
            if (lover1 == player)
                return lover2;
            if (lover2 == player)
                return lover1;
            return null;
        }
    }

    public static class Seer {
        public static PlayerControl seer;
        public static Color color = new Color32(97, 178, 108, byte.MaxValue);
        public static List<Vector3> deadBodyPositions = new List<Vector3>();

        public static float soulDuration = 15f;
        public static bool limitSoulDuration = false;
        public static int mode = 0;

        private static Sprite soulSprite;
        public static Sprite getSoulSprite() {
            if (soulSprite) return soulSprite;
            soulSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.Soul.png", 500f);
            return soulSprite;
        }

        public static void clearAndReload() {
            seer = null;
            deadBodyPositions = new List<Vector3>();
            limitSoulDuration = CustomOptionHolder.seerLimitSoulDuration.getBool();
            soulDuration = CustomOptionHolder.seerSoulDuration.getFloat();
            mode = CustomOptionHolder.seerMode.getSelection();
        }
    }

    public static class Morphling {
        public static PlayerControl morphling;
        public static Color color = Palette.ImpostorRed;
        private static Sprite sampleSprite;
        private static Sprite morphSprite;
    
        public static float cooldown = 30f;
        public static float duration = 10f;

        public static PlayerControl currentTarget;
        public static PlayerControl sampledTarget;
        public static PlayerControl morphTarget;
        public static float morphTimer = 0f;

        public static void resetMorph() {
            morphTarget = null;
            morphTimer = 0f;
            if (morphling == null) return;
            morphling.setDefaultLook();
        }

        public static void clearAndReload() {
            resetMorph();
            morphling = null;
            currentTarget = null;
            sampledTarget = null;
            morphTarget = null;
            morphTimer = 0f;
            cooldown = CustomOptionHolder.morphlingCooldown.getFloat();
            duration = CustomOptionHolder.morphlingDuration.getFloat();
        }

        public static Sprite getSampleSprite() {
            if (sampleSprite) return sampleSprite;
            sampleSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.SampleButton.png", 115f);
            return sampleSprite;
        }

        public static Sprite getMorphSprite() {
            if (morphSprite) return morphSprite;
            morphSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.MorphButton.png", 115f);
            return morphSprite;
        }
    }

    public static class Camouflager {
        public static PlayerControl camouflager;
        public static Color color = Palette.ImpostorRed;
    
        public static float cooldown = 30f;
        public static float duration = 10f;
        public static float camouflageTimer = 0f;
        public static bool camoComms = false;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.CamoButton.png", 115f);
            return buttonSprite;
        }

        public static void resetCamouflage() {
            if (Helpers.isCamoComms()) return;
			    camoComms = false;
            camouflageTimer = 0f;
            foreach (PlayerControl p in CachedPlayer.AllPlayers) {
                if (p == Ninja.ninja && Ninja.isInvisble)
                    continue;
                p.setDefaultLook();
            }
        }

        public static void clearAndReload() {
            resetCamouflage();
            camouflager = null;
            camouflageTimer = 0f;
            cooldown = CustomOptionHolder.camouflagerCooldown.getFloat();
            duration = CustomOptionHolder.camouflagerDuration.getFloat();
        }
    }

    public static class Hacker {
        public static PlayerControl hacker;
        public static Minigame vitals = null;
        public static Minigame doorLog = null;
        public static Color color = new Color32(117, 250, 76, byte.MaxValue);

        public static float cooldown = 30f;
        public static float duration = 10f;
        public static float toolsNumber = 5f;
        public static bool onlyColorType = false;
        public static float hackerTimer = 0f;
        public static int rechargeTasksNumber = 2;
        public static int rechargedTasks = 2;
        public static int chargesVitals = 1;
        public static int chargesAdminTable = 1;
        public static bool cantMove = true;

        private static Sprite buttonSprite;
        private static Sprite vitalsSprite;
        private static Sprite logSprite;
        private static Sprite adminSprite;

        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.HackerButton.png", 115f);
            return buttonSprite;
        }

        public static Sprite getVitalsSprite() {
            if (vitalsSprite) return vitalsSprite;
            vitalsSprite = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.VitalsButton].Image;
            return vitalsSprite;
        }

        public static Sprite getLogSprite() {
            if (logSprite) return logSprite;
            logSprite = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.DoorLogsButton].Image;
            return logSprite;
        }

        public static Sprite getAdminSprite() {
            byte mapId = GameOptionsManager.Instance.currentNormalGameOptions.MapId;
            UseButtonSettings button = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.PolusAdminButton]; // Polus
            if (Helpers.isSkeld() || mapId == 3) button = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.AdminMapButton]; // Skeld || Dleks
            else if (Helpers.isMira()) button = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.MIRAAdminButton]; // Mira HQ
            else if (Helpers.isAirship()) button = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.AirshipAdminButton]; // Airship
            else if (Helpers.isFungle()) button = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.AdminMapButton];  // Hacker can Access the Admin panel on Fungle
            adminSprite = button.Image;
            return adminSprite;
        }

        public static void clearAndReload() {
            hacker = null;
            vitals = null;
            doorLog = null;
            hackerTimer = 0f;
            adminSprite = null;
            cooldown = CustomOptionHolder.hackerCooldown.getFloat();
            duration = CustomOptionHolder.hackerHackeringDuration.getFloat();
            onlyColorType = CustomOptionHolder.hackerOnlyColorType.getBool();
            toolsNumber = CustomOptionHolder.hackerToolsNumber.getFloat();
            rechargeTasksNumber = Mathf.RoundToInt(CustomOptionHolder.hackerRechargeTasksNumber.getFloat());
            rechargedTasks = Mathf.RoundToInt(CustomOptionHolder.hackerRechargeTasksNumber.getFloat());
            chargesVitals = Mathf.RoundToInt(CustomOptionHolder.hackerToolsNumber.getFloat()) / 2;
            chargesAdminTable = Mathf.RoundToInt(CustomOptionHolder.hackerToolsNumber.getFloat()) / 2;
            cantMove = CustomOptionHolder.hackerNoMove.getBool();
        }
    }

    public static class Tracker {
        public static PlayerControl tracker;
        public static Color color = new Color32(100, 58, 220, byte.MaxValue);
        public static List<Arrow> localArrows = new();

        public static float updateIntervall = 5f;
        public static bool resetTargetAfterMeeting = false;
        public static bool canTrackCorpses = false;
        public static float corpsesTrackingCooldown = 30f;
        public static float corpsesTrackingDuration = 5f;
        public static float corpsesTrackingTimer = 0f;
        public static int trackingMode = 0;
        public static List<Vector3> deadBodyPositions = new();

        public static PlayerControl currentTarget;
        public static PlayerControl tracked;
        public static bool usedTracker = false;
        public static float timeUntilUpdate = 0f;
        public static Arrow arrow = new(Color.blue);

        public static GameObject DangerMeterParent;
        public static DangerMeter Meter;

        private static Sprite trackCorpsesButtonSprite;
        public static Sprite getTrackCorpsesButtonSprite()
        {
            if (trackCorpsesButtonSprite) return trackCorpsesButtonSprite;
            trackCorpsesButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.PathfindButton.png", 115f);
            return trackCorpsesButtonSprite;
        }

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.TrackerButton.png", 115f);
            return buttonSprite;
        }

        public static void resetTracked() {
            currentTarget = tracked = null;
            usedTracker = false;
            if (arrow?.arrow != null) UnityEngine.Object.Destroy(arrow.arrow);
            arrow = new Arrow(Color.blue);
            if (arrow.arrow != null) arrow.arrow.SetActive(false);
        }

        public static void clearAndReload() {
            tracker = null;
            resetTracked();
            timeUntilUpdate = 0f;
            updateIntervall = CustomOptionHolder.trackerUpdateIntervall.getFloat();
            resetTargetAfterMeeting = CustomOptionHolder.trackerResetTargetAfterMeeting.getBool();
            if (localArrows != null) {
                foreach (Arrow arrow in localArrows)
                    if (arrow?.arrow != null)
                        UnityEngine.Object.Destroy(arrow.arrow);
            }
            deadBodyPositions = new List<Vector3>();
            corpsesTrackingTimer = 0f;
            corpsesTrackingCooldown = CustomOptionHolder.trackerCorpsesTrackingCooldown.getFloat();
            corpsesTrackingDuration = CustomOptionHolder.trackerCorpsesTrackingDuration.getFloat();
            canTrackCorpses = CustomOptionHolder.trackerCanTrackCorpses.getBool();
            trackingMode = CustomOptionHolder.trackerTrackingMethod.getSelection();
            if (DangerMeterParent) {
                Meter.gameObject.Destroy();
                DangerMeterParent.Destroy();
            }
        }
    }

    public static class Vampire {
        public static PlayerControl vampire;
        public static Color color = Palette.ImpostorRed;

        public static float delay = 10f;
        public static float cooldown = 30f;
        public static bool canKillNearGarlics = true;
        public static bool localPlacedGarlic = false;
        public static bool garlicsActive = true;
        public static bool garlicButton = false;

        public static PlayerControl currentTarget;
        public static PlayerControl bitten; 
        public static bool targetNearGarlic = false;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.VampireButton.png", 115f);
            return buttonSprite;
        }

        private static Sprite garlicButtonSprite;
        public static Sprite getGarlicButtonSprite() {
            if (garlicButtonSprite) return garlicButtonSprite;
            garlicButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.GarlicButton.png", 115f);
            return garlicButtonSprite;
        }

        public static void clearAndReload() {
            vampire = null;
            bitten = null;
            targetNearGarlic = false;
            localPlacedGarlic = false;
            currentTarget = null;
            garlicsActive = CustomOptionHolder.vampireSpawnRate.getSelection() > 0;
            delay = CustomOptionHolder.vampireKillDelay.getFloat();
            cooldown = CustomOptionHolder.vampireCooldown.getFloat();
            canKillNearGarlics = CustomOptionHolder.vampireCanKillNearGarlics.getBool();
            garlicButton = CustomOptionHolder.vampireGarlicButton.getBool();
        }
    }

    public static class Snitch {
        public static PlayerControl snitch;
        public static Color color = new Color32(184, 251, 79, byte.MaxValue);
        public enum Mode {
            Chat = 0,
            Map = 1,
            ChatAndMap = 2
        }
        public enum Targets {
            EvilPlayers = 0,
            Killers = 1
        }

        public static Mode mode = Mode.Chat;
        public static Targets targets = Targets.EvilPlayers;
        public static int taskCountForReveal = 1;
		public static bool seeInMeeting = false;


        public static bool isRevealed = false;
        public static Dictionary<byte, byte> playerRoomMap = new Dictionary<byte, byte>();
        public static TMPro.TextMeshPro text = null;
        public static bool needsUpdate = true;

        public static void clearAndReload() {
            taskCountForReveal = Mathf.RoundToInt(CustomOptionHolder.snitchLeftTasksForReveal.getFloat());
            snitch = null;
            isRevealed = false;
            playerRoomMap = new Dictionary<byte, byte>();
            if (text != null) UnityEngine.Object.Destroy(text);
            text = null;
            needsUpdate = true;
            mode = (Mode) CustomOptionHolder.snitchMode.getSelection();
            targets = (Targets) CustomOptionHolder.snitchTargets.getSelection();
            seeInMeeting = CustomOptionHolder.snitchSeeMeeting.getBool();
        }
    }

	 public static class Swooper {
        public static PlayerControl swooper;
        public static PlayerControl currentTarget;
        public static float cooldown = 30f;
        public static bool isInvisable = false;
        public static Color color = new Color32(224, 197, 219, byte.MaxValue);
        public static float duration = 5f;
        public static float swoopCooldown = 30f;
        public static float swoopTimer = 0f;
        public static Sprite buttonSprite;
        public static bool hasImpVision = false;
       public static bool swooperAsWell = false; 

        public static Sprite getSwoopButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.Swoop.png", 115f);
            return buttonSprite;
        }
      
        public static Vector3 getSwooperSwoopVector() {
            return CustomButton.ButtonPositions.upperRowLeft; //brb
        }

        public static void clearAndReload() {
          swooper = null;
          isInvisable = false;
          cooldown = CustomOptionHolder.jackalKillCooldown.getFloat();
          swoopCooldown = CustomOptionHolder.swooperCooldown.getFloat();
          duration = CustomOptionHolder.swooperDuration.getFloat();
          hasImpVision = CustomOptionHolder.swooperHasImpVision.getBool();

        }
    }
    
    public static class Mimic
    {
        public static PlayerControl mimic;
		public static bool hasMimic = false;
        public static Color color = Palette.ImpostorRed;
        public static List<PlayerControl> killed = new List<PlayerControl>();



        public static void clearAndReload(bool clearList = true)
        {
            mimic = null;
			if (clearList) hasMimic = false;

        }
    }

     public class Miner {
        public readonly static List<Vent> Vents = new List<Vent>();
        public static PlayerControl miner;
        public KillButton _mineButton;
        public static DateTime LastMined;
        public static Sprite buttonSprite;

        public static float cooldown = 30f;
        public static float duration = 0f;
        public static Color color = Palette.ImpostorRed;

        public bool CanPlace { get; set; }
        public static Vector2 VentSize { get; set; }

        public static void clearAndReload() {
            miner = null;
            cooldown = CustomOptionHolder.minerCooldown.getFloat();
        }
        
        public static Sprite getMineButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.Mine.png", 115f);
            return buttonSprite;
        }
    }
    
    public static class Pitfaller {
        public static PlayerControl pitfaller;
        public static PlayerControl pitfallbody;
        public static Color color = Palette.ImpostorRed;

        public static float cooldown = 30f;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite()
        {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.PitfallTarget.png", 115f);
            return buttonSprite;
        }

        public static void clearAndReload()
        {
            pitfaller = null;
            pitfallbody = null;
            cooldown = CustomOptionHolder.pitfallerCooldown.getFloat();
        }
    }


    public static class Shinobi {
        public static PlayerControl shinobi;
        public static Color color = Palette.ImpostorRed;
        public static float cooldown = 30f;
        public static float duration = 10f;
        public static float shinobiTimer = 0f;
        public static int killDistance = 0;
        public static bool moveToPlayer = true;

        private static Sprite shinobiSprite;
        public static Sprite getButtonSprite() {
            if(shinobiSprite) return shinobiSprite;
            shinobiSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.ShinobiButton.png", 115f);
            return shinobiSprite;
        }

        public static void resetShinobi() {
            shinobiTimer = 0f;
            if(shinobi == null) return;
            GameOptionsManager.Instance.currentNormalGameOptions.KillDistance = 2;
            moveToPlayer = true;
        }

        // Call in TheOtherRoles.cs
        public static void clearAndReload() {
            resetShinobi();
            shinobi = null;
            shinobiTimer = 0f;
            cooldown = CustomOptionHolder.shinobiCooldown.getFloat();
            duration = CustomOptionHolder.shinobiDuration.getFloat();
            killDistance = Convert.ToInt32(CustomOptionHolder.shinobiKillDistance.getFloat()); 
        }

        // Call in UpdatePatch.cs
        public static void shinobiAction() {
            float oldShinobiTimer = Shinobi.shinobiTimer;

            Shinobi.shinobiTimer -= Time.deltaTime;

            // Set Shinobi effect
            if(Shinobi.shinobiTimer > 0f) {
                GameOptionsManager.Instance.currentNormalGameOptions.KillDistance = killDistance;
                moveToPlayer = false;
            }

            if(oldShinobiTimer > 0f && Shinobi.shinobiTimer <= 0f) {
                Shinobi.resetShinobi();
            }
        }

        // Call in RPC.cs
        public static void shinobiExpand() {
            if(Shinobi.shinobi == null) return;
            Shinobi.shinobiTimer = Shinobi.duration;
        }
    }



        public static class Escapist {
        public static PlayerControl escapist;
        public static Color color = Palette.ImpostorRed;

        public static float escapistEscapeTime = 30f;
        public static float escapistChargesOnPlace = 1f;
        public static bool resetPlaceAfterMeeting = false;
    //    public static float jumperChargesGainOnMeeting = 2f;
        public static float escapistMaxCharges = 3f;
        public static float escapistCharges = 0f;

        public static Vector3 escapeLocation;

        private static Sprite escapeMarkButtonSprite;
        private static Sprite escapeButtonSprite;
        public static bool usedPlace = false;

        public static Sprite getEscapeMarkButtonSprite() {
            if (escapeMarkButtonSprite) return escapeMarkButtonSprite;
            escapeMarkButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.JumperButton.png", 115f);
            return escapeMarkButtonSprite;
        }

        public static Sprite getEscapeButtonSprite() {
            if (escapeButtonSprite) return escapeButtonSprite;
            escapeButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.JumperJumpButton.png", 115f);
            return escapeButtonSprite;
        }

        public static void resetPlaces() {
            escapistCharges = Mathf.RoundToInt(CustomOptionHolder.escapistChargesOnPlace.getFloat());
            escapeLocation = Vector3.zero;
            usedPlace = false;
        }

        public static void clearAndReload() {
            resetPlaces();
            escapeLocation = Vector3.zero;
            escapist = null;
            resetPlaceAfterMeeting = true;
            escapistCharges = 0f;
            escapistEscapeTime = CustomOptionHolder.escapistEscapeTime.getFloat();
            escapistChargesOnPlace = CustomOptionHolder.escapistChargesOnPlace.getFloat();
      //      jumperChargesGainOnMeeting = CustomOptionHolder.jumperChargesGainOnMeeting.getFloat();
            escapistMaxCharges = CustomOptionHolder.escapistMaxCharges.getFloat();
            usedPlace = false;
        }
    }


     public static class Snarer
    {
        public static PlayerControl snarer;
        public static Color color = Palette.ImpostorRed;

        public static float minDistance = 0f;
        public static float maxDistance;
        public static int numSnare;
        public static float extensionTime;
        public static float killTimer;
        public static float cooldown;
        public static float snareRange;
        public static float penaltyTime;
        public static float bonusTime;
        public static bool isSnareKill = false;
        public static bool meetingFlag;

        public static Sprite snareButtonSprite;
        public static DateTime placedTime;

        public static Sprite getSnareButtonSprite()
        {
            if (snareButtonSprite) return snareButtonSprite;
            snareButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.SnarerButton.png", 115f);
            return snareButtonSprite;
        }

        public static void setSnare()
        {
            var pos = CachedPlayer.LocalPlayer.PlayerControl.transform.position;
            byte[] buff = new byte[sizeof(float) * 2];
            Buffer.BlockCopy(BitConverter.GetBytes(pos.x), 0, buff, 0 * sizeof(float), sizeof(float));
            Buffer.BlockCopy(BitConverter.GetBytes(pos.y), 0, buff, 1 * sizeof(float), sizeof(float));
            MessageWriter writer = AmongUsClient.Instance.StartRpc(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.PlaceSnare, Hazel.SendOption.Reliable);
            writer.WriteBytesAndSize(buff);
            writer.EndMessage();
            RPCProcedure.placeSnare(buff);
            placedTime = DateTime.UtcNow;
        }

        public static void clearAndReload()
        {
            snarer = null;
            numSnare = (int)CustomOptionHolder.snarerNumSnare.getFloat();
            extensionTime = CustomOptionHolder.snarerExtensionTime.getFloat();
            killTimer = CustomOptionHolder.snarerKillTimer.getFloat();
            cooldown = CustomOptionHolder.snarerCooldown.getFloat();
            snareRange = CustomOptionHolder.snarerSnareRange.getFloat();
            penaltyTime = CustomOptionHolder.snarerPenaltyTime.getFloat();
            bonusTime = CustomOptionHolder.snarerBonusTime.getFloat();
            meetingFlag = false;
            
        }
    }






    public static class Werewolf
    {
        public static PlayerControl werewolf;
        public static PlayerControl currentTarget;
        public static Color color = new Color32(79, 56, 21, byte.MaxValue);

        // Kill Button 
        public static float killCooldown = 3f;

        // Rampage Button
        public static float rampageCooldown = 30f;
        public static float rampageDuration = 5f;
        public static bool canUseVents = false;
        public static bool canKill = false;
        public static bool hasImpostorVision = false;

        public static Sprite buttonSprite;

        public static Sprite getRampageButtonSprite()
        {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.Rampage.png", 115f);
            return buttonSprite;
        }

        public static Vector3 getRampageVector()
        {
            return new Vector3(-2.7f, -0.06f, 0);
        }

        public static void clearAndReload()
        {
            werewolf = null;
            currentTarget = null;
            canUseVents = false;
            canKill = false;
            hasImpostorVision = false;
            rampageCooldown = CustomOptionHolder.werewolfRampageCooldown.getFloat();
            rampageDuration = CustomOptionHolder.werewolfRampageDuration.getFloat();
            killCooldown = CustomOptionHolder.werewolfKillCooldown.getFloat();

        }
    }

    public static class Juggernaut
    {
        public static PlayerControl juggernaut;
        public static Color color = new Color(0.55f, 0f, 0.3f, Byte.MaxValue);
        public static PlayerControl currentTarget;

        public static float cooldown = 30f;
        public static float reducedkill = 5f;
        public static bool hasImpostorVision = false;

        public static void setkill() {
            cooldown = cooldown - reducedkill;
            if (cooldown <= 0f) {
                cooldown = 0f;
            }

        }
        public static void clearAndReload()
        {
            juggernaut = null;
            currentTarget = null;
            hasImpostorVision = CustomOptionHolder.juggernautHasImpVision.getBool();
            cooldown = CustomOptionHolder.juggernautCooldown.getFloat();
            reducedkill = CustomOptionHolder.juggernautReducedkillEach.getFloat();

        }

    }



    public static class Jackal {
        public static PlayerControl jackal;
        public static Color color = new Color32(0, 180, 235, byte.MaxValue);
        public static PlayerControl fakeSidekick;
        public static PlayerControl currentTarget;
        public static List<PlayerControl> formerJackals = new List<PlayerControl>();
        
        public static float cooldown = 30f;
        public static float createSidekickCooldown = 30f;
        public static bool canUseVents = true;
        public static bool canCreateSidekick = true;
        public static Sprite buttonSprite;
        public static bool jackalPromotedFromSidekickCanCreateSidekick = true;
        public static bool canCreateSidekickFromImpostor = false;
        public static bool hasImpostorVision = false;
        public static bool wasTeamRed;
        public static bool wasImpostor;
        public static bool wasSpy;
        public static bool canSabotageLights;

        public static Sprite getSidekickButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.SidekickButton.png", 115f);
            return buttonSprite;
        }

        public static void removeCurrentJackal() {
            if (!formerJackals.Any(x => x.PlayerId == jackal.PlayerId)) formerJackals.Add(jackal);
            jackal = null;
            currentTarget = null;
            fakeSidekick = null;
            cooldown = CustomOptionHolder.jackalKillCooldown.getFloat();
            createSidekickCooldown = CustomOptionHolder.jackalCreateSidekickCooldown.getFloat();
        }

        public static void clearAndReload() {
            jackal = null;
            currentTarget = null;
            fakeSidekick = null;
            cooldown = CustomOptionHolder.jackalKillCooldown.getFloat();
            createSidekickCooldown = CustomOptionHolder.jackalCreateSidekickCooldown.getFloat();
            canUseVents = CustomOptionHolder.jackalCanUseVents.getBool();
            canCreateSidekick = CustomOptionHolder.jackalCanCreateSidekick.getBool();
            jackalPromotedFromSidekickCanCreateSidekick = CustomOptionHolder.jackalPromotedFromSidekickCanCreateSidekick.getBool();
            //canCreateSidekickFromImpostor = CustomOptionHolder.jackalCanCreateSidekickFromImpostor.getBool();
            formerJackals.Clear();
            hasImpostorVision = CustomOptionHolder.jackalAndSidekickHaveImpostorVision.getBool();
            wasTeamRed = wasImpostor = wasSpy = false;
            canSabotageLights = CustomOptionHolder.jackalCanSabotageLights.getBool();
        }
        
    }

    public static class Sidekick {
        public static PlayerControl sidekick;
        public static Color color = new Color32(0, 180, 235, byte.MaxValue);

        public static PlayerControl currentTarget;

        public static bool wasTeamRed;
        public static bool wasImpostor;
        public static bool wasSpy;

        public static float cooldown = 30f;
        public static bool canUseVents = true;
        public static bool canKill = true;
        public static bool promotesToJackal = true;
        public static bool hasImpostorVision = false;
        public static bool canSabotageLights;

        public static void clearAndReload() {
            sidekick = null;
            currentTarget = null;
            cooldown = CustomOptionHolder.jackalKillCooldown.getFloat();
            canUseVents = CustomOptionHolder.sidekickCanUseVents.getBool();
            canKill = CustomOptionHolder.sidekickCanKill.getBool();
            promotesToJackal = CustomOptionHolder.sidekickPromotesToJackal.getBool();
            hasImpostorVision = CustomOptionHolder.jackalAndSidekickHaveImpostorVision.getBool();
            wasTeamRed = wasImpostor = wasSpy = false;
            canSabotageLights = CustomOptionHolder.sidekickCanSabotageLights.getBool();
        }
    }

    public static class Eraser {
        public static PlayerControl eraser;
        public static Color color = Palette.ImpostorRed;

        public static List<byte> alreadyErased = new List<byte>();

        public static List<PlayerControl> futureErased = new List<PlayerControl>();
        public static PlayerControl currentTarget;
        public static float cooldown = 30f;
        public static bool canEraseAnyone = false; 

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.EraserButton.png", 115f);
            return buttonSprite;
        }

        public static void clearAndReload() {
            eraser = null;
            futureErased = new List<PlayerControl>();
            currentTarget = null;
            cooldown = CustomOptionHolder.eraserCooldown.getFloat();
            canEraseAnyone = CustomOptionHolder.eraserCanEraseAnyone.getBool();
            alreadyErased = new List<byte>();
        }
    }
    
    public static class Spy {
        public static PlayerControl spy;
        public static Color color = Palette.ImpostorRed;

        public static bool impostorsCanKillAnyone = true;
        public static bool canEnterVents = false;
        public static bool hasImpostorVision = false;

        public static void clearAndReload() {
            spy = null;
            impostorsCanKillAnyone = CustomOptionHolder.spyImpostorsCanKillAnyone.getBool();
            canEnterVents = CustomOptionHolder.spyCanEnterVents.getBool();
            hasImpostorVision = CustomOptionHolder.spyHasImpostorVision.getBool();
        }
    }

    public static class Trickster {
        public static PlayerControl trickster;
        public static Color color = Palette.ImpostorRed;
        public static float placeBoxCooldown = 30f;
        public static float lightsOutCooldown = 30f;
        public static float lightsOutDuration = 10f;
        public static float lightsOutTimer = 0f;

        private static Sprite placeBoxButtonSprite;
        private static Sprite lightOutButtonSprite;
        private static Sprite tricksterVentButtonSprite;

        public static Sprite getPlaceBoxButtonSprite() {
            if (placeBoxButtonSprite) return placeBoxButtonSprite;
            placeBoxButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.PlaceJackInTheBoxButton.png", 115f);
            return placeBoxButtonSprite;
        }

        public static Sprite getLightsOutButtonSprite() {
            if (lightOutButtonSprite) return lightOutButtonSprite;
            lightOutButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.LightsOutButton.png", 115f);
            return lightOutButtonSprite;
        }

        public static Sprite getTricksterVentButtonSprite() {
            if (tricksterVentButtonSprite) return tricksterVentButtonSprite;
            tricksterVentButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.TricksterVentButton.png", 115f);
            return tricksterVentButtonSprite;
        }

        public static void clearAndReload() {
            trickster = null;
            lightsOutTimer = 0f;
            placeBoxCooldown = CustomOptionHolder.tricksterPlaceBoxCooldown.getFloat();
            lightsOutCooldown = CustomOptionHolder.tricksterLightsOutCooldown.getFloat();
            lightsOutDuration = CustomOptionHolder.tricksterLightsOutDuration.getFloat();
            JackInTheBox.UpdateStates(); // if the role is erased, we might have to update the state of the created objects
        }

    }

    public static class Cleaner {
        public static PlayerControl cleaner;
        public static Color color = Palette.ImpostorRed;

        public static float cooldown = 30f;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.CleanButton.png", 115f);
            return buttonSprite;
        }

        public static void clearAndReload() {
            cleaner = null;
            cooldown = CustomOptionHolder.cleanerCooldown.getFloat();
        }
    }

  
	
    public static class Poucher
    {
        public static PlayerControl poucher;
        public static Color color = Palette.ImpostorRed;
        public static List<PlayerControl> killed = new List<PlayerControl>();



        public static void clearAndReload(bool clearList = true)
        {
            poucher = null;
			if (clearList) killed = new List<PlayerControl>();

        }
    }

    public static class Warlock {

        public static PlayerControl warlock;
        public static Color color = Palette.ImpostorRed;

        public static PlayerControl currentTarget;
        public static PlayerControl curseVictim;
        public static PlayerControl curseVictimTarget;

        public static float cooldown = 30f;
        public static float rootTime = 5f;

        private static Sprite curseButtonSprite;
        private static Sprite curseKillButtonSprite;

        public static Sprite getCurseButtonSprite() {
            if (curseButtonSprite) return curseButtonSprite;
            curseButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.CurseButton.png", 115f);
            return curseButtonSprite;
        }

        public static Sprite getCurseKillButtonSprite() {
            if (curseKillButtonSprite) return curseKillButtonSprite;
            curseKillButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.CurseKillButton.png", 115f);
            return curseKillButtonSprite;
        }

        public static void clearAndReload() {
            warlock = null;
            currentTarget = null;
            curseVictim = null;
            curseVictimTarget = null;
            cooldown = CustomOptionHolder.warlockCooldown.getFloat();
            rootTime = CustomOptionHolder.warlockRootTime.getFloat();
        }

        public static void resetCurse() {
            HudManagerStartPatch.warlockCurseButton.Timer = HudManagerStartPatch.warlockCurseButton.MaxTimer;
            HudManagerStartPatch.warlockCurseButton.Sprite = Warlock.getCurseButtonSprite();
            HudManagerStartPatch.warlockCurseButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
            currentTarget = null;
            curseVictim = null;
            curseVictimTarget = null;
        }
    }

    public static class SecurityGuard {
        public static PlayerControl securityGuard;
        public static Color color = new Color32(195, 178, 95, byte.MaxValue);

        public static float cooldown = 30f;
        public static int remainingScrews = 7;
        public static int totalScrews = 7;
        public static int ventPrice = 1;
        public static int camPrice = 2;
        public static int placedCameras = 0;
        public static float duration = 10f;
        public static int maxCharges = 5;
        public static int rechargeTasksNumber = 3;
        public static int rechargedTasks = 3;
        public static int charges = 1;
        public static bool cantMove = true;
        public static Vent ventTarget = null;
        public static Minigame minigame = null;

        private static Sprite closeVentButtonSprite;
        public static Sprite getCloseVentButtonSprite() {
            if (closeVentButtonSprite) return closeVentButtonSprite;
            closeVentButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.CloseVentButton.png", 115f);
            return closeVentButtonSprite;
        }

        private static Sprite placeCameraButtonSprite;
        public static Sprite getPlaceCameraButtonSprite() {
            if (placeCameraButtonSprite) return placeCameraButtonSprite;
            placeCameraButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.PlaceCameraButton.png", 115f);
            return placeCameraButtonSprite;
        }

        private static Sprite animatedVentSealedSprite;
        private static float lastPPU;
        public static Sprite getAnimatedVentSealedSprite() {
            float ppu = 185f;
            if (SubmergedCompatibility.IsSubmerged) ppu = 120f;
            if (lastPPU != ppu) {
                animatedVentSealedSprite = null;
                lastPPU = ppu;
            }
            if (animatedVentSealedSprite) return animatedVentSealedSprite;
            animatedVentSealedSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.AnimatedVentSealed.png", ppu);
            return animatedVentSealedSprite;
        }

        private static Sprite staticVentSealedSprite;
        public static Sprite getStaticVentSealedSprite() {
            if (staticVentSealedSprite) return staticVentSealedSprite;
            staticVentSealedSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.StaticVentSealed.png", 160f);
            return staticVentSealedSprite;
        }

        private static Sprite fungleVentSealedSprite;
        public static Sprite getFungleVentSealedSprite() {
            if (fungleVentSealedSprite) return fungleVentSealedSprite;
            fungleVentSealedSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.FungleVentSealed.png", 160f);
            return fungleVentSealedSprite;
        }


        private static Sprite submergedCentralUpperVentSealedSprite;
        public static Sprite getSubmergedCentralUpperSealedSprite() {
            if (submergedCentralUpperVentSealedSprite) return submergedCentralUpperVentSealedSprite;
            submergedCentralUpperVentSealedSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.CentralUpperBlocked.png", 145f);
            return submergedCentralUpperVentSealedSprite;
        }

        private static Sprite submergedCentralLowerVentSealedSprite;
        public static Sprite getSubmergedCentralLowerSealedSprite() {
            if (submergedCentralLowerVentSealedSprite) return submergedCentralLowerVentSealedSprite;
            submergedCentralLowerVentSealedSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.CentralLowerBlocked.png", 145f);
            return submergedCentralLowerVentSealedSprite;
        }

        private static Sprite camSprite;
        public static Sprite getCamSprite() {
            if (camSprite) return camSprite;
            camSprite = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.CamsButton].Image;
            return camSprite;
        }

        private static Sprite logSprite;
        public static Sprite getLogSprite() {
            if (logSprite) return logSprite;
            logSprite = FastDestroyableSingleton<HudManager>.Instance.UseButton.fastUseSettings[ImageNames.DoorLogsButton].Image;
            return logSprite;
        }

        public static void clearAndReload() {
            securityGuard = null;
            ventTarget = null;
            minigame = null;
            duration = CustomOptionHolder.securityGuardCamDuration.getFloat();
            maxCharges = Mathf.RoundToInt(CustomOptionHolder.securityGuardCamMaxCharges.getFloat());
            rechargeTasksNumber = Mathf.RoundToInt(CustomOptionHolder.securityGuardCamRechargeTasksNumber.getFloat());
            rechargedTasks = Mathf.RoundToInt(CustomOptionHolder.securityGuardCamRechargeTasksNumber.getFloat());
            charges = Mathf.RoundToInt(CustomOptionHolder.securityGuardCamMaxCharges.getFloat()) /2;
            placedCameras = 0;
            cooldown = CustomOptionHolder.securityGuardCooldown.getFloat();
            totalScrews = remainingScrews = Mathf.RoundToInt(CustomOptionHolder.securityGuardTotalScrews.getFloat());
            camPrice = Mathf.RoundToInt(CustomOptionHolder.securityGuardCamPrice.getFloat());
            ventPrice = Mathf.RoundToInt(CustomOptionHolder.securityGuardVentPrice.getFloat());
            cantMove = CustomOptionHolder.securityGuardNoMove.getBool();
        }
    }

    public static class Arsonist {
        public static PlayerControl arsonist;
        public static Color color = new Color32(238, 112, 46, byte.MaxValue);

        public static float cooldown = 30f;
        public static float duration = 3f;
        public static bool triggerArsonistWin = false;

        public static PlayerControl currentTarget;
        public static PlayerControl douseTarget;
        public static List<PlayerControl> dousedPlayers = new List<PlayerControl>();
	public static List<PoolablePlayer> poolIcons = new List<PoolablePlayer>();

        private static Sprite douseSprite;
        public static Sprite getDouseSprite() {
            if (douseSprite) return douseSprite;
            douseSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.DouseButton.png", 115f);
            return douseSprite;
        }

        private static Sprite igniteSprite;
        public static Sprite getIgniteSprite() {
            if (igniteSprite) return igniteSprite;
            igniteSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.IgniteButton.png", 115f);
            return igniteSprite;
        }

        public static bool dousedEveryoneAlive() {
            return CachedPlayer.AllPlayers.All(x => { return x.PlayerControl == Arsonist.arsonist || x.Data.IsDead || x.Data.Disconnected || Arsonist.dousedPlayers.Any(y => y.PlayerId == x.PlayerId); });
        }

        public static void clearAndReload() {
            arsonist = null;
            currentTarget = null;
            douseTarget = null; 
            triggerArsonistWin = false;
            dousedPlayers = new List<PlayerControl>();
            foreach (PoolablePlayer p in TORMapOptions.playerIcons.Values) {
                if (p != null && p.gameObject != null) p.gameObject.SetActive(false);
            }
            cooldown = CustomOptionHolder.arsonistCooldown.getFloat();
            duration = CustomOptionHolder.arsonistDuration.getFloat();
        }
    }

    public static class Guesser {
        public static PlayerControl niceGuesser;
        public static PlayerControl evilGuesser;
        public static Color color = new Color32(255, 255, 0, byte.MaxValue);

        public static int remainingShotsEvilGuesser = 2;
        public static int remainingShotsNiceGuesser = 2;

        public static bool isGuesser (byte playerId) {
            if ((niceGuesser != null && niceGuesser.PlayerId == playerId) || (evilGuesser != null && evilGuesser.PlayerId == playerId)) return true;
            return false;
        }

        public static void clear (byte playerId) {
            if (niceGuesser != null && niceGuesser.PlayerId == playerId) niceGuesser = null;
            else if (evilGuesser != null && evilGuesser.PlayerId == playerId) evilGuesser = null;
        }

        public static int remainingShots(byte playerId, bool shoot = false) {
            int remainingShots = remainingShotsEvilGuesser;
            if (niceGuesser != null && niceGuesser.PlayerId == playerId) {
                remainingShots = remainingShotsNiceGuesser;
                if (shoot) remainingShotsNiceGuesser = Mathf.Max(0, remainingShotsNiceGuesser - 1);
            } else if (shoot) {
                remainingShotsEvilGuesser = Mathf.Max(0, remainingShotsEvilGuesser - 1);
            }
            return remainingShots;
        }

        public static void clearAndReload() {
            niceGuesser = null;
            evilGuesser = null;
            remainingShotsEvilGuesser = Mathf.RoundToInt(CustomOptionHolder.guesserNumberOfShots.getFloat());
            remainingShotsNiceGuesser = Mathf.RoundToInt(CustomOptionHolder.guesserNumberOfShots.getFloat());
        }
    }

    public static class BountyHunter {
        public static PlayerControl bountyHunter;
        public static Color color = Palette.ImpostorRed;

        public static Arrow arrow;
        public static float bountyDuration = 30f;
        public static bool showArrow = true;
        public static float bountyKillCooldown = 0f;
        public static float punishmentTime = 15f;
        public static float arrowUpdateIntervall = 10f;

        public static float arrowUpdateTimer = 0f;
        public static float bountyUpdateTimer = 0f;
        public static PlayerControl bounty;
        public static TMPro.TextMeshPro cooldownText;

        public static void clearAndReload() {
            arrow = new Arrow(color);
            bountyHunter = null;
            bounty = null;
            arrowUpdateTimer = 0f;
            bountyUpdateTimer = 0f;
            if (arrow != null && arrow.arrow != null) UnityEngine.Object.Destroy(arrow.arrow);
            arrow = null;
            if (cooldownText != null && cooldownText.gameObject != null) UnityEngine.Object.Destroy(cooldownText.gameObject);
            cooldownText = null;
            foreach (PoolablePlayer p in TORMapOptions.playerIcons.Values) {
                if (p != null && p.gameObject != null) p.gameObject.SetActive(false);
            }


            bountyDuration = CustomOptionHolder.bountyHunterBountyDuration.getFloat();
            bountyKillCooldown = CustomOptionHolder.bountyHunterReducedCooldown.getFloat();
            punishmentTime = CustomOptionHolder.bountyHunterPunishmentTime.getFloat();
            showArrow = CustomOptionHolder.bountyHunterShowArrow.getBool();
            arrowUpdateIntervall = CustomOptionHolder.bountyHunterArrowUpdateIntervall.getFloat();
        }
    }

    public static class Vulture {
        public static PlayerControl vulture;
        public static Color color = new Color32(139, 69, 19, byte.MaxValue);
        public static List<Arrow> localArrows = new List<Arrow>();
        public static float cooldown = 30f;
        public static int vultureNumberToWin = 4;
        public static int eatenBodies = 0;
        public static bool triggerVultureWin = false;
        public static bool canUseVents = true;
        public static bool showArrows = true;
        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.VultureButton.png", 115f);
            return buttonSprite;
        }

        public static void clearAndReload() {
            vulture = null;
            vultureNumberToWin = Mathf.RoundToInt(CustomOptionHolder.vultureNumberToWin.getFloat());
            eatenBodies = 0;
            cooldown = CustomOptionHolder.vultureCooldown.getFloat();
            triggerVultureWin = false;
            canUseVents = CustomOptionHolder.vultureCanUseVents.getBool();
            showArrows = CustomOptionHolder.vultureShowArrows.getBool();
            if (localArrows != null) {
                foreach (Arrow arrow in localArrows)
                    if (arrow?.arrow != null)
                        UnityEngine.Object.Destroy(arrow.arrow);
            }
            localArrows = new List<Arrow>();
        }
    }


    public static class Medium {
        public static PlayerControl medium;
        public static DeadPlayer target;
        public static DeadPlayer soulTarget;
        public static Color color = new Color32(98, 120, 115, byte.MaxValue);
        public static List<Tuple<DeadPlayer, Vector3>> deadBodies = new List<Tuple<DeadPlayer, Vector3>>();
        public static List<Tuple<DeadPlayer, Vector3>> futureDeadBodies = new List<Tuple<DeadPlayer, Vector3>>();
        public static List<SpriteRenderer> souls = new List<SpriteRenderer>();
        public static DateTime meetingStartTime = DateTime.UtcNow;

        public static float cooldown = 30f;
        public static float duration = 3f;
        public static bool oneTimeUse = false;
        public static float chanceAdditionalInfo = 0f;

        private static Sprite soulSprite;

        enum SpecialMediumInfo {
            SheriffSuicide,
            ThiefSuicide,
            ActiveLoverDies,
            PassiveLoverSuicide,
            LawyerKilledByClient,
            JackalKillsSidekick,
            ImpostorTeamkill,
            SubmergedO2,
            WarlockSuicide,
            BodyCleaned,
        }

        public static Sprite getSoulSprite() {
            if (soulSprite) return soulSprite;
            soulSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.Soul.png", 500f);
            return soulSprite;
        }

        private static Sprite question;
        public static Sprite getQuestionSprite() {
            if (question) return question;
            question = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.MediumButton.png", 115f);
            return question;
        }

        public static void clearAndReload() {
            medium = null;
            target = null;
            soulTarget = null;
            deadBodies = new List<Tuple<DeadPlayer, Vector3>>();
            futureDeadBodies = new List<Tuple<DeadPlayer, Vector3>>();
            souls = new List<SpriteRenderer>();
            meetingStartTime = DateTime.UtcNow;
            cooldown = CustomOptionHolder.mediumCooldown.getFloat();
            duration = CustomOptionHolder.mediumDuration.getFloat();
            oneTimeUse = CustomOptionHolder.mediumOneTimeUse.getBool();
            chanceAdditionalInfo = CustomOptionHolder.mediumChanceAdditionalInfo.getSelection() / 10f;
        }

        public static string getInfo(PlayerControl target, PlayerControl killer) {
            string msg = "";

            List<SpecialMediumInfo> infos = new List<SpecialMediumInfo>();
            // collect fitting death info types.
            // suicides:
            if (killer == target) {
                if (target == Sheriff.sheriff || target == Sheriff.formerSheriff) infos.Add(SpecialMediumInfo.SheriffSuicide);
                if (target == Lovers.lover1 || target == Lovers.lover2) infos.Add(SpecialMediumInfo.PassiveLoverSuicide);
                if (target == Thief.thief) infos.Add(SpecialMediumInfo.ThiefSuicide);
                if (target == Warlock.warlock) infos.Add(SpecialMediumInfo.WarlockSuicide);
            } else {
                if (target == Lovers.lover1 || target == Lovers.lover2) infos.Add(SpecialMediumInfo.ActiveLoverDies);
                if (target.Data.Role.IsImpostor && killer.Data.Role.IsImpostor && Thief.formerThief != killer) infos.Add(SpecialMediumInfo.ImpostorTeamkill);
            }
            if (target == Sidekick.sidekick && (killer == Jackal.jackal || Jackal.formerJackals.Any(x => x.PlayerId == killer.PlayerId))) infos.Add(SpecialMediumInfo.JackalKillsSidekick);
            if (target == Lawyer.lawyer && killer == Lawyer.target) infos.Add(SpecialMediumInfo.LawyerKilledByClient);
            if (Medium.target.wasCleaned) infos.Add(SpecialMediumInfo.BodyCleaned);
            
            if (infos.Count > 0) {
                var selectedInfo = infos[rnd.Next(infos.Count)];
                switch (selectedInfo) {
                    case SpecialMediumInfo.SheriffSuicide:
                        msg = "害, 警长走火了!";
                        break;
                    case SpecialMediumInfo.WarlockSuicide:
                        msg = "哎呀. 我的诅咒貌似降临到自己身上，咒杀了自己!";
                        break;
                    case SpecialMediumInfo.ThiefSuicide:
                        msg = "我想要从他们的兜里抢到一把枪, 但是他们却是满面笑容、天真无邪地看向我...";
                        break;
                    case SpecialMediumInfo.ActiveLoverDies:
                        msg = "不仅一次我想要剪断这让她痛不欲生的红线。";
                        break;
                    case SpecialMediumInfo.PassiveLoverSuicide:
                        msg = "我最挚爱的人死了, 我也想要一了百了了, 去天国迎接他的吻。";
                        break;
                    case SpecialMediumInfo.LawyerKilledByClient:
                        msg = "我的客户把我杀了。请问我还能拿到酬金吗?";
                        break;
                    case SpecialMediumInfo.JackalKillsSidekick:
                        msg = "他们招募了我, 却又杀了我。不过至少看样子我不用做任务了。";
                        break;
                    case SpecialMediumInfo.ImpostorTeamkill:
                        msg = "呵呵, 我想他们把我当做卧底了?";
                        break;
                    case SpecialMediumInfo.BodyCleaned:
                        msg = "我的尸体成为艺术品了吗? 让我看看有没有价值辶...啊? 我尸体呢?";
                        break;
                }
            } else {
                int randomNumber = rnd.Next(4);
                string typeOfColor = Helpers.isLighterColor(Medium.target.killerIfExisting) ? "浅色的" : "深色的";
                float timeSinceDeath = ((float)(Medium.meetingStartTime - Medium.target.timeOfDeath).TotalMilliseconds);
                var roleString = RoleInfo.GetRolesString(Medium.target.player, false);
                if (randomNumber == 0) {
                    if (!roleString.Contains("内鬼") && !roleString.Contains("船员"))
                        msg = "如果我死了,那么场上将不在有 " + roleString + " 了。";
                    else
                        msg = "我是一个" + roleString + " 没有其他额外的角色了."; 
                } else if (randomNumber == 1) msg = "我不确定, 但是我认为是一个有着" + typeOfColor + " 颜色的人杀了我。";
                else if (randomNumber == 2) msg = "如果我没记错的话, 我在会议前 " + Math.Round(timeSinceDeath / 1000) + "秒就死了。";
                else msg = "我想那杀我的凶手职业应该是 " + RoleInfo.GetRolesString(Medium.target.killerIfExisting, false, false, true) + "。";
            }

            if (rnd.NextDouble() < chanceAdditionalInfo) {
                int count = 0;
                string condition = "";
                var alivePlayersList = PlayerControl.AllPlayerControls.ToArray().Where(pc => !pc.Data.IsDead);
                switch (rnd.Next(3)) {
                    case 0:
                        count = alivePlayersList.Where(pc => pc.Data.Role.IsImpostor || new List<RoleInfo>() { RoleInfo.jackal, RoleInfo.sidekick, RoleInfo.sheriff, RoleInfo.thief, RoleInfo.swooper, RoleInfo.werewolf, RoleInfo.juggernaut, RoleInfo.cerenovus, RoleInfo.onetimekiller }.Contains(RoleInfo.getRoleInfoForPlayer(pc, false).FirstOrDefault())).Count();
                        condition = "名持刃者" + (count == 1 ? "" : "");
                        break;
                    case 1:
                        count = alivePlayersList.Where(Helpers.roleCanUseVents).Count();
                        condition = "名玩家" + (count == 1 ? "" : "") + " 作为可跳管的人";
                        break;
                    case 2:
                        count = alivePlayersList.Where(pc => Helpers.isNeutral(pc) && pc != Jackal.jackal && pc != Sidekick.sidekick && pc != Thief.thief && pc != Swooper.swooper && pc != Werewolf.werewolf && pc != Juggernaut.juggernaut && pc != Cerenovus.cerenovus).Count();
                        condition = "名玩家" + (count == 1 ? "" : "s") + " 作为 " + (count == 1 ? "" : "") + " 无刀独立职业";
                        break;
                    case 3:
                        //count = alivePlayersList.Where(pc =>
                        break;               
                }
                msg += $"\n当你询问时, {count} " + condition + (count == 1 ? " " : " ") + " 仍然活着";
            }

            return Medium.target.player.Data.PlayerName + "的灵魂:\n" + msg;
        }
    }

    public static class Lawyer {
        public static PlayerControl lawyer;
        public static PlayerControl target;
        public static Color color = new Color32(134, 153, 25, byte.MaxValue);
        public static Sprite targetSprite;
        public static bool triggerProsecutorWin = false;
        public static bool isProsecutor = false;
        public static bool canCallEmergency = true;

        public static float vision = 1f;
        public static bool lawyerKnowsRole = false;
        public static bool targetCanBeJester = false;
        public static bool targetWasGuessed = false;

        public static Sprite getTargetSprite() {
            if (targetSprite) return targetSprite;
            targetSprite = Helpers.loadSpriteFromResources("", 150f);
            return targetSprite;
        }

        public static void clearAndReload(bool clearTarget = true) {
            lawyer = null;
            if (clearTarget) {
                target = null;
                targetWasGuessed = false;
            }
            isProsecutor = false;
            triggerProsecutorWin = false;
            vision = CustomOptionHolder.lawyerVision.getFloat();
            lawyerKnowsRole = CustomOptionHolder.lawyerKnowsRole.getBool();
            targetCanBeJester = CustomOptionHolder.lawyerTargetCanBeJester.getBool();
            canCallEmergency = CustomOptionHolder.jesterCanCallEmergency.getBool();
        }
    }

    public static class Pursuer {
        public static PlayerControl pursuer;
        public static PlayerControl target;
        public static Color color = Lawyer.color;
        public static List<PlayerControl> blankedList = new List<PlayerControl>();
        public static int blanks = 0;
        public static Sprite blank;
        public static bool notAckedExiled = false;

        public static float cooldown = 30f;
        public static int blanksNumber = 5;

        public static Sprite getTargetSprite() {
            if (blank) return blank;
            blank = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.PursuerButton.png", 115f);
            return blank;
        }

        public static void clearAndReload() {
            pursuer = null;
            target = null;
            blankedList = new List<PlayerControl>();
            blanks = 0;
            notAckedExiled = false;

            cooldown = CustomOptionHolder.pursuerCooldown.getFloat();
            blanksNumber = Mathf.RoundToInt(CustomOptionHolder.pursuerBlanksNumber.getFloat());
        }
    }

    public static class Witch {
        public static PlayerControl witch;
        public static Color color = Palette.ImpostorRed;

        public static List<PlayerControl> futureSpelled = new List<PlayerControl>();
        public static PlayerControl currentTarget;
        public static PlayerControl spellCastingTarget;
        public static float cooldown = 30f;
        public static float spellCastingDuration = 2f;
        public static float cooldownAddition = 10f;
        public static float currentCooldownAddition = 0f;
        public static bool canSpellAnyone = false;
        public static bool triggerBothCooldowns = true;
        public static bool witchVoteSavesTargets = true;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.SpellButton.png", 115f);
            return buttonSprite;
        }

        private static Sprite spelledOverlaySprite;
        public static Sprite getSpelledOverlaySprite() {
            if (spelledOverlaySprite) return spelledOverlaySprite;
            spelledOverlaySprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.SpellButtonMeeting.png", 225f);
            return spelledOverlaySprite;
        }


        public static void clearAndReload() {
            witch = null;
            futureSpelled = new List<PlayerControl>();
            currentTarget = spellCastingTarget = null;
            cooldown = CustomOptionHolder.witchCooldown.getFloat();
            cooldownAddition = CustomOptionHolder.witchAdditionalCooldown.getFloat();
            currentCooldownAddition = 0f;
            canSpellAnyone = CustomOptionHolder.witchCanSpellAnyone.getBool();
            spellCastingDuration = CustomOptionHolder.witchSpellCastingDuration.getFloat();
            triggerBothCooldowns = CustomOptionHolder.witchTriggerBothCooldowns.getBool();
            witchVoteSavesTargets = CustomOptionHolder.witchVoteSavesTargets.getBool();
        }
    }

    public static class Ninja {
        public static PlayerControl ninja;
        public static Color color = Palette.ImpostorRed;

        public static PlayerControl ninjaMarked;
        public static PlayerControl currentTarget;
        public static float cooldown = 30f;
        public static float traceTime = 1f;
        public static bool knowsTargetLocation = false;
        public static float invisibleDuration = 5f;

        public static float invisibleTimer = 0f;
        public static bool isInvisble = false;
        private static Sprite markButtonSprite;
        private static Sprite killButtonSprite;
        public static Arrow arrow = new Arrow(Color.black);
        public static Sprite getMarkButtonSprite() {
            if (markButtonSprite) return markButtonSprite;
            markButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.NinjaMarkButton.png", 115f);
            return markButtonSprite;
        }

        public static Sprite getKillButtonSprite() {
            if (killButtonSprite) return killButtonSprite;
            killButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.NinjaAssassinateButton.png", 115f);
            return killButtonSprite;
        }

        public static void clearAndReload() {
            ninja = null;
            currentTarget = ninjaMarked = null;
            cooldown = CustomOptionHolder.ninjaCooldown.getFloat();
            knowsTargetLocation = CustomOptionHolder.ninjaKnowsTargetLocation.getBool();
            traceTime = CustomOptionHolder.ninjaTraceTime.getFloat();
            invisibleDuration = CustomOptionHolder.ninjaInvisibleDuration.getFloat();
            invisibleTimer = 0f;
            isInvisble = false;
            if (arrow?.arrow != null) UnityEngine.Object.Destroy(arrow.arrow);
            arrow = new Arrow(Color.black);
            if (arrow.arrow != null) arrow.arrow.SetActive(false);
        }
    }

    public static class Thief {
        public static PlayerControl thief;
        public static Color color = new Color32(71, 99, 45, Byte.MaxValue);
        public static PlayerControl currentTarget;
        public static PlayerControl formerThief;

        public static float cooldown = 30f;

        public static bool suicideFlag = false;  // Used as a flag for suicide

        public static bool hasImpostorVision;
        public static bool canUseVents;
        public static bool canKillSheriff;
        public static bool canStealWithGuess;
        public static bool canKillDeputy;
        public static bool canKillVeteren;

        public static void clearAndReload() {
            thief = null;
            suicideFlag = false;
            currentTarget = null;
            formerThief = null;
            hasImpostorVision = CustomOptionHolder.thiefHasImpVision.getBool();
            cooldown = CustomOptionHolder.thiefCooldown.getFloat();
            canUseVents = CustomOptionHolder.thiefCanUseVents.getBool();
            canKillSheriff = CustomOptionHolder.thiefCanKillSheriff.getBool();
            canStealWithGuess = CustomOptionHolder.thiefCanStealWithGuess.getBool();
            canKillDeputy = CustomOptionHolder.thiefCanKillDeputy.getBool();
            canKillVeteren = CustomOptionHolder.thiefCanKillVeteren.getBool();
        }

        public static bool isFailedThiefKill(PlayerControl target, PlayerControl killer, RoleInfo targetRole) {
            return killer == Thief.thief && !target.Data.Role.IsImpostor && !new List<RoleInfo> { RoleInfo.jackal, canKillSheriff ? RoleInfo.sheriff : null,canKillDeputy ? RoleInfo.deputy : null,canKillVeteren ? RoleInfo.veteren : null, RoleInfo.sidekick, RoleInfo.swooper, RoleInfo.werewolf, RoleInfo.juggernaut, RoleInfo.cerenovus }.Contains(targetRole);
        }
    }

        public static class Trapper {
        public static PlayerControl trapper;
        public static Color color = new Color32(110, 57, 105, byte.MaxValue);

        public static float cooldown = 30f;
        public static int maxCharges = 5;
        public static int rechargeTasksNumber = 3;
        public static int rechargedTasks = 3;
        public static int charges = 1;
        public static int trapCountToReveal = 2;
        public static List<PlayerControl> playersOnMap = new List<PlayerControl>();
        public static bool anonymousMap = false;
        public static int infoType = 0; // 0 = Role, 1 = Good/Evil, 2 = Name
        public static float trapDuration = 5f; 

        private static Sprite trapButtonSprite;

        public static Sprite getButtonSprite() {
            if (trapButtonSprite) return trapButtonSprite;
            trapButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.Trapper_Place_Button.png", 115f);
            return trapButtonSprite;
        }

        public static void clearAndReload() {
            trapper = null;
            cooldown = CustomOptionHolder.trapperCooldown.getFloat();
            maxCharges = Mathf.RoundToInt(CustomOptionHolder.trapperMaxCharges.getFloat());
            rechargeTasksNumber = Mathf.RoundToInt(CustomOptionHolder.trapperRechargeTasksNumber.getFloat());
            rechargedTasks = Mathf.RoundToInt(CustomOptionHolder.trapperRechargeTasksNumber.getFloat());
            charges = Mathf.RoundToInt(CustomOptionHolder.trapperMaxCharges.getFloat()) / 2;
            trapCountToReveal = Mathf.RoundToInt(CustomOptionHolder.trapperTrapNeededTriggerToReveal.getFloat());
            playersOnMap = new List<PlayerControl>();
            anonymousMap = CustomOptionHolder.trapperAnonymousMap.getBool();
            infoType = CustomOptionHolder.trapperInfoType.getSelection();
            trapDuration = CustomOptionHolder.trapperTrapDuration.getFloat();
        }
    }

    public static class Bomber {
        public static PlayerControl bomber = null;
        public static Color color = Palette.ImpostorRed;

        public static Bomb bomb = null;
        public static bool isPlanted = false;
        public static bool isActive = false;
        public static float destructionTime = 20f;
        public static float destructionRange = 2f;
        public static float hearRange = 30f;
        public static float defuseDuration = 3f;
        public static float bombCooldown = 15f;
        public static float bombActiveAfter = 3f;

        private static Sprite buttonSprite;

        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.Bomb_Button_Plant.png", 115f);
            return buttonSprite;
        }

        public static void clearBomb(bool flag = true) {
            if (bomb != null) {
                UnityEngine.Object.Destroy(bomb.bomb);
                UnityEngine.Object.Destroy(bomb.background);
                bomb = null;
            }
            isPlanted = false;
            isActive = false;
            if (flag) SoundEffectsManager.stop("bombFuseBurning");
        }

        public static void clearAndReload() {
            clearBomb(false);
            bomber = null;
            bomb = null;
            isPlanted = false;
            isActive = false;
            destructionTime = CustomOptionHolder.bomberBombDestructionTime.getFloat();
            destructionRange = CustomOptionHolder.bomberBombDestructionRange.getFloat() / 10;
            hearRange = CustomOptionHolder.bomberBombHearRange.getFloat() / 10;
            defuseDuration = CustomOptionHolder.bomberDefuseDuration.getFloat();
            bombCooldown = CustomOptionHolder.bomberBombCooldown.getFloat();
            bombActiveAfter = CustomOptionHolder.bomberBombActiveAfter.getFloat();
            Bomb.clearBackgroundSprite();
        }
    }


    public static class Yoyo {
        public static PlayerControl yoyo = null;
        public static Color color = Palette.ImpostorRed;

        public static float blinkDuration = 0;
        public static float markCooldown = 0;
        public static bool markStaysOverMeeting = false;
        public static bool hasAdminTable = false;
        public static float adminCooldown = 0;
        public static float SilhouetteVisibility => (silhouetteVisibility == 0 && (PlayerControl.LocalPlayer == yoyo || PlayerControl.LocalPlayer.Data.IsDead)) ? 0.1f : silhouetteVisibility;
        public static float silhouetteVisibility = 0;

        public static Vector3? markedLocation = null;

        private static Sprite markButtonSprite;

        public static Sprite getMarkButtonSprite() {
            if (markButtonSprite) return markButtonSprite;
            markButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.YoyoMarkButtonSprite.png", 115f);
            return markButtonSprite;
        }
        private static Sprite blinkButtonSprite;

        public static Sprite getBlinkButtonSprite() {
            if (blinkButtonSprite) return blinkButtonSprite;
            blinkButtonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.YoyoBlinkButtonSprite.png", 115f);
            return blinkButtonSprite;
        }

        public static void markLocation(Vector3 position) {
            markedLocation = position;
        }

        public static void clearAndReload() {
            blinkDuration = CustomOptionHolder.yoyoBlinkDuration.getFloat();
            markCooldown = CustomOptionHolder.yoyoMarkCooldown.getFloat();
            markStaysOverMeeting = CustomOptionHolder.yoyoMarkStaysOverMeeting.getBool();
            hasAdminTable = CustomOptionHolder.yoyoHasAdminTable.getBool();
            adminCooldown = CustomOptionHolder.yoyoAdminTableCooldown.getFloat();
            silhouetteVisibility = CustomOptionHolder.yoyoSilhouetteVisibility.getSelection() / 10f;

            markedLocation = null;
            
        }
    }





    public static class Akujo
    {
        public static Color color = new Color32(142, 69, 147, byte.MaxValue);
        public static PlayerControl akujo;
        public static PlayerControl honmei;
        public static List<PlayerControl> keeps;
        public static PlayerControl currentTarget;
        public static DateTime startTime;

        public static float timeLimit = 1300f;
        public static bool knowsRoles = true;
        public static int timeLeft;
        public static int keepsLeft;
        public static int numKeeps;

        private static Sprite honmeiSprite;
        public static Sprite getHonmeiSprite()
        {
            if (honmeiSprite) return honmeiSprite;
            honmeiSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.AkujoHonmeiButton.png", 115f);
            return honmeiSprite;
        }

        private static Sprite keepSprite;
        public static Sprite getKeepSprite()
        {
            if (keepSprite) return keepSprite;
            keepSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.AkujoKeepButton.png", 115f);
            return keepSprite;
        }

        public static void breakLovers(PlayerControl lover)
        {
            if ((Lovers.lover1 != null && lover == Lovers.lover1) || (Lovers.lover2 != null && lover == Lovers.lover2))
            {
                PlayerControl otherLover = lover.getPartner();
                if (otherLover != null)
                {
                    Lovers.clearAndReload();
                    otherLover.MurderPlayer(otherLover, MurderResultFlags.Succeeded);
                    GameHistory.overrideDeathReasonAndKiller(otherLover, DeadPlayer.CustomDeathReason.LoveStolen);
                }
            }
        }

        public static void clearAndReload()
        {
            akujo = null;
            honmei = null;
            keeps = new List<PlayerControl>();
            currentTarget = null;
            startTime = DateTime.UtcNow;
            timeLimit = CustomOptionHolder.akujoTimeLimit.getFloat() + 180f;
            knowsRoles = CustomOptionHolder.akujoKnowsRoles.getBool();
            timeLeft = (int)Math.Ceiling(timeLimit - (DateTime.UtcNow - startTime).TotalSeconds);
            numKeeps = Math.Min((int)CustomOptionHolder.akujoNumKeeps.getFloat(), PlayerControl.AllPlayerControls.Count - 2);
            keepsLeft = numKeeps;
        }
    }

    public static class Cerenovus
    {
        public static PlayerControl cerenovus;
        public static PlayerControl formerCerenovus;
        public static Color color = Color.green;

        public static PlayerControl tmpTarget;
        public static PlayerControl target;
        public static PlayerControl currentTarget;
        public static PlayerControl killTarget;
        public static List<PlayerControl> brainwashed;

        public static int counter;

        public static float brainwashTime = 2f;
        public static float brainwashCooldown = 30f;
        public static int numberToWin = 3;

        public static Sprite brainwashIcon;

        public static List<Arrow> arrows = new();
        public static float updateTimer = 0f;
        public static float arrowUpdateInterval = 0.5f;
        public static TMPro.TMP_Text targetPositionText;

        public static bool triggerCerenovusWin = false;

        public static Sprite getBrainwashIcon()
        {
            if (brainwashIcon) return brainwashIcon;
            brainwashIcon = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.BrainwashButton.png", 115f);
            return brainwashIcon;
        }

        public static void arrowUpdate()
        {

            //前フレームからの経過時間をマイナスする
            updateTimer -= Time.fixedDeltaTime;

            // 1秒経過したらArrowを更新
            if (updateTimer <= 0.0f)
            {

                // 前回のArrowをすべて破棄する
                foreach (Arrow arrow in arrows)
                {
                    if (arrow != null && arrow.arrow != null)
                    {
                        arrow.arrow.SetActive(false);
                        UnityEngine.Object.Destroy(arrow.arrow);
                    }
                }

                // Arrows一覧
                arrows = new List<Arrow>();
                // ターゲットの位置を示すArrowを描画
                if (target != null && !target.Data.IsDead)
                {
                    Arrow arrow = new(Palette.CrewmateBlue);
                    arrow.arrow.SetActive(true);
                    arrow.Update(target.transform.position);
                    arrows.Add(arrow);
                    if (targetPositionText == null)
                    {
                        RoomTracker roomTracker = HudManager.Instance?.roomTracker;
                        if (roomTracker == null) return;
                        GameObject gameObject = UnityEngine.Object.Instantiate(roomTracker.gameObject);
                        UnityEngine.Object.DestroyImmediate(gameObject.GetComponent<RoomTracker>());
                        gameObject.transform.SetParent(HudManager.Instance.transform);
                        gameObject.transform.localPosition = new Vector3(0, -2.0f, gameObject.transform.localPosition.z);
                        gameObject.transform.localScale = Vector3.one * 1.0f;
                        targetPositionText = gameObject.GetComponent<TMPro.TMP_Text>();
                        targetPositionText.alpha = 1.0f;
                    }
                    PlainShipRoom room = Helpers.getPlainShipRoom(target);
                    targetPositionText.gameObject.SetActive(true);
                    int nearestPlayer = 0;
                    foreach (var p in PlayerControl.AllPlayerControls)
                    {
                        if (p != target && !p.Data.IsDead)
                        {
                            float dist = Vector2.Distance(p.transform.position, target.transform.position);
                            if (dist < 7f) nearestPlayer += 1;
                        }
                    }
                    if (room != null)
                    {
                        targetPositionText.text = "<color=#8CFFFFFF>" + $"{target.Data.PlayerName}({nearestPlayer})(" + DestroyableSingleton<TranslationController>.Instance.GetString(room.RoomId) + ")</color>";
                    }
                    else
                    {
                        targetPositionText.text = "<color=#8CFFFFFF>" + $"{target.Data.PlayerName}({nearestPlayer})</color>";
                    }
                }
                else
                {
                    if (targetPositionText != null)
                    {
                        targetPositionText.text = "";
                    }
                }

                // タイマーに時間をセット
                updateTimer = arrowUpdateInterval;
            }
        }

        public static void clearAndReload()
        {
            cerenovus = null;
            formerCerenovus = null;
            tmpTarget = null;
            target = null;
            currentTarget = null;
            killTarget = null;
            brainwashed = new List<PlayerControl>();
            counter = 0;
            triggerCerenovusWin = false;
            brainwashCooldown = CustomOptionHolder.cerenovusBrainwashCooldown.getFloat();
            brainwashTime = CustomOptionHolder.cerenovusBrainwashTime.getFloat();
            numberToWin = (int)CustomOptionHolder.cerenovusNumberToWin.getFloat();

            if (targetPositionText != null) UnityEngine.Object.Destroy(targetPositionText);
            targetPositionText = null;
            if (arrows != null)
            {
                foreach (Arrow arrow in arrows)
                    if (arrow?.arrow != null)
                        UnityEngine.Object.Destroy(arrow.arrow);
            }
            arrows = new List<Arrow>();
        }
    }

        [HarmonyPatch]
    public static class Kataomoi
    {
        public static PlayerControl kataomoi;
        public static Color color = new Color32(199, 21, 133, byte.MaxValue);

        public static float stareCooldown = 30f;
        public static float stareDuration = 3f;
        public static int stareCount = 1;
        public static int stareCountMax = 1;
        public static float stalkingCooldown = 30f;
        public static float stalkingDuration = 5f;
        public static float stalkingFadeTime = 0.5f;
        public static float searchCooldown = 30f;
        public static float searchDuration = 5f;
        public static bool isSearch = false;
        public static float stalkingTimer = 0f;
        public static float stalkingEffectTimer = 0f;
        public static bool triggerKataomoiWin = false;
        public static PlayerControl target = null;
        public static PlayerControl currentTarget = null;
        public static TMPro.TextMeshPro stareText = null;
        public static SpriteRenderer[] gaugeRenderer = new SpriteRenderer[3];
        public static Arrow arrow;
        public static float gaugeTimer = 0.0f;
        public static float baseGauge = 0f;

        static bool _isStalking = false;

        static Sprite stareSprite;
        public static Sprite getStareSprite() {
            if (stareSprite) return stareSprite;
            stareSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.KataomoiStareButton.png", 115f);
            return stareSprite;
        }

        static Sprite loveSprite;
        public static Sprite getLoveSprite() {
            if (loveSprite) return loveSprite;
            loveSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.KataomoiLoveButton.png", 115f);
            return loveSprite;
        }

        static Sprite searchSprite;
        public static Sprite getSearchSprite() {
            if (searchSprite) return searchSprite;
            searchSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.KataomoiSearchButton.png", 115f);
            return searchSprite;
        }

        static Sprite stalkingSprite;
        public static Sprite getStalkingSprite() {
            if (stalkingSprite) return stalkingSprite;
            stalkingSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.KataomoiStalkingButton.png", 115f);
            return stalkingSprite;
        }

        static Sprite[] loveGaugeSprites = new Sprite[3];
        public static Sprite getLoveGaugeSprite(int index) {
            if (index < 0 || index >= loveGaugeSprites.Length) return null;
            if (loveGaugeSprites[index]) return loveGaugeSprites[index];

            int id = 0;
            switch (index) {
                case 0: id = 1; break;
                case 1: id = 2; break;
                case 2: id = 11; break;
            }
            loveGaugeSprites[index] = Helpers.loadSpriteFromResources(String.Format("TheOtherRoles.Resources.KataomoiGauge_{0:d2}.png", id), 115f);
            return loveGaugeSprites[index];
        }

        public static void doStare() {
            baseGauge = getLoveGauge();
            gaugeTimer = 1.0f;
            stareCount = Mathf.Max(0, stareCount - 1);

            if (gaugeRenderer[2] != null && stareCount == 0) {
                gaugeRenderer[2].color = color;
            }
            if (Constants.ShouldPlaySfx()) SoundManager.Instance.PlaySound(DestroyableSingleton<HudManager>.Instance.TaskCompleteSound, false, 0.8f);
        }

        public static void doStalking() {
            if (kataomoi == null) return;
            stalkingTimer = stalkingDuration;
            _isStalking = true;
        }

        public static void resetStalking() {
            if (kataomoi == null) return;
            _isStalking = false;
            setkataomoiAlpha(1.0f);
        }

        public static bool isStalking(PlayerControl player) {
            if (player == null || player != kataomoi) return false;
            return _isStalking && stalkingTimer > 0;
        }

        public static bool isStalking() {
            return isStalking(kataomoi);
        }

        public static void doSearch() {
            if (kataomoi == null) return;
            isSearch = true;
        }

        public static void resetSearch() {
            if (kataomoi == null) return;
            isSearch = false;
        }

        public static bool canLove() {
            return stareCount <= 0;
        }

        public static float getLoveGauge() {
            return 1.0f - (stareCountMax == 0 ? 0f : (float)stareCount / (float)stareCountMax);
        }

        public static void clearAndReload() {
            resetStalking();

            kataomoi = null;
            stareCooldown = CustomOptionHolder.kataomoiStareCooldown.getFloat();
            stareDuration = CustomOptionHolder.kataomoiStareDuration.getFloat();
            stareCount = stareCountMax = (int)CustomOptionHolder.kataomoiStareCount.getFloat();
            stalkingCooldown = CustomOptionHolder.kataomoiStalkingCooldown.getFloat();
            stalkingDuration = CustomOptionHolder.kataomoiStalkingDuration.getFloat();
            stalkingFadeTime = CustomOptionHolder.kataomoiStalkingFadeTime.getFloat();
            searchCooldown = CustomOptionHolder.kataomoiSearchCooldown.getFloat();
            searchDuration = CustomOptionHolder.kataomoiSearchDuration.getFloat();
            isSearch = false;
            stalkingTimer = 0f;
            stalkingEffectTimer = 0f;
            triggerKataomoiWin = false;
            target = null;
            currentTarget = null;
            if (stareText != null && stareText.gameObject != null) UnityEngine.Object.Destroy(stareText.gameObject);
            stareText = null;
            if (arrow != null && arrow.arrow != null) UnityEngine.Object.Destroy(arrow.arrow);
            for (int i = 0; i < gaugeRenderer.Length; ++i) {
                if (gaugeRenderer[i] != null) {
                    UnityEngine.Object.Destroy(gaugeRenderer[i].gameObject);
                    gaugeRenderer[i] = null;
                }
            }
            arrow = null;
            gaugeTimer = 0.0f;
            baseGauge = 0.0f;
        }

        public static void FixedUpdate(PlayerPhysics __instance) {
            if (kataomoi == null) return;
            if (kataomoi != __instance.myPlayer) return;

            if (gaugeRenderer[1] != null && gaugeTimer > 0.0f)
            {
                gaugeTimer = Mathf.Max(gaugeTimer - Time.fixedDeltaTime, 0.0f);
                float gauge = getLoveGauge();
                float nowGauge = Mathf.Lerp(baseGauge, gauge, 1.0f - gaugeTimer);
                gaugeRenderer[1].transform.localPosition = new Vector3(Mathf.Lerp(-3.470784f - 1.121919f, -3.470784f, nowGauge), -2.626999f, -8.1f);
                gaugeRenderer[1].transform.localScale = new Vector3(nowGauge, 1, 1);
            }

            if (kataomoi.isDead()) return;
            if (_isStalking && stalkingTimer > 0)
            {
                kataomoi.cosmetics.currentBodySprite.BodySprite.material.SetFloat("_Outline", 0f);
                stalkingTimer = Mathf.Max(0f, stalkingTimer - Time.fixedDeltaTime);
                if (stalkingFadeTime > 0)
                {
                    float elapsedTime = stalkingDuration - stalkingTimer;
                    float alpha = Mathf.Min(elapsedTime, stalkingFadeTime) / stalkingFadeTime;
                    alpha = Mathf.Clamp(1f - alpha, CachedPlayer.LocalPlayer.PlayerControl == kataomoi || CachedPlayer.LocalPlayer.PlayerControl.isDead() ? 0.1f : 0f, 1f);
                    setkataomoiAlpha(alpha);
                }
                else
                {
                    setkataomoiAlpha(CachedPlayer.LocalPlayer.PlayerControl == kataomoi ? 0.1f : 0f);
                }

                if (stalkingTimer <= 0f)
                {
                    _isStalking = false;
                    stalkingEffectTimer = stalkingFadeTime;
                }
            }
            else if (!_isStalking && stalkingEffectTimer > 0)
            {
                stalkingEffectTimer = Mathf.Max(0f, stalkingEffectTimer - Time.fixedDeltaTime);
                if (stalkingFadeTime > 0)
                {
                    float elapsedTime = stalkingFadeTime - stalkingEffectTimer;
                    float alpha = Mathf.Min(elapsedTime, stalkingFadeTime) / stalkingFadeTime;
                    alpha = Mathf.Clamp(alpha, CachedPlayer.LocalPlayer.PlayerControl == kataomoi || CachedPlayer.LocalPlayer.PlayerControl.isDead() ? 0.1f : 0f, 1f);
                    setkataomoiAlpha(alpha);
                }
                else
                {
                    setkataomoiAlpha(1.0f);
                }
            }
            else
            {
                setkataomoiAlpha(1.0f);
            }
        }

        static void setkataomoiAlpha(float alpha) {
            if (kataomoi == null) return;
            var color = Color.Lerp(Palette.ClearWhite, Palette.White, alpha);
            try {
                if (kataomoi.cosmetics?.currentBodySprite?.BodySprite != null)
                    kataomoi.cosmetics.currentBodySprite.BodySprite.color = color;

                if (kataomoi.cosmetics?.skin?.layer != null)
                    kataomoi.cosmetics.skin.layer.color = color;

                if (kataomoi.cosmetics.hat != null)
                    kataomoi.cosmetics.hat.SetMaterialColor(color.ToInteger(true));

                //if (kataomoi.cosmetics.currentPet?.rend != null)
                 //   kataomoi.cosmetics.currentPet.rend.color = color;

                //if (kataomoi.cosmetics.currentPet?.shadowRend != null)
                //    kataomoi.cosmetics.currentPet.shadowRend.color = color;

                foreach (var rend in kataomoi.cosmetics.currentPet.renderers)
                    rend.color = color;

                foreach (var shadowRend in kataomoi.cosmetics.currentPet.shadows)
                    shadowRend.color = color;

                if (kataomoi.cosmetics.visor != null)
                    kataomoi.cosmetics.visor.Alpha = alpha;

                kataomoi.SetHatAndVisorAlpha(alpha);

            } catch { }
        }
    }




    // Modifier
    public static class Disperser {
        public static PlayerControl disperser;
        public static Color color = new Color32(48, 21, 89, byte.MaxValue);

        public static float cooldown = 30f;
        public static int remainingDisperses = 1;
        public static bool dispersesToVent;
        private static Sprite buttonSprite;

        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.Disperse.png", 115f);
            return buttonSprite;
        }

        public static void clearAndReload() {
            disperser = null;
            cooldown = CustomOptionHolder.modifierDisperserCooldown.getFloat();
            remainingDisperses = CustomOptionHolder.modifierDisperserNumberOfUses.getSelection() + 1;
            dispersesToVent = CustomOptionHolder.modifierDisperserDispersesToVent.getBool(); 
        }
    }



    public static class Bait {
        public static List<PlayerControl> bait = new List<PlayerControl>();
        public static Dictionary<DeadPlayer, float> active = new Dictionary<DeadPlayer, float>();
        public static Color color = new Color32(0, 247, 255, byte.MaxValue);

        public static float reportDelayMin = 0f;
        public static float reportDelayMax = 0f;
        public static bool showKillFlash = true;

        public static void clearAndReload() {
            bait = new List<PlayerControl>();
            active = new Dictionary<DeadPlayer, float>();
            reportDelayMin = CustomOptionHolder.modifierBaitReportDelayMin.getFloat();
            reportDelayMax = CustomOptionHolder.modifierBaitReportDelayMax.getFloat();
            if (reportDelayMin > reportDelayMax) reportDelayMin = reportDelayMax;
            showKillFlash = CustomOptionHolder.modifierBaitShowKillFlash.getBool();
        }
    }

    public static class Bloody {
        public static List<PlayerControl> bloody = new List<PlayerControl>();
        public static Dictionary<byte, float> active = new Dictionary<byte, float>();
        public static Dictionary<byte, byte> bloodyKillerMap = new Dictionary<byte, byte>();

        public static float duration = 5f;

        public static void clearAndReload() {
            bloody = new List<PlayerControl>();
            active = new Dictionary<byte, float>();
            bloodyKillerMap = new Dictionary<byte, byte>();
            duration = CustomOptionHolder.modifierBloodyDuration.getFloat();
        }
    }

    public static class AntiTeleport {
        public static List<PlayerControl> antiTeleport = new List<PlayerControl>();
        public static Vector3 position;

        public static void clearAndReload() {
            antiTeleport = new List<PlayerControl>();
            position = Vector3.zero;
        }

        public static void setPosition() {
            if (position == Vector3.zero) return;  // Check if this has been set, otherwise first spawn on submerged will fail
            if (antiTeleport.FindAll(x => x.PlayerId == CachedPlayer.LocalPlayer.PlayerId).Count > 0) {
                CachedPlayer.LocalPlayer.NetTransform.RpcSnapTo(position);
                if (SubmergedCompatibility.IsSubmerged) {
                    SubmergedCompatibility.ChangeFloor(position.y > -7);
                }
            }
        }
    }

    public static class Tiebreaker {
        public static PlayerControl tiebreaker;

        public static bool isTiebreak = false;

        public static void clearAndReload() {
            tiebreaker = null;
            isTiebreak = false;
        }
    }
    
    public static class Indomitable {
        public static PlayerControl indomitable;
        public static Color color = new Color32(48, 21, 89, byte.MaxValue);


        public static void clearAndReload() {
            indomitable = null;
        }
    }
    
    public static class AntiReport
    {
        public static List<PlayerControl> antiReport = new List<PlayerControl>();

        public static void clearAndReload()
        {
            antiReport = new List<PlayerControl>();
        }
    }




    public static class Cursed {
        public static PlayerControl cursed;
        public static Color crewColor = new Color32(0, 247, 255, byte.MaxValue);
        public static Color impColor = Palette.ImpostorRed;
        public static Color color = crewColor;
        public static void clearAndReload() {
            cursed = null;
        }
    }
    
    public static class Slueth {
        public static PlayerControl slueth;
        public static Color color = new Color32(48, 21, 89, byte.MaxValue);
        public static List<PlayerControl> reported = new List<PlayerControl>();

        public static void clearAndReload() {
            slueth = null;
            reported = new List<PlayerControl>();
        }
    }
    
     public static class Radar {
        public static PlayerControl radar;
        public static List<Arrow> localArrows = new List<Arrow>();
        public static PlayerControl ClosestPlayer;
        public static Color color = new Color32(208, 0, 93, byte.MaxValue);
        public static bool showArrows = true;
        


        public static void clearAndReload() {
            radar = null;
            showArrows = true;
            if (localArrows != null) {
                foreach (Arrow arrow in localArrows)
                    if (arrow?.arrow != null)
                        UnityEngine.Object.Destroy(arrow.arrow);
            }
            localArrows = new List<Arrow>();
        }
    }


    public static class AllKnowing {

        public static Dictionary<PlayerControl, Arrow> AliveCrew = new();

        public static List<PlayerControl> Allknowers = new();

        public static void clearAndReload() {
            AliveCrew.Clear();
            Allknowers.Clear();
        }
    }

    public static class Viewer
    {
        public static PlayerControl viewer;
        public static readonly Dictionary<byte, Arrow> Arrows = new();
        private static bool _revealImpostors;
        
        public static void clearAndReload() {
            viewer = null;
            _revealImpostors = true; // First round with impostor arrow pointing
            foreach (var a in Arrows)
            {
                if (!a.Value.arrow) continue;
                UnityEngine.Object.Destroy(a.Value.arrow);
            }
            Arrows.Clear();
        }

        public static void ToggleArrowMode()
        {
            _revealImpostors = !_revealImpostors;
        }

        public static void UpdateArrows()
        {
            if (viewer != CachedPlayer.LocalPlayer.PlayerControl) return;
            var localPlayerId = CachedPlayer.LocalPlayer.PlayerId;
            var remainingKeys = Arrows.Keys.ToList();
            var shouldBeActive = AmongUsClient.Instance && AmongUsClient.Instance.IsGameStarted && !MeetingHud.Instance;
            foreach (var p in CachedPlayer.AllPlayers)
            {
                if (p.Data == null || !p.PlayerControl || p.Data.IsDead || p.PlayerId == localPlayerId) continue;
                remainingKeys.Remove(p.PlayerId);
                if (!Arrows.TryGetValue(p.PlayerId, out var arrow))
                {
                    arrow = Arrows[p.PlayerId] = new Arrow(Color.white);
                }

                var isActive = shouldBeActive && ((_revealImpostors && p.Data.Role.IsImpostor) || (!_revealImpostors && !p.Data.Role.IsImpostor));
                arrow.arrow.SetActive(isActive);
                if (isActive)
                {
                    arrow.Update(p.PlayerControl.transform.position, _revealImpostors ? Palette.ImpostorRed : Color.white);
                }
            }

            foreach (var k in remainingKeys)
            {
                if (Arrows[k].arrow)
                {
                    UnityEngine.Object.Destroy(Arrows[k].arrow);
                }
                Arrows.Remove(k);
            }
        }
    }



	
    public static class Paranoid {
        public static PlayerControl paranoid;
        public static Color color = new Color32(48, 21, 89, byte.MaxValue);
        public static void clearAndReload() {
            paranoid = null;
        }
    }

    public static class Sunglasses {
        public static List<PlayerControl> sunglasses = new List<PlayerControl>();
        public static int vision = 1;

        public static void clearAndReload() {
            sunglasses = new List<PlayerControl>();
            vision = CustomOptionHolder.modifierSunglassesVision.getSelection() + 1;
        }
    }
    public static class Mini {
        public static PlayerControl mini;
        public static Color color = Color.yellow;
        public const float defaultColliderRadius = 0.2233912f;
        public const float defaultColliderOffset = 0.3636057f;

        public static float growingUpDuration = 400f;
        public static bool isGrowingUpInMeeting = true;
        public static DateTime timeOfGrowthStart = DateTime.UtcNow;
        public static DateTime timeOfMeetingStart = DateTime.UtcNow;
        public static float ageOnMeetingStart = 0f;
        public static bool triggerMiniLose = false;

        public static void clearAndReload() {
            mini = null;
            triggerMiniLose = false;
            growingUpDuration = CustomOptionHolder.modifierMiniGrowingUpDuration.getFloat();
            isGrowingUpInMeeting = CustomOptionHolder.modifierMiniGrowingUpInMeeting.getBool();
            timeOfGrowthStart = DateTime.UtcNow;
        }

        public static float growingProgress() {
            float timeSinceStart = (float)(DateTime.UtcNow - timeOfGrowthStart).TotalMilliseconds;
            return Mathf.Clamp(timeSinceStart / (growingUpDuration * 1000), 0f, 1f);
        }

        public static bool isGrownUp() {
            return growingProgress() == 1f;
        }

    }
    public static class Vip {
        public static List<PlayerControl> vip = new List<PlayerControl>();
        public static bool showColor = true;

        public static void clearAndReload() {
            vip = new List<PlayerControl>();
            showColor = CustomOptionHolder.modifierVipShowColor.getBool();
        }
    }

    public static class Invert {
        public static List<PlayerControl> invert = new List<PlayerControl>();
        public static int meetings = 3;

        public static void clearAndReload() {
            invert = new List<PlayerControl>();
            meetings = (int) CustomOptionHolder.modifierInvertDuration.getFloat();
        }
    }

    public static class Chameleon {
        public static List<PlayerControl> chameleon = new List<PlayerControl>();
        public static float minVisibility = 0.2f;
        public static float holdDuration = 1f;
        public static float fadeDuration = 0.5f;
        public static Dictionary<byte, float> lastMoved;

        public static void clearAndReload() {
            chameleon = new List<PlayerControl>();
            lastMoved = new Dictionary<byte, float>();
            holdDuration = CustomOptionHolder.modifierChameleonHoldDuration.getFloat();
            fadeDuration = CustomOptionHolder.modifierChameleonFadeDuration.getFloat();
            minVisibility = CustomOptionHolder.modifierChameleonMinVisibility.getSelection() / 10f;
        }

        public static float visibility(byte playerId) {
            float visibility = 1f;
            if (lastMoved != null && lastMoved.ContainsKey(playerId)) {
                var tStill = Time.time - lastMoved[playerId];
                if (tStill > holdDuration) {
                    if (tStill - holdDuration > fadeDuration) visibility = minVisibility;
                    else visibility = (1 - (tStill - holdDuration) / fadeDuration) * (1 - minVisibility) + minVisibility;
                }
            }
            if (PlayerControl.LocalPlayer.Data.IsDead && visibility < 0.1f) {  // Ghosts can always see!
                visibility = 0.1f;
            }
            return visibility;
        }

        public static void update() {
            foreach (var chameleonPlayer in chameleon) {
                if (chameleonPlayer == Ninja.ninja && Ninja.isInvisble) continue;  // Dont make Ninja visible...
                // check movement by animation
                PlayerPhysics playerPhysics = chameleonPlayer.MyPhysics;
                var currentPhysicsAnim = playerPhysics.Animations.Animator.GetCurrentAnimation();
                if (currentPhysicsAnim != playerPhysics.Animations.group.IdleAnim) {
                    lastMoved[chameleonPlayer.PlayerId] = Time.time;
                }
                // calculate and set visibility
                float visibility = Chameleon.visibility(chameleonPlayer.PlayerId);
                float petVisibility = visibility;
                if (chameleonPlayer.Data.IsDead) {
                    visibility = 0.5f;
                    petVisibility = 1f;
                }

                try {  // Sometimes renderers are missing for weird reasons. Try catch to avoid exceptions
                    chameleonPlayer.cosmetics.currentBodySprite.BodySprite.color = chameleonPlayer.cosmetics.currentBodySprite.BodySprite.color.SetAlpha(visibility);
                    if (DataManager.Settings.Accessibility.ColorBlindMode) chameleonPlayer.cosmetics.colorBlindText.color = chameleonPlayer.cosmetics.colorBlindText.color.SetAlpha(visibility);
                    chameleonPlayer.SetHatAndVisorAlpha(visibility);
                    chameleonPlayer.cosmetics.skin.layer.color = chameleonPlayer.cosmetics.skin.layer.color.SetAlpha(visibility);
                    chameleonPlayer.cosmetics.nameText.color = chameleonPlayer.cosmetics.nameText.color.SetAlpha(visibility);
                    foreach (var rend in chameleonPlayer.cosmetics.currentPet.renderers)
                        rend.color = rend.color.SetAlpha(petVisibility);
                    foreach (var shadowRend in chameleonPlayer.cosmetics.currentPet.shadows)
                        shadowRend.color = shadowRend.color.SetAlpha(petVisibility);
                } catch { }
            }
                
        }
    }

    public static class Shifter {
        public static PlayerControl shifter;

        public static PlayerControl futureShift;
        public static PlayerControl currentTarget;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.ShiftButton.png", 115f);
            return buttonSprite;
        }

        public static void shiftRole (PlayerControl player1, PlayerControl player2, bool repeat = true) {
            if (Mayor.mayor != null && Mayor.mayor == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Mayor.mayor = player1;
            } else if (Portalmaker.portalmaker != null && Portalmaker.portalmaker == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Portalmaker.portalmaker = player1;
            } else if (Engineer.engineer != null && Engineer.engineer == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Engineer.engineer = player1;
            } else if (Sheriff.sheriff != null && Sheriff.sheriff == player2) {
                if (repeat) shiftRole(player2, player1, false);
                if (Sheriff.formerDeputy != null && Sheriff.formerDeputy == Sheriff.sheriff) Sheriff.formerDeputy = player1;  // Shifter also shifts info on promoted deputy (to get handcuffs)
                Sheriff.sheriff = player1;
            } else if (Deputy.deputy != null && Deputy.deputy == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Deputy.deputy = player1;
            } else if (Lighter.lighter != null && Lighter.lighter == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Lighter.lighter = player1;
            } else if (Detective.detective != null && Detective.detective == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Detective.detective = player1;
            } else if (TimeMaster.timeMaster != null && TimeMaster.timeMaster == player2) {
                if (repeat) shiftRole(player2, player1, false);
                TimeMaster.timeMaster = player1;
            }  else if (Medic.medic != null && Medic.medic == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Medic.medic = player1;
            }  else if (Veteren.veteren != null && Veteren.veteren == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Veteren.veteren = player1;
            }  else if (BodyGuard.bodyguard != null && BodyGuard.bodyguard == player2) {
                if (repeat) shiftRole(player2, player1, false);
                BodyGuard.bodyguard = player1;
            }  else if (Transporter.transporter != null && Transporter.transporter == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Transporter.transporter = player1;
            }  else if (Teleporter.teleporter != null && Teleporter.teleporter == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Teleporter.teleporter = player1;
            }  else if (Arbiter.arbiter != null && Arbiter.arbiter == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Arbiter.arbiter = player1;
            } else if (Swapper.swapper != null && Swapper.swapper == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Swapper.swapper = player1;
            } else if (Seer.seer != null && Seer.seer == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Seer.seer = player1;
            } else if (Hacker.hacker != null && Hacker.hacker == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Hacker.hacker = player1;
            } else if (Tracker.tracker != null && Tracker.tracker == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Tracker.tracker = player1;
            } else if (Snitch.snitch != null && Snitch.snitch == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Snitch.snitch = player1;
            } else if (Spy.spy != null && Spy.spy == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Spy.spy = player1;
            } else if (SecurityGuard.securityGuard != null && SecurityGuard.securityGuard == player2) {
                if (repeat) shiftRole(player2, player1, false);
                SecurityGuard.securityGuard = player1;
            } else if (Guesser.niceGuesser != null && Guesser.niceGuesser == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Guesser.niceGuesser = player1;
            } else if (Medium.medium != null && Medium.medium == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Medium.medium = player1;
            } else if (Pursuer.pursuer != null && Pursuer.pursuer == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Pursuer.pursuer = player1;
            } else if (Trapper.trapper != null && Trapper.trapper == player2) {
                if (repeat) shiftRole(player2, player1, false);
                Trapper.trapper = player1;
            }
        }

        public static void clearAndReload() {
            shifter = null;
            currentTarget = null;
            futureShift = null;
        }
    }

     public static class Giant {
            public static List<PlayerControl> giant = new List<PlayerControl>();

            public static Color color = Color.yellow;

            public static float speed;

            public static float colliderOffset = 05f;
            public static float colliderRadius = 1.7f; 
            public static float scale = 1.5f;

            public static Vector3 Scale = new Vector3(1.5f, 1.5f, 1.5f);

            public static void clearAndReload() {
                giant = new List<PlayerControl>();
                scale = CustomOptionHolder.modifierGiantSize.getFloat();
                Scale = new Vector3(scale,scale, scale);
                speed = CustomOptionHolder.modifierGiantSpeed.getFloat();
            }

        }
        public static class Flash {
            public static List<PlayerControl> flash = new List<PlayerControl>();

            public static Color color = Color.yellow;

            public static float speed = 1.8f;

            public static void clearAndReload() {
                flash = new List<PlayerControl>();
                speed = CustomOptionHolder.modifierFlashSpeed.getFloat();
            }

        }
        public static class OneTimeKiller {
            public static PlayerControl onetimekiller;
            public static Color color = Palette.Orange;
            public static PlayerControl currentTarget;
            public static bool hasKilled = false;

            public static void clearAndReload() {
                onetimekiller = null;
                currentTarget = null;
                hasKilled = false;
            }
        }

        public static class Recruiter {
            public static List<PlayerControl> recruiter = new List<PlayerControl>();

            public static Color color = new Color32(150, 0, 0, byte.MaxValue);

            public static PlayerControl FutureRecruited;
            public static PlayerControl currentTarget;

            public static Sprite recruitSprite = null;

            public static Sprite getRecruitSprite() {
                if (recruitSprite) return recruitSprite;
                recruitSprite = Helpers.loadSpriteFromResources("TheOtherRoles.Resources.TargetIcon.png", 100);
                return recruitSprite;
            }

            public static void clearAndReload() {
                currentTarget = null;
                FutureRecruited = null;
                recruiter = new List<PlayerControl>();
                recruitSprite = null;
            }

        }


}
