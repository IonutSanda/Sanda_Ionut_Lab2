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
            CommandBinding cmd1 = new CommandBinding();
            cmd1.Command = ApplicationCommands.Print;
            ApplicationCommands.Print.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Alt));
            cmd1.Executed += new ExecutedRoutedEventHandler(CtrlP_CommandHandler);
            this.CommandBindings.Add(cmd1);

            CommandBinding cmd2 = new CommandBinding();
            cmd2.Command = CustomComands.StopCommand.Launch;
            cmd2.Executed += new ExecutedRoutedEventHandler(CtrlS_CommandHandler);
            this.CommandBindings.Add(cmd2);
        }

        private void CtrlP_CommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("You have in stock: " + mRaisedGlazed + " Glazed, " + mRaisedSugar + " Sugar, " + mFilledChocolate + " Chocolate, " + mFilledLemon + " Lemon, " + mFilledVanilla + " Vanilla");
        }

        private void CtrlS_CommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("Ctrl + S was pressed! Your doughnut machine will stop!");
            this.stopToolMenuItem_Click(sender, e);
        }

        private DoughnutMachine myDoughnutMachine;
        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            myDoughnutMachine = new DoughnutMachine();
            myDoughnutMachine.DoughnutComplete += new DoughnutMachine.DoughnutCompleteDelegate(DoughnutCompleteHandler);

            cmbType.ItemsSource = PriceList;
            cmbType.DisplayMemberPath = "Key";
            cmbType.SelectedValuePath = "Value";

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
            this.Title = "Virtual Doughnut Factory";
            e.Handled = true;
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

        private void FilledItems_Click(object sender, RoutedEventArgs e)
        {
            string DoughnutFlavor;

            MenuItem SelectedItem = (MenuItem)e.OriginalSource;
            DoughnutFlavor = SelectedItem.Header.ToString();

            Enum.TryParse(DoughnutFlavor, out DoughnutType myFlavor);
            myDoughnutMachine.MakeDoughnuts(myFlavor);
        }

        private void FilledItemsShow_Click(object sender, RoutedEventArgs e)
        {
            string mesaj;

            MenuItem SelectedItem = (MenuItem)e.OriginalSource;
            mesaj = SelectedItem.Header.ToString() + " doughnuts are being cooked!";
            this.Title = mesaj;
        }

        //partea inferioara a fabricii

        KeyValuePair<DoughnutType, double>[] PriceList =
        {
            new KeyValuePair<DoughnutType, double>(DoughnutType.Sugar, 2.5),
            new KeyValuePair<DoughnutType, double>(DoughnutType.Glazed, 3),
            new KeyValuePair<DoughnutType, double>(DoughnutType.Lemon, 3.5),
            new KeyValuePair<DoughnutType, double>(DoughnutType.Chocolate, 4.5),
            new KeyValuePair<DoughnutType, double>(DoughnutType.Vanilla, 4),
        };

        DoughnutType selectedDoughnut;

        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtPrice.Text = cmbType.SelectedValue.ToString();
            KeyValuePair<DoughnutType, double> selectedEntry = (KeyValuePair<DoughnutType, double>)cmbType.SelectedItem;
            selectedDoughnut = selectedEntry.Key;
        }

        private int ValidateQuantity(DoughnutType selectedDoughnut)
        {
            int q = int.Parse(txtQuantity.Text);
            int r = 1;

            switch (selectedDoughnut)
            {
                case DoughnutType.Glazed:
                    if (q > mRaisedGlazed)
                        r = 0;
                    break;
                case DoughnutType.Sugar:
                    if (q > mRaisedSugar)
                        r = 0;
                    break;
                case DoughnutType.Chocolate:
                    if (q > mFilledChocolate)
                        r = 0;
                    break;
                case DoughnutType.Lemon:
                    if (q > mFilledLemon)
                        r = 0;
                    break;
                case DoughnutType.Vanilla:
                    if (q > mFilledVanilla)
                        r = 0;
                    break;

            }
            return r;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateQuantity(selectedDoughnut) > 0)
            {
                lstSale.Items.Add(txtQuantity.Text + " " + selectedDoughnut.ToString() + ":" + txtPrice.Text + " " + double.Parse(txtQuantity.Text) * double.Parse(txtPrice.Text));
            } else
            {
                MessageBox.Show("Cantitatea introdusa nu este in stoc!");
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            lstSale.Items.Remove(lstSale.SelectedItem);
        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            txtTotal.Text = (double.Parse(txtTotal.Text) + double.Parse(txtQuantity.Text) * double.Parse(txtPrice.Text)).ToString();

            foreach(string s in lstSale.Items)
            {
                switch(s.Substring(s.IndexOf(" ") + 1, s.IndexOf(":") - s.IndexOf(" ") - 1))
                {
                    case "Glazed":
                        mRaisedGlazed -= Int32.Parse(s.Substring(0, s.IndexOf(" ")));
                        txtGlazedRaised.Text = mRaisedGlazed.ToString();
                        break;
                    case "Sugar":
                        mRaisedSugar -= Int32.Parse(s.Substring(0, s.IndexOf(" ")));
                        txtSugarRaised.Text = mRaisedSugar.ToString();
                        break;
                    case "Chcolate":
                        mFilledChocolate -= Int32.Parse(s.Substring(0, s.IndexOf(" ")));
                        txtChocolateFilled.Text = mFilledChocolate.ToString();
                        break;
                    case "Vanilla":
                        mFilledVanilla -= Int32.Parse(s.Substring(0, s.IndexOf(" ")));
                        txtVanillaFilled.Text = mFilledVanilla.ToString();
                        break;
                    case "Lemon":
                        mFilledLemon -= Int32.Parse(s.Substring(0, s.IndexOf(" ")));
                        txtLemonFiled.Text = mFilledLemon.ToString();
                        break;
                }
            }
        }
    }
}
