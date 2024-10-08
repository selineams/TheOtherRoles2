using System.Collections.Generic;
using UnityEngine;
using static TheOtherRoles.TheOtherRoles;
using Types = TheOtherRoles.CustomOption.CustomOptionType;

namespace TheOtherRoles {
    public class CustomOptionHolder {
        public static string[] rates = new string[]{"0%", "10%", "20%", "30%", "40%", "50%", "60%", "70%", "80%", "90%", "100%"};
        public static string[] ratesModifier = new string[]{"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15" };
        public static string[] presets = new string[]{"预设 1", "预设 2", "Random Preset Skeld", "Random Preset Mira HQ", "Random Preset Polus", "Random Preset Airship", "Random Preset Submerged" };

        public static CustomOption presetSelection;
        public static CustomOption activateRoles;
        public static CustomOption crewmateRolesCountMin;
        public static CustomOption crewmateRolesCountMax;
        public static CustomOption crewmateRolesFill;
        public static CustomOption neutralRolesCountMin;
        public static CustomOption neutralRolesCountMax;
        public static CustomOption impostorRolesCountMin;
        public static CustomOption impostorRolesCountMax;
        public static CustomOption modifiersCountMin;
        public static CustomOption modifiersCountMax;

        public static CustomOption anyPlayerCanStopStart;
        public static CustomOption enableEventMode;
        public static CustomOption deadImpsBlockSabotage;

		public static CustomOption swooperSpawnRate;
        public static CustomOption swooperCooldown;
        public static CustomOption swooperDuration;
        public static CustomOption swooperHasImpVision;
		
        public static CustomOption mafiaSpawnRate;
        public static CustomOption janitorCooldown;

        public static CustomOption morphlingSpawnRate;
        public static CustomOption morphlingCooldown;
        public static CustomOption morphlingDuration;
        
    
        public static CustomOption camouflagerSpawnRate;
        public static CustomOption camouflagerCooldown;
        public static CustomOption camouflagerDuration;

        public static CustomOption vampireSpawnRate;
        public static CustomOption vampireKillDelay;
        public static CustomOption vampireCooldown;
        public static CustomOption vampireCanKillNearGarlics;
        public static CustomOption vampireGarlicButton;

        public static CustomOption poucherSpawnRate;
        public static CustomOption mimicSpawnRate;

        public static CustomOption escapistSpawnRate;
        public static CustomOption escapistEscapeTime;
        public static CustomOption escapistChargesOnPlace;
        public static CustomOption escapistResetPlaceAfterMeeting;
     //   public static CustomOption jumperChargesGainOnMeeting;
        public static CustomOption escapistMaxCharges;

        public static CustomOption pitfallerSpawnRate;
        public static CustomOption pitfallerCooldown;

        public static CustomOption shinobiSpawnRate;
        public static CustomOption shinobiCooldown;
        public static CustomOption shinobiDuration;
        public static CustomOption shinobiKillDistance;
        
        public static CustomOption snarerSpawnRate;
        public static CustomOption snarerNumSnare;
        public static CustomOption snarerKillTimer;
        public static CustomOption snarerCooldown;
        public static CustomOption snarerMaxDistance;
        public static CustomOption snarerSnareRange;
        public static CustomOption snarerExtensionTime;
        public static CustomOption snarerPenaltyTime;
        public static CustomOption snarerBonusTime;

        public static CustomOption minerSpawnRate;
		public static CustomOption minerCooldown;

        public static CustomOption eraserSpawnRate;
        public static CustomOption eraserCooldown;
        public static CustomOption eraserCanEraseAnyone;
        public static CustomOption guesserSpawnRate;
        public static CustomOption guesserIsImpGuesserRate;
        public static CustomOption guesserNumberOfShots;
        public static CustomOption guesserHasMultipleShotsPerMeeting;
        public static CustomOption guesserKillsThroughShield;
        public static CustomOption guesserEvilCanKillSpy;
        public static CustomOption guesserSpawnBothRate;
        public static CustomOption guesserCantGuessSnitchIfTaksDone;

        public static CustomOption jesterSpawnRate;
        public static CustomOption jesterCanCallEmergency;
        public static CustomOption jesterCanVent;
        public static CustomOption jesterHasImpostorVision;
			
        public static CustomOption amnisiacSpawnRate;
        public static CustomOption amnisiacShowArrows;
        public static CustomOption amnisiacResetRole;

        public static CustomOption arsonistSpawnRate;
        public static CustomOption arsonistCooldown;
        public static CustomOption arsonistDuration;

        public static CustomOption jackalSpawnRate;
        public static CustomOption jackalKillCooldown;
        public static CustomOption jackalCreateSidekickCooldown;
        public static CustomOption jackalCanSabotageLights;
        public static CustomOption jackalCanUseVents;
        public static CustomOption jackalCanCreateSidekick;
        public static CustomOption sidekickPromotesToJackal;
        public static CustomOption sidekickCanKill;
        public static CustomOption sidekickCanUseVents;
        public static CustomOption sidekickCanSabotageLights;
        public static CustomOption jackalPromotedFromSidekickCanCreateSidekick;
        public static CustomOption jackalCanCreateSidekickFromImpostor;
        public static CustomOption jackalAndSidekickHaveImpostorVision;

        public static CustomOption bountyHunterSpawnRate;
        public static CustomOption bountyHunterBountyDuration;
        public static CustomOption bountyHunterReducedCooldown;
        public static CustomOption bountyHunterPunishmentTime;
        public static CustomOption bountyHunterShowArrow;
        public static CustomOption bountyHunterArrowUpdateIntervall;

        public static CustomOption witchSpawnRate;
        public static CustomOption witchCooldown;
        public static CustomOption witchAdditionalCooldown;
        public static CustomOption witchCanSpellAnyone;
        public static CustomOption witchSpellCastingDuration;
        public static CustomOption witchTriggerBothCooldowns;
        public static CustomOption witchVoteSavesTargets;

        public static CustomOption ninjaSpawnRate;
        public static CustomOption ninjaCooldown;
        public static CustomOption ninjaKnowsTargetLocation;
        public static CustomOption ninjaTraceTime;
        public static CustomOption ninjaTraceColorTime;
        public static CustomOption ninjaInvisibleDuration;

        public static CustomOption mayorSpawnRate;
        public static CustomOption mayorCanSeeVoteColors;
        public static CustomOption mayorTasksNeededToSeeVoteColors;
        public static CustomOption mayorMeetingButton;
        public static CustomOption mayorMaxRemoteMeetings;
        public static CustomOption mayorChooseSingleVote;

        public static CustomOption portalmakerSpawnRate;
        public static CustomOption portalmakerCooldown;
        public static CustomOption portalmakerUsePortalCooldown;
        public static CustomOption portalmakerLogOnlyColorType;
        public static CustomOption portalmakerLogHasTime;
        public static CustomOption portalmakerCanPortalFromAnywhere;

        public static CustomOption engineerSpawnRate;
        public static CustomOption engineerNumberOfFixes;
        public static CustomOption engineerHighlightForImpostors;
        public static CustomOption engineerHighlightForTeamJackal;

        public static CustomOption sheriffSpawnRate;
        public static CustomOption sheriffCooldown;
        public static CustomOption sheriffCanKillNeutrals;
        public static CustomOption deputySpawnRate;

        public static CustomOption deputyNumberOfHandcuffs;
        public static CustomOption deputyHandcuffCooldown;
        public static CustomOption deputyGetsPromoted;
        public static CustomOption deputyKeepsHandcuffs;
        public static CustomOption deputyHandcuffDuration;
        public static CustomOption deputyKnowsSheriff;

        public static CustomOption lighterSpawnRate;
        public static CustomOption lighterModeLightsOnVision;
        public static CustomOption lighterModeLightsOffVision;
        public static CustomOption lighterFlashlightWidth;

        public static CustomOption detectiveSpawnRate;
        public static CustomOption detectiveAnonymousFootprints;
        public static CustomOption detectiveFootprintIntervall;
        public static CustomOption detectiveFootprintDuration;
        public static CustomOption detectiveReportNameDuration;
        public static CustomOption detectiveReportColorDuration;

        public static CustomOption timeMasterSpawnRate;
        public static CustomOption timeMasterCooldown;
        public static CustomOption timeMasterRewindTime;
        public static CustomOption timeMasterShieldDuration;


        public static CustomOption veterenSpawnRate;
        public static CustomOption veterenCooldown;
        public static CustomOption veterenAlertDuration;

        public static CustomOption medicSpawnRate;
        public static CustomOption medicShowShielded;
        public static CustomOption medicShowAttemptToShielded;
        public static CustomOption medicSetOrShowShieldAfterMeeting;
        public static CustomOption medicShowAttemptToMedic;
        public static CustomOption medicSetShieldAfterMeeting;

        public static CustomOption swapperSpawnRate;
        public static CustomOption swapperCanCallEmergency;
        public static CustomOption swapperCanOnlySwapOthers;
        public static CustomOption swapperSwapsNumber;
        public static CustomOption swapperRechargeTasksNumber;

        public static CustomOption seerSpawnRate;
        public static CustomOption seerMode;
        public static CustomOption seerSoulDuration;
        public static CustomOption seerLimitSoulDuration;

        public static CustomOption hackerSpawnRate;
        public static CustomOption hackerCooldown;
        public static CustomOption hackerHackeringDuration;
        public static CustomOption hackerOnlyColorType;
        public static CustomOption hackerToolsNumber;
        public static CustomOption hackerRechargeTasksNumber;
        public static CustomOption hackerNoMove;

        public static CustomOption trackerSpawnRate;
        public static CustomOption trackerUpdateIntervall;
        public static CustomOption trackerResetTargetAfterMeeting;
        public static CustomOption trackerCanTrackCorpses;
        public static CustomOption trackerCorpsesTrackingCooldown;
        public static CustomOption trackerCorpsesTrackingDuration;
        public static CustomOption trackerTrackingMethod;

        public static CustomOption snitchSpawnRate;
        public static CustomOption snitchLeftTasksForReveal;
        public static CustomOption snitchMode;
        public static CustomOption snitchTargets;

        public static CustomOption snitchSeeMeeting;


        public static CustomOption spySpawnRate;
        public static CustomOption spyCanDieToSheriff;
        public static CustomOption spyImpostorsCanKillAnyone;
        public static CustomOption spyCanEnterVents;
        public static CustomOption spyHasImpostorVision;

        public static CustomOption tricksterSpawnRate;
        public static CustomOption tricksterPlaceBoxCooldown;
        public static CustomOption tricksterLightsOutCooldown;
        public static CustomOption tricksterLightsOutDuration;

        public static CustomOption cleanerSpawnRate;
        public static CustomOption cleanerCooldown;
        
        public static CustomOption warlockSpawnRate;
        public static CustomOption warlockCooldown;
        public static CustomOption warlockRootTime;

        public static CustomOption securityGuardSpawnRate;
        public static CustomOption securityGuardCooldown;
        public static CustomOption securityGuardTotalScrews;
        public static CustomOption securityGuardCamPrice;
        public static CustomOption securityGuardVentPrice;
        public static CustomOption securityGuardCamDuration;
        public static CustomOption securityGuardCamMaxCharges;
        public static CustomOption securityGuardCamRechargeTasksNumber;
        public static CustomOption securityGuardNoMove;

        public static CustomOption bodyGuardSpawnRate;
        public static CustomOption bodyGuardFlash;
        public static CustomOption bodyGuardResetTargetAfterMeeting;

        public static CustomOption arbiterSpawnRate;
        public static CustomOption arbiterNumberOfSpecialVotes;
        public static CustomOption arbiterSpecificMessageMode;

        public static CustomOption vultureSpawnRate;
        public static CustomOption vultureCooldown;
        public static CustomOption vultureNumberToWin;
        public static CustomOption vultureCanUseVents;
        public static CustomOption vultureShowArrows;

        public static CustomOption mediumSpawnRate;
        public static CustomOption mediumCooldown;
        public static CustomOption mediumDuration;
        public static CustomOption mediumOneTimeUse;
        public static CustomOption mediumChanceAdditionalInfo;

        public static CustomOption lawyerSpawnRate;
        public static CustomOption lawyerIsProsecutorChance;
        public static CustomOption lawyerTargetCanBeJester;
        public static CustomOption lawyerVision;
        public static CustomOption lawyerKnowsRole;
        public static CustomOption lawyerCanCallEmergency;
        public static CustomOption pursuerCooldown;
        public static CustomOption pursuerBlanksNumber;

        public static CustomOption thiefSpawnRate;
        public static CustomOption thiefCooldown;
        public static CustomOption thiefHasImpVision;
        public static CustomOption thiefCanUseVents;
        public static CustomOption thiefCanKillSheriff;
        public static CustomOption thiefCanStealWithGuess;
        public static CustomOption thiefCanKillDeputy;
        public static CustomOption thiefCanKillVeteren;


        public static CustomOption trapperSpawnRate;
        public static CustomOption trapperCooldown;
        public static CustomOption trapperMaxCharges;
        public static CustomOption trapperRechargeTasksNumber;
        public static CustomOption trapperTrapNeededTriggerToReveal;
        public static CustomOption trapperAnonymousMap;
        public static CustomOption trapperInfoType;
        public static CustomOption trapperTrapDuration;

        public static CustomOption transporterSpawnRate;
        public static CustomOption transporterScanCooldown;
        public static CustomOption transporterDelaiAfterScan;
        public static CustomOption transporterAddArrow;
        public static CustomOption transporterUpdateIntervall;

        public static CustomOption teleporterSpawnRate;
        public static CustomOption teleporterCooldown;
        public static CustomOption teleporterSampleCooldown;
        public static CustomOption teleporterTeleportNumber;

        public static CustomOption transportistSpawnRate;
        public static CustomOption transportistScanCooldown;
        public static CustomOption transportistDelaiAfterScan;
        public static CustomOption transportistAddArrow;
        public static CustomOption transportistUpdateIntervall;

        public static CustomOption bomberSpawnRate;
        public static CustomOption bomberBombDestructionTime;
        public static CustomOption bomberBombDestructionRange;
        public static CustomOption bomberBombHearRange;
        public static CustomOption bomberDefuseDuration;
        public static CustomOption bomberBombCooldown;
        public static CustomOption bomberBombActiveAfter;

        public static CustomOption yoyoSpawnRate;
        public static CustomOption yoyoBlinkDuration;
        public static CustomOption yoyoMarkCooldown;
        public static CustomOption yoyoMarkStaysOverMeeting;
        public static CustomOption yoyoHasAdminTable;
        public static CustomOption yoyoAdminTableCooldown;
        public static CustomOption yoyoSilhouetteVisibility;

        public static CustomOption werewolfSpawnRate;
        public static CustomOption werewolfRampageCooldown;
        public static CustomOption werewolfRampageDuration;
        public static CustomOption werewolfKillCooldown;

        public static CustomOption juggernautSpawnRate;
        public static CustomOption juggernautCooldown;
        public static CustomOption juggernautHasImpVision;
        public static CustomOption juggernautReducedkillEach;

        
        public static CustomOption doomsayerSpawnRate;
        public static CustomOption doomsayerCooldown;
        public static CustomOption doomsayerHasMultipleShotsPerMeeting;
        public static CustomOption doomsayerShowInfoInGhostChat;
        public static CustomOption doomsayerCanGuessNeutral;
        public static CustomOption doomsayerCanGuessImpostor;
        public static CustomOption doomsayerOnlineTarger;
        public static CustomOption doomsayerGuesserCantGuessSnitch;
        public static CustomOption doomsayerKillToWin;
        public static CustomOption doomsayerDormationNum;

        public static CustomOption kataomoiSpawnRate;
        public static CustomOption kataomoiStareCooldown;
        public static CustomOption kataomoiStareDuration;
        public static CustomOption kataomoiStareCount;
        public static CustomOption kataomoiStalkingCooldown;
        public static CustomOption kataomoiStalkingDuration;
        public static CustomOption kataomoiStalkingFadeTime;
        public static CustomOption kataomoiSearchCooldown;
        public static CustomOption kataomoiSearchDuration;

        public static CustomOption akujoSpawnRate;
        public static CustomOption akujoTimeLimit;
        public static CustomOption akujoKnowsRoles;
        public static CustomOption akujoNumKeeps;

        public static CustomOption cerenovusSpawnRate;
        public static CustomOption cerenovusBrainwashTime;
        public static CustomOption cerenovusBrainwashCooldown;
        public static CustomOption cerenovusNumberToWin;

        public static CustomOption modifiersAreHidden;

        public static CustomOption modifierBait;
        public static CustomOption modifierBaitQuantity;
        public static CustomOption modifierBaitReportDelayMin;
        public static CustomOption modifierBaitReportDelayMax;
        public static CustomOption modifierBaitShowKillFlash;

        public static CustomOption modifierLover;
        public static CustomOption modifierLoverImpLoverRate;
        public static CustomOption modifierLoverBothDie;
        public static CustomOption modifierLoverEnableChat;

        public static CustomOption modifierBloody;
        public static CustomOption modifierBloodyQuantity;
        public static CustomOption modifierBloodyDuration;

        public static CustomOption modifierAntiTeleport;
        public static CustomOption modifierAntiTeleportQuantity;

        public static CustomOption modifierTieBreaker;

        public static CustomOption modifierSunglasses;
        public static CustomOption modifierSunglassesQuantity;
        public static CustomOption modifierSunglassesVision;
        
        public static CustomOption modifierMini;
        public static CustomOption modifierMiniGrowingUpDuration;
        public static CustomOption modifierMiniGrowingUpInMeeting;

		public static CustomOption modifierCursed;
        public static CustomOption modifierAntiReport;
        public static CustomOption modifierSlueth;
        public static CustomOption modifierParanoid;
        public static CustomOption modifierRadar;
        public static CustomOption modifierIndomitable;

        public static CustomOption modifierDisperser;
        public static CustomOption modifierDisperserDispersesToVent;
        public static CustomOption modifierDisperserCooldown;
        public static CustomOption modifierDisperserNumberOfUses;
        public static CustomOption modifierAllknowing;
        public static CustomOption modifierAllknowingQuantity;
        public static CustomOption modifierViewer;

        public static CustomOption modifierVip;
        public static CustomOption modifierVipQuantity;
        public static CustomOption modifierVipShowColor;

        public static CustomOption modifierInvert;
        public static CustomOption modifierInvertQuantity;
        public static CustomOption modifierInvertDuration;

        public static CustomOption modifierChameleon;
        public static CustomOption modifierChameleonQuantity;
        public static CustomOption modifierChameleonHoldDuration;
        public static CustomOption modifierChameleonFadeDuration;
        public static CustomOption modifierChameleonMinVisibility;

        public static CustomOption modifierShifter;

        //ADDED:
        public static CustomOption modifierFlash;
        public static CustomOption modifierFlashQuantity;
        public static CustomOption modifierFlashSpeed;

        //ADDED:
        public static CustomOption modifierGiant;
        public static CustomOption modifierGiantQuantity;
        public static CustomOption modifierGiantSpeed;
        public static CustomOption modifierGiantSize;

         //ADDED:

        public static CustomOption modifierRecruiter;
        public static CustomOption modifierRecruiterQuantity;

         // ADDED:
        public static CustomOption modifierOneTimeKiller;


        public static CustomOption maxNumberOfMeetings;
        public static CustomOption blockSkippingInEmergencyMeetings;
        public static CustomOption noVoteIsSelfVote;
        public static CustomOption hidePlayerNames;

        public static CustomOption blockGameEnd;
        public static CustomOption allowParallelMedBayScans;
        public static CustomOption shieldFirstKill;
        public static CustomOption finishTasksBeforeHauntingOrZoomingOut;
        public static CustomOption camsNightVision;
        public static CustomOption camsNoNightVisionIfImpVision;

        public static CustomOption impostorSeeRoles;
        public static CustomOption enableCamoComms;
        public static CustomOption randomGameStartPosition;
        public static CustomOption showButtonTarget;

        public static CustomOption dynamicMap;
        public static CustomOption dynamicMapEnableSkeld;
        public static CustomOption dynamicMapEnableMira;
        public static CustomOption dynamicMapEnablePolus;
        public static CustomOption dynamicMapEnableAirShip;
        public static CustomOption dynamicMapEnableFungle;
        public static CustomOption dynamicMapEnableSubmerged;
        public static CustomOption dynamicMapSeparateSettings;

        //Guesser Gamemode
        public static CustomOption guesserGamemodeCrewNumber;
        public static CustomOption guesserGamemodeNeutralNumber;
        public static CustomOption guesserGamemodeImpNumber;
        public static CustomOption guesserForceJackalGuesser;
        public static CustomOption guesserForceThiefGuesser;
        public static CustomOption guesserForceDoomsayerGuesser;
        public static CustomOption guesserGamemodeHaveModifier;
        public static CustomOption guesserGamemodeNumberOfShots;
        public static CustomOption guesserGamemodeHasMultipleShotsPerMeeting;
        public static CustomOption guesserGamemodeKillsThroughShield;
        public static CustomOption guesserGamemodeEvilCanKillSpy;
        public static CustomOption guesserGamemodeCantGuessSnitchIfTaksDone;
        public static CustomOption guesserGamemodeSidekickIsAlwaysGuesser;

        // Hide N Seek Gamemode
        public static CustomOption hideNSeekHunterCount;
        public static CustomOption hideNSeekKillCooldown;
        public static CustomOption hideNSeekHunterVision;
        public static CustomOption hideNSeekHuntedVision;
        public static CustomOption hideNSeekTimer;
        public static CustomOption hideNSeekCommonTasks;
        public static CustomOption hideNSeekShortTasks;
        public static CustomOption hideNSeekLongTasks;
        public static CustomOption hideNSeekTaskWin;
        public static CustomOption hideNSeekTaskPunish;
        public static CustomOption hideNSeekCanSabotage;
        public static CustomOption hideNSeekMap;
        public static CustomOption hideNSeekHunterWaiting;

        public static CustomOption hunterLightCooldown;
        public static CustomOption hunterLightDuration;
        public static CustomOption hunterLightVision;
        public static CustomOption hunterLightPunish;
        public static CustomOption hunterAdminCooldown;
        public static CustomOption hunterAdminDuration;
        public static CustomOption hunterAdminPunish;
        public static CustomOption hunterArrowCooldown;
        public static CustomOption hunterArrowDuration;
        public static CustomOption hunterArrowPunish;

        public static CustomOption huntedShieldCooldown;
        public static CustomOption huntedShieldDuration;
        public static CustomOption huntedShieldRewindTime;
        public static CustomOption huntedShieldNumber;

        // Prop Hunt Settings
        public static CustomOption propHuntMap;
        public static CustomOption propHuntTimer;
        public static CustomOption propHuntNumberOfHunters;
        public static CustomOption hunterInitialBlackoutTime;
        public static CustomOption hunterMissCooldown;
        public static CustomOption hunterHitCooldown;
        public static CustomOption hunterMaxMissesBeforeDeath;
        public static CustomOption propBecomesHunterWhenFound;
        public static CustomOption propHunterVision;
        public static CustomOption propVision;
        public static CustomOption propHuntRevealCooldown;
        public static CustomOption propHuntRevealDuration;
        public static CustomOption propHuntRevealPunish;
        public static CustomOption propHuntUnstuckCooldown;
        public static CustomOption propHuntUnstuckDuration;
        public static CustomOption propHuntInvisCooldown;
        public static CustomOption propHuntInvisDuration;
        public static CustomOption propHuntSpeedboostCooldown;
        public static CustomOption propHuntSpeedboostDuration;
        public static CustomOption propHuntSpeedboostSpeed;
        public static CustomOption propHuntSpeedboostEnabled;
        public static CustomOption propHuntInvisEnabled;
        public static CustomOption propHuntAdminCooldown;
        public static CustomOption propHuntFindCooldown;
        public static CustomOption propHuntFindDuration;

        internal static Dictionary<byte, byte[]> blockedRolePairings = new Dictionary<byte, byte[]>();

        public static string cs(Color c, string s) {
            return string.Format("<color=#{0:X2}{1:X2}{2:X2}{3:X2}>{4}</color>", ToByte(c.r), ToByte(c.g), ToByte(c.b), ToByte(c.a), s);
        }
 
        private static byte ToByte(float f) {
            f = Mathf.Clamp01(f);
            return (byte)(f * 255);
        }

        public static bool isMapSelectionOption(CustomOption option) {
            return option == CustomOptionHolder.propHuntMap && option == CustomOptionHolder.hideNSeekMap;
        }

        public static void Load() {

            CustomOption.vanillaSettings = TheOtherRolesPlugin.Instance.Config.Bind("Preset0", "VanillaOptions", "");

            // Role Options
            presetSelection = CustomOption.Create(0, Types.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "设置"), presets, null, true);
            activateRoles = CustomOption.Create(1, Types.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "开启MOD职业并关闭原版职业"), true, null, true);
            anyPlayerCanStopStart = CustomOption.Create(2, Types.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "任一玩家可终止游戏开始"), false, null, false);

            if (Utilities.EventUtility.canBeEnabled) enableEventMode = CustomOption.Create(10423, Types.General, cs(Color.green, "开启特殊模式"), true, null, true);

            // Using new id's for the options to not break compatibilty with older versions
            crewmateRolesCountMin = CustomOption.Create(300, Types.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "最小船员职业数"), 15f, 0f, 15f, 1f, null, true);
            crewmateRolesCountMax = CustomOption.Create(301, Types.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "最大船员职业数"), 15f, 0f, 15f, 1f);
            crewmateRolesFill = CustomOption.Create(308, Types.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "所有船员必定拥有职业\n(无视最大/最小职业数)"), true);
            neutralRolesCountMin = CustomOption.Create(302, Types.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "最小独立阵营职业数"), 15f, 0f, 15f, 1f);
            neutralRolesCountMax = CustomOption.Create(303, Types.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "最大独立阵营职业数"), 15f, 0f, 15f, 1f);
            impostorRolesCountMin = CustomOption.Create(304, Types.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "最小内鬼职业数"), 15f, 0f, 15f, 1f);
            impostorRolesCountMax = CustomOption.Create(305, Types.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "最大内鬼职业数"), 15f, 0f, 15f, 1f);
            modifiersCountMin = CustomOption.Create(306, Types.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "最小附加职业数"), 15f, 0f, 15f, 1f);
            modifiersCountMax = CustomOption.Create(307, Types.General, cs(new Color(204f / 255f, 204f / 255f, 0, 1f), "最大附加职业数"), 15f, 0f, 15f, 1f);

            mafiaSpawnRate = CustomOption.Create(18, Types.Impostor, cs(Janitor.color, "黑手党"), rates, null, true);
            janitorCooldown = CustomOption.Create(19, Types.Impostor, "清洁工清理冷却", 30f, 10f, 60f, 2.5f, mafiaSpawnRate);

            morphlingSpawnRate = CustomOption.Create(20, Types.Impostor, cs(Morphling.color, "化形者"), rates, null, true);
            morphlingCooldown = CustomOption.Create(21, Types.Impostor, "化形冷却", 25f, 10f, 60f, 2.5f, morphlingSpawnRate);
            morphlingDuration = CustomOption.Create(22, Types.Impostor, "化形持续时间", 15f, 1f, 20f, 0.5f, morphlingSpawnRate);

            camouflagerSpawnRate = CustomOption.Create(30, Types.Impostor, cs(Camouflager.color, "隐蔽者"), rates, null, true);
            camouflagerCooldown = CustomOption.Create(31, Types.Impostor, "隐蔽冷却", 25f, 10f, 60f, 2.5f, camouflagerSpawnRate);
            camouflagerDuration = CustomOption.Create(32, Types.Impostor, "隐蔽持续时间", 15f, 1f, 20f, 0.5f, camouflagerSpawnRate);

            vampireSpawnRate = CustomOption.Create(40, Types.Impostor, cs(Vampire.color, "吸血鬼"), rates, null, true);
            vampireKillDelay = CustomOption.Create(41, Types.Impostor, "吸血推迟的击杀时间", 6f, 1f, 20f, 1f, vampireSpawnRate);
            vampireCooldown = CustomOption.Create(42, Types.Impostor, "吸血冷却", 25f, 10f, 60f, 2.5f, vampireSpawnRate);
            vampireGarlicButton = CustomOption.Create(44, Types.Impostor, "开启大蒜按钮", false, vampireSpawnRate);
            vampireCanKillNearGarlics = CustomOption.Create(43, Types.Impostor, "吸血鬼可在大蒜附近击杀", false, vampireSpawnRate);
            

            eraserSpawnRate = CustomOption.Create(230, Types.Impostor, cs(Eraser.color, "抹除者"), rates, null, true);
            eraserCooldown = CustomOption.Create(231, Types.Impostor, "抹除冷却", 25f, 10f, 120f, 5f, eraserSpawnRate);
            eraserCanEraseAnyone = CustomOption.Create(232, Types.Impostor, "抹除者可抹除任何玩家", false, eraserSpawnRate);

            tricksterSpawnRate = CustomOption.Create(250, Types.Impostor, cs(Trickster.color, "骗术师"), rates, null, true);
            tricksterPlaceBoxCooldown = CustomOption.Create(251, Types.Impostor, "骗术盒冷却", 5f, 2.5f, 30f, 2.5f, tricksterSpawnRate);
            tricksterLightsOutCooldown = CustomOption.Create(252, Types.Impostor, "骗术师熄灯冷却", 20f, 10f, 60f, 5f, tricksterSpawnRate);
            tricksterLightsOutDuration = CustomOption.Create(253, Types.Impostor, "骗术师熄灯持续时间", 15f, 5f, 60f, 2.5f, tricksterSpawnRate);

            cleanerSpawnRate = CustomOption.Create(260, Types.Impostor, cs(Cleaner.color, "清理者"), rates, null, true);
            cleanerCooldown = CustomOption.Create(261, Types.Impostor, "清理冷却", 0f, 0f, 60f, 2.5f, cleanerSpawnRate);

            warlockSpawnRate = CustomOption.Create(270, Types.Impostor, cs(Cleaner.color, "术士"), rates, null, true);
            warlockCooldown = CustomOption.Create(271, Types.Impostor, "诅咒冷却", 25f, 10f, 60f, 2.5f, warlockSpawnRate);
            warlockRootTime = CustomOption.Create(272, Types.Impostor, "咒杀后术士定身持续时间", 5f, 0f, 15f, 1f, warlockSpawnRate);

            bountyHunterSpawnRate = CustomOption.Create(320, Types.Impostor, cs(BountyHunter.color, "赏金猎人"), rates, null, true);
            bountyHunterBountyDuration = CustomOption.Create(321, Types.Impostor, "悬赏目标更换时间间隔",  60f, 10f, 180f, 10f, bountyHunterSpawnRate);
            bountyHunterReducedCooldown = CustomOption.Create(322, Types.Impostor, "击杀悬赏目标的奖励冷却", 2.5f, 0f, 30f, 2.5f, bountyHunterSpawnRate);
            bountyHunterPunishmentTime = CustomOption.Create(323, Types.Impostor, "击杀非悬赏目标的惩罚冷却", 20f, 0f, 60f, 2.5f, bountyHunterSpawnRate);
            bountyHunterShowArrow = CustomOption.Create(324, Types.Impostor, "显示指向悬赏目标的箭头", true, bountyHunterSpawnRate);
            bountyHunterArrowUpdateIntervall = CustomOption.Create(325, Types.Impostor, "箭头更新间隔时间", 0f, 0f, 60f, 2.5f, bountyHunterShowArrow);

            witchSpawnRate = CustomOption.Create(370, Types.Impostor, cs(Witch.color, "女巫"), rates, null, true);
            witchCooldown = CustomOption.Create(371, Types.Impostor, "女巫下咒冷却", 10f, 10f, 120f, 5f, witchSpawnRate);
            witchAdditionalCooldown = CustomOption.Create(372, Types.Impostor, "女巫每次下咒追加的下咒冷却", 0f, 0f, 60f, 5f, witchSpawnRate);
            witchCanSpellAnyone = CustomOption.Create(373, Types.Impostor, "女巫可对任何人下咒", false, witchSpawnRate);
            witchSpellCastingDuration = CustomOption.Create(374, Types.Impostor, "下咒所需时间", 0f, 0f, 10f, 1f, witchSpawnRate);
            witchTriggerBothCooldowns = CustomOption.Create(375, Types.Impostor, "下咒与击杀共用冷却", false, witchSpawnRate);
            witchVoteSavesTargets = CustomOption.Create(376, Types.Impostor, "放逐女巫可拯救被下咒的玩家", false, witchSpawnRate);

	
            ninjaSpawnRate = CustomOption.Create(380, Types.Impostor, cs(Ninja.color, "忍者"), rates, null, true);
            ninjaCooldown = CustomOption.Create(381, Types.Impostor, "忍者标记冷却", 25f, 10f, 120f, 5f, ninjaSpawnRate);
            ninjaKnowsTargetLocation = CustomOption.Create(382, Types.Impostor, "对忍者显示指向被标记玩家的箭头", true, ninjaSpawnRate);
            ninjaTraceTime = CustomOption.Create(383, Types.Impostor, "忍者留下的痕迹的持续时间", 5f, 1f, 20f, 0.5f, ninjaSpawnRate);
            ninjaTraceColorTime = CustomOption.Create(384, Types.Impostor, "忍者留下的痕迹褪色所需时间", 2f, 0f, 20f, 0.5f, ninjaSpawnRate);
            ninjaInvisibleDuration = CustomOption.Create(385, Types.Impostor, "忍者隐身持续时间", 7f, 0f, 20f, 1f, ninjaSpawnRate);

            bomberSpawnRate = CustomOption.Create(460, Types.Impostor, cs(Bomber.color, "爆破手"), rates, null, true);
            bomberBombDestructionTime = CustomOption.Create(461, Types.Impostor, "炸弹爆炸倒计时", 15f, 0f, 120f, 2.5f, bomberSpawnRate);
            bomberBombDestructionRange = CustomOption.Create(462, Types.Impostor, "爆炸破坏范围", 100f, 5f, 150f, 5f, bomberSpawnRate);
            bomberBombHearRange = CustomOption.Create(463, Types.Impostor, "爆炸破坏预警范围", 100f, 5f, 150f, 5f, bomberSpawnRate);
            bomberDefuseDuration = CustomOption.Create(464, Types.Impostor, "拆除炸弹所需时间", 3f, 0.5f, 30f, 0.5f, bomberSpawnRate);
            bomberBombCooldown = CustomOption.Create(465, Types.Impostor, "安放炸弹冷却时间", 20f, 2.5f, 30f, 2.5f, bomberSpawnRate);
            bomberBombActiveAfter = CustomOption.Create(466, Types.Impostor, "炸弹安放后激活所需时间", 5f, 0.5f, 15f, 0.5f, bomberSpawnRate);

            yoyoSpawnRate = CustomOption.Create(470, Types.Impostor, cs(Yoyo.color, "悠悠球"), rates, null, true);
            yoyoBlinkDuration = CustomOption.Create(471, Types.Impostor, "传送持续时长", 20f, 2.5f, 120f, 2.5f, yoyoSpawnRate);
            yoyoMarkCooldown = CustomOption.Create(472, Types.Impostor, "标记地点冷却", 20f, 2.5f, 120f, 2.5f, yoyoSpawnRate);
            yoyoMarkStaysOverMeeting = CustomOption.Create(473, Types.Impostor, "标记地点会议后依旧保留", true, yoyoSpawnRate);
            yoyoHasAdminTable = CustomOption.Create(474, Types.Impostor, "可查看管理桌地图", true, yoyoSpawnRate);
            yoyoAdminTableCooldown = CustomOption.Create(475, Types.Impostor, "查看管理桌冷却", 2.5f, 2.5f, 120f, 2.5f, yoyoHasAdminTable);
            yoyoSilhouetteVisibility = CustomOption.Create(476, Types.Impostor, "剪影能见度", new string[] { "0%", "10%", "20%", "30%", "40%", "50%" }, yoyoSpawnRate);

            poucherSpawnRate = CustomOption.Create(901, Types.Impostor, cs(Poucher.color, "赶尸人"), rates, null, true);
			mimicSpawnRate = CustomOption.Create(902, Types.Impostor, cs(Mimic.color, "食人族"), rates, null, true);

            minerSpawnRate = CustomOption.Create(914, Types.Impostor, cs(Miner.color, "管道工"), rates, null, true); //SpawnRate);
            minerCooldown = CustomOption.Create(915, Types.Impostor, "挖洞冷却", 10f, 10f, 60f, 2.5f, minerSpawnRate);

            escapistSpawnRate = CustomOption.Create(930, Types.Impostor, cs(Escapist.color, "逃逸者"), rates, null, true);
            escapistEscapeTime = CustomOption.Create(931, Types.Impostor, "标记与逃逸冷却", 30f, 0f, 60f, 2.5f, escapistSpawnRate);
            escapistChargesOnPlace = CustomOption.Create(932, Types.Impostor, "单轮逃逸次数", 10f, 0f, 10f, 1f, escapistSpawnRate);
     //       jumperResetPlaceAfterMeeting = CustomOption.Create(9052, Types.Crewmate, "Reset Places After Meeting", true, jumperSpawnRate);
     //       jumperChargesGainOnMeeting = CustomOption.Create(9053, Types.Crewmate, "Charges Gained After Meeting", 2, 0, 10, 1, jumperSpawnRate);
            escapistMaxCharges = CustomOption.Create(933, Types.Impostor, "全局游戏逃逸次数", 10f, 1f, 10f, 1f, escapistSpawnRate);

       
            pitfallerSpawnRate = CustomOption.Create(940, Types.Impostor, cs(Pitfaller.color, "制咒师"), rates, null, true);
            pitfallerCooldown = CustomOption.Create(941, Types.Impostor, "制咒冷却", 30f, 10f, 60f, 2.5f, pitfallerSpawnRate);
            
            shinobiSpawnRate = CustomOption.Create(942, Types.Impostor, cs(Shinobi.color, "潜刃手"), rates, null, true);
            shinobiCooldown = CustomOption.Create(943, Types.Impostor, "潜刃冷却", 25f, 10f, 60f, 2.5f, shinobiSpawnRate);
            shinobiDuration = CustomOption.Create(944, Types.Impostor, "潜刃持续时间", 30f, 0f, 30f, 1f, shinobiSpawnRate);
            shinobiKillDistance = CustomOption.Create(945, Types.Impostor, "潜刃击杀距离", 3f, 0f, 5f, 1f, shinobiSpawnRate);

            transportistSpawnRate = CustomOption.Create(990, Types.Impostor, cs(Transportist.color, "闪跃者"), rates, null, true);
            transportistScanCooldown = CustomOption.Create(991, Types.Impostor, "闪跃采样冷却", 20f, 1f, 30f, 1f, transportistSpawnRate);
            transportistDelaiAfterScan = CustomOption.Create(992, Types.Impostor, "闪跃采样后多久可将人拉倒跟前", 5f, 1f, 30f, 1f, transportistSpawnRate);
            transportistAddArrow = CustomOption.Create(993, Types.Impostor, "显示对闪跃采样对象的箭头", true, transportistSpawnRate);
            transportistUpdateIntervall = CustomOption.Create(994, Types.Impostor, "闪跃箭头更新时间", 0.5f, 0.2f, 5f, 0.2f, transportistSpawnRate);

            snarerSpawnRate = CustomOption.Create(950, Types.Impostor, cs(Snarer.color, "设陷师"), rates, null, true);
            snarerNumSnare = CustomOption.Create(951, Types.Impostor, "设陷师可放置陷阱数", 5f, 1f, 10f, 1f, snarerSpawnRate);
            snarerExtensionTime = CustomOption.Create(952, Types.Impostor, "设陷师布下陷阱后陷阱激活所需时间", 5f, 2f, 10f, 0.5f, snarerSpawnRate);
            snarerCooldown = CustomOption.Create(953, Types.Impostor, "设置陷阱的冷却时间", 20f, 10f, 60f, 2.5f, snarerSpawnRate);
            snarerKillTimer = CustomOption.Create(954, Types.Impostor, "玩家掉进陷阱后被陷阱击杀所需时间", 5f, 1f, 30f, 1f, snarerSpawnRate);
            snarerSnareRange = CustomOption.Create(955, Types.Impostor, "陷阱有效范围", 1f, 0.5f, 5f, 0.1f, snarerSpawnRate);
            snarerMaxDistance = CustomOption.Create(956, Types.Impostor, "听不到陷阱布下声音时玩家距陷阱距离", 10f, 1f, 50f, 1f, snarerSpawnRate);
            snarerPenaltyTime = CustomOption.Create(957, Types.Impostor, "设陷师进行通常击杀的追加冷却", 0f, 0f, 50f, 1f, snarerSpawnRate);
            snarerBonusTime = CustomOption.Create(958, Types.Impostor, "设陷师通常击杀落入陷阱的玩家的缩减冷却", 8f, 0f, 9f, 1f, snarerSpawnRate);

            guesserSpawnRate = CustomOption.Create(310, Types.Neutral, cs(Guesser.color, "赌怪"), rates, null, true);
            guesserIsImpGuesserRate = CustomOption.Create(311, Types.Neutral, "邪恶赌怪生成概率", rates, guesserSpawnRate);
            guesserNumberOfShots = CustomOption.Create(312, Types.Neutral, "赌怪猜测次数", 3f, 1f, 15f, 1f, guesserSpawnRate);
            guesserHasMultipleShotsPerMeeting = CustomOption.Create(313, Types.Neutral, "赌怪可在单次会议多次猜测", true, guesserSpawnRate);
            guesserKillsThroughShield  = CustomOption.Create(315, Types.Neutral, "赌怪无视医生护盾", true, guesserSpawnRate);
            guesserEvilCanKillSpy  = CustomOption.Create(316, Types.Neutral, "邪恶赌怪可猜测卧底", true, guesserSpawnRate);
            guesserSpawnBothRate = CustomOption.Create(317, Types.Neutral, "场上同时存在正义邪恶赌怪概率", rates, guesserSpawnRate);
            guesserCantGuessSnitchIfTaksDone = CustomOption.Create(318, Types.Neutral, "做完任务的告密者无法被猜测", true, guesserSpawnRate);

            jesterSpawnRate = CustomOption.Create(60, Types.Neutral, cs(Jester.color, "小丑"), rates, null, true);
            jesterCanCallEmergency = CustomOption.Create(61, Types.Neutral, "小丑可以召开紧急会议", true, jesterSpawnRate);
            jesterHasImpostorVision = CustomOption.Create(62, Types.Neutral, "小丑拥有内鬼视野", true, jesterSpawnRate);
            jesterCanVent = CustomOption.Create(63, Types.Neutral, "小丑可以钻洞", true, jesterSpawnRate);

            arsonistSpawnRate = CustomOption.Create(290, Types.Neutral, cs(Arsonist.color, "纵火犯"), rates, null, true);
            arsonistCooldown = CustomOption.Create(291, Types.Neutral, "涂油冷却", 10f, 2.5f, 60f, 2.5f, arsonistSpawnRate);
            arsonistDuration = CustomOption.Create(292, Types.Neutral, "涂油所需时间", 0f, 0f, 10f, 1f, arsonistSpawnRate);

            amnisiacSpawnRate = CustomOption.Create(903, Types.Neutral, cs(Amnisiac.color, "失忆者"), rates, null, true);
            amnisiacShowArrows = CustomOption.Create(904, Types.Neutral, "对失忆者显示指向尸体的箭头", true, amnisiacSpawnRate);
            amnisiacResetRole = CustomOption.Create(905, Types.Neutral, "失忆者获取职业后重置该职业的能力使用次数", false, amnisiacSpawnRate);

            jackalSpawnRate = CustomOption.Create(220, Types.Neutral, cs(Jackal.color, "爪牙"), rates, null, true);
            jackalKillCooldown = CustomOption.Create(221, Types.Neutral, "爪牙团队击杀冷却", 25f, 10f, 60f, 2.5f, jackalSpawnRate);
            jackalCreateSidekickCooldown = CustomOption.Create(222, Types.Neutral, "爪牙招募随从冷却", 12.5f, 10f, 60f, 2.5f, jackalSpawnRate);
            jackalCanUseVents = CustomOption.Create(223, Types.Neutral, "爪牙可以使用管道", true, jackalSpawnRate);
            jackalCanSabotageLights = CustomOption.Create(431, Types.Neutral, "爪牙可破坏灯光", true, jackalSpawnRate);
            jackalCanCreateSidekick = CustomOption.Create(224, Types.Neutral, "爪牙可以招募随从", true, jackalSpawnRate);
            sidekickPromotesToJackal = CustomOption.Create(225, Types.Neutral, "爪牙死后真的随从可晋升为爪牙", true, jackalCanCreateSidekick);
            sidekickCanKill = CustomOption.Create(226, Types.Neutral, "真的随从可击杀", true, jackalCanCreateSidekick);
            sidekickCanUseVents = CustomOption.Create(227, Types.Neutral, "真的随从可使用管道", true, jackalCanCreateSidekick);
            sidekickCanSabotageLights = CustomOption.Create(432, Types.Neutral, "随从可破坏灯光", true, jackalCanCreateSidekick);
            jackalPromotedFromSidekickCanCreateSidekick = CustomOption.Create(228, Types.Neutral, "真的随从晋升为爪牙后可招募新随从", true, sidekickPromotesToJackal);
            //jackalCanCreateSidekickFromImpostor = CustomOption.Create(229, Types.Neutral, "爪牙可招募内鬼为跟班", false, jackalCanCreateSidekick);
            jackalAndSidekickHaveImpostorVision = CustomOption.Create(430, Types.Neutral, "爪牙团队拥有内鬼视野", false, jackalSpawnRate);

            
            swooperSpawnRate = CustomOption.Create(906, Types.Neutral, cs(Swooper.color, "隐身人"), rates, null, true);                      //);
            swooperCooldown = CustomOption.Create(907, Types.Neutral, "隐身冷却", 25f, 10f, 60f, 2.5f, swooperSpawnRate);
            swooperDuration = CustomOption.Create(908, Types.Neutral, "隐身持续时间", 10f, 1f, 20f, 0.5f, swooperSpawnRate);
            swooperHasImpVision = CustomOption.Create(909, Types.Neutral, "隐身人拥有内鬼视野", true, swooperSpawnRate);

            vultureSpawnRate = CustomOption.Create(340, Types.Neutral, cs(Vulture.color, "秃鹫"), rates, null, true);
            vultureCooldown = CustomOption.Create(341, Types.Neutral, "吞噬冷却", 15f, 10f, 60f, 2.5f, vultureSpawnRate);
            vultureNumberToWin = CustomOption.Create(342, Types.Neutral, "胜利所需吞噬的尸体数量", 4f, 1f, 10f, 1f, vultureSpawnRate);
            vultureCanUseVents = CustomOption.Create(343, Types.Neutral, "秃鹫可使用管道", true, vultureSpawnRate);
            vultureShowArrows = CustomOption.Create(344, Types.Neutral, "对秃鹫显示指向尸体的箭头", true, vultureSpawnRate);

            lawyerSpawnRate = CustomOption.Create(350, Types.Neutral, cs(Lawyer.color, "律师"), rates, null, true);
            lawyerIsProsecutorChance = CustomOption.Create(358, Types.Neutral, "检察官生成概率", rates, lawyerSpawnRate);
            lawyerVision = CustomOption.Create(354, Types.Neutral, "律师视野", 3f, 0.25f, 3f, 0.25f, lawyerSpawnRate);
            lawyerKnowsRole = CustomOption.Create(355, Types.Neutral, "律师/检察官知道目标的职业", true, lawyerSpawnRate);
            lawyerCanCallEmergency = CustomOption.Create(352, Types.Neutral, "律师/检察官可以召开紧急会议", true, lawyerSpawnRate);
            lawyerTargetCanBeJester = CustomOption.Create(351, Types.Neutral, "律师的客户可以是小丑", true, lawyerSpawnRate);
            pursuerCooldown = CustomOption.Create(356, Types.Neutral, "起诉人空包弹冷却", 30f, 5f, 60f, 2.5f, lawyerSpawnRate);
            pursuerBlanksNumber = CustomOption.Create(357, Types.Neutral, "起诉人空包弹个数", 5f, 1f, 20f, 1f, lawyerSpawnRate);

            werewolfSpawnRate = CustomOption.Create(910, Types.Neutral, cs(Werewolf.color, "月下狼人"), rates, null, true);
            werewolfRampageCooldown  = CustomOption.Create(911, Types.Neutral, "暴走冷却", 25f, 10f, 60f, 2.5f, werewolfSpawnRate);
            werewolfRampageDuration = CustomOption.Create(912, Types.Neutral, "暴走持续时间", 3f, 1f, 20f, 0.5f, werewolfSpawnRate);
            werewolfKillCooldown = CustomOption.Create(913, Types.Neutral, "暴走时击杀冷却", 0f, 0f, 60f, 1f, werewolfSpawnRate);  

            juggernautSpawnRate = CustomOption.Create(916, Types.Neutral, cs(Juggernaut.color, "天启"), rates, null, true);
            juggernautCooldown = CustomOption.Create(917, Types.Neutral, "击杀冷却", 25f, 2.5f, 60f, 2.5f, juggernautSpawnRate);
            juggernautHasImpVision = CustomOption.Create(918, Types.Neutral, "天启拥有内鬼视野", true, juggernautSpawnRate);
            juggernautReducedkillEach = CustomOption.Create(919, Types.Neutral, "每次击杀后减少的cd", 10f, 2.5f, 15f, 2.5f, juggernautSpawnRate);  
        

            mayorSpawnRate = CustomOption.Create(80, Types.Crewmate, cs(Mayor.color, "市长"), rates, null, true);
            mayorCanSeeVoteColors = CustomOption.Create(81, Types.Crewmate, "匿名投票对市长无效", true, mayorSpawnRate);
            mayorTasksNeededToSeeVoteColors = CustomOption.Create(82, Types.Crewmate, "市长无视匿名投票需要完成的任务数", 0f, 0f, 20f, 1f, mayorCanSeeVoteColors);
            mayorMeetingButton = CustomOption.Create(83, Types.Crewmate, "随身远程紧急会议按钮", true, mayorSpawnRate);
            mayorMaxRemoteMeetings = CustomOption.Create(84, Types.Crewmate, "远程会议次数", 1f, 1f, 5f, 1f, mayorMeetingButton);
            mayorChooseSingleVote = CustomOption.Create(85, Types.Crewmate, "市长可选择投1张票", new string[] { "关闭", "投票前选择", "直到会议结束均可选择" }, mayorSpawnRate);
			
		

            engineerSpawnRate = CustomOption.Create(90, Types.Crewmate, cs(Engineer.color, "工程师"), rates, null, true);
            engineerNumberOfFixes = CustomOption.Create(91, Types.Crewmate, "远程维修次数", 2f, 1f, 3f, 1f, engineerSpawnRate);
            engineerHighlightForImpostors = CustomOption.Create(92, Types.Crewmate, "内鬼能看见工程师在管道中", true, engineerSpawnRate);
            engineerHighlightForTeamJackal = CustomOption.Create(93, Types.Crewmate, "爪牙团队能看到工程师在管道中", true, engineerSpawnRate);

            sheriffSpawnRate = CustomOption.Create(100, Types.Crewmate, cs(Sheriff.color, "警长"), rates, null, true);
            sheriffCooldown = CustomOption.Create(101, Types.Crewmate, "警长击杀冷却", 25f, 10f, 60f, 2.5f, sheriffSpawnRate);
            sheriffCanKillNeutrals = CustomOption.Create(102, Types.Crewmate, "警长可击杀中立阵营", false, sheriffSpawnRate);
            deputySpawnRate = CustomOption.Create(103, Types.Crewmate, "警长存在时辅警的出现概率", rates, sheriffSpawnRate);
            deputyNumberOfHandcuffs = CustomOption.Create(104, Types.Crewmate, "辅警手铐数量", 3f, 1f, 10f, 1f, deputySpawnRate);
            deputyHandcuffCooldown = CustomOption.Create(105, Types.Crewmate, "手铐冷却", 25f, 10f, 60f, 2.5f, deputySpawnRate);
            deputyHandcuffDuration = CustomOption.Create(106, Types.Crewmate, "手铐持续时间", 10f, 5f, 60f, 2.5f, deputySpawnRate);
            deputyKnowsSheriff = CustomOption.Create(107, Types.Crewmate, "警长和辅警互相确认身份 ", true, deputySpawnRate);
            deputyGetsPromoted = CustomOption.Create(108, Types.Crewmate, "辅警在警长死后是否升职为警长", new string[] { "否", "立刻升职", "会议后升职" }, deputySpawnRate);
            deputyKeepsHandcuffs = CustomOption.Create(109, Types.Crewmate, "辅警升职为警长后保留辅警技能", true, deputyGetsPromoted);

            lighterSpawnRate = CustomOption.Create(110, Types.Crewmate, cs(Lighter.color, "执灯人"), rates, null, true);
            lighterModeLightsOnVision = CustomOption.Create(111, Types.Crewmate, "非熄灯状态下的执灯人视野", 3.5f, 0.25f, 5f, 0.25f, lighterSpawnRate);
            lighterModeLightsOffVision = CustomOption.Create(112, Types.Crewmate, "熄灯状态状态下的执灯人视野", 3.5f, 0.25f, 5f, 0.25f, lighterSpawnRate);
            lighterFlashlightWidth = CustomOption.Create(113, Types.Crewmate, "手电筒范围", 1f, 0.1f, 1f, 0.1f, lighterSpawnRate);

            detectiveSpawnRate = CustomOption.Create(120, Types.Crewmate, cs(Detective.color, "侦探"), rates, null, true);
            detectiveAnonymousFootprints = CustomOption.Create(121, Types.Crewmate, "使用匿名脚印", false, detectiveSpawnRate); 
            detectiveFootprintIntervall = CustomOption.Create(122, Types.Crewmate, "脚印生成间隔时间", 0.5f, 0.25f, 10f, 0.25f, detectiveSpawnRate);
            detectiveFootprintDuration = CustomOption.Create(123, Types.Crewmate, "脚印持续时间", 5f, 0.25f, 10f, 0.25f, detectiveSpawnRate);
            detectiveReportNameDuration = CustomOption.Create(124, Types.Crewmate, "侦探多久报告尸体能显示凶手姓名", 10, 0, 60, 2.5f, detectiveSpawnRate);
            detectiveReportColorDuration = CustomOption.Create(125, Types.Crewmate, "侦探多久报告尸体能显示凶手颜色", 30, 0, 120, 2.5f, detectiveSpawnRate);

            timeMasterSpawnRate = CustomOption.Create(130, Types.Crewmate, cs(TimeMaster.color, "时间之主"), rates, null, true);
            timeMasterCooldown = CustomOption.Create(131, Types.Crewmate, "时间之主冷却", 15f, 10f, 120f, 2.5f, timeMasterSpawnRate);
            timeMasterRewindTime = CustomOption.Create(132, Types.Crewmate, "时光之盾触发时回溯时间", 3f, 1f, 10f, 1f, timeMasterSpawnRate);
            timeMasterShieldDuration = CustomOption.Create(133, Types.Crewmate, "时光之盾持续时间", 20f, 1f, 20f, 1f, timeMasterSpawnRate);

            medicSpawnRate = CustomOption.Create(140, Types.Crewmate, cs(Medic.color, "医生"), rates, null, true);
            medicShowShielded = CustomOption.Create(143, Types.Crewmate, "可见医生护盾的玩家", new string[] {"所有人", "持有护盾的玩家与医生", "医生"}, medicSpawnRate);
            medicShowAttemptToShielded = CustomOption.Create(144, Types.Crewmate, "持有护盾的玩家可见击杀尝试", true, medicSpawnRate);
            medicSetOrShowShieldAfterMeeting = CustomOption.Create(145, Types.Crewmate, "护盾生效与可见时机", new string[] { "立即生效且可见", "立即生效，会议后可见", "会议后生效且可见" }, medicSpawnRate);

            medicShowAttemptToMedic = CustomOption.Create(146, Types.Crewmate, "医生可见对持有护盾的玩家的击杀尝试", true, medicSpawnRate);

           
            swapperSpawnRate = CustomOption.Create(150, Types.Crewmate, cs(Swapper.color, "换票师"), rates, null, true);
            swapperCanCallEmergency = CustomOption.Create(151, Types.Crewmate, "换票师可以召开紧急会议", false, swapperSpawnRate);
            swapperCanOnlySwapOthers = CustomOption.Create(152, Types.Crewmate, "换票师只可交换他人的得票", false, swapperSpawnRate);

            swapperSwapsNumber = CustomOption.Create(153, Types.Crewmate, "初始换票次数", 2f, 0f, 5f, 1f, swapperSpawnRate);
            swapperRechargeTasksNumber = CustomOption.Create(154, Types.Crewmate, "换票师补充一次换票次数需要完成的任务数", 1f, 1f, 10f, 1f, swapperSpawnRate);

            seerSpawnRate = CustomOption.Create(160, Types.Crewmate, cs(Seer.color, "灵媒"), rates, null, true);
            seerMode = CustomOption.Create(161, Types.Crewmate, "灵媒模式", new string[]{ "死亡闪光+灵魂", "死亡闪光", "灵魂"}, seerSpawnRate);
            seerLimitSoulDuration = CustomOption.Create(163, Types.Crewmate, "灵媒可见灵魂有限制时间", false, seerSpawnRate);
            seerSoulDuration = CustomOption.Create(162, Types.Crewmate, "灵媒可见灵魂限制时间", 15f, 0f, 120f, 5f, seerLimitSoulDuration);
        
            hackerSpawnRate = CustomOption.Create(170, Types.Crewmate, cs(Hacker.color, "黑客"), rates, null, true);
            hackerCooldown = CustomOption.Create(171, Types.Crewmate, "骇入冷却", 5f, 5f, 60f, 5f, hackerSpawnRate);
            hackerHackeringDuration = CustomOption.Create(172, Types.Crewmate, "骇入持续时间", 10f, 2.5f, 60f, 2.5f, hackerSpawnRate);
            hackerOnlyColorType = CustomOption.Create(173, Types.Crewmate, "骇入状态下只可见颜色深浅", false, hackerSpawnRate);
            hackerToolsNumber = CustomOption.Create(174, Types.Crewmate, "移动装置最大充能次数", 30f, 1f, 30f, 1f, hackerSpawnRate);
            hackerRechargeTasksNumber = CustomOption.Create(175, Types.Crewmate, "黑客进行一次充能需要完成的任务数", 1f, 1f, 5f, 1f, hackerSpawnRate);
            hackerNoMove = CustomOption.Create(176, Types.Crewmate, "黑客使用移动装置时无法移动", false, hackerSpawnRate);

            trackerSpawnRate = CustomOption.Create(200, Types.Crewmate, cs(Tracker.color, "追踪者"), rates, null, true);
            trackerUpdateIntervall = CustomOption.Create(201, Types.Crewmate, "追踪箭头的更新间隔时间", 1f, 1f, 30f, 1f, trackerSpawnRate);
            trackerResetTargetAfterMeeting = CustomOption.Create(202, Types.Crewmate, "追踪者会议后可重选追踪目标", true, trackerSpawnRate);
            trackerCanTrackCorpses = CustomOption.Create(203, Types.Crewmate, "追踪者可追踪尸体", true, trackerSpawnRate);
            trackerCorpsesTrackingCooldown = CustomOption.Create(204, Types.Crewmate, "追踪尸体冷却", 25f, 5f, 120f, 5f, trackerCanTrackCorpses);
            trackerCorpsesTrackingDuration = CustomOption.Create(205, Types.Crewmate, "追踪尸体持续时间", 15f, 2.5f, 30f, 2.5f, trackerCanTrackCorpses);
            trackerTrackingMethod = CustomOption.Create(206, Types.Crewmate, "追踪者如何得知位置", new string[] { "仅箭头", "仅距离传感器", "箭头 + 距离传感器" }, trackerSpawnRate);
                           
            snitchSpawnRate = CustomOption.Create(210, Types.Crewmate, cs(Snitch.color, "告密者"), rates, null, true);
            snitchLeftTasksForReveal = CustomOption.Create(219, Types.Crewmate, "内鬼阵营玩家确认告密者时告密者剩余任务", 1f, 0f, 25f, 1f, snitchSpawnRate);
            snitchMode = CustomOption.Create(211, Types.Crewmate, "获取线索方式", new string[] { "聊天栏", "地图", "聊天栏 & 地图" }, snitchSpawnRate);
            snitchTargets = CustomOption.Create(212, Types.Crewmate, "线索指向的目标", new string[] { "所有邪恶玩家", "仅持刃者" }, snitchSpawnRate);
            snitchSeeMeeting = CustomOption.Create(218, Types.Crewmate, "会议中显示告密结果", true, snitchSpawnRate);

            spySpawnRate = CustomOption.Create(240, Types.Crewmate, cs(Spy.color, "卧底"), rates, null, true);
            spyCanDieToSheriff = CustomOption.Create(241, Types.Crewmate, "卧底可被警长杀死", true, spySpawnRate);
            spyImpostorsCanKillAnyone = CustomOption.Create(242, Types.Crewmate, "如果有卧底存在，内鬼可击杀任何人", true, spySpawnRate);
            spyCanEnterVents = CustomOption.Create(243, Types.Crewmate, "卧底可以进入管道", true, spySpawnRate);
            spyHasImpostorVision = CustomOption.Create(244, Types.Crewmate, "卧底拥有内鬼视野", true, spySpawnRate);

            portalmakerSpawnRate = CustomOption.Create(390, Types.Crewmate, cs(Portalmaker.color, "传送门师"), rates, null, true);
            portalmakerCooldown = CustomOption.Create(391, Types.Crewmate, "制造传送门冷却", 5f, 5f, 60f, 2.5f, portalmakerSpawnRate);
            portalmakerUsePortalCooldown = CustomOption.Create(392, Types.Crewmate, "使用传送门冷却", 20f, 10f, 60f, 2.5f, portalmakerSpawnRate);
            portalmakerLogOnlyColorType = CustomOption.Create(393, Types.Crewmate, "传送记录只显示颜色深浅", true, portalmakerSpawnRate);
            portalmakerLogHasTime = CustomOption.Create(394, Types.Crewmate, "记录玩家经过传送门的时间", true, portalmakerSpawnRate);
            portalmakerCanPortalFromAnywhere = CustomOption.Create(395, Types.Crewmate, "传送门师可以从任何位置传送到传送门", true, portalmakerSpawnRate);

            securityGuardSpawnRate = CustomOption.Create(280, Types.Crewmate, cs(SecurityGuard.color, "保安"), rates, null, true);
            securityGuardCooldown = CustomOption.Create(281, Types.Crewmate, "保安技能冷却", 10f, 10f, 60f, 2.5f, securityGuardSpawnRate);
            securityGuardTotalScrews = CustomOption.Create(282, Types.Crewmate, "保安拥有螺丝钉数", 6f, 1f, 15f, 1f, securityGuardSpawnRate);
            securityGuardCamPrice = CustomOption.Create(283, Types.Crewmate, "安放摄像头消耗的螺丝数量", 3f, 1f, 15f, 1f, securityGuardSpawnRate);
            securityGuardVentPrice = CustomOption.Create(284, Types.Crewmate, "封锁管道消耗的螺丝数量", 3f, 1f, 15f, 1f, securityGuardSpawnRate);
            securityGuardCamDuration = CustomOption.Create(285, Types.Crewmate, "看远程监控可持续时间", 10f, 2.5f, 60f, 2.5f, securityGuardSpawnRate);
            securityGuardCamMaxCharges = CustomOption.Create(286, Types.Crewmate, "保安看远程监控次数上限", 30f, 1f, 30f, 1f, securityGuardSpawnRate);
            securityGuardCamRechargeTasksNumber = CustomOption.Create(287, Types.Crewmate, "保安进行一次充能需要完成的任务数", 1f, 1f, 10f, 1f, securityGuardSpawnRate);
            securityGuardNoMove = CustomOption.Create(288, Types.Crewmate, "保安不能在远程观看监控时移动", false, securityGuardSpawnRate);

            mediumSpawnRate = CustomOption.Create(360, Types.Crewmate, cs(Medium.color, "通灵师"), rates, null, true);
            mediumCooldown = CustomOption.Create(361, Types.Crewmate, "通灵师通灵冷却", 10f, 5f, 120f, 5f, mediumSpawnRate);
            mediumDuration = CustomOption.Create(362, Types.Crewmate, "询问所需时间", 1f, 0f, 15f, 1f, mediumSpawnRate);
            mediumOneTimeUse = CustomOption.Create(363, Types.Crewmate, "每个幽灵只可询问一次", false, mediumSpawnRate);
            mediumChanceAdditionalInfo = CustomOption.Create(364, Types.Crewmate, "所获答案中含有额外信息的几率", rates, mediumSpawnRate);

            thiefSpawnRate = CustomOption.Create(400, Types.Neutral, cs(Thief.color, "身份窃贼"), rates, null, true);
            thiefCooldown = CustomOption.Create(401, Types.Neutral, "击杀冷却", 25f, 5f, 120f, 5f, thiefSpawnRate);
            thiefCanKillSheriff = CustomOption.Create(402, Types.Neutral, "身份窃贼可击杀警长", true, thiefSpawnRate);
            thiefHasImpVision = CustomOption.Create(403, Types.Neutral, "身份窃贼拥有内鬼视野", true, thiefSpawnRate);
            thiefCanUseVents = CustomOption.Create(404, Types.Neutral, "身份窃贼可使用管道", true, thiefSpawnRate);
            thiefCanStealWithGuess = CustomOption.Create(405, Types.Neutral, "身份窃贼可以通过猜测窃取身份", true, thiefSpawnRate);
            thiefCanKillDeputy = CustomOption.Create(406, Types.Neutral, "身份窃贼可以击杀辅警", true, thiefSpawnRate);
            thiefCanKillVeteren = CustomOption.Create(407, Types.Neutral, "身份窃贼可以击杀老兵", true, thiefSpawnRate);

            doomsayerSpawnRate = CustomOption.Create(961, Types.Neutral, cs(Doomsayer.color, "末日使徒"), rates, null, true);
            doomsayerCooldown = CustomOption.Create(962, Types.Neutral, "技能冷却", 25f, 2.5f, 60f, 2.5f, doomsayerSpawnRate);
            doomsayerHasMultipleShotsPerMeeting = CustomOption.Create(963, Types.Neutral, "同一轮会议可多次猜测", true, doomsayerSpawnRate);
            doomsayerShowInfoInGhostChat = CustomOption.Create(964, Types.Neutral, "灵魂可见猜测结果", true, doomsayerSpawnRate);
            doomsayerCanGuessNeutral = CustomOption.Create(965, Types.Neutral, "可以猜测中立", true, doomsayerSpawnRate);
            doomsayerCanGuessImpostor = CustomOption.Create(966, Types.Neutral, "可以猜测伪装者", true, doomsayerSpawnRate);
            doomsayerOnlineTarger = CustomOption.Create(967, Types.Neutral, "洞察仅获取在线玩家的职业", false, doomsayerSpawnRate);
            doomsayerKillToWin = CustomOption.Create(968, Types.Neutral, "需要成功猜测几次获胜", 3f, 1f, 10f, 1f, doomsayerSpawnRate);
            doomsayerDormationNum = CustomOption.Create(969, Types.Neutral, "洞察给出的职业数量", 8f, 1f, 10f, 1f, doomsayerSpawnRate);

            kataomoiSpawnRate = CustomOption.Create(970, Types.Neutral, cs(Kataomoi.color, "痴恋"), rates, null, true);
            kataomoiStareCooldown = CustomOption.Create(971, Types.Neutral, "痴恋相思冷却", 15f, 2.5f, 60f, 2.5f, kataomoiSpawnRate);
            kataomoiStareDuration = CustomOption.Create(972, Types.Neutral, "痴恋相思时长", 1f, 1f, 10f, 1f, kataomoiSpawnRate);
            kataomoiStareCount = CustomOption.Create(973, Types.Neutral, "痴恋须相思次数", 3f, 1f, 100f, 1f, kataomoiSpawnRate);
            kataomoiStalkingCooldown = CustomOption.Create(974, Types.Neutral, "痴恋隐身冷却", 15f, 2.5f, 60f, 2.5f, kataomoiSpawnRate);
            kataomoiStalkingDuration = CustomOption.Create(975, Types.Neutral, "痴恋隐身时长", 15f, 1f, 30f, 1f, kataomoiSpawnRate);
            kataomoiStalkingFadeTime = CustomOption.Create(976, Types.Neutral, "痴恋隐身变化过程时长", 0.0f, 0.0f, 2.5f, 0.5f, kataomoiSpawnRate);
            kataomoiSearchCooldown = CustomOption.Create(977, Types.Neutral, "痴恋追踪冷却", 15f, 2.5f, 60f, 2.5f, kataomoiSpawnRate);
            kataomoiSearchDuration = CustomOption.Create(978, Types.Neutral, "痴恋追踪时长", 10f, 1f, 30f, 1f, kataomoiSpawnRate);

            akujoSpawnRate = CustomOption.Create(980, Types.Neutral, cs(Akujo.color, "魅魔"), rates, null, true);
            akujoTimeLimit = CustomOption.Create(981, Types.Neutral, "必须在此时间内招募真爱与备胎", 120f, 30f, 1200f, 30f, akujoSpawnRate);
            akujoNumKeeps = CustomOption.Create(982, Types.Neutral, "备胎数量", 2f, 0f, 10f, 1f, akujoSpawnRate);
            akujoKnowsRoles = CustomOption.Create(983, Types.Neutral, "魅魔可得知眷属职业", true, akujoSpawnRate);

            cerenovusSpawnRate = CustomOption.Create(984, Types.Neutral, cs(Cerenovus.color, "洗脑师"), rates, null, true);
            cerenovusBrainwashCooldown = CustomOption.Create(985, Types.Neutral, "操纵洗脑冷却", 25f, 10f, 60f, 2.5f, cerenovusSpawnRate);
            cerenovusBrainwashTime = CustomOption.Create(986, Types.Neutral, "操纵洗脑时长", 25f, 5f, 60f, 2.5f, cerenovusSpawnRate);
            cerenovusNumberToWin = CustomOption.Create(987, Types.Neutral,"操纵洗脑击杀几次后获胜", 3f, 1f, 10f, 1f, cerenovusSpawnRate);


            trapperSpawnRate = CustomOption.Create(410, Types.Crewmate, cs(Trapper.color, "诱捕者"), rates, null, true);
            trapperCooldown = CustomOption.Create(420, Types.Crewmate, "设置陷阱冷却", 25f, 5f, 120f, 5f, trapperSpawnRate);
            trapperMaxCharges = CustomOption.Create(440, Types.Crewmate, "陷阱最大使用次数", 15f, 1f, 15f, 1f, trapperSpawnRate);
            trapperRechargeTasksNumber = CustomOption.Create(450, Types.Crewmate, "诱捕者进行一次充能需要完成的任务数", 1f, 1f, 15f, 1f, trapperSpawnRate);
            trapperTrapNeededTriggerToReveal = CustomOption.Create(451, Types.Crewmate, "陷阱触发提示所需人数", 3f, 2f, 10f, 1f, trapperSpawnRate);
            trapperAnonymousMap = CustomOption.Create(452, Types.Crewmate, "显示匿名地图", true, trapperSpawnRate);
            trapperInfoType = CustomOption.Create(453, Types.Crewmate, "信息揭示类型", new string[] { "职业", "好/坏阵营", "玩家ID" }, trapperSpawnRate);
            trapperTrapDuration = CustomOption.Create(454, Types.Crewmate, "陷阱困人定身时间", 5f, 1f, 15f, 1f, trapperSpawnRate);

            bodyGuardSpawnRate = CustomOption.Create(920, Types.Crewmate, cs(BodyGuard.color, "保镖"), rates, null, true);
            bodyGuardResetTargetAfterMeeting = CustomOption.Create(921, Types.Crewmate, "每轮会议后重置目标", true, bodyGuardSpawnRate);
            bodyGuardFlash = CustomOption.Create(922, Types.Crewmate, "死亡时显示闪光", true, bodyGuardSpawnRate);


            veterenSpawnRate = CustomOption.Create(923, Types.Crewmate, cs(Veteren.color, "老兵"), rates, null, true);
            veterenCooldown = CustomOption.Create(924, Types.Crewmate, "警戒冷却", 25f, 10f, 120f, 2.5f, veterenSpawnRate);
	        veterenAlertDuration = CustomOption.Create(925, Types.Crewmate, "警戒持续时间", 10f, 1f, 20f, 1f, veterenSpawnRate);

            transporterSpawnRate = CustomOption.Create(934, Types.Crewmate, cs(Transporter.color, "位移师"), rates, null, true);
            transporterScanCooldown = CustomOption.Create(935, Types.Crewmate, "采样冷却", 5f, 1f, 30f, 1f, transporterSpawnRate);
            transporterDelaiAfterScan = CustomOption.Create(936, Types.Crewmate, "采样后多久可传送位置", 10f, 1f, 30f, 1f, transporterSpawnRate);
            transporterAddArrow = CustomOption.Create(937, Types.Crewmate, "显示对采样对象的箭头", true, transporterSpawnRate);
            transporterUpdateIntervall = CustomOption.Create(938, Types.Crewmate, "箭头更新时间", 0.5f, 0.2f, 5f, 0.2f, transporterSpawnRate);

            arbiterSpawnRate = CustomOption.Create(926, Types.Crewmate, cs(Arbiter.color, "裁决者"), rates, null, true);
            arbiterNumberOfSpecialVotes = CustomOption.Create(927, Types.Crewmate, "裁决技能可使用次数", 1f, 1f, 15f, 1f, arbiterSpawnRate);
            arbiterSpecificMessageMode = CustomOption.Create(928, Types.Crewmate, "裁决后放逐阶段显示裁决信息", true, arbiterSpawnRate);

            teleporterSpawnRate = CustomOption.Create(946, Types.Crewmate, cs(Teleporter.color, "传送师"), rates, null, true);
            teleporterSampleCooldown = CustomOption.Create(947, Types.Crewmate, "采样冷却", 5f, 1f, 60f, 1f, teleporterSpawnRate);
            teleporterCooldown = CustomOption.Create(948, Types.Crewmate, "交换冷却", 25f, 5f, 120f, 2.5f, teleporterSpawnRate);
            teleporterTeleportNumber = CustomOption.Create(949, Types.Crewmate, "交换次数", 10f, 1f, 10f, 1f, teleporterSpawnRate);

            // Modifier (1000 - 1999)
            modifiersAreHidden = CustomOption.Create(1009, Types.Modifier, cs(Color.yellow, "开局不显示诱饵网红溅血者"), false, null, true);

            modifierDisperser = CustomOption.Create(1900, Types.Modifier, cs(Color.red, "分散者"), rates, null, true);
            modifierDisperserDispersesToVent = CustomOption.Create(1941, Types.Modifier, "分散者分散至洞口", true, modifierDisperser);
            modifierDisperserCooldown = CustomOption.Create(1942, Types.Modifier, "分散冷却", 25f, 10f, 60f, 2.5f, modifierDisperser);
            modifierDisperserNumberOfUses = CustomOption.Create(1943, Types.Modifier, "分散可使用次数", 1, 1, 5, 1, modifierDisperser);

            modifierAllknowing = CustomOption.Create(1907, Types.Modifier, cs(Color.red, "全知者"), rates, null, true);
            modifierAllknowingQuantity = CustomOption.Create(1908, Types.Modifier, cs(Color.red, "全知者数量"), ratesModifier, modifierAllknowing);

            modifierRecruiter = CustomOption.Create(1938, Types.Modifier, cs(Recruiter.color, "密教徒（仅红方数量设置为1时出现）"), rates, null, true);
            modifierRecruiterQuantity = CustomOption.Create(1939, Types.Modifier, "密教徒数量", 1f, 0f, 1f, 1f, modifierRecruiter);

            modifierViewer = CustomOption.Create(1110, Types.Modifier, cs(Color.grey, "观测者"), rates, null, true);

            modifierBloody = CustomOption.Create(1000, Types.Modifier, cs(Color.yellow, "溅血者"), rates, null, true);
            modifierBloodyQuantity = CustomOption.Create(1001, Types.Modifier, cs(Color.yellow, "溅血者数量"), ratesModifier, modifierBloody);
            modifierBloodyDuration = CustomOption.Create(1002, Types.Modifier, "血迹持续时间", 10f, 3f, 60f, 1f, modifierBloody);

            modifierAntiTeleport = CustomOption.Create(1010, Types.Modifier, cs(Color.yellow, "通讯兵"), rates, null, true);
            modifierAntiTeleportQuantity = CustomOption.Create(1011, Types.Modifier, cs(Color.yellow, "通讯兵数量"), ratesModifier, modifierAntiTeleport);

            modifierTieBreaker = CustomOption.Create(1020, Types.Modifier, cs(Color.yellow, "破平者"), rates, null, true);

            modifierBait = CustomOption.Create(1030, Types.Modifier, cs(Color.yellow, "诱饵"), rates, null, true);
            modifierBaitQuantity = CustomOption.Create(1031, Types.Modifier, cs(Color.yellow, "诱饵数量"), ratesModifier, modifierBait);
            modifierBaitReportDelayMin = CustomOption.Create(1032, Types.Modifier, "诱饵的最短延迟报告时间", 0f, 0f, 10f, 1f, modifierBait);
            modifierBaitReportDelayMax = CustomOption.Create(1033, Types.Modifier, "诱饵的最长延迟报告时间", 0f, 0f, 10f, 1f, modifierBait);
            modifierBaitShowKillFlash = CustomOption.Create(1034, Types.Modifier, "用闪光警告击杀诱饵的玩家", true, modifierBait);

            modifierLover = CustomOption.Create(1040, Types.Modifier, cs(Color.yellow, "恋人"), rates, null, true);
            modifierLoverImpLoverRate = CustomOption.Create(1041, Types.Modifier, "恋人中存在内鬼的概率", rates, modifierLover);
            modifierLoverBothDie = CustomOption.Create(1042, Types.Modifier, "恋人共死", true, modifierLover);
            modifierLoverEnableChat = CustomOption.Create(1043, Types.Modifier, "恋人拥有私聊频道", true, modifierLover);

            modifierSunglasses = CustomOption.Create(1050, Types.Modifier, cs(Color.yellow, "太阳镜"), rates, null, true);
            modifierSunglassesQuantity = CustomOption.Create(1051, Types.Modifier, cs(Color.yellow, "太阳镜数量"), ratesModifier, modifierSunglasses);
            modifierSunglassesVision = CustomOption.Create(1052, Types.Modifier, "太阳镜的视野范围", new string[] { "-10%", "-20%", "-30%", "-40%", "-50%" }, modifierSunglasses);

            modifierMini = CustomOption.Create(1061, Types.Modifier, cs(Color.yellow, "小孩"), rates, null, true);
            modifierMiniGrowingUpDuration = CustomOption.Create(1062, Types.Modifier, "小孩长大所需时间", 200f, 100f, 1500f, 100f, modifierMini);
            modifierMiniGrowingUpInMeeting = CustomOption.Create(1063, Types.Modifier, "会议中的小孩也会长大", true, modifierMini);

            modifierVip = CustomOption.Create(1070, Types.Modifier, cs(Color.yellow, "VIP"), rates, null, true);
            modifierVipQuantity = CustomOption.Create(1071, Types.Modifier, cs(Color.yellow, "网红数量"), ratesModifier, modifierVip);
            modifierVipShowColor = CustomOption.Create(1072, Types.Modifier, "网红死亡时全场闪光所属阵营的颜色", true, modifierVip);

            modifierInvert = CustomOption.Create(1080, Types.Modifier, cs(Color.yellow, "醉鬼"), rates, null, true);
            modifierInvertQuantity = CustomOption.Create(1081, Types.Modifier, cs(Color.yellow, "醉鬼数量"), ratesModifier, modifierInvert);
            modifierInvertDuration = CustomOption.Create(1082, Types.Modifier, "醉鬼状态持续几轮会议", 1f, 1f, 15f, 1f, modifierInvert);

            modifierChameleon = CustomOption.Create(1090, Types.Modifier, cs(Color.yellow, "变色龙"), rates, null, true);
            modifierChameleonQuantity = CustomOption.Create(1091, Types.Modifier, cs(Color.yellow, "变色龙数量"), ratesModifier, modifierChameleon);
            modifierChameleonHoldDuration = CustomOption.Create(1092, Types.Modifier, "从不动到褪色开始的间隔时间", 3f, 1f, 10f, 0.5f, modifierChameleon);
            modifierChameleonFadeDuration = CustomOption.Create(1093, Types.Modifier, "褪色过程持续时间", 1f, 0.25f, 10f, 0.25f, modifierChameleon);
            modifierChameleonMinVisibility = CustomOption.Create(1094, Types.Modifier, "最低透明度", new string[] { "0%", "10%", "20%", "30%", "40%", "50%" }, modifierChameleon);

            modifierShifter = CustomOption.Create(1100, Types.Modifier, cs(Color.yellow, "交换师"), rates, null, true);
            
            modifierCursed = CustomOption.Create(1901, Types.Modifier, cs(Color.yellow, "反骨"), rates, null, true);
            modifierRadar = CustomOption.Create(1902, Types.Modifier, cs(Color.yellow, "雷达"), rates, null, true);
            modifierIndomitable = CustomOption.Create(1903, Types.Modifier, cs(Color.yellow, "无畏"), rates, null, true);
            modifierParanoid = CustomOption.Create(1904, Types.Modifier, cs(Color.yellow, "多线程"), rates, null, true);
            modifierSlueth = CustomOption.Create(1905, Types.Modifier, cs(Color.yellow, "掘墓人"), rates, null, true);
            modifierAntiReport = CustomOption.Create(1906, Types.Modifier, cs(Color.yellow, "胆小鬼"), rates, null, true);

            modifierFlash = CustomOption.Create(1931, Types.Modifier, cs(Color.yellow, "闪电侠"), rates, null, true);
            modifierFlashQuantity = CustomOption.Create(1932, Types.Modifier, "闪电侠数量", ratesModifier, modifierFlash);
            modifierFlashSpeed = CustomOption.Create(1933, Types.Modifier, "闪电侠速度", 2f, 1f, 3.0f, 0.2f, modifierFlash);

            modifierGiant = CustomOption.Create(1934, Types.Modifier, cs(Color.yellow, "巨人"), rates, null, true);
            modifierGiantQuantity = CustomOption.Create(1935, Types.Modifier, "巨人数量", ratesModifier, modifierGiant);
            modifierGiantSpeed = CustomOption.Create(1936, Types.Modifier, "巨人速度", 0.6f, 0.1f, 3f, 0.1f, modifierGiant);
            modifierGiantSize = CustomOption.Create(1937, Types.Modifier, "巨人尺寸", 1.2f, 0.8f, 2, 0.1f, modifierGiant);

            modifierOneTimeKiller = CustomOption.Create(1940, Types.Modifier, cs(Sheriff.color, "正义使者"), rates, null, true);
            

            // Guesser Gamemode (2000 - 2999)
            guesserGamemodeCrewNumber = CustomOption.Create(2001, Types.Guesser, cs(Guesser.color, "船员阵营赌怪数量"), 15f, 1f, 15f, 1f, null, true);
            guesserGamemodeNeutralNumber = CustomOption.Create(2002, Types.Guesser, cs(Guesser.color, "独立阵营赌怪数量"), 15f, 1f, 15f, 1f, null, true);
            guesserGamemodeImpNumber = CustomOption.Create(2003, Types.Guesser, cs(Guesser.color, "内鬼阵营赌怪数量"), 15f, 1f, 15f, 1f, null, true);
            guesserForceJackalGuesser = CustomOption.Create(2007, Types.Guesser, "强制爪牙作为赌怪", false, null, true);
            guesserGamemodeSidekickIsAlwaysGuesser = CustomOption.Create(2012, Types.Guesser, "随从始终作为赌怪", false, null);
            guesserForceDoomsayerGuesser = CustomOption.Create(2019, Types.Guesser, "强制末日使徒作为赌怪（不开启该职业无技能）", true, null, true);
            guesserForceThiefGuesser = CustomOption.Create(2011, Types.Guesser, "强制身份窃贼作为赌怪", true, null, true);
            guesserGamemodeHaveModifier = CustomOption.Create(2004, Types.Guesser, "赌怪可以拥有附加职业", true, null);
            guesserGamemodeNumberOfShots = CustomOption.Create(2005, Types.Guesser, "赌怪最大猜测次数", 3f, 1f, 15f, 1f, null);
            guesserGamemodeHasMultipleShotsPerMeeting = CustomOption.Create(2006, Types.Guesser, "一轮会议可多次猜测", true, null);
            guesserGamemodeKillsThroughShield = CustomOption.Create(2008, Types.Guesser, "赌怪猜测无视医生护盾", true, null);
            guesserGamemodeEvilCanKillSpy = CustomOption.Create(2009, Types.Guesser, "邪恶的赌怪可猜测卧底", true, null);
            guesserGamemodeCantGuessSnitchIfTaksDone = CustomOption.Create(2010, Types.Guesser, "赌怪不可猜测已完成任务的告密者", true, null);

            // Hide N Seek Gamemode (3000 - 3999)
            hideNSeekMap = CustomOption.Create(3020, Types.HideNSeekMain, cs(Color.yellow, "捉迷藏地图"), new string[] { "The Skeld", "Mira", "Polus", "Airship", "Fungle", "Submerged", "LI Map"}, null, true, onChange: () => { int map = hideNSeekMap.selection; if (map >= 3) map++; GameOptionsManager.Instance.currentNormalGameOptions.MapId = (byte)map; });
            hideNSeekHunterCount = CustomOption.Create(3000, Types.HideNSeekMain, cs(Color.yellow, "猎人数量"), 1f, 1f, 3f, 1f);
            hideNSeekKillCooldown = CustomOption.Create(3021, Types.HideNSeekMain, cs(Color.yellow, "击杀冷却"), 10f, 2.5f, 60f, 2.5f);
            hideNSeekHunterVision = CustomOption.Create(3001, Types.HideNSeekMain, cs(Color.yellow, "猎人视野"), 0.5f, 0.25f, 2f, 0.25f);
            hideNSeekHuntedVision = CustomOption.Create(3002, Types.HideNSeekMain, cs(Color.yellow, "猎物视野"), 2f, 0.25f, 5f, 0.25f);
            hideNSeekCommonTasks = CustomOption.Create(3023, Types.HideNSeekMain, cs(Color.yellow, "普通任务数"), 1f, 0f, 4f, 1f);
            hideNSeekShortTasks = CustomOption.Create(3024, Types.HideNSeekMain, cs(Color.yellow, "短任务数"), 3f, 1f, 23f, 1f);
            hideNSeekLongTasks = CustomOption.Create(3025, Types.HideNSeekMain, cs(Color.yellow, "长任务数"), 3f, 0f, 15f, 1f);
            hideNSeekTimer = CustomOption.Create(3003, Types.HideNSeekMain, cs(Color.yellow, "游戏时间"), 5f, 1f, 30f, 0.5f);
            hideNSeekTaskWin = CustomOption.Create(3004, Types.HideNSeekMain, cs(Color.yellow, "猎物完成任务后获胜"), false);
            hideNSeekTaskPunish = CustomOption.Create(3017, Types.HideNSeekMain, cs(Color.yellow, "猎物完成一个任务的惩罚时间"), 10f, 0f, 30f, 1f);
            hideNSeekCanSabotage = CustomOption.Create(3019, Types.HideNSeekMain, cs(Color.yellow, "猎人可以破坏"), false);
            hideNSeekHunterWaiting = CustomOption.Create(3022, Types.HideNSeekMain, cs(Color.yellow, "游戏开始时猎人等待时间"), 15f, 2.5f, 60f, 2.5f);


            hunterLightCooldown = CustomOption.Create(3005, Types.HideNSeekRoles, cs(Color.red, "猎人开灯冷却"), 30f, 5f, 60f, 1f, null, true);
            hunterLightDuration = CustomOption.Create(3006, Types.HideNSeekRoles, cs(Color.red, "猎人开灯持续时间"), 5f, 1f, 60f, 1f);
            hunterLightVision = CustomOption.Create(3007, Types.HideNSeekRoles, cs(Color.red, "猎人开灯视野"), 3f, 1f, 5f, 0.25f);
            hunterLightPunish = CustomOption.Create(3008, Types.HideNSeekRoles, cs(Color.red, "猎人开灯惩罚时间"), 5f, 0f, 30f, 1f);
            hunterAdminCooldown = CustomOption.Create(3009, Types.HideNSeekRoles, cs(Color.red, "猎人观看管理地图冷却"), 30f, 5f, 60f, 1f);
            hunterAdminDuration = CustomOption.Create(3010, Types.HideNSeekRoles, cs(Color.red, "猎人观看管理地图持续时间"), 5f, 1f, 60f, 1f);
            hunterAdminPunish = CustomOption.Create(3011, Types.HideNSeekRoles, cs(Color.red, "猎人观看管理地图惩罚时间"), 5f, 0f, 30f, 1f);
            hunterArrowCooldown = CustomOption.Create(3012, Types.HideNSeekRoles, cs(Color.red, "猎人开启箭头冷却"), 30f, 5f, 60f, 1f);
            hunterArrowDuration = CustomOption.Create(3013, Types.HideNSeekRoles, cs(Color.red, "猎人开启箭头持续时间"), 5f, 0f, 60f, 1f);
            hunterArrowPunish = CustomOption.Create(3014, Types.HideNSeekRoles, cs(Color.red, "猎人开启箭头惩罚时间"), 5f, 0f, 30f, 1f);

            huntedShieldCooldown = CustomOption.Create(3015, Types.HideNSeekRoles, cs(Color.gray, "猎物开启护盾冷却"), 30f, 5f, 60f, 1f, null, true);
            huntedShieldDuration = CustomOption.Create(3016, Types.HideNSeekRoles, cs(Color.gray, "猎物护盾持续时间"), 5f, 1f, 60f, 1f);
            huntedShieldRewindTime = CustomOption.Create(3018, Types.HideNSeekRoles, cs(Color.gray, "猎物护盾回溯时间"), 3f, 1f, 10f, 1f);
            huntedShieldNumber = CustomOption.Create(3026, Types.HideNSeekRoles, cs(Color.gray, "猎物护盾使用次数"), 3f, 1f, 15f, 1f);

            // Prop Hunt General Options
            propHuntMap = CustomOption.Create(4020, Types.PropHunt, cs(Color.yellow, "地图"), new string[] { "The Skeld", "Mira", "Polus", "Airship", "Fungle", "Submerged", "LI Map"}, null, true, onChange: ()=> { int map = propHuntMap.selection; if (map >= 3) map++; GameOptionsManager.Instance.currentNormalGameOptions.MapId = (byte)map;});
            propHuntTimer = CustomOption.Create(4021, Types.PropHunt, cs(Color.yellow, "游戏时长"), 5f, 1f, 30f, 0.5f);
            propHuntUnstuckCooldown = CustomOption.Create(4011, Types.PropHunt, cs(Color.yellow, "穿墙冷却"), 30f, 2.5f, 60f, 2.5f);
            propHuntUnstuckDuration = CustomOption.Create(4012, Types.PropHunt, cs(Color.yellow, "穿墙持续时间"), 2f, 1f, 60f, 1f);
            propHunterVision = CustomOption.Create(4006, Types.PropHunt, cs(Color.yellow, "鬼视野"), 0.5f, 0.25f, 2f, 0.25f);
            propVision = CustomOption.Create(4007, Types.PropHunt, cs(Color.yellow, "匿藏者视野"), 2f, 0.25f, 5f, 0.25f);

            // Hunter Options
            propHuntNumberOfHunters = CustomOption.Create(4000, Types.PropHunt, cs(Color.red, "鬼数量"), 1f, 1f, 5f, 1f, null, true);
            hunterInitialBlackoutTime = CustomOption.Create(4001, Types.PropHunt, cs(Color.red, "鬼闭眼倒数时长"), 10f, 5f, 20f, 1f);
            hunterMissCooldown = CustomOption.Create(4004, Types.PropHunt, cs(Color.red, "未命中目标后击杀冷却"), 10f, 2.5f, 60f, 2.5f);
            hunterHitCooldown = CustomOption.Create(4005, Types.PropHunt, cs(Color.red, "命中目标后击杀冷却"), 10f, 2.5f, 60f, 2.5f);
            propHuntRevealCooldown = CustomOption.Create(4008, Types.PropHunt, cs(Color.red, "揭露匿藏者技能冷却"), 30f, 10f, 90f, 2.5f);
            propHuntRevealDuration = CustomOption.Create(4009, Types.PropHunt, cs(Color.red, "揭露匿藏者技能时长"), 5f, 1f, 60f, 1f);
            propHuntRevealPunish = CustomOption.Create(4010, Types.PropHunt, cs(Color.red, "揭露技能所追加惩罚时间"), 10f, 0f, 1800f, 5f);
            propHuntAdminCooldown = CustomOption.Create(4022, Types.PropHunt, cs(Color.red, "查看管理员地图冷却"), 30f, 2.5f, 1800f, 2.5f);
            propHuntFindCooldown = CustomOption.Create(4023, Types.PropHunt, cs(Color.red, "搜索技能冷却"), 60f, 2.5f, 1800f, 2.5f);
            propHuntFindDuration = CustomOption.Create(4024, Types.PropHunt, cs(Color.red, "搜索技能时长"), 5f, 1f, 15f, 1f);
            // Prop Options
            propBecomesHunterWhenFound = CustomOption.Create(4003, Types.PropHunt, cs(Palette.CrewmateBlue, "匿藏者被抓后变成鬼"), true, null, true);
            propHuntInvisEnabled = CustomOption.Create(4013, Types.PropHunt, cs(Palette.CrewmateBlue, "启用隐身能力"), true, null, true);
            propHuntInvisCooldown = CustomOption.Create(4014, Types.PropHunt, cs(Palette.CrewmateBlue, "隐身能力冷却"), 120f, 10f, 1800f, 2.5f, propHuntInvisEnabled);
            propHuntInvisDuration = CustomOption.Create(4015, Types.PropHunt, cs(Palette.CrewmateBlue, "隐身能力时长"), 5f, 1f, 30f, 1f, propHuntInvisEnabled);
            propHuntSpeedboostEnabled = CustomOption.Create(4016, Types.PropHunt, cs(Palette.CrewmateBlue, "启用加速技能"), true, null, true);
            propHuntSpeedboostCooldown = CustomOption.Create(4017, Types.PropHunt, cs(Palette.CrewmateBlue, "加速技能冷却"), 60f, 2.5f, 1800f, 2.5f, propHuntSpeedboostEnabled);
            propHuntSpeedboostDuration = CustomOption.Create(4018, Types.PropHunt, cs(Palette.CrewmateBlue, "加速技能时长"), 5f, 1f, 15f, 1f, propHuntSpeedboostEnabled);
            propHuntSpeedboostSpeed = CustomOption.Create(4019, Types.PropHunt, cs(Palette.CrewmateBlue, "加速速率倍数"), 2f, 1.25f, 5f, 0.25f, propHuntSpeedboostEnabled);



            // Other options
            maxNumberOfMeetings = CustomOption.Create(3, Types.General, "紧急会议总次数（市长会议除外）", 2, 0, 4, 1, null, true);
            blockSkippingInEmergencyMeetings = CustomOption.Create(4, Types.General, "紧急会议禁止弃票", false);
            noVoteIsSelfVote = CustomOption.Create(5, Types.General, "不投票等于投自己", false, blockSkippingInEmergencyMeetings);
            hidePlayerNames = CustomOption.Create(6, Types.General, "隐藏玩家昵称", false);
            allowParallelMedBayScans = CustomOption.Create(7, Types.General, "允许同时进行扫描任务", false);
            shieldFirstKill = CustomOption.Create(8, Types.General, "首位死亡玩家可在下局游戏获得一个护盾", true);
            finishTasksBeforeHauntingOrZoomingOut = CustomOption.Create(9, Types.General, "全地图视角与幽幽鬼影仅可在完成任务后使用", false);
            camsNightVision = CustomOption.Create(11, Types.General, "如果灯光被破坏, 监控会切换到夜视模式", false, null, true);
            camsNoNightVisionIfImpVision = CustomOption.Create(12, Types.General, "内鬼不受到监控的夜视效果影响", false, camsNightVision, false);
            deadImpsBlockSabotage = CustomOption.Create(13, Types.General, cs(Palette.ImpostorRed, "死亡内鬼无法开启破坏"), false, null, false);


            dynamicMap = CustomOption.Create(500, Types.General, "在随机地图上游玩", false, null, true);
            dynamicMapEnableSkeld = CustomOption.Create(501, Types.General, "Skeld", rates, dynamicMap, false);
            dynamicMapEnableMira = CustomOption.Create(502, Types.General, "Mira", rates, dynamicMap, false);
            dynamicMapEnablePolus = CustomOption.Create(503, Types.General, "Polus", rates, dynamicMap, false);
            dynamicMapEnableAirShip = CustomOption.Create(504, Types.General, "Airship", rates, dynamicMap, false);
            dynamicMapEnableFungle = CustomOption.Create(506, Types.General, "Fungle", rates, dynamicMap, false);
            dynamicMapEnableSubmerged = CustomOption.Create(505, Types.General, "Submerged", rates, dynamicMap, false);
            dynamicMapSeparateSettings = CustomOption.Create(509, Types.General, "Use Random Map Setting Presets", false, dynamicMap, false);



            blockGameEnd = CustomOption.Create(9995, Types.General, "特定船员职业在场时，非船员阵营绑票游戏也不会结束", true, null, true);
            impostorSeeRoles = CustomOption.Create(9996, Types.General, "无卧底时，内鬼可以看到队友职业", true);
            enableCamoComms = CustomOption.Create(9997, Types.General, "通讯破坏时开启隐蔽效果", true);
            randomGameStartPosition = CustomOption.Create(9998, Types.General, "随机开局出生点", true);
            showButtonTarget = CustomOption.Create(9994, Types.General, "按钮显示目标姓名", true);

           // blockedRolePairings.Add((byte)RoleId.Vampire, new [] { (byte)RoleId.Warlock});
          //  blockedRolePairings.Add((byte)RoleId.Warlock, new [] { (byte)RoleId.Vampire});
            blockedRolePairings.Add((byte)RoleId.Spy, new [] { (byte)RoleId.Mini});
            blockedRolePairings.Add((byte)RoleId.Mini, new [] { (byte)RoleId.Spy});
          //  blockedRolePairings.Add((byte)RoleId.Vulture, new [] { (byte)RoleId.Cleaner});
         //   blockedRolePairings.Add((byte)RoleId.Cleaner, new [] { (byte)RoleId.Vulture});
            blockedRolePairings.Add((byte)RoleId.Mayor, new [] { (byte)RoleId.AntiReport});
            blockedRolePairings.Add((byte)RoleId.AntiReport, new [] { (byte)RoleId.Mayor});
         //   blockedRolePairings.Add((byte)RoleId.AntiReport, new [] { (byte)RoleId.Detective});
         //   blockedRolePairings.Add((byte)RoleId.Detective, new [] { (byte)RoleId.AntiReport});
        }
    }
}
