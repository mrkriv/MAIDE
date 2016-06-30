using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.ComponentModel;
using WeifenLuo.WinFormsUI.Docking;
using System;
using System.Drawing;
using ASM.VM;
using ASM.Utilit;

namespace ASM
{
    [ModuleAtribute(dysplayName = "Операторы", defaultShow = false, dock = DockState.DockRight)]
    public partial class OperatorsWindow : DockContent
    {
        public OperatorsWindow()
        {
            InitializeComponent();
            
            foreach (var op in Operators.OperationsList)
                list.Items.Add(op.Name);
        }

        private void list_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            activeName.Text = list.SelectedItem as string;
            MethodInfo method = Operators.OperationsList.First(w => w.Name == activeName.Text);

            activeDec.Clear();
            activeDec.AppendText(method.GetCustomAttribute<DescriptionAttribute>().Description);
            activeDec.AppendText("\r\n\r\nПример:\r\n ");
            addTextByColor(method.Name, Color.DarkBlue);
            activeDec.AppendText("\t");

            var paramTypes = method.GetParameters();
            for (int i = 0; i < paramTypes.Length; i++)
            {
                string name = "param";
                Color color = Color.Black;

                if (paramTypes[i].ParameterType.BaseType == typeof(Register))
                {
                    name = "reg";
                    color = Color.BlueViolet;
                }
                else if (paramTypes[i].ParameterType == typeof(Link))
                {
                    name = "metka";
                    color = Color.Green;
                }
                else if (paramTypes[i].ParameterType == typeof(int))
                {
                    name = "#";
                    color = Color.Red;
                }

                addTextByColor(name + i, color);
                if (i != paramTypes.Length - 1)
                    activeDec.AppendText(", ");
            }
        }

        void addTextByColor(string text, Color color)
        {
            activeDec.AppendText(text);
            activeDec.SelectionStart = activeDec.Text.Length - text.Length;
            activeDec.SelectionLength = text.Length;
            activeDec.SelectionColor = color;
        }

        private void mask_TextChanged(object sender, EventArgs e)
        {
            list.Items.Clear();
            foreach (var op in Operators.OperationsList.Where(w => w.Name.Contains(mask.Text)))
                list.Items.Add(op.Name);
        }
    }
}