using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace TestTextBoxWaterMark
{
    public class WateMarkTextBox : TextBox
    {
        private Label wateMarkLable;

        private ScrollViewer wateMarkScrollViewer;
        private string _lastText = "";
        private string lastText = "";
        static WateMarkTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WateMarkTextBox), new FrameworkPropertyMetadata(typeof(WateMarkTextBox)));
        }

        public WateMarkTextBox()
        {
            this.Loaded += new RoutedEventHandler(PerfectWateMarkTextBox_Loaded);
            this.TextChanged += new TextChangedEventHandler(PerfectWateMarkTextBox_TextChanged);
        }

        void PerfectWateMarkTextBox_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        void PerfectWateMarkTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            this.wateMarkLable.Content = WateMark;
        }

        void PerfectWateMarkTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.wateMarkLable.Visibility = Visibility.Hidden;
        }

        public string WateMark
        {
            get { return (string)GetValue(WateMarkProperty); }

            set { SetValue(WateMarkProperty, value); }
        }

        public static DependencyProperty WateMarkProperty =
            DependencyProperty.Register("WateMark", typeof(string), typeof(WateMarkTextBox), new UIPropertyMetadata("水印"));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.wateMarkLable = this.GetTemplateChild("TextPrompt") as Label;

            this.wateMarkScrollViewer = this.GetTemplateChild("PART_ContentHost") as ScrollViewer;
        }
        void PerfectWateMarkTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = Text;
            if (PrepareText(ref text))
            {
                Text = text;
                _lastText = Text;
            }
            else
            {
                Text = _lastText;
            }
            e.Handled = true;
            base.OnTextChanged(e);
            if(Text== lastText)
            base.SelectionStart = base.Text.Length;
            lastText = Text;
        }
        private bool PrepareText(ref string text)
        {
            int i = 0;
            if (text.Length > 5)
                return false;

            string t = "";

            foreach (char c in text)
            {

                if (c >= '0' && c <= '9')
                {
                    if (i < 4) t += c;
                    else if ((i == 4))
                    {
                        t += c;
                        if (Convert.ToUInt32(t) > 65535)
                            return false;
                    } 
                    else return false;

                }
                else
                {
                    return false;
                }
                i++;
            }

            if (t.Length > 5)
                return false;
            text = t;
            return true;
        }
    }


}
