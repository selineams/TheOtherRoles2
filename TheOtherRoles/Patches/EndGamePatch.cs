  
using HarmonyLib;
using static TheOtherRoles.TheOtherRoles;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using System.Text;
using TheOtherRoles.Players;
using TheOtherRoles.Utilities;
using TheOtherRoles.CustomGameModes;

namespace TheOtherRoles.Patches {
    enum CustomGameOverReason {
        LoversWin = 10,
        TeamJackalWin = 11,
        MiniLose = 12,
        JesterWin = 13,
        ArsonistWin = 14,
        VultureWin = 15,
        ProsecutorWin = 16,
		SwooperWin = 17,
        WerewolfWin = 18,
        JuggernautWin = 19,
        DoomsayerWin = 20,
        KataomoiWin = 21,
        AkujoWin = 22,
        CerenovusWin = 23
    }

    enum WinCondition {
        Default,
        LoversTeamWin,
        LoversSoloWin,
        JesterWin,
        JackalWin,
        MiniLose,
        ArsonistWin,
        VultureWin,
        AdditionalLawyerBonusWin,
        AdditionalAlivePursuerWin,
		ProsecutorWin,
		SwooperWin,
        WerewolfWin,
        JuggernautWin,
        DoomsayerWin,
        KataomoiWin,
        AkujoWin,
        CerenovusWin
	}

    static class AdditionalTempData {
        // Should be implemented using a proper GameOverReason in the future
        public static WinCondition winCondition = WinCondition.Default;
        public static List<WinCondition> additionalWinConditions = new List<WinCondition>();
        public static List<PlayerRoleInfo> playerRoles = new List<PlayerRoleInfo>();
        public static float timer = 0;

        public static void clear() {
            playerRoles.Clear();
            additionalWinConditions.Clear();
            winCondition = WinCondition.Default;
            timer = 0;
        }

        internal class PlayerRoleInfo {
            public string PlayerName { get; set; }
            public List<RoleInfo> Roles {get;set;}
            public string RoleNames { get; set; }
            public int TasksCompleted  {get;set;}
            public int TasksTotal  {get;set;}
            public bool IsGuesser {get; set;}
            public int? Kills {get; set;}
            public bool IsAlive { get; set; }
            public string ExtraInfo { get; set; }
        }
    }


    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnGameEnd))]
    public class OnGameEndPatch {
        private static GameOverReason gameOverReason;
        public static void Prefix(AmongUsClient __instance, [HarmonyArgument(0)]ref EndGameResult endGameResult) {
            gameOverReason = endGameResult.GameOverReason;
            if ((int)endGameResult.GameOverReason >= 10) endGameResult.GameOverReason = GameOverReason.ImpostorByKill;

            // Reset zoomed out ghosts
            Helpers.toggleZoom(reset: true);
        }

        public static void Postfix(AmongUsClient __instance, [HarmonyArgument(0)]ref EndGameResult endGameResult) {
            AdditionalTempData.clear();

            foreach(var playerControl in CachedPlayer.AllPlayers) {
                var roles = RoleInfo.getRoleInfoForPlayer(playerControl);
                var (tasksCompleted, tasksTotal) = TasksHandler.taskInfo(playerControl.Data);
                bool isGuesser = HandleGuesser.isGuesserGm && HandleGuesser.isGuesser(playerControl.PlayerId);
                int? killCount = GameHistory.deadPlayers.FindAll(x => x.killerIfExisting != null && x.killerIfExisting.PlayerId == playerControl.PlayerId).Count;
                if (killCount == 0 && !(new List<RoleInfo>() { RoleInfo.sheriff, RoleInfo.jackal, RoleInfo.sidekick, RoleInfo.thief, RoleInfo.cerenovus }.Contains(RoleInfo.getRoleInfoForPlayer(playerControl, false).FirstOrDefault()) || playerControl.Data.Role.IsImpostor)) {
                    killCount = null;
                    }
                string roleString = RoleInfo.GetRolesString(playerControl, true, true, false);
                string extraInfo = "";
                if (Kataomoi.kataomoi != null && Kataomoi.target == playerControl.PlayerControl)
                        extraInfo = Helpers.cs(Kataomoi.color, "♡");
                AdditionalTempData.playerRoles.Add(new AdditionalTempData.PlayerRoleInfo() { PlayerName = playerControl.Data.PlayerName, Roles = roles, RoleNames = roleString, TasksTotal = tasksTotal, TasksCompleted = tasksCompleted, IsGuesser = isGuesser, Kills = killCount, IsAlive = !playerControl.Data.IsDead, ExtraInfo = extraInfo});
            }

            // Remove Jester, Arsonist, Vulture, Jackal, former Jackals and Sidekick from winners (if they win, they'll be readded)
            // Remove Jester, Prosecutor, Amnesiac, Arsonist, Vulture, Jackal, former Jackals and Sidekick from winners (if they win, they'll be readded)
            // Remove Jester, Swooper, Prosecutor, Amnesiac, Arsonist, Vulture, Jackal, former Jackals and Sidekick from winners (if they win, they'll be readded)
            List<PlayerControl> notWinners = new List<PlayerControl>();
            if (Jester.jester != null) notWinners.Add(Jester.jester);
			if (Swooper.swooper != null) notWinners.Add(Swooper.swooper);
            if (Sidekick.sidekick != null) notWinners.Add(Sidekick.sidekick);
            if (Amnisiac.amnisiac != null) notWinners.Add(Amnisiac.amnisiac);
            if (Jackal.jackal != null) notWinners.Add(Jackal.jackal);
            if (Arsonist.arsonist != null) notWinners.Add(Arsonist.arsonist);
            if (Vulture.vulture != null) notWinners.Add(Vulture.vulture);
            if (Werewolf.werewolf != null) notWinners.Add(Werewolf.werewolf);
            if (Juggernaut.juggernaut != null) notWinners.Add(Juggernaut.juggernaut);
            if (Doomsayer.doomsayer != null) notWinners.Add(Doomsayer.doomsayer);
            if (Kataomoi.kataomoi != null) notWinners.Add(Kataomoi.kataomoi);
            if (Akujo.akujo != null) notWinners.Add(Akujo.akujo);
            if (Cerenovus.cerenovus != null) notWinners.Add(Cerenovus.cerenovus);
            if (Cerenovus.formerCerenovus != null) notWinners.Add(Cerenovus.formerCerenovus);
            if (Lawyer.lawyer != null) notWinners.Add(Lawyer.lawyer);
            if (Pursuer.pursuer != null) notWinners.Add(Pursuer.pursuer);
            if (Thief.thief != null) notWinners.Add(Thief.thief);

            notWinners.AddRange(Jackal.formerJackals);

            List<WinningPlayerData> winnersToRemove = new List<WinningPlayerData>();
            foreach (WinningPlayerData winner in TempData.winners.GetFastEnumerator()) {
                if (notWinners.Any(x => x.Data.PlayerName == winner.PlayerName)) winnersToRemove.Add(winner);
            }
            foreach (var winner in winnersToRemove) TempData.winners.Remove(winner);

            bool jesterWin = Jester.jester != null && gameOverReason == (GameOverReason)CustomGameOverReason.JesterWin;
			bool swooperWin = gameOverReason == (GameOverReason)CustomGameOverReason.SwooperWin && ((Swooper.swooper != null && !Swooper.swooper.Data.IsDead));
            bool werewolfWin = gameOverReason == (GameOverReason)CustomGameOverReason.WerewolfWin && ((Werewolf.werewolf != null && !Werewolf.werewolf.Data.IsDead));
            bool juggernautWin = gameOverReason == (GameOverReason)CustomGameOverReason.JuggernautWin && ((Juggernaut.juggernaut != null && !Juggernaut.juggernaut.Data.IsDead));
            bool doomsayerWin = Doomsayer.doomsayer != null && gameOverReason == (GameOverReason)CustomGameOverReason.DoomsayerWin;
            bool kataomoiWin = Kataomoi.kataomoi != null && gameOverReason == (GameOverReason)CustomGameOverReason.KataomoiWin;
            bool akujoWin = Akujo.akujo != null && gameOverReason == (GameOverReason)CustomGameOverReason.AkujoWin && (Akujo.honmei != null && !Akujo.honmei.Data.IsDead && !Akujo.akujo.Data.IsDead);
            bool cerenovusWin = Cerenovus.cerenovus != null && gameOverReason == (GameOverReason)CustomGameOverReason.CerenovusWin;
            bool arsonistWin = Arsonist.arsonist != null && gameOverReason == (GameOverReason)CustomGameOverReason.ArsonistWin;
            bool miniLose = Mini.mini != null && gameOverReason == (GameOverReason)CustomGameOverReason.MiniLose;
            bool loversWin = Lovers.existingAndAlive() && (gameOverReason == (GameOverReason)CustomGameOverReason.LoversWin || (GameManager.Instance.DidHumansWin(gameOverReason) && !Lovers.existingWithKiller())); // Either they win if they are among the last 3 players, or they win if they are both Crewmates and both alive and the Crew wins (Team Imp/Jackal Lovers can only win solo wins)
            bool teamJackalWin = gameOverReason == (GameOverReason)CustomGameOverReason.TeamJackalWin && ((Jackal.jackal != null && !Jackal.jackal.Data.IsDead) || (Sidekick.sidekick != null && !Sidekick.sidekick.Data.IsDead));
            bool vultureWin = Vulture.vulture != null && gameOverReason == (GameOverReason)CustomGameOverReason.VultureWin;
            bool prosecutorWin = Lawyer.lawyer != null && gameOverReason == (GameOverReason)CustomGameOverReason.ProsecutorWin;

            bool isPursurerLose = jesterWin || arsonistWin || miniLose || vultureWin|| doomsayerWin || kataomoiWin || akujoWin || teamJackalWin;

            // Mini lose
            if (miniLose) {
                TempData.winners = new Il2CppSystem.Collections.Generic.List<WinningPlayerData>();
                WinningPlayerData wpd = new WinningPlayerData(Mini.mini.Data);
                wpd.IsYou = false; // If "no one is the Mini", it will display the Mini, but also show defeat to everyone
                TempData.winners.Add(wpd);
                AdditionalTempData.winCondition = WinCondition.MiniLose;  
            }

            // Jester win
            else if (jesterWin) {
                TempData.winners = new Il2CppSystem.Collections.Generic.List<WinningPlayerData>();
                WinningPlayerData wpd = new WinningPlayerData(Jester.jester.Data);
                TempData.winners.Add(wpd);
                AdditionalTempData.winCondition = WinCondition.JesterWin;
            }

            // Arsonist win
            else if (arsonistWin) {
                TempData.winners = new Il2CppSystem.Collections.Generic.List<WinningPlayerData>();
                WinningPlayerData wpd = new WinningPlayerData(Arsonist.arsonist.Data);
                TempData.winners.Add(wpd);
                AdditionalTempData.winCondition = WinCondition.ArsonistWin;
            }

            // Vulture win
            else if (vultureWin) {
                TempData.winners = new Il2CppSystem.Collections.Generic.List<WinningPlayerData>();
                WinningPlayerData wpd = new WinningPlayerData(Vulture.vulture.Data);
                TempData.winners.Add(wpd);
                AdditionalTempData.winCondition = WinCondition.VultureWin;
            }

            // Jester win
            else if (prosecutorWin) {
                TempData.winners = new Il2CppSystem.Collections.Generic.List<WinningPlayerData>();
                WinningPlayerData wpd = new WinningPlayerData(Lawyer.lawyer.Data);
                TempData.winners.Add(wpd);
                AdditionalTempData.winCondition = WinCondition.ProsecutorWin;
            }

            // Lovers win conditions
            else if (loversWin) {
                // Double win for lovers, crewmates also win
                if (!Lovers.existingWithKiller()) {
                    AdditionalTempData.winCondition = WinCondition.LoversTeamWin;
                    TempData.winners = new Il2CppSystem.Collections.Generic.List<WinningPlayerData>();
                    foreach (PlayerControl p in CachedPlayer.AllPlayers) {
                        if (p == null) continue;
                        if (p == Lovers.lover1 || p == Lovers.lover2)
                            TempData.winners.Add(new WinningPlayerData(p.Data));
                        else if (p == Pursuer.pursuer && !Pursuer.pursuer.Data.IsDead)
                            TempData.winners.Add(new WinningPlayerData(p.Data));
                        else if (p != Jester.jester && p != Jackal.jackal && p != Sidekick.sidekick && p != Swooper.swooper && p != Werewolf.werewolf && p != Juggernaut.juggernaut && p != Doomsayer.doomsayer && p != Kataomoi.kataomoi && p != Akujo.akujo && p != Cerenovus.cerenovus && p != Cerenovus.formerCerenovus && p != Arsonist.arsonist && p != Vulture.vulture && !Jackal.formerJackals.Contains(p) && !p.Data.Role.IsImpostor)
                            TempData.winners.Add(new WinningPlayerData(p.Data));
                    }
                }
                // Lovers solo win
                else {
                    AdditionalTempData.winCondition = WinCondition.LoversSoloWin;
                    TempData.winners = new Il2CppSystem.Collections.Generic.List<WinningPlayerData>();
                    TempData.winners.Add(new WinningPlayerData(Lovers.lover1.Data));
                    TempData.winners.Add(new WinningPlayerData(Lovers.lover2.Data));
                }
            }
            
            // Jackal win condition (should be implemented using a proper GameOverReason in the future)
            else if (teamJackalWin) {
                // Jackal wins if nobody except jackal is alive
                AdditionalTempData.winCondition = WinCondition.JackalWin;
                TempData.winners = new Il2CppSystem.Collections.Generic.List<WinningPlayerData>();
                WinningPlayerData wpd = new WinningPlayerData(Jackal.jackal.Data);
                wpd.IsImpostor = false; 
                TempData.winners.Add(wpd);
                // If there is a sidekick. The sidekick also wins
                if (Sidekick.sidekick != null) {
                    WinningPlayerData wpdSidekick = new WinningPlayerData(Sidekick.sidekick.Data);
                    wpdSidekick.IsImpostor = false; 
                    TempData.winners.Add(wpdSidekick);
                }
                foreach(var player in Jackal.formerJackals) {
                    WinningPlayerData wpdFormerJackal = new WinningPlayerData(player.Data);
                    wpdFormerJackal.IsImpostor = false; 
                    TempData.winners.Add(wpdFormerJackal);
                }
            }

			else if (swooperWin) {
                // Swooper wins if nobody except jackal is alive
                AdditionalTempData.winCondition = WinCondition.SwooperWin;
                TempData.winners = new Il2CppSystem.Collections.Generic.List<WinningPlayerData>();
                WinningPlayerData wpd = new WinningPlayerData(Swooper.swooper.Data);
                wpd.IsImpostor = false; 
                TempData.winners.Add(wpd);
            }
            
			else if (werewolfWin) {
                // Werewolf wins if nobody except jackal is alive
                AdditionalTempData.winCondition = WinCondition.WerewolfWin;
                TempData.winners = new Il2CppSystem.Collections.Generic.List<WinningPlayerData>();
                WinningPlayerData wpd = new WinningPlayerData(Werewolf.werewolf.Data);
                wpd.IsImpostor = false; 
                TempData.winners.Add(wpd);
            }
            else if (juggernautWin){
                // JuggernautWin wins if nobody except jackal is alive
                AdditionalTempData.winCondition = WinCondition.JuggernautWin;
                TempData.winners = new Il2CppSystem.Collections.Generic.List<WinningPlayerData>();
                WinningPlayerData wpd = new WinningPlayerData(Juggernaut.juggernaut.Data);
                wpd.IsImpostor = false;
                TempData.winners.Add(wpd);
            }
            else if (doomsayerWin){
                // DoomsayerWin wins if nobody except jackal is alive
                AdditionalTempData.winCondition = WinCondition.DoomsayerWin;
                TempData.winners = new Il2CppSystem.Collections.Generic.List<WinningPlayerData>();
                WinningPlayerData wpd = new WinningPlayerData(Doomsayer.doomsayer.Data);
                TempData.winners.Add(wpd);
                AdditionalTempData.winCondition = WinCondition.DoomsayerWin;
            }
            // Kataomoi win
            else if (kataomoiWin){
                TempData.winners = new Il2CppSystem.Collections.Generic.List<WinningPlayerData>();
                WinningPlayerData wpd = new WinningPlayerData(Kataomoi.kataomoi.Data);
                TempData.winners.Add(wpd);
                AdditionalTempData.winCondition = WinCondition.KataomoiWin;
            }
            // Akujo win
            else if (akujoWin){
                AdditionalTempData.winCondition = WinCondition.AkujoWin;
                TempData.winners = new Il2CppSystem.Collections.Generic.List<WinningPlayerData>();
                TempData.winners.Add(new WinningPlayerData(Akujo.akujo.Data));
                TempData.winners.Add(new WinningPlayerData(Akujo.honmei.Data));
            }
             else if (cerenovusWin){
                TempData.winners = new Il2CppSystem.Collections.Generic.List<WinningPlayerData>();
                WinningPlayerData wpd = new WinningPlayerData(Cerenovus.cerenovus.Data);
                TempData.winners.Add(wpd);
                AdditionalTempData.winCondition = WinCondition.CerenovusWin;

                if (Cerenovus.formerCerenovus != null)
                {
                    WinningPlayerData wpdFormerCerenovus = new WinningPlayerData(Cerenovus.formerCerenovus.Data);
                    TempData.winners.Add(wpdFormerCerenovus);
                }
            }
			


            // Possible Additional winner: Lawyer
            if (Lawyer.lawyer != null && Lawyer.target != null && (!Lawyer.target.Data.IsDead || Lawyer.target == Jester.jester) && !Pursuer.notAckedExiled && !Lawyer.isProsecutor) {
                WinningPlayerData winningClient = null;
                foreach (WinningPlayerData winner in TempData.winners.GetFastEnumerator()) {
                    if (winner.PlayerName == Lawyer.target.Data.PlayerName)
                        winningClient = winner;
                }
                if (winningClient != null) { // The Lawyer wins if the client is winning (and alive, but if he wasn't the Lawyer shouldn't exist anymore)
                    if (!TempData.winners.ToArray().Any(x => x.PlayerName == Lawyer.lawyer.Data.PlayerName))
                        TempData.winners.Add(new WinningPlayerData(Lawyer.lawyer.Data));
                    AdditionalTempData.additionalWinConditions.Add(WinCondition.AdditionalLawyerBonusWin); // The Lawyer wins together with the client
                } 
            }

            // Possible Additional winner: Pursuer
            if (Pursuer.pursuer != null && !Pursuer.pursuer.Data.IsDead && !Pursuer.notAckedExiled && !isPursurerLose && !TempData.winners.ToArray().Any(x => x.IsImpostor)) {
                if (!TempData.winners.ToArray().Any(x => x.PlayerName == Pursuer.pursuer.Data.PlayerName))
                    TempData.winners.Add(new WinningPlayerData(Pursuer.pursuer.Data));
                AdditionalTempData.additionalWinConditions.Add(WinCondition.AdditionalAlivePursuerWin);
            }

            AdditionalTempData.timer = ((float)(DateTime.UtcNow - (HideNSeek.isHideNSeekGM ? HideNSeek.startTime : PropHunt.startTime)).TotalMilliseconds) / 1000;

            // Reset Settings
            if (TORMapOptions.gameMode == CustomGamemodes.HideNSeek ) ShipStatusPatch.resetVanillaSettings();
            RPCProcedure.resetVariables();
            EventUtility.gameEndsUpdate();
        }
    }

    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.SetEverythingUp))]
    public class EndGameManagerSetUpPatch {
        public static void Postfix(EndGameManager __instance) {
            // Delete and readd PoolablePlayers always showing the name and role of the player
            foreach (PoolablePlayer pb in __instance.transform.GetComponentsInChildren<PoolablePlayer>()) {
                UnityEngine.Object.Destroy(pb.gameObject);
            }
            int num = Mathf.CeilToInt(7.5f);
            List<WinningPlayerData> list = TempData.winners.ToArray().ToList().OrderBy(delegate(WinningPlayerData b)
            {
                if (!b.IsYou)
                {
                    return 0;
                }
                return -1;
            }).ToList<WinningPlayerData>();
            for (int i = 0; i < list.Count; i++) {
                WinningPlayerData winningPlayerData2 = list[i];
                int num2 = (i % 2 == 0) ? -1 : 1;
                int num3 = (i + 1) / 2;
                float num4 = (float)num3 / (float)num;
                float num5 = Mathf.Lerp(1f, 0.75f, num4);
                float num6 = (float)((i == 0) ? -8 : -1);
                PoolablePlayer poolablePlayer = UnityEngine.Object.Instantiate<PoolablePlayer>(__instance.PlayerPrefab, __instance.transform);
                poolablePlayer.transform.localPosition = new Vector3(1f * (float)num2 * (float)num3 * num5, FloatRange.SpreadToEdges(-1.125f, 0f, num3, num), num6 + (float)num3 * 0.01f) * 0.9f;
                float num7 = Mathf.Lerp(1f, 0.65f, num4) * 0.9f;
                Vector3 vector = new Vector3(num7, num7, 1f);
                poolablePlayer.transform.localScale = vector;
                if (winningPlayerData2.IsDead) {
                    poolablePlayer.SetBodyAsGhost();
                    poolablePlayer.SetDeadFlipX(i % 2 == 0);
                } else {
                    poolablePlayer.SetFlipX(i % 2 == 0);
                }
                poolablePlayer.UpdateFromPlayerOutfit(winningPlayerData2, PlayerMaterial.MaskType.None, winningPlayerData2.IsDead, true);

                poolablePlayer.cosmetics.nameText.color = Color.white;
                poolablePlayer.cosmetics.nameText.transform.localScale = new Vector3(1f / vector.x, 1f / vector.y, 1f / vector.z);
                poolablePlayer.cosmetics.nameText.transform.localPosition = new Vector3(poolablePlayer.cosmetics.nameText.transform.localPosition.x, poolablePlayer.cosmetics.nameText.transform.localPosition.y, -15f);
                poolablePlayer.cosmetics.nameText.text = winningPlayerData2.PlayerName;

                foreach(var data in AdditionalTempData.playerRoles) {
                    if (data.PlayerName != winningPlayerData2.PlayerName) continue;
                    var roles = 
                    poolablePlayer.cosmetics.nameText.text += $"\n{string.Join("\n", data.Roles.Select(x => Helpers.cs(x.color, x.name)))}";
                }
            }

            // Additional code
            GameObject bonusText = UnityEngine.Object.Instantiate(__instance.WinText.gameObject);
            bonusText.transform.position = new Vector3(__instance.WinText.transform.position.x, __instance.WinText.transform.position.y - 0.5f, __instance.WinText.transform.position.z);
            bonusText.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            TMPro.TMP_Text textRenderer = bonusText.GetComponent<TMPro.TMP_Text>();
            textRenderer.text = "";

            if (AdditionalTempData.winCondition == WinCondition.JesterWin) {
                textRenderer.text = "终于..不用被人嘲笑了, 哈..哈哈哈...";
                textRenderer.color = Jester.color;
            }
            else if (AdditionalTempData.winCondition == WinCondition.SwooperWin) {
                textRenderer.text = "来无影去无踪";
                textRenderer.color = Swooper.color;
            }
            else if (AdditionalTempData.winCondition == WinCondition.WerewolfWin) {
                textRenderer.text = "嗷呜~~~";
                textRenderer.color = Werewolf.color;
            }
            else if (AdditionalTempData.winCondition == WinCondition.JuggernautWin){
                textRenderer.text = "癫狂屠戮";
                textRenderer.color = Juggernaut.color;
            }
            else if (AdditionalTempData.winCondition == WinCondition.DoomsayerWin){
                textRenderer.text = "赌神！！！！！！！！";
                textRenderer.color = Doomsayer.color;
            }
            else if (AdditionalTempData.winCondition == WinCondition.KataomoiWin) {
                textRenderer.text = "单相思胜利";
                foreach (var data in AdditionalTempData.playerRoles) {
                    if (data.ExtraInfo.Contains("♡")) {
                        textRenderer.text += $"\n我想让你更加沉溺于扭曲的爱之中...";
                        break;
                    }
                }
                textRenderer.color = Kataomoi.color;
                __instance.BackgroundBar.material.SetColor("_Color", Kataomoi.color);
            }
            else if (AdditionalTempData.winCondition == WinCondition.AkujoWin) {
                textRenderer.text = "魅魔胜利！";
                foreach (var data in AdditionalTempData.playerRoles){
                    if (data.ExtraInfo.Contains("♥")){
                        textRenderer.text += $"\n我想让你更加沉溺于扭曲的爱之中...";
                        break;
                    }
                }
                textRenderer.color = Akujo.color;
                __instance.BackgroundBar.material.SetColor("_Color", Akujo.color);
            }
            else if (AdditionalTempData.winCondition == WinCondition.CerenovusWin){
                textRenderer.text = "洗脑师胜利";
                textRenderer.color = Cerenovus.color;
                __instance.BackgroundBar.material.SetColor("_Color", Cerenovus.color);
            }  
            else if (AdditionalTempData.winCondition == WinCondition.ArsonistWin) {
                textRenderer.text = "天干物燥，小心火烛";
                textRenderer.color = Arsonist.color;
            }
            else if (AdditionalTempData.winCondition == WinCondition.VultureWin) {
                textRenderer.text = "多谢款待！";
                textRenderer.color = Vulture.color;
            }
            else if (AdditionalTempData.winCondition == WinCondition.ProsecutorWin) {
                textRenderer.text = "異議あり!";
                textRenderer.color = Lawyer.color;
            }
            else if (AdditionalTempData.winCondition == WinCondition.LoversTeamWin) {
                textRenderer.text = "恋人和船员同时胜利";
                textRenderer.color = Lovers.color;
                __instance.BackgroundBar.material.SetColor("_Color", Lovers.color);
            } 
            else if (AdditionalTempData.winCondition == WinCondition.LoversSoloWin) {
                textRenderer.text = "♥♥♥磕到了♥♥♥";
                textRenderer.color = Lovers.color;
                __instance.BackgroundBar.material.SetColor("_Color", Lovers.color);
            }
            else if (AdditionalTempData.winCondition == WinCondition.JackalWin) {
                textRenderer.text = "如狼似虎";
                textRenderer.color = Jackal.color;
            }
            else if (AdditionalTempData.winCondition == WinCondition.MiniLose) {
                textRenderer.text = "他还只是个孩子啊";
                textRenderer.color = Mini.color;
            }

            foreach (WinCondition cond in AdditionalTempData.additionalWinConditions) {
                if (cond == WinCondition.AdditionalLawyerBonusWin) {
                    textRenderer.text += $"\n{Helpers.cs(Lawyer.color, "律师和客户胜利")}";
                } else if (cond == WinCondition.AdditionalAlivePursuerWin) {
                    textRenderer.text += $"\n{Helpers.cs(Pursuer.color, "起诉者存活")}";
                }
            }

            if (TORMapOptions.showRoleSummary || HideNSeek.isHideNSeekGM || PropHunt.isPropHuntGM) {
                var position = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, Camera.main.nearClipPlane));
                GameObject roleSummary = UnityEngine.Object.Instantiate(__instance.WinText.gameObject);
                roleSummary.transform.position = new Vector3(__instance.Navigation.ExitButton.transform.position.x + 0.1f, position.y - 0.1f, -214f); 
                roleSummary.transform.localScale = new Vector3(1f, 1f, 1f);

                var roleSummaryText = new StringBuilder();
                if (HideNSeek.isHideNSeekGM || PropHunt.isPropHuntGM) {
                    int minutes = (int)AdditionalTempData.timer / 60;
                    int seconds = (int)AdditionalTempData.timer % 60;
                    roleSummaryText.AppendLine($"<color=#FAD934FF>游戏时长: {minutes:00}:{seconds:00}</color> \n");
                }
                roleSummaryText.AppendLine("游戏总结:");
                foreach(var data in AdditionalTempData.playerRoles) {
                    //var roles = string.Join(" ", data.Roles.Select(x => Helpers.cs(x.color, x.name)));
                    string roles = data.RoleNames;
                    //if (data.IsGuesser) roles += " (Guesser)";
                    var taskInfo = data.TasksTotal > 0 ? $" - <color=#FAD934FF>({data.TasksCompleted}/{data.TasksTotal})</color>" : "";
                    if (data.Kills != null) taskInfo += $" - <color=#FF0000FF>(击杀数: {data.Kills})</color>";
                    roleSummaryText.AppendLine($"{Helpers.cs(data.IsAlive ? Color.white : new Color(.7f,.7f,.7f), data.PlayerName)} - {roles}{taskInfo}"); 
                }
                TMPro.TMP_Text roleSummaryTextMesh = roleSummary.GetComponent<TMPro.TMP_Text>();
                roleSummaryTextMesh.alignment = TMPro.TextAlignmentOptions.TopLeft;
                roleSummaryTextMesh.color = Color.white;
                roleSummaryTextMesh.fontSizeMin = 1.5f;
                roleSummaryTextMesh.fontSizeMax = 1.5f;
                roleSummaryTextMesh.fontSize = 1.5f;
                
                var roleSummaryTextMeshRectTransform = roleSummaryTextMesh.GetComponent<RectTransform>();
                roleSummaryTextMeshRectTransform.anchoredPosition = new Vector2(position.x + 3.5f, position.y - 0.1f);
                roleSummaryTextMesh.text = roleSummaryText.ToString();
            }
            AdditionalTempData.clear();
        }
    }

    [HarmonyPatch(typeof(LogicGameFlowNormal), nameof(LogicGameFlowNormal.CheckEndCriteria))] 
    class CheckEndCriteriaPatch {
        public static bool Prefix(ShipStatus __instance) {
            if (!GameData.Instance) return false;
            if (DestroyableSingleton<TutorialManager>.InstanceExists) // InstanceExists | Don't check Custom Criteria when in Tutorial
                return true;
            var statistics = new PlayerStatistics(__instance);
            if (CheckAndEndGameForMiniLose(__instance)) return false;
            if (CheckAndEndGameForJesterWin(__instance)) return false;
            if (CheckAndEndGameForDoomsayerWin(__instance)) return false;
            if (CheckAndEndGameForKataomoiWin(__instance)) return false;
            if (CheckAndEndGameForAkujoWin(__instance, statistics)) return false;
            if (CheckAndEndGameForArsonistWin(__instance)) return false;
            if (CheckAndEndGameForVultureWin(__instance)) return false;
            if (CheckAndEndGameForSabotageWin(__instance)) return false;
            if (CheckAndEndGameForTaskWin(__instance)) return false;
            if (CheckAndEndGameForProsecutorWin(__instance)) return false;
            if (CheckAndEndGameForLoverWin(__instance, statistics)) return false;
            if (CheckAndEndGameForJackalWin(__instance, statistics)) return false;
			if (CheckAndEndGameForSwooperWin(__instance, statistics)) return false;
            if (CheckAndEndGameForWerewolfWin(__instance,statistics)) return false;
            if (CheckAndEndGameForJuggernautWin(__instance, statistics)) return false;
            if (CheckAndEndGameForCerenovusWin(__instance, statistics)) return false;
            if (CheckAndEndGameForImpostorWin(__instance, statistics)) return false;
            if (CheckAndEndGameForCrewmateWin(__instance, statistics)) return false;
            return false;
        }

        private static bool CheckAndEndGameForMiniLose(ShipStatus __instance) {
            if (Mini.triggerMiniLose) {
                //__instance.enabled = false;
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.MiniLose, false);
                return true;
            }
            return false;
        }

        private static bool CheckAndEndGameForJesterWin(ShipStatus __instance) {
            if (Jester.triggerJesterWin) {
                //__instance.enabled = false;
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.JesterWin, false);
                return true;
            }
            return false;
        }

        private static bool CheckAndEndGameForDoomsayerWin(ShipStatus __instance){
            if (Doomsayer.triggerDoomsayerrWin)
            {
                //__instance.enabled = false;
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.DoomsayerWin, false);
                return true;
            }
            return false;
        }

        private static bool CheckAndEndGameForKataomoiWin(ShipStatus __instance)
            {
                if (Kataomoi.triggerKataomoiWin)
                {
                    //__instance.enabled = false;
                    GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.KataomoiWin, false);
                    return true;
                }
                return false;
            }
        private static bool CheckAndEndGameForAkujoWin(ShipStatus __instance, PlayerStatistics statistics)
            {
                if (statistics.TeamAkujoAlive == 2 && statistics.TotalAlive <= 3)
                {
                    //__instance.enabled = false;
                    GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.AkujoWin, false);
                    return true;
                }
                return false;
            }

        private static bool CheckAndEndGameForArsonistWin(ShipStatus __instance) {
            if (Arsonist.triggerArsonistWin) {
                //__instance.enabled = false;
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.ArsonistWin, false);
                return true;
            }
            return false;
        }

        private static bool CheckAndEndGameForVultureWin(ShipStatus __instance) {
            if (Vulture.triggerVultureWin) {
                //__instance.enabled = false;
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.VultureWin, false);
                return true;
            }
            return false;
        }

        private static bool CheckAndEndGameForSabotageWin(ShipStatus __instance) {
            if (MapUtilities.Systems == null) return false;
            var systemType = MapUtilities.Systems.ContainsKey(SystemTypes.LifeSupp) ? MapUtilities.Systems[SystemTypes.LifeSupp] : null;
            if (systemType != null) {
                LifeSuppSystemType lifeSuppSystemType = systemType.TryCast<LifeSuppSystemType>();
                if (lifeSuppSystemType != null && lifeSuppSystemType.Countdown < 0f) {
                    EndGameForSabotage(__instance);
                    lifeSuppSystemType.Countdown = 10000f;
                    return true;
                }
            }
            var systemType2 = MapUtilities.Systems.ContainsKey(SystemTypes.Reactor) ? MapUtilities.Systems[SystemTypes.Reactor] : null;
            if (systemType2 == null) {
                systemType2 = MapUtilities.Systems.ContainsKey(SystemTypes.Laboratory) ? MapUtilities.Systems[SystemTypes.Laboratory] : null;
            }
            if (systemType2 != null) {
                ICriticalSabotage criticalSystem = systemType2.TryCast<ICriticalSabotage>();
                if (criticalSystem != null && criticalSystem.Countdown < 0f) {
                    EndGameForSabotage(__instance);
                    criticalSystem.ClearSabotage();
                    return true;
                }
            }
            return false;
        }

        private static bool CheckAndEndGameForTaskWin(ShipStatus __instance) {
            if (HideNSeek.isHideNSeekGM && !HideNSeek.taskWinPossible || PropHunt.isPropHuntGM) return false;
            if (GameData.Instance.TotalTasks > 0 && GameData.Instance.TotalTasks <= GameData.Instance.CompletedTasks) {
                //__instance.enabled = false;
                GameManager.Instance.RpcEndGame(GameOverReason.HumansByTask, false);
                return true;
            }
            return false;
        }

        private static bool CheckAndEndGameForProsecutorWin(ShipStatus __instance) {
            if (Lawyer.triggerProsecutorWin) {
                //__instance.enabled = false;
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.ProsecutorWin, false);
                return true;
            }
            return false;
        }

        private static bool CheckAndEndGameForLoverWin(ShipStatus __instance, PlayerStatistics statistics) {
            if (statistics.TeamLoversAlive == 2 && statistics.TotalAlive <= 3) {
                //__instance.enabled = false;
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.LoversWin, false);
                return true;
            }
            return false;
        }

        private static bool CheckAndEndGameForJackalWin(ShipStatus __instance, PlayerStatistics statistics) {
            if (statistics.TeamJackalAlive >= statistics.TotalAlive - statistics.TeamJackalAlive && statistics.TeamImpostorsAlive == 0 && !(statistics.TeamJackalHasAliveLover && statistics.TeamLoversAlive == 2) && !Helpers.killingCrewAlive() && statistics.TeamWerewolfAlive == 0 && statistics.TeamJuggernautAlive == 0 && statistics.TeamSwooperAlive == 0 && statistics.TeamAkujoAlive == 0 && statistics.TeamCerenovusAlive == 0 ) {
                //__instance.enabled = false;
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.TeamJackalWin, false);
                return true;
            }
            return false;
        }

        private static bool CheckAndEndGameForSwooperWin(ShipStatus __instance, PlayerStatistics statistics) {
            if (statistics.TeamSwooperAlive >= statistics.TotalAlive - statistics.TeamSwooperAlive && statistics.TeamImpostorsAlive == 0 && statistics.TeamJackalAlive == 0 && statistics.TeamWerewolfAlive == 0 && statistics.TeamJuggernautAlive == 0 && statistics.TeamCerenovusAlive == 0 && !(statistics.TeamSwooperHasAliveLover && statistics.TeamLoversAlive == 2) && !Helpers.killingCrewAlive()) {
                //__instance.enabled = false;
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.SwooperWin, false);
                return true;
            }
            return false;
        }
        
        private static bool CheckAndEndGameForWerewolfWin(ShipStatus __instance, PlayerStatistics statistics) {
            if (
                statistics.TeamWerewolfAlive >= statistics.TotalAlive - statistics.TeamWerewolfAlive && 
                statistics.TeamImpostorsAlive == 0 &&  
                statistics.TeamJackalAlive == 0 && 
                statistics.TeamSwooperAlive == 0 &&
                statistics.TeamJuggernautAlive == 0 &&
                statistics.TeamCerenovusAlive == 0 && 
                !(statistics.TeamWerewolfHasAliveLover && statistics.TeamLoversAlive == 2) &&
                !Helpers.killingCrewAlive()
            ) {
                //__instance.enabled = false;
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.WerewolfWin, false);
                return true;
            }
            return false;
        }

        
        private static bool CheckAndEndGameForJuggernautWin(ShipStatus __instance, PlayerStatistics statistics)
        {
            if (
                statistics.TeamJuggernautAlive >= statistics.TotalAlive - statistics.TeamJuggernautAlive &&
                statistics.TeamImpostorsAlive == 0 &&
                statistics.TeamJackalAlive == 0 &&
                statistics.TeamWerewolfAlive == 0 &&
                statistics.TeamCerenovusAlive == 0 && 
                !(statistics.TeamJuggernautHasAliveLover &&
                statistics.TeamLoversAlive == 2) &&
                !Helpers.killingCrewAlive()
            )
            {
                //__instance.enabled = false;
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.JuggernautWin, false);
                return true;
            }
            return false;
        }

        private static bool CheckAndEndGameForCerenovusWin(ShipStatus __instance, PlayerStatistics statistics)
        {
            if (
                (statistics.TeamCerenovusAlive >= statistics.TotalAlive - statistics.TeamCerenovusAlive && 
                statistics.TeamImpostorsAlive == 0 &&  
                statistics.TeamJackalAlive == 0 && 
                statistics.TeamSwooperAlive == 0 &&
                statistics.TeamJuggernautAlive == 0 &&
                statistics.TeamWerewolfAlive == 0 && 
                !(statistics.TeamCerenovusHasAliveLover && statistics.TeamLoversAlive == 2) &&
                !Helpers.killingCrewAlive()) || Cerenovus.triggerCerenovusWin)
            {
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.CerenovusWin, false);
                return true;
            }
            return false;
        }

        private static bool CheckAndEndGameForImpostorWin(ShipStatus __instance, PlayerStatistics statistics) {
            if (HideNSeek.isHideNSeekGM || PropHunt.isPropHuntGM) 
                if ((0 != statistics.TotalAlive - statistics.TeamImpostorsAlive)) return false;

            if (statistics.TeamImpostorsAlive >= statistics.TotalAlive - statistics.TeamImpostorsAlive && statistics.TeamJackalAlive == 0 && statistics.TeamSwooperAlive == 0 && statistics.TeamJuggernautAlive == 0 && statistics.TeamWerewolfAlive == 0 && statistics.TeamAkujoAlive == 0 && statistics.TeamCerenovusAlive == 0 && !(statistics.TeamImpostorHasAliveLover && statistics.TeamLoversAlive == 2) && !Helpers.killingCrewAlive()) {
                //__instance.enabled = false;
                GameOverReason endReason;
                switch (TempData.LastDeathReason) {
                    case DeathReason.Exile:
                        endReason = GameOverReason.ImpostorByVote;
                        break;
                    case DeathReason.Kill:
                        endReason = GameOverReason.ImpostorByKill;
                        break;
                    default:
                        endReason = GameOverReason.ImpostorByVote;
                        break;
                }
                GameManager.Instance.RpcEndGame(endReason, false);
                return true;
            }
            return false;
        }

        private static bool CheckAndEndGameForCrewmateWin(ShipStatus __instance, PlayerStatistics statistics) {
            if (HideNSeek.isHideNSeekGM && HideNSeek.timer <= 0 && !HideNSeek.isWaitingTimer) {
                //__instance.enabled = false;
                GameManager.Instance.RpcEndGame(GameOverReason.HumansByVote, false);
                return true;
            }
            if (PropHunt.isPropHuntGM && PropHunt.timer <= 0 && PropHunt.timerRunning) {
                GameManager.Instance.RpcEndGame(GameOverReason.HumansByVote, false);
                return true;
            }
            if (statistics.TeamImpostorsAlive == 0 && statistics.TeamJackalAlive == 0 && statistics.TeamSwooperAlive == 0 && statistics.TeamWerewolfAlive == 0 && statistics.TeamJuggernautAlive == 0 && statistics.TeamArsonistAlive == 0 && statistics.TeamAkujoAlive == 0 && statistics.TeamCerenovusAlive == 0) {
                //__instance.enabled = false;
                GameManager.Instance.RpcEndGame(GameOverReason.HumansByVote, false);
                return true;
            }
            return false;
        }

        private static void EndGameForSabotage(ShipStatus __instance) {
            //__instance.enabled = false;
            GameManager.Instance.RpcEndGame(GameOverReason.ImpostorBySabotage, false);
            return;
        }

    }

    internal class PlayerStatistics {
        public int TeamImpostorsAlive {get;set;}
        public int TeamJackalAlive {get;set;}
        public int TeamLoversAlive {get;set;}
        public int TotalAlive {get;set;}
        public bool TeamImpostorHasAliveLover {get;set;}
        public bool TeamJackalHasAliveLover {get;set;}
        
		public int TeamSwooperAlive {get;set;}
		public bool TeamSwooperHasAliveLover {get;set;}

        public int TeamJuggernautAlive { get; set; }
        public bool TeamJuggernautHasAliveLover { get; set; }

		public int TeamWerewolfAlive {get;set;}
		public bool TeamWerewolfHasAliveLover {get;set;}

        public int TeamArsonistAlive {get;set;}
		public bool TeamArsonistHasAliveLover { get;set;}

        public int TeamAkujoAlive { get; set; }
        public int TeamCerenovusAlive { get; set; }
        public bool TeamCerenovusHasAliveLover { get; set; }



        public PlayerStatistics(ShipStatus __instance) {
            GetPlayerCounts();
        }

        private bool isLover(GameData.PlayerInfo p) {
            return (Lovers.lover1 != null && Lovers.lover1.PlayerId == p.PlayerId) || (Lovers.lover2 != null && Lovers.lover2.PlayerId == p.PlayerId);
        }

        private void GetPlayerCounts() {
            int numJackalAlive = 0;
            int numImpostorsAlive = 0;
            int numLoversAlive = 0;
            int numTotalAlive = 0;
            bool impLover = false;
            bool jackalLover = false;
            
			int numSwooperAlive = 0;
			bool swooperLover = false; 

            int numJuggernautAlive = 0;
            bool juggernautLover = false;

			int numWerewolfAlive = 0;
			bool werewolfLover = false;

            int numArsonistAlive = 0;
			bool arsonistLover = false;  

            int numAkujoAlive = 0;

            int numCerenovusAlive = 0;
            bool cerenovusLover = false;


            foreach (var playerInfo in GameData.Instance.AllPlayers.GetFastEnumerator())
            {
                if (!playerInfo.Disconnected)
                {
                    if (!playerInfo.IsDead)
                    {
                        numTotalAlive++;

                        bool lover = isLover(playerInfo);
                        if (lover) numLoversAlive++;

                        if (playerInfo.Role.IsImpostor) {
                            numImpostorsAlive++;
                            if (lover) impLover = true;
                        }
                        if (Jackal.jackal != null && Jackal.jackal.PlayerId == playerInfo.PlayerId) {
                            numJackalAlive++;
                            if (lover) jackalLover = true;
                        }
                        if (Sidekick.sidekick != null && Sidekick.sidekick.PlayerId == playerInfo.PlayerId) {
                            numJackalAlive++;
                            if (lover) jackalLover = true;
                        }

                        if (Swooper.swooper != null && Swooper.swooper.PlayerId == playerInfo.PlayerId) {
                            numSwooperAlive++;
                            if (lover) swooperLover = true;
                        }

                        if (Juggernaut.juggernaut != null && Juggernaut.juggernaut.PlayerId == playerInfo.PlayerId)
                        {
                            numJuggernautAlive++;
                            if (lover) juggernautLover = true;
                        }
                        
                        if (Werewolf.werewolf != null && Werewolf.werewolf.PlayerId == playerInfo.PlayerId) {
                            numWerewolfAlive++;
                            if (lover) werewolfLover = true;
                        }
                        

                        if (Arsonist.arsonist != null && Arsonist.arsonist.PlayerId == playerInfo.PlayerId) {
                            numArsonistAlive++;
                            if (lover) arsonistLover = true;
                        }
                        if (Akujo.akujo != null && Akujo.akujo.PlayerId == playerInfo.PlayerId){
                            numAkujoAlive++;
                        }
                        if (Akujo.honmei != null && Akujo.honmei.PlayerId == playerInfo.PlayerId){
                            numAkujoAlive++;
                        }
                        if (Cerenovus.cerenovus != null && Cerenovus.cerenovus.PlayerId == playerInfo.PlayerId)
                        {
                            numCerenovusAlive++;
                            if (lover) cerenovusLover = true;
                        }
                    }
                }
            }

            TeamJackalAlive = numJackalAlive;
            TeamImpostorsAlive = numImpostorsAlive;
            TeamLoversAlive = numLoversAlive;
            TotalAlive = numTotalAlive;
            TeamImpostorHasAliveLover = impLover;
            TeamJackalHasAliveLover = jackalLover;
            
			TeamSwooperHasAliveLover = swooperLover;
			TeamSwooperAlive = numSwooperAlive;

            TeamJuggernautAlive = numJuggernautAlive;
            TeamJuggernautHasAliveLover = juggernautLover;

			TeamWerewolfHasAliveLover = werewolfLover;
			TeamWerewolfAlive = numWerewolfAlive;

            TeamArsonistHasAliveLover = arsonistLover;
			TeamArsonistAlive = numArsonistAlive;

            TeamAkujoAlive = numAkujoAlive;
            
            TeamCerenovusAlive = numCerenovusAlive;
            TeamCerenovusHasAliveLover = cerenovusLover;

        }
    }
}
