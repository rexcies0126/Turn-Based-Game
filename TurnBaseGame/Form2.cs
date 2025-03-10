using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using WMPLib;

namespace TurnBaseGame
{
    public partial class Form2 : Form
    {
        private Player player1 = new Player("Charizard", 100, 10, 3,60);
        private AIPlayer player2 = new AIPlayer("Gengar", 100, 10, 3 ,60);
        private Player currentPlayer;
        private Player opponent;

        private WindowsMediaPlayer mediaPlayer;
        public Form2()
        {
            InitializeComponent();
            InitializeGame();
            mediaPlayer = new WindowsMediaPlayer();
            PlayBackground();
        }

        private void PlayBackground()
        {
            SoundPlayer player = new SoundPlayer(Properties.Resources.battle_rival);
            player.PlayLooping();
        }

        private async Task ShowAttackEffect(PictureBox targetPictureBox)
        {
            mediaPlayer.URL = "C:\\Users\\Rexcies Valentin\\source\\repos\\TurnBaseGame\\TurnBaseGame\\Resources\\slash1-94367.wav";
            mediaPlayer.controls.play();

            Image originalImage = targetPictureBox.Image;
            targetPictureBox.Image = Properties.Resources.slash;

            await Task.Delay(500);
            targetPictureBox.Image = originalImage;
            mediaPlayer.controls.play();
        }

        private async Task ShowSkillEffect(PictureBox targetPictureBox)
        {
            mediaPlayer.URL = "C:\\Users\\Rexcies Valentin\\source\\repos\\TurnBaseGame\\TurnBaseGame\\Resources\\slash1-94367.wav";
            mediaPlayer.controls.play();

            Image originalImage = targetPictureBox.Image;
            targetPictureBox.Image = Properties.Resources.flame;

            await Task.Delay(500);
            targetPictureBox.Image = originalImage;
            mediaPlayer.controls.play();
        }

        private void InitializeGame()
        {
            currentPlayer = player1;
            opponent = player2;
            progressBar1.Maximum = progressBar2.Maximum = 100;
            progressBar3.Maximum = progressBar4.Maximum = 60;   
            UpdateUI();
        }

        private void UpdateUI()
        {
            progressBar1.Value = player1.Health;
            progressBar2.Value = player2.Health;
            if (opponent.Mana >= 60)
            {
                progressBar3.Value = progressBar4.Value = 60;
            }
            else
            {
                progressBar3.Value = player1.Mana;
                progressBar4.Value = player2.Mana;
            }
        }

        private async void SwapTurns()
        {
            Player temp = currentPlayer;
            currentPlayer = opponent;
            opponent = temp;
            label1.Text = $"{currentPlayer.Name}'s Turn";

            if (currentPlayer is AIPlayer aIPlayer)
            {      
                if (aIPlayer.TakeTurn(opponent))
                {
                    await Task.Delay(500);
                    await ShowAttackEffect(pictureBox2);
                }
                else
                {
                    await Task.Delay(1000);
                    await ShowSkillEffect(pictureBox2);
                }
                UpdateUI();
                if (opponent.Health <= 0)
                {
                    MessageBox.Show($"{currentPlayer.Name}'s win", "Game Over");
                    InitializeGame();
                    return;
                }
                SwapTurns();
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await ShowAttackEffect(pictureBox1);
            if (currentPlayer.AttackPlayer(opponent))
            {
                UpdateUI();
                if (opponent.Health <= 0)
                {
                    MessageBox.Show($"{currentPlayer.Name}'s win", "Game Over");
                    InitializeGame();
                    return;
                }
            }
            else
            {
                MessageBox.Show($"{currentPlayer.Name} missed the attack", "Miss");
            }
            SwapTurns();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            switch (currentPlayer.UseSkill(opponent))
            {
                case 1:
                    await ShowSkillEffect(pictureBox1);
                    UpdateUI();
                    if (opponent.Health <= 0)
                    {
                        MessageBox.Show($"{currentPlayer.Name}'s win", "Game Over");
                        InitializeGame();
                        return;
                    }
                    break;
                case 2:
                    MessageBox.Show($"{currentPlayer.Name} missed the skill", "Miss");
                    break;
                default:
                    MessageBox.Show("Not enough mana to use skill!", "Skill Fail");
                    break;
            }
            SwapTurns();
        }
    }
}
