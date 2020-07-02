﻿using SmartPillowLib.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SmartPillowLib.Models
{
    public class AlarmListViewWrapper : NotifyClass
    {
        public int Id { get; private set; }

        /// <summary>
        ///     Signals that the state of this alarm has changed.
        ///     This means it needs to be enabled or disabled on the native platform.
        /// </summary>
        public static event Action<AlarmListViewWrapper> AlarmStateChanged;

        private bool isAlarmEnabled;
        public bool IsAlarmEnabled 
        { 
            get => isAlarmEnabled; 
            set 
            {
                if (IsAlarmEnabled == value) return;
                isAlarmEnabled = value;
                AlarmStateChanged?.Invoke(this);
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                if (Name == value) return;
                name = value;
                NotifyPropertyChanged();
            }
        }

        private bool toBeDeleted = false;
        /// <summary>
        ///     Controls whether the delete button is present.
        /// </summary>
        public bool ToBeDeleted
        {
            get => toBeDeleted;
            set
            {
                if (ToBeDeleted == value) return;
                toBeDeleted = value;
                NotifyPropertyChanged();
            }
        }

        public AlarmListViewWrapper(int _id, string _name, bool _isAlarmEnabled)
        {
            Id = _id;
            Name = _name;
            IsAlarmEnabled = _isAlarmEnabled;
        }

        public AlarmListViewWrapper(Alarm _source)
        {
            Id = _source.Id;
            Name = _source.Name;
            IsAlarmEnabled = _source.IsAlarmEnabled;
        }
    }
}
