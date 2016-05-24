using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Projet {
    public class Emote {

        public string Nom;
        public string Description;
        public Image image = new Image();
        
        public override string ToString() {
            return string.Format("{0}", Nom);
        }
    }
}
