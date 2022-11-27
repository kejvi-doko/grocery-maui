﻿using System;
using System.ComponentModel;

namespace MyGrocery.MobileClient.Models
{
	public class Grocery:INotifyPropertyChanged
	{
        int _id;
        public int Id { get => _id; set
            {
                if (_id == value)
                    return;

                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
            }
        }

        string _name;

        public string Name
        {
            get => _name; set
            {
                if (_name == value)
                    return;

                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

