using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodifyName
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private void Reset(bool message, bool name, bool radio)
        {
            if (message)
            {
                messageLabel.InnerHtml = "Message";
                messageLabel.Style.Add("color", "#3B3F45");
            }
            if (name)
            {
                nameLabel.InnerHtml = "What's your name?";
                nameLabel.Style.Add("color", "#3B3F45");
            }
            if (radio)
            {
                radioLabel.InnerHtml = "What do you want to do?";
                radioLabel.Style.Add("color", "#3B3F45");
            }
            TextRadio.Value = "";
        }

        protected void Button_Click(object sender, EventArgs e)
        {
            try
            {
                string radio = TextRadio.Value;
                string input = q4.Value;
                string name = q1.Value;
                if (!string.IsNullOrEmpty(radio))
                {
                    if (name.ToCharArray().Distinct().Count() > 4)
                    {
                        CodifyNameClass cncs = new CodifyNameClass();
                        //CodifyNameCSharp cncs = new CodifyNameCSharp();
                        if (radio == "encode")
                            q4.Value = cncs.CodifyName(input, name, true);
                        else if (radio == "decode")
                            q4.Value = cncs.CodifyName(input, name, false);
                        else
                            q4.Value = q4.Value;
                        Reset(true, true, true);
                    }
                    else
                    {
                        nameLabel.InnerHtml = "Your name should have atleast 5 distinct characters";
                        nameLabel.Style.Add("color", "red");
                        Reset(true, false, true);
                    }
                }
                else
                {
                    radioLabel.InnerHtml = "Please select one of the two";
                    radioLabel.Style.Add("color", "red");
                    Reset(true, true, false);
                }
            }
            catch (Exception)
            {
                messageLabel.InnerHtml = "Not valid message";
                messageLabel.Style.Add("color", "red");
                TextRadio.Value = "";
            }

        }

    }
}