using HarmonyLib;
using Hazel;
using static TheOtherRoles.TheOtherRoles;
using static TheOtherRoles.HudManagerStartPatch;
using static TheOtherRoles.GameHistory;
using static TheOtherRoles.TORMapOptions;
using TheOtherRoles.Objects;
using TheOtherRoles.Patches;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using TheOtherRoles.Players;
using TheOtherRoles.Utilities;
using TheOtherRoles.CustomGameModes;
using AmongUs.Data;
using AmongUs.GameOptions;
using Assets.CoreScripts;
using Reactor.Utilities.Extensions;
namespace TheOtherRoles
{
    public enum RoleId {
        Jester,
        Mayor,
        Portalmaker,
        Engineer,
        Sheriff,
        Deputy,
        Lighter,
        Werewolf,
        Juggernaut,
        Doomsayer,
        Kataomoi,
        Akujo,
        Cerenovus,
        Godfather,
        Mafioso,
        Janitor,
        Detective,
        TimeMaster,
	    Veteren,
		Swooper,
        Medic,
        Swapper,
        Seer,
        Morphling,
        Camouflager,
        Hacker,
        Tracker,
        Vampire,
        Snitch,
        Jackal,
        Sidekick,
        Eraser,
        BodyGuard,
        Transporter,
        Transportist,
        Teleporter,
        Arbiter,
        Spy,
        Trickster,
        Cleaner,
        Warlock,
        SecurityGuard,
        Arsonist,
        EvilGuesser,
        NiceGuesser,
        BountyHunter,
        Vulture,
        Medium,
        Trapper,
        Lawyer,
        Prosecutor,
        Pursuer,
        Witch,
        Ninja,
        Thief,
        Bomber,
        Yoyo,
        Amnisiac,
		Poucher,
        Mimic,
        Escapist,
        Pitfaller,
        Shinobi,
        Snarer,
        Miner,

        Crewmate,
        Impostor,
        // Modifier ---
		Paranoid,
        Lover,
        Bait,
        Bloody,
        AntiTeleport,
        Tiebreaker,
        Disperser,
        Slueth,
        Radar,
        Indomitable,
        AntiReport,
        AllKnowing,
        Viewer,
        Sunglasses,
        Cursed,
        Mini,
        Vip,
        Invert,
        Chameleon,
        Shifter,
        Recruiter,
        Flash,
        Giant,
        OneTimeKiller
    }

    enum CustomRPC
    {
        // Main Controls

        ResetVaribles = 100,
        ShareOptions,
        ForceEnd,
        WorkaroundSetRoles,
        SetRole,
        SetModifier,
        VersionHandshake,
        UseUncheckedVent,
        UncheckedMurderPlayer,
        UncheckedCmdReportDeadBody,
        UncheckedExilePlayer,
        DynamicMapOption,
        SetGameStarting,
        ShareGamemode,
        StopStart,

        // Role functionality

        EngineerFixLights = 120,
        EngineerFixSubmergedOxygen,
        EngineerUsedRepair,
        CleanBody,

        ShowIndomitableFlash,

        MedicSetShielded,
        ShowBodyGuardFlash,
        ShieldedMurderAttempt,
        TimeMasterShield,
        TimeMasterRewindTime,
		TurnToImpostor,
        BodyGuardGuardPlayer,
        VeterenAlert,
        VeterenKill,
	ShifterShift,
        SwapperSwap,
        MorphlingMorph,
        CamouflagerCamouflage,
        TrackerUsedTracker,
        VampireSetBitten,
        PlaceGarlic,
        DeputyUsedHandcuffs,
        DeputyPromotes,
        JackalCreatesSidekick,
        SidekickPromotes,
        ErasePlayerRoles,
        SetFutureErased,
        SetFutureShifted,
        SetFutureShielded,
        SetFutureSpelled,
        PlaceNinjaTrace,
        PlacePortal,
	    AmnisiacTakeRole,
        UsePortal,
        PlaceJackInTheBox,
        LightsOut,
        PlaceCamera,
        SealVent,
        ArsonistWin,
        GuesserShoot,
        LawyerSetTarget,
        LawyerPromotesToPursuer,
        SetBlanked,
        Bloody,
        SetFirstKill,
        Invert,
        SetTiebreak,
        SetInvisible,
        ThiefStealsRole,
        SetTrap,
        TriggerTrap,
        MayorSetVoteTwice,
        PlaceBomb,
        DefuseBomb,
        ShareRoom,
        YoyoMarkLocation,
        YoyoBlink,

        Mine,
        SetInvisibleGen,
		SetSwoop,
        Disperse,
        MimicMimicRole,
        SetPositionESC,
        PitfallBody,
        ShinobiExpand,
        TransporterSwap,
        TransportistSwap,
        TeleporterTeleport,
        SnarerKill,
        PlaceSnare,
        ClearSnare,
        ActivateSnare,
        DisableSnare,
        SnarerMeetingFlag,
        ArbiterSpecialVote,
        ArbiterSpecialVote_DoCastVote,
        SetFutureReveal,
        KataomoiSetTarget,
        KataomoiWin,
        KataomoiStalking,
        AkujoSetHonmei,
        AkujoSetKeep,
        AkujoSuicide,
        SetBrainwash,
        CerenovusKill,

        // Gamemode
        SetGuesserGm,
        HuntedShield,
        HuntedRewindTime,
        SetProp,
        SetRevealed,
        PropHuntStartTimer,
        PropHuntSetInvis,
        PropHuntSetSpeedboost,

        // Other functionality
        ShareTimer,
        ShareGhostInfo,


    }

    public static class RPCProcedure {

        // Main Controls

        public static void resetVariables() {
            Garlic.clearGarlics();
            JackInTheBox.clearJackInTheBoxes();
            NinjaTrace.clearTraces();
            Silhouette.clearSilhouettes();
            Portal.clearPortals();
            Bloodytrail.resetSprites();
            Trap.clearTraps();
            clearAndReloadMapOptions();
            clearAndReloadRoles();
            clearGameHistory();
            setCustomButtonCooldowns();
            reloadPluginOptions();
            Helpers.toggleZoom(reset : true);
            GameStartManagerPatch.GameStartManagerUpdatePatch.startingTimer = 0;
            SurveillanceMinigamePatch.nightVisionOverlays = null;
            EventUtility.clearAndReload();
            MapBehaviourPatch.clearAndReload();
        }

    public static void HandleShareOptions(byte numberOfOptions, MessageReader reader) {            
            try {
                for (int i = 0; i < numberOfOptions; i++) {
                    uint optionId = reader.ReadPackedUInt32();
                    uint selection = reader.ReadPackedUInt32();
                    CustomOption option = CustomOption.options.First(option => option.id == (int)optionId);
                    option.updateSelection((int)selection, i == numberOfOptions - 1);
                }
            } catch (Exception e) {
                TheOtherRolesPlugin.Logger.LogError("Error while deserializing options: " + e.Message);
            }
        }

        public static void forceEnd() {
            if (AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Started) return;
            foreach (PlayerControl player in CachedPlayer.AllPlayers)
            {
                if (!player.Data.Role.IsImpostor)
                {
                    
                    GameData.Instance.GetPlayerById(player.PlayerId); // player.RemoveInfected(); (was removed in 2022.12.08, no idea if we ever need that part again, replaced by these 2 lines.) 
                    player.CoSetRole(RoleTypes.Crewmate, true);

                    player.MurderPlayer(player);
                    player.Data.IsDead = true;
                }
            }
        }

        public static void shareGamemode(byte gm) {
            TORMapOptions.gameMode = (CustomGamemodes) gm;
            LobbyViewSettingsPatch.currentButtons?.ForEach(x => x.gameObject?.Destroy());
            LobbyViewSettingsPatch.currentButtons?.Clear();
            LobbyViewSettingsPatch.currentButtonTypes?.Clear();
        }

        public static void stopStart(byte playerId) {
            if (AmongUsClient.Instance.AmHost && CustomOptionHolder.anyPlayerCanStopStart.getBool()) {
                GameStartManager.Instance.ResetStartState();
                PlayerControl.LocalPlayer.RpcSendChat($"{Helpers.playerById(playerId).Data.PlayerName} stopped the game start!");
            }
        }

        public static void workaroundSetRoles(byte numberOfRoles, MessageReader reader)
        {
                for (int i = 0; i < numberOfRoles; i++)
                {                   
                    byte playerId = (byte) reader.ReadPackedUInt32();
                    byte roleId = (byte) reader.ReadPackedUInt32();
                    try {
                        setRole(roleId, playerId);
                    } catch (Exception e) {
                        TheOtherRolesPlugin.Logger.LogError("Error while deserializing roles: " + e.Message);
                    }
            }
            
        }

        public static void setRole(byte roleId, byte playerId) {
            PlayerControl target = Helpers.playerById(playerId);
            foreach (PlayerControl player in CachedPlayer.AllPlayers) {
                if (player.PlayerId == playerId) {
                    switch((RoleId)roleId) {
                    case RoleId.Jester:
                        Jester.jester = player;
                        break;
                    case RoleId.Werewolf:
                        Werewolf.werewolf = player;
                        break;
                    case RoleId.Juggernaut:
                        Juggernaut.juggernaut = player;
                        break;
                    case RoleId.Doomsayer:
                        Doomsayer.doomsayer = player;
                        break;
                    case RoleId.Kataomoi:
                        Kataomoi.kataomoi = player;
                        break;
                    case RoleId.Akujo:
                        Akujo.akujo = player;
                        break;
                    case RoleId.Cerenovus:
                        Cerenovus.cerenovus = player;
                        break;
                    case RoleId.Mayor:
                        Mayor.mayor = player;
                        break;
                    case RoleId.Portalmaker:
                        Portalmaker.portalmaker = player;
                        break;
                    case RoleId.Engineer:
                        Engineer.engineer = player;
                        break;
                    case RoleId.Sheriff:
                        Sheriff.sheriff = player;
                        break;
                    case RoleId.BodyGuard:
                        BodyGuard.bodyguard = player;
                        break;
                    case RoleId.Deputy:
                        Deputy.deputy = player;
                        break;
                    case RoleId.Lighter:
                        Lighter.lighter = player;
                        break;
                    case RoleId.Godfather:
                        Godfather.godfather = player;
                        break;
                    case RoleId.Mafioso:
                        Mafioso.mafioso = player;
                        break;
                    case RoleId.Janitor:
                        Janitor.janitor = player;
                        break;
                    case RoleId.Detective:
                        Detective.detective = player;
                        break;
                    case RoleId.TimeMaster:
                        TimeMaster.timeMaster = player;
                        break;
                    case RoleId.Veteren:
                        Veteren.veteren = player;
                        break;
                    case RoleId.Medic:
                        Medic.medic = player;
                        break;
                    case RoleId.Shifter:
                        Shifter.shifter = player;
                        break;
                    case RoleId.Swapper:
                        Swapper.swapper = player;
                        break;
                    case RoleId.Seer:
                        Seer.seer = player;
                        break;
                    case RoleId.Morphling:
                        Morphling.morphling = player;
                        break;
                    case RoleId.Camouflager:
                        Camouflager.camouflager = player;
                        break;
                    case RoleId.Hacker:
                        Hacker.hacker = player;
                        break;
                    case RoleId.Tracker:
                        Tracker.tracker = player;
                        break;
                    case RoleId.Vampire:
                        Vampire.vampire = player;
                        break;
                    case RoleId.Snitch:
                        Snitch.snitch = player;
                        break;
                    case RoleId.Jackal:
						Jackal.jackal = player;
						break;
                    case RoleId.Swooper:
						Swooper.swooper = player;
						break;
                    case RoleId.Sidekick:
                        Sidekick.sidekick = player;
                        break;
                    case RoleId.Eraser:
                        Eraser.eraser = player;
                        break;
                    case RoleId.Spy:
                        Spy.spy = player;
                        break;
                    case RoleId.Trickster:
                        Trickster.trickster = player;
                        break;
                    case RoleId.Cleaner:
                        Cleaner.cleaner = player;
                        break;

                    case RoleId.Poucher:
                        Poucher.poucher = player;
                        break;
                    case RoleId.Mimic:
                        Mimic.mimic = player;
                        break;
                    case RoleId.Escapist:
                        Escapist.escapist = player;
                        break;
                    case RoleId.Snarer:
                        Snarer.snarer = player;
                        break;
                    case RoleId.Warlock:
                        Warlock.warlock = player;
                        break;
                    case RoleId.SecurityGuard:
                        SecurityGuard.securityGuard = player;
                        break;
                    case RoleId.Arsonist:
                        Arsonist.arsonist = player;
                        break;
                    case RoleId.EvilGuesser:
                        Guesser.evilGuesser = player;
                        break;
                    case RoleId.NiceGuesser:
                        Guesser.niceGuesser = player;
                        break;
                    case RoleId.BountyHunter:
                        BountyHunter.bountyHunter = player;
                        break;
                    case RoleId.Vulture:
                        Vulture.vulture = player;
                        break;
                    case RoleId.Medium:
                        Medium.medium = player;
                        break;
                    case RoleId.Trapper:
                        Trapper.trapper = player;
                        break;
                    case RoleId.Lawyer:
                        Lawyer.lawyer = player;
                        break;
                    case RoleId.Prosecutor:
                        Lawyer.lawyer = player;
                        Lawyer.isProsecutor = true;
                        break;
                    case RoleId.Pursuer:
                        Pursuer.pursuer = player;
                        break;
                    case RoleId.Witch:
                        Witch.witch = player;
                        break;
                    case RoleId.Ninja:
                        Ninja.ninja = player;
                        break;
                    case RoleId.Thief:
                        Thief.thief = player;
                        break;
                    case RoleId.Bomber:
                        Bomber.bomber = player;
                        break;
                    case RoleId.Yoyo:
                        Yoyo.yoyo = player;
                        break;
                    case RoleId.Amnisiac:
                        Amnisiac.amnisiac = player;
                        break;
                    case RoleId.Pitfaller:
                        Pitfaller.pitfaller = player;
                        break;
                    case RoleId.Shinobi:
                        Shinobi.shinobi = player;
                        break;
                    case RoleId.Transporter:
                        Transporter.transporter = player;
                        break;
                    case RoleId.Transportist:
                        Transportist.transportist = player;
                        break;
                    case RoleId.Teleporter:
                        Teleporter.teleporter = player;
                        break;    
                    case RoleId.Miner:
                        Miner.miner = player;
                        break;
                    case RoleId.Arbiter:
                        Arbiter.arbiter = player;
                        break;      
                        
                    }
                    if (AmongUsClient.Instance.AmHost && Helpers.roleCanUseVents(player) && !player.Data.Role.IsImpostor) {
                        player.RpcSetRole(RoleTypes.Engineer);
                        player.CoSetRole(RoleTypes.Engineer, true);
                    }                   
                }
            }
        }

        public static void setModifier(byte modifierId, byte playerId, byte flag) {
            PlayerControl player = Helpers.playerById(playerId); 
            switch ((RoleId)modifierId) {
                case RoleId.Bait:
                    Bait.bait.Add(player);
                    break;
                case RoleId.Lover:
                    if (flag == 0) Lovers.lover1 = player;
                    else Lovers.lover2 = player;
                    break;
                case RoleId.Bloody:
                    Bloody.bloody.Add(player);
                    break;
                case RoleId.AntiTeleport:
                    AntiTeleport.antiTeleport.Add(player);
                    break;
                case RoleId.Tiebreaker:
                    Tiebreaker.tiebreaker = player;
                    break;
                case RoleId.Sunglasses:
                    Sunglasses.sunglasses.Add(player);
                    break;
                case RoleId.Mini:
                    Mini.mini = player;
                    break;
                case RoleId.Vip:
                    Vip.vip.Add(player);
                    break;
                case RoleId.Cursed:
                    Cursed.cursed = player;
                    break;

                case RoleId.Slueth:
                    Slueth.slueth = player;
                    break;
                case RoleId.Disperser:
                    Disperser.disperser = player;
                    break;
                case RoleId.Radar:
                    Radar.radar = player;
                    break;
                case RoleId.AntiReport:
                    AntiReport.antiReport.Add(player);
                    break;
                case RoleId.AllKnowing:
                    AllKnowing.Allknowers.Add(player);
                    break;
                case RoleId.Viewer:
                    Viewer.viewer = player;
                    break;
                 
                case RoleId.Indomitable:
                    Indomitable.indomitable = player;
                    break;
                case RoleId.Paranoid:
                    Paranoid.paranoid = player;
                    break;
                case RoleId.Invert:
                    Invert.invert.Add(player);
                    break;
                case RoleId.Chameleon:
                    Chameleon.chameleon.Add(player);
                    break;
                case RoleId.Shifter:
                    Shifter.shifter = player;
                    break;
                case RoleId.Recruiter:
                    Recruiter.recruiter.Add(player);
                    break;
                case RoleId.Giant:
                    Giant.giant.Add(player);
                    break;
                case RoleId.Flash:
                    Flash.flash.Add(player);
                    break;
                case RoleId.OneTimeKiller:
                    OneTimeKiller.onetimekiller = player;
                    break;
            }
        }

        public static void versionHandshake(int major, int minor, int build, int revision, Guid guid, int clientId) {
            System.Version ver;
            if (revision < 0) 
                ver = new System.Version(major, minor, build);
            else 
                ver = new System.Version(major, minor, build, revision);
            GameStartManagerPatch.playerVersions[clientId] = new GameStartManagerPatch.PlayerVersion(ver, guid);
        }

        public static void useUncheckedVent(int ventId, byte playerId, byte isEnter) {
            PlayerControl player = Helpers.playerById(playerId);
            if (player == null) return;
            // Fill dummy MessageReader and call MyPhysics.HandleRpc as the corountines cannot be accessed
            MessageReader reader = new MessageReader();
            byte[] bytes = BitConverter.GetBytes(ventId);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            reader.Buffer = bytes;
            reader.Length = bytes.Length;

            JackInTheBox.startAnimation(ventId);
            player.MyPhysics.HandleRpc(isEnter != 0 ? (byte)19 : (byte)20, reader);
        }

        public static void uncheckedMurderPlayer(byte sourceId, byte targetId, byte showAnimation) {
            if (AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Started) return;
            PlayerControl source = Helpers.playerById(sourceId);
            PlayerControl target = Helpers.playerById(targetId);
            if (source != null && target != null) {
                if (showAnimation == 0) KillAnimationCoPerformKillPatch.hideNextAnimation = true;
                source.MurderPlayer(target);
            }
        }

        public static void uncheckedCmdReportDeadBody(byte sourceId, byte targetId) {
            PlayerControl source = Helpers.playerById(sourceId);
            var t = targetId == Byte.MaxValue ? null : Helpers.playerById(targetId).Data;
            if (source != null) source.ReportDeadBody(t);
        }

        public static void uncheckedExilePlayer(byte targetId) {
            PlayerControl target = Helpers.playerById(targetId);
            if (target != null) target.Exiled();
        }

        public static void dynamicMapOption(byte mapId) {
           GameOptionsManager.Instance.currentNormalGameOptions.MapId = mapId;
        }

        public static void setGameStarting() {
            GameStartManagerPatch.GameStartManagerUpdatePatch.startingTimer = 5f;
        }

        // Role functionality

        public static void engineerFixLights() {
            SwitchSystem switchSystem = MapUtilities.Systems[SystemTypes.Electrical].CastFast<SwitchSystem>();
            switchSystem.ActualSwitches = switchSystem.ExpectedSwitches;
        }

        public static void engineerFixSubmergedOxygen() {
            SubmergedCompatibility.RepairOxygen();
        }

        public static void engineerUsedRepair() {
            Engineer.remainingFixes--;
            if (Helpers.shouldShowGhostInfo()) {
                Helpers.showFlash(Engineer.color, 0.5f, "工程师修复"); ;
            }
        }
        
        public static void showIndomitableFlash() {
            if (Indomitable.indomitable  == CachedPlayer.LocalPlayer.PlayerControl) {
                Helpers.showFlash(Indomitable.color);
            }
        }

        public static void cleanBody(byte playerId, byte cleaningPlayerId)
        {
            if (Medium.futureDeadBodies != null)
            {
                var body = Medium.futureDeadBodies.Find(x => x.Item1.player.PlayerId == playerId);
                if (body != null && body.Item1 != null)
                {
                    var deadBody = body.Item1;
                    deadBody.wasCleaned = true;
                }
            }

            DeadBody[] array = UnityEngine.Object.FindObjectsOfType<DeadBody>();
            for (int i = 0; i < array.Length; i++)
            {
                var playerData = GameData.Instance.GetPlayerById(array[i].ParentId);
                if (playerData != null && playerData.PlayerId == playerId)
                {
                    UnityEngine.Object.Destroy(array[i].gameObject);
                }
            }

            if (Vulture.vulture != null && cleaningPlayerId == Vulture.vulture.PlayerId)
            {
                Vulture.eatenBodies++;
                if (Vulture.eatenBodies == Vulture.vultureNumberToWin)
                {
                    Vulture.triggerVultureWin = true;
                }
            }
        }


        public static void timeMasterRewindTime() {
            TimeMaster.shieldActive = false; // Shield is no longer active when rewinding
            SoundEffectsManager.stop("timemasterShield");  // Shield sound stopped when rewinding
            if(TimeMaster.timeMaster != null && TimeMaster.timeMaster == CachedPlayer.LocalPlayer.PlayerControl) {
                resetTimeMasterButton();
            }
            FastDestroyableSingleton<HudManager>.Instance.FullScreen.color = new Color(0f, 0.5f, 0.8f, 0.3f);
            FastDestroyableSingleton<HudManager>.Instance.FullScreen.enabled = true;
            FastDestroyableSingleton<HudManager>.Instance.FullScreen.gameObject.SetActive(true);
            FastDestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(TimeMaster.rewindTime / 2, new Action<float>((p) => {
                if (p == 1f) FastDestroyableSingleton<HudManager>.Instance.FullScreen.enabled = false;
            })));

            if (TimeMaster.timeMaster == null || CachedPlayer.LocalPlayer.PlayerControl == TimeMaster.timeMaster) return; // Time Master himself does not rewind

            TimeMaster.isRewinding = true;

            if (MapBehaviour.Instance)
                MapBehaviour.Instance.Close();
            if (Minigame.Instance)
                Minigame.Instance.ForceClose();
            CachedPlayer.LocalPlayer.PlayerControl.moveable = false;
        }

        public static void timeMasterShield() {
            TimeMaster.shieldActive = true;
            FastDestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(TimeMaster.shieldDuration, new Action<float>((p) => {
                if (p == 1f) TimeMaster.shieldActive = false;
            })));
        }

	public static void amnisiacTakeRole(byte targetId) {
	    PlayerControl target = Helpers.playerById(targetId);
	    PlayerControl amnisiac = Amnisiac.amnisiac;
            if (target == null || amnisiac == null) return;
            List<RoleInfo> targetInfo = RoleInfo.getRoleInfoForPlayer(target);
            RoleInfo roleInfo = targetInfo.Where(info => !info.isModifier).FirstOrDefault();
            switch((RoleId)roleInfo.roleId) {
                
                case RoleId.Crewmate:
                    Amnisiac.clearAndReload();
                    break;
                case RoleId.Impostor:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Jester:
                    if (Amnisiac.resetRole) Jester.clearAndReload();
                    Jester.jester = amnisiac;
                    Amnisiac.clearAndReload();
		    Amnisiac.amnisiac = target;
                    break;
                    
                case RoleId.Veteren:
                    if (Amnisiac.resetRole) Veteren.clearAndReload();
                    Veteren.veteren = amnisiac;
                    Amnisiac.clearAndReload();
                    break;
                    
                case RoleId.BodyGuard:
                    if (Amnisiac.resetRole) BodyGuard.clearAndReload();
                    BodyGuard.bodyguard = amnisiac;
                    Amnisiac.clearAndReload();
                    break;
                    
                case RoleId.Werewolf:
                    if (Amnisiac.resetRole) Werewolf.clearAndReload();
                    Werewolf.werewolf = amnisiac;
                    Amnisiac.clearAndReload();
                    Amnisiac.amnisiac = target;
                    break;
                case RoleId.Juggernaut:
                    if (Amnisiac.resetRole) Juggernaut.clearAndReload();
                    Juggernaut.juggernaut = amnisiac;
                    Amnisiac.clearAndReload();
                    Amnisiac.amnisiac = target;
                    break;
                case RoleId.Doomsayer:
                    if (Amnisiac.resetRole) Doomsayer.clearAndReload();
                    Doomsayer.doomsayer = amnisiac;
                    Amnisiac.clearAndReload();
                    Amnisiac.amnisiac = target;
                    break;
                case RoleId.Kataomoi:
                    if (Amnisiac.resetRole) Kataomoi.clearAndReload();
                    Kataomoi.kataomoi = amnisiac;
                    Amnisiac.clearAndReload();
                    Amnisiac.amnisiac = target;
                    break;
                case RoleId.Akujo:
                    if (Amnisiac.resetRole) Akujo.clearAndReload();
                    Akujo.akujo = amnisiac;
                    Amnisiac.clearAndReload();
                    Amnisiac.amnisiac = target;
                    break;
                case RoleId.Cerenovus:
                    if (Amnisiac.resetRole) Cerenovus.clearAndReload();
                    Cerenovus.cerenovus = amnisiac;
                    Amnisiac.clearAndReload();
                    Amnisiac.amnisiac = target;
                    break;


                case RoleId.Mayor:
                    if (Amnisiac.resetRole) Mayor.clearAndReload();
                    Mayor.mayor = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Portalmaker:
                    if (Amnisiac.resetRole) Portalmaker.clearAndReload();
                    Portalmaker.portalmaker = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Engineer:
                    if (Amnisiac.resetRole) Engineer.clearAndReload();
                    Engineer.engineer = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Sheriff:
		    // Never reload Sheriff
                    if (Sheriff.formerDeputy != null && Sheriff.formerDeputy == Sheriff.sheriff) Sheriff.formerDeputy = amnisiac; // Ensure amni gets handcuffs
                    Sheriff.sheriff = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Deputy:
                    if (Amnisiac.resetRole) Deputy.clearAndReload();
                    Deputy.deputy = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Lighter:
                    if (Amnisiac.resetRole) Lighter.clearAndReload();
                    Lighter.lighter = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Godfather:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Godfather.clearAndReload();
                    Godfather.godfather = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Mafioso:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Mafioso.clearAndReload();
                    Mafioso.mafioso = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Janitor:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Janitor.clearAndReload();
                    Janitor.janitor = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Detective:
                    if (Amnisiac.resetRole) Detective.clearAndReload();
                    Detective.detective = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.TimeMaster:
                    if (Amnisiac.resetRole) TimeMaster.clearAndReload();
                    TimeMaster.timeMaster = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Transporter:
                    if (Amnisiac.resetRole) Transporter.clearAndReload();
                    Transporter.transporter = amnisiac;
                    Amnisiac.clearAndReload();
                    break;
                case RoleId.Teleporter:
                    if (Amnisiac.resetRole) Teleporter.clearAndReload();
                    Teleporter.teleporter = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Medic:
                    if (Amnisiac.resetRole) Medic.clearAndReload();
                    Medic.medic = amnisiac;
                    Amnisiac.clearAndReload();
                    break;


                case RoleId.Swapper:
                    if (Amnisiac.resetRole) Swapper.clearAndReload();
                    Swapper.swapper = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Seer:
                    if (Amnisiac.resetRole) Seer.clearAndReload();
                    Seer.seer = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Morphling:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Morphling.clearAndReload();
                    Morphling.morphling = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Camouflager:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Camouflager.clearAndReload();
                    Camouflager.camouflager = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Hacker:
                    if (Amnisiac.resetRole) Hacker.clearAndReload();
                    Hacker.hacker = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Tracker:
                    if (Amnisiac.resetRole) Tracker.clearAndReload();
                    Tracker.tracker = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Vampire:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Vampire.clearAndReload();
                    Vampire.vampire = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Snitch:
                    if (Amnisiac.resetRole) Snitch.clearAndReload();
                    Snitch.snitch = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Jackal:
                    Jackal.jackal = amnisiac;
		    Jackal.formerJackals.Add(target);
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Sidekick:
		    Jackal.formerJackals.Add(target);
                    if (Amnisiac.resetRole) Sidekick.clearAndReload();
                    Sidekick.sidekick = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Swooper:
                    if (Amnisiac.resetRole) Swooper.clearAndReload();
                    Swooper.swooper = amnisiac;
                    Amnisiac.clearAndReload();
                    Amnisiac.amnisiac = target;                 
                    break;

                case RoleId.Eraser:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Eraser.clearAndReload();
                    Eraser.eraser = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Spy:
                    if (Amnisiac.resetRole) Spy.clearAndReload();
                    Spy.spy = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Trickster:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Trickster.clearAndReload();
                    Trickster.trickster = amnisiac;
                    Amnisiac.clearAndReload();
                    break;
					
                case RoleId.Poucher:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Poucher.clearAndReload(false);
                    Poucher.poucher = amnisiac;
                    Amnisiac.clearAndReload();
                    break;
                case RoleId.Mimic:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Mimic.clearAndReload(false);
                    Mimic.mimic = amnisiac;
                    Amnisiac.clearAndReload();
                    break;
                case RoleId.Escapist:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Escapist.clearAndReload();
                    Escapist.escapist = amnisiac;
                    Amnisiac.clearAndReload();
                    break;
                 case RoleId.Pitfaller:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Pitfaller.clearAndReload();
                    Pitfaller.pitfaller = amnisiac;
                    Amnisiac.clearAndReload();
                    break;
                case RoleId.Shinobi:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Shinobi.clearAndReload();
                    Shinobi.shinobi = amnisiac;
                    Amnisiac.clearAndReload();
                    break;
                case RoleId.Snarer:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Snarer.clearAndReload();
                    Snarer.snarer = amnisiac;
                    Amnisiac.clearAndReload();
                    break;
                case RoleId.Transportist:
                    if (Amnisiac.resetRole) Transportist.clearAndReload();
                    Transportist.transportist = amnisiac;
                    Amnisiac.clearAndReload();
                    break;
                 case RoleId.Miner:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Miner.clearAndReload();
                    Miner.miner = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Cleaner:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Cleaner.clearAndReload();
                    Cleaner.cleaner = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Warlock:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Warlock.clearAndReload();
                    Warlock.warlock = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.SecurityGuard:
                    if (Amnisiac.resetRole) SecurityGuard.clearAndReload();
                    SecurityGuard.securityGuard = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Arsonist:
                    if (Amnisiac.resetRole) Arsonist.clearAndReload();
                    Arsonist.arsonist = amnisiac;
                    Amnisiac.clearAndReload();
                    Amnisiac.amnisiac = target;

                    if (CachedPlayer.LocalPlayer.PlayerControl == Arsonist.arsonist)
                    {
                        int playerCounter = 0;
                        Vector3 bottomLeft = new Vector3(-FastDestroyableSingleton<HudManager>.Instance.UseButton.transform.localPosition.x, FastDestroyableSingleton<HudManager>.Instance.UseButton.transform.localPosition.y, FastDestroyableSingleton<HudManager>.Instance.UseButton.transform.localPosition.z);
                        foreach (PlayerControl p in CachedPlayer.AllPlayers)
                        {
                            if (TORMapOptions.playerIcons.ContainsKey(p.PlayerId) && p != Arsonist.arsonist)
                            {
                                //Arsonist.poolIcons.Add(p);
                                if (Arsonist.dousedPlayers.Contains(p))
                                {
                                    TORMapOptions.playerIcons[p.PlayerId].setSemiTransparent(false);
                                }
                                else
                                {
                                    TORMapOptions.playerIcons[p.PlayerId].setSemiTransparent(true);
                                }

                                TORMapOptions.playerIcons[p.PlayerId].transform.localPosition = bottomLeft + new Vector3(-0.25f, -0.25f, 0) + Vector3.right * playerCounter++ * 0.35f;
                                TORMapOptions.playerIcons[p.PlayerId].transform.localScale = Vector3.one * 0.2f;
                                TORMapOptions.playerIcons[p.PlayerId].gameObject.SetActive(true);
                            }
                        }
                    }
                    break;

                case RoleId.EvilGuesser:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
		    // Never Reload Guesser
                    Guesser.evilGuesser = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.NiceGuesser:
		    // Never Reload Guesser
                    Guesser.niceGuesser = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.BountyHunter:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) BountyHunter.clearAndReload();
                    BountyHunter.bountyHunter = amnisiac;
                    Amnisiac.clearAndReload();

                    BountyHunter.bountyUpdateTimer = 0f;
                    if (CachedPlayer.LocalPlayer.PlayerControl == BountyHunter.bountyHunter)
                    {
                        Vector3 bottomLeft = new Vector3(-FastDestroyableSingleton<HudManager>.Instance.UseButton.transform.localPosition.x, FastDestroyableSingleton<HudManager>.Instance.UseButton.transform.localPosition.y, FastDestroyableSingleton<HudManager>.Instance.UseButton.transform.localPosition.z) + new Vector3(-0.25f, 1f, 0);
                        BountyHunter.cooldownText = UnityEngine.Object.Instantiate<TMPro.TextMeshPro>(FastDestroyableSingleton<HudManager>.Instance.KillButton.cooldownTimerText, FastDestroyableSingleton<HudManager>.Instance.transform);
                        BountyHunter.cooldownText.alignment = TMPro.TextAlignmentOptions.Center;
                        BountyHunter.cooldownText.transform.localPosition = bottomLeft + new Vector3(0f, -1f, -1f);
                        BountyHunter.cooldownText.gameObject.SetActive(true);

                        foreach (PlayerControl p in CachedPlayer.AllPlayers)
                        {
                            if (TORMapOptions.playerIcons.ContainsKey(p.PlayerId))
                            {
                                TORMapOptions.playerIcons[p.PlayerId].setSemiTransparent(false);
                                TORMapOptions.playerIcons[p.PlayerId].transform.localPosition = bottomLeft + new Vector3(0f, -1f, 0);
                                TORMapOptions.playerIcons[p.PlayerId].transform.localScale = Vector3.one * 0.4f;
                                TORMapOptions.playerIcons[p.PlayerId].gameObject.SetActive(false);
                            }
                        }
                    }
                    break;

                case RoleId.Vulture:
                    if (Amnisiac.resetRole) Vulture.clearAndReload();
                    Vulture.vulture = amnisiac;
                    Amnisiac.clearAndReload();
		    Amnisiac.amnisiac = target;
                    break;

                case RoleId.Medium:
                    if (Amnisiac.resetRole) Medium.clearAndReload();
                    Medium.medium = amnisiac;
                    Amnisiac.clearAndReload();
                    break;


                case RoleId.Trapper:
                    if (Amnisiac.resetRole) Trapper.clearAndReload();
                    Trapper.trapper = amnisiac;
                    Amnisiac.clearAndReload();
                    break;
   
                case RoleId.Thief:
                    if (Amnisiac.resetRole) Thief.clearAndReload();
                    Thief.thief = amnisiac;
                    Amnisiac.clearAndReload();
                    Amnisiac.amnisiac = target;
                    break;                    

                case RoleId.Lawyer:
                    // Never reset Lawyer
                    Lawyer.lawyer = amnisiac;
                    Amnisiac.clearAndReload();
   		            Amnisiac.amnisiac = target;
                    break;
                case RoleId.Prosecutor:
                    // Never reset Lawyer
                    Lawyer.lawyer = amnisiac;
                    Lawyer.isProsecutor = true;
                    Amnisiac.clearAndReload();
   		            Amnisiac.amnisiac = target;
                    break;


                case RoleId.Pursuer:
                    if (Amnisiac.resetRole) Pursuer.clearAndReload();
                    Pursuer.pursuer = amnisiac;
                    Amnisiac.clearAndReload();
                    Amnisiac.amnisiac = target;
                    break;

                case RoleId.Witch:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Witch.clearAndReload();
                    Witch.witch = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Ninja:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Ninja.clearAndReload();
                    Ninja.ninja = amnisiac;
                    Amnisiac.clearAndReload();
                    break;

                case RoleId.Bomber:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Bomber.clearAndReload();
                    Bomber.bomber = amnisiac;
                    Amnisiac.clearAndReload();
                    break;
                case RoleId.Yoyo:
                    Helpers.turnToImpostor(Amnisiac.amnisiac);
                    if (Amnisiac.resetRole) Yoyo.clearAndReload();
                    Yoyo.yoyo = amnisiac;
                    Amnisiac.clearAndReload();
                    break;


	    }
	}

         public static void mimicMimicRole(byte targetId) {
            PlayerControl target = Helpers.playerById(targetId);
                if (target == null || Mimic.mimic == null) return;
                List<RoleInfo> targetInfo = RoleInfo.getRoleInfoForPlayer(target);
                RoleInfo roleInfo = targetInfo.Where(info => !info.isModifier).FirstOrDefault();
                switch((RoleId)roleInfo.roleId) {

                case RoleId.BodyGuard:
                    if (Amnisiac.resetRole) BodyGuard.clearAndReload();
                    BodyGuard.bodyguard = Mimic.mimic;
					bodyGuardGuardButton.PositionOffset = CustomButton.ButtonPositions.upperRowLeft;
					Mimic.hasMimic = true;
                    break;

                case RoleId.Transporter:
                    if (Amnisiac.resetRole) Transporter.clearAndReload();
                    Transporter.transporter = Mimic.mimic;
					transporterButton.PositionOffset = CustomButton.ButtonPositions.upperRowLeft;
					Mimic.hasMimic = true;
                    break;
                case RoleId.Teleporter:
                    if (Amnisiac.resetRole) Teleporter.clearAndReload();
                    Teleporter.teleporter = Mimic.mimic;
					teleporterTeleportButton.PositionOffset = CustomButton.ButtonPositions.upperRowLeft;
                    teleporterSampleButton.PositionOffset = CustomButton.ButtonPositions.upperRowFarLeft;
					Mimic.hasMimic = true;
                    break;
                case RoleId.Arbiter:
                    if (Amnisiac.resetRole) Arbiter.clearAndReload();
                    Arbiter.arbiter = Mimic.mimic;
					Mimic.hasMimic = true;
                    break;
                    

                case RoleId.Mayor:
                    if (Amnisiac.resetRole) Mayor.clearAndReload();
                    Mayor.mayor = Mimic.mimic;
					mayorMeetingButton.PositionOffset = CustomButton.ButtonPositions.upperRowLeft;

					Mimic.hasMimic = true;
                    break;

                case RoleId.Trapper:
                    if (Amnisiac.resetRole) Trapper.clearAndReload();
                    Trapper.trapper = Mimic.mimic;
					trapperButton.PositionOffset = CustomButton.ButtonPositions.upperRowLeft;
					Mimic.hasMimic = true;
                    break;

                case RoleId.Portalmaker:
                    if (Amnisiac.resetRole) Portalmaker.clearAndReload();
                    Portalmaker.portalmaker = Mimic.mimic;
					portalmakerPlacePortalButton.PositionOffset = CustomButton.ButtonPositions.upperRowLeft;
					Mimic.hasMimic = true;
                    break;

                case RoleId.Engineer:
                    if (Amnisiac.resetRole) Engineer.clearAndReload();
                    Engineer.engineer = Mimic.mimic;
					engineerRepairButton.PositionOffset = CustomButton.ButtonPositions.upperRowLeft;
					Mimic.hasMimic = true;
                    break;


                case RoleId.Detective:
                    if (Amnisiac.resetRole) Detective.clearAndReload();
                    Detective.detective = Mimic.mimic;
					Mimic.hasMimic = true;
                    break;

                case RoleId.TimeMaster:
                    if (Amnisiac.resetRole) TimeMaster.clearAndReload();
                    TimeMaster.timeMaster = Mimic.mimic;
					timeMasterShieldButton.PositionOffset = CustomButton.ButtonPositions.upperRowLeft;
					Mimic.hasMimic = true;
                    break;

                case RoleId.Veteren:
                    if (Amnisiac.resetRole) Veteren.clearAndReload();
                    Veteren.veteren = Mimic.mimic;
					veterenAlertButton.PositionOffset = CustomButton.ButtonPositions.upperRowLeft;
					Mimic.hasMimic = true;
                    break;

                case RoleId.Medic:
                    if (Amnisiac.resetRole) Medic.clearAndReload();
                    Medic.medic = Mimic.mimic;
					medicShieldButton.PositionOffset = CustomButton.ButtonPositions.upperRowLeft;
					Mimic.hasMimic = true;
                    break;

                case RoleId.Swapper:
                    if (Amnisiac.resetRole) Swapper.clearAndReload();
                    Swapper.swapper = Mimic.mimic;
					Mimic.hasMimic = true;
                    break;

                case RoleId.Seer:
                    if (Amnisiac.resetRole) Seer.clearAndReload();
                    Seer.seer = Mimic.mimic;
					Mimic.hasMimic = true;
                    break;

                case RoleId.Hacker:
                    if (Amnisiac.resetRole) Hacker.clearAndReload();
                    Hacker.hacker = Mimic.mimic;
					hackerAdminTableButton.PositionOffset = CustomButton.ButtonPositions.upperRowFarLeft;
					hackerVitalsButton.PositionOffset = CustomButton.ButtonPositions.lowerRowFarLeft;
					hackerButton.PositionOffset = CustomButton.ButtonPositions.upperRowLeft;

					Mimic.hasMimic = true;
                    break;

                case RoleId.Tracker:
                    if (Amnisiac.resetRole) Tracker.clearAndReload();
                    Tracker.tracker = Mimic.mimic;
					trackerTrackPlayerButton.PositionOffset = CustomButton.ButtonPositions.upperRowLeft;

					Mimic.hasMimic = true;
                    break;

                case RoleId.SecurityGuard:
                    if (Amnisiac.resetRole) SecurityGuard.clearAndReload();
                    SecurityGuard.securityGuard = Mimic.mimic;
					securityGuardButton.PositionOffset = CustomButton.ButtonPositions.upperRowLeft;
					securityGuardCamButton.PositionOffset = CustomButton.ButtonPositions.upperRowLeft;

					Mimic.hasMimic = true;
                    break;

                case RoleId.Medium:
                    if (Amnisiac.resetRole) Medium.clearAndReload();
                    Medium.medium = Mimic.mimic;
					mediumButton.PositionOffset = CustomButton.ButtonPositions.upperRowLeft;
					Mimic.hasMimic = true;
                    break;

                case RoleId.Lighter:
                    if (Amnisiac.resetRole) Lighter.clearAndReload();
                    Lighter.lighter = Mimic.mimic;
					Mimic.hasMimic = true;
                    break;

                case RoleId.NiceGuesser:
                    if (Amnisiac.resetRole) Guesser.clearAndReload();
                    Guesser.niceGuesser = Mimic.mimic;
					Mimic.hasMimic = true;
                    break;

                case RoleId.Sheriff:
                    if (Amnisiac.resetRole) Sheriff.clearAndReload();
                    Sheriff.sheriff = Mimic.mimic;
                    sheriffKillButton.PositionOffset = CustomButton.ButtonPositions.upperRowFarLeft;
					Mimic.hasMimic = true;
                    break;

                case RoleId.Deputy:
                    if (Amnisiac.resetRole) Deputy.clearAndReload();
                    Deputy.deputy = Mimic.mimic;
                    deputyHandcuffButton.PositionOffset = CustomButton.ButtonPositions.upperRowLeft;
					Mimic.hasMimic = true;
                    break;

               
            }
    }


         public static void setPositionESC(byte playerId, float x, float y) {
            PlayerControl target = Helpers.playerById(playerId);
            target.transform.localPosition = new Vector3(x, y, 0);
            target.transform.position = new Vector3(x, y, 0);
        }

        public static void pitfallBody (byte playerId) {
            DeadBody[] array = UnityEngine.Object.FindObjectsOfType<DeadBody>();
            for (int i = 0; i < array.Length; i++) {
                if (GameData.Instance.GetPlayerById(array[i].ParentId).PlayerId == playerId)
                    Pitfaller.pitfallbody = Helpers.playerById(playerId);
            }
        }
        public static void shinobiExpand() {
            Shinobi.shinobiExpand();
        }

        public static void arbiterSpecialVote(byte playerid, byte targetid)
        {
            if (!MeetingHud.Instance) return;
            if (!Arbiter.isArbiter(playerid)) return;
            PlayerControl target = Helpers.playerById(targetid);
            if (target == null) return;
            Arbiter.specialVoteTargetPlayerId = targetid;
            Arbiter.remainingSpecialVotes(true);
        }

        public static void arbiterSpecialVote_DoCastVote()
        {
            if (!MeetingHud.Instance) return;
            if (!Arbiter.isArbiter(CachedPlayer.LocalPlayer.PlayerControl.PlayerId)) return;
            PlayerControl target = Helpers.playerById(Arbiter.specialVoteTargetPlayerId);
            if (target == null) return;
            MeetingHud.Instance.CmdCastVote(CachedPlayer.LocalPlayer.PlayerControl.PlayerId, target.PlayerId);
        }       

        public static void kataomoiSetTarget(byte playerId) {
            Kataomoi.target = Helpers.playerById(playerId);
        }

        public static void kataomoiWin() {
            if (Kataomoi.kataomoi == null) return;

            Kataomoi.triggerKataomoiWin = true;
            if (Kataomoi.target != null)
                Kataomoi.target.Exiled();
        }

        public static void kataomoiStalking(byte playerId) {
            PlayerControl player = Helpers.playerById(playerId);
            if (Kataomoi.kataomoi == null || Kataomoi.kataomoi != player) return;

            Kataomoi.doStalking();
        }

        public static void updateMeeting(byte targetId)
        {
            if (MeetingHud.Instance)
            {
                foreach (PlayerVoteArea pva in MeetingHud.Instance.playerStates)
                {
                    if (pva.TargetPlayerId == targetId)
                    {
                        pva.SetDead(pva.DidReport, true);
                        pva.Overlay.gameObject.SetActive(true);
                    }

                    // Give players back their vote if target is shot dead
                    if (pva.VotedFor != targetId) continue;
                    pva.UnsetVote();
                    var voteAreaPlayer = Helpers.playerById(pva.TargetPlayerId);
                    if (!voteAreaPlayer.AmOwner) continue;
                    MeetingHud.Instance.ClearVote();
                }

                if (AmongUsClient.Instance.AmHost)
                    MeetingHud.Instance.CheckForEndVoting();
            }
        }

        public static void akujoSetHonmei(byte akujoId, byte targetId)
        {
            PlayerControl akujo = Helpers.playerById(akujoId);
            PlayerControl target = Helpers.playerById(targetId);

            if (akujo != null && Akujo.honmei == null)
            {
                Akujo.honmei = target;
                Akujo.breakLovers(target);
            }
        }

        public static void akujoSetKeep(byte akujoId, byte targetId)
        {
            var akujo = Helpers.playerById(akujoId);
            PlayerControl target = Helpers.playerById(targetId);

            if (akujo != null && Akujo.keepsLeft > 0)
            {
                Akujo.keeps.Add(target);
                Akujo.breakLovers(target);
                Akujo.keepsLeft--;
            }
        }

        public static void akujoSuicide(byte akujoId)
        {
            var akujo = Helpers.playerById(akujoId);
            if (akujo != null)
            {
                akujo.MurderPlayer(akujo, MurderResultFlags.Succeeded);
                GameHistory.overrideDeathReasonAndKiller(akujo, DeadPlayer.CustomDeathReason.Loneliness);
                if (MeetingHud.Instance) updateMeeting(akujoId);
            }
        }

        public static void setBrainwash(byte playerId)
        {
            var p = Helpers.playerById(playerId);
            Cerenovus.target = p;
            Cerenovus.brainwashed.Add(p);
        }

        public static void cerenovusKill(byte targetId)
        {
            PlayerControl target = Helpers.playerById(targetId);
            GameHistory.overrideDeathReasonAndKiller(target, DeadPlayer.CustomDeathReason.BrainwashedKilled, Cerenovus.cerenovus);
            if (PlayerControl.LocalPlayer == Cerenovus.target)
            {
                if (Constants.ShouldPlaySfx()) SoundManager.Instance.PlaySound(target.KillSfx, false, 0.8f);
            }
            Cerenovus.counter += 1;
            if (Cerenovus.numberToWin == Cerenovus.counter) Cerenovus.triggerCerenovusWin = true;
        }



        public static void teleporterTeleport(byte target1Id, byte target2Id)
        {
            PlayerControl target1 = Helpers.playerById(target1Id);
            PlayerControl target2 = Helpers.playerById(target2Id);

            target1.MyPhysics.ResetMoveState();
            target2.MyPhysics.ResetMoveState();
            var targetPosition = target1.GetTruePosition();
            var TempFacing = target1.cosmetics.currentBodySprite.BodySprite.flipX;
            target1.NetTransform.RpcSnapTo(new Vector2(target2.GetTruePosition().x, target2.GetTruePosition().y + 0.3636f));
            target1.cosmetics.currentBodySprite.BodySprite.flipX = target2.cosmetics.currentBodySprite.BodySprite.flipX;
            target2.NetTransform.RpcSnapTo(new Vector2(targetPosition.x, targetPosition.y + 0.3636f));
            target2.cosmetics.currentBodySprite.BodySprite.flipX = TempFacing;

            if (CachedPlayer.LocalPlayer.PlayerControl == target1 || CachedPlayer.LocalPlayer.PlayerControl == target2)
            {
                Helpers.showFlash(Teleporter.color);
                if (Minigame.Instance) Minigame.Instance.Close();
            }
        }

         public static void TransporterSwap(byte playerId) {
        
            PlayerControl target = Helpers.playerById(playerId);
            if (Transporter.transporter == null || target == null) return;
            Transporter.sampledTarget = target;
            Transporter.TransportPlayers(target);
        }
        public static void TransportistSwap(byte playerId) {
        
            PlayerControl target = Helpers.playerById(playerId);
            if (Transportist.transportist == null || target == null) return;
            Transportist.sampledTarget = target;
            Transportist.TransportistPlayers(target);
        }
        public static void activateSnare(byte snareId, byte snarerId, byte playerId)
        {
            var snarer = Helpers.playerById(snarerId);
            var player = Helpers.playerById(playerId);
            Snare.activateSnare(snareId, snarer, player);
        }

        public static void disableSnare(byte snareId)
        {
            Snare.disableSnare(snareId);
        }

        public static void placeSnare(byte[] buff)
        {
            Vector3 pos = Vector3.zero;
            pos.x = BitConverter.ToSingle(buff, 0 * sizeof(float));
            pos.y = BitConverter.ToSingle(buff, 1 * sizeof(float)) - 0.2f;
            Snare snare = new(pos);
        }

        public static void snarerKill(byte snareId, byte snarerId, byte playerId)
        {
            var snarer = Helpers.playerById(snarerId);
            var target = Helpers.playerById(playerId);
            Snare.snareKill(snareId, snarer, target);
        }

        public static void clearSnare()
        {
            Snare.clearAllSnares();
        }

        public static void snarerMeetingFlag()
        {
            Snare.onMeeting();
        }




        public static void turnToImpostor(byte targetId) {
            PlayerControl player = Helpers.playerById(targetId);
            if (Lawyer.target == player && Lawyer.isProsecutor && Lawyer.lawyer != null && !Lawyer.lawyer.Data.IsDead) Lawyer.isProsecutor = false;
            erasePlayerRoles(player.PlayerId, true);
            Helpers.turnToImpostor(player);
        }
        
        public static void veterenAlert() {
            Veteren.alertActive = true;
            FastDestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(Veteren.alertDuration, new Action<float>((p) => {
                if (p == 1f) Veteren.alertActive = false;
            })));
        }

        public static void veterenKill(byte targetId) {
	  if (PlayerControl.LocalPlayer == Veteren.veteren) {
            PlayerControl player = Helpers.playerById(targetId);
  	    Helpers.checkMuderAttemptAndKill(Veteren.veteren, player);
          }
        }

        public static void medicSetShielded(byte shieldedId) {
            Medic.usedShield = true;
            Medic.shielded = Helpers.playerById(shieldedId);
            Medic.futureShielded = null;
        }

        public static void shieldedMurderAttempt() {
            if (Medic.shielded == null || Medic.medic == null) return;
            
            bool isShieldedAndShow = Medic.shielded == CachedPlayer.LocalPlayer.PlayerControl && Medic.showAttemptToShielded;
            isShieldedAndShow = isShieldedAndShow && (Medic.meetingAfterShielding || !Medic.showShieldAfterMeeting);  // Dont show attempt, if shield is not shown yet
            bool isMedicAndShow = Medic.medic == CachedPlayer.LocalPlayer.PlayerControl && Medic.showAttemptToMedic;

            if (isShieldedAndShow || isMedicAndShow || Helpers.shouldShowGhostInfo()) Helpers.showFlash(Palette.ImpostorRed, duration: 0.5f, "刀到了一个盾");
        }

        public static void shifterShift(byte targetId) {
            PlayerControl oldShifter = Shifter.shifter;
            PlayerControl player = Helpers.playerById(targetId);
            if (player == null || oldShifter == null) return;

            Shifter.futureShift = null;
            Shifter.clearAndReload();

            // Suicide (exile) when impostor or impostor variants
            if ((player.Data.Role.IsImpostor || player == Swooper.swooper || player == Werewolf.werewolf || player == Juggernaut.juggernaut || Helpers.isNeutral(player)) && !oldShifter.Data.IsDead) {
                oldShifter.Exiled();
                GameHistory.overrideDeathReasonAndKiller(oldShifter, DeadPlayer.CustomDeathReason.Shift, player);
                if (oldShifter == Lawyer.target && AmongUsClient.Instance.AmHost && Lawyer.lawyer != null) {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.LawyerPromotesToPursuer, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.lawyerPromotesToPursuer();
                }
                return;
            }
            
            Shifter.shiftRole(oldShifter, player);

            // Set cooldowns to max for both players
            if (CachedPlayer.LocalPlayer.PlayerControl == oldShifter || CachedPlayer.LocalPlayer.PlayerControl == player)
                CustomButton.ResetAllCooldowns();
        }

        public static void swapperSwap(byte playerId1, byte playerId2) {
            if (MeetingHud.Instance) {
                Swapper.playerId1 = playerId1;
                Swapper.playerId2 = playerId2;
            }
        }

        public static void morphlingMorph(byte playerId) {  
            PlayerControl target = Helpers.playerById(playerId);
            if (Morphling.morphling == null || target == null) return;

            Morphling.morphTimer = Morphling.duration;
            Morphling.morphTarget = target;
            if (Camouflager.camouflageTimer <= 0f  && !Helpers.isActiveCamoComms())
                Morphling.morphling.setLook(target.Data.PlayerName, target.Data.DefaultOutfit.ColorId, target.Data.DefaultOutfit.HatId, target.Data.DefaultOutfit.VisorId, target.Data.DefaultOutfit.SkinId, target.Data.DefaultOutfit.PetId);
        }

        public static void camouflagerCamouflage(byte setTimer) {
            if (Helpers.isActiveCamoComms() && setTimer != 2) return;
            if (Helpers.isCamoComms()) Camouflager.camoComms = true;

            if (Camouflager.camouflager == null && !Camouflager.camoComms) return;
            if (setTimer == 1) Camouflager.camouflageTimer = Camouflager.duration;

            if (Helpers.MushroomSabotageActive()) return; // Dont overwrite the fungle "camo"
            foreach (PlayerControl player in CachedPlayer.AllPlayers)
                player.setLook("", 6, "", "", "", "");

        }

        public static void vampireSetBitten(byte targetId, byte performReset) {
            if (performReset != 0) {
                Vampire.bitten = null;
                return;
            }

            if (Vampire.vampire == null) return;
            foreach (PlayerControl player in CachedPlayer.AllPlayers) {
                if (player.PlayerId == targetId && !player.Data.IsDead) {
                        Vampire.bitten = player;
                }
            }
        }

        public static void placeGarlic(byte[] buff) {
            Vector3 position = Vector3.zero;
            position.x = BitConverter.ToSingle(buff, 0*sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1*sizeof(float));
            new Garlic(position);
        }

        public static void trackerUsedTracker(byte targetId) {
            Tracker.usedTracker = true;
            foreach (PlayerControl player in CachedPlayer.AllPlayers)
                if (player.PlayerId == targetId)
                    Tracker.tracked = player;
        }

        public static void deputyUsedHandcuffs(byte targetId)
        {
            Deputy.remainingHandcuffs--;
            Deputy.handcuffedPlayers.Add(targetId);
        }

        public static void deputyPromotes()
        {
            if (Deputy.deputy != null) {  // Deputy should never be null here, but there appeared to be a race condition during testing, which was removed.
                Sheriff.replaceCurrentSheriff(Deputy.deputy);
                Sheriff.formerDeputy = Deputy.deputy;
                Deputy.deputy = null;
                // No clear and reload, as we need to keep the number of handcuffs left etc
            }
        }

        public static void jackalCreatesSidekick(byte targetId) {
            PlayerControl player = Helpers.playerById(targetId);
            if (player == null) return;
            if (Lawyer.target == player && Lawyer.isProsecutor && Lawyer.lawyer != null && !Lawyer.lawyer.Data.IsDead) Lawyer.isProsecutor = false;

            if ((player.Data.Role.IsImpostor) || player == Werewolf.werewolf || player == Swooper.swooper || player == Juggernaut.juggernaut || player == Cerenovus.cerenovus) {
                Jackal.fakeSidekick = player;
            } else {
                bool wasSpy = Spy.spy != null && player == Spy.spy;
                bool wasImpostor = player.Data.Role.IsImpostor;  // This can only be reached if impostors can be sidekicked.
                FastDestroyableSingleton<RoleManager>.Instance.SetRole(player, RoleTypes.Crewmate);
                if (player == Lawyer.lawyer && Lawyer.target != null)
                {
                    Transform playerInfoTransform = Lawyer.target.cosmetics.nameText.transform.parent.FindChild("Info");
                    TMPro.TextMeshPro playerInfo = playerInfoTransform != null ? playerInfoTransform.GetComponent<TMPro.TextMeshPro>() : null;
                    if (playerInfo != null) playerInfo.text = "";
                }
                erasePlayerRoles(player.PlayerId, true);
                Sidekick.sidekick = player;
                if (player.PlayerId == CachedPlayer.LocalPlayer.PlayerId) CachedPlayer.LocalPlayer.PlayerControl.moveable = true;
                if (wasSpy || wasImpostor) Sidekick.wasTeamRed = true;
                Sidekick.wasSpy = wasSpy;
                Sidekick.wasImpostor = wasImpostor;
                if (player == CachedPlayer.LocalPlayer.PlayerControl) SoundEffectsManager.play("jackalSidekick");
                if (HandleGuesser.isGuesserGm && CustomOptionHolder.guesserGamemodeSidekickIsAlwaysGuesser.getBool() && !HandleGuesser.isGuesser(targetId))
                    setGuesserGm(targetId);
            }
            Jackal.canCreateSidekick = false;
        }

        public static void sidekickPromotes() {
            Jackal.removeCurrentJackal();
            Jackal.jackal = Sidekick.sidekick;
            Jackal.canCreateSidekick = Jackal.jackalPromotedFromSidekickCanCreateSidekick;
            Jackal.wasTeamRed = Sidekick.wasTeamRed;
            Jackal.wasSpy = Sidekick.wasSpy;
            Jackal.wasImpostor = Sidekick.wasImpostor;
            Sidekick.clearAndReload();
            return;
        }
        
        public static void erasePlayerRoles(byte playerId, bool ignoreModifier = true) {
            PlayerControl player = Helpers.playerById(playerId);
            if (player == null || !player.canBeErased()) return;

            // Crewmate roles
            if (player == Mayor.mayor) Mayor.clearAndReload();
            if (player == Portalmaker.portalmaker) Portalmaker.clearAndReload();
            if (player == Engineer.engineer) Engineer.clearAndReload();
            if (player == Sheriff.sheriff) Sheriff.clearAndReload();
            if (player == Deputy.deputy) Deputy.clearAndReload();
            if (player == Lighter.lighter) Lighter.clearAndReload();
            if (player == Detective.detective) Detective.clearAndReload();
            if (player == TimeMaster.timeMaster) TimeMaster.clearAndReload();
            if (player == Veteren.veteren) Veteren.clearAndReload();
            if (player == Medic.medic) Medic.clearAndReload();
            if (player == Shifter.shifter) Shifter.clearAndReload();
            if (player == Seer.seer) Seer.clearAndReload();
            if (player == Hacker.hacker) Hacker.clearAndReload();
            if (player == BodyGuard.bodyguard) BodyGuard.clearAndReload();
            if (player == Arbiter.arbiter ) Arbiter.clearAndReload();
            if (player == Tracker.tracker) Tracker.clearAndReload();
            if (player == Snitch.snitch) Snitch.clearAndReload();
            if (player == Swapper.swapper) Swapper.clearAndReload();
            if (player == Spy.spy) Spy.clearAndReload();
            if (player == SecurityGuard.securityGuard) SecurityGuard.clearAndReload();
            if (player == Medium.medium) Medium.clearAndReload();
            if (player == Trapper.trapper) Trapper.clearAndReload();
            if (player == Transporter.transporter) Transporter.clearAndReload();
            if (player == Teleporter.teleporter) Teleporter.clearAndReload();

            // Impostor roles
            if (player == Morphling.morphling) Morphling.clearAndReload();
            if (player == Camouflager.camouflager) Camouflager.clearAndReload();
            if (player == Godfather.godfather) Godfather.clearAndReload();
            if (player == Mafioso.mafioso) Mafioso.clearAndReload();
            if (player == Janitor.janitor) Janitor.clearAndReload();
            if (player == Vampire.vampire) Vampire.clearAndReload();
            if (player == Eraser.eraser) Eraser.clearAndReload();
            if (player == Trickster.trickster) Trickster.clearAndReload();
            if (player == Cleaner.cleaner) Cleaner.clearAndReload();
            if (player == Mimic.mimic) Mimic.clearAndReload();
            if (player == Escapist.escapist) Escapist.clearAndReload();
            if (player == Snarer.snarer) Snarer.clearAndReload();
            if (player == Poucher.poucher) Poucher.clearAndReload();
            if (player == Warlock.warlock) Warlock.clearAndReload();
            if (player == Witch.witch) Witch.clearAndReload();
            if (player == Ninja.ninja) Ninja.clearAndReload();
            if (player == Bomber.bomber) Bomber.clearAndReload();
            if (player == Yoyo.yoyo) Yoyo.clearAndReload();
            if (player == Pitfaller.pitfaller) Pitfaller.clearAndReload();
            if (player == Shinobi.shinobi) Shinobi.clearAndReload();
            if (player == Miner.miner) Miner.clearAndReload();
            if (player == Transportist.transportist) Transportist.clearAndReload();

            // Other roles
            if (player == Jester.jester) Jester.clearAndReload();
			if (player == Swooper.swooper) Swooper.clearAndReload();
            if (player == Werewolf.werewolf) Werewolf.clearAndReload();
            if (player == Juggernaut.juggernaut) Juggernaut.clearAndReload();
            if (player == Doomsayer.doomsayer) Doomsayer.clearAndReload();
            if (player == Kataomoi.kataomoi) Kataomoi.clearAndReload();
            if (player == Cerenovus.cerenovus) Cerenovus.clearAndReload();
            if (player == Kataomoi.kataomoi) Kataomoi.clearAndReload();
            if (player == Arsonist.arsonist) Arsonist.clearAndReload();
            if (Guesser.isGuesser(player.PlayerId)) Guesser.clear(player.PlayerId);
            if (player == Jackal.jackal) { // Promote Sidekick and hence override the the Jackal or erase Jackal
                if (Sidekick.promotesToJackal && Sidekick.sidekick != null && !Sidekick.sidekick.Data.IsDead) {
                    RPCProcedure.sidekickPromotes();
                } else {
                    Jackal.clearAndReload();
                }
            }
            if (player == Sidekick.sidekick) Sidekick.clearAndReload();
            if (player == BountyHunter.bountyHunter) BountyHunter.clearAndReload();
            if (player == Vulture.vulture) Vulture.clearAndReload();
            if (player == Lawyer.lawyer) Lawyer.clearAndReload();
            if (player == Pursuer.pursuer) Pursuer.clearAndReload();
            if (player == Thief.thief) Thief.clearAndReload();
            if (player == Amnisiac.amnisiac) Amnisiac.clearAndReload();

            
            // Modifier
            if (!ignoreModifier)
            {
                if (player == Lovers.lover1 || player == Lovers.lover2) Lovers.clearAndReload(); // The whole Lover couple is being erased
                if (Bait.bait.Any(x => x.PlayerId == player.PlayerId)) Bait.bait.RemoveAll(x => x.PlayerId == player.PlayerId);
                if (Bloody.bloody.Any(x => x.PlayerId == player.PlayerId)) Bloody.bloody.RemoveAll(x => x.PlayerId == player.PlayerId);
                if (AntiTeleport.antiTeleport.Any(x => x.PlayerId == player.PlayerId)) AntiTeleport.antiTeleport.RemoveAll(x => x.PlayerId == player.PlayerId);
                if (Sunglasses.sunglasses.Any(x => x.PlayerId == player.PlayerId)) Sunglasses.sunglasses.RemoveAll(x => x.PlayerId == player.PlayerId);
                if (player == Tiebreaker.tiebreaker) Tiebreaker.clearAndReload();
                if (player == Mini.mini) Mini.clearAndReload();
                if (player == Cursed.cursed) Cursed.clearAndReload();
                if (player == Radar.radar) Radar.clearAndReload();
                if (player == AllKnowing.Allknowers.Any(x => x.PlayerId == x.PlayerId)) AllKnowing.Allknowers.RemoveAll(x => x.PlayerId == player.PlayerId);
                if (player == Viewer.viewer) Viewer.clearAndReload();
                if (player == Paranoid.paranoid) Paranoid.clearAndReload();
                if (player == Slueth.slueth) Slueth.clearAndReload();
                if (player == Disperser.disperser) Disperser.clearAndReload();
                if (player == Indomitable.indomitable) Indomitable.clearAndReload();
                if (AntiReport.antiReport.Any(x => x.PlayerId == player.PlayerId)) AntiReport.antiReport.RemoveAll(x => x.PlayerId == player.PlayerId);
                if (Vip.vip.Any(x => x.PlayerId == player.PlayerId)) Vip.vip.RemoveAll(x => x.PlayerId == player.PlayerId);
                if (Invert.invert.Any(x => x.PlayerId == player.PlayerId)) Invert.invert.RemoveAll(x => x.PlayerId == player.PlayerId);
                if (Chameleon.chameleon.Any(x => x.PlayerId == player.PlayerId)) Chameleon.chameleon.RemoveAll(x => x.PlayerId == player.PlayerId);
            }
        }

        public static void setFutureReveal(byte playerId)
        {
            PlayerControl player = Helpers.playerById(playerId);
            if (Doomsayer.playerTargetinformation == null)
                Doomsayer.playerTargetinformation = new List<PlayerControl>();
            if (player != null)
            {
                Doomsayer.playerTargetinformation.Add(player);
            }
        }


        public static void setFutureErased(byte playerId) {
            PlayerControl player = Helpers.playerById(playerId);
            if (Eraser.futureErased == null) 
                Eraser.futureErased = new List<PlayerControl>();
            if (player != null) {
                Eraser.futureErased.Add(player);
            }
        }

        public static void setFutureShifted(byte playerId) {
            Shifter.futureShift = Helpers.playerById(playerId);
        }

        public static void setFutureShielded(byte playerId) {
            Medic.futureShielded = Helpers.playerById(playerId);
            Medic.usedShield = true;
        }

        public static void setFutureSpelled(byte playerId) {
            PlayerControl player = Helpers.playerById(playerId);
            if (Witch.futureSpelled == null)
                Witch.futureSpelled = new List<PlayerControl>();
            if (player != null) {
                Witch.futureSpelled.Add(player);
            }
        }

        public static void placeNinjaTrace(byte[] buff) {
            Vector3 position = Vector3.zero;
            position.x = BitConverter.ToSingle(buff, 0 * sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1 * sizeof(float));
            new NinjaTrace(position, Ninja.traceTime);
            if (CachedPlayer.LocalPlayer.PlayerControl != Ninja.ninja)
                Ninja.ninjaMarked = null;
        }

        public static void setInvisible(byte playerId, byte flag)
        {
            PlayerControl target = Helpers.playerById(playerId);
            if (target == null) return;
            if (flag == byte.MaxValue)
            {
                target.cosmetics.currentBodySprite.BodySprite.color = Color.white;
                target.cosmetics.colorBlindText.gameObject.SetActive(DataManager.Settings.Accessibility.ColorBlindMode);
                target.cosmetics.colorBlindText.color = target.cosmetics.colorBlindText.color.SetAlpha(1f);

                if (Camouflager.camouflageTimer <= 0 && !Helpers.MushroomSabotageActive() && !Helpers.isActiveCamoComms()) target.setDefaultLook();
                Ninja.isInvisble = false;
                return;
            }

            target.setLook("", 6, "", "", "", "");
            Color color = Color.clear;
            bool canSee = CachedPlayer.LocalPlayer.Data.Role.IsImpostor || CachedPlayer.LocalPlayer.Data.IsDead;
            if (canSee) color.a = 0.1f;
            target.cosmetics.currentBodySprite.BodySprite.color = color;
            target.cosmetics.colorBlindText.gameObject.SetActive(false);
            target.cosmetics.colorBlindText.color = target.cosmetics.colorBlindText.color.SetAlpha(canSee ? 0.1f : 0f);
            Ninja.invisibleTimer = Ninja.invisibleDuration;
            Ninja.isInvisble = true;
        }

        

		 public static void setSwoop(byte playerId, byte flag) {        
            PlayerControl target = Helpers.playerById(playerId);
            if (target == null) return;
            if (flag == byte.MaxValue) {
                target.cosmetics.currentBodySprite.BodySprite.color = Color.white;
                target.cosmetics.colorBlindText.gameObject.SetActive(DataManager.Settings.Accessibility.ColorBlindMode);
                if (Camouflager.camouflageTimer <= 0 && !Helpers.MushroomSabotageActive() && !Helpers.isActiveCamoComms()) target.setDefaultLook();
                Swooper.isInvisable = false;
                return;
            }
            target.setLook("", 6, "", "", "", "");
            Color color = Color.clear;           
            if (Swooper.swooper == CachedPlayer.LocalPlayer.PlayerControl || CachedPlayer.LocalPlayer.Data.IsDead || (Swooper.swooper == Jackal.jackal && Sidekick.sidekick == CachedPlayer.LocalPlayer.PlayerControl)) color.a = 0.1f;
            target.cosmetics.currentBodySprite.BodySprite.color = color;
            target.cosmetics.colorBlindText.gameObject.SetActive(false);
            Swooper.swoopTimer = Swooper.duration;
            Swooper.isInvisable = true;
        }


        public static void setInvisibleGen(byte playerId, byte flag)
        {
            PlayerControl target = Helpers.playerById(playerId);
            if (target == null) return;
            if (flag == byte.MaxValue)
            {
                target.cosmetics.currentBodySprite.BodySprite.color = Color.white;
                target.setDefaultLook();
                return;
            }

            target.setLook("", 6, "", "", "", "");
            Color color = Color.clear;           
            if (CachedPlayer.LocalPlayer.Data.IsDead) color.a = 0.1f;
            target.cosmetics.currentBodySprite.BodySprite.color = color;
        }

       public static void Mine(int ventId, PlayerControl role, byte[] buff, float zAxis) {
            Vector3 position = Vector3.zero;
            position.x = BitConverter.ToSingle(buff, 0*sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1*sizeof(float));
            
            var ventPrefab = UnityEngine.Object.FindObjectOfType<Vent>();
            var vent = UnityEngine.Object.Instantiate(ventPrefab, ventPrefab.transform.parent);
            vent.Id = ventId;
            vent.transform.position = new Vector3(position.x, position.y, zAxis);

            if (Miner.Vents.Count > 0) {
                var leftVent = Miner.Vents[^1];
                vent.Left = leftVent;
                leftVent.Right = vent;
            } else {
                vent.Left = null;
            }
            vent.Right = null;
            vent.Center = null;
            var allVents = ShipStatus.Instance.AllVents.ToList();
            allVents.Add(vent);
            ShipStatus.Instance.AllVents = allVents.ToArray();
            Miner.Vents.Add(vent);
            Miner.LastMined = DateTime.UtcNow;
            if (SubmergedCompatibility.IsSubmerged) {
                vent.gameObject.layer = 12;
                vent.gameObject.AddSubmergedComponent(SubmergedCompatibility.Classes.ElevatorMover); // just in case elevator vent is not blocked
                if (vent.gameObject.transform.position.y > -7) vent.gameObject.transform.position = new Vector3(vent.gameObject.transform.position.x, vent.gameObject.transform.position.y, 0.03f);
                else {
                    vent.gameObject.transform.position = new Vector3(vent.gameObject.transform.position.x, vent.gameObject.transform.position.y, 0.0009f);
                    vent.gameObject.transform.localPosition = new Vector3(vent.gameObject.transform.localPosition.x, vent.gameObject.transform.localPosition.y, -0.003f);
                }
            }
        }


        public static void placePortal(byte[] buff) {
            Vector3 position = Vector2.zero;
            position.x = BitConverter.ToSingle(buff, 0 * sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1 * sizeof(float));
            new Portal(position);
        }

        public static void usePortal(byte playerId, byte exit) {
            Portal.startTeleport(playerId, exit);
        }

        public static void placeJackInTheBox(byte[] buff) {
            Vector3 position = Vector3.zero;
            position.x = BitConverter.ToSingle(buff, 0*sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1*sizeof(float));
            new JackInTheBox(position);
        }

        public static void lightsOut() {
            Trickster.lightsOutTimer = Trickster.lightsOutDuration;
            // If the local player is impostor indicate lights out
            if(Helpers.hasImpVision(GameData.Instance.GetPlayerById(CachedPlayer.LocalPlayer.PlayerId))) {
                new CustomMessage("骗术师把灯灭了", Trickster.lightsOutDuration);
            }
        }

        public static void placeCamera(byte[] buff) {
            var referenceCamera = UnityEngine.Object.FindObjectOfType<SurvCamera>(); 
            if (referenceCamera == null) return; // Mira HQ

            SecurityGuard.remainingScrews -= SecurityGuard.camPrice;
            SecurityGuard.placedCameras++;

            Vector3 position = Vector3.zero;
            position.x = BitConverter.ToSingle(buff, 0*sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1*sizeof(float));

            var camera = UnityEngine.Object.Instantiate<SurvCamera>(referenceCamera);
            camera.transform.position = new Vector3(position.x, position.y, referenceCamera.transform.position.z - 1f);
            camera.CamName = $"安保监控 {SecurityGuard.placedCameras}";
            camera.Offset = new Vector3(0f, 0f, camera.Offset.z);
            if (GameOptionsManager.Instance.currentNormalGameOptions.MapId == 2 || GameOptionsManager.Instance.currentNormalGameOptions.MapId == 4) camera.transform.localRotation = new Quaternion(0, 0, 1, 1); // Polus and Airship 

            if (SubmergedCompatibility.IsSubmerged) {
                // remove 2d box collider of console, so that no barrier can be created. (irrelevant for now, but who knows... maybe we need it later)
                var fixConsole = camera.transform.FindChild("FixConsole");
                if (fixConsole != null) {
                    var boxCollider = fixConsole.GetComponent<BoxCollider2D>();
                    if (boxCollider != null) UnityEngine.Object.Destroy(boxCollider);
                }
            }


            if (CachedPlayer.LocalPlayer.PlayerControl == SecurityGuard.securityGuard) {
                camera.gameObject.SetActive(true);
                camera.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            } else {
                camera.gameObject.SetActive(false);
            }
            TORMapOptions.camerasToAdd.Add(camera);
        }

        public static void sealVent(int ventId) {
            Vent vent = MapUtilities.CachedShipStatus.AllVents.FirstOrDefault((x) => x != null && x.Id == ventId);
            if (vent == null) return;

            SecurityGuard.remainingScrews -= SecurityGuard.ventPrice;
            if (CachedPlayer.LocalPlayer.PlayerControl == SecurityGuard.securityGuard) {
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
                rend.color = new Color(1f, 1f, 1f, 0.5f);
                vent.name = "FutureSealedVent_" + vent.name;
            }

            TORMapOptions.ventsToSeal.Add(vent);
        }

        public static void arsonistWin() {
            Arsonist.triggerArsonistWin = true;
            foreach (PlayerControl p in CachedPlayer.AllPlayers) {
                if (p != Arsonist.arsonist && !p.Data.IsDead) {
                    p.Exiled();
                    overrideDeathReasonAndKiller(p, DeadPlayer.CustomDeathReason.Arson, Arsonist.arsonist);
                }
            }
        }

        public static void lawyerSetTarget(byte playerId) {
            Lawyer.target = Helpers.playerById(playerId);
        }

        public static void lawyerPromotesToPursuer() {
            PlayerControl player = Lawyer.lawyer;
            PlayerControl client = Lawyer.target;
            Lawyer.clearAndReload(false);

            Pursuer.pursuer = player;

            if (player.PlayerId == CachedPlayer.LocalPlayer.PlayerId && client != null) {
                    Transform playerInfoTransform = client.cosmetics.nameText.transform.parent.FindChild("Info");
                    TMPro.TextMeshPro playerInfo = playerInfoTransform != null ? playerInfoTransform.GetComponent<TMPro.TextMeshPro>() : null;
                    if (playerInfo != null) playerInfo.text = "";
            }
        }

        public static void guesserShoot(byte killerId, byte dyingTargetId, byte guessedTargetId, byte guessedRoleId) {
            PlayerControl dyingTarget = Helpers.playerById(dyingTargetId);
            if (dyingTarget == null ) return;
            if (Lawyer.target != null && dyingTarget == Lawyer.target) Lawyer.targetWasGuessed = true;  // Lawyer shouldn't be exiled with the client for guesses
            PlayerControl dyingLoverPartner = Lovers.bothDie ? dyingTarget.getPartner() : null; // Lover check
            PlayerControl kataomoiPlayer = Kataomoi.kataomoi != null && Kataomoi.target == dyingTarget ? Kataomoi.kataomoi : null; // Kataomoi check
            PlayerControl dyingAkujoPartner;
            if (Lawyer.target != null && dyingLoverPartner == Lawyer.target) Lawyer.targetWasGuessed = true;  // Lawyer shouldn't be exiled with the client for guesses
            if (Arbiter.arbiter != null && dyingTarget == Arbiter.arbiter) Arbiter.specialVoteTargetPlayerId = byte.MaxValue;
            if (Arbiter.arbiter != null && dyingTarget.PlayerId == Arbiter.specialVoteTargetPlayerId) Arbiter.specialVoteTargetPlayerId = byte.MaxValue;
            

            PlayerControl guesser = Helpers.playerById(killerId);
            if (Thief.thief != null && Thief.thief.PlayerId == killerId && Thief.canStealWithGuess) {
                RoleInfo roleInfo = RoleInfo.allRoleInfos.FirstOrDefault(x => (byte)x.roleId == guessedRoleId);
                if (!Thief.thief.Data.IsDead && !Thief.isFailedThiefKill(dyingTarget, guesser, roleInfo)) {
                    RPCProcedure.thiefStealsRole(dyingTarget.PlayerId);
                }
            }
            
            //doom shoot
            
            if (Doomsayer.doomsayer != null && Doomsayer.doomsayer == guesser && Doomsayer.canGuess)
            {
                RoleInfo roleInfo = RoleInfo.allRoleInfos.FirstOrDefault(x => (byte)x.roleId == guessedRoleId);
                if (!Doomsayer.doomsayer.Data.IsDead && guessedTargetId == dyingTargetId)
                {

                    Doomsayer.killedToWin++;
                    if (Doomsayer.killedToWin >= Doomsayer.killToWin)
                    {

                        Doomsayer.triggerDoomsayerrWin = true;

                    }
                    //FastDestroyableSingleton<HudManager>.Instance.KillOverlay.ShowKillAnimation(guesser.Data, dyingTarget.Data);
                    if (MeetingHudPatch.guesserUI != null) MeetingHudPatch.guesserUIExitButton.OnClick.Invoke();

                }
                else
                {
                    return;
                }


            }

            if ((Akujo.akujo != null && dyingTarget == Akujo.akujo) || (Akujo.honmei != null && dyingTarget == Akujo.honmei)) dyingAkujoPartner = dyingTarget == Akujo.akujo ? Akujo.honmei : Akujo.akujo;
            else dyingAkujoPartner = null;

            bool lawyerDiedAdditionally = false;
            if (Lawyer.lawyer != null && !Lawyer.isProsecutor && Lawyer.lawyer.PlayerId == killerId && Lawyer.target != null && Lawyer.target.PlayerId == dyingTargetId) {
                // Lawyer guessed client.
                if (CachedPlayer.LocalPlayer.PlayerControl == Lawyer.lawyer) {
                    FastDestroyableSingleton<HudManager>.Instance.KillOverlay.ShowKillAnimation(Lawyer.lawyer.Data, Lawyer.lawyer.Data);
                    if (MeetingHudPatch.guesserUI != null) MeetingHudPatch.guesserUIExitButton.OnClick.Invoke();
                }
                Lawyer.lawyer.Exiled();
                lawyerDiedAdditionally = true;
                GameHistory.overrideDeathReasonAndKiller(Lawyer.lawyer, DeadPlayer.CustomDeathReason.LawyerSuicide, guesser);
            }

            dyingTarget.Exiled();
            GameHistory.overrideDeathReasonAndKiller(dyingTarget, DeadPlayer.CustomDeathReason.Guess, guesser);
            byte partnerId = dyingLoverPartner != null ? dyingLoverPartner.PlayerId : dyingTargetId;
            byte partnerId2 = kataomoiPlayer != null ? kataomoiPlayer.PlayerId : dyingTargetId;
            byte akujoPartnerId = dyingAkujoPartner != null ? dyingAkujoPartner.PlayerId : byte.MaxValue;

            HandleGuesser.remainingShots(killerId, true);
            if (Constants.ShouldPlaySfx()) SoundManager.Instance.PlaySound(dyingTarget.KillSfx, false, 0.8f);
            if (MeetingHud.Instance) {
                foreach (PlayerVoteArea pva in MeetingHud.Instance.playerStates) {
                    if (pva.TargetPlayerId == dyingTargetId || pva.TargetPlayerId == partnerId || lawyerDiedAdditionally && Lawyer.lawyer.PlayerId == pva.TargetPlayerId)
                    {
                        pva.SetDead(pva.DidReport, true);
                        pva.Overlay.gameObject.SetActive(true);
                        MeetingHudPatch.swapperCheckAndReturnSwap(MeetingHud.Instance, pva.TargetPlayerId);
                    }

                    //Give players back their vote if target is shot dead
                    if (pva.VotedFor != dyingTargetId && pva.VotedFor != partnerId && (!lawyerDiedAdditionally || Lawyer.lawyer.PlayerId != pva.VotedFor) || pva.VotedFor != partnerId2 || pva.VotedFor != akujoPartnerId) continue;
                    pva.UnsetVote();
                    var voteAreaPlayer = Helpers.playerById(pva.TargetPlayerId);
                    if (!voteAreaPlayer.AmOwner) continue;
                    MeetingHud.Instance.ClearVote();

                }
                if (AmongUsClient.Instance.AmHost) 
                    MeetingHud.Instance.CheckForEndVoting();
            }
            if (FastDestroyableSingleton<HudManager>.Instance != null && guesser != null)
                if (CachedPlayer.LocalPlayer.PlayerControl == dyingTarget) {
                    FastDestroyableSingleton<HudManager>.Instance.KillOverlay.ShowKillAnimation(guesser.Data, dyingTarget.Data);
                    if (MeetingHudPatch.guesserUI != null) MeetingHudPatch.guesserUIExitButton.OnClick.Invoke();
                } else if (dyingLoverPartner != null && CachedPlayer.LocalPlayer.PlayerControl == dyingLoverPartner) {
                    FastDestroyableSingleton<HudManager>.Instance.KillOverlay.ShowKillAnimation(dyingLoverPartner.Data, dyingLoverPartner.Data);
                    if (MeetingHudPatch.guesserUI != null) MeetingHudPatch.guesserUIExitButton.OnClick.Invoke();
                }
                else if (kataomoiPlayer != null && CachedPlayer.LocalPlayer.PlayerControl == kataomoiPlayer){
                    FastDestroyableSingleton<HudManager>.Instance.KillOverlay.ShowKillAnimation(kataomoiPlayer.Data, kataomoiPlayer.Data);
                    if (MeetingHudPatch.guesserUI != null) MeetingHudPatch.guesserUIExitButton.OnClick.Invoke();
                }
                else if (dyingAkujoPartner != null && CachedPlayer.LocalPlayer.PlayerControl == dyingAkujoPartner){
                    FastDestroyableSingleton<HudManager>.Instance.KillOverlay.ShowKillAnimation(dyingAkujoPartner.Data, dyingAkujoPartner.Data);
                    if (MeetingHudPatch.guesserUI != null) MeetingHudPatch.guesserUIExitButton.OnClick.Invoke();
                }

            // remove shoot button from targets for all guessers and close their guesserUI
            if (GuesserGM.isGuesser(PlayerControl.LocalPlayer.PlayerId) && PlayerControl.LocalPlayer != guesser && !PlayerControl.LocalPlayer.Data.IsDead && GuesserGM.remainingShots(PlayerControl.LocalPlayer.PlayerId) > 0 && MeetingHud.Instance) {
                MeetingHud.Instance.playerStates.ToList().ForEach(x => { if (x.TargetPlayerId == dyingTarget.PlayerId && x.transform.FindChild("ShootButton") != null) UnityEngine.Object.Destroy(x.transform.FindChild("ShootButton").gameObject); });
                if (dyingLoverPartner != null)
                    MeetingHud.Instance.playerStates.ToList().ForEach(x => { if (x.TargetPlayerId == dyingLoverPartner.PlayerId && x.transform.FindChild("ShootButton") != null) UnityEngine.Object.Destroy(x.transform.FindChild("ShootButton").gameObject); });

                if (MeetingHudPatch.guesserUI != null && MeetingHudPatch.guesserUIExitButton != null) {
                    if (MeetingHudPatch.guesserCurrentTarget == dyingTarget.PlayerId)
                        MeetingHudPatch.guesserUIExitButton.OnClick.Invoke();
                    else if (dyingLoverPartner != null && MeetingHudPatch.guesserCurrentTarget == dyingLoverPartner.PlayerId)
                        MeetingHudPatch.guesserUIExitButton.OnClick.Invoke();
                }
            }


            PlayerControl guessedTarget = Helpers.playerById(guessedTargetId);
            if (CachedPlayer.LocalPlayer.Data.IsDead && guessedTarget != null && guesser != null) {
                RoleInfo roleInfo = RoleInfo.allRoleInfos.FirstOrDefault(x => (byte)x.roleId == guessedRoleId);
                string msg = $"{guesser.Data.PlayerName} 猜测的职业是 {roleInfo?.name ?? ""} ，猜测对象为 {guessedTarget.Data.PlayerName}!";
                if (AmongUsClient.Instance.AmClient && FastDestroyableSingleton<HudManager>.Instance)
                    FastDestroyableSingleton<HudManager>.Instance.Chat.AddChat(guesser, msg);
                if (msg.IndexOf("who", StringComparison.OrdinalIgnoreCase) >= 0)
                    FastDestroyableSingleton<UnityTelemetry>.Instance.SendWho();
            }
        }


    public static void showBodyGuardFlash() {
        if (CustomOptionHolder.bodyGuardFlash.getBool()) {
            Helpers.showFlash(BodyGuard.color, 1f); }
    }


    
    public static void bodyGuardGuardPlayer(byte targetId) {
        PlayerControl target = Helpers.playerById(targetId);
        BodyGuard.usedGuard = true;
        BodyGuard.guarded = target;
    }



        public static void setBlanked(byte playerId, byte value) {
            PlayerControl target = Helpers.playerById(playerId);
            if (target == null) return;
            Pursuer.blankedList.RemoveAll(x => x.PlayerId == playerId);
            if (value > 0) Pursuer.blankedList.Add(target);            
        }

        public static void bloody(byte killerPlayerId, byte bloodyPlayerId) {
            if (Bloody.active.ContainsKey(killerPlayerId)) return;
            Bloody.active.Add(killerPlayerId, Bloody.duration);
            Bloody.bloodyKillerMap.Add(killerPlayerId, bloodyPlayerId);
        }

        public static void setFirstKill(byte playerId) {
            PlayerControl target = Helpers.playerById(playerId);
            if (target == null) return;
            TORMapOptions.firstKillPlayer = target;
        }

        public static void setTiebreak() {
            Tiebreaker.isTiebreak = true;
        }



         public static List<Vector3> FindVentPoss()
        {
            var poss = new List<Vector3>();
            foreach (var vent in DestroyableSingleton<ShipStatus>.Instance.AllVents)
            {
                var Transform = vent.transform;
                var position = Transform.position;
                poss.Add(new Vector3(position.x, position.y + 0.2f, position.z - 50));
            }
            return poss;
        }


        public static void disperse() {
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

                AntiTeleport.setPosition();
                Helpers.showFlash(Cleaner.color, 1f);
            if  (AntiTeleport.antiTeleport.FindAll(x => x.PlayerId == CachedPlayer.LocalPlayer.PlayerControl.PlayerId).Count == 0 && !CachedPlayer.LocalPlayer.Data.IsDead) {
                foreach (PlayerControl player in CachedPlayer.AllPlayers){
                    if (MapBehaviour.Instance)
                MapBehaviour.Instance.Close();
            if (Minigame.Instance)
                Minigame.Instance.ForceClose();
                    if (PlayerControl.LocalPlayer.inVent)
                {
                    PlayerControl.LocalPlayer.MyPhysics.RpcExitVent(Vent.currentVent.Id);
                    PlayerControl.LocalPlayer.MyPhysics.ExitAllVents();
                };
                    if (Disperser.dispersesToVent){
                        CachedPlayer.LocalPlayer.PlayerControl.transform.position = FindVentPoss()[rnd.Next(FindVentPoss().Count)];}
                    else {
                        switch (GameOptionsManager.Instance.currentNormalGameOptions.MapId){
                            case 0:
                                CachedPlayer.LocalPlayer.PlayerControl.transform.position = skeldSpawn[rnd.Next(skeldSpawn.Count)];
                                break;
                            case 1:
                                CachedPlayer.LocalPlayer.PlayerControl.transform.position = miraSpawn[rnd.Next(miraSpawn.Count)];
                                break;
                            case 2:
                                CachedPlayer.LocalPlayer.PlayerControl.transform.position = polusSpawn[rnd.Next(polusSpawn.Count)];
                                break;
                            case 3:
                                CachedPlayer.LocalPlayer.PlayerControl.transform.position = dleksSpawn[rnd.Next(dleksSpawn.Count)];
                                break;
                            case 4:
                                CachedPlayer.LocalPlayer.PlayerControl.transform.position = airshipSpawn[rnd.Next(airshipSpawn.Count)];
                                break;
                            case 5:
                                CachedPlayer.LocalPlayer.PlayerControl.transform.position = fungleSpawn[rnd.Next(fungleSpawn.Count)];
                                break;
                            }
                        }
                }
                Disperser.remainingDisperses--;
            }
        }




        public static void thiefStealsRole(byte playerId) {
            PlayerControl target = Helpers.playerById(playerId);
            PlayerControl thief = Thief.thief;
            if (target == null) return;
            if (target == Sheriff.sheriff) Sheriff.sheriff = thief;
            if (target == Deputy.deputy) Deputy.deputy = thief;
            if (target == Veteren.veteren) Veteren.veteren = thief;
            if (target == Jackal.jackal) {
                Jackal.jackal = thief;
                Jackal.formerJackals.Add(target);
            }
            if (target == Sidekick.sidekick) {
                Sidekick.sidekick = thief;
                Jackal.formerJackals.Add(target);
                if (HandleGuesser.isGuesserGm && CustomOptionHolder.guesserGamemodeSidekickIsAlwaysGuesser.getBool() && !HandleGuesser.isGuesser(thief.PlayerId))
                    setGuesserGm(thief.PlayerId);
            }
            if (target == Cerenovus.cerenovus)
            {
                Cerenovus.cerenovus = thief;
                Cerenovus.formerCerenovus = target;
            }
            if (target == Swooper.swooper) Swooper.swooper = thief;
            if (target == Werewolf.werewolf) Werewolf.werewolf = thief;
            if (target == Juggernaut.juggernaut) Juggernaut.juggernaut = thief;
            if (target == Mimic.mimic) {
				Mimic.mimic = thief;
				Mimic.hasMimic = false;
			}
			if (target == Poucher.poucher) Poucher.poucher = thief;
            if (target == Escapist.escapist) Escapist.escapist = thief;
            if (target == Shinobi.shinobi) Shinobi.shinobi = thief;
            if (target == Pitfaller.pitfaller) Pitfaller.pitfaller = thief;
            if (target == Escapist.escapist) Escapist.escapist = thief;
            if (target == Snarer.snarer) Snarer.snarer = thief;
            if (target == Miner.miner) Miner.miner = thief;
            if (target == Transportist.transportist) Transportist.transportist = thief;

            if (target == Guesser.evilGuesser) Guesser.evilGuesser = thief;
            if (target == Godfather.godfather) Godfather.godfather = thief;
            if (target == Mafioso.mafioso) Mafioso.mafioso = thief;
            if (target == Janitor.janitor) Janitor.janitor = thief;
            if (target == Morphling.morphling) Morphling.morphling = thief;
            if (target == Camouflager.camouflager) Camouflager.camouflager = thief;
            if (target == Vampire.vampire) Vampire.vampire = thief;
            if (target == Eraser.eraser) Eraser.eraser = thief;
            if (target == Trickster.trickster) Trickster.trickster = thief;
            if (target == Cleaner.cleaner) Cleaner.cleaner = thief;
            if (target == Warlock.warlock) Warlock.warlock = thief;
            if (target == BountyHunter.bountyHunter) BountyHunter.bountyHunter = thief;
            if (target == Witch.witch) {
                Witch.witch = thief;
                if (MeetingHud.Instance) 
                    if (Witch.witchVoteSavesTargets)  // In a meeting, if the thief guesses the witch, all targets are saved or no target is saved.
                        Witch.futureSpelled = new();
                else  // If thief kills witch during the round, remove the thief from the list of spelled people, keep the rest
                    Witch.futureSpelled.RemoveAll(x => x.PlayerId == thief.PlayerId);
            }
            if (target == Ninja.ninja) Ninja.ninja = thief;
            if (target == Bomber.bomber) Bomber.bomber = thief;
            if (target == Yoyo.yoyo) {
                Yoyo.yoyo = thief;
                Yoyo.markedLocation = null;
            }
            if (target.Data.Role.IsImpostor) {
                RoleManager.Instance.SetRole(Thief.thief, RoleTypes.Impostor);
                FastDestroyableSingleton<HudManager>.Instance.KillButton.SetCoolDown(Thief.thief.killTimer, GameOptionsManager.Instance.currentNormalGameOptions.KillCooldown);
            }
            if (Lawyer.lawyer != null && target == Lawyer.target)
                Lawyer.target = thief;
            if (Thief.thief == PlayerControl.LocalPlayer) CustomButton.ResetAllCooldowns();
            Thief.clearAndReload();
            Thief.formerThief = thief;  // After clearAndReload, else it would get reset...
        }
        
        public static void setTrap(byte[] buff) {
            if (Trapper.trapper == null) return;
            Trapper.charges -= 1;
            Vector3 position = Vector3.zero;
            position.x = BitConverter.ToSingle(buff, 0 * sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1 * sizeof(float));
            new Trap(position);
        }

        public static void triggerTrap(byte playerId, byte trapId) {
            Trap.triggerTrap(playerId, trapId);
        }

        public static void setGuesserGm (byte playerId) {
            PlayerControl target = Helpers.playerById(playerId);
            if (target == null) return;
            new GuesserGM(target);
        }

        public static void shareTimer(float punish) {
            HideNSeek.timer -= punish;
        }

        public static void huntedShield(byte playerId) {
            if (!Hunted.timeshieldActive.Contains(playerId)) Hunted.timeshieldActive.Add(playerId);
            FastDestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(Hunted.shieldDuration, new Action<float>((p) => {
                if (p == 1f) Hunted.timeshieldActive.Remove(playerId);
            })));
        }

        public static void huntedRewindTime(byte playerId) {
            Hunted.timeshieldActive.Remove(playerId); // Shield is no longer active when rewinding
            SoundEffectsManager.stop("timemasterShield");  // Shield sound stopped when rewinding
            if (playerId == CachedPlayer.LocalPlayer.PlayerControl.PlayerId) {
                resetHuntedRewindButton();
            }
            FastDestroyableSingleton<HudManager>.Instance.FullScreen.color = new Color(0f, 0.5f, 0.8f, 0.3f);
            FastDestroyableSingleton<HudManager>.Instance.FullScreen.enabled = true;
            FastDestroyableSingleton<HudManager>.Instance.FullScreen.gameObject.SetActive(true);
            FastDestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(Hunted.shieldRewindTime, new Action<float>((p) => {
                if (p == 1f) FastDestroyableSingleton<HudManager>.Instance.FullScreen.enabled = false;
            })));

            if (!CachedPlayer.LocalPlayer.Data.Role.IsImpostor) return; // only rewind hunter

            TimeMaster.isRewinding = true;

            if (MapBehaviour.Instance)
                MapBehaviour.Instance.Close();
            if (Minigame.Instance)
                Minigame.Instance.ForceClose();
            CachedPlayer.LocalPlayer.PlayerControl.moveable = false;
        }

        public static void propHuntStartTimer(bool blackout = false) {
            if (blackout) {
                PropHunt.blackOutTimer = PropHunt.initialBlackoutTime;
                PropHunt.transformLayers();
            } else {
                PropHunt.timerRunning = true;
                PropHunt.blackOutTimer = 0f;
            }            
            PropHunt.startTime = DateTime.UtcNow;
            foreach (var pc in PlayerControl.AllPlayerControls.ToArray().Where(x => x.Data.Role.IsImpostor)) {
                pc.MyPhysics.SetBodyType(PlayerBodyTypes.Seeker);
            }
        }

        public static void propHuntSetProp(byte playerId, string propName, float posX) {
            PlayerControl player = Helpers.playerById(playerId);
            var prop = PropHunt.FindPropByNameAndPos(propName, posX);
            if (prop == null) return;
            try {
                player.GetComponent<SpriteRenderer>().sprite = prop.GetComponent<SpriteRenderer>().sprite;
            } catch {
                player.GetComponent<SpriteRenderer>().sprite = prop.transform.GetComponentInChildren<SpriteRenderer>().sprite;
            }
            player.transform.localScale = prop.transform.lossyScale;
            player.Visible = false;
            PropHunt.currentObject[player.PlayerId] = new Tuple<string, float>(propName, posX);
        }

        public static void propHuntSetRevealed(byte playerId) {
            SoundEffectsManager.play("morphlingMorph");
            PropHunt.isCurrentlyRevealed.Add(playerId, PropHunt.revealDuration);
            PropHunt.timer -= PropHunt.revealPunish;
        }
        public static void propHuntSetInvis(byte playerId) {
            PropHunt.invisPlayers.Add(playerId, PropHunt.invisDuration);
        }
        public static void propHuntSetSpeedboost(byte playerId) {
            PropHunt.speedboostActive.Add(playerId, PropHunt.speedboostDuration);
        }

        public enum GhostInfoTypes {
            HandcuffNoticed,
            HandcuffOver,
            ArsonistDouse,
            BountyTarget,
            NinjaMarked,
            WarlockTarget,
            MediumInfo,
            BlankUsed,
            DetectiveOrMedicInfo,
            VampireTimer,
            DeathReasonAndKiller,
        }

        public static void receiveGhostInfo (byte senderId, MessageReader reader) {
            PlayerControl sender = Helpers.playerById(senderId);

            GhostInfoTypes infoType = (GhostInfoTypes)reader.ReadByte();
            switch (infoType) {
                case GhostInfoTypes.HandcuffNoticed:
                    Deputy.setHandcuffedKnows(true, senderId);
                    break;
                case GhostInfoTypes.HandcuffOver:
                    _ = Deputy.handcuffedKnows.Remove(senderId);
                    break;
                case GhostInfoTypes.ArsonistDouse:
                    Arsonist.dousedPlayers.Add(Helpers.playerById(reader.ReadByte()));
                    break;
                case GhostInfoTypes.BountyTarget:
                    BountyHunter.bounty = Helpers.playerById(reader.ReadByte());
                    break;
                case GhostInfoTypes.NinjaMarked:
                    Ninja.ninjaMarked = Helpers.playerById(reader.ReadByte());
                    break;
                case GhostInfoTypes.WarlockTarget:
                    Warlock.curseVictim = Helpers.playerById(reader.ReadByte());
                    break;
                case GhostInfoTypes.MediumInfo:
                    string mediumInfo = reader.ReadString();
		             if (Helpers.shouldShowGhostInfo())
                    	FastDestroyableSingleton<HudManager>.Instance.Chat.AddChat(sender, mediumInfo);
                    break;
                case GhostInfoTypes.DetectiveOrMedicInfo:
                    string detectiveInfo = reader.ReadString();
                    if (Helpers.shouldShowGhostInfo())
		    	        FastDestroyableSingleton<HudManager>.Instance.Chat.AddChat(sender, detectiveInfo);
                    break;
                case GhostInfoTypes.BlankUsed:
                    Pursuer.blankedList.Remove(sender);
                    break;
                case GhostInfoTypes.VampireTimer:
                    HudManagerStartPatch.vampireKillButton.Timer = (float)reader.ReadByte();
                    break;
                case GhostInfoTypes.DeathReasonAndKiller:
                    GameHistory.overrideDeathReasonAndKiller(Helpers.playerById(reader.ReadByte()), (DeadPlayer.CustomDeathReason)reader.ReadByte(), Helpers.playerById(reader.ReadByte()));
                    break;
            }
        }

        public static void placeBomb(byte[] buff) {
            if (Bomber.bomber == null) return;
            Vector3 position = Vector3.zero;
            position.x = BitConverter.ToSingle(buff, 0 * sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1 * sizeof(float));
            new Bomb(position);
        }

        public static void defuseBomb() {
            try {
                SoundEffectsManager.playAtPosition("bombDefused", Bomber.bomb.bomb.transform.position, range: Bomber.hearRange);
            } catch { }
            Bomber.clearBomb();
            bomberButton.Timer = bomberButton.MaxTimer;
            bomberButton.isEffectActive = false;
            bomberButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
        }

        public static void shareRoom(byte playerId, byte roomId) {
            if (Snitch.playerRoomMap.ContainsKey(playerId)) Snitch.playerRoomMap[playerId] = roomId;
            else Snitch.playerRoomMap.Add(playerId, roomId);
        }

        public static void yoyoMarkLocation(byte[] buff) {
            if (Yoyo.yoyo == null) return;
            Vector3 position = Vector3.zero;
            position.x = BitConverter.ToSingle(buff, 0 * sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1 * sizeof(float));
            Yoyo.markLocation(position);
            new Silhouette(position, -1, false);
        }

        public static void yoyoBlink(bool isFirstJump, byte[] buff) {
            if (Yoyo.yoyo == null || Yoyo.markedLocation == null) return;
            var markedPos = (Vector3)Yoyo.markedLocation;
            Yoyo.yoyo.NetTransform.SnapTo(markedPos);

            var markedSilhouette = Silhouette.silhouettes.FirstOrDefault(s => s.gameObject.transform.position.x == markedPos.x && s.gameObject.transform.position.y == markedPos.y);
            if (markedSilhouette != null)
                markedSilhouette.permanent = false;

            Vector3 position = Vector3.zero;
            position.x = BitConverter.ToSingle(buff, 0 * sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1 * sizeof(float));
            // Create Silhoutte At Start Position:
            if (isFirstJump) {
                Yoyo.markLocation(position);
                new Silhouette(position, Yoyo.blinkDuration, true);
            } else {
                new Silhouette(position, 5, true);
                Yoyo.markedLocation = null;
            }
            if (Chameleon.chameleon.Any(x => x.PlayerId == Yoyo.yoyo.PlayerId)) // Make the Yoyo visible if chameleon!
                Chameleon.lastMoved[Yoyo.yoyo.PlayerId] = Time.time;            
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
    class RPCHandlerPatch
    {
        static void Postfix([HarmonyArgument(0)]byte callId, [HarmonyArgument(1)]MessageReader reader)
        {
            byte packetId = callId;
            switch (packetId) {

                // Main Controls

                case (byte)CustomRPC.ResetVaribles:
                    RPCProcedure.resetVariables();
                    break;
                case (byte)CustomRPC.ShareOptions:
                    RPCProcedure.HandleShareOptions(reader.ReadByte(), reader);
                    break;
                case (byte)CustomRPC.ForceEnd:
                    RPCProcedure.forceEnd();
                    break; 
                case (byte)CustomRPC.WorkaroundSetRoles:
                    RPCProcedure.workaroundSetRoles(reader.ReadByte(), reader);
                    break;
                case (byte)CustomRPC.SetRole:
                    byte roleId = reader.ReadByte();
                    byte playerId = reader.ReadByte();
                    RPCProcedure.setRole(roleId, playerId);
                    break;
                case (byte)CustomRPC.SetModifier:
                    byte modifierId = reader.ReadByte();
                    byte pId = reader.ReadByte();
                    byte flag = reader.ReadByte();
                    RPCProcedure.setModifier(modifierId, pId, flag);
                    break;
                case (byte)CustomRPC.VersionHandshake:
                    byte major = reader.ReadByte();
                    byte minor = reader.ReadByte();
                    byte patch = reader.ReadByte();
                    float timer = reader.ReadSingle();
                    if (!AmongUsClient.Instance.AmHost && timer >= 0f) GameStartManagerPatch.timer = timer;
                    int versionOwnerId = reader.ReadPackedInt32();
                    byte revision = 0xFF;
                    Guid guid;
                    if (reader.Length - reader.Position >= 17) { // enough bytes left to read
                        revision = reader.ReadByte();
                        // GUID
                        byte[] gbytes = reader.ReadBytes(16);
                        guid = new Guid(gbytes);
                    } else {
                        guid = new Guid(new byte[16]);
                    }
                    RPCProcedure.versionHandshake(major, minor, patch, revision == 0xFF ? -1 : revision, guid, versionOwnerId);
                    break;
                case (byte)CustomRPC.UseUncheckedVent:
                    int ventId = reader.ReadPackedInt32();
                    byte ventingPlayer = reader.ReadByte();
                    byte isEnter = reader.ReadByte();
                    RPCProcedure.useUncheckedVent(ventId, ventingPlayer, isEnter);
                    break;
                case (byte)CustomRPC.UncheckedMurderPlayer:
                    byte source = reader.ReadByte();
                    byte target = reader.ReadByte();
                    byte showAnimation = reader.ReadByte();
                    RPCProcedure.uncheckedMurderPlayer(source, target, showAnimation);
                    break;
                case (byte)CustomRPC.UncheckedExilePlayer:
                    byte exileTarget = reader.ReadByte();
                    RPCProcedure.uncheckedExilePlayer(exileTarget);
                    break;
                case (byte)CustomRPC.UncheckedCmdReportDeadBody:
                    byte reportSource = reader.ReadByte();
                    byte reportTarget = reader.ReadByte();
                    RPCProcedure.uncheckedCmdReportDeadBody(reportSource, reportTarget);
                    break;
                case (byte)CustomRPC.DynamicMapOption:
                    byte mapId = reader.ReadByte();
                    RPCProcedure.dynamicMapOption(mapId);
                    break;
                case (byte)CustomRPC.SetGameStarting:
                    RPCProcedure.setGameStarting();
                    break;

                // Role functionality

                case (byte)CustomRPC.EngineerFixLights:
                    RPCProcedure.engineerFixLights();
                    break;
                case (byte)CustomRPC.EngineerFixSubmergedOxygen:
                    RPCProcedure.engineerFixSubmergedOxygen();
                    break;
                case (byte)CustomRPC.EngineerUsedRepair:
                    RPCProcedure.engineerUsedRepair();
                    break;
                case (byte)CustomRPC.CleanBody:
                    RPCProcedure.cleanBody(reader.ReadByte(), reader.ReadByte());
                    break;
                case (byte)CustomRPC.TimeMasterRewindTime:
                    RPCProcedure.timeMasterRewindTime();
                    break;
                case (byte)CustomRPC.TimeMasterShield:
                    RPCProcedure.timeMasterShield();
                    break;
                case (byte)CustomRPC.MimicMimicRole:
                    RPCProcedure.mimicMimicRole(reader.ReadByte());
                    break;
                case (byte)CustomRPC.ShowIndomitableFlash:
                    RPCProcedure.showIndomitableFlash();
                    break;
                case (byte)CustomRPC.VeterenAlert:
                    RPCProcedure.veterenAlert();
                    break;
                case (byte)CustomRPC.VeterenKill:
                    RPCProcedure.veterenKill(reader.ReadByte());
                    break;
                case (byte)CustomRPC.MedicSetShielded:
                    RPCProcedure.medicSetShielded(reader.ReadByte());
                    break;
                case (byte)CustomRPC.ShieldedMurderAttempt:
                    RPCProcedure.shieldedMurderAttempt();
                    break;
                case (byte)CustomRPC.ShifterShift:
                    RPCProcedure.shifterShift(reader.ReadByte());
                    break;
                case (byte)CustomRPC.SwapperSwap:
                    byte playerId1 = reader.ReadByte();
                    byte playerId2 = reader.ReadByte();
                    RPCProcedure.swapperSwap(playerId1, playerId2);
                    break;
                case (byte)CustomRPC.MayorSetVoteTwice:
                    Mayor.voteTwice = reader.ReadBoolean();
                    break;
                case (byte)CustomRPC.MorphlingMorph:
                    RPCProcedure.morphlingMorph(reader.ReadByte());
                    break;
                case (byte)CustomRPC.CamouflagerCamouflage:
                    byte setTimer = reader.ReadByte();
                    RPCProcedure.camouflagerCamouflage(setTimer);
                    break;
                case (byte)CustomRPC.VampireSetBitten:
                    byte bittenId = reader.ReadByte();
                    byte reset = reader.ReadByte();
                    RPCProcedure.vampireSetBitten(bittenId, reset);
                    break;
                case (byte)CustomRPC.PlaceGarlic:
                    RPCProcedure.placeGarlic(reader.ReadBytesAndSize());
                    break;
                case (byte)CustomRPC.TrackerUsedTracker:
                    RPCProcedure.trackerUsedTracker(reader.ReadByte());
                    break;  
                case (byte)CustomRPC.BodyGuardGuardPlayer:
                    RPCProcedure.bodyGuardGuardPlayer(reader.ReadByte());
                    break;  
                case (byte)CustomRPC.DeputyUsedHandcuffs:
                    RPCProcedure.deputyUsedHandcuffs(reader.ReadByte());
                    break;
                case (byte)CustomRPC.DeputyPromotes:
                    RPCProcedure.deputyPromotes();
                    break;
                case (byte)CustomRPC.JackalCreatesSidekick:
                    RPCProcedure.jackalCreatesSidekick(reader.ReadByte());
                    break;
                case (byte)CustomRPC.SidekickPromotes:
                    RPCProcedure.sidekickPromotes();
                    break;
                case (byte)CustomRPC.ErasePlayerRoles:
                    byte eraseTarget = reader.ReadByte();
                    RPCProcedure.erasePlayerRoles(eraseTarget);
                    Eraser.alreadyErased.Add(eraseTarget);
                    break;
                case (byte)CustomRPC.SetFutureErased:
                    RPCProcedure.setFutureErased(reader.ReadByte());
                    break;
                case (byte)CustomRPC.SetFutureShifted:
                    RPCProcedure.setFutureShifted(reader.ReadByte());
                    break;
                case (byte)CustomRPC.SetFutureShielded:
                    RPCProcedure.setFutureShielded(reader.ReadByte());
                    break;
                case (byte)CustomRPC.PlaceNinjaTrace:
                    RPCProcedure.placeNinjaTrace(reader.ReadBytesAndSize());
                    break;
                case (byte)CustomRPC.PlacePortal:
                    RPCProcedure.placePortal(reader.ReadBytesAndSize());
                    break;
                case (byte)CustomRPC.UsePortal:
                    RPCProcedure.usePortal(reader.ReadByte(), reader.ReadByte());
                    break;
                case (byte)CustomRPC.PlaceJackInTheBox:
                    RPCProcedure.placeJackInTheBox(reader.ReadBytesAndSize());
                    break;
                case (byte)CustomRPC.LightsOut:
                    RPCProcedure.lightsOut();
                    break;
                case (byte)CustomRPC.PlaceCamera:
                    RPCProcedure.placeCamera(reader.ReadBytesAndSize());
                    break;
                case (byte)CustomRPC.SealVent:
                    RPCProcedure.sealVent(reader.ReadPackedInt32());
                    break;
                case (byte)CustomRPC.ArsonistWin:
                    RPCProcedure.arsonistWin();
                    break;
                case (byte)CustomRPC.GuesserShoot:
                    byte killerId = reader.ReadByte();
                    byte dyingTarget = reader.ReadByte();
                    byte guessedTarget = reader.ReadByte();
                    byte guessedRoleId = reader.ReadByte();
                    RPCProcedure.guesserShoot(killerId, dyingTarget, guessedTarget, guessedRoleId);
                    break;
                case (byte)CustomRPC.LawyerSetTarget:
                    RPCProcedure.lawyerSetTarget(reader.ReadByte()); 
                    break;
                case (byte)CustomRPC.LawyerPromotesToPursuer:
                    RPCProcedure.lawyerPromotesToPursuer();
                    break;
                case (byte)CustomRPC.SetBlanked:
                    var pid = reader.ReadByte();
                    var blankedValue = reader.ReadByte();
                    RPCProcedure.setBlanked(pid, blankedValue);
                    break;
                case (byte)CustomRPC.SetFutureSpelled:
                    RPCProcedure.setFutureSpelled(reader.ReadByte());
                    break;
                case (byte)CustomRPC.Bloody:
                    byte bloodyKiller = reader.ReadByte();
                    byte bloodyDead = reader.ReadByte();
                    RPCProcedure.bloody(bloodyKiller, bloodyDead);
                    break;
                case (byte)CustomRPC.SetFirstKill:
                    byte firstKill = reader.ReadByte();
                    RPCProcedure.setFirstKill(firstKill);
                    break;
                case (byte)CustomRPC.SetTiebreak:
                    RPCProcedure.setTiebreak();
                    break;
                case (byte)CustomRPC.SetInvisible:
                    byte invisiblePlayer = reader.ReadByte();
                    byte invisibleFlag = reader.ReadByte();
                    RPCProcedure.setInvisible(invisiblePlayer, invisibleFlag);
                    break;
                case (byte)CustomRPC.SetSwoop:
                    byte invisiblePlayer2 = reader.ReadByte();
                    byte invisibleFlag2 = reader.ReadByte();
                    RPCProcedure.setSwoop(invisiblePlayer2, invisibleFlag2);
                    break;  
                case (byte)CustomRPC.SetInvisibleGen:
                    byte invisiblePlayer3 = reader.ReadByte();
                    byte invisibleFlag3 = reader.ReadByte();
                    RPCProcedure.setInvisibleGen(invisiblePlayer3, invisibleFlag3);
                    break; 
                case (byte)CustomRPC.Mine:
                    var newVentId = reader.ReadInt32();
                    var role = Helpers.playerById(reader.ReadByte());
                    var pos = reader.ReadBytesAndSize();
                    var zAxis = reader.ReadSingle();            
                    RPCProcedure.Mine(newVentId, role, pos, zAxis);
                    break;
                case (byte)CustomRPC.ThiefStealsRole:
                    byte thiefTargetId = reader.ReadByte();
                    RPCProcedure.thiefStealsRole(thiefTargetId);
                    break;
                case (byte)CustomRPC.SetTrap:
                    RPCProcedure.setTrap(reader.ReadBytesAndSize());
                    break;
                case (byte)CustomRPC.TriggerTrap:
                    byte trappedPlayer = reader.ReadByte();
                    byte trapId = reader.ReadByte();
                    RPCProcedure.triggerTrap(trappedPlayer, trapId);
                    break;
                case (byte)CustomRPC.PlaceBomb:
                    RPCProcedure.placeBomb(reader.ReadBytesAndSize());
                    break;
                case (byte)CustomRPC.DefuseBomb:
                    RPCProcedure.defuseBomb();
                    break;
                case (byte)CustomRPC.ShareGamemode:
                    byte gm = reader.ReadByte();
                    RPCProcedure.shareGamemode(gm);
                    break;
                case (byte)CustomRPC.AmnisiacTakeRole:
                    RPCProcedure.amnisiacTakeRole(reader.ReadByte());
                    break;
                case (byte)CustomRPC.TurnToImpostor:
                    RPCProcedure.turnToImpostor(reader.ReadByte());
                    break;
                case (byte)CustomRPC.Disperse:
                    RPCProcedure.disperse();
                    break;
                case (byte)CustomRPC.PitfallBody:
                    RPCProcedure.pitfallBody(reader.ReadByte());
                    break;
                case (byte)CustomRPC.ShinobiExpand:
                    RPCProcedure.shinobiExpand();
                    break;
                case (byte)CustomRPC.TransporterSwap:
                    RPCProcedure.TransporterSwap(reader.ReadByte());
                    break;
                case (byte)CustomRPC.TransportistSwap:
                    RPCProcedure.TransportistSwap(reader.ReadByte());
                    break;
                case (byte)CustomRPC.TeleporterTeleport:
                    RPCProcedure.teleporterTeleport(reader.ReadByte(), reader.ReadByte());
                    break;
                case (byte)CustomRPC.PlaceSnare:
                    RPCProcedure.placeSnare(reader.ReadBytesAndSize());
                    break;
                case (byte)CustomRPC.ClearSnare:
                    RPCProcedure.clearSnare();
                    break;
                case (byte)CustomRPC.ActivateSnare:
                    RPCProcedure.activateSnare(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
                    break;
                case (byte)CustomRPC.DisableSnare:
                    RPCProcedure.disableSnare(reader.ReadByte());
                    break;
                case (byte)CustomRPC.SnarerMeetingFlag:
                    RPCProcedure.snarerMeetingFlag();
                    break;
                case (byte)CustomRPC.SnarerKill:
                    RPCProcedure.snarerKill(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
                    break;
                case (byte)CustomRPC.ArbiterSpecialVote:
                    byte id = reader.ReadByte();
                    byte targetId = reader.ReadByte();
                    RPCProcedure.arbiterSpecialVote(id, targetId);
                    if (AmongUsClient.Instance.AmHost && Arbiter.isArbiter(id))
                    {
                        int clientId = Helpers.GetClientId(Arbiter.arbiter);
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(CachedPlayer.LocalPlayer.PlayerControl.NetId, (byte)CustomRPC.ArbiterSpecialVote_DoCastVote, Hazel.SendOption.Reliable, clientId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                    }
                    break;
                case (byte)CustomRPC.ArbiterSpecialVote_DoCastVote:
                    RPCProcedure.arbiterSpecialVote_DoCastVote();
                    break;
                case (byte)CustomRPC.SetFutureReveal:
                    RPCProcedure.setFutureReveal(reader.ReadByte());
                    break;
                case (byte)CustomRPC.KataomoiSetTarget:
                    playerId = reader.ReadByte();
                    RPCProcedure.kataomoiSetTarget(playerId);
                    break;
                case (byte)CustomRPC.KataomoiWin:
                    RPCProcedure.kataomoiWin();
                    break;
                case (byte)CustomRPC.KataomoiStalking:
                    playerId = reader.ReadByte();
                    RPCProcedure.kataomoiStalking(playerId);
                    break;
                case (byte)CustomRPC.AkujoSetHonmei:
                    RPCProcedure.akujoSetHonmei(reader.ReadByte(), reader.ReadByte());
                    break;
                case (byte)CustomRPC.AkujoSetKeep:
                    RPCProcedure.akujoSetKeep(reader.ReadByte(), reader.ReadByte());
                    break;
                case (byte)CustomRPC.AkujoSuicide:
                    RPCProcedure.akujoSuicide(reader.ReadByte());
                    break;
                case (byte)CustomRPC.SetBrainwash:
                    RPCProcedure.setBrainwash(reader.ReadByte());
                    break;
                case (byte)CustomRPC.CerenovusKill:
                    RPCProcedure.cerenovusKill(reader.ReadByte());
                    break;

                case (byte)CustomRPC.StopStart:
                    RPCProcedure.stopStart(reader.ReadByte());
                    break;
                case (byte)CustomRPC.YoyoMarkLocation:
                    RPCProcedure.yoyoMarkLocation(reader.ReadBytesAndSize());
                    break;
                case (byte)CustomRPC.YoyoBlink:
                    RPCProcedure.yoyoBlink(reader.ReadByte() == byte.MaxValue, reader.ReadBytesAndSize());
                    break;

                // Game mode
                case (byte)CustomRPC.SetGuesserGm:
                    byte guesserGm = reader.ReadByte();
                    RPCProcedure.setGuesserGm(guesserGm);
                    break;
                case (byte)CustomRPC.ShareTimer:
                    float punish = reader.ReadSingle();
                    RPCProcedure.shareTimer(punish);
                    break;
                case (byte)CustomRPC.HuntedShield:
                    byte huntedPlayer = reader.ReadByte();
                    RPCProcedure.huntedShield(huntedPlayer);
                    break;
                case (byte)CustomRPC.HuntedRewindTime:
                    byte rewindPlayer = reader.ReadByte();
                    RPCProcedure.huntedRewindTime(rewindPlayer);
                    break;
                case (byte)CustomRPC.PropHuntStartTimer:
                    RPCProcedure.propHuntStartTimer(reader.ReadBoolean());
                    break;
                case (byte)CustomRPC.SetProp:
                    byte targetPlayer = reader.ReadByte();
                    string propName = reader.ReadString();
                    float posX = reader.ReadSingle();
                    RPCProcedure.propHuntSetProp(targetPlayer, propName, posX);
                    break;
                case (byte)CustomRPC.SetRevealed:
                    RPCProcedure.propHuntSetRevealed(reader.ReadByte());
                    break;
                case (byte)CustomRPC.PropHuntSetInvis:
                    RPCProcedure.propHuntSetInvis(reader.ReadByte());
                    break;
                case (byte)CustomRPC.PropHuntSetSpeedboost:
                    RPCProcedure.propHuntSetSpeedboost(reader.ReadByte());
                    break;
                case (byte)CustomRPC.ShareGhostInfo:
                    RPCProcedure.receiveGhostInfo(reader.ReadByte(), reader);
                    break;


                case (byte)CustomRPC.ShareRoom:
                    byte roomPlayer = reader.ReadByte();
                    byte roomId = reader.ReadByte();
                    RPCProcedure.shareRoom(roomPlayer, roomId);
                    break;
            }
        }
    }
} 
