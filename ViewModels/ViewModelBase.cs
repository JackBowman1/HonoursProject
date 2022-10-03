using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;
using HonoursProject.Services;
using System.Collections.ObjectModel;
using HonoursProject.Models;
namespace HonoursProject.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public IDataStore<InformationSection> DataStore => DependencyService.Get<IDataStore<InformationSection>>();
        public IDataStore<Diary> DiaryStore => DependencyService.Get<IDataStore<Diary>>();
        public IDataStore<User> UserStore => DependencyService.Get<IDataStore<User>>();
        public IDataStore<UserGoal> GoalStore => DependencyService.Get<IDataStore<UserGoal>>();
        public ITempDataStore<UserGoal> TempUserGoalStore => DependencyService.Get<ITempDataStore<UserGoal>>();

        public ViewModelBase()
        {
            InformationSection section = new InformationSection();
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
        public class Grouping<K, T> : ObservableCollection<T> 
        {
            public K Key  { get; private set; }
            public Grouping(K key, IEnumerable<T> items) 
            {
                Key = key;
                foreach (var item in items) 
                {
                    this.Items.Add(item);
                }                
            }
        }
    }
}
