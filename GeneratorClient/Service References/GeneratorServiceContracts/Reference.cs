﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.34014
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GeneratorClient.GeneratorServiceContracts
{


    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Message", Namespace="http://schemas.datacontract.org/2004/07/GeneratorServiceContracts")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(GeneratorClient.GeneratorServiceContracts.Operations))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(GeneratorClient.GeneratorServiceContracts.Status))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(object[]))]
    public partial class Message : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ApplicationTokenField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private object[] DataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string InfoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private GeneratorClient.GeneratorServiceContracts.Operations OperationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private GeneratorClient.GeneratorServiceContracts.Status StatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UserTokenField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ApplicationToken {
            get {
                return this.ApplicationTokenField;
            }
            set {
                if ((object.ReferenceEquals(this.ApplicationTokenField, value) != true)) {
                    this.ApplicationTokenField = value;
                    this.RaisePropertyChanged("ApplicationToken");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public object[] Data {
            get {
                return this.DataField;
            }
            set {
                if ((object.ReferenceEquals(this.DataField, value) != true)) {
                    this.DataField = value;
                    this.RaisePropertyChanged("Data");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Info {
            get {
                return this.InfoField;
            }
            set {
                if ((object.ReferenceEquals(this.InfoField, value) != true)) {
                    this.InfoField = value;
                    this.RaisePropertyChanged("Info");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public GeneratorClient.GeneratorServiceContracts.Operations Operation {
            get {
                return this.OperationField;
            }
            set {
                if ((this.OperationField.Equals(value) != true)) {
                    this.OperationField = value;
                    this.RaisePropertyChanged("Operation");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public GeneratorClient.GeneratorServiceContracts.Status Status {
            get {
                return this.StatusField;
            }
            set {
                if ((this.StatusField.Equals(value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UserToken {
            get {
                return this.UserTokenField;
            }
            set {
                if ((object.ReferenceEquals(this.UserTokenField, value) != true)) {
                    this.UserTokenField = value;
                    this.RaisePropertyChanged("UserToken");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Operations", Namespace="http://schemas.datacontract.org/2004/07/GeneratorServiceContracts")]
    public enum Operations : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Authenticate = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Decode = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Finish = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GetCompleted = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        GetDecrypted = 4,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Status", Namespace="http://schemas.datacontract.org/2004/07/GeneratorServiceContracts")]
    public enum Status : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Sent = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Suceeded = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Failed = 2,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="GeneratorServiceContracts.IWorkService")]
    public interface IWorkService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWorkService/ServiceOperation", ReplyAction="http://tempuri.org/IWorkService/ServiceOperationResponse")]
        GeneratorClient.GeneratorServiceContracts.Message ServiceOperation(GeneratorClient.GeneratorServiceContracts.Message msg);
        
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IWorkService/ServiceOperation", ReplyAction="http://tempuri.org/IWorkService/ServiceOperationResponse")]
        System.IAsyncResult BeginServiceOperation(GeneratorClient.GeneratorServiceContracts.Message msg, System.AsyncCallback callback, object asyncState);
        
        GeneratorClient.GeneratorServiceContracts.Message EndServiceOperation(System.IAsyncResult result);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWorkServiceChannel : GeneratorClient.GeneratorServiceContracts.IWorkService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceOperationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        public ServiceOperationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        public GeneratorClient.GeneratorServiceContracts.Message Result {
            get {
                base.RaiseExceptionIfNecessary();
                return ((GeneratorClient.GeneratorServiceContracts.Message)(this.results[0]));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WorkServiceClient : System.ServiceModel.ClientBase<GeneratorClient.GeneratorServiceContracts.IWorkService>, GeneratorClient.GeneratorServiceContracts.IWorkService {
        
        private BeginOperationDelegate onBeginServiceOperationDelegate;
        
        private EndOperationDelegate onEndServiceOperationDelegate;
        
        private System.Threading.SendOrPostCallback onServiceOperationCompletedDelegate;
        
        public WorkServiceClient() {
        }
        
        public WorkServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WorkServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WorkServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WorkServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public event System.EventHandler<ServiceOperationCompletedEventArgs> ServiceOperationCompleted;
        
        public GeneratorClient.GeneratorServiceContracts.Message ServiceOperation(GeneratorClient.GeneratorServiceContracts.Message msg) {
            return base.Channel.ServiceOperation(msg);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.IAsyncResult BeginServiceOperation(GeneratorClient.GeneratorServiceContracts.Message msg, System.AsyncCallback callback, object asyncState) {
            return base.Channel.BeginServiceOperation(msg, callback, asyncState);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public GeneratorClient.GeneratorServiceContracts.Message EndServiceOperation(System.IAsyncResult result) {
            return base.Channel.EndServiceOperation(result);
        }
        
        private System.IAsyncResult OnBeginServiceOperation(object[] inValues, System.AsyncCallback callback, object asyncState) {
            GeneratorClient.GeneratorServiceContracts.Message msg = ((GeneratorClient.GeneratorServiceContracts.Message)(inValues[0]));
            return this.BeginServiceOperation(msg, callback, asyncState);
        }
        
        private object[] OnEndServiceOperation(System.IAsyncResult result) {
            GeneratorClient.GeneratorServiceContracts.Message retVal = this.EndServiceOperation(result);
            return new object[] {
                    retVal};
        }
        
        private void OnServiceOperationCompleted(object state) {
            if ((this.ServiceOperationCompleted != null)) {
                InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
                this.ServiceOperationCompleted(this, new ServiceOperationCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
            }
        }
        
        public void ServiceOperationAsync(GeneratorClient.GeneratorServiceContracts.Message msg) {
            this.ServiceOperationAsync(msg, null);
        }
        
        public void ServiceOperationAsync(GeneratorClient.GeneratorServiceContracts.Message msg, object userState) {
            if ((this.onBeginServiceOperationDelegate == null)) {
                this.onBeginServiceOperationDelegate = new BeginOperationDelegate(this.OnBeginServiceOperation);
            }
            if ((this.onEndServiceOperationDelegate == null)) {
                this.onEndServiceOperationDelegate = new EndOperationDelegate(this.OnEndServiceOperation);
            }
            if ((this.onServiceOperationCompletedDelegate == null)) {
                this.onServiceOperationCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnServiceOperationCompleted);
            }
            base.InvokeAsync(this.onBeginServiceOperationDelegate, new object[] {
                        msg}, this.onEndServiceOperationDelegate, this.onServiceOperationCompletedDelegate, userState);
        }
    }
}
