using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MathGame
{
	using System.Windows.Threading;
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		DispatcherTimer timer= new DispatcherTimer();
		int tenthsOfSecondsElapsed;
		int matchesFound;

		public MainWindow()
		{
			InitializeComponent();
			timer.Interval = TimeSpan.FromSeconds(.1);
			timer.Tick += Timer_Tick;
			SetUpGame();
		}

		private void SetUpGame()
		{
			List<string> animalEmoji = new List<string>
			{
				"🐅","🐅",
				"🐆","🐆",
				"🐄","🐄",
				"🐪","🐪",
				"🐁","🐁",
				"🐦","🐦",
				"🐕","🐕",
				"🐍","🐍"
			};

			Random rand = new Random();
			foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
			{
				if (textBlock.Name != "Time"){
					textBlock.Visibility = Visibility.Visible;
					int index = rand.Next(animalEmoji.Count);
					string nextEmoji = animalEmoji[index];
					textBlock.Text = nextEmoji;
					animalEmoji.RemoveAt(index);
				}
			}
			timer.Start();
			tenthsOfSecondsElapsed = 0;
			matchesFound = 0;
		}

		TextBlock lastTextBlockClicked;
		bool findingMatch = false;

		private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
		{
			TextBlock textBlock = sender as TextBlock; 
			if (findingMatch == false)
			{
				textBlock.Visibility = Visibility.Hidden;
				lastTextBlockClicked = textBlock;
				findingMatch = true;
			}
			else if (textBlock.Text == lastTextBlockClicked.Text)
			{
				matchesFound++;
				textBlock.Visibility = Visibility.Hidden;
				findingMatch = false;
			}
			else
			{
				lastTextBlockClicked.Visibility = Visibility.Visible;
				findingMatch = false;
			}
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			tenthsOfSecondsElapsed++;
			Time.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
			if (matchesFound == 8)
			{
				timer.Stop();
				Time.Text += " - Play Again?";
			}
		}

		private void Time_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (matchesFound == 8)
			{
				SetUpGame();
			}
		}

		private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
		{

		}
	}
}
