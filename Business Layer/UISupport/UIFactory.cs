using BusinessLayer.Support._interfaces;
using System;
using System.Windows.Forms;

namespace BusinessLayer.Support
{
    public class UIFactory : IUIFactory
    {
        Button IUIFactory.GetButton(Button button, string buttonType, string id, string text)
        {
            button.Name = id;
            button.Text = text;

            switch (buttonType)
            {
                case "blueButton":
                BlueButton(button);
                    break;
                case "redButton":
                RedButton(button);
                    break;
                default:
                    break;
            }
            return button;
        }

        ListBox IUIFactory.GetListBox(ListBox listBox, string listBoxType, string id, string text)
        {
            throw new NotImplementedException();
        }

        Label IUIFactory.GetLabel(Label label, string labelType, string id, string text)
        {
            throw new NotImplementedException();
        }

        TextBox IUIFactory.GetTextBox(TextBox textBox, string textBoxType, string id, string text)
        {
            throw new NotImplementedException();
        }

        private Button BlueButton(Button button)
        {
            button.BackColor = System.Drawing.Color.Blue;
            button.ForeColor = System.Drawing.Color.White;
            GetDefaultButton(button);
            return button;
        }

        private Button RedButton(Button button)
        {
            button.BackColor = System.Drawing.Color.Red;
            button.ForeColor = System.Drawing.Color.White;
            return button;
        }

        private void GetDefaultButton(Button button)
        {
            button.Size = new System.Drawing.Size(150, 30);
            button.UseVisualStyleBackColor = false;
        }
    }
}
