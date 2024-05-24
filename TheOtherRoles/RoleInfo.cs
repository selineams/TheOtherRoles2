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
        public bool isImpostor;
        public string fullDescription;

        public RoleInfo(string name, Color color, string introDescription, string shortDescription, RoleId roleId, bool isNeutral = false, bool isModifier = false, bool isImpostor = false, string fullDescription = "") {
            this.color = color;
            this.name = name;
            this.introDescription = introDescription;
            this.shortDescription = shortDescription;
            this.roleId = roleId;
            this.isNeutral = isNeutral;
            this.isModifier = isModifier;
            this.isImpostor = isImpostor;
            this.fullDescription = fullDescription;
        }

        public static RoleInfo jester = new RoleInfo("小丑", Jester.color, "让自己的人生变成一场喜剧", "想办法被投出去", RoleId.Jester, true, fullDescription: "小丑属于独立阵营，没有任务。\n如果小丑在会议中被投出，小丑单独获胜。");
        public static RoleInfo mayor = new RoleInfo("市长", Mayor.color, "让飞船再次伟大！", "利用权力帮助船员", RoleId.Mayor, fullDescription: "市长投出的票算作两票。\n市长没有紧急会议次数限制，即使到了最大会议次数仍可使用。\n根据模组设置，市长可以拥有便携的紧急会议按钮。\n根据模组设置，市长可以在完成一定任务数量后看到投票的颜色。\n根据模组设置，市长可以在会议上选择投出1票还是2票");
        public static RoleInfo portalmaker = new RoleInfo("传送门师", Portalmaker.color, "蛋糕是个谎言", "帮船员建造传送门", RoleId.Portalmaker, fullDescription: "传送师可以在地图上放置两个传送门，两个传送门相互连接\n传送门在下次会议后可见，且所有人都可使用。\n此外根据模组设置传送师可以在聊天框中获取传送门的使用记录。\n 传送门的使用按钮会在传送师设置了传送门的下一次会议后出现\n当一个玩家使用传送门时，其余玩家将无法使用，直到传送完成\n鬼魂也可使用传送门，但不会与存活的玩家发生冲突，且不会留下传送记录\n如果化形者使用了传送门，将根据模组设置，显示被化形者的名字和颜色\n如果伪装者使用了传送门，记录将显示为 一个伪装者使用了传送门\n如果设置启用，传送者可以从任何地方将自己传送到他们放置的传送门。");
        public static RoleInfo engineer = new RoleInfo("工程师",  Engineer.color, "啊哈，修MAKER", "维护飞船上的重要设施", RoleId.Engineer, fullDescription: "工程师存活时可以在地图的任意位置修复被破坏设施.工程师可以使用通风管道。\n如果工程师在通风管道内，根据模组设置，\n豺狼和伪装者团队的成员将在地图上的所有通风管道周围看到蓝色轮廓（以警告他们）。\n由于通风管道按钮的限制，工程师可能无法通过“使用”按钮打开任务，可通过双击打开任务。");
        public static RoleInfo sheriff = new RoleInfo("警长", Sheriff.color, "正义制裁！", "击毙内鬼", RoleId.Sheriff, fullDescription: "警长可以击杀伪装者与独立阵营.\n如果警长击杀了船员，那么他们会自己死亡");
        public static RoleInfo deputy = new RoleInfo("辅警", Sheriff.color, "FBI!Open the door!", "铐住他人来帮助警长", RoleId.Deputy, fullDescription: "辅警可以给其他玩家带上手铐。\n手铐会被隐藏，直到被铐上的玩家使用技能按钮。\n手铐生效的持续时间从被拷玩家使用按钮时开始计算");
        public static RoleInfo lighter = new RoleInfo("执灯人", Lighter.color, "你的光芒永不熄灭", "你的光芒永不熄灭", RoleId.Lighter, fullDescription: "执灯人的视野与其他人不同，这取决于设置。\n他们的视野看起来像一个可以移动的手电筒圆锥体\n（参考官方捉迷藏模式中的手电筒视野）。");
        public static RoleInfo godfather = new RoleInfo("教父", Godfather.color, "带领<color=#FF1919FF>黑手党击杀船员", "击杀船员", RoleId.Godfather, fullDescription: "黑手党是一个由三个伪装者组成的团体。 \n教父没有特殊能力的普通伪装者。\n小弟在教父死之前不能杀人。 \n清洁工是一个不能杀人的伪装者，但他们可以清理尸体。\n黑手党只能在有三个伪装者的情况下启用");
        public static RoleInfo mafioso = new RoleInfo("小弟", Mafioso.color, "帮助<color=#FF1919FF>黑手党</color>击杀船员", "继承教父遗志击杀船员", RoleId.Mafioso, fullDescription: "黑手党是一个由三个伪装者组成的团体。 \n教父没有特殊能力的普通伪装者。\n小弟在教父死之前不能杀人。 \n清洁工是一个不能杀人的伪装者，但他们可以清理尸体。\n黑手党只能在有三个伪装者的情况下启用");
        public static RoleInfo janitor = new RoleInfo("清洁工", Janitor.color, "帮助<color=#FF1919FF>黑手党</color> 收尸", "为死人收尸", RoleId.Janitor, fullDescription: "黑手党是一个由三个伪装者组成的团体。 \n教父没有特殊能力的普通伪装者。\n小弟在教父死之前不能杀人。 \n清洁工是一个不能杀人的伪装者，但他们可以清理尸体。\n黑手党只能在有三个伪装者的情况下启用");
        public static RoleInfo morphling = new RoleInfo("化形者", Morphling.color, "幻化无穷，生生不息", "变幻自己的外表", RoleId.Morphling, fullDescription: "化形者可以取样一个玩家的外观并在任意时间变成被取样者的外观。\n在伪装者阵营的视角变形后游戏名仍为红色");
        public static RoleInfo camouflager = new RoleInfo("隐蔽者", Camouflager.color, "隐匿于人群之中", "隐匿于人群之中", RoleId.Camouflager, fullDescription: "隐蔽者使用技能后所有玩家的皮肤和装扮都会消失并变成相同颜色。");
        public static RoleInfo vampire = new RoleInfo("吸血鬼", Vampire.color, "吸你一下可以吗？", "吸干他人的血", RoleId.Vampire, fullDescription: "吸血鬼在咬了其他玩家后，被咬玩家将根据游戏设置延迟一定时间后死亡。\n可设置是否开启大蒜功能，当开启大蒜功能时。\n若吸血鬼分配概率大于0%，则游戏中没有分配吸血鬼职业玩家可放置大蒜。\n当玩家在大蒜有效范围内，吸血鬼只能进行普通击杀。\n被吸血玩家会议开始前未到死亡时间的，将会在会议开始时死亡");
        public static RoleInfo eraser = new RoleInfo("抹除者", Eraser.color, "你不准发动技能", "抹除他人的职业", RoleId.Eraser, fullDescription: "被抹除的玩家会在那次会议结束后失去原有职业，变为船员。\n这个技能的冷却会随着抹除者抹除的次数递增。\n即便会议前抹除者的目标已经死亡，他依然会被抹除。\n默认设置下，抹除者无法抹除卧底或其他伪装者的职业。\n根据房间设置的不同，抹除者也可以对这些玩家进行抹除\n（如果抹除到其他伪装者，他们的身份将变为普通伪装者）。\n抹除者在抹除交换师的场合，交换师效果优先发动。\n玩家在当前会议结束前被抹除,而后被投票放逐");
        public static RoleInfo trickster = new RoleInfo("骗术师", Trickster.color, "惊不惊喜，意不意外？", "放置盒子搭建管道，并操纵照明网络", RoleId.Trickster, fullDescription: "骗术师属于伪装者阵营，可以在地图上放置三个骗术箱。骗术箱在那轮会议结束前对其他玩家不可见。\n如果骗术师已经将所有骗术箱放置完毕，\n它们将变成仅供骗术师使用的管道网络，但它们对其他玩家可见。\n如果骗术箱已经形成了管道网络，骗术师将在那轮会议结束后获得新技能“关灯”。\n这个技能将限制非伪装者玩家的视野，并且不可以通过修复照明的方法解除。\n一定时间后，灯光将会恢复。\n特殊关灯下伪装者会在屏幕底部获得提示文本。");
        public static RoleInfo cleaner = new RoleInfo("清理者", Cleaner.color, "藏好自己，做好清理", "清理尸体", RoleId.Cleaner, fullDescription: "清洁工是拥有清理尸体能力的伪装者。");
        public static RoleInfo warlock = new RoleInfo("术士", Warlock.color, "那么代价是什么呢？", "诅咒他人并远程击杀船员", RoleId.Warlock, fullDescription: "术士可以诅咒其他玩家。被诅咒的玩家不会发现自己被诅咒。 \n如果被诅咒的玩家站在另一名玩家身边，术士可以击杀后者，无论他距离前两者多远。 \n那次击杀如果成功，被诅咒的玩家的诅咒会被解除，同时术士将在一定时间内无法行动。 \n术士仍然可以使用普通击杀，但两种击杀方式共同计算冷却。\n术士的技能击杀可以击杀任何人，包括其他伪装者与自己");
        public static RoleInfo bountyHunter = new RoleInfo("赏金猎人", BountyHunter.color, "W A N T E D", "猎杀你的悬赏目标", RoleId.BountyHunter, fullDescription: "赏金猎人将持续获得一个悬赏目标。这名玩家不知道自己是悬赏目标。 \n每次会议结束后或一定时间后，悬赏目标将会变更。 \n如果赏金猎人成功击杀悬赏目标，他的下一次击杀冷却将大幅减少。\n如果击杀了其他玩家，下一次击杀冷却将增加。 \n根据房间设置的不同，赏金猎人可以看到一个指向悬赏目标的箭头。");
        public static RoleInfo detective = new RoleInfo("侦探", Detective.color, "真相只有一个！", "检查脚印&尸体获取信息", RoleId.Detective, fullDescription: "侦探可以看到其他玩家留下的脚印。\n当侦探报告尸体时，能获得与凶手相关的线索，这取决于找到被击杀的尸体的时间");
        public static RoleInfo timeMaster = new RoleInfo("时间之主", TimeMaster.color, "EL·PSY·KONGROO！", "用时光之盾保护自己", RoleId.TimeMaster, fullDescription: "时间之主可以使用时光之盾，根据设置，时光之盾激活后在一定时间内有效.\n如果有玩家试图在时光之盾激活的有效时间内击杀时间之主，\n那么击杀不会发生，所有玩家将倒退一定时间。\n凶手的击杀冷却时间不会重置，因此时间之主需确保自己不会再次被击杀.\n时间之主不会受到时光倒退的影响。");
        public static RoleInfo medic = new RoleInfo("医生", Medic.color, "用医术保护船员", "用护盾保护船员", RoleId.Medic, fullDescription: "医生可以在每场游戏中保护一个玩家（被保护玩家周围会突出显示轮廓），使得被保护玩家无法被击杀。\n被保护的玩家仍然可以被投票放逐，也可能是伪装者。\n根据模组设置，如果有人（伪装者，警长等）试图击杀他们，\n被保护的玩家和或医生将在屏幕上出现红色闪光。\n如果医生死了，则护盾也会随他们一起消失。\n如果警长试图杀死一个被保护的船员，则他们都不会死，\n如果他们试图杀死一个被保护的伪装者，也不会执行击杀。\n根据模组设置情况，来自赌怪的猜测也将被护盾阻止，并且会通知被保护的玩家和医生。\n当医生报告尸体时：他们可以看到玩家的死亡时间。");
        public static RoleInfo swapper = new RoleInfo("换票师", Swapper.color, "交换他人的得票", "交换他人的得票", RoleId.Swapper, fullDescription: "换票师可以在会议期间交换两个玩家的选票。\n由于交换师在会议中的优势，\n根据模组设置，他们可能无法使用紧急会议按钮，也无法修复灯光和通信。\n交换师可设置初始的充能数，并且在完成相应的任务数量后进行充能。");
        public static RoleInfo seer = new RoleInfo("灵媒", Seer.color, "你能感知死亡", "通过闪光与灵魂感知死亡玩家", RoleId.Seer, fullDescription: "灵媒有两种能力（可以在模组设置中激活其中一种或全部激活） \n灵媒可以看到一轮前死亡的玩家的灵魂，灵魂会慢慢消失。 \n如果玩家在地图上的某个地方死亡，则灵媒会在屏幕上闪烁蓝光。");
        public static RoleInfo hacker = new RoleInfo("黑客", Hacker.color, "黑入飞船来探查他人行踪", "黑入飞船来探查他人行踪", RoleId.Hacker, fullDescription: "黑客激活黑人技能后可以从管理员地图和心电图中获取更多信息。 \n其他时候他与其他玩家所见信息相同 \n管理员地图：黑客可以看到地图上玩家的颜色（或颜色类型） \n心电图： 黑客可以看到死亡的玩家及死亡时间 黑客可以远程使用管理员地图和心电图工具，\n可以设置最大电量以及完成任务以获取充电 \n当使用远程工具时，黑客可设置是否可以移动");
        public static RoleInfo tracker = new RoleInfo("追踪者", Tracker.color, "我盯上你了", "追踪目标并寻找尸体", RoleId.Tracker, fullDescription: "追踪者可以选择一个玩家进行追踪。\n根据模组设置，追踪者可以在每次会议后追踪重新选择追踪目标\n或追踪者在整场游戏中追踪同一个人。\n箭头指向玩家最后被追踪的位置。箭头根据设置每隔一段时间更新一次位置。\n根据模组设置，追踪者可以在设定的时间内追踪地图上的所有尸体，\n即使它们被秃鹫清理或吃掉了。");
        public static RoleInfo snitch = new RoleInfo("告密者", Snitch.color, "完成任务并发现 <color=#FF1919FF>内鬼</color>", "完成任务并发现内鬼", RoleId.Snitch, fullDescription: "当告密者完成他们所有的任务时，\n他们会在聊天中得到所有杀手的最后位置的信息，在会议开始阶段。\n当告密者只剩下可配置的任务量时，会在邪恶玩家的屏幕上显示出游戏中存在一个告密者的文字。\n告密者可设置是否在会议上可见告密职业。");
        public static RoleInfo jackal = new RoleInfo("爪牙", Jackal.color, "杀死所有其他玩家来获得胜利", "杀死所有其他玩家并招募随从", RoleId.Jackal, true, fullDescription: "豺狼属于独立阵营，目标是干掉所有其他玩家。 \n豺狼没有任务，可以杀死内鬼、船员和其他独立阵营角色。 \n根据模组设置，豺狼可以选择一名玩家，将其招募为跟班。 \n一名玩家被招募为跟班时，他所有的任务进度都将移除，\n阵营变为豺狼团队。跟班失去原有职业\n（当招募目标为恋人时，恋人阵营维持，跟班同时隶属豺狼阵营与恋人阵营）。\n 根据模组设置，每个豺狼只能使用一次招募技能，\n或每局游戏豺狼只能进行一次招募。 \n豺狼不可以招募带刀坏人为跟班，招募他们时招募无效，\n被招募的带刀坏人身份无变化，但在豺狼视角其身份将变为跟班。");
        public static RoleInfo sidekick = new RoleInfo("随从", Sidekick.color, "帮助爪牙杀死所有人", "帮助爪牙杀死所有人", RoleId.Sidekick, true, fullDescription: "在豺狼对一名玩家使用招募技能时出现，被招募的玩家变成跟班，\n加入豺狼阵营，胜利条件变为杀死所有其他玩家。\n根据模组设置，豺狼死亡时，跟班可能晋升为豺狼，\n甚至可能进一步获得招募新的跟班的权利。\n被招募为跟班后将失去原有的职业和任务。\n迷你船员在长大前无法被招募或击杀");
        public static RoleInfo spy = new RoleInfo("卧底", Spy.color, "其实我想做个好人", "隐藏于内鬼之中", RoleId.Spy, fullDescription: "卧底是一个没有特殊技能的船员职业。在伪装者视角，卧底看起来就像一个伪装者，无法分辨。\n根据模组设置，可以有以下两种情况:\n伪装者无法击杀卧底 （但他们的击杀按钮会显示卧底身份）\n伪装者可以击杀卧底（但同时他们也可以击杀其他伪装者）\n根据模组设置，可以决定警长是否可击杀卧底");
        public static RoleInfo securityGuard = new RoleInfo("保安", SecurityGuard.color, "封锁管道并安装监控", "封锁管道并安装监控", RoleId.SecurityGuard, fullDescription: "保安有用一定数量的螺丝钉，可以用来封锁通风管道或安放摄像头。\n放置摄像头和封锁通风管道所需的螺丝钉可以在模组设置中更改。\n保安拥有的螺丝钉数量可设置。\n新安装的摄像头将在下次会议后显示，且所有玩家均可使用\n被封锁的通风管道将在下次会议后无法使用，玩家仍可移动到该通风管道，但无法进入和离开");
        public static RoleInfo arsonist = new RoleInfo("纵火犯", Arsonist.color, "用火焰净化一切", "给所有人涂油来获得胜利", RoleId.Arsonist, true, fullDescription: "纵火犯属于独立阵营，没有任务。\n纵火犯独自胜利。 \n通过在一名玩家身边使用“涂油”技能并留在那名玩家身边一段时间，\n纵火犯可以给其他玩家涂油。 \n如果被涂油玩家离开了纵火犯的技能范围，技能冷却会回到0。 \n当存活的所有其他玩家都被涂油后，纵火犯便可以点火烧死所有玩家，\n纵火犯独自胜利。");
        public static RoleInfo goodGuesser = new RoleInfo("侠客", Guesser.color, "代表光明,进行一场伟大的交易", "在会议中猜测他人身份并击杀", RoleId.NiceGuesser, fullDescription: "据模组设置，赌怪可以属于船员阵营或者伪装者阵营。 \n赌怪可以通过在会议中猜测一名玩家的身份来狙杀那名玩家。 如果赌怪猜测错误，赌怪自己死亡。 \n你可以根据设置决定赌怪可以在每次会议使用技能击杀玩家数量\n以及每局游戏赌怪可以使用技能击杀的最大玩家数。 \n赌怪如果猜测一名玩家身份为“伪装者”或“船员”，\n则仅当被猜测玩家无特殊职业时，猜测才算正确。 \n赌怪只能在会议期间发动技能。\n根据模组设置，赌怪无法猜测被保护的玩家，并且根据医生的模组设置情况，\n被保护目标及医生将会收到通知（无论赌怪猜测什么都不会有人死亡）");
        public static RoleInfo badGuesser = new RoleInfo("刺客", Palette.ImpostorRed, "代表黑暗,组织一场秘密的谋杀", "在会议中猜测他人身份并击杀", RoleId.EvilGuesser, fullDescription: "据模组设置，赌怪可以属于船员阵营或者伪装者阵营。 \n赌怪可以通过在会议中猜测一名玩家的身份来狙杀那名玩家。 如果赌怪猜测错误，赌怪自己死亡。 \n你可以根据设置决定赌怪可以在每次会议使用技能击杀玩家数量\n以及每局游戏赌怪可以使用技能击杀的最大玩家数。 \n赌怪如果猜测一名玩家身份为“伪装者”或“船员”，\n则仅当被猜测玩家无特殊职业时，猜测才算正确。 \n赌怪只能在会议期间发动技能。\n根据模组设置，赌怪无法猜测被保护的玩家，并且根据医生的模组设置情况，\n被保护目标及医生将会收到通知（无论赌怪猜测什么都不会有人死亡）");
        public static RoleInfo vulture = new RoleInfo("秃鹫", Vulture.color, "饿饿, 饭饭!", "吞噬尸体获得胜利", RoleId.Vulture, true, fullDescription: "秃鹫没有任何任务，需要独自获胜。秃鹫属于独立阵营，\n根据模组设置，秃鹫需要吃掉一定量的玩家尸体来获得胜利。\n根据模组设置，一名玩家死亡时，秃鹫可能可以看到一个指向那名玩家尸体的箭头。");
        public static RoleInfo medium = new RoleInfo("通灵师", Medium.color, "对死者通灵", "向亡灵询问，并在会议上得到回答", RoleId.Medium, fullDescription: "通灵师是一个船员职业，可以向死去的玩家的灵魂询问信息。\n与灵媒一样，它可以看到玩家死亡的地方（在下一次会议之后），并可以询问他们。\n然后，它会在讯问中获取有关灵魂或凶手的信息。\n灵魂只停留一轮，即直到下一次会议。根据模组设置，灵魂可能只能被询问一次，然后消失。\n1.特定角色的具体信息：警长自杀,盗贼自杀，主动的恋人死亡，被动的恋人自杀\n律师的客户杀了律师，杀队友之蓝方（豺狼与跟班），杀队友之红方，\n术士自杀，秃鹰/清洁工吃了/清理了尸体。\n2.其他的随机：“我不确定，但我猜是一种更深/更浅的颜色杀死了我。”\n“如果我没算错的话，那么我在会议开始前X秒就死了。”\n“如果我的角色还没有被保存，那么游戏中就没有（角色）了。”\n“好像杀我的人是（角色）。”\n3.答案中包含其他信息的可能性：。当你问起时，X个杀手仍然活着。\n当你问起时，有X个能使用通风口的玩家仍然活着。\n当你问起时，有X个中立但不能杀人的玩家仍然活着。");
        public static RoleInfo trapper = new RoleInfo("诱捕者", Trapper.color, "探寻内鬼的行动", "放置陷阱以获取信息", RoleId.Trapper, fullDescription: "陷阱师是一名船员，他可以设置陷阱来诱捕玩家，并从他们那里获得信息。\n陷阱会将玩家困住X秒（取决于设置），并在聊天中显示他们的 “角色 “信息，\n如果他们是一个 “好/坏角色 “或他们的 “名字”。\n陷阱是不可见的，直到满足一定数量的玩家被困住。\n当陷阱可见时，陷阱师将在聊天中获得信息（以随机顺序）。\n如果一个陷阱被触发（并且该选项被激活），陷阱师的地图将被打开，并显示哪个陷阱被触发。\n陷阱有一个最大的充电量（使用），并且需要一个可配置的任务量来充能。");
        public static RoleInfo lawyer = new RoleInfo("律师", Lawyer.color, "好了, 放心说话吧, 我来了", "保护你的客户", RoleId.Lawyer, true, fullDescription: "律师属于独立阵营，拥有一名客户。\n客户只可能是一个没有恋人的伪装者或豺狼或其他带刀坏人。\n根据模组设置，客户也可以是小丑律师。\n需帮助客户获胜才能赢得胜利。\n客户不知道律师的身份。如果客户被放逐，则律师会与客户以其死亡。\n如果客户被杀死，律师将变为起诉人，胜利条件也将改变。\n律师获胜条件:当客户存活时，律师无论存活或死亡均可与客户一同获胜。\n当客户为小丑时，小丑被放逐，律师将与小丑一同获胜。\n如果客户玩家断开连接，则律师将变成起诉人根据模组设置的情况，\n律师可确定客户身份这些任务只有在律师晋升为追捕者时才算数。\n如果律师在其客户之前死亡，他们将失去所有的任务。");
        public static RoleInfo prosecutor = new RoleInfo("检察官", Lawyer.color, "别狡辩了, 罪人 ", "投出你的目标", RoleId.Prosecutor, true, fullDescription: "检察官是一个类似于律师的中立角色。\n检察官有一个目标，他是一名船员。\n检察官需要投出他们的客户，以赢得比赛。检察官的客户不知道自己是他们的客户。\n如果目标被招募成跟班，检察官就会改变自己的角色，成为目标的律师，并且从现在开始必须保护目标。\n如果检察官的目标死了，检察官就会改变他们的角色，成为起诉人。\n检察官的角色设置与律师的设置是共享的。\n如果客户断开连接，检察官也会变成起诉人。\n根据模组设置的情况，检察官可确定客户身份。\n任务只有在检察官晋升为追捕者时才算数。如果检察官在其客户之前死亡，他们将失去所有的任务。");
        public static RoleInfo pursuer = new RoleInfo("起诉人", Pursuer.color, "塞空包弹并活到最后", "给其他玩家塞空包弹", RoleId.Pursuer, true, fullDescription: "起诉人依然属于独立阵营，但胜利条件发生了改变：\n起诉人必须在游戏结束时保证自己存活（无论哪个阵营获胜）。\n为了获得胜利，起诉人拥有技能“空包弹”：\n他们可以在拥有击杀能力的玩家（包括警长）的武器内塞上空包弹，\n当那名玩家如果尝试击杀其他玩家时，该次击杀失效，击杀冷却仍然正常计算。");
        public static RoleInfo impostor = new RoleInfo("内鬼", Palette.ImpostorRed, Helpers.cs(Palette.ImpostorRed, "破坏并击杀"), "破坏并击杀", RoleId.Impostor, fullDescription: "白板");
        public static RoleInfo crewmate = new RoleInfo("船员", Color.white, "寻找坏人", "寻找坏人", RoleId.Crewmate, fullDescription: "白板");
        public static RoleInfo witch = new RoleInfo("女巫", Witch.color, "对你的敌人下咒", "对你的敌人下咒", RoleId.Witch, fullDescription: "女巫是一个伪装者，她的能力是对其他玩家施放咒语。\n在下一次会议中，被下咒的玩家将被标记，会议结束后立即死亡。\n女巫若给拥有护盾的玩家下咒或下咒前被起诉人塞空包弹，那次下咒无效。\n当女巫下咒后起诉人对女巫使用了空包弹或者被下咒人获得医生的护盾，\n那么将会出现会议结束后被下咒玩家仍然存活的情况；\n如果女巫在会议开始前或者在会议期间死亡或放逐，\n被下咒的玩家在会议期间将被标记，但会议结束后会存活下来\n（根据模组设置情况，可以选择女巫死亡后是否能拯救被下咒玩家）\n被下咒的玩家死亡结算优先于被驱逐玩家。");
        public static RoleInfo ninja = new RoleInfo("忍者", Ninja.color, "忍法·飞雷神之术！", "看我, 飞雷神斩!", RoleId.Ninja, fullDescription: "忍者可以杀死地图上的任意的玩家，他可以标记一个玩家，\n并在任意时间地点传送到被标记玩家的身边击杀他。\n根据设置情况，忍者可以看到被标记玩家位置。\n如果忍者使用能力，根据模组设置情况，将会在能力的地方一定时间内留下痕迹（叶子）。");
        public static RoleInfo thief = new RoleInfo("身份窃贼", Thief.color, "拿 来 吧", "杀害能击杀他人的玩家取而代之", RoleId.Thief, true, fullDescription: "盗贼必须杀死另一个带刀者（或者可以设置猜测带刀者的职业），才能获得胜利条件。\n如果 “盗贼 ”没有杀死带刀者，他们将输掉游戏。\n如果盗贼杀死了带刀者，盗贼就会变成该角色并加入该团队。然后盗贼拥有新的胜利条件。\n如果盗贼试图杀死任何非带刀者（船员或中立者），他们的死法类似于误杀警长。\n如果盗贼杀死了女巫，那么已经被施了巫术的玩家仍然被施了巫术(盗贼除外)。\n如果盗贼可以猜测盗取角色，猜测女巫会保下所有的目标或没有目标\n(取决于女巫的设置: 投票出女巫是否保下所有的目标)\n如果 “盗贼可以杀死警长 ”的选项是开启的，那么盗贼的任务只有在他们杀死警长后才会开始计算。\n当 “盗贼 ”还没有开火时，他们的任务不会被计入任务赢利。\n如果 “盗贼可以杀死警长 ”的选项是关闭的，那么盗贼就不会有任务。");
        public static RoleInfo bomber = new RoleInfo("爆破手", Bomber.color, "爆炸就是艺术", "在地上安装炸弹", RoleId.Bomber, fullDescription: "爆破手是一个具有很强爆发力的伪装者。他们有能力放置炸弹来分散抱团的船员，也能杀死他们。\n爆破手也有能力像所有伪装者一样进行正常的杀戮。\n布置炸弹的cd可以与一般刀的CD不同（依据设置）\n船员们可以来拆除炸弹。（根据设置）\n炸弹不会杀死有防护罩的玩家，可以杀死爆破手和他们的队友。\n预警显示范围可以高于/低于实际破坏范围，这取决于设置，\n并以一个红圈视觉指示器的形式显示\n视觉指示器会慢慢变成红色，直到炸弹爆炸，\n它不会显示实际爆炸范围（只有预警显示范围）!\n炸弹可以通过站在上面剪断导火线（按钮）来拆除。\n注意区分预警显示范围与实际爆破范围的区别 ");
        public static RoleInfo yoyo = new RoleInfo("悠悠球", Yoyo.color, "来来回回之间，花式千变万化", "传送到指定位置", RoleId.Yoyo, fullDescription:"溜溜球是一个伪装者，它能够标记一个位置，然后闪烁（传送）到这个位置。\n在第一次闪烁之后，溜溜球有固定的时间（可设置）做任何他们想做的事情，\n然后自动闪烁回第一次闪烁的起点。\n根据设置，溜溜球还可以访问移动管理表。\n第一跳的标记位置只显示给悠悠球和幽灵。\n 第一次闪烁的剪影（几乎）保持到悠悠球返回为止\n第二次闪烁（返回）的剪影保持 5 秒钟");

        public static RoleInfo hunter = new RoleInfo("猎人", Palette.ImpostorRed, Helpers.cs(Palette.ImpostorRed, "寻找并击杀"), "寻找并击杀", RoleId.Impostor, fullDescription: "jesterFullDesc");
        public static RoleInfo hunted = new RoleInfo("猎物", Color.white, "躲避猎人的猎杀", "躲避猎人的猎杀", RoleId.Crewmate, fullDescription: "jesterFullDesc");

        public static RoleInfo prop = new RoleInfo("藏匿者", Color.white, "伪装并存活", "伪装成物件", RoleId.Crewmate, fullDescription: "jesterFullDesc");

        public static RoleInfo bodyguard  = new RoleInfo("保镖", BodyGuard.color, "用你的生命保护你选择的目标", "用你的生命保护你选择的目标并顺便反杀敌人!", RoleId.BodyGuard, false, fullDescription: "保镖可以选择保护一个人，\n若保护的人被刀，则凶手和保镖一同死亡（双死）");
        public static RoleInfo amnisiac = new RoleInfo("失忆者", Amnisiac.color, "回忆起你的身份", "窃取死亡玩家的职业", RoleId.Amnisiac, true, fullDescription: "失忆者需要通过对死亡玩家的尸体发动技能“回忆”，\n获得那名玩家的职业来获取与那名玩家相同的胜利条件。");
        public static RoleInfo swooper = new RoleInfo("隐身人", Swooper.color, "我在你身后", "隐身并击杀船员", RoleId.Swooper, true, fullDescription: "你可以隐身");
        public static RoleInfo veteren = new RoleInfo("老兵", Veteren.color, "老骥伏枥，时刻警惕", "时刻警惕以反杀对手", RoleId.Veteren, fullDescription: "每局游戏若干次，行动阶段，老兵可以进入警戒状态。\n若任何其他玩家尝试对警戒状态下的老兵发动技能，老兵击杀那些玩家。");
        public static RoleInfo werewolf = new RoleInfo("月下狼人", Werewolf.color, "为胜利变成狼人模样", "暴走并击杀所有人", RoleId.Werewolf, true, fullDescription: "月下狼人拥有击杀冷却很短的击杀能力，但通常情况下不能使用。\n行动阶段，月下狼人可以发动技能“暴走”。\n技能持续时间内，月下狼人拥有内鬼视野，且激活击杀能力。");
        public static RoleInfo poucher = new RoleInfo("赶尸人", Poucher.color, "查看将死之人的职业", "查看被击杀玩家的职业", RoleId.Poucher, fullDescription: "你可以知晓你击杀过的玩家的职业");
        public static RoleInfo mimic = new RoleInfo("食人族", Mimic.color, "夺走你的生命与职业", "夺走第一个击杀的船员玩家的职业", RoleId.Mimic, fullDescription: "你可以获取你第一个击杀的船员的职业，\n不能获取告密者，执灯人，卧底。");
        public static RoleInfo escapist = new RoleInfo("逃逸者", Escapist.color, "溜了溜了", "传送并逃离现场", RoleId.Escapist, fullDescription: "每轮行动阶段限一次，逃逸者可以使用技能“标记”。\n那之后，直到这轮行动阶段结束，逃逸者的技能变为“瞬移”，\n发动后，逃逸者传送到使用“标记”时的位置。");
        public static RoleInfo pitfaller = new RoleInfo("制咒师", Pitfaller.color, "制作咒杀陷阱", "用尸体制作咒杀陷阱", RoleId.Pitfaller, fullDescription: "你可以对尸体下咒，若其他人报告了这具尸体，则立刻死亡。");
        public static RoleInfo shinobi = new RoleInfo("潜刃手", Shinobi.color, "悄悄地来", "潜行使击杀没有动画效果", RoleId.Shinobi, fullDescription: "你的使用潜刃，使用时击杀无动画效果。");
        public static RoleInfo transporter = new RoleInfo("位移师", Transporter.color, "移形换位", "与他人交换位置", RoleId.Transporter, fullDescription: "你可以交换自己与选择目标的位置");
        public static RoleInfo transportist = new RoleInfo("闪跃者", Transportist.color, "给我拉过来", "将其他人拉倒自己跟前", RoleId.Transportist, fullDescription: "你可以将其他人拉倒自己跟前");      
        public static RoleInfo snarer = new RoleInfo("设陷师", Snarer.color, "放置陷阱击杀他人", "放置陷阱击杀他人？", RoleId.Snarer, fullDescription: "你可以放置陷阱来击杀玩家，通过陷阱击杀后有奖励。");
        public static RoleInfo miner = new RoleInfo("管道工", Miner.color, "你看我像不像马里奥？", "挖掘额外的管道", RoleId.Miner, fullDescription: "行动阶段，管道工可以发动技能“挖洞”，在技能发动位置生成一个管道口。\n管道工生成的管道口互相连接为独立的管道网络。");
        public static RoleInfo arbiter = new RoleInfo("裁决者", Arbiter.color, "做出你的判断", "强制放逐玩家", RoleId.Arbiter, fullDescription: "每局游戏限X次，裁决者可以在会议阶段选择一名玩家使用技能。\n技能使用后，本轮视为对那名玩家投出所有的票，\n且其他玩家的所有投票全部无效。");
        public static RoleInfo juggernaut = new RoleInfo("天启", Juggernaut.color, "不断修炼，不断前行。", "不断出刀减少CD，杀光所有人", RoleId.Juggernaut, true, fullDescription: "天启可以击杀。每一次天启进行击杀，\n天启的击杀冷却永久减少。");
        public static RoleInfo doomsayer = new RoleInfo("末日使徒", Doomsayer.color, "猜猜看呐", "猜测身份", RoleId.Doomsayer, true, fullDescription: "末日预言者拥有每轮会议不限次数的刺杀（赌杀）能力。\n若末日预言者猜测错误，末日预言者不会死亡，但本轮会议不可以再猜测。\n末日预言者累计猜测成功到达一定次数，则末日预言者单独获胜。\n此外，行动阶段，末日预言者可以对其他玩家发动技能“洞察”。\n技能发动的下一个会议阶段，\n末日预言者将获得一个包含数个职业的职业类别，那名玩家的职业属于这个类别");
        public static RoleInfo kataomoi = new RoleInfo("痴恋", Kataomoi.color, "为~爱~痴~狂~", "与你的单相思融为一体", RoleId.Kataomoi, true, fullDescription: "游戏开始阶段你会指定一个目标，\n痴恋需要对目标求爱一定次数，并对其告白后，单独获得胜利.\n痴恋有隐身和寻找目标的能力");
        public static RoleInfo akujo = new RoleInfo("魅魔", Akujo.color, "你来的正是时候！", "与真爱一起活下去！", RoleId.Akujo, true, fullDescription: "魅魔需要在一定时间内选择真爱与备胎。\n选择后与真爱胜利，死亡方式同恋人。");
        public static RoleInfo teleporter = new RoleInfo("传送师", Teleporter.color, "操纵传送", "交换他人位置", RoleId.Teleporter, fullDescription: "你可以交换选择的2个目标的位置");
        public static RoleInfo cerenovus = new RoleInfo("洗脑师", Cerenovus.color, "借刀杀人", "操纵他人击杀他人以获得胜利", RoleId.Cerenovus, true, fullDescription: "你可以操纵一名玩家，操纵该玩家击杀其周围的人。\n操纵击杀成功3次后单独获胜。");


        // Modifier
        public static RoleInfo bloody = new RoleInfo("溅血者", Color.yellow, "用自己的血留下死亡信息", "杀害你的人会留下你的血迹", RoleId.Bloody, false, true, fullDescription: "如果溅血者被杀，会在凶手身上留下数秒的血印，血印有着被害人的颜色，\n伪装者阵营、独立阵营及船员阵营均可获得该附加职业。");
        public static RoleInfo antiTeleport = new RoleInfo("通讯兵", Color.yellow, "你只需要参加线上会议", "你不会被随意传送", RoleId.AntiTeleport, false, true, fullDescription: "如果有尸体被报告或者召开紧急会议时，通讯兵不会被传送至会议室，\n会议结束后玩家还会在会议召开前的位置。\n伪装者阵营、独立阵营及船员阵营均可获得该附加职业。");
        public static RoleInfo tiebreaker = new RoleInfo("破平者", Color.yellow, "打破僵局", "你的投票算作1.5票", RoleId.Tiebreaker, false, true, fullDescription: "若投票以平局结束，破平者将额外获得一张选票来打破平局，\n此票对所有人都不可见，但所有人都会知道破平者参与了会议。\n伪装者阵营、独立阵营及船员阵营均可获得该附加职业。");
        public static RoleInfo bait = new RoleInfo("诱饵", Color.yellow, "引诱他人击杀", "杀害你的人会立刻报告", RoleId.Bait, false, true, fullDescription: "杀死诱饵的凶手会自动报告（玩家可以在设置中修改报告延迟）\n伪装者阵营、独立阵营及船员阵营均可获得该附加职业。");
        public static RoleInfo sunglasses = new RoleInfo("太阳镜", Color.yellow, "你戴了副不是很酷的太阳镜", "你的视野减弱", RoleId.Sunglasses, false, true, fullDescription: "船员携带了太阳镜，使得船员的视野受到一定比例的干扰，\n干扰的比例可在模组设置中修改、熄灯时，视野也会受到影响");
        public static RoleInfo lover = new RoleInfo("恋人", Lovers.color, $"你坠入了爱河", $"你坠入了爱河", RoleId.Lover, false, true, fullDescription: "恋人在游戏中可使用私人聊天频道，恋人无法知道伴侣的职业，他们只知道伴侣是谁。\n如果游戏结束时恋人存活，那么他们将会获胜，他们也可以与原来的团队一起获胜.\n如果恋人的其中一方拥有击杀能力，那么他们仅可以获得恋人的胜利.");
        public static RoleInfo mini = new RoleInfo("小孩", Color.yellow, "你受飞船的法律保护", "长大前没有人可以伤害你", RoleId.Mini, false, true, fullDescription: "由于迷你船员比较小，因此游戏中所有人都将知道迷你船员是谁。\n迷你船员在18岁以前不可被击杀，但是可以通过投票被放逐。\n迷你船员成长过程中，击杀冷却时间将增加一倍，\n成年后击杀冷却时间为普通冷却时间的三分之二。 如果被放逐，将无事发生。\n好迷你船员在游戏前期是无敌的存在 如果好迷你船员在成年前被放逐，则全员失败\n 如果迷你小丑被投出，游戏将以小丑胜利告终");
        public static RoleInfo vip = new RoleInfo("VIP", Color.yellow, "尊敬的会...很会的人员", "你的死亡会告知给所有人", RoleId.Vip, false, true, fullDescription: "伪装者、豺狼或船员都可能获得VIP。\nVIP玩家在被击杀时，所有人都将收到类似于灵媒的闪光\n根据设置，如果现实团队颜色选项为激活状态，每个人可收到专属于不同阵营的闪光\n阵营:伪装者阵营 = 红色，独立阵营 = 蓝色，船员阵营 = 白色");
        public static RoleInfo invert = new RoleInfo("醉鬼", Color.yellow, "再喝！再喝！", "你的移动方向颠倒", RoleId.Invert, false, true, fullDescription: "醉鬼的附加职业会使你的控制完全颠倒（无论鼠标或键盘）\n醉鬼可以为任意阵营的附加职业");
        public static RoleInfo chameleon = new RoleInfo("变色龙", Color.yellow, "看不见我看不见我看不见我", "不动的时候会隐身", RoleId.Chameleon, false, true, fullDescription: "变色龙在静止的X秒内（取决于设置）会变得（部分或完全）隐形。\n你可以在隐身状态下使用能力，只有移动才会使你再次显现。\n伪装者阵营、独立阵营及船员阵营均可获得该附加职业。");
        public static RoleInfo shifter = new RoleInfo("交换师", Color.yellow, "你不觉得你的职业不好玩吗？", "与他人交换身份", RoleId.Shifter, false, true, fullDescription: "交换师可以交换一个船员的附加职业，如果另一个玩家也是船员，他们将交换他们的角色。\n职业交换将在下一次会议结束时、玩家被放逐前执行，被交换目标需要在会议开始前选择\n即使交换师或被交换目标在会议之前死亡，交换仍然会继续执行\n当被交换目标为伪装者或独立阵营时，交换师将在下次会议结束后自杀（不会出现尸体）\n交换师旨在通过交换接管被伪装者发现的警长或医生来拯救该类职业免于离开游戏。\n这在对抗抹除者上特别有效，但也使交换师变得像抹除者一样。\n与交换师存在交互的其他职业在文章中都已注明。");
  
        public static RoleInfo cursed  = new RoleInfo("反骨", Color.yellow, "公若不弃，吾愿拜为义父", "被内鬼击杀后会投靠内鬼阵营", RoleId.Cursed, false, true, fullDescription: "若被伪装者击杀，则变成伪装者");
        public static RoleInfo slueth  = new RoleInfo("掘墓人", Color.yellow, "知晓报告的尸体的身份", "知晓报告的尸体的身份", RoleId.Slueth, false, true, fullDescription: "掘墓人可以确认自己报告的死亡玩家的职业。");
        public static RoleInfo paranoid  = new RoleInfo("多线程", Color.yellow, "你能一心两用!", "你的交互界面是透明的!", RoleId.Paranoid, false, true, fullDescription: "多线程的交互界面半透明。");
        public static RoleInfo indomitable  = new RoleInfo("无畏", Color.yellow, "你无惧刺客的刺杀", "你免疫刺客的刺杀!", RoleId.Indomitable, false, true, fullDescription: "无畏无法被猜测");
        public static RoleInfo disperser = new RoleInfo("分散者", Color.red, "分散大伙", "让所有人散开", RoleId.Disperser, false, true, fullDescription: "行动阶段，分散者可以发动技能“分散”，\n将所有存活玩家分别传送到随机的地方");
        public static RoleInfo radar  = new RoleInfo("雷达", Color.yellow, "时刻警惕", "始终有一个指向离你最近的人的箭头", RoleId.Radar, false, true, fullDescription: "行动阶段，雷达持续通过箭头确认与自己距离最近的存活玩家。");
        public static RoleInfo antiReport = new RoleInfo("胆小鬼", Color.yellow, "啊啊啊啊啊啊啊啊啊啊啊啊啊！！！！！！", "你没有报告按钮", RoleId.AntiReport, false, true, fullDescription: "胆小鬼没有报告按钮");
        public static RoleInfo Allknowing = new("全知者", Color.red, "没有一个船员可以逃脱出你的眼睛", "箭头指向所有活着的船员", RoleId.AllKnowing, false, true, fullDescription: "全知者知晓场上所有船员的位置（除卧底）");
        public static RoleInfo viewer = new RoleInfo("观测者", Color.grey, "定位所有人", "轮流定位各种势力", RoleId.Viewer, true, true, fullDescription: "观测者轮流知晓场上所有伪装者和除部分中立以外的非伪装者的位置");
        public static RoleInfo recruiter = new RoleInfo("密教徒", Recruiter.color, "招募一位信徒", "招募一位信徒", RoleId.Recruiter, false, true, fullDescription: "游戏开局时仅一名伪装者的情况下，其可以招募一名信徒");
        public static RoleInfo flash = new RoleInfo("闪电侠", Flash.color, "光速飞行", "速度加快", RoleId.Flash, false, true, fullDescription: "闪电侠的移动速度增加。");
        public static RoleInfo giant = new RoleInfo("巨人", Giant.color, "看，那座大山会走路", "又大又慢", RoleId.Giant, false, true, fullDescription: "巨人的体型变大，移动速度减少。");
        public static RoleInfo onetimekiller = new RoleInfo("正义使者", OneTimeKiller.color, "值此一刀", "你可以出刀一次", RoleId.OneTimeKiller, false, true, fullDescription: "正义使者全场游戏可以无负面效果的出刀一次。");

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
            poucher,
            mimic,
            miner,
            escapist,
            pitfaller,
            shinobi,       
            snarer,
            transportist,       
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
            werewolf,
            swooper,
            juggernaut,
	        amnisiac,
            doomsayer,
            kataomoi,
            akujo,
            cerenovus,
	        veteren,
            bodyguard,
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
            transporter,
            teleporter,
            arbiter,
            bloody,
            antiTeleport,
            tiebreaker,
            sunglasses,
            cursed,
            mini,
            vip,
            invert,
            chameleon,
            shifter,    
            Allknowing,
            viewer,
			paranoid,
            slueth,
            radar,
            indomitable,
            disperser,
            recruiter, 
            flash, 
            giant, 
            onetimekiller,

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
                if (AntiReport.antiReport.Any(x => x.PlayerId == p.PlayerId)) infos.Add(antiReport);
                if (p == Cursed.cursed) infos.Add(cursed);
                if (p == Paranoid.paranoid) infos.Add(paranoid);
                if (p == Radar.radar) infos.Add(radar);
                if (p == AllKnowing.Allknowers.Any(x => x.PlayerId == p.PlayerId)) infos.Add(Allknowing);
                if (p == Viewer.viewer) infos.Add(viewer);
                if (p == Slueth.slueth) infos.Add(slueth);
                if (p == Indomitable.indomitable) infos.Add(indomitable);
                if (p == Disperser.disperser) infos.Add(disperser);

                if (Invert.invert.Any(x => x.PlayerId == p.PlayerId)) infos.Add(invert);
                if (Chameleon.chameleon.Any(x => x.PlayerId == p.PlayerId)) infos.Add(chameleon);
                if (p == Shifter.shifter) infos.Add(shifter);

                if (Recruiter.recruiter.Any(x => x.PlayerId == p.PlayerId)) infos.Add(recruiter);
                if (p == Flash.flash.Any(x => x.PlayerId == p.PlayerId)) infos.Add(flash);
                if (p == Giant.giant.Any(x => x.PlayerId == p.PlayerId)) infos.Add(giant);
                if (p == OneTimeKiller.onetimekiller) infos.Add(onetimekiller); 
            }

            int count = infos.Count;  // Save count after modifiers are added so that the role count can be checked

            // Special roles
            if (p == Jester.jester) infos.Add(jester);
			if (p == Swooper.swooper) infos.Add(swooper);
            if (p == Werewolf.werewolf) infos.Add(werewolf);
            if (p == Juggernaut.juggernaut) infos.Add(juggernaut);
            if (p == Doomsayer.doomsayer) infos.Add(doomsayer);
            if (p == Kataomoi.kataomoi) infos.Add(kataomoi);
            if (p == Akujo.akujo) infos.Add(akujo);
            if (p == Cerenovus.cerenovus || p == Cerenovus.formerCerenovus) infos.Add(cerenovus);
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


            if (p == Mimic.mimic) infos.Add(mimic);
            if (p == Poucher.poucher) infos.Add(poucher);
            if (p == Escapist.escapist) infos.Add(escapist);
            if (p == Snarer.snarer) infos.Add(snarer);
            if (p == Warlock.warlock) infos.Add(warlock);
            if (p == Witch.witch) infos.Add(witch);
            if (p == Ninja.ninja) infos.Add(ninja);
            if (p == Bomber.bomber) infos.Add(bomber);
            if (p == Yoyo.yoyo) infos.Add(yoyo);
            if (p == Pitfaller.pitfaller) infos.Add(pitfaller);
            if (p == Shinobi.shinobi) infos.Add(shinobi);
            if (p == Miner.miner) infos.Add(miner);
            if (p == Transportist.transportist) infos.Add(transportist);
            
            if (p == Detective.detective) infos.Add(detective);
            if (p == TimeMaster.timeMaster) infos.Add(timeMaster);
            if (p == Veteren.veteren) infos.Add(veteren);
            if (p == Medic.medic) infos.Add(medic);
            if (p == Swapper.swapper) infos.Add(swapper);
            if (p == BodyGuard.bodyguard) infos.Add(bodyguard);
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
            if (p == Transporter.transporter) infos.Add(transporter);
            if (p == Teleporter.teleporter) infos.Add(teleporter);
            if (p == Arbiter.arbiter) infos.Add(arbiter);
            if (p == Pursuer.pursuer) infos.Add(pursuer);
            if (p == Thief.thief) infos.Add(thief);
            if (p == Amnisiac.amnisiac) infos.Add(amnisiac);

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
            if (HandleGuesser.isGuesserGm && HandleGuesser.isGuesser(p.PlayerId)) roleName += " (带赌)";

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
                        roleName = Helpers.cs(Warlock.color, "(被术士咒) ") + roleName;
                    if (p == Ninja.ninjaMarked)
                        roleName = Helpers.cs(Ninja.color, "(被忍者标) ") + roleName;
                    if (Pursuer.blankedList.Contains(p) && !p.Data.IsDead)
                        roleName = Helpers.cs(Pursuer.color, "(被空包弹) ") + roleName;
                    if (Witch.futureSpelled.Contains(p) && !MeetingHud.Instance) // This is already displayed in meetings!
                        roleName = Helpers.cs(Witch.color, "被女巫咒☆ ") + roleName;
                    if (BountyHunter.bounty == p)
                        roleName = Helpers.cs(BountyHunter.color, "(被赏金) ") + roleName;
                    if (Arsonist.dousedPlayers.Contains(p))
                        roleName = Helpers.cs(Arsonist.color, "♨ ") + roleName;
                    if (p == Arsonist.arsonist)
                        roleName = roleName + Helpers.cs(Arsonist.color, $" (未涂油数 {CachedPlayer.AllPlayers.Count(x => { return x.PlayerControl != Arsonist.arsonist && !x.Data.IsDead && !x.Data.Disconnected && !Arsonist.dousedPlayers.Any(y => y.PlayerId == x.PlayerId); })} 人)");
                    if (p == Jackal.fakeSidekick)
                        roleName = Helpers.cs(Sidekick.color, $" (假随从)") + roleName;
                    if (Doomsayer.playerTargetinformation.Contains(p))
                        roleName = Helpers.cs(Color.gray, "(被洞察) ") + roleName;
                    if (p == Kataomoi.target)
                        roleName = Helpers.cs(Kataomoi.color, "(被单恋♡)") + roleName;
                    if (Akujo.keeps.Contains(p))
                        roleName = Helpers.cs(Color.gray, "(备胎)") + roleName;
                    if (p == Akujo.honmei)
                        roleName = Helpers.cs(Akujo.color, "(真爱)") + roleName;

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
                                    deathReasonString = $" - {Helpers.cs(Witch.color, "被咒杀")} ，女巫 {Helpers.cs(killerColor, deadPlayer.killerIfExisting.Data.PlayerName)} 标记";
                                    break;
                                case DeadPlayer.CustomDeathReason.LoverSuicide:
                                    deathReasonString = $" - {Helpers.cs(Lovers.color, "恋人殉情")}";
                                    break;
                                case DeadPlayer.CustomDeathReason.KataomoiSuicide:
                                    deathReasonString = $" - {Helpers.cs(Kataomoi.color, "单思情终")}";
                                    break;
                                case DeadPlayer.CustomDeathReason.LawyerSuicide:
                                    deathReasonString = $" - {Helpers.cs(Lawyer.color, "律师谢罪")}";
                                    break;
                                case DeadPlayer.CustomDeathReason.Bomb:
                                    deathReasonString = $" - 被 {Helpers.cs(killerColor, deadPlayer.killerIfExisting.Data.PlayerName)} 炸了";
                                    break;
                                case DeadPlayer.CustomDeathReason.Arson:
                                    deathReasonString = $" - 被 by {Helpers.cs(killerColor, deadPlayer.killerIfExisting.Data.PlayerName)} 烧了";
                                    break;
                                case DeadPlayer.CustomDeathReason.LoveStolen:
                                    deathReasonString = $" - {Helpers.cs(Akujo.color, "爱人被夺")}";
                                    break;
                                case DeadPlayer.CustomDeathReason.Loneliness:
                                    deathReasonString = $" - {Helpers.cs(Akujo.color, "精力衰竭")}";
                                    break;
                                case DeadPlayer.CustomDeathReason.BrainwashedKilled:
                                    deathReasonString = $" - 被 {Helpers.cs(killerColor, deadPlayer.killerIfExisting.Data.PlayerName)} 洗了";
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

