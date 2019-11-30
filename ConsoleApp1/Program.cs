using System;
using System.Diagnostics;
using System.Threading;
using System.Globalization;
//“****”代表尚未实现的东西
#region 用来方便调用的东西
public enum MenuEnum //菜单选项的枚举类型
{
    StartGame,
    ExitGame,
    AboutTheGame,
}
public enum RoleChoice//角色选项的枚举类型
{
    KujoJotaro,//卖鱼强
    HigashikataJosuke,//仗助
    GiornoGiovanna,//茸茸
}

namespace ConsoleApp1
{

    //换行打印以及下划线打印
    class AboutPrint
    {
        public static void PrintLineFeed(int LineFeednumber)
        {
            int i;
            for (i = 0; i < LineFeednumber; i++)
                Console.WriteLine();
        }
        //一个界面切换成另一个界面时每次都进行调用
        public static void PrintUnderLine()
        {
            int i;
            for (i = 0; i < 40; i++)
                Console.Write("--");
            Console.WriteLine();
        }
    }

    //存放全局变量的类
    public class Statics
    {
        public static int RoleChose;
        public static int[] Levels = new int[11] { 0, 10, 30, 55, 85, 120, 160, 210, 270, 340, 420 };
        public static int TheNumberOfRole;
    }
    #endregion
    //主程序
    class Program
    {
        #region 主函数
        static void Main(string[] args)
        {
            //***************************************
            ROLES[] roles = new ROLES[4];//主角色类数组
            Role1 role1 = new Role1(); //分别构造三种jojo
            Role2 role2 = new Role2();
            Role3 role3 = new Role3();
            roles[1] = role1;
            roles[2] = role2;
            roles[3] = role3;
            int[] item = new int[4];//初始化背包中的物品
            item[1] = 0;
            item[2] = 0;
            item[3] = 0;
            //初始化************************************
            Menu menu = new Menu();
            TheShop theShop = new TheShop();
            TheGamePanel theGamePanel = new TheGamePanel();
            //在游戏框架内循环
            while (true)
            {
                int theChoiceOfMenuPanel = menu.MenuPanelPrint();
                //包括了 开始游戏 结束游戏 关于游戏三个方法
                //开始游戏              
                if (theChoiceOfMenuPanel == 1)
                {
                    int RoleNumber = menu.BeforeTheStartGame();//theRolNumber保存了用户选择的角色的编号
                    int GamePanelNumber;
                    TheStoryModel theStoryModel = new TheStoryModel(roles[RoleNumber]);
                    //进入游戏走向不归路
                    while (true)
                    {
                        GamePanelNumber = theGamePanel.TheGamePanelPrint();
                        switch (GamePanelNumber)
                        {
                            case 1: //查看角色属性
                                roles[RoleNumber].PrintTheAtrributesOfRole();

                                break;
                            case 2://剧情模式
                                TheStoryModel storyModel = new TheStoryModel(roles[RoleNumber]);
                                storyModel.TheWholeModelAction(roles[RoleNumber]);

                                break;
                            case 3://商店
                                theShop.PrintfTheShop();

                                break;
                            case 4://背包
                                theShop.GetToTheBeg();

                                break;
                            case 5://退出
                                Process.GetCurrentProcess().Kill();

                                break;
                            default:
                                break;
                        }
                    }
                }
                //结束游戏
                else if (theChoiceOfMenuPanel == 2)
                {
                    Process.GetCurrentProcess().Kill();
                }
                //关于游戏
                else if (theChoiceOfMenuPanel == 3)
                {
                    menu.AboutTheGame();
                }
            }
        }//Main函数
        #endregion
    }//class program
    #region 开始菜单
    /// <summary>  开始菜单 </summary>
    class Menu
    {
        //玩家选择

        //游戏开始界面 包含所有方法调用的路径
        public int MenuPanelPrint()
        {
            AboutPrint.PrintUnderLine();
            Console.WriteLine("Jo Jo の 糟糕冒险--偷 工 减 料 汉化版");
            AboutPrint.PrintLineFeed(1);
            Console.WriteLine("                             ------------作者 ？");
            Console.WriteLine("1.  开 始 游 戏");
            AboutPrint.PrintLineFeed(2);
            Console.WriteLine("2.  结 束 游 戏");
            AboutPrint.PrintLineFeed(2);
            Console.WriteLine("3.  关 于 游 戏");
            Console.WriteLine("请输入选项:");
            int theChoiceOfMenuPanel = Convert.ToInt32(Console.ReadLine());
            if (theChoiceOfMenuPanel == 1)
            { return 1; }
            if (theChoiceOfMenuPanel == 2)
            { return 2; }//离开
            if (theChoiceOfMenuPanel == 3)
            { return 3; }
            return 0;
        }
        #region 选择关于游戏后
        //用户输入菜单页面的选择
        public int AboutTheGame()
        {
            AboutPrint.PrintUnderLine();

            Console.WriteLine("JOJO の 糟糕冒险");

            AboutPrint.PrintLineFeed(2);
            Console.WriteLine("1. 关 于 游 戏");

            AboutPrint.PrintLineFeed(2);
            Console.WriteLine("2. 世 界 观 ");

            AboutPrint.PrintLineFeed(2);
            Console.WriteLine("3. 游 戏 运 行 说 明");
            AboutPrint.PrintLineFeed(4);
            Console.WriteLine("请输入需要查看的选项或者输入0退出");
            return PlayerMenuChoiceOfAboutTheGame();
        }
        //游戏开始界面选择3
        public int PlayerMenuChoiceOfAboutTheGame()
        {
            int theAboutGameChoice = Convert.ToInt32(Console.ReadLine());
            int PlayerMenuChoiceOfAboutTheGameFlag;
            if (theAboutGameChoice == 0)
                return 0;
            if (theAboutGameChoice == 1)
            {
                AboutPrint.PrintUnderLine();
                Console.WriteLine("本游戏由计算机大二咸鱼在二十天内加班打出，纯手打，没有存档功能，质量不敢恭维，请大佬手下留情给点活路");
                Console.WriteLine("返回请输入1 退出请按0：");
                AboutPrint.PrintLineFeed(3);
                PlayerMenuChoiceOfAboutTheGameFlag = Convert.ToInt32(Console.ReadLine());
                if (PlayerMenuChoiceOfAboutTheGameFlag == 1)
                    AboutTheGame();
                else { }//退出
            }

            else if (theAboutGameChoice == 2)
            {
                AboutPrint.PrintUnderLine();
                Console.WriteLine("jojo的世界观嘛 什么替身什么欧拉木大");
                Console.WriteLine("嘴上说是jojo但实际上一点智斗的因素都没有( ?′ω`? ) 现在那么菜怎么智斗");
                Console.WriteLine("返回请输入1 退出请按0：");
                AboutPrint.PrintLineFeed(3);
                PlayerMenuChoiceOfAboutTheGameFlag = Convert.ToInt32(Console.ReadLine());
                if (PlayerMenuChoiceOfAboutTheGameFlag == 1)
                    AboutTheGame();
                else { }//退出
            }
            else if (theAboutGameChoice == 3)
            {
                AboutPrint.PrintUnderLine();
                Console.WriteLine("根据自己所要的选项来选择数字，遇到bug就当做没遇到啦");
                Console.WriteLine("返回请输入1 退出请按0：");
                AboutPrint.PrintLineFeed(3);


                PlayerMenuChoiceOfAboutTheGameFlag = Convert.ToInt32(Console.ReadLine());
                if (PlayerMenuChoiceOfAboutTheGameFlag == 1)
                    AboutTheGame();//返回关于游戏界面
                else { }//退出
            }

            return 0;

        }
        #endregion

        //选项二 开始游戏
        public int BeforeTheStartGame()
        {
            int BeforeTheStartGameflag;
            AboutPrint.PrintUnderLine();
            Console.WriteLine("众 所 周 知 ，jojo家  世 世 代 代  都是  绅 士");
            Console.WriteLine("这一次jo家人决定派出一个代表去揍扁   豫  章  书  院  的优秀代表教师:");
            Console.WriteLine("人民的好老师---  杨   永  信");
            Console.WriteLine("jojo家的  绅♂士♂  精神 就交给你来守护了！");
            AboutPrint.PrintLineFeed(3);
            Console.WriteLine("                           -->to be contiune");
            Console.WriteLine("                           按1继续");
            BeforeTheStartGameflag = Convert.ToInt32(Console.ReadLine());
            if (BeforeTheStartGameflag == 1)
                return StartGamePanel();//进入游戏面板

            Console.WriteLine("按1！");
            BeforeTheStartGame();
            return 0;
        }
        public int StartGamePanel()
        {

            AboutPrint.PrintUnderLine();
            Console.WriteLine(" 选 择 角 色");
            AboutPrint.PrintUnderLine();
            Console.WriteLine("1.  Kujo Jotaro");
            Console.WriteLine("卖鱼强");
            AboutPrint.PrintLineFeed(3);
            Console.WriteLine("2.  Higashikata Josuke");
            Console.WriteLine("东方仗助");
            AboutPrint.PrintLineFeed(3);
            Console.WriteLine("3.  Giorno Giovanna");
            Console.WriteLine("茸茸");
            AboutPrint.PrintLineFeed(3);
            Console.WriteLine("选择你想要的角色:");
            return ReturnTheNumberOfRole();
        }
        public int ReturnTheNumberOfRole()
        {
            TheGamePanel gamePanel = new TheGamePanel();
            int PlayerChoiceWhatName;
            PlayerChoiceWhatName = Convert.ToInt32(Console.ReadLine());
            switch (PlayerChoiceWhatName)
            {
                //阿强
                case 1:
                    Statics.TheNumberOfRole = 1;
                    AboutPrint.PrintUnderLine();
                    AboutPrint.PrintLineFeed(3);
                    Console.WriteLine("力速双A的 快乐，就是那么  朴  实  无  华，且  枯  燥。");
                    Console.WriteLine("承太郎！快用你那  无  敌  的  白  金  之 星   想想办法啊！");
                    return 1;//在region游戏本体中

                //仗助
                case 2:
                    Statics.TheNumberOfRole = 2;
                    AboutPrint.PrintUnderLine();
                    AboutPrint.PrintLineFeed(3);
                    Console.WriteLine("哇!这个  飞  机  头  难道是！");
                    Console.WriteLine("你说我的  发  型   怎么？？？");
                    return 2;
                //茸茸
                case 3:
                    Statics.TheNumberOfRole = 3;
                    AboutPrint.PrintUnderLine();
                    AboutPrint.PrintLineFeed(3);
                    Console.WriteLine("我  乔鲁诺乔巴纳  有个梦想！");
                    Console.WriteLine("我要成为广场中央的  秧  歌  star ！！");
                    return 3;
                default:
                    AboutPrint.PrintUnderLine();
                    AboutPrint.PrintLineFeed(3);
                    Console.WriteLine("请输入正确的数字");
                    return 0;
            }
        }
    }
    #endregion
    #region 游戏本体
    //--------------------------------------------------游戏开始界面实现完毕
    //以下开始实现游戏本体
    class TheGamePanel
    {
        //初始化背包中的物品
        public int TheGamePanelPrint() //***************尚未和主菜单关联
        {
            AboutPrint.PrintUnderLine();
            AboutPrint.PrintLineFeed(3);
            Console.WriteLine("1.   查看人物属性");
            Console.WriteLine("2.   剧情模式");
            Console.WriteLine("3.   商店");
            Console.WriteLine("4.   背包");
            Console.WriteLine("5.   退出" + "   （游戏没有存档功能，直接离开会失去进度且结束程序）");
            AboutPrint.PrintLineFeed(3);
            Console.WriteLine("请输入数字：");
            return TheChoiceOfTheGamePanel();
        }
        public int TheChoiceOfTheGamePanel()
        {
            int TheGamePanelFlag;
            TheGamePanelFlag = Convert.ToInt32(Console.ReadLine());
            switch (TheGamePanelFlag)
            {
                case 1://***********************查看人物属性
                    return 1;
                case 2://************************剧情模式
                    return 2;
                case 3://*************************商店
                    return 3;
                case 4://*************************背包
                    return 4;
                case 5://**************************退出
                    return 5;
                default:
                    return 0;
            }
       
        }
        public void GetToTheAtrributesOfRole(ROLES role)
        {
            role.PrintTheAtrributesOfRole();
        }
        public void GetToTheStoryModel()//***************
        {

        }
        public void GetToTheShop()//**********目前要实现的商店 如果买了该物品 会返回物品对应物体的下标 此时该物品数量加一
        {
            TheShop theShop = new TheShop();
            theShop.PrintfTheShop();
        }

    }
    public class TheShop
    {
        public static int[] item = new int[4] { 0, 0, 0, 0 };
        public void GetToTheBeg()//打印背包所有的东西
        {
            AboutPrint.PrintUnderLine();
            Console.WriteLine("背包界面：");
            AboutPrint.PrintLineFeed(3);
            Console.WriteLine("拥有治疗药水         " + "x" + item[1]);
            Console.WriteLine("拥有全属性加强药水      " + "x" + item[2]);
            Console.WriteLine("拥有减CD药水        " + "x" + item[3]);
            AboutPrint.PrintLineFeed(2);
            Console.WriteLine("按1退出");
            Console.ReadLine();
        }
        public void PrintfTheShop()
        {
            AboutPrint.PrintUnderLine();
            Console.WriteLine("欢迎来到jo护车商店！");
            AboutPrint.PrintLineFeed(3);
            Console.WriteLine("jojo才不为没用的东西付钱呢！");
            Console.WriteLine("可购买物品：");
            Console.WriteLine("1.   治疗药水(恢复10点HP)                  ");
            Console.WriteLine("2.   全属性加强药水(攻击防御速度+5）              ");
            Console.WriteLine("3.   减CD药水(减少技能CD三回合)                ");
            Console.WriteLine("请输入你想购买的物品：");
            int thing = Convert.ToInt32(Console.ReadLine());
            AboutPrint.PrintUnderLine();
            AboutPrint.PrintLineFeed(2);
            Console.WriteLine("我jojo虽然遇到不喜欢的东西买了也不付钱");
            Console.WriteLine( "但也明白什么是正义");
            Console.WriteLine("拿完就跑！");
            AboutPrint.PrintLineFeed(2);
            Console.WriteLine("按1退出");
            Console.ReadLine();
            item[thing]++;

        }

    }
    public class TheStoryModel
    {
        private ROLES roles;
        private TheShop theShop;
        public TheStoryModel(ROLES roles)
        {
            this.roles = roles;
        }
        public void TheWholeModelAction(ROLES roles)
        {
            int NumberOfContentOfTasks;
            PrintTasks();
            NumberOfContentOfTasks = ContentOfTasks();
            switch (NumberOfContentOfTasks)
            {
                case 1:
                    roles.text(1);
                    break;
                case 2:
                    roles.text(2);
                    break;
                case 3:
                    roles.text(3);
                    break;
                case 4:
                    roles.text(4);
                    break;
                case 5:
                    roles.text(5);
                    break;
                case 6:
                    roles.BossText();
                    break;

                default:
                    break;
            }
        }
        public void PrintTasks() //***********战斗系统未实现
        {
            AboutPrint.PrintUnderLine();
            AboutPrint.PrintLineFeed(2);
            
            Console.Write("任务1.  小试jo刀    ");
            Console.WriteLine("适合等级 " + "0级");         
            AboutPrint.PrintLineFeed(2);
            Console.Write("任务2.  步入歧途    ");
            Console.WriteLine("适合等级 " + "2级");
            AboutPrint.PrintLineFeed(2);
            Console.Write("任务3.  逐渐jo化    ");
            Console.WriteLine("适合等级 " + "5级");            
            AboutPrint.PrintLineFeed(2);
            Console.Write("任务4.  痛击舔狗    ");
            Console.WriteLine("适合等级 " + "7级"); 
            AboutPrint.PrintLineFeed(2);
            Console.Write("任务5.  惩治滑稽    ");
            Console.WriteLine("适合等级 " + "9级");
            AboutPrint.PrintLineFeed(2);
            Console.Write("最终决战.  放电大叔    ");
            Console.WriteLine("适合等级 " + "10级");
            AboutPrint.PrintLineFeed(2);
            Console.WriteLine("输入数字开始任务");
        }
        public int ContentOfTasks()
        {
            int thebeginFlag;
            int choiceofTasks = Convert.ToInt32(Console.ReadLine());
            switch (choiceofTasks)
            {
                //用户输入0时
                case 0:
                    return 0;
                case 1:           //任务一
                    AboutPrint.PrintUnderLine();
                    Console.WriteLine("身为jojo，世世代代都是绅士。作为主角的你更不例外");
                    Console.WriteLine("今天在福大的食堂，一群插队的辅导员不幸遇到了你");
                    Console.WriteLine("jo家的基因燃起了你心中中二的小宇宙，众所周知，真正的英雄在脑袋做出反应之前");
                    Console.WriteLine("身体就先行动起来了！！");
                    Console.WriteLine("敌人信息：");
                    Console.WriteLine("攻击力：10    防御力： 3   生命值： 20   速度：  2    ");
                    AboutPrint.PrintLineFeed(3);
                    Console.WriteLine("输入1任务开始");
                    thebeginFlag = Convert.ToInt32(Console.ReadLine());
                    if (thebeginFlag == 1) { return 1; }
                    /*
                    if (thebeginFlag == 0)
                    {
                        PrintTasks();
                    }
                    */
                    return 1000;
                case 2:           //任务二

                    AboutPrint.PrintUnderLine();
                    Console.WriteLine("也不知道哪些人是好人哪些是坏人，那就全部打一顿好了");
                    Console.WriteLine("毕竟这才是最佳解决方式吧");
                    Console.WriteLine("敌人信息：");
                    Console.WriteLine("攻击力： 20   防御力： 5   生命值：150    速度： .3     ");
                    AboutPrint.PrintLineFeed(3);
                    Console.WriteLine("输入1任务开始");
                    thebeginFlag = Convert.ToInt32(Console.ReadLine());
                    if (thebeginFlag == 1) { return 2; }
                    /*
                    if (thebeginFlag == 0)
                    {
                        PrintTasks();
                    }
                    */
                    return 1000;
                case 3:             //任务三
                    AboutPrint.PrintUnderLine();
                    Console.WriteLine("豫章书院还在胡作非为！");
                    Console.WriteLine("既然键盘不能打败他们");
                    Console.WriteLine("那就用拳头来说服他们");
                    Console.WriteLine("敌人信息：");
                    Console.WriteLine("攻击力： 30   防御力： 10   生命值：200    速度： 3     ");
                    AboutPrint.PrintLineFeed(3);
                    Console.WriteLine("输入1任务开始");
                    thebeginFlag = Convert.ToInt32(Console.ReadLine());
                    if (thebeginFlag == 1) { return 3; }
                    return 1000;

                case 4:
                    AboutPrint.PrintUnderLine();
                    Console.WriteLine("杨永信设置了两道关卡");
                    Console.WriteLine("两条舔狗挡在了你的面前....");
                    Console.WriteLine("行吧....舔狗是真的牛逼！");
                    Console.WriteLine("敌人信息：");
                    Console.WriteLine("攻击力： 25   防御力： 50   生命值：300    速度：  3    ");
                    AboutPrint.PrintLineFeed(3);
                    Console.WriteLine("输入1任务开始");
                    thebeginFlag = Convert.ToInt32(Console.ReadLine());
                    if (thebeginFlag == 1)
                    { return 4; }
                    return 1000;
                case 5:
                    AboutPrint.PrintUnderLine();
                    Console.WriteLine("来到了第二关卡");
                    Console.WriteLine("两个滑稽怪挡在了你的面前....");
                    Console.WriteLine("整天逛贴吧的你怒火中烧！");
                    Console.WriteLine("敌人信息：");
                    Console.WriteLine("攻击力：60    防御力： 20   生命值： 300   速度：  3    ");
                    AboutPrint.PrintLineFeed(3);
                    Console.WriteLine("输入1任务开始");
                    thebeginFlag = Convert.ToInt32(Console.ReadLine());
                    if (thebeginFlag == 1)
                    { return 5; }
                    return 1000;
                case 6:
                    AboutPrint.PrintUnderLine();
                    Console.WriteLine("“你终于来了”");
                    Console.WriteLine("辣个浑身放电的大叔站在了你的前面");
                    Console.WriteLine("”网瘾就应该受到非人的对待吗“");
                    Console.WriteLine("“我这是用效率最高的方式让他们摆脱网络的毒害”");
                    Console.WriteLine("既然眼前的这个人连是非和人性都没有，那只能用拳头告诉他谁是对的了");
                    Console.WriteLine("敌人信息：");
                    Console.WriteLine("攻击力：45    防御力：20    生命值：200    速度：2      ");
                    AboutPrint.PrintLineFeed(3);
                    Console.WriteLine("输入1任务开始");
                    thebeginFlag = Convert.ToInt32(Console.ReadLine());
                    if (thebeginFlag == 1)
                    { return 6; }
                    return 1000;
                default:
                    return 1000;
            }
        }
        public void RunTasks()
        {

        }

    }
    //查看角色目前属性的类

    #endregion
    #region 抽象类角色
    //角色的属性和技能
    public abstract class ROLES//抽象类角色 
    {
        protected string skill1 = "普通攻击";
        protected string skill2 = "防御";
        protected string skill3;
        protected string skill4;
        protected string name;
        protected int EXPERIENCE=0;
        protected int LEVEL;
        protected int HP;
        protected int ATK;
        protected int DEF;
        protected int CD;
        protected float PT;
        protected int SPEED;
        //抽象类的构造函数//还没决定角色的属性呢....
        public abstract int skill(int a);
        public int returnTheItem()
        {
            int a;
            Console.WriteLine("1.治疗药水(恢复10点HP) " );
            Console.WriteLine("2.全属性加强药水(攻击防御速度+5）" );
            Console.WriteLine("3.减CD药水(减少技能CD三回合) ");
            Console.WriteLine("请输入：");
            a = Convert.ToInt32(Console.ReadLine());
            return a;
        }
        public void text(int textChoice)
        {
            Enemy enemy = new Enemy(10, 3, 20, 2, "某导");
            switch (textChoice)
            {
                case 1:
                    enemy = new Enemy(10, 3, 20, 2, "某导");
                    break;
                case 2:
                    enemy = new Enemy(20, 5, 40, 3, "面包人");//面包人
                    break;
                case 3:
                    enemy = new Enemy(30, 10, 100, 3, "官方");//官方势力
                    break;
                case 4:
                    enemy = new Enemy(25, 50, 200, 3, "舔狗");//舔狗
                    break;
                case 5:
                    enemy = new Enemy(60, 20, 250, 3, "滑稽怪");//滑稽怪
                    break;
                default:
                    break;
            }

            //敌人属性;
            int EnemyHP = enemy.GetHP();
            int EnemyATK = enemy.GetATK();
            int EnemyDEF = enemy.GetDef();
            int isEnemyDEF = 0;
            int isIbuffFlag = 0;
            int isIdefFlag = 0;//我方防御
            int isTimeStop = 0;
            int isCrazy = 0;
            int isGold = 0;
            int MyHP = this.HP;
            int MyATK = this.ATK;
            int MyDEF = this.DEF;
            int TheEndFlag = 0;
            int SkillCd = this.CD;
            //判断先后手
            while (EnemyHP > 0 && MyHP > 0)
            {

                if (TheEndFlag == 1) { break; }//不做
                else
                {       
                    SkillCd--;//对手回合也要调用
                    if (SkillCd <= 0)//大招cd
                        SkillCd = 0;
                    if (isIbuffFlag == 1){//我方收到debuff或者buff判断状态 主角就是没有debuff我不管啊
                        if (isCrazy == 1)
                        { MyHP = this.HP;}       
                        if (isGold == 1)
                        {
                            MyHP += (int)0.3 * MyHP;
                            MyATK += (int)0.3 * MyATK;
                        }
                        isIbuffFlag = 0;
                    }
                    //我方回合 turn    
                    AboutPrint.PrintLineFeed(1);
                    AboutPrint.PrintUnderLine();
                    AboutPrint.PrintUnderLine();
                    AboutPrint.PrintLineFeed(1);
                    Console.WriteLine("我 方 角 色  拥有HP:  " + MyHP);
                    AboutPrint.PrintLineFeed(2);
                    Console.WriteLine("我 方 大 招  还剩CD：" + SkillCd);
                    AboutPrint.PrintLineFeed(2);
                    Console.WriteLine("敌 方 角 色  拥有HP:  " +EnemyHP);
                    AboutPrint.PrintLineFeed(2);
                    Console.WriteLine("1. 攻击   ");
                    Console.WriteLine("2. 防御   ");
                    Console.WriteLine("3. 技能   ");
                    Console.WriteLine("4. 背包   ");
                    Console.WriteLine("5. 逃跑    ");
                    Console.WriteLine("请输入你的选择");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            int OurAtk = turn(1);
                            if (isEnemyDEF == 1)//无法破防
                            {
                                if (OurAtk < EnemyDEF)
                                { Console.WriteLine("无 法 破 防 !"); }
                                else
                                {
                                    EnemyHP -= (OurAtk - EnemyDEF);
                                    if (OurAtk == this.ATK * 2)
                                    {
                                        AboutPrint.PrintUnderLine();
                                        AboutPrint.PrintLineFeed(3);
                                        Console.WriteLine(this.name + " 使用了 " + this.skill3 + "  对  " + enemy.GetName() + " 造成 " + (OurAtk - EnemyDEF) + " 点 伤害") ;
                                        AboutPrint.PrintLineFeed(3);
                                        AboutPrint.PrintUnderLine();
                                        AboutPrint.PrintLineFeed(1);

                                    }
                                       
                                    else
                                    {
                                        AboutPrint.PrintUnderLine();
                                        AboutPrint.PrintLineFeed(3);
                                        Console.WriteLine(this.name + " 使用了 " + this.skill1 + "  对  " + enemy.GetName() + " 造成 " + (OurAtk - EnemyDEF) + " 点 伤害");
                                        AboutPrint.PrintLineFeed(3);
                                        AboutPrint.PrintUnderLine();
                                        AboutPrint.PrintLineFeed(1);
                                    }
                                        

                                }//敌方扣血}
                                isEnemyDEF = 0;
                            }
                            else
                            {                               
                                    EnemyHP -= OurAtk;
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(3);
                                    Console.WriteLine(this.name + " 使用了 " + this.skill1 + "   对   " + enemy.GetName() + " 造 成 " + (OurAtk) + " 点 伤 害");
                                    AboutPrint.PrintLineFeed(3);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(1);
                                
                            }
                            break;
                        case 2:
                            turn(2);//我方进入防御状态
                            isIdefFlag = 1;
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintLineFeed(3);
                            Console.WriteLine(this.name + "   进入了  防御  状态");
                            AboutPrint.PrintLineFeed(3);
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintLineFeed(1);
                            break;
                        case 3:
                            if (SkillCd == 0)
                            {
                                int judgeTheBuff;
                                turn(3);
                                judgeTheBuff = this.skill(4);//三种大招三种状态
                                switch (judgeTheBuff)
                                {
                                    case 1:
                                        isTimeStop = 3;
                                        AboutPrint.PrintUnderLine();
                                        AboutPrint.PrintLineFeed(2);
                                        Console.WriteLine("The   World！");
                                        AboutPrint.PrintLineFeed(2);
                                        AboutPrint.PrintUnderLine();
                                        SkillCd = this.CD;
                                        break;
                                    case 2:
                                        isCrazy = 1;
                                        AboutPrint.PrintUnderLine();
                                        AboutPrint.PrintLineFeed(2);
                                        Console.WriteLine("Crazy   diamond");
                                        AboutPrint.PrintLineFeed(2);
                                        AboutPrint.PrintUnderLine();
                                        SkillCd = this.CD;
                                        isIbuffFlag = 1;
                                        break;
                                    case 3:
                                        isGold = 1;
                                        AboutPrint.PrintUnderLine();
                                        AboutPrint.PrintLineFeed(2);
                                        Console.WriteLine("Golden   Experience");
                                        AboutPrint.PrintLineFeed(2);
                                        AboutPrint.PrintUnderLine();
                                        SkillCd = this.CD;
                                        isIbuffFlag = 1;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                        case 4:
                            AboutPrint.PrintUnderLine();
                            int IchoiceItem = returnTheItem();
                            switch (IchoiceItem)
                            {
                                case 1:
                                    MyHP += 10;
                                    AboutPrint.PrintLineFeed(1);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(2);
                                    Console.WriteLine(" 使用成功 ！" + "   角色目前生命值为   " + MyHP);
                                    AboutPrint.PrintLineFeed(2);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(1);
                                    break;
                                case 2:
                                    MyATK += 5;
                                    MyDEF += 5;
                                    AboutPrint.PrintLineFeed(1);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(2);
                                    Console.WriteLine(" 使用成功 ！" + "   角色目前攻击力为   " + MyATK);
                                    Console.WriteLine(" 使用成功 ！" + "   角色目前防御力为   " + MyDEF);
                                    AboutPrint.PrintLineFeed(2);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(1);
                                    break;
                                case 3:
                                    SkillCd -= 2;
                                    AboutPrint.PrintLineFeed(1);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(2);
                                    Console.WriteLine(" 使用成功 ！" + "  角色的技能CD为   " + SkillCd);
                                    AboutPrint.PrintLineFeed(2);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(1);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 5:
                            TheEndFlag = 1;
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintLineFeed(3);
                            Console.WriteLine("( T A T) 凸");
                            Console.WriteLine("变强一点再来吧~");
                            AboutPrint.PrintLineFeed(3);
                            break;
                        default:
                            break;
                    }

                    if (EnemyHP < 0)
                    {
                        AboutPrint.PrintUnderLine();
                        AboutPrint.PrintUnderLine();
                        Console.WriteLine("~ 任 务 胜 利！");
                        this.GainExperience(20*textChoice);
                        if (!isLevelUp())
                        {
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintLineFeed(1);
                            Console.WriteLine("恭喜   获得经验   10点");
                            AboutPrint.PrintLineFeed(3);
                            Console.WriteLine("距离 下一级 还差  " + (Statics.Levels[this.LEVEL + 1] - this.EXPERIENCE));
                            AboutPrint.PrintLineFeed(3);
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintLineFeed(1);
                            Console.WriteLine("按1继续");
                            int res = Convert.ToInt32(Console.ReadLine());
                        }
                        else
                        {
                            if (this.LEVEL < 11)
                            {
                                AboutPrint.PrintUnderLine();
                                AboutPrint.PrintLineFeed(1);
                                Console.WriteLine(this.name + "   升级！   当前等级  " + this.LEVEL);
                                AboutPrint.PrintLineFeed(3);
                                Console.WriteLine(this.name + "变强了");
                                Console.WriteLine("攻击力   ---->" + this.ATK);
                                AboutPrint.PrintLineFeed(2);
                                Console.WriteLine("防御力   ---->" + this.DEF);
                                AboutPrint.PrintLineFeed(2);
                                Console.WriteLine("生命值   ---->" + this.HP);
                                AboutPrint.PrintLineFeed(3);
                                if (this.LEVEL <= 10) {
                                Console.WriteLine("距离 下一级 还差  " + (Statics.Levels[this.LEVEL + 1] - this.EXPERIENCE)); }
                                else
                                Console.WriteLine("你家jojo已经满级了");
                                AboutPrint.PrintLineFeed(3);
                                AboutPrint.PrintUnderLine();
                                AboutPrint.PrintUnderLine();
                                AboutPrint.PrintLineFeed(1);
                                Console.WriteLine("按1继续");
                                int res = Convert.ToInt32(Console.ReadLine());
                            }
                            else
                            {
                                AboutPrint.PrintUnderLine();
                                AboutPrint.PrintLineFeed(1);
                                Console.WriteLine(this.name + "已经满级了！");
                            }
                        }
                        TheEndFlag = 1;
                    }
                }
                if (TheEndFlag == 1) {
                    if (MyHP < 0)
                    {//失败
                        AboutPrint.PrintUnderLine();
                        AboutPrint.PrintUnderLine();
                        AboutPrint.PrintLineFeed(3);
                        Console.WriteLine("( T A T) 凸");
                        Console.WriteLine("变强一点再来吧~");
                        AboutPrint.PrintLineFeed(3);
                        TheEndFlag = 1;
                    }
                    break; }
                else //敌方回合 turn
                {
                   
                    if (isTimeStop!=0 ) 
                    {
                        isTimeStop--;     
                    }//被时停
                    else
                    {
                        Random random2 = new Random();
                        int EnemyrandomSkill = random2.Next(1, 5);
                        if (EnemyrandomSkill == 1 || EnemyrandomSkill == 2)
                        {
                            if (isIdefFlag == 1)//无法破防
                            {
                                if (EnemyATK <= this.DEF)
                                {
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(3);
                                    Console.WriteLine("  无  法  破  防 ! ");
                                    AboutPrint.PrintLineFeed(3);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(1);
                                }
                                else
                                {
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(3);
                                    Console.WriteLine(enemy.GetName() + "对" + this.name + "造成" + (EnemyATK - this.DEF) + "点伤害");
                                    AboutPrint.PrintLineFeed(3);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(1);
                                    MyHP -= (EnemyATK - this.DEF);
                                    isIdefFlag = 0;
                                }//我方扣血

                            }

                            else
                            {
                                if (EnemyATK <= this.DEF)
                                {
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(3);
                                    Console.WriteLine("  无  法  破  防 ! ");
                                    AboutPrint.PrintLineFeed(3);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(1);
                                }
                                else
                                {
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(3);
                                    Console.WriteLine(enemy.GetName() + "  对  " + this.name + "  造成  " + EnemyATK + " 点  伤害");
                                    MyHP -= EnemyATK;
                                    AboutPrint.PrintLineFeed(3);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(1);
                                }//我方扣血
                            }
                        }
                        else
                        {
                            isEnemyDEF = 1;
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintLineFeed(3);
                            Console.WriteLine("  敌人"+enemy.GetName()+"使用了  防御  " + "下回合将  抵消  " + EnemyDEF + " 点  伤害");
                            AboutPrint.PrintLineFeed(3);
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintLineFeed(1);
                        }
                        if (MyHP < 0)
                        {//失败
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintLineFeed(3);
                            Console.WriteLine("( T A T) 凸");
                            Console.WriteLine("变强一点再来吧~");
                            AboutPrint.PrintLineFeed(3);
                            TheEndFlag = 1;
                        }
                        SkillCd--;
                    }
                }
            }
        }

        public void BossText()
        {
            BOSS boss = new BOSS(30,30,600,2,"电击之王永信",2);


            //敌人属性;
            int EnemyHP = boss.GetHP();
            int EnemyATK = boss.GetATK();
            int EnemyDEF = boss.GetDef();
            int BossCD = boss.GetCD();
            int isEnemyDEF = 0;
            int isIbuffFlag = 0;
            int isIdefFlag = 0;//我方防御
            int isTimeStop = 0;
            int isCrazy = 0;
            int isGold = 0;
            int MyHP = this.HP;
            int MyATK = this.ATK;
            int MyDEF = this.DEF;
            int TheEndFlag = 0;
            int SkillCd = this.CD;
            
            //判断先后手
            while (EnemyHP > 0 && MyHP > 0)
            {

                if (TheEndFlag == 1) { break; }//不做
                else
                {
                    SkillCd--;//对手回合也要调用
                    BossCD--;
                    if (SkillCd <= 0)//大招cd
                        SkillCd = 0;
                    if (isIbuffFlag == 1)
                    {//我方收到debuff或者buff判断状态 主角就是没有debuff我不管啊
                        if (isCrazy == 1)
                        { MyHP = this.HP; }
                        if (isGold == 1)
                        {
                            MyHP += (int)0.3 * MyHP;
                            MyATK += (int)0.3 * MyATK;
                        }
                        isIbuffFlag = 0;
                    }
                    //我方回合 turn    
                    AboutPrint.PrintLineFeed(1);
                    AboutPrint.PrintUnderLine();
                    AboutPrint.PrintUnderLine();
                    AboutPrint.PrintLineFeed(1);
                    Console.WriteLine("我 方 角 色  拥有HP:  " + MyHP);
                    AboutPrint.PrintLineFeed(2);
                    Console.WriteLine("我 方 大 招  还剩CD：" + SkillCd);
                    AboutPrint.PrintLineFeed(2);
                    Console.WriteLine("敌 方 角 色  拥有HP:  " + EnemyHP);
                    AboutPrint.PrintLineFeed(2);
                    Console.WriteLine("1. 攻击   ");
                    Console.WriteLine("2. 防御   ");
                    Console.WriteLine("3. 技能   ");
                    Console.WriteLine("4. 背包   ");
                    Console.WriteLine("5. 逃跑    ");
                    Console.WriteLine("请输入你的选择");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            int OurAtk = turn(1);
                            if (isEnemyDEF == 1)//无法破防
                            {
                                if (OurAtk < EnemyDEF)
                                { Console.WriteLine("无 法 破 防 !"); }
                                else
                                {
                                    EnemyHP -= (OurAtk - EnemyDEF);
                                    if (OurAtk == this.ATK * 2)
                                    {
                                        AboutPrint.PrintUnderLine();
                                        AboutPrint.PrintLineFeed(3);
                                        Console.WriteLine(this.name + " 使用了 " + this.skill3 + "  对  " + "王永信" + " 造成 " + (OurAtk - EnemyDEF) + " 点 伤害");
                                        AboutPrint.PrintLineFeed(3);
                                        AboutPrint.PrintUnderLine();
                                        AboutPrint.PrintLineFeed(1);

                                    }

                                    else
                                    {
                                        AboutPrint.PrintUnderLine();
                                        AboutPrint.PrintLineFeed(3);
                                        Console.WriteLine(this.name + " 使用了 " + this.skill1 + "  对  " + "王永信" + " 造成 " + (OurAtk - EnemyDEF) + " 点 伤害");
                                        AboutPrint.PrintLineFeed(3);
                                        AboutPrint.PrintUnderLine();
                                        AboutPrint.PrintLineFeed(1);
                                    }


                                }//敌方扣血}
                                isEnemyDEF = 0;
                            }
                            else
                            {
                                EnemyHP -= OurAtk;
                                AboutPrint.PrintUnderLine();
                                AboutPrint.PrintLineFeed(3);
                                Console.WriteLine(this.name + " 使用了 " + this.skill1 + "   对   " + "王永信" + " 造 成 " + (OurAtk) + " 点 伤 害");
                                AboutPrint.PrintLineFeed(3);
                                AboutPrint.PrintUnderLine();
                                AboutPrint.PrintLineFeed(1);

                            }
                            break;
                        case 2:
                            turn(2);//我方进入防御状态
                            isIdefFlag = 1;
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintLineFeed(3);
                            Console.WriteLine(this.name + "   进入了  防御  状态");
                            AboutPrint.PrintLineFeed(3);
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintLineFeed(1);
                            break;
                        case 3:
                            if (SkillCd == 0)
                            {
                                int judgeTheBuff;
                                turn(3);
                                judgeTheBuff = this.skill(4);//三种大招三种状态
                                switch (judgeTheBuff)
                                {
                                    case 1:
                                        isTimeStop = 3;
                                        AboutPrint.PrintUnderLine();
                                        AboutPrint.PrintLineFeed(2);
                                        Console.WriteLine("The   World！");
                                        AboutPrint.PrintLineFeed(2);
                                        AboutPrint.PrintUnderLine();
                                        SkillCd = this.CD;
                                        break;
                                    case 2:
                                        isCrazy = 1;
                                        AboutPrint.PrintUnderLine();
                                        AboutPrint.PrintLineFeed(2);
                                        Console.WriteLine("Crazy   diamond");
                                        AboutPrint.PrintLineFeed(2);
                                        AboutPrint.PrintUnderLine();
                                        SkillCd = this.CD;
                                        isIbuffFlag = 1;
                                        break;
                                    case 3:
                                        isGold = 1;
                                        AboutPrint.PrintUnderLine();
                                        AboutPrint.PrintLineFeed(2);
                                        Console.WriteLine("Golden   Experience");
                                        AboutPrint.PrintLineFeed(2);
                                        AboutPrint.PrintUnderLine();
                                        SkillCd = this.CD;
                                        isIbuffFlag = 1;
                                        break;
                                    default:
                                        break;
                                }
                            }
                            break;
                        case 4:
                            AboutPrint.PrintUnderLine();
                            int IchoiceItem = returnTheItem();
                            switch (IchoiceItem)
                            {
                                case 1:
                                    MyHP += 10;
                                    AboutPrint.PrintLineFeed(1);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(2);
                                    Console.WriteLine(" 使用成功 ！" + "   角色目前生命值为   " + MyHP);
                                    AboutPrint.PrintLineFeed(2);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(1);
                                    break;
                                case 2:
                                    MyATK += 5;
                                    MyDEF += 5;
                                    AboutPrint.PrintLineFeed(1);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(2);
                                    Console.WriteLine(" 使用成功 ！" + "   角色目前攻击力为   " + MyATK);
                                    Console.WriteLine(" 使用成功 ！" + "   角色目前防御力为   " + MyDEF);
                                    AboutPrint.PrintLineFeed(2);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(1);
                                    break;
                                case 3:
                                    SkillCd -= 2;
                                    AboutPrint.PrintLineFeed(1);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(2);
                                    Console.WriteLine(" 使用成功 ！" + "  角色的技能CD为   " + SkillCd);
                                    AboutPrint.PrintLineFeed(2);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(1);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case 5:
                            TheEndFlag = 1;
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintLineFeed(3);
                            Console.WriteLine("( T A T) 凸");
                            Console.WriteLine("变强一点再来吧~");
                            AboutPrint.PrintLineFeed(3);
                            break;
                        default:
                            break;
                    }

                    if (EnemyHP < 0)
                    {
                        AboutPrint.PrintUnderLine();
                        AboutPrint.PrintUnderLine();
                        Console.WriteLine("JOJO你做到了");
                        Console.WriteLine("仅仅是经过几分钟的残酷试炼");
                        Console.WriteLine("你就变得如此强大了");
                        Console.WriteLine("不愧是你，不愧是unity组的");
                        AboutPrint.PrintUnderLine();
                        AboutPrint.PrintUnderLine();
                        Console.WriteLine("完结撒花    ✿✿ヽ(°▽°)ノ✿");
                        Console.WriteLine("谢谢陪伴    Thanks♪(･ω･)ﾉ");
                        Console.WriteLine("按任意键退出");
                        Console.ReadLine();
                        Process.GetCurrentProcess().Kill();
                        TheEndFlag = 1;
                    }
                }
                if (TheEndFlag == 1)
                {
                    if (MyHP < 0)
                    {//失败
                        AboutPrint.PrintUnderLine();
                        AboutPrint.PrintUnderLine();
                        AboutPrint.PrintLineFeed(3);
                        Console.WriteLine("( T A T) 凸");
                        Console.WriteLine("变强一点再来吧~");
                        AboutPrint.PrintLineFeed(3);
                        TheEndFlag = 1;
                    }
                    break;
                }
                else //敌方回合 turn
                {
                    BossCD--;
                    if (BossCD <= 0)
                    {
                        AboutPrint.PrintUnderLine();
                        AboutPrint.PrintLineFeed(2);
                        Console.WriteLine("王永信忍不住电击了自己！");
                        Console.WriteLine("永信 攻击力和防御力都提升了！");
                        AboutPrint.PrintUnderLine();
                        EnemyATK += 6;
                        EnemyDEF += 6;
                        BossCD = 2;
                    }
                    if (isTimeStop != 0)
                    {
                        isTimeStop--;
                    }//被时停
                    else
                    {
                        Random random2 = new Random();
                        int EnemyrandomSkill = random2.Next(1, 5);
                        if (EnemyrandomSkill == 1 || EnemyrandomSkill == 2)
                        {
                            if (isIdefFlag == 1)//无法破防
                            {
                                if (EnemyATK <= this.DEF)
                                {
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(3);
                                    Console.WriteLine("  无  法  破  防 ! ");
                                    AboutPrint.PrintLineFeed(3);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(1);
                                }
                                else
                                {
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(3);
                                    Console.WriteLine("王永信" + "对" + this.name + "造成" + (EnemyATK - this.DEF) + "点伤害");
                                    AboutPrint.PrintLineFeed(3);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(1);
                                    MyHP -= (EnemyATK - this.DEF);
                                    isIdefFlag = 0;
                                }//我方扣血

                            }

                            else
                            {
                                if (EnemyATK <= this.DEF)
                                {
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(3);
                                    Console.WriteLine("  无  法  破  防 ! ");
                                    AboutPrint.PrintLineFeed(3);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(1);
                                }
                                else
                                {
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(3);
                                    Console.WriteLine("王永信" + "  对  " + this.name + "  造成  " + EnemyATK + " 点  伤害");
                                    MyHP -= EnemyATK;
                                    AboutPrint.PrintLineFeed(3);
                                    AboutPrint.PrintUnderLine();
                                    AboutPrint.PrintLineFeed(1);
                                }//我方扣血
                            }
                        }
                        else
                        {
                            isEnemyDEF = 1;
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintLineFeed(3);
                            Console.WriteLine("  敌人" + "王永信" + "使用了  防御  " + "下回合将  抵消  " + EnemyDEF + " 点  伤害");
                            AboutPrint.PrintLineFeed(3);
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintLineFeed(1);
                        }
                        if (MyHP < 0)
                        {//失败
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintUnderLine();
                            AboutPrint.PrintLineFeed(3);
                            Console.WriteLine("( T A T) 凸");
                            Console.WriteLine("变强一点再来吧~");
                            AboutPrint.PrintLineFeed(3);
                            TheEndFlag = 1;
                        }
                        SkillCd--;
                    }
                }
            }
        }
        public int turn(int turnChioceNumber)//主角回合
        {
            switch (turnChioceNumber)
            {
                case 1:
                    Random random = new Random();
                    int randomSkill = random.Next(1, 5);
                    if (randomSkill == 1)
                    {
                        return this.skill(3);
                    }
                    else
                    {
                        return this.skill(1);
                    }
                case 2:
                    return this.skill(2);
                case 3://技能
                    return this.skill(4);
                default:
                    break;
            }
            return 0;
        }
        //判断是否升级的方法布尔类型
        public bool isLevelUp()
        {

            int i = 0;
            int BeforeLevel = this.LEVEL;
            foreach (int levels in Statics.Levels)
            {
                i++;
                if (i >= 10)
                    break;
                else
                if (this.EXPERIENCE >= Statics.Levels[i] && this.EXPERIENCE <= Statics.Levels[i + 1])
                { break;}
            }
            if (BeforeLevel < i)
            {
                this.LEVEL = i;
                this.ATK += (i-BeforeLevel)* 10;
                this.DEF += (i - BeforeLevel)*10;
                this.HP   += (i - BeforeLevel)*40; 
                return true;
            }
            else
                return false;
        }

        //获得经验的方法
        public int GainExperience(int GainExperiences)
        {
            this.EXPERIENCE += GainExperiences;
            Console.WriteLine("目前经验" + this.EXPERIENCE);
            return this.EXPERIENCE;
        }

        //显示角色的属性以及所有物（所有物未实现）
        public void PrintTheAtrributesOfRole()
        {
            AboutPrint.PrintUnderLine();
            AboutPrint.PrintLineFeed(1);
            Console.Write("角色：   ");
            Console.WriteLine(this.name);
            AboutPrint.PrintLineFeed(4);
            Console.Write("LEVEL：   ");
            Console.WriteLine(this.LEVEL);
            AboutPrint.PrintLineFeed(1);
            Console.Write("HP：      ");
            Console.WriteLine(this.HP);
            AboutPrint.PrintLineFeed(1);
            Console.Write("ATK：     ");
            Console.WriteLine(this.ATK);
            AboutPrint.PrintLineFeed(1);
            Console.Write("DEF：     ");
            Console.WriteLine(this.DEF);
            AboutPrint.PrintLineFeed(3);
            Console.WriteLine("SKILL：");
            Console.Write("1. " + this.skill1+"       ");
            Console.WriteLine("2. " + this.skill2+"   ");
            AboutPrint.PrintLineFeed(2);
            Console.Write("3. " + this.skill3+"   ");
            Console.WriteLine("4. " + this.skill4+ "   ");
            AboutPrint.PrintLineFeed(2);
            Console.WriteLine("按1退出");
            Console.ReadLine();
        }
    }

    #endregion
    #region 空条承太郎
    //角色：空条承太郎 
    public class Role1 : ROLES
    {

        public Role1()
        {
            this.skill3 = "欧拉欧拉欧拉";
            this.skill4 = "The World!!";
            this.name = " KujoJotaro";
            this.EXPERIENCE = 0;
            this.LEVEL = 0;
            this.HP = 20;
            this.ATK = 15;
            this.DEF = 4;
            this.CD = 10;
            this.PT = 1.0f;
            this.SPEED = 10;
        }
        public override int skill(int skillNumber)
        {
            switch (skillNumber)
            {
                //普通攻击待实现
                case 1:
                    return this.ATK;

                //防御待实现待实现
                case 2:
                    return this.DEF; ;
                //欧拉欧拉欧拉欧拉待实现
                case 3:
                    return this.ATK * 2;
                    ;
                //时停 
                case 4:
                    return 1;
                default:
                    Console.WriteLine("没有该技能");
                    return 0;
                    ;
            }
        }
    }
    #endregion
    #region 东方仗助
    //角色 东方仗助
    public class Role2 : ROLES
    {

        public Role2()
        {
            this.skill3 = "嘟啦嘟啦嘟啦嘟啦";
            this.skill4 = "Crazy diamond!!";
            this.name = " HigashikataJosuke";
            this.EXPERIENCE = 0;
            this.LEVEL = 0;
            this.HP = 35;
            this.ATK = 7;
            this.DEF = 7;
            this.CD = 2;
            this.PT = 0.75f;
            this.SPEED = 8;
        }
        public override int skill(int SkillNumber)
        {
            switch (SkillNumber)
            {
                //普通攻击  待实现
                case 1:
                    return this.ATK;

                //防御待实现   待实现
                case 2:
                    return this.DEF;

                //嘟啦嘟啦嘟啦嘟啦 待实现
                case 3:
                    return this.ATK * 2;

                // 疯狂钻石 等级五解锁
                case 4:
                    return 2;

                default:
                    Console.WriteLine("没有该技能");
                    return 0;

            }
        }
    }
    #endregion
    #region 乔鲁诺乔巴纳


    //角色 乔鲁诺乔巴纳 隐藏技 黄金体验镇魂曲 未实现
    public class Role3 : ROLES
    {

        public Role3()
        {
            this.skill3 = "木大木大木大";
            this.skill4 = "Gold Experience!";
            this.name = "GiornoGiovanna";
            this.EXPERIENCE = 0;
            this.LEVEL = 0;
            this.HP = 18;
            this.ATK = 5;
            this.DEF = 5;
            this.CD = 3;
            this.PT = 0.75f;
            this.SPEED = 5;
        }
        public override int skill(int SkillNumber)
        {
            switch (SkillNumber)
            {
                //普通攻击  待实现
                case 1:
                    return this.ATK;

                //防御待实现   待实现
                case 2:
                    return this.DEF;

                //木大木大木大木 待实现
                case 3:
                    return this.ATK * 2;

                // 黄金体验 等级五解锁
                case 4:
                    return 3;

                default:
                    Console.WriteLine("没有该技能");
                    return 0;

            }
        }
    }
    #endregion
    #region TeiKis
    public class Enemy
    {
        private int ATK;
        private int DEF;
        private int HP;
        private int SPEED;
        private string Name;
        public Enemy(int atk, int def, int hp, int speed, string Name)//攻击 防御 血量 名字
        {
            this.ATK = atk;
            this.DEF = def;
            this.HP = hp;
            this.SPEED = speed;
            this.Name = Name;
        }
        public int GetSpeed()
        {
            return this.SPEED;
        }
        public int GetDef()
        {
            return this.DEF;
        }
        public int GetATK()
        {
            return this.ATK;
        }
        public int GetHP()
        {
            return this.HP;
        }
        public string GetName()
        {
            return this.Name;
        }
    }
    public class BOSS : Enemy
    {
        private int cd = 4;
        public BOSS(int atk, int def, int hp, int speed, string Name, int cd)
            : base(atk, def, hp, speed, Name)
        {
            this.cd = cd;
        }
        public int GetCD()
        {
            return this.cd;
        }
    }
    #endregion
}


