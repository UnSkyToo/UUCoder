using System;
using System.Collections.Generic;
using System.Text;

namespace UUCoder.CodeElement
{
    public class UDataElement
    {
        public List<UDataElement> Items { get; protected set; }

        public bool HasItem
        {
            get
            {
                return Items.Count != 0;
            }
        }

        public string Name { get; set; }
        public string Tag { get; set; }
        public string Value { get; set; }

        public UDataElement()
        {
            this.Items = new List<UDataElement>();

            this.Name = string.Empty;
            this.Tag = string.Empty;
            this.Value = string.Empty;
        }

        public void Dispose()
        {
            if (Items.Count != 0)
            {
                foreach (UDataElement element in Items)
                {
                    element.Dispose();
                }
            }
            else
            {
                Items.Clear();
            }
        }

        public virtual void SetData(string name, string value)
        {
            foreach (UDataElement element in Items)
            {
                if (element.Name == name)
                {
                    element.Value = value;
                    return;
                }
            }
        }

        public virtual void AddData(string name, string value)
        {
            UDataElement ue = new UDataElement();
            ue.Name = name;
            ue.Value = value;

            Items.Add(ue);
        }

        public virtual void DelData(string name)
        {
            foreach (UDataElement element in Items)
            {
                if (element.Name == name)
                {
                    Items.Remove(element);
                    return;
                }
            }
        }
    }
}
