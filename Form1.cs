/* @author Jefim Holmgren
 * Slutprojekt Breakout
 * 2022/02/18 - 2022/04/10
 * 
 * Versionshistorik
 * 
 * Version 1 (V.7): 
 * Skapade ett temporärt spelplan.
 * Skapade tangentstyrning för paddeln.
 * 
 * Version 2 (V.8):
 * Bollen kan förflytta sig.
 * Paddeln kan styras med muspekaren.
 * Bollen kan byta sin riktning vid kollision med spelplanets gränser eller paddeln.
 * Bytte bollbilden till en hacka.
 * Stenblock kan placeras ut via kod.
 * Hackan kan ta bort stenblock vid kollision.
 * Spelar upp en ljudeffekt då hackan kolliderar med stenarna.
 * Implementerade en poängräknare för hur många block som spelaren har tagit sönder.
 * 
 * Version 3 (V.10):
 * Skapade en tidtagarur som sätts igång då spelet startas.
 * Skapade en temporär startmeny.
 * Skapade en temporär "Game Over" meny. 
 * Spelet avslutas då hackan kommer nedanför paddeln. 
 * Förbättrat kollisionen mellan hackan och stenblocken. 
 * Spelet kan nu upptäcka ifall alla stenblocken är förstörda för att kunna förtsätta till nästa nivå. 
 * 
 * Version 4 (V.11)
 * Utfallsvinkeln ändras beroende på vart på paddeln bollen hamnar.
 * Flera variationer av block kan nu slumpas ut beroende på sannolikhetsfaktorn för respektive blocktyp.
 * 
 * Version 5 (V.12)
 * Blocken visas endast efter användaren har interagerat med startknappen. 
 * Om hackan träffar den vänstra halvan av paddeln kommer hackan att studsa åt vänster, och tvärtom. 
 * Hackan kan endast slå sönder en block åt gången. 
 * Sista kolumnen av block brukade inte synas fullständigt. Men nu är de jämnt fördelade över spelfönstret. 
 * Spelfönstret har blivit smalare och längre. 
 * Nya block med nya positioner spawnas då alla blocken är förstörda.
 * 
 * Version 6 (V.13)
 * Hackan bytter riktningen i x-led då den kolliderar med blockens sidor på x-led. 
 * Guldblock (gula blocken) kan stå emot två slag, och diamantblock (blåa blocken) kan stå emot tre slag. 
 * När nivån höjs förflyttas blocken ett steg nedåt så att det uppstår mellanrum mellan taket och blocken. 
 * 
 * Version 7 (V.14)
 * Flera hackor kan nu förekomma i spelet. 
 * Skapade en omstarts funktionalitet.
 * Blocken fyller raden på ett oregelbundet sätt. 
 * Nu kan man välja ett namn.
 * Spelet kan spara en sorterad poänglista till en textfil och läsa av poänglistan från textfilen.
 * Ändrade paddeltexturen och färgerna på kanterna av paddeln.
 * Bytte menytexturer. 
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing.Text;

namespace JefimHolmgrenBreakout
{
    public partial class Form1 : Form
    {
        bool hasGameStarted = false;

        int pickaxeMovementSpeedX = 1;
        int pickaxeMovementSpeedY = 4;

        static int blockRows = 2;
        static int blockColumns = 7; 

        int playerScore = 0;
        int highScore = 0;
        int speedModifier = 0;
        int currentLevel = 1;

        int minutes = 0;
        int seconds = 0;

        int blockHeight = 64;
        int stoneblocksAmount = 0;
        int amountOfPickaxesSpawned = 0;

        int chanceForIron = 70;
        int chanceForGold = 25;
        int chanceForDiamond = 5;

        int chanceForPickaxe = 10;

        int livesForDiamond = 3;
        int livesForGold = 2;
        int livesForIron = 1;

        int finalBlockRowBottomPosition = 0;

        string playerName;

        bool hasCollidedWithABlock;

        PictureBox[,] block;
        List<PictureBox> pickaxes = new List<PictureBox>();
        List<int> pickaxeDX = new List<int>();
        List<int> pickaxeDY = new List<int>();
        List<Player> players = new List<Player>();
        List<string> highScoreData = new List<string>();

        int[,] blockLives = new int[blockRows, blockColumns];

        Random random = new Random();
        PrivateFontCollection pfc = new PrivateFontCollection();

        public Form1()
        {
            InitializeComponent();
            StartAllTimers(false);
            pickaxeDX.Add(pickaxeMovementSpeedX);
            pickaxeDY.Add(pickaxeMovementSpeedY);
        }

        /// <summary>
        /// Ger labels en importerad font. 
        /// </summary>
        private void AssignCustomFont()
        {
            rTBHighScoreList.Font = new Font(pfc.Families[0], 12, FontStyle.Regular);
            rTBPlayerName.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            lblHighScore.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            lblPlayerName.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            lblHighScore.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            lblGameOver.Font = new Font(pfc.Families[0], 24, FontStyle.Regular);
            lblScoreboard.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            lblInGameTime.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            lblScore.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
            lblGameOverName.Font = new Font(pfc.Families[0], 14, FontStyle.Regular);
        }

        /// <summary>
        /// Placerar ut block på spelplanet.
        /// </summary>
        private void CreateBlocks()
        {
            int newBlockWidth = 0;
            int totalBlockWidth;
            block = new PictureBox[blockRows, blockColumns];

            for (int x = 0; x < blockRows; x++)
            {
                totalBlockWidth = 0;

                for (int y = 0; y < blockColumns; y++)
                {
                    newBlockWidth = GetBlockWidth(y, totalBlockWidth);
                    totalBlockWidth += newBlockWidth;
                    stoneblocksAmount++;
                    blockLives[x, y] = 1;
                    block[x, y] = new PictureBox();
                    block[x, y].SizeMode = PictureBoxSizeMode.StretchImage;
                    block[x, y].Height = blockHeight;
                    block[x, y].Width = newBlockWidth;
                    block[x, y].Top = blockHeight * x;
                    block[x, y].Left = totalBlockWidth - newBlockWidth;
                    block[x, y].Image = Properties.Resources.stone_block;
                    block[x, y].BorderStyle = BorderStyle.Fixed3D;
                    block[x, y].BackColor = Color.Transparent;
                    this.Controls.Add(block[x, y]);
                    block[x, y].Visible = false;
                    block[x, y].BringToFront();
                }
            }
        }

        /// <summary>
        /// Avgör höjden på blocken.
        /// </summary>
        /// <param name="y">Blocket y-värde.</param>
        /// <param name="totalOccupiedWidth">Hur stor andel av bredden är ockuperad av blocken
        /// i pixlar.</param>
        /// <returns>Returnerar höjden på blocket.</returns>
        private int GetBlockWidth(int y, int totalOccupiedWidth)
        {
            int blockWidth = 0;
            int formWidth = this.Width;
            int maxBlockWidth = formWidth / blockColumns;
            int minBlockWidth = maxBlockWidth - 15;
            int lastBlock = blockColumns - 1;
            int widthLeft = formWidth - totalOccupiedWidth;

            if (y == lastBlock)
            { 
                blockWidth = widthLeft;
            }
            else
            {
                blockWidth = random.Next(minBlockWidth, maxBlockWidth);
            }

            return blockWidth;
        }

        /// <summary>
        /// Laddar poänglistan från textfilen eller skapar den om filen inte existerar när programmet startas.
        /// Dessutom ger labels en importerad font. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            pfc.AddFontFile("Minecraft.ttf");

            AssignCustomFont();

            if (File.Exists("highScore_list.txt"))
            {
                highScoreData = File.ReadAllLines("highScore_list.txt").ToList();
                LoadScoreListToPlayerList("highScore_list.txt", highScoreData);
            }
            else
            {
                File.Create("highScore_list.txt");
            }
        }

        /// <summary>
        /// Skapar en hacka. 
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        private void SpawnPickaxe(int posX, int posY)
        {
            int height = 32;
            int width = 32;
            int newlyAddedPickaxe;

            pickaxes.Add(new PictureBox());
            newlyAddedPickaxe = pickaxes.Count - 1;
            pickaxes[newlyAddedPickaxe].Width = width;
            pickaxes[newlyAddedPickaxe].Height = height;
            pickaxes[newlyAddedPickaxe].SizeMode = PictureBoxSizeMode.StretchImage;
            pickaxes[newlyAddedPickaxe].Image = Properties.Resources.pickaxe;
            pickaxes[newlyAddedPickaxe].BackColor = Color.Transparent;
            pickaxes[newlyAddedPickaxe].Left = posX;
            pickaxes[newlyAddedPickaxe].Top = posY;
            Controls.Add(pickaxes[newlyAddedPickaxe]);
            pickaxes[newlyAddedPickaxe].BringToFront();

            pickaxeDX.Add(1);
            pickaxeDY.Add(pickaxeMovementSpeedY);
        }

        /// <summary>
        /// Visar eller gömmer de skapade blocken. 
        /// </summary>
        private void HideBlocks(bool hideBlocks)
        {
            for (int x = 0; x < blockRows; x++)
            {
                for (int y = 0; y < blockColumns; y++)
                {
                    if (!hideBlocks)
                    {
                        block[x, y].Visible = true;
                    }
                    else
                    {
                        block[x, y].Visible = false;
                    }
                }
            }
        }

        /// <summary>
        /// Skapar mellanrum mellan taket och första blockraden. 
        /// </summary>
        private void ResetBlocksYPosition(bool resetPos)
        {
            for (int x = 0; x < blockRows; x++)
            {
                for (int y = 0; y < blockColumns; y++)
                {
                    if (resetPos)
                    {
                        block[x, y].Top = blockHeight * x;
                    }
                    else if (!resetPos)
                    {
                        block[x, y].Top = (blockHeight * ((currentLevel - 1) + x));
                    }

                    if (x == blockRows - 1)
                    {
                        finalBlockRowBottomPosition = block[x, y].Bottom;
                    }
                }
            }
        }

        /// <summary>
        /// Placerar ut olika varianter av block beroende på sannolikhetsfaktorn för respektive block.
        /// </summary>
        private void SpawnDifferentBlockTypes()
        {
            int randomFactor;

            for (int x = 0; x < blockRows; x++)
            {
                for (int y = 0; y < blockColumns; y++)
                {
                    randomFactor = random.Next(1, 101);

                    if(randomFactor >= 0 && randomFactor <= chanceForDiamond)
                    {
                        block[x, y].Image = Properties.Resources.diamond_block;
                        blockLives[x, y] = livesForDiamond;
                    }
                    else if(randomFactor > 10 && randomFactor <= chanceForGold)
                    {
                        block[x, y].Image = Properties.Resources.gold_block;
                        blockLives[x, y] = livesForGold;
                    }
                    else if(randomFactor > chanceForGold && randomFactor <= chanceForIron)
                    {
                        block[x, y].Image = Properties.Resources.iron_block;
                        blockLives[x, y] = livesForIron;
                    }
                }
            }
        }

        /// <summary>
        /// Hanterar bollens (hackans) kollision med stenblocken.
        /// </summary>
        private void HandleBlockCollision()
        {
            bool hasSpawnedPickaxes;
            for (int x = 0; x < blockRows; x++)
            {
                hasCollidedWithABlock = false;
                hasSpawnedPickaxes = false;
                for (int y = 0; y < blockColumns; y++)
                {
                    if (!hasCollidedWithABlock)
                    {
                        for (int i = 0; i < pickaxes.Count; i++)
                        {
                            if (pickaxes[i].Bounds.IntersectsWith(block[x, y].Bounds) && 
                                block[x, y].Visible)
                            {
                                HandleDirectionalChangeOnCollision(x, y, i);
                                HandlePickaxeSpawning(x, y, hasSpawnedPickaxes);
                                blockLives[x, y]--;
                                PlaySoundEffect("mining-sound-effect.wav");
                                if (blockLives[x, y] <= 0)
                                {
                                    OnBlockDestroyed(x, y);
                                }
                                else
                                {
                                    AddScore(1);
                                }
                                hasCollidedWithABlock = true;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Om ett hackan kolliderar med ett blick vid sidan så studsar den åt andra hållet. Om 
        /// hackan kolliderar på udersidan av blocket studsar den nedåt. 
        /// </summary>
        /// <param name="blockX">X-värdet på blocket som hackan kolliderade med.</param>
        /// <param name="blockY">Y-värdet på blocket som hackan kolliderade med.</param>
        /// <param name="pickaxeIndex">Indexet på hackan som kolliderade med blocken.</param>
        private void HandleDirectionalChangeOnCollision(int blockX, int blockY, int pickaxeIndex)
        {
            if (pickaxes[pickaxeIndex].Top >= (block[blockX, blockY].Top + 60))
            {
                pickaxeDY[pickaxeIndex] *= -1;
            }
            else
            {
                pickaxeDX[pickaxeIndex] *= -1;
            }
        }

        /// <summary>
        /// Lägger till poäng, gömmer det förstörda blocket och subtraherar den totala mängden
        /// levande block vid hackans kollision med blocket. 
        /// </summary>
        /// <param name="blockX">X-värdet på blocket som hackan kolliderade med.</param>
        /// <param name="blockY">Y-värdet på blocket som hackan kolliderade med.</param>
        private void OnBlockDestroyed(int blockX, int blockY)
        {
            AddScore(2);
            block[blockX, blockY].Visible = false;
            stoneblocksAmount--;
        }

        /// <summary>
        /// Hanterar skapandet av hackan vid den existerande hackans kollision med blocken.
        /// </summary>
        /// <param name="blockX">X-värdet på blocket som hackan kolliderade med.</param>
        /// <param name="blockY">Y-värdet på blocket som hackan kolliderade med.</param>
        /// <param name="hasSpawnedPickaxes">Har en hacka redan skapats?</param>
        private void HandlePickaxeSpawning(int blockX, int blockY, bool hasSpawnedPickaxes)
        {
            int randomFactor = random.Next(1, 101);

            if (!hasSpawnedPickaxes)
            {
                if (randomFactor >= 1 && randomFactor <= chanceForPickaxe)
                {
                    if (amountOfPickaxesSpawned < currentLevel)
                    {
                        SpawnPickaxe(block[blockX, blockY].Left + 16, block[blockX, blockY].Top);
                        amountOfPickaxesSpawned++;
                        hasSpawnedPickaxes = true;
                    }
                }
            }
        }

        /// <summary>
        /// Spelar upp en ljudfil
        /// </summary>
        /// <param name="fileName">Namnet på ljudfilen som ska spelas upp</param>
        private void PlaySoundEffect(string fileName)
        {
            SoundPlayer soundEffect = new SoundPlayer(fileName);
            soundEffect.Play();
        }

        /// <summary>
        /// Läser av tangentryckningar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            PaddelKeyControlls(e);
        }

        /// <summary>
        /// Hanterar paddle styrning med tangentknappar.
        /// </summary>
        /// <param name="e"></param>
        private void PaddelKeyControlls(KeyEventArgs e)
        {
            int dX = 10;
            int rightBorder = panelGameArea.Width - pbPaddel.Width;

            if (e.KeyCode == Keys.Left && pbPaddel.Left > 0)
            {
                pbPaddel.Left -= dX;
            }
            else if (e.KeyCode == Keys.Right && pbPaddel.Left <= rightBorder)
            {
                pbPaddel.Left += dX;
            }
        }

        /// <summary>
        /// Förflyttar bollen
        /// </summary>
        private void HandleBallMovement()
        {
            for (int i = 0; i < pickaxes.Count; i++)
            {
                pickaxes[i].Left += pickaxeDX[i];
                pickaxes[i].Top += pickaxeDY[i];
            }
        }

        /// <summary>
        /// Lägger till poäng och uppdaterar poängtexten.
        /// </summary>
        private void AddScore(int amount)
        {
            playerScore += amount;
            lblScore.Text = "Score: " + playerScore.ToString();
        }

        /// <summary>
        /// Ändrar bollens (hackans) riktning vid kollision med spelplanets gränser.
        /// </summary>
        private void HandleBallBorderCollisions()
        {
            for (int i = 0; i < pickaxes.Count; i++)
            {
                if (pickaxes[i].Bounds.IntersectsWith(pbPaddel.Bounds))
                {
                    pickaxeDY[i] = -pickaxeDY[i];
                    pickaxeDX[i] = GetXSpeed(i);
                }

                if (pickaxes[i].Right > panelGameArea.Width || pickaxes[i].Left < 0)
                {
                    pickaxeDX[i] = -pickaxeDX[i];
                }
                else if (pickaxes[i].Top < 0)
                {
                    pickaxeDY[i] = -pickaxeDY[i];
                }
                else if (pickaxes[i].Top > panelGameArea.Height)
                {
                    OnGameOver();
                }
            }
        }

        /// <summary>
        /// Avgör hackans utfallsvinkel.
        /// </summary>
        /// <returns></returns>
        private int GetXSpeed(int pickaxeIndex)
        {
            int newXSpeed = 0;
            int paddleMiddlePoint = pbPaddel.Left + (pbPaddel.Width / 2);
            int ballMiddlePoint = pickaxes[pickaxeIndex].Left + (pickaxes[pickaxeIndex].Width / 2);

            if (ballMiddlePoint >= (paddleMiddlePoint - 10) && ballMiddlePoint <= (paddleMiddlePoint + 10))
            {
                newXSpeed = 1 + speedModifier;
            }
            else if (ballMiddlePoint >= (paddleMiddlePoint - 40) && ballMiddlePoint <= (paddleMiddlePoint + 40))
            {
                newXSpeed = 4 + speedModifier;
            }
            else if (ballMiddlePoint >= (paddleMiddlePoint - 70) && ballMiddlePoint <= (paddleMiddlePoint + 70))
            {
                newXSpeed = 6 + speedModifier;
            }

            if (IsXDirectionNegative(pickaxeIndex))
            {
                newXSpeed *= -1;
            }

            return newXSpeed;
        }

        /// <summary>
        /// Avgör ifall x-värdet är negativt för respektive hacka. 
        /// </summary>
        /// <param name="pickaxeIndex">Index på hackan som ska kontrolleras</param>
        /// <returns></returns>
        private bool IsXDirectionNegative(int pickaxeIndex)
        {
            int paddleMiddlePoint = pbPaddel.Left + (pbPaddel.Width / 2);
            int ballMiddlePoint = pickaxes[pickaxeIndex].Left + (pickaxes[pickaxeIndex].Width / 2);
            bool isNegative = false;

            if (ballMiddlePoint < paddleMiddlePoint)
            {
                isNegative = true;
            }
            else if (ballMiddlePoint > paddleMiddlePoint)
            {
                isNegative = false;
            }

            return isNegative;
        }

        /// <summary>
        /// Avslutar spelet då hackan kommer utanför spelplanets gränser.
        /// </summary>
        private void OnGameOver()
        {
            PlaySoundEffect("death.wav");
            lblGameOverName.Text = "Name: " + playerName;
            hasGameStarted = false;
            lblInGameTime.Text = "Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
            panelGameOver.Visible = true;
            pbPaddel.Visible = false;

            HideBlocks(true);
            StartAllTimers(false);
            panelGameOver.BringToFront();

            UpdateHighScore();
            SaveScoreToList();
            SortHighScoreList();
            DisplayHighScoreList();
            WriteScoreToFile("highScore_list.txt");

            foreach (var pickaxe in pickaxes)
            {
                RemovePictureBox(pickaxe);
            }
        }

        /// <summary>
        /// Sparar namnet till spelarlistan.
        /// </summary>
        private void SaveScoreToList()
        {
            int currentPlayerHighScore = highScore;
            int playerIndex = 0;
            bool containsName = false;

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].name == playerName)
                {
                    playerIndex = i;
                    containsName = true;
                }
            }

            if (containsName)
            {
                if(players[playerIndex].highScore <= currentPlayerHighScore)
                {
                    players[playerIndex].highScore = currentPlayerHighScore;
                }
            }
            else
            {
                players.Add(new Player(playerName, currentPlayerHighScore));
            }
        }

        /// <summary>
        /// Lagrar poänglistan i en textfil.
        /// </summary>
        /// <param name="fileName">Namnet på textfilen där data ska lagras.</param>
        private void WriteScoreToFile(string fileName)
        {
            List<string> newHighScoreData = new List<string>();
            string line;

            foreach (var player in players)
            {
                line = player.name + ": " + player.highScore;
                newHighScoreData.Add(line);
            }

            File.WriteAllLines(fileName, newHighScoreData);
        }

        /// <summary>
        /// Laddar poänglistan från den sparade textfilen. 
        /// </summary>
        /// <param name="fileName">Namnet på filen som data ska läsas ifrån.</param>
        private void LoadScoreListToPlayerList(string fileName, List<string> lines)
        {
            int indexOfColon;
            string name;
            int highScore;

            for (int i = 0; i < highScoreData.Count; i++)
            {
                indexOfColon = lines[i].IndexOf(":");
                name = lines[i].Substring(0, indexOfColon);
                highScore = int.Parse(highScoreData[i].Substring(indexOfColon + 1));

                players.Add(new Player(name, highScore));
            }
        }

        /// <summary>
        /// Sorterar poänglistan. 
        /// </summary>
        /// <param name="highScoreList">Poänglistan som ska sorteras.</param>
        private void SortHighScoreList()
        {
            int temporaryHighScore;
            string temporaryName;

            for (int i = 0; i < players.Count - 1; i++)
            {
                for (int j = 0; j < players.Count - (1 + i); j++)
                {
                    if (players[j].highScore < players[j + 1].highScore)
                    {
                        temporaryHighScore = players[j + 1].highScore;
                        temporaryName = players[j + 1].name;

                        players[j + 1].highScore = players[j].highScore;
                        players[j].highScore = temporaryHighScore;

                        players[j + 1].name = players[j].name;
                        players[j].name = temporaryName;
                    }
                }
            }
        }

        /// <summary>
        /// Ritar ut poänglistan på RichTextBoxen. 
        /// </summary>
        private void DisplayHighScoreList()
        {
            rTBHighScoreList.Text = "";

            foreach (var player in players)
            {
                rTBHighScoreList.AppendText(player.name + ": " + player.highScore + "\r\n");
            }
        }

        /// <summary>
        /// Raderar en picturebox-element. 
        /// </summary>
        /// <param name="pB">PictureBox-elementet som ska raderas.</param>
        private void RemovePictureBox(PictureBox pB)
        {
            this.Controls.Remove(pB);
        }

        /// <summary>
        /// Uppdaterar högsta poängen som användaren lyckades att uppnå. 
        /// </summary>
        private void UpdateHighScore()
        {
            if (playerScore >= highScore)
            {
                highScore = playerScore;
            }

            lblHighScore.Text = "High score: " + highScore.ToString();
        }

        /// <summary>
        /// Påbörjar eller avslutar alla timers som finns i spelet.
        /// </summary>
        /// <param name="startTimers">Starta eller stäng av timers.</param>
        private void StartAllTimers(bool startTimers)
        {
            if (startTimers)
            {
                timerInGameTime.Start();
                timerGameLoop.Start();
            }
            else if (!startTimers)
            {
                timerInGameTime.Stop();
                timerGameLoop.Stop();
            }
        }

        /// <summary>
        /// Spelloop.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerGameLoop_Tick(object sender, EventArgs e)
        {
            if (hasGameStarted)
            {
                HandleBallMovement();
                HandleBallBorderCollisions();
                HandleBlockCollision();
                HandleLevels();
            }
        }

        /// <summary>
        /// Nollställer blockens textur till ursprungstextur.
        /// </summary>
        private void ResetOres()
        {
            for (int x = 0; x < blockRows; x++)
            {
                for (int y = 0; y < blockColumns; y++)
                {
                    block[x, y].Image = Properties.Resources.stone_block;
                }
            }
        }

        /// <summary>
        /// Hanter nivåskiftning. 
        /// </summary>
        private void HandleLevels()
        {
            bool hasSpawnedNewBlocks = false;

            if(!hasSpawnedNewBlocks && stoneblocksAmount <= 0)
            {
                hasGameStarted = false;
                currentLevel++;
                speedModifier += 1;
                ResetOres();
                SpawnDifferentBlockTypes();
                if (currentLevel < 4)
                {
                    ResetBlocksYPosition(false);
                }
                HideBlocks(false);
                ResetPickaxes(200, 20);
                stoneblocksAmount = blockRows * blockColumns;
                hasSpawnedNewBlocks = true;
                hasGameStarted = true;
            }
        }

        /// <summary>
        /// Nollställer hackornas position och hastighet då leveln höjs.
        /// </summary>
        /// <param name="startXPos">Startposition på x-ledet.</param>
        /// <param name="gapBetweenPickaxes">Mellanrum mellan hackorna</param>
        private void ResetPickaxes(int startXPos, int gapBetweenPickaxes)
        {
            for (int i = 0; i < pickaxes.Count; i++)
            {
                pickaxeDY[i] = pickaxeMovementSpeedY;
                pickaxeDX[i] = pickaxeMovementSpeedX;
                pickaxes[i].Top = finalBlockRowBottomPosition + 20;
                pickaxes[i].Left = startXPos + pickaxes[i].Width;
                startXPos += gapBetweenPickaxes;
            }
        }

        /// <summary>
        /// Tidtagarur.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerInGameTime_Tick(object sender, EventArgs e)
        {
            seconds++;

            if (seconds >= 60)
            {
                minutes++;
                seconds = 0;
            }
        }

        /// <summary>
        /// Påbörjar spelet då användaren interagerar med startknappen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            CreateBlocks();
            PlaySoundEffect("button-click.wav");
            SpawnDifferentBlockTypes();
            HandlePlayerNaming();
            SpawnPickaxe((Width / 2) - 32, 300);
            lblScore.BringToFront();

            HideBlocks(false);
            StartAllTimers(true);

            panelStart.Visible = false;
            lblScore.Visible = true;
            hasGameStarted = true;
            pbPaddel.Visible = true;
        }

        /// <summary>
        /// Kontrollerar ifall spelarens namn innehåller bokstäver, siffror eller understräck. 
        /// Ifall namnet inte gör det sätts namnet till "Anonymous user".
        /// </summary>
        private void HandlePlayerNaming()
        {
            playerName = rTBPlayerName.Text;

            if (!Regex.IsMatch(playerName, @"^[a-zA-Z0-9_]+$"))
            {
                playerName = "Anonymous user";
            }
        }

        /// <summary>
        /// Startar om spelet vid interaktion med omstart knappen. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestart_Click(object sender, EventArgs e)
        {
            StartAllTimers(true);
            HideBlocks(false);
            PlaySoundEffect("button-click.wav");

            ResetBlocksYPosition(true);
            panelGameArea.Visible = true;
            panelGameOver.Visible = false;
            hasGameStarted = true;
            pbPaddel.Visible = true;

            ResetGameVariables();

            SpawnPickaxe(50, 300);
        }

        /// <summary>
        /// Nollställer spelprogressionen. 
        /// </summary>
        private void ResetGameVariables()
        {
            currentLevel = 1;
            speedModifier = 0;
            playerScore = 0;
            seconds = 0;
            //Nolställer poängtexten. 
            AddScore(0);
            amountOfPickaxesSpawned = 0;

            pickaxes.Clear();
            pickaxeDX.Clear();
            pickaxeDY.Clear();
        }

        /// <summary>
        /// Hanterar paddelstyrning med hjälp av muspekaren.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelGameArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (hasGameStarted)
            {
                pbPaddel.Left = e.X - (pbPaddel.Width / 2);

                if (pbPaddel.Left >= (panelGameArea.Width - pbPaddel.Width))
                {
                    pbPaddel.Left = panelGameArea.Width - pbPaddel.Width;
                }
                else if (pbPaddel.Left <= 0)
                {
                    pbPaddel.Left = 0;
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
