using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Serialization;
using System.Web.Script.Services;

namespace WCFServiceWebRole1
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "IService1" no arquivo de código e configuração ao mesmo tempo.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebGet]
        string GetData(int value);

        [OperationContract, XmlSerializerFormat(Style = OperationFormatStyle.Document)]
        [WebMethod]
        [SoapDocumentMethod("http://services.mysite.com/MyService/MyMethod", RequestNamespace = "http://services.mysite.com/MyService", ResponseNamespace = "http://services.mysite.com/MyService", Use = System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle = System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        string GetDato();

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        [WebGet]
        List<Consumptions> GetConsumptions();

        [OperationContract]
        [WebGet(UriTemplate = "SetConsumptions?idDevice={iddevice}&dt={dt}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool SetConsumptions(int idDevice, DateTime dt);

        [OperationContract]
        [WebGet(UriTemplate = "SetTemperatures?idDevice={iddevice}&s1={s1}&s2={s2}&s3={s3}&s4={s4}&s5={s5}&s6={s6}&unixTimeStamp={unixTimeStamp}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool SetTemperatures(int idDevice, double s1, double s2, double s3, double s4, double s5, double s6, int unixTimeStamp);

        [OperationContract]
        [WebGet(UriTemplate = "SetConsumptionsFromUnix?idDevice={iddevice}&unixTimeStamp={unixTimeStamp}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool SetConsumptionsFromUnix(int idDevice, int unixTimeStamp);


        [OperationContract]
        [WebGet(UriTemplate = "GetLastConsumption?idDevice={iddevice}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string GetLastConsumption(int idDevice);
        
        [OperationContract]
        [WebGet(UriTemplate = "GetLastConsumptionUnix?idDevice={iddevice}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        int GetLastConsumptionUnix(int idDevice);

        [OperationContract]
        [WebGet(UriTemplate = "GetLastConsumptions?idDevice={iddevice}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        string GetLastConsumptions(int idDevice);


        [OperationContract]
        [WebGet(UriTemplate = "GetLastTemperatureUnix?idDevice={iddevice}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        int GetLastTemperatureUnix(int idDevice);
        // TODO: Adicione suas operações de serviço aqui
    }


    // Use um contrato de dados como ilustrado no exemplo abaixo para adicionar tipos compostos a operações de serviço.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }

      

        
    }
}
