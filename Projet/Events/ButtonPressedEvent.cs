using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet.Events
{
    public class ButtonPressedEvent
    {

        private static ButtonPressedEvent _myEvent
        {
            get;
            set;
        }

        private ButtonPressedEvent()
        {

        }

        public static ButtonPressedEvent GetEvent()
        {
            if (_myEvent == null)
            {
                _myEvent = new ButtonPressedEvent();
            }
            return _myEvent;

        }


        public event EventHandler Handler;

        public void OnButtonPressedHandler(EventArgs e)
        {
            if (Handler != null)
            {
                Handler(this, e);
            }
        }

    }
}
