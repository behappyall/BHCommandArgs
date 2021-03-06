﻿using System;
using System.Windows;

namespace BeHappy.Args
{
    [Obsolete("Class is deprecated, please use Arg<int> from Generics instead.)")]
    public class ArgInt : Arg
    {
        int _value;
        int _default;
        public ArgInt(int value, string shortDesignation, string longDesignation, string help, string toolTip = "", bool required = false)
            : base(shortDesignation, longDesignation, help, toolTip, required)
        {
            this._value = value;
            this._default = value;
        }
        public void SetValue(int value)
        {
            this._value = value;
        }

        public void Restore()
        {
            this._value = this._default;
        }

        protected override void set(ref int i, string[] ps)
        {
            ++i;
            if (i < ps.Length)
            {
                try
                {
                    this._value = int.Parse(ps[i]);
                }
                catch
                {
                    string text = string.Format("wrong value for {0}/{1} argument: {2} ", this._shortDesignation, this._longDesignation, ps[i]);
                    if (Common.HasConsole())
                    {
                        Console.Error.WriteLine(text);
                        Environment.Exit(1);
                    }
                    else
                    {
                        int num = (int)MessageBox.Show(text, "Error");
                        Environment.Exit(1);
                    }
                }
            }
            else
            {
                string text = string.Format("there is no value for {0}/{1} argument", this._shortDesignation, this._longDesignation);
                if (Common.HasConsole())
                {
                    Console.Error.WriteLine(text);
                    Environment.Exit(1);
                }
                else
                {
                    MessageBox.Show(text, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Environment.Exit(1);
                }
            }
        }

        public static implicit operator int(ArgInt argString)
        {
            return argString._value;
        }

        public static implicit operator bool(ArgInt argString)
        {
            return argString._isUsed;
        }

        public override string ToString()
        {
            return _isUsed ? _value.ToString() : _isUsed.ToString();
        }
    }
}
