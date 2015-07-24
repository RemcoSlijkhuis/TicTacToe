using System;
using System.ServiceProcess;
using TicTacToeServer;

namespace TicTacToeServerService
{
    public partial class Service : ServiceBase
    {
        IDisposable server = null;

        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            server = TicTacToeServer.Program.StartServer();
        }

        protected override void OnStop()
        {
            server.Dispose();
        }

        private void InitializeComponent()
        {
            // 
            // Service
            // 
            this.ServiceName = "Ultimate Tic Tac Toe Server";

        }

    }
}
