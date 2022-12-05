using System;
using System.ComponentModel;

namespace MicroRenameLogic.DLG
{
    public class NamingData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region Defaults
        private const string DEFAULTPREFIX = "Prefix_";
        private const string DEFAULTSUFFIX = "_suffix";
        private const int DEFAULTINDEX = 1;
        private const int DEFAULTDIGITS = 4;
        private const int DEFAULTINCREMENT = 1;

        private const bool DEFAULTKEEPEXT = true;
        private const int DEFAULTMAXINDEX = 9999;
        #endregion

        #region Prefix
        private string prefix = DEFAULTPREFIX;
        public string Prefix
        {
            get => prefix;
            set
            {
                prefix = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Prefix)));
            }
        }
        #endregion

        #region Suffix
        private string suffix = DEFAULTSUFFIX;
        public string Suffix
        {
            get => suffix;
            set
            {
                suffix = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Suffix)));
            }
        }
        #endregion

        #region Extension
        private string extension;
        public string Extension
        {
            get => extension;
            set
            {
                extension = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Extension)));
            }
        }
        #endregion

        #region Index
        private int index = DEFAULTINDEX;
        public int Index
        {
            get => index;
            set
            {
                index = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Index)));
            }
        }
        #endregion

        #region Digits
        private int digits = DEFAULTDIGITS;
        public int Digits
        {
            get => digits;
            set
            {
                if (value < 1) value = 1;
                if (value > 9) value = 9;

                digits = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Digits)));

                MaxIndex = (int)Math.Pow(10, digits) - 1;
            }
        }
        #endregion

        #region MaxValue
        private int maxIndex = DEFAULTMAXINDEX;
        public int MaxIndex
        {
            get => maxIndex;
            set
            {
                maxIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MaxIndex)));
            }
        }
        #endregion

        #region Increment
        private int increment = DEFAULTINCREMENT;
        public int Increment
        {
            get => increment;
            set
            {
                if (value < 1) value = 1;
                if (value > 9) value = 9;

                increment = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Increment)));
            }
        }
        #endregion

        #region KeepExtension
        private bool keepExtension = DEFAULTKEEPEXT;
        public bool KeepExtension
        {
            get => keepExtension;
            set
            {
                keepExtension = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(KeepExtension)));
            }
        }
        #endregion

        public void Reset()
        {
            Prefix = DEFAULTPREFIX;
            Suffix = DEFAULTSUFFIX;
            Index = DEFAULTINDEX;
            Digits = DEFAULTDIGITS;
            Increment = DEFAULTINCREMENT;

            KeepExtension = DEFAULTKEEPEXT;
        }
    }
}
