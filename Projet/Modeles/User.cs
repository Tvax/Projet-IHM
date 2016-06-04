using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Modeles {
    public class User {
        private string _usr;
        private string _pswd;

        public string Username {
            get { return _usr; }
            set { _usr = value; }
        }
        public string Password {
            get { return _pswd; }
            set { _pswd = value; }
        }
    }
}