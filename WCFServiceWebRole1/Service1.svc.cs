using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;

namespace WCFServiceWebRole1
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da classe "Service1" no arquivo de código, svc e configuração ao mesmo tempo.
    // OBSERVAÇÃO: Para iniciar o cliente de teste do WCF para testar esse serviço, selecione Service1.svc ou Service1.svc.cs no Gerenciador de Soluções e inicie a depuração.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public string GetDato()
        {
            return string.Format("You entered:");
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
        DataClasses1DataContext data = new DataClasses1DataContext();

        public List<Consumptions> GetConsumptions()
        {
            try
            {
                return (from consumption in data.Consumptions
                        select consumption).ToList();
            }
            catch
            {
                return null;
            }
        }
        public bool SetConsumptions(int idDevice, DateTime dt)
        {
            try
            {
                Consumptions c = new Consumptions() { FK_Device = idDevice, Date = dt };
                data.Consumptions.InsertOnSubmit(c);
                data.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetLastConsumption(int idDevice)
        {
            try
            {
                var qry = data.Consumptions
                        .Where(m => m.FK_Device == idDevice)
                        .OrderByDescending(m => m.Date)    
                        .FirstOrDefault();
                return qry.Date.ToString();
            }
            catch
            {
                return "";
            }
        }

        public int GetLastConsumptionUnix(int idDevice)
        {
            try
            {
                var qry = data.Consumptions
                        .Where(m => m.FK_Device == idDevice)
                        .OrderByDescending(m => m.Date)
                        .FirstOrDefault();


                return (Int32)(qry.Date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            }
            catch
            {
                return 0;
            }
        }


        public string GetLastConsumptions(int idDevice)
        {
            try
            {
               // List<String> datas = new List<string>();
                var datas = from tbl in data.Consumptions
                                     where tbl.FK_Device == idDevice
                                     orderby tbl.Date descending
                                     select tbl.Date.ToString() ;

                /*  var qry = data.Consumptions
                           .Where(m => m.FK_Device == idDevice)
                           .OrderByDescending(m => m.Date);
                           */

                return new JavaScriptSerializer().Serialize(datas);

            }
            catch
            {
                return "";
            }
        }

        public int GetLastTemperatureUnix(int idDevice)
        {
            try
            {
                var qry = data.Temperatures
                        .Where(m => m.FK_Device == idDevice)
                        .OrderByDescending(m => m.Date)
                        .FirstOrDefault();


                return (Int32)(qry.Date.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                 
            }
            catch
            {
                return 0;
            }
        }



        public bool SetConsumptionsFromUnix(int idDevice, int unixTimeStamp)
        {
            try
            {

                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();

                Consumptions c = new Consumptions() { };

                c.FK_Device = idDevice;
                c.Date = dtDateTime;

                data.Consumptions.InsertOnSubmit(c);
                data.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SetTemperatures(int idDevice, double s1, double s2, double s3, double s4, double s5, double s6, int unixTimeStamp)
        {
            try
            {
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();

                Temperatures t = new Temperatures() { };

                t.FK_Device = idDevice;
                t.Date = dtDateTime;
                t.S1 = s1;
                t.S2 = s2;
                t.S3 = s3;
                t.S4 = s4;
                t.S5 = s5;
                t.S6 = s6;

                data.Temperatures.InsertOnSubmit(t);
                data.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
