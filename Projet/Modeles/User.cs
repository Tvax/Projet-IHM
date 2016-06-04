using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Modeles {
    public class User {
        private string _usr;
        private string _pswd;
        private System.Windows.Media.Brush _th;
        private string _list;

        public string Username {
            get { return _usr; }
            set { _usr = value; }
        }
        public string Password {
            get { return _pswd; }
            set { _pswd = value; }
        }
        public System.Windows.Media.Brush Theme {
            get { return _th; }
            set { _th = value; }
        }
        public string List {
            get { return _list; }
            set { _list = value; }
        }

    }
}