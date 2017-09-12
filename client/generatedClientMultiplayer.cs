
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.

namespace ServiceMultiGame
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace= "http://ServiceMultiGame", ConfigurationName= "ServiceMultiGame.IMultiplayer", CallbackContract=typeof(ServiceMultiGame.IMultiplayerCallback), SessionMode=System.ServiceModel.SessionMode.Required)]
    public interface IMultiplayer
    {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=false, Action= "http://ServiceMultiGame/IMultiplayer/RegisterUser")]
        string RegisterUser(string user);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = false, Action = "http://ServiceMultiGame/IMultiplayer/CreateRoom")]
        string CreateRoom(string room);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://ServiceMultiGame/IMultiplayer/AddScore")]
        void AddScore(double n, string token);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://ServiceMultiGame/IMultiplayer/RemoveScore")]
        void RemoveScore(double n, string token);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://ServiceMultiGame/IMultiplayer/GetScore")]
        void GetScore(string token);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://ServiceMultiGame/IMultiplayer/PlayGame")]
        void PlayGame(string token, string room);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = false, Action = "http://ServiceMultiGame/IMultiplayer/GetRoom")]
        string GetRoom();
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://ServiceMultiGame/IMultiplayer/KeyUp")]
        void KeyUp(string token);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://ServiceMultiGame/IMultiplayer/KeyDown")]
        void KeyDown(string token);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://ServiceMultiGame/IMultiplayer/Register")]
        void Register();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMultiplayerCallback
    {
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://ServiceMultiGame/IMultiplayer/ResultScore")]
        void ResultScore(double result, string token);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://ServiceMultiGame/IMultiplayer/ResultMsg")]
        void ResultMsg(string eqn);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://ServiceMultiGame/IMultiplayer/GetPosition")]
        void GetPosition(double result);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://ServiceMultiGame/IMultiplayer/KeyUp")]
        void KeyUp(string token);
        [System.ServiceModel.OperationContractAttribute(IsOneWay = true, Action = "http://ServiceMultiGame/IMultiplayer/KeyDown")]
        void KeyDown(string token);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMultiplayerChannel : ServiceMultiGame.IMultiplayer, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceMultiplayerClient : System.ServiceModel.DuplexClientBase<ServiceMultiGame.IMultiplayer>, ServiceMultiGame.IMultiplayer
    {
        
        public ServiceMultiplayerClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance)
        {
        }
        
        public ServiceMultiplayerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName)
        {
        }
        
        public ServiceMultiplayerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }
        
        public ServiceMultiplayerClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress)
        {
        }
        
        public ServiceMultiplayerClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress)
        {
        }
        
        public void AddScore(double n, string token)
        {
            base.Channel.AddScore(n, token);
        }

        public string CreateRoom(string room)
        {
            return base.Channel.CreateRoom(room);
        }

        public string GetRoom()
        {
            return base.Channel.GetRoom();
        }

        public void GetScore(string token)
        {
            base.Channel.GetScore(token);
        }

        public void KeyDown(string token)
        {
            base.Channel.KeyDown(token);
        }

        public void KeyUp(string token)
        {
            base.Channel.KeyUp(token);
        }

        public void PlayGame(string token, string room)
        {
            base.Channel.PlayGame(token, room);
        }

        public void Register()
        {
            base.Channel.Register();
        }

        public string RegisterUser(string user)
        {
            return base.Channel.RegisterUser(user);
        }

        public void RemoveScore(double n, string token)
        {
            base.Channel.RemoveScore(n, token);
        }
    }
}
