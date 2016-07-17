using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Sappi
{
    /// <summary>
    /// Interaction logic for StartUp.xaml
    /// </summary>
    public partial class StartUp : UserControl
    {
        public StartUp()
        {
            InitializeComponent();
        }
        bool scaleButtonPressed = false;

        private void newAppButton_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow.Main.ContentArea.Content = new StudentForm();
            MainWindow.ChangeState(App.Page.StudentForm);
        }

        private void DatabaseButton_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow.Main.ContentArea.Content = new DatabaseView();
            MainWindow.ChangeState(App.Page.DatabaseView);
        }

        private void Scale(object sender, RoutedEventArgs e)
        {
            newAppButton.RenderTransformOrigin = new Point(0.5,0.5);
            if (scaleButtonPressed == false)
            {
                (sender as Button).Content = "Reset";

                ScaleTransform st = new ScaleTransform();
                st.ScaleX = 5;
                st.ScaleY = 5;

                //newAppButton.Template

                newAppButton.RenderTransform = st;
                scaleButtonPressed = true;
            }
            else
            {
                (sender as Button).Content = "Scale Up";

                ScaleTransform st = new ScaleTransform();
                st.ScaleX = 1;
                st.ScaleY = 1;

                newAppButton.RenderTransform = st;
                scaleButtonPressed = false;
            }
        }

        private void Image_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //if(((sender as Image).RenderTransform as ScaleTransform).ScaleX != 1 &&
            //   ((sender as Image).RenderTransform as ScaleTransform).ScaleY != 1)
            //{

            //}

            ////
            //Image img = (Image)sender;

            ////x
            //DoubleAnimation fadeInX = new DoubleAnimation( 5, new Duration(TimeSpan.FromSeconds(2)));
            //Storyboard.SetTarget(fadeInX, img);
            //Storyboard.SetTargetProperty(fadeInX, new PropertyPath(RenderTransformProperty.(ScaleTransform.ScaleXProperty));
            ////y
            //DoubleAnimation fadeInY = new DoubleAnimation( 5, new Duration(TimeSpan.FromSeconds(2)));
            //Storyboard.SetTarget(fadeInY, img);
            //Storyboard.SetTargetProperty(fadeInY, new PropertyPath(ScaleTransform.ScaleYProperty));

            //Storyboard sb = new Storyboard();
            //sb.Children.Add(fadeInX);
            //sb.Children.Add(fadeInY);
            //sb.Begin();
        }

        private void Image_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //Image img = (Image)sender;

            ////x
            //DoubleAnimation fadeOutX = new DoubleAnimation( 1, new Duration(TimeSpan.FromSeconds(2)));
            //Storyboard.SetTarget(fadeOutX, img);
            //Storyboard.SetTargetProperty(fadeOutX, new PropertyPath(ScaleTransform.ScaleXProperty));
            ////y
            //DoubleAnimation fadeOutY = new DoubleAnimation( 1, new Duration(TimeSpan.FromSeconds(2)));
            //Storyboard.SetTarget(fadeOutY, img);
            //Storyboard.SetTargetProperty(fadeOutY, new PropertyPath(ScaleTransform.ScaleYProperty));

            //Storyboard sb = new Storyboard();
            //sb.Children.Add(fadeOutX);
            //sb.Children.Add(fadeOutY);
            //sb.Begin();
        }
    }
}
