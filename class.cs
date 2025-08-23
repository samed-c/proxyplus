using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Microsoft.Win32;
using Microsoft.VisualBasic;

using System.Net;
using System.IO;

using System.Collections;
using System.Security.Principal;



namespace proxyplus
{ 

   class proxyplus
    {
       public void IEproxystatus(int onoff)
       {

           int ieproxyenable;
           string ieproxyserver;

           if (onoff==1)
           {
            ieproxyenable=1;
            ieproxyserver=Program.mainform.textBox1.Text + ":" + Program.mainform.textBox2.Text;
           }
           else
           {
            ieproxyenable=0;
            ieproxyserver=":";
           }

           RegistryKey registryT = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", true);
           registryT.SetValue("ProxyEnable", ieproxyenable);
           registryT.SetValue("ProxyServer", ieproxyserver);
           registryT.Close();
       
       }

       public void FFproxystatus(int onoff)
       {


           string foldername = "";

           foreach (string name in getFFconfdir())
           {
               foldername = name;

               String line = "";
               String setting = "";

               String proxyaddress = "";
               String proxyport = "";

               int codecolumn1 = -1;
               int codecolumn2 = -1;
               int codecolumn3 = -1;

               int codecolumnread1 = 0;
               int codecolumnread2 = 0;
               int codecolumnread3 = 0;

               string ffsettingfile = @name + "\\" + "prefs.js";
               string settinfilecontext = "";
               int ffproxystatus;

               createfile(ffsettingfile);


               TextReader tr = new StreamReader(ffsettingfile);

               while ((line = tr.ReadLine()) != null)
               {
            

                   codecolumn1 = line.IndexOf("user_pref(\"network.proxy.http\", \"");
                   codecolumn2 = line.IndexOf("user_pref(\"network.proxy.http_port\", ");
                   codecolumn3 = line.IndexOf("user_pref(\"network.proxy.type\", ");

    

                   if (codecolumn1 < 0 && codecolumn2 < 0 && codecolumn3 < 0)
                   {
                       settinfilecontext = settinfilecontext + "\r\n" + line;
                   }

                   if (codecolumn1 >= 0)
                   {
                       codecolumnread1 = 1;
                       settinfilecontext = settinfilecontext + "\r\n" + line.Replace(line, "user_pref(\"network.proxy.http\", \"" + Program.mainform.textBox1.Text + "\");");
                   }

                   if (codecolumn2 >= 0)
                   {
                       settinfilecontext = settinfilecontext + "\r\n" + line.Replace(line, "user_pref(\"network.proxy.http_port\", " + Program.mainform.textBox2.Text + ");");
                       codecolumnread2 = 1;
                   }

                   if (codecolumn3 >= 0)
                   {
                       settinfilecontext = settinfilecontext + "\r\n" + line.Replace(line, "user_pref(\"network.proxy.type\", " + onoff + ");");
                       codecolumnread3 = 1;
                   }


               }

               tr.Close();

               StreamWriter writer = new StreamWriter(ffsettingfile);
               writer.Write(settinfilecontext);
               writer.Close();

               updatefile(ffsettingfile, settinfilecontext);


               addnewFFproperty(ffsettingfile, codecolumnread1, codecolumnread2, codecolumnread3, onoff);


           }

       }

       public void Winampproxystatus(int onoff)
       {
           string winampsetfile = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + @"\Winamp\winamp.ini";

           createfile(winampsetfile);

           int codecolumn1 = -1;
           int codecolumn2 = -1;

           int codecolumnread1 = 0;
           int codecolumnread2 = 0;

           String line = "";
           

           string settinfilecontext = "";

           TextReader tr = new StreamReader(winampsetfile);

           while ((line = tr.ReadLine()) != null)
           {


               codecolumn1 = line.IndexOf("proxy80=");
               codecolumn2 = line.IndexOf("proxy=");


               string winproxy;


               if (onoff == 1)
                   winproxy = Program.mainform.textBox1.Text + ":" + Program.mainform.textBox2.Text;
               else
                   winproxy = "";
               



               if (codecolumn1 < 0 && codecolumn2 < 0)
               {
                   settinfilecontext = settinfilecontext + "\r\n" + line;
               }

               if (codecolumn1 >= 0)
               {
                   codecolumnread1 = 1;
                   settinfilecontext = settinfilecontext + "\r\n" + line.Replace(line, "proxy80=0");
               }

               if (codecolumn2 >= 0)
               {
                   settinfilecontext = settinfilecontext + "\r\n" + line.Replace(line, "proxy=" + winproxy);
                   codecolumnread2 = 1;
               }

           }

           tr.Close();

           updatefile(winampsetfile, settinfilecontext);

       }

       public void LimeWireproxystatus(int onoff)
       {
           string limesetfile = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + @"\LimeWire\limewire.props";

           createfile(limesetfile);

           int codecolumn1 = -1;
           int codecolumn2 = -1;
           int codecolumn3 = -1;
           int codecolumn4 = -1;

           int codecolumnread1 = 0;
           int codecolumnread2 = 0;
           int codecolumnread3 = 0;
           int codecolumnread4 = 0;

           String line = "";


           string limeproxy;
           string limeport;
           string limepublish;
           string limeenable;


           string settinfilecontext = "";

           TextReader tr = new StreamReader(limesetfile);

           while ((line = tr.ReadLine()) != null)
           {


               codecolumn1 = line.IndexOf("ENABLE_PUSH_PROXY_QUERIES=");
               codecolumn2 = line.IndexOf("PROXY_HOST=");
               codecolumn3 = line.IndexOf("PROXY_PORT=");
               codecolumn4 = line.IndexOf("PUBLISH_PUSH_PROXIES=");
               

               string winproxy;


               if (onoff == 1)
               {
               limeproxy=Program.mainform.textBox1.Text;
               limeport=Program.mainform.textBox2.Text;
               limepublish="true";
               limeenable="true";
               }
               else
                      {
               limeproxy="";
               limeport="";
               limepublish="false";
               limeenable="false";
               }



               if (codecolumn1 < 0 && codecolumn2 < 0 && codecolumn3 < 0 && codecolumn4 < 0)
               {
                   settinfilecontext = settinfilecontext + "\r\n" + line;
               }

               if (codecolumn1 >= 0)
               {
                   codecolumnread1 = 1;
                   settinfilecontext = settinfilecontext + "\r\n" + line.Replace(line, "ENABLE_PUSH_PROXY_QUERIES=" + limeenable);
               }

               if (codecolumn2 >= 0)
               {
                   settinfilecontext = settinfilecontext + "\r\n" + line.Replace(line, "PROXY_HOST=" + limeproxy);
                   codecolumnread2 = 1;
               }

               if (codecolumn3 >= 0)
               {
                   settinfilecontext = settinfilecontext + "\r\n" + line.Replace(line, "PROXY_PORT=" + limeport);
                   codecolumnread3 = 1;
               }

               if (codecolumn4 >= 0)
               {
                   settinfilecontext = settinfilecontext + "\r\n" + line.Replace(line, "PUBLISH_PUSH_PROXIES=" + limepublish);
                   codecolumnread4 = 1;
               }


           }

           tr.Close();


           updatefile(limesetfile, settinfilecontext);

       }



      
       public void addnewFFproperty(string ffsettingfile, int codecolumnread1, int codecolumnread2, int codecolumnread3, int onoff)
{

                   StreamWriter sw = new StreamWriter(ffsettingfile, true);

                   if (codecolumnread1 == 0)
                   {
                       sw.WriteLine("user_pref(\"network.proxy.http\", \"" + Program.mainform.textBox1.Text + "\");");              
                   }

                   if (codecolumnread2 == 0)
                   {           
                       sw.WriteLine("user_pref(\"network.proxy.http_port\", " + Program.mainform.textBox2.Text + ");");
                    }

                   if (codecolumnread3 == 0)
                   {
                       sw.WriteLine("user_pref(\"network.proxy.type\", " + onoff + ");");
                   }

                   sw.Close();
}

       public string[] getFFconfdir()
       {
           string user = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
           int slash = user.IndexOf("\\", 0);
           string currentuser = user.Substring(slash + 1, user.Length - user.IndexOf("\\", 0) - 1);


           string docandsets;
           if (System.Environment.OSVersion.Version.Major >= 6)
               docandsets = "Users";
           else
               docandsets = "Documents and Settings";

          

           


           string systemdriver = Environment.GetEnvironmentVariable("windir", EnvironmentVariableTarget.Machine); systemdriver = systemdriver.Substring(0, systemdriver.IndexOf(@"\", 0) + 1);
           string[] array1 = Directory.GetDirectories(@systemdriver + docandsets + @"\" + @currentuser + @"\Application Data\Mozilla\Firefox\Profiles\");

           return array1;
       }

       public void createfile(string filedir)
       {
           if (File.Exists(filedir) != true)
           {
               StreamWriter filecreater;
               filecreater = File.CreateText(filedir);
               filecreater.Close();
           }
       }

       public void updatefile(string file, string context)
       {
           StreamWriter writer = new StreamWriter(file);
           writer.Write(context);
           writer.Close();

       }



       public void getcountryicon(string realcountrycode)
       {
           try
           {

               Program.mainform.notifyIcon1.Icon = Icon.FromHandle(new Bitmap(WebRequest.Create("http://www.ip2location.com/images/country/" + realcountrycode + ".gif").GetResponse().GetResponseStream()).GetHicon());
           }
           catch { }

       }

       public string getlocation()
       {

           string sURL;
           sURL = "http://www.melissadata.com/lookups/iplocation.asp?ipaddress=" + Program.mainform.textBox1.Text;

           WebRequest wrGETURL;
           wrGETURL = WebRequest.Create(sURL);

           WebProxy myProxy = new WebProxy("myproxy", 80);
           myProxy.BypassProxyOnLocal = true;

           wrGETURL.Proxy = WebProxy.GetDefaultProxy();

           Stream objStream;
           objStream = wrGETURL.GetResponse().GetResponseStream();



           StreamReader objReader = new StreamReader(objStream);




           string sLine = "";
           string sLineBulk = "";
           int i = 0;

           while (sLine != null)
           {
               i++;
               sLine = objReader.ReadLine();
               sLineBulk += sLine;
           }

           return sLineBulk;
       }

       public string getcountrycode(string country){

           string countrycode = "";
           string countryname = "";
           string realcountrycode = "";

           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AC"; countryname = "Ascension Island";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AD"; countryname = "Andorra";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AE"; countryname = "United Arab Emirates";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AF"; countryname = "Afghanistan";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AG"; countryname = "Antigua and Barbuda";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AI"; countryname = "Anguilla";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AL"; countryname = "Albania";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AM"; countryname = "Armenia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AN"; countryname = "Netherlands Antilles";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AO"; countryname = "Angola";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AQ"; countryname = "Antarctica";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AR"; countryname = "Argentina";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AS"; countryname = "American Samoa";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AT"; countryname = "Austria";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AU"; countryname = "Australia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AW"; countryname = "Aruba";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AX"; countryname = "Aland Islands";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "AZ"; countryname = "Azerbaijan";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BA"; countryname = "Bosnia and Herzegovina";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BB"; countryname = "Barbados";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BD"; countryname = "Bangladesh";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BE"; countryname = "Belgium";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BF"; countryname = "Burkina Faso";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BG"; countryname = "Bulgaria";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BH"; countryname = "Bahrain";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BI"; countryname = "Burundi";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BJ"; countryname = "Benin";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BM"; countryname = "Bermuda";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BN"; countryname = "Brunei Darussalam";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BO"; countryname = "Bolivia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BR"; countryname = "Brazil";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BS"; countryname = "Bahamas";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BT"; countryname = "Bhutan";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BV"; countryname = "Bouvet Island";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BW"; countryname = "Botswana";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BY"; countryname = "Belarus";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "BZ"; countryname = "Belize";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CA"; countryname = "Canada";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CC"; countryname = "Cocos (Keeling) Islands";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CD"; countryname = "Congo, Democratic Republic";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CF"; countryname = "Central African Republic";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CG"; countryname = "Congo";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CH"; countryname = "Switzerland";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CI"; countryname = "Cote D'Ivoire (Ivory Coast)";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CK"; countryname = "Cook Islands";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CL"; countryname = "Chile";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CM"; countryname = "Cameroon";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CN"; countryname = "China";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CO"; countryname = "Colombia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CR"; countryname = "Costa Rica";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CS"; countryname = "Czechoslovakia (former)";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CU"; countryname = "Cuba";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CV"; countryname = "Cape Verde";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CX"; countryname = "Christmas Island";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CY"; countryname = "Cyprus";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "CZ"; countryname = "Czech Republic";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "DE"; countryname = "Germany";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "DJ"; countryname = "Djibouti";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "DK"; countryname = "Denmark";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "DM"; countryname = "Dominica";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "DO"; countryname = "Dominican Republic";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "DZ"; countryname = "Algeria";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "EC"; countryname = "Ecuador";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "EE"; countryname = "Estonia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "EG"; countryname = "Egypt";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "EH"; countryname = "Western Sahara";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "ER"; countryname = "Eritrea";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "ES"; countryname = "Spain";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "ET"; countryname = "Ethiopia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "FI"; countryname = "Finland";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "FJ"; countryname = "Fiji";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "FK"; countryname = "Falkland Islands (Malvinas)";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "FM"; countryname = "Micronesia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "FO"; countryname = "Faroe Islands";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "FR"; countryname = "France";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "FX"; countryname = "France, Metropolitan";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GA"; countryname = "Gabon";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GB"; countryname = "Great Britain (UK)";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GD"; countryname = "Grenada";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GE"; countryname = "Georgia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GF"; countryname = "French Guiana";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GH"; countryname = "Ghana";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GI"; countryname = "Gibraltar";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GL"; countryname = "Greenland";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GM"; countryname = "Gambia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GN"; countryname = "Guinea";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GP"; countryname = "Guadeloupe";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GQ"; countryname = "Equatorial Guinea";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GR"; countryname = "Greece";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GS"; countryname = "S. Georgia and S. Sandwich Isls.";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GT"; countryname = "Guatemala";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GU"; countryname = "Guam";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GW"; countryname = "Guinea-Bissau";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "GY"; countryname = "Guyana";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "HK"; countryname = "Hong Kong";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "HM"; countryname = "Heard and McDonald Islands";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "HN"; countryname = "Honduras";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "HR"; countryname = "Croatia (Hrvatska)";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "HT"; countryname = "Haiti";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "HU"; countryname = "Hungary";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "ID"; countryname = "Indonesia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "IE"; countryname = "Ireland";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "IL"; countryname = "Israel";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "IM"; countryname = "Isle of Man";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "IN"; countryname = "India";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "IO"; countryname = "British Indian Ocean Territory";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "IQ"; countryname = "Iraq";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "IR"; countryname = "Iran";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "IS"; countryname = "Iceland";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "IT"; countryname = "Italy";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "JE"; countryname = "Jersey";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "JM"; countryname = "Jamaica";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "JO"; countryname = "Jordan";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "JP"; countryname = "Japan";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "KE"; countryname = "Kenya";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "KG"; countryname = "Kyrgyzstan";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "KH"; countryname = "Cambodia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "KI"; countryname = "Kiribati";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "KM"; countryname = "Comoros";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "KN"; countryname = "Saint Kitts and Nevis";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "KP"; countryname = "Korea (North)";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "KR"; countryname = "Korea (South)";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "KW"; countryname = "Kuwait";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "KY"; countryname = "Cayman Islands";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "KZ"; countryname = "Kazakhstan";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "LA"; countryname = "Laos";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "LB"; countryname = "Lebanon";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "LC"; countryname = "Saint Lucia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "LI"; countryname = "Liechtenstein";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "LK"; countryname = "Sri Lanka";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "LR"; countryname = "Liberia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "LS"; countryname = "Lesotho";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "LT"; countryname = "Lithuania";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "LU"; countryname = "Luxembourg";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "LV"; countryname = "Latvia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "LY"; countryname = "Libya";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MA"; countryname = "Morocco";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MC"; countryname = "Monaco";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MD"; countryname = "Moldova";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "ME"; countryname = "Montenegro";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MG"; countryname = "Madagascar";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MH"; countryname = "Marshall Islands";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MK"; countryname = "F.Y.R.O.M. (Macedonia)";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "ML"; countryname = "Mali";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MM"; countryname = "Myanmar";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MN"; countryname = "Mongolia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MO"; countryname = "Macau";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MP"; countryname = "Northern Mariana Islands";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MQ"; countryname = "Martinique";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MR"; countryname = "Mauritania";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MS"; countryname = "Montserrat";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MT"; countryname = "Malta";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MU"; countryname = "Mauritius";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MV"; countryname = "Maldives";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MW"; countryname = "Malawi";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MX"; countryname = "Mexico";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MY"; countryname = "Malaysia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "MZ"; countryname = "Mozambique";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "NA"; countryname = "Namibia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "NC"; countryname = "New Caledonia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "NE"; countryname = "Niger";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "NF"; countryname = "Norfolk Island";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "NG"; countryname = "Nigeria";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "NI"; countryname = "Nicaragua";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "NL"; countryname = "Netherlands";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "NO"; countryname = "Norway";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "NP"; countryname = "Nepal";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "NR"; countryname = "Nauru";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "NT"; countryname = "Neutral Zone";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "NU"; countryname = "Niue";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "NZ"; countryname = "New Zealand (Aotearoa)";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "OM"; countryname = "Oman";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "PA"; countryname = "Panama";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "PE"; countryname = "Peru";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "PF"; countryname = "French Polynesia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "PG"; countryname = "Papua New Guinea";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "PH"; countryname = "Philippines";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "PK"; countryname = "Pakistan";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "PL"; countryname = "Poland";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "PM"; countryname = "St. Pierre and Miquelon";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "PN"; countryname = "Pitcairn";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "PR"; countryname = "Puerto Rico";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "PS"; countryname = "Palestinian Territory, Occupied";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "PT"; countryname = "Portugal";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "PW"; countryname = "Palau";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "PY"; countryname = "Paraguay";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "QA"; countryname = "Qatar";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "RE"; countryname = "Reunion";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "RO"; countryname = "Romania";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "RS"; countryname = "Serbia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "RU"; countryname = "Russian Federation";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "RW"; countryname = "Rwanda";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SA"; countryname = "Saudi Arabia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SB"; countryname = "Solomon Islands";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SC"; countryname = "Seychelles";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SD"; countryname = "Sudan";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SE"; countryname = "Sweden";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SG"; countryname = "Singapore";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SH"; countryname = "St. Helena";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SI"; countryname = "Slovenia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SJ"; countryname = "Svalbard &amp; Jan Mayen Islands";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SK"; countryname = "Slovak Republic";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SL"; countryname = "Sierra Leone";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SM"; countryname = "San Marino";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SN"; countryname = "Senegal";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SO"; countryname = "Somalia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SR"; countryname = "Suriname";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "ST"; countryname = "Sao Tome and Principe";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SU"; countryname = "USSR (former)";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SV"; countryname = "El Salvador";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SY"; countryname = "Syria";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "SZ"; countryname = "Swaziland";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "TC"; countryname = "Turks and Caicos Islands";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "TD"; countryname = "Chad";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "TF"; countryname = "French Southern Territories";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "TG"; countryname = "Togo";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "TH"; countryname = "Thailand";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "TJ"; countryname = "Tajikistan";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "TK"; countryname = "Tokelau";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "TM"; countryname = "Turkmenistan";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "TN"; countryname = "Tunisia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "TO"; countryname = "Tonga";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "TP"; countryname = "East Timor";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "TR"; countryname = "Turkey";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "TT"; countryname = "Trinidad and Tobago";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "TV"; countryname = "Tuvalu";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "TW"; countryname = "Taiwan";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "TZ"; countryname = "Tanzania";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "UA"; countryname = "Ukraine";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "UG"; countryname = "Uganda";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "UK"; countryname = "United Kingdom";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "UM"; countryname = "US Minor Outlying Islands";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "US"; countryname = "United States";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "UY"; countryname = "Uruguay";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "UZ"; countryname = "Uzbekistan";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "VA"; countryname = "Vatican City State (Holy See)";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "VC"; countryname = "Saint Vincent &amp; the Grenadines";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "VE"; countryname = "Venezuela";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "VG"; countryname = "British Virgin Islands";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "VI"; countryname = "Virgin Islands (U.S.)";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "VN"; countryname = "Viet Nam";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "VU"; countryname = "Vanuatu";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "WF"; countryname = "Wallis and Futuna Islands";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "WS"; countryname = "Samoa";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "YE"; countryname = "Yemen";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "YT"; countryname = "Mayotte";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "YU"; countryname = "Yugoslavia (former)";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "ZA"; countryname = "South Africa";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "ZM"; countryname = "Zambia";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "ZR"; countryname = "Zaire";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }
           countrycode = "ZW"; countryname = "Zimbabwe";
           if (country.ToLower() == countryname.ToLower()) { realcountrycode = countrycode; }

           return realcountrycode;
       }



       public void traybarElement()
       {

               try
               {

                   string area = getlocation();

                   string opentag = "</td><td><b>";
                   string closetag = "</b>";

                   int pos1 = area.IndexOf("City" + opentag, 0);
                   int pos2 = area.IndexOf(closetag, pos1 + 16);
                   string city = area.Substring(pos1 + 16, pos2 - pos1 - 16);


                   int pos3 = area.IndexOf("State or Region" + opentag, 0);
                   int pos4 = area.IndexOf(closetag, pos3 + 27);
                   string state = area.Substring(pos3 + 27, pos4 - pos3 - 27);


                   int pos5 = area.IndexOf("Country" + opentag, 0);
                   int pos6 = area.IndexOf(closetag, pos5 + 19);
                   string country = area.Substring(pos5 + 19, pos6 - pos5 - 19);



                   int pos7 = area.IndexOf("ISP" + opentag, 0);
                   int pos8 = area.IndexOf(closetag, pos7 + 15);
                   string isp = area.Substring(pos7 + 15, pos8 - pos7 - 15);

                   isp = isp.Replace(".&nbsp;", "");

                   Program.mainform.notifyIcon1.ShowBalloonTip(100, "Host Resolved", city + " / " + state + "\n" + country + "\n" + isp, ToolTipIcon.Info);


                   getcountryicon(getcountrycode(country));


               }
               catch
               {
                   Program.mainform.notifyIcon1.ShowBalloonTip(100, "", "Unable to resolve proxy host", ToolTipIcon.Info);
               }


         

       }

       public void resetnotifyicon()
       {
           Program.mainform.notifyIcon1.Icon = Program.mainform.mainico;   // ICON BACK
       }

       public void passive(bool silent)
       {
           
           IEproxystatus(0);
           FFproxystatus(0);
           Winampproxystatus(0);
           LimeWireproxystatus(0);

           resetnotifyicon();
         
               Program.mainform.checkBox1.Checked = false;
               Program.mainform.checkBox2.Checked = false;
               Program.mainform.checkBox3.Checked = false;
               Program.mainform.checkBox4.Checked = false;
           
           if (silent == false)
           {
               Program.mainform.hidemainform();
               Program.mainform.notifyIcon1.ShowBalloonTip(100, "Proxy Plus", "Proxy " + Program.mainform.textBox1.Text + " Deactivated!", ToolTipIcon.Info);
           }
       }
       
       public void activate()
       {
           
           if (Program.mainform.checkBox1.Checked == false && Program.mainform.checkBox2.Checked == false && Program.mainform.checkBox3.Checked == false && Program.mainform.checkBox4.Checked == false)
           {
               passive(true);
     
           }
           else
           {
               traybarElement();
           }

            if (Program.mainform.checkBox1.Checked == true)
                IEproxystatus(1);
            else
                IEproxystatus(0);


            if (Program.mainform.checkBox2.Checked == true)
                FFproxystatus(1);
            else
                FFproxystatus(0);


            if (Program.mainform.checkBox3.Checked == true)
                Winampproxystatus(1);
              else
                Winampproxystatus(0);


               if (Program.mainform.checkBox4.Checked == true)
                   LimeWireproxystatus(1);
              else
                   LimeWireproxystatus(0);



           

        }





    }
}
