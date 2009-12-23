using Adis.Log.Contract;
using System.Collections.Generic;

[System.Diagnostics.DebuggerStepThroughAttribute()]
public partial class ReporterContractClient : System.ServiceModel.ClientBase<IReporterContract>, IReporterContract
{
    
    public ReporterContractClient()
    {
    }
    
    public ReporterContractClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public ReporterContractClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public ReporterContractClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public ReporterContractClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public IEnumerable<LogTransportObject> GetRecords(Adis.Log.Contract.RequestFilter filter, int skipFirst, int maxRecords)
    {
        return base.Channel.GetRecords(filter, skipFirst, maxRecords);
    }
    
    public int GetCount(RequestFilter filter)
    {
        return base.Channel.GetCount(filter);
    }

		public IEnumerable<string> GetCategoryList()
		{
			return base.Channel.GetCategoryList();
		}

		public IEnumerable<string> GetApplicationList()
		{
			return base.Channel.GetApplicationList();
		}
}
