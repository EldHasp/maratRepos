
using Laboratoria.MVVM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Laboratoria.ViewModels
{
    class UsersViewModel : ObservableObject
    {

        private string fio;
        public string Fio
        {
            get { return fio; }
            set
            {
                fio = value;
                RaisePropertyChangedEvent("Fio");
            }
        }
        private string position;
        public string Position
        {
            get { return position; }
            set
            {
                position = value;
                RaisePropertyChangedEvent("Position");
            }
        }
    }
}