using PneuMalik.Models.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace PneuMalik.Helpers
{
    public class MailHelper
    {
        private string strEmailAddresFrom;
        private string strEmailAddresTo;

        private void NastavAdresaty()
        {

            var adminMail = ConfigurationManager.AppSettings["AdminMail"];

            strEmailAddresFrom = adminMail;
            strEmailAddresTo = adminMail;

        }

        public bool SendMailToSomeone(string Message, string Title, string Email, string Priloha, bool ToAdmin, string From)
        {
            NastavAdresaty();

            return MainSendMail((From == null ? strEmailAddresFrom : From), (ToAdmin ? strEmailAddresTo : Email), Message, Title, Priloha);
        }

        public void SendMailToAdmin(Customer customer)
        {

            string strBODY = "<TABLE>";

            strBODY += $"<tr><td><b>Úspěšná registrace v WWW obchodním systému {ConfigurationManager.AppSettings["ProjectName"]}</b></td><td></td></tr>"
                + $"<tr><td><b>Jméno: </b></td><td>{customer.Name} {customer.Surname}</td></tr>"
                + $"<tr><td><b>Ulice:</b></td><td>{customer.Street}</td></tr>"
                + $"<tr><td><b>Místo:</b></td><td>{customer.City}</td></tr>"
                + $"<tr><td><b>PSČ:</b></td><td>{customer.Zip}</td></tr>"
                + $"<tr><td><b>Stát:</b></td><td>{customer.Country}</td></tr>"
                + $"<tr><td><b>Telefon:</b></td><td>{customer.Phone}</td></tr>"
                //+ "<tr><td><b>Fax:</b></td><td> " + ds.Tables["User"].Rows[0].ItemArray.GetValue(7).ToString() + "</td></tr>"
                // +  "<tr><td><b>Poznámka: </b></td><td> " + ds.Tables["User"].Rows[0].ItemArray.GetValue(8).ToString()                    + "</td></tr>"
                + $"<tr><td><b>IČ:</b></td><td>{customer.CompanyId}</td></tr>"
                + $"<tr><td><b>DIČ:</b></td><td>{customer.TaxId}</td></tr>"
                + $"<tr><td><b>E-mail: </b></td><td>{customer.Email}</td></tr>"
                // +  "<tr><td><b>&nbsp;</b></td><td></td></tr>" 
                //+ "<tr><td><b>Osoba:</b></td><td>" + ds.Tables["User"].Rows[0].ItemArray.GetValue(21).ToString() + " " + ds.Tables["User"].Rows[0].ItemArray.GetValue(22).ToString() + " " + ds.Tables["User"].Rows[0].ItemArray.GetValue(23).ToString() + "</td></tr>"
                // +  "<tr><td><b>Typ:</b></td><td> " + (ds.Tables["User"].Rows[0].ItemArray.GetValue(24).ToString() == "1" ? "maloobchodník" : "velkoobchodník")  +"</td></tr>"
                + "</TABLE>";

            NastavAdresaty();
            
            string strTo = strEmailAddresTo;
            string strSubject = $"Registrace v WWW obchodním systému {ConfigurationManager.AppSettings["ProjectName"]}";

            /// pošli mail
            MainSendMail(strEmailAddresFrom, strTo, strBODY, strSubject, "");
        }

        public void SendMail(Order order, List<OrderItem> orderItems, Customer customer, List<Product> products,
            bool blnToAdmin, bool blnToCustomer)
        {
            string strBODY = "";

            // doprava
            var shippingInfo = "";
            if (order.Shipping == 3 || order.Shipping == 4 || order.Shipping == 5)
            {
                // osobní
                shippingInfo = "<p align=\"left\"><font size=\"1\"><font face=\"Verdana\">Pneumatiky, které si zákazník objedná, si musí vyzvednout do 3 pracovních dní nebo uhradit platbou hotově nebo fakturou a ponechat u nás do termínu přezutí. </font></font></p>";
            }
            else
            {
                // doprava
                shippingInfo = "<p align=\"left\"><font face=\"Verdana\"><font size=\"1\">Doprava osobních pneu za <font color=\"#ff0000\">1 ks 200,- kč</font>, <font color=\"#ff0000\">2 ks a více 100,- kč za 1 ks&#160;</font>(balení obsahuje dvě pneumatiky). Motocyklové pneu za 200,- kč do 2 ks<font color=\"#ff0000\">.</font> Cena za dopravu nákladních pneu po domluvě.<br />Všechny ceny jsou uvedeny s DPH.</ font ></ font ></ p > ";
            }

            strBODY += shippingInfo;

            var shippingName = "";
            switch (order.Shipping)
            {
                case 1:
                    shippingName = "Doprava, platba při převzetí zboží";
                    break;
                case 2:
                    shippingName = "Doprava, platba převodem předem";
                    break;
                case 3:
                    shippingName = "Osobní odběr, platba hotově";
                    break;
                case 4:
                    shippingName = "Osobní odběr, platba kartou";
                    break;
                case 5:
                    shippingName = "Osobní odběr, platba převodem předem";
                    break;
            }

            strBODY += "<TABLE style=\"font-family:Verdana;font-size:8pt;width:100%;border-collapse:collapse;\">";
            strBODY += $"<tr><td><b>Poznámka:</b></td><td>{order.Note}</td></tr>"
                + "<tr><td><b>&nbsp;</b></td><td>&nbsp;" + "</td></tr>"
                // +  "<tr><td width=\"30%\"><b>Obecné</b></td><td> " + "</td></tr>" 
                + $"<tr><td><b>Číslo objednávky:</b></td><td>{order.Id}</td></tr>"
                // + "<tr><td><b>Datum:</b></td><td> " + ds.Tables["Obj"].Rows[0].ItemArray.GetValue(1).ToString() + "</td></tr>"
                + $"<tr><td><b>Zákazník: </b></td><td>{order.Name}</td></tr>"
                + $"<tr><td><b>Ulice:</b></td><td>{order.Street}</td></tr>"
                + $"<tr><td><b>Místo:</b></td><td>{order.City}</td></tr>"
                + $"<tr><td><b>PSČ:</b></td><td>{order.Zip}</td></tr>"
                + $"<tr><td><b>Stát:</b></td><td>{order.Country}</td></tr>"
                + $"<tr><td><b>Telefon:</b></td><td>{customer.Phone}</td></tr>"
                //+ "<tr><td><b>Fax:</b></td><td> " + ds.Tables["Obj"].Rows[0].ItemArray.GetValue(9).ToString() + "</td></tr>"
                // +  "<tr><td><b>Poznámka: </b></td><td> " + ds.Tables["Obj"].Rows[0].ItemArray.GetValue(10).ToString()                    + "</td></tr>"
                + $"<tr><td><b>IČ:</b></td><td>{customer.CompanyId}</td></tr>"
                + $"<tr><td><b>DIČ:</b></td><td>{customer.TaxId}</td></tr>"
                + $"<tr><td><b>E-mail: </b></td><td>{customer.Email}</td></tr>"
                //+  "<tr><td><b>WWW: </b></td><td> " + ds.Tables["Obj"].Rows[0].ItemArray.GetValue(14).ToString()                    + "</td></tr>"
                //+ "<tr><td><b>&nbsp;</b></td><td></td></tr>"
                //+ "<tr><td><b>Místo dodání</b></td><td> " + "</td></tr>"
                //+ "<tr><td><b>Zákazník: </b></td><td> " + ds.Tables["Obj"].Rows[0].ItemArray.GetValue(15).ToString() + "</td></tr>"
                //+ "<tr><td><b>Ulice: </b></td><td> " + ds.Tables["Obj"].Rows[0].ItemArray.GetValue(16).ToString() + "</td></tr>"
                //+ "<tr><td><b>Místo: </b></td><td> " + ds.Tables["Obj"].Rows[0].ItemArray.GetValue(17).ToString() + "</td></tr>"
                //+ "<tr><td><b>PSČ: </b></td><td> " + ds.Tables["Obj"].Rows[0].ItemArray.GetValue(18).ToString() + "</td></tr>"
                //+ "<tr><td><b>Stát: </b></td><td> " + ds.Tables["Obj"].Rows[0].ItemArray.GetValue(19).ToString() + "</td></tr>"
                // +  "<tr><td><b>Typ</b></td><td> " + (ds.Tables["Obj"].Rows[0].ItemArray.GetValue(23).ToString() == "1" ? "maloobchodník" : "velkoobchodník")  +"</td></tr>"
                + "<tr><td><b>&nbsp;</b></td><td></td></tr>"
                + $"<tr><td><b>Doprava:</b></td><td>{shippingName}</td></tr>"
                // +  "<tr><td><b>Obchodni_podminky:</b></td><td> " + (ds.Tables["Obj"].Rows[0].ItemArray.GetValue(26).ToString() == "True" ? "ma zájem o zaslání obchodních podmínek pro velkoodběratele" : "nema_zajem")  +"</td></tr>"
                + "</TABLE>";

            string strBody2 = "<BR><BR><TABLE style=\"font-family: Verdana, Tahoma, Verdana, Arial, Helvetica, sans-serif; font-size: 11px; BORDER-RIGHT: #cccccc 1px solid; BORDER-TOP: #cccccc 1px solid; BORDER-LEFT: #cccccc 1px solid; BORDER-BOTTOM: #cccccc 1px solid; background-color:#f7f7f7;\">";
            strBody2 += "<tr class=\"MnuItem\"><td class=\"ts\" style=\"background-color:#f0f0f0;\" colspan=7><b>Položky objednávky</b></td></tr>";
            // strBody2 += "<tr><td><b>Číslo produktu:</b></td><td><b>Název položky:</b></td><td><b>sazba DPH</b></td><td><b>Typ Balení</b></td><td><b>Množství</b></td><td><b>Cena</b></td><td><b>Cena s DPH</b></td><td><b>Cena Celkem</b></td>";
            strBody2 += "<tr><td class=\"ts\" style=\"background-color:#f0f0f0;\"><b>Kat. číslo</b></td>"
                + "<td class=\"ts\" style=\"background-color:#f0f0f0;\"><b>Název</b></td>"
                + "<td class=\"ts\" style=\"background-color:#f0f0f0;\"><b>Rozměr</b></td>"
                + "<td class=\"ts\" style=\"background-color:#f0f0f0;\"><b>Jednotka</b></td>"
                + "<td class=\"ts\" style=\"background-color:#f0f0f0;\"><b>Množství</b></td>"
                + "<td class=\"ts\" style=\"background-color:#f0f0f0;\"><b>Cena s DPH</b></td>"
                + "<td class=\"ts\" style=\"background-color:#f0f0f0;\"><b>Cena mezisoučet</b></td>";

            foreach (var row in orderItems)
            {

                var product = products.FirstOrDefault(p => p.Id == row.ProductId);

                strBody2 += "<tr Style=\"background-color:#cccccc;\"><td colspan=\"6\"></td>";
                strBody2 += "<tr>";

                // strBody2 += "<td style=\"\"><table width=\"100%\" height=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" style=\"font-family:Verdana;font-size:8pt;width:100%;border-collapse:collapse;\"><tr><td style=\"background-color:#F4F4F4;\" valign=\"top\">" + ds2.Tables["ObjItems"].Rows[i].ItemArray.GetValue(3).ToString() + "</td></tr><tr><td style=\"background-color:#F4F4F4;\" valign=\"top\">" + ds2.Tables["ObjItems"].Rows[i].ItemArray.GetValue(13).ToString() + "</td></tr></table></td>"
                strBody2 += $"<td style=\"\" valign=\"top\">{product.Code}</td>"
                    + $"<td style=\"\" valign=\"top\">{row.Name}</td>"
                    + $"<td style=\"\" valign=\"top\">rozměry</td>"
                    // +  "<td style=\"\">" + ds2.Tables["ObjItems"].Rows[i].ItemArray.GetValue(12).ToString() + "</td>"
                    // +  "<td>" + ds2.Tables["ObjItems"].Rows[i].ItemArray.GetValue(4).ToString() + "</td>"
                    // +  "<td>" + ds2.Tables["ObjItems"].Rows[i].ItemArray.GetValue(5).ToString() + "</td>"
                    + "<td style=\"\" valign=\"top\">ks</td>"
                    + $"<td style=\"\" valign=\"top\">{row.Quantity}</td>"
                    + $"<td style=\"\" valign=\"top\">{row.Price}</td>"
                    + $"<td style=\"\" valign=\"top\">{row.Price * row.Quantity:0}</td>";
                // poznamka +  "<td style=\"background-color:#F4F4F4;\">" + ds2.Tables["ObjItems"].Rows[i].ItemArray.GetValue(10).ToString() + "</td>"
                // +  "<td>" + ds2.Tables["ObjItems"].Rows[i].ItemArray.GetValue(8).ToString() + "</td>" 
                // +  "<td>" + ds2.Tables["ObjItems"].Rows[i].ItemArray.GetValue(9).ToString() + "</td>";
                // +  "<td style=\"\">" +((ds2.Tables["ObjItems"].Rows[i].ItemArray.GetValue(11).ToString() == "") ? "&nbsp;" : "<img Src=\""+ ConfigurationSettings.AppSettings["RootUrl"] +"gfx/obr/"+ ds2.Tables["ObjItems"].Rows[i].ItemArray.GetValue(11).ToString() +"\" border=\"0\">") +"</td>";

                strBody2 += "</tr>";
            }

            // celkem (bez poštovného, PHE...)
            strBody2 += "<tr><td style=\"background-color:#f0f0f0;\">"
                + "<b>Celkem:</b></td style=\"background-color:#f0f0f0;\"><td colspan=5 style=\"background-color:#f0f0f0;\">&nbsp;</td>"
                + $"<td style=\"background-color:#f0f0f0;\">{order.Total}</td></tr>";

            // PHE
            //strBody2 += "<tr><td style=\"background-color:#f0f0f0;\"><b>Z toho recyklační poplatek PHE:</b></td style=\"background-color:#f0f0f0;\"><td colspan=4 style=\"background-color:#f0f0f0;\">&nbsp;</td><td style=\"background-color:#f0f0f0;\">" + String.Format("{0:F} Kč", dblPHE) + "</td></tr>";

            // strBody2 += "<tr><td style=\"background-color:#f0f0f0;\"><b>Sleva:</b></td style=\"background-color:#f0f0f0;\"><td colspan=3 style=\"background-color:#f0f0f0;\">&nbsp;</td><td style=\"background-color:#f0f0f0;\">" + String.Format("{0:F} %", ds.Tables["Obj"].Rows[0].ItemArray.GetValue(33)) + "</td><td style=\"background-color:#f0f0f0;\">" + String.Format("{0:F} Kč", ds.Tables["Obj"].Rows[0].ItemArray.GetValue(34)) + "</td></tr>";
            // double KonecnaCena = double.Parse(ds.Tables["Obj"].Rows[0].ItemArray[32].ToString())
            //                     - double.Parse(ds.Tables["Obj"].Rows[0].ItemArray[34].ToString());

            double KonecnaCena = (double)order.Total;

            // poštovné - dopravné
            if (order.ShippingPrice >= 0)
            {
                strBody2 += "<tr><td style=\"background-color:#f0f0f0;\"><b>Dopravné:</b></td style=\"background-color:#f0f0f0;\"><td colspan=5 style=\"background-color:#f0f0f0;\">&nbsp;</td>"
                    + $"<td style=\"background-color:#f0f0f0;\">{order.ShippingPrice:F} Kč</td></tr>";

                KonecnaCena = KonecnaCena + (double)order.ShippingPrice;
            }
            else
            {
                strBody2 += "<tr><td style=\"background-color:#f0f0f0;\"><b>Dopravné:</b></td style=\"background-color:#f0f0f0;\"><td colspan=5 style=\"background-color:#f0f0f0;\">&nbsp;</td>"
                    + "<td style=\"background-color:#f0f0f0;\">bude upřesněno</td></tr>";
            }

            // poplatky
            //if (double.Parse((String.IsNullOrEmpty(ds.Tables["Obj"].Rows[0].ItemArray[37].ToString()) ? "0" : ds.Tables["Obj"].Rows[0].ItemArray[37].ToString())) > 0)
            //{
            //    strBody2 += "<tr><td style=\"background-color:#f0f0f0;\"><b>" + objSysText.fncGetStrForNum(156, 0) + ":</b></td>"
            //        + "<td colspan=5 style=\"background-color:#f0f0f0;\">&nbsp;</td>"
            //        + "<td style=\"background-color:#f0f0f0;\">" + String.Format("{0:F} Kč", (double.Parse(ds.Tables["Obj"].Rows[0].ItemArray[37].ToString()))) + "</td></tr>";
            //    KonecnaCena = KonecnaCena + double.Parse(ds.Tables["Obj"].Rows[0].ItemArray[37].ToString());
            //}

            strBody2 += "<tr><td style=\"background-color:#f0f0f0;\"><b>Konečná cena:</b></td>"
                + "<td colspan=5 style=\"background-color:#f0f0f0;\">&nbsp;</td>"
                + $"<td style=\"background-color:#f0f0f0;\">{KonecnaCena:F} Kč</td></tr>";

            strBody2 += "</TABLE><br>";

            NastavAdresaty();

            // admin
            string strSubject = $"{ConfigurationManager.AppSettings["ProjectName"]} – on-line objednavka {order.Id}";
            if (blnToAdmin)
            {
                MainSendMail(strEmailAddresFrom, strEmailAddresTo, strBody2 + "<br /><br />" + strBODY, strSubject, "");
            }

            // poslani e-mailu zakaznikovi 
            string strUvod = "<TABLE style=\"font-family:Verdana;font-size:8pt;width:100%;border-collapse:collapse;\">"
                + "<tr><td>Vážený zákazníku,<br><br>děkujeme Vám za Vaší objednávku v obchodním systému " + ConfigurationManager.AppSettings["ProjectName"]
                + ", <br>objednávka je registrována pod číslem " + order.Id + " s uvedenými údaji:"
                + "</td></tr>"
                + "</table>";

            string strSubject2 = ConfigurationManager.AppSettings["ProjectName"]
                + " – on-line objednávka " + order.Id;

            if (blnToCustomer)
            {
                MainSendMail(strEmailAddresFrom, customer.Email, strUvod + strBody2 + "<br /><br />" + strBODY, strSubject2, "");
            }
        }

        /// <summary>
        /// funkce samotného odeslání emailu
        /// </summary>
        /// <param name="strMailFrom">odesílatel</param>
        /// <param name="strMailTo">adresát</param>
        /// <param name="strMessage">zpráva</param>
        /// <param name="strTitle">subject</param>
        /// <param name="strAttach">příloha</param>
        /// <returns>logická hodnota, zda se podařilo odeslat mail</returns>
        private bool MainSendMail(string From, string To, string Message, string Subject, 
            string Attachment)
        {
            var maFrom = new MailAddress(From);
            var maTo = new MailAddress(To);

            var objMail = new MailMessage(maFrom, maTo)
            {
                Subject = Subject,
                IsBodyHtml = true
            };

            bool blnStatus = true;

            if (!string.IsNullOrEmpty(Attachment))
            {
                try
                {
                    objMail.Attachments.Add(new Attachment(Attachment));
                }
                catch
                {
                    Message += "<br /><br /><i>Přílohu se bohužel nepodařilo připojit. (" + Attachment.Substring(Attachment.LastIndexOf("/")) + ")</i>";
                }
            }

            objMail.Body = Message;
            objMail.Priority = MailPriority.Normal;

            var smtp = new SmtpClient(ConfigurationManager.AppSettings["Smtp"]);

            string strException = "";

            try
            {
                smtp.Send(objMail);

                objMail.Dispose();
            }
            catch (Exception e)
            {
                strException = e.Message;
                blnStatus = false;
            }

            string SendMailInfo = "\n\nCas odeslani e-mailu: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
            SendMailInfo += "\n\nFrom: " + From +
                "\nTo: " + To +
                "\nTitle: " + Subject +
                "\nStatus: " + (blnStatus ? "OK" : "NOK");
            if (!String.IsNullOrEmpty(strException)) SendMailInfo += "\nException: " + strException;
            SendMailInfo += "\n------------------------------------------------";

            // zápis do souboru // *** //
            string FILE_NAME = ConfigurationManager.AppSettings["ErrorLog"] + "sentMailLog.txt";

            byte[] bin = new byte[SendMailInfo.Length];
            bin = ASCIIEncoding.GetEncoding("utf-8").GetBytes(SendMailInfo);

            System.IO.FileStream fse1 = new System.IO.FileStream(FILE_NAME, System.IO.FileMode.Append, System.IO.FileAccess.Write);
            fse1.Write(bin, 0, bin.Length);
            fse1.Close();

            return true;
        }
    }
}