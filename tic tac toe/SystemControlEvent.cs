using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SCE
{
    internal class SystemControlEvent
    {
        public SystemControlEvent()
        { }

        public SystemControlEvent(List<Button> buttons)
        {
            foreach (var item in buttons)
                Variable.Add(item, (null, null));
        }

        private readonly Dictionary<Button, (EventHandler Default, EventHandler Now)> Variable = new Dictionary<Button, (EventHandler Default, EventHandler Now)>();

        private readonly List<Button> Keys = new List<Button>();

        private bool Save = false;

        public void SaveCondition()
        {
            Save = true;
            foreach (var item in Keys)
            {
                Variable[item] = (Variable[item].Now, Variable[item].Now);
            }
        }

        public void LoadSave()
        {
            if (Save)
                foreach (var key in Keys)
                {
                    if (Variable[key].Now != null)
                        foreach (var e in Variable[key].Now.GetInvocationList())
                            key.Click -= (EventHandler)e;
                    Variable[key] = (Variable[key].Default, null);
                    foreach (var item in Variable[key].Default.GetInvocationList())
                        key.Click += (EventHandler)item;
                    Variable[key] = (Variable[key].Default, Variable[key].Default);
                }
        }

        public void Add(Button b, EventHandler e)
        {
            if (!Variable.ContainsKey(b))
                Variable.Add(b, (null, null));
            if (!Keys.Contains(b))
                Keys.Add(b);
            var temp = Variable[b].Now;
            temp += e;
            b.Click += e;
            Variable[b] = (Variable[b].Default, temp);
        }

        public void AddClear(Button b, EventHandler e)
        {
            if (!Variable.ContainsKey(b))
                Variable.Add(b, (null, null));
            if (!Keys.Contains(b))
                Keys.Add(b);
            var temp = Variable[b].Now;
            temp += e;
            Variable[b] = (Variable[b].Default, temp);
        }

        public void Remove(Button b, EventHandler e)
        {
            if (Variable.ContainsKey(b))
                if (Variable[b].Now != null)
                    foreach (var item in Variable[b].Now.GetInvocationList())
                        if (item == (Delegate)e)
                        {
                            var temp = Variable[b].Now;
                            temp -= e;
                            b.Click -= e;
                            Variable[b] = (Variable[b].Default, temp);
                        }
        }

    }
}
