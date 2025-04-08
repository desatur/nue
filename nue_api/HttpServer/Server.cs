﻿using nue.CustomConsole;
using System.Net;

namespace nue.HttpServer
{
    public class Server
    {
        private readonly string endpoint;
        private readonly ushort port;
        private readonly string url;
        public readonly List<IApiEndpoint> endpoints;
        public static readonly Server Instance = new();
        public readonly HttpListener listener;

        public Server(string endpoint = "localhost", ushort port = 8069)
        {
            this.endpoint = string.IsNullOrEmpty(endpoint) ? "localhost" : endpoint;
            this.port = port == 0 ? (ushort)8069 : port;

            listener = new HttpListener();
            url = $"http://{this.endpoint}:{this.port}/";
            listener.Prefixes.Add(url);

            endpoints = [];
        }

        public void Start()
        {
            if (listener != null)
            {
                try
                {
                    listener.Start();
                    Logger.Instance.Log(LogType.Info, $"Server started. @ -> {url}");
                    Task.Run(() => Listen());
                }
                catch (Exception ex)
                {
                    Logger.Instance.Log(LogType.Error, $"Error starting server: {ex.Message}");
                }
            }
        }

        private async Task Listen()
        {
            while (listener.IsListening)
            {
                try
                {
                    HttpListenerContext context = await listener.GetContextAsync();
                    ProcessRequest(context);
                }
                catch (HttpListenerException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Logger.Instance.Log(LogType.Error, $"Error processing request: {ex.Message}");
                }
            }
        }

        public void RegisterEndpoint(IApiEndpoint endpoint)
        {
            if (endpoints.Any(ep => ep.Path == endpoint.Path))
            {
                Logger.Instance.Log(LogType.Warning, $"Endpoint '{endpoint.Path}' is already registered.");
                return;
            }

            endpoints.Add(endpoint);
            Logger.Instance.Log(LogType.Info, $"Endpoint '{endpoint.Path}' registered successfully.");
        }

        public void UnregisterEndpoint(string endpointPath)
        {
            IApiEndpoint endpoint = endpoints.FirstOrDefault(ep => ep.Path == endpointPath);
            if (endpoint != null)
            {
                endpoints.Remove(endpoint);
                Logger.Instance.Log(LogType.Info, $"Endpoint '{endpointPath}' unregistered successfully.");
            }
            else
            {
                Logger.Instance.Log(LogType.Warning, $"Endpoint '{endpointPath}' not found to unregister.");
            }
        }

        public void ReregisterEndpoint(IApiEndpoint oldEndpoint, IApiEndpoint newEndpoint)
        {
            bool replaced = false;

            for (int i = 0; i < endpoints.Count; i++)
            {
                if (endpoints[i].Path == oldEndpoint.Path)
                {
                    endpoints[i] = newEndpoint;
                    replaced = true;
                }
            }

            if (replaced)
            {
                Logger.Instance.Log(LogType.Info, $"Endpoint '{oldEndpoint.Path}' reloaded successfully.");
            }
            else
            {
                Logger.Instance.Log(LogType.Warning, $"Endpoint '{oldEndpoint.Path}' not found to reload.");
            }
        }

        public void ReregisterAllEndpoints(List<IApiEndpoint> newEndpoints)
        {
            foreach (var endpoint in endpoints.ToList())
            {
                endpoints.Remove(endpoint);
            }

            foreach (var endpoint in newEndpoints)
            {
                RegisterEndpoint(endpoint);
            }
            Logger.Instance.Log(LogType.Info, "All endpoints reregistered successfully.");
        }

        private void ProcessRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            string requestUrl = request.Url.AbsolutePath;

            Logger.Instance.Log(LogType.Info, $"RQST by {request.UserHostAddress} for {requestUrl}");

            IApiEndpoint matchedEndpoint = FindMatchingEndpoint(requestUrl);
            if (matchedEndpoint != null)
            {
                Task.Run(() => matchedEndpoint.Handle(context));
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                using (var writer = new StreamWriter(context.Response.OutputStream))
                {
                    writer.Write("Endpoint not found");
                }
                context.Response.Close();
                Logger.Instance.Log(LogType.Warning, $"Endpoint not found for {requestUrl}");
            }
        }

        private IApiEndpoint FindMatchingEndpoint(string requestUrl)
        {
            return endpoints.Find(endpoint => endpoint.Path == requestUrl);
        }

        public void Stop()
        {
            if (listener != null && listener.IsListening)
            {
                listener.Stop();
                listener.Close();
                Logger.Instance.Log(LogType.Info, $"Server stopped. !@ -> {url}");
            }
        }
    }
}