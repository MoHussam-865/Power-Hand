using System.Globalization;
using System.Windows.Input;
using Power_Hand.Data.Other;
using Power_Hand.Features.FeatureApp.FeatureCasher.Channels;
using Power_Hand.Interfaces;
using Prism.Events;

namespace Power_Hand.Utils.ViewModels
{
    public class CalculatorVM: ViewModel
    {
        private string? _currentText;
        public string? CurrentText
        {
            get => _currentText;
            set { _currentText = value; OnPropertyChanged(); }
        }

        private DateTime? _time = null;
        private readonly IEventAggregator _eventAggregator;


        public ICommand OnKeyPressCommand { get; set; }
        public ICommand PhysicalKeyPressedCommand {  get; set; }

        public CalculatorVM(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
            OnKeyPressCommand = new ClickCommand<string>((x) => OnKeyPressed(x));
            PhysicalKeyPressedCommand = new ClickCommand<KeyEventArgs>(OnPhysicalKeyPressed);
        }

        /// <summary>
        /// this is responsible for capturing keyboard key press
        /// </summary>
        public void OnPhysicalKeyPressed(KeyEventArgs e)
        {
            //_keyEvent = e;
            if (e.Key == Key.Enter)
            {
                BarcodeHandler(true);
            }
            else if (e.Key == Key.Delete)
            {
                CurrentText = null;
            } 
            else if (e.Key == Key.OemComma || e.Key == Key.Decimal)
            {
                if (string.IsNullOrEmpty(_currentText))
                {
                    // if its null then add 0.
                    CurrentText = "0.";
                }
                // do nothing if it already has .
                else if (CurrentText != null && !_currentText.Contains('.'))
                {
                    CurrentText += ".";
                }
                BarcodeHandler();
            }
            else if (e.Key >= Key.D0 && e.Key <= Key.D9)
            {
                int number = e.Key - Key.D0;
                CurrentText += number.ToString();
                BarcodeHandler();
            }
            else if (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
            {
                int number = e.Key - Key.NumPad0;
                CurrentText += number.ToString();
            }
            else if (e.Key == Key.Back)
            {
                if (string.IsNullOrEmpty(_currentText)) return;

                var x = _currentText.ToList();
                x.RemoveAt(_currentText.Length - 1);
                var y = "";
                x.ForEach(x => y += x);
                CurrentText = y;
            }
        }

        /// <summary>
        /// this responsible for capturing the key values of Calculator.xaml
        /// user control UI element
        /// 
        /// </summary>
        private void OnKeyPressed(string key)
        {
            //if (_keyEvent != null && _keyEvent.Key == Key.Enter) return;

            if (IsNumber(key))
            {
                CurrentText += key;
            }
            else if (key == ".")
            {
                if (string.IsNullOrEmpty(_currentText))
                {
                    // if its null then add 0.
                    CurrentText = "0.";
                }
                // do nothing if it already has .
                else if (CurrentText != null && !_currentText.Contains('.'))
                {
                    CurrentText += key;
                }
            }
            // delete btn
            else if (key == "C")
            {
                CurrentText = null;
            }
            // backspace
            else if (key == "&lt;" || key == "<")
            {
                if (string.IsNullOrEmpty(_currentText)) return;
                
                var x = _currentText.ToList();
                x.RemoveAt(_currentText.Length - 1);
                var y = "";
                x.ForEach(x => y += x);
                CurrentText = y;
            } 
            
        }

        /// <summary>
        /// Check if the key pressed is a number
        /// </summary>
        private static bool IsNumber(string value)
        {
            try
            {
                double.Parse(value);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// is used by other view models to get the value
        /// 
        /// </summary>
        public double GetValue()
        {
            string? value = new(CurrentText);
            CurrentText = null;

            if (string.IsNullOrEmpty(value)) return 1;
            try
            {
                return double.Parse(value);
            }
            catch
            {
                return 1;
            }
        }


        private void BarcodeHandler(bool end = false)
        {
            if (!end && _time == null && CurrentText == null)
            {
                _time = DateTime.Now;
            }
            // the end of the number is called when the enter key is pressed
            else if (end && _time != null && CurrentText != null && CurrentText.Length > 3)
            {
                double time = (DateTime.Now - _time ?? new TimeSpan(2000)).TotalMilliseconds * CurrentText.Length;
                if (time < 500)
                {
                    // this is a Barcode
                    _eventAggregator.GetEvent<BarcodeChannel>().Publish(new(CurrentText));
                    CurrentText = null;
                    _time = null;
                }
            }
        }
    
    }
}
