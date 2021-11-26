using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sanda_Ionut_Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int mRaisedGlazed;
        private int mRaisedSugar;
        private int mFilledLemon;
        private int mFilledChocolate;
        private int mFilledVanilla;

        public MainWindow()
        {
            InitializeComponent();
        }

        private DoughnutMachine myDoughnutMachine;
        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            myDoughnutMachine = new DoughnutMachine();
            myDoughnutMachine.DoughnutComplete += new DoughnutMachine.DoughnutCompleteDelegate(DoughnutCompleteHandler);

        }

        private void glazedToolMenuItem_Click(object sender, RoutedEventArgs e)
        {
            glazedToolMenuItem.IsChecked = true;
            sugarToolMenuItem.IsChecked = false;
            myDoughnutMachine.MakeDoughnuts(DoughnutType.Glazed);
        }
        private void sugarToolMenuItem_Click(object sender, RoutedEventArgs e)
        {
            glazedToolMenuItem.IsChecked = false;
            sugarToolMenuItem.IsChecked = true;
            myDoughnutMachine.MakeDoughnuts(DoughnutType.Sugar);
        }
        private void lemonToolMenuItem_Click(object sender, RoutedEventArgs e)
        {
            lemonFilledMenuItem.IsChecked = true;
            chocolateFilledMenuItem.IsChecked = false;
            vanillaFilledMenuItem.IsChecked = false;
            myDoughnutMachine.MakeDoughnuts(DoughnutType.Lemon);
        }
        private void chocolateToolMenuItem_Click(object sender, RoutedEventArgs e)
        {
            lemonFilledMenuItem.IsChecked = false;
            chocolateFilledMenuItem.IsChecked = true;
            vanillaFilledMenuItem.IsChecked = false;
            myDoughnutMachine.MakeDoughnuts(DoughnutType.Chocolate);
        }
        private void vanillaToolMenuItem_Click(object sender, RoutedEventArgs e)
        {
            lemonFilledMenuItem.IsChecked = false;
            chocolateFilledMenuItem.IsChecked = false;
            vanillaFilledMenuItem.IsChecked = true;
            myDoughnutMachine.MakeDoughnuts(DoughnutType.Vanilla);
        }

        private void DoughnutCompleteHandler()
        {
            switch (myDoughnutMachine.Flavor)
            {
                case DoughnutType.Glazed:
                    mRaisedGlazed++;
                    txtGlazedRaised.Text = mRaisedGlazed.ToString();
                    break;
                case DoughnutType.Sugar:
                    mRaisedSugar++;
                    txtSugarRaised.Text = mRaisedSugar.ToString();
                    break;
                case DoughnutType.Lemon:
                    mFilledLemon++;
                    txtLemonFiled.Text = mFilledLemon.ToString();
                    break;
                case DoughnutType.Chocolate:
                    mFilledChocolate++;
                    txtChocolateFilled.Text = mFilledChocolate.ToString();
                    break;
                case DoughnutType.Vanilla:
                    mFilledVanilla++;
                    txtVanillaFilled.Text = mFilledVanilla.ToString();
                    break;
            }
        }

        private void stopToolMenuItem_Click(object sender, RoutedEventArgs e)
        {
            myDoughnutMachine.Enabled = false;
        }

        private void exitToolMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtQuantity_KeyPress(object sender, KeyEventArgs e)
        {
            if(!(e.Key>=Key.D0 && e.Key <= Key.D9))
            {
                MessageBox.Show("Numai cifre sunt permise", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtQuantity.Text = "0";
            }
        }
    }
}
