using System.Linq;
using System;
using System.Collections.Generic;
using TheOtherRoles.Players;
using static TheOtherRoles.TheOtherRoles;
using UnityEngine;
using TheOtherRoles.Utilities;
using TheOtherRoles.CustomGameModes;
using System.Threading.Tasks;
using System.Net.Http;

namespace TheOtherRoles
{
    public class RoleInfo {
        public Color color;
        public string name;
        public string introDescription;
        public string shortDescription;
        public RoleId roleId;
        public bool isNeutral;
        public bool isModifier;

        public RoleInfo(string name, Color color, string introDescription, string shortDescription, RoleId roleId, bool isNeutral = false, bool isModifier = false) {
            this.color = color;
            this.name = name;
            this.introDescription = introDescription;
            this.shortDescription = shortDescription;
            this.roleId = roleId;
            this.isNeutral = isNeutral;
            this.isModifier = isModifier;
        }

        public static RoleInfo jester = new RoleInfo("小丑", Jester.color, "让自己的人生变成一场喜剧", "想办法被投出去", RoleId.Jester, true);
        public static RoleInfo mayor = new RoleInfo("市长", Mayor.color, "让飞船再次伟大！", "利用权力帮助船员", RoleId.Mayor);
        public static RoleInfo portalmaker = new RoleInfo("传送门师", Portalmaker.color, "蛋糕是个谎言", "帮船员建造传送门", RoleId.Portalmaker);
        public static RoleInfo engineer = new RoleInfo("工程师",  Engineer.color, "维护飞船上的重要设施", "维护飞船上的重要设施", RoleId.Engineer);
        public static RoleInfo sheriff = new RoleInfo("警长", Sheriff.color, "正义制裁！", "击毙内鬼", RoleId.Sheriff);
        public static RoleInfo deputy = new RoleInfo("辅警", Sheriff.color, "FBI!Open the door!", "铐住他人来帮助警长", RoleId.Deputy);
        public static RoleInfo lighter = new RoleInfo("执灯人", Lighter.color, "你的光芒永不熄灭", "你的光芒永不熄灭", RoleId.Lighter);
        public static RoleInfo godfather = new RoleInfo("教父", Godfather.color, "带领<color=#FF1919FF>黑手党击杀船员", "击杀船员", RoleId.Godfather);
        public static RoleInfo mafioso = new RoleInfo("小弟", Mafioso.color, "帮助<color=#FF1919FF>黑手党</color>击杀船员", "继承教父遗志击杀船员", RoleId.Mafioso);
        public static RoleInfo janitor = new RoleInfo("清洁工", Janitor.color, "帮助<color=#FF1919FF>黑手党</color> 收尸", "为死人收尸", RoleId.Janitor);
        public static RoleInfo morphling = new RoleInfo("化形者", Morphling.color, "夏 日 重 现", "变幻自己的外表", RoleId.Morphling);
        public static RoleInfo camouflager = new RoleInfo("隐蔽者", Camouflager.color, "隐匿于人群之中", "隐匿于人群之中", RoleId.Camouflager);
        public static RoleInfo vampire = new RoleInfo("吸血鬼", Vampire.color, "吸你一下可以吗？", "吸干他人的血", RoleId.Vampire);
        public static RoleInfo eraser = new RoleInfo("抹除者", Eraser.color, "你不准发动技能", "抹除他人的职业", RoleId.Eraser);
        public static RoleInfo trickster = new RoleInfo("骗术师", Trickster.color, "惊不惊喜，意不意外？", "放置盒子搭建管道，并操纵照明网络", RoleId.Trickster);
        public static RoleInfo cleaner = new RoleInfo("清理者", Cleaner.color, "藏好自己，做好清理", "清理尸体", RoleId.Cleaner);
        public static RoleInfo warlock = new RoleInfo("术士", Warlock.color, "那么代价是什么呢？", "诅咒他人并远程击杀船员", RoleId.Warlock);
        public static RoleInfo bountyHunter = new RoleInfo("赏金猎人", BountyHunter.color, "WANTED", "猎杀你的悬赏目标", RoleId.BountyHunter);
        public static RoleInfo detective = new RoleInfo("侦探", Detective.color, "真相只有一个！", "检查脚印&尸体获取信息", RoleId.Detective);
        public static RoleInfo timeMaster = new RoleInfo("时间之主", TimeMaster.color, "EL·PSY·CONGROO！", "用时光之盾保护自己", RoleId.TimeMaster);
        public static RoleInfo medic = new RoleInfo("医生", Medic.color, "用医术保护船员", "用护盾保护船员", RoleId.Medic);
        public static RoleInfo swapper = new RoleInfo("换票师", Swapper.color, "交换他人的得票", "交换他人的得票", RoleId.Swapper);
        public static RoleInfo seer = new RoleInfo("灵媒", Seer.color, "你能感知死亡", "通过闪光与灵魂感知死亡玩家", RoleId.Seer);
        public static RoleInfo hacker = new RoleInfo("黑客", Hacker.color, "黑入飞船来探查他人行踪", "黑入飞船来探查他人行踪", RoleId.Hacker);
        public static RoleInfo tracker = new RoleInfo("追踪者", Tracker.color, "我盯上你了", "追踪目标并寻找尸体", RoleId.Tracker);
        public static RoleInfo snitch = new RoleInfo("告密者", Snitch.color, "完成任务并发现 <color=#FF1919FF>内鬼</color>", "完成任务并发现内鬼", RoleId.Snitch);
        public static RoleInfo jackal = new RoleInfo("豺狼", Jackal.color, "杀死所有其他玩家来获得胜利", "杀死所有其他玩家并招募跟班", RoleId.Jackal, true);
        public static RoleInfo sidekick = new RoleInfo("跟班", Sidekick.color, "帮助豺狼杀死所有人", "帮助豺狼杀死所有人", RoleId.Sidekick, true);
        public static RoleInfo spy = new RoleInfo("卧底", Spy.color, "其实我想做个好人", "隐藏于内鬼之中", RoleId.Spy);
        public static RoleInfo securityGuard = new RoleInfo("保安", SecurityGuard.color, "封锁管道并安装监控", "封锁管道并安装监控", RoleId.SecurityGuard);
        public static RoleInfo arsonist = new RoleInfo("纵火犯", Arsonist.color, "用火焰净化一切", "给所有人涂油来获得胜利", RoleId.Arsonist, true);
        public static RoleInfo goodGuesser = new RoleInfo("侠客", Guesser.color, "生命就是豪赌", "在会议中猜测他人身份并击杀", RoleId.NiceGuesser);
        public static RoleInfo badGuesser = new RoleInfo("刺客", Palette.ImpostorRed, "生命就是豪赌", "在会议中猜测他人身份并击杀", RoleId.EvilGuesser);
        public static RoleInfo vulture = new RoleInfo("秃鹫", Vulture.color, "吞噬尸体获得胜利", "吞噬尸体获得胜利", RoleId.Vulture, true);
        public static RoleInfo medium = new RoleInfo("通灵师", Medium.color, "对死者通灵", "向亡灵询问，并在会议上得到回答", RoleId.Medium);
        public static RoleInfo trapper = new RoleInfo("陷阱师", Trapper.color, "探寻内鬼的行动", "放置陷阱以获取信息", RoleId.Trapper);
        public static RoleInfo lawyer = new RoleInfo("律师", Lawyer.color, "为你的客户辩护", "保护你的客户", RoleId.Lawyer, true);
        public static RoleInfo prosecutor = new RoleInfo("检察官", Lawyer.color, "投出你的目标 ", "投出你的目标", RoleId.Prosecutor, true);
        public static RoleInfo pursuer = new RoleInfo("起诉人", Pursuer.color, "塞空包弹并活到最后", "给其他玩家塞空包弹", RoleId.Pursuer);
        public static RoleInfo impostor = new RoleInfo("内鬼", Palette.ImpostorRed, Helpers.cs(Palette.ImpostorRed, "破坏并击杀"), "破坏并击杀", RoleId.Impostor);
        public static RoleInfo crewmate = new RoleInfo("船员", Color.white, "寻找坏人", "寻找坏人", RoleId.Crewmate);
        public static RoleInfo witch = new RoleInfo("女巫", Witch.color, "对你的敌人下咒", "对你的敌人下咒", RoleId.Witch);
        public static RoleInfo ninja = new RoleInfo("忍者", Ninja.color, "忍法·飞雷神之术！", "出其不意暗杀敌人", RoleId.Ninja);
        public static RoleInfo thief = new RoleInfo("身份窃贼", Thief.color, "拿 来 吧", "杀害能击杀他人的玩家取而代之", RoleId.Thief, true);
        public static RoleInfo bomber = new RoleInfo("爆破手", Bomber.color, "爆炸就是艺术", "在地上安装炸弹", RoleId.Bomber);
        public static RoleInfo yoyo = new RoleInfo("悠悠球", Yoyo.color, "来来回回之间，花式千变万化", "传送到指定位置", RoleId.Yoyo);

        public static RoleInfo hunter = new RoleInfo("猎人", Palette.ImpostorRed, Helpers.cs(Palette.ImpostorRed, "寻找并击杀"), "寻找并击杀", RoleId.Impostor);
        public static RoleInfo hunted = new RoleInfo("猎物", Color.white, "躲避猎人的猎杀", "躲避猎人的猎杀", RoleId.Crewmate);

        public static RoleInfo prop = new RoleInfo("藏匿者", Color.white, "伪装并存活", "伪装成物件", RoleId.Crewmate);



        // Modifier
        public static RoleInfo bloody = new RoleInfo("溅血者", Color.yellow, "用自己的血留下死亡信息", "杀害你的人会留下你的血迹", RoleId.Bloody, false, true);
        public static RoleInfo antiTeleport = new RoleInfo("反传送", Color.yellow, "你只需要参加线上会议", "你不会被随意传送", RoleId.AntiTeleport, false, true);
        public static RoleInfo tiebreaker = new RoleInfo("破平者", Color.yellow, "打破僵局", "你的投票算作1.5票", RoleId.Tiebreaker, false, true);
        public static RoleInfo bait = new RoleInfo("诱饵", Color.yellow, "引诱他人击杀", "杀害你的人会立刻报告", RoleId.Bait, false, true);
        public static RoleInfo sunglasses = new RoleInfo("太阳镜", Color.yellow, "你戴了副不是很酷的太阳镜", "你的视野减弱", RoleId.Sunglasses, false, true);
        public static RoleInfo lover = new RoleInfo("恋人", Lovers.color, $"你坠入了爱河", $"你坠入了爱河", RoleId.Lover, false, true);
        public static RoleInfo mini = new RoleInfo("小孩", Color.yellow, "你受飞船的法律保护", "长大前没有人可以伤害你", RoleId.Mini, false, true);
        public static RoleInfo vip = new RoleInfo("VIP", Color.yellow, "你的死亡会告知给所有人", "你的死亡会告知给所有人", RoleId.Vip, false, true);
        public static RoleInfo invert = new RoleInfo("醉鬼", Color.yellow, "再喝！再喝！", "你的移动方向颠倒", RoleId.Invert, false, true);
        public static RoleInfo chameleon = new RoleInfo("变色龙", Color.yellow, "看不见我看不见我看不见我", "不动的时候会隐身", RoleId.Chameleon, false, true);
        public static RoleInfo shifter = new RoleInfo("交换师", Color.yellow, "与他人交换身份", "与他人交换身份", RoleId.Shifter, false, true);


        public static List<RoleInfo> allRoleInfos = new List<RoleInfo>() {
            impostor,
            godfather,
            mafioso,
            janitor,
            morphling,
            camouflager,
            vampire,
            eraser,
            trickster,
            cleaner,
            warlock,
            bountyHunter,
            witch,
            ninja,
            bomber,
            yoyo,
            goodGuesser,
            badGuesser,
            lover,
            jester,
            arsonist,
            jackal,
            sidekick,
            vulture,
            pursuer,
            lawyer,
            thief,
            prosecutor,
            crewmate,
            mayor,
            portalmaker,
            engineer,
            sheriff,
            deputy,
            lighter,
            detective,
            timeMaster,
            medic,
            swapper,
            seer,
            hacker,
            tracker,
            snitch,
            spy,
            securityGuard,
            bait,
            medium,
            trapper,
            bloody,
            antiTeleport,
            tiebreaker,
            sunglasses,
            mini,
            vip,
            invert,
            chameleon,
            shifter
        };

        public static List<RoleInfo> getRoleInfoForPlayer(PlayerControl p, bool showModifier = true) {
            List<RoleInfo> infos = new List<RoleInfo>();
            if (p == null) return infos;

            // Modifier
            if (showModifier) {
                // after dead modifier
                if (!CustomOptionHolder.modifiersAreHidden.getBool() || PlayerControl.LocalPlayer.Data.IsDead || AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Ended)
                {
                    if (Bait.bait.Any(x => x.PlayerId == p.PlayerId)) infos.Add(bait);
                    if (Bloody.bloody.Any(x => x.PlayerId == p.PlayerId)) infos.Add(bloody);
                    if (Vip.vip.Any(x => x.PlayerId == p.PlayerId)) infos.Add(vip);
                }
                if (p == Lovers.lover1 || p == Lovers.lover2) infos.Add(lover);
                if (p == Tiebreaker.tiebreaker) infos.Add(tiebreaker);
                if (AntiTeleport.antiTeleport.Any(x => x.PlayerId == p.PlayerId)) infos.Add(antiTeleport);
                if (Sunglasses.sunglasses.Any(x => x.PlayerId == p.PlayerId)) infos.Add(sunglasses);
                if (p == Mini.mini) infos.Add(mini);
                if (Invert.invert.Any(x => x.PlayerId == p.PlayerId)) infos.Add(invert);
                if (Chameleon.chameleon.Any(x => x.PlayerId == p.PlayerId)) infos.Add(chameleon);
                if (p == Shifter.shifter) infos.Add(shifter);
            }

            int count = infos.Count;  // Save count after modifiers are added so that the role count can be checked

            // Special roles
            if (p == Jester.jester) infos.Add(jester);
            if (p == Mayor.mayor) infos.Add(mayor);
            if (p == Portalmaker.portalmaker) infos.Add(portalmaker);
            if (p == Engineer.engineer) infos.Add(engineer);
            if (p == Sheriff.sheriff || p == Sheriff.formerSheriff) infos.Add(sheriff);
            if (p == Deputy.deputy) infos.Add(deputy);
            if (p == Lighter.lighter) infos.Add(lighter);
            if (p == Godfather.godfather) infos.Add(godfather);
            if (p == Mafioso.mafioso) infos.Add(mafioso);
            if (p == Janitor.janitor) infos.Add(janitor);
            if (p == Morphling.morphling) infos.Add(morphling);
            if (p == Camouflager.camouflager) infos.Add(camouflager);
            if (p == Vampire.vampire) infos.Add(vampire);
            if (p == Eraser.eraser) infos.Add(eraser);
            if (p == Trickster.trickster) infos.Add(trickster);
            if (p == Cleaner.cleaner) infos.Add(cleaner);
            if (p == Warlock.warlock) infos.Add(warlock);
            if (p == Witch.witch) infos.Add(witch);
            if (p == Ninja.ninja) infos.Add(ninja);
            if (p == Bomber.bomber) infos.Add(bomber);
            if (p == Yoyo.yoyo) infos.Add(yoyo);
            if (p == Detective.detective) infos.Add(detective);
            if (p == TimeMaster.timeMaster) infos.Add(timeMaster);
            if (p == Medic.medic) infos.Add(medic);
            if (p == Swapper.swapper) infos.Add(swapper);
            if (p == Seer.seer) infos.Add(seer);
            if (p == Hacker.hacker) infos.Add(hacker);
            if (p == Tracker.tracker) infos.Add(tracker);
            if (p == Snitch.snitch) infos.Add(snitch);
            if (p == Jackal.jackal || (Jackal.formerJackals != null && Jackal.formerJackals.Any(x => x.PlayerId == p.PlayerId))) infos.Add(jackal);
            if (p == Sidekick.sidekick) infos.Add(sidekick);
            if (p == Spy.spy) infos.Add(spy);
            if (p == SecurityGuard.securityGuard) infos.Add(securityGuard);
            if (p == Arsonist.arsonist) infos.Add(arsonist);
            if (p == Guesser.niceGuesser) infos.Add(goodGuesser);
            if (p == Guesser.evilGuesser) infos.Add(badGuesser);
            if (p == BountyHunter.bountyHunter) infos.Add(bountyHunter);
            if (p == Vulture.vulture) infos.Add(vulture);
            if (p == Medium.medium) infos.Add(medium);
            if (p == Lawyer.lawyer && !Lawyer.isProsecutor) infos.Add(lawyer);
            if (p == Lawyer.lawyer && Lawyer.isProsecutor) infos.Add(prosecutor);
            if (p == Trapper.trapper) infos.Add(trapper);
            if (p == Pursuer.pursuer) infos.Add(pursuer);
            if (p == Thief.thief) infos.Add(thief);

            // Default roles (just impostor, just crewmate, or hunter / hunted for hide n seek, prop hunt prop ...
            if (infos.Count == count) {
                if (p.Data.Role.IsImpostor)
                    infos.Add(TORMapOptions.gameMode == CustomGamemodes.HideNSeek || TORMapOptions.gameMode == CustomGamemodes.PropHunt ? RoleInfo.hunter : RoleInfo.impostor);
                else
                    infos.Add(TORMapOptions.gameMode == CustomGamemodes.HideNSeek ? RoleInfo.hunted : TORMapOptions.gameMode == CustomGamemodes.PropHunt ? RoleInfo.prop : RoleInfo.crewmate);
            }

            return infos;
        }

        public static String GetRolesString(PlayerControl p, bool useColors, bool showModifier = true, bool suppressGhostInfo = false) {
            string roleName;
            roleName = String.Join(" ", getRoleInfoForPlayer(p, showModifier).Select(x => useColors ? Helpers.cs(x.color, x.name) : x.name).ToArray());
            if (Lawyer.target != null && p.PlayerId == Lawyer.target.PlayerId && CachedPlayer.LocalPlayer.PlayerControl != Lawyer.target) 
                roleName += (useColors ? Helpers.cs(Pursuer.color, " § ") : " § ");
            if (HandleGuesser.isGuesserGm && HandleGuesser.isGuesser(p.PlayerId)) roleName += " (赌)";

            if (!suppressGhostInfo && p != null) {
                if (p == Shifter.shifter && (CachedPlayer.LocalPlayer.PlayerControl == Shifter.shifter || Helpers.shouldShowGhostInfo()) && Shifter.futureShift != null)
                    roleName += Helpers.cs(Color.yellow, " ← " + Shifter.futureShift.Data.PlayerName);
                if (p == Vulture.vulture && (CachedPlayer.LocalPlayer.PlayerControl == Vulture.vulture || Helpers.shouldShowGhostInfo()))
                    roleName = roleName + Helpers.cs(Vulture.color, $" (还需食用 {Vulture.vultureNumberToWin - Vulture.eatenBodies} 个)");
                if (Helpers.shouldShowGhostInfo()) {
                    if (Eraser.futureErased.Contains(p))
                        roleName = Helpers.cs(Color.gray, "(被抹) ") + roleName;
                    if (Vampire.vampire != null && !Vampire.vampire.Data.IsDead && Vampire.bitten == p && !p.Data.IsDead)
                        roleName = Helpers.cs(Vampire.color, $"(被咬 {(int)HudManagerStartPatch.vampireKillButton.Timer + 1}) ") + roleName;
                    if (Deputy.handcuffedPlayers.Contains(p.PlayerId))
                        roleName = Helpers.cs(Color.gray, "(被拷) ") + roleName;
                    if (Deputy.handcuffedKnows.ContainsKey(p.PlayerId))  // Active cuff
                        roleName = Helpers.cs(Deputy.color, "(被拷) ") + roleName;
                    if (p == Warlock.curseVictim)
                        roleName = Helpers.cs(Warlock.color, "(术士咒) ") + roleName;
                    if (p == Ninja.ninjaMarked)
                        roleName = Helpers.cs(Ninja.color, "(忍者标) ") + roleName;
                    if (Pursuer.blankedList.Contains(p) && !p.Data.IsDead)
                        roleName = Helpers.cs(Pursuer.color, "(被空包弹) ") + roleName;
                    if (Witch.futureSpelled.Contains(p) && !MeetingHud.Instance) // This is already displayed in meetings!
                        roleName = Helpers.cs(Witch.color, "女巫咒☆ ") + roleName;
                    if (BountyHunter.bounty == p)
                        roleName = Helpers.cs(BountyHunter.color, "(赏金标) ") + roleName;
                    if (Arsonist.dousedPlayers.Contains(p))
                        roleName = Helpers.cs(Arsonist.color, "上油标♨ ") + roleName;
                    if (p == Arsonist.arsonist)
                        roleName = roleName + Helpers.cs(Arsonist.color, $" (未涂油数 {CachedPlayer.AllPlayers.Count(x => { return x.PlayerControl != Arsonist.arsonist && !x.Data.IsDead && !x.Data.Disconnected && !Arsonist.dousedPlayers.Any(y => y.PlayerId == x.PlayerId); })} 人)");
                    if (p == Jackal.fakeSidekick)
                        roleName = Helpers.cs(Sidekick.color, $" (假跟班)") + roleName;

                    // Death Reason on Ghosts
                    if (p.Data.IsDead) {
                        string deathReasonString = "";
                        var deadPlayer = GameHistory.deadPlayers.FirstOrDefault(x => x.player.PlayerId == p.PlayerId);

                        Color killerColor = new();
                        if (deadPlayer != null && deadPlayer.killerIfExisting != null) {
                            killerColor = RoleInfo.getRoleInfoForPlayer(deadPlayer.killerIfExisting, false).FirstOrDefault().color;
                        }

                        if (deadPlayer != null) {
                            switch (deadPlayer.deathReason) {
                                case DeadPlayer.CustomDeathReason.Disconnect:
                                    deathReasonString = " - 离线";
                                    break;
                                case DeadPlayer.CustomDeathReason.Exile:
                                    deathReasonString = " - 被投票";
                                    break;
                                case DeadPlayer.CustomDeathReason.Kill:
                                    deathReasonString = $" - 被 {Helpers.cs(killerColor, deadPlayer.killerIfExisting.Data.PlayerName)} 击杀";
                                    break;
                                case DeadPlayer.CustomDeathReason.Guess:
                                    if (deadPlayer.killerIfExisting.Data.PlayerName == p.Data.PlayerName)
                                        deathReasonString = $" - 猜错了，自杀";
                                    else
                                        deathReasonString = $" - 被 {Helpers.cs(killerColor, deadPlayer.killerIfExisting.Data.PlayerName)} 猜中了";
                                    break;
                                case DeadPlayer.CustomDeathReason.Shift:
                                    deathReasonString = $" - {Helpers.cs(Color.yellow, "被交换")} 导致自杀，由 {Helpers.cs(killerColor, deadPlayer.killerIfExisting.Data.PlayerName)} 造成";
                                    break;
                                case DeadPlayer.CustomDeathReason.WitchExile:
                                    deathReasonString = $" - {Helpers.cs(Witch.color, "被咒杀")} ，女巫 {Helpers.cs(killerColor, deadPlayer.killerIfExisting.Data.PlayerName)} 标";
                                    break;
                                case DeadPlayer.CustomDeathReason.LoverSuicide:
                                    deathReasonString = $" - {Helpers.cs(Lovers.color, "恋人死亡")}";
                                    break;
                                case DeadPlayer.CustomDeathReason.LawyerSuicide:
                                    deathReasonString = $" - {Helpers.cs(Lawyer.color, "不称职的律师")}";
                                    break;
                                case DeadPlayer.CustomDeathReason.Bomb:
                                    deathReasonString = $" - 被 {Helpers.cs(killerColor, deadPlayer.killerIfExisting.Data.PlayerName)} 炸了";
                                    break;
                                case DeadPlayer.CustomDeathReason.Arson:
                                    deathReasonString = $" - 被 by {Helpers.cs(killerColor, deadPlayer.killerIfExisting.Data.PlayerName)} 烧了";
                                    break;
                            }
                            roleName = roleName + deathReasonString;
                        }
                    }
                }
            }
            return roleName;
        }


        static string ReadmePage = "";
        public static async Task loadReadme() {
            if (ReadmePage == "") {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://raw.githubusercontent.com/TheOtherRolesAU/TheOtherRoles/main/README.md");
                response.EnsureSuccessStatusCode();
                string httpres = await response.Content.ReadAsStringAsync();
                ReadmePage = httpres;
            }
        }
        public static string GetRoleDescription(RoleInfo roleInfo) {
            while (ReadmePage == "") {
            }
                
            int index = ReadmePage.IndexOf($"## {roleInfo.name}");
            int endindex = ReadmePage.Substring(index).IndexOf("### 游戏选项");
            return ReadmePage.Substring(index, endindex);

        }
    }
}
