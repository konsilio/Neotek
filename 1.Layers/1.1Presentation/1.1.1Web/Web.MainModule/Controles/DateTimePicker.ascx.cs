using System;
using System.ComponentModel;
using System.Drawing;

namespace UserControls
{
    public partial class DateTimePicker : System.Web.UI.UserControl
    {
        #region public
        [System.ComponentModel.Browsable(true)]
        public event EventHandler TextChanged;

        [Bindable(false), Browsable(true), Category("Bit-Options"), Description("The Date.")]
        public DateTime GetDate
        {
            get
            {
                DateTime oDate = new DateTime();
                if (!string.IsNullOrEmpty(tbDatePicker.Text.Trim()))
                {
                    if (!DateTime.TryParse(tbDatePicker.Text.Trim(), out oDate))
                    {
                        //"MM/dd/yyyy"
                        oDate = DateTime.Parse(string.Format("{0}/{1}/{2}",
                                        tbDatePicker.Text.Trim().Split('/')[1],
                                        tbDatePicker.Text.Trim().Split('/')[0],
                                        tbDatePicker.Text.Trim().Split('/')[2])
                                        );
                    }
                }
                return oDate;
            }
        }

        [Bindable(false), Browsable(true), Category("Bit-Options"), Description("Check if the box is not empty.")]
        public bool HasValue
        {
            get
            {
                return !string.IsNullOrEmpty(tbDatePicker.Text.Trim());
            }
        }

        [Bindable(false), Browsable(true), Category("Bit-Options"), Description("Text.")]
        public string Text
        {
            get
            {
                return tbDatePicker.Text.Trim();
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    value = tbDatePicker.Text.Trim();
                }

                if (!string.IsNullOrEmpty(value))
                {
                    try
                    {
                        tbDatePicker.Text = DateTime.Parse(value).ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        //tbDatePicker.Text = DateTime.Now.ToString("MM/dd/yyyy");
                        tbDatePicker.ToolTip = "Cannot Set current Date, verify, your globalization";
                    }
                }
                else
                {
                    tbDatePicker.Text = "";
                }
            }
        }

        [Bindable(false), Browsable(true), Category("Bit-Options"), Description("BackColor.")]
        public Color BackColor
        {
            get
            {
                return tbDatePicker.BackColor;
            }
            set
            {
                tbDatePicker.BackColor = value;
            }
        }

        [Bindable(false), Browsable(true), Category("Bit-Options"), Description("Enable")]
        public bool Enabled
        {
            get
            {
                imgDatePicker.Visible = tbDatePicker.Enabled;
                return tbDatePicker.Enabled;
            }
            set
            {
                imgDatePicker.Visible = value;
                tbDatePicker.Enabled = value;
            }
        }

        [Bindable(false), Browsable(true), Category("Bit-Options"), Description("Actives the required validator.")]
        public bool Required
        {
            get
            {
                return rfvDatePicker.Enabled;
            }
            set
            {
                rfvDatePicker.Enabled = value;
            }
        }

        [Bindable(false), Browsable(true), Category("Bit-Options"), Description("Get or Set Validation Group.")]
        public string ValidatorValidationGroup
        {
            get
            {
                return rfvDatePicker.ValidationGroup;
            }
            set
            {
                rfvDatePicker.ValidationGroup = value;
            }
        }
        #endregion public
    }
}