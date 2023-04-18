using HermesVideo.ADOApp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace HermesVideo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly HermesVideoEntities _connection = new HermesVideoEntities();
        public static HermesVideoEntities Connection => _connection;
        public static bool IsAdministratorMode { get; set; } = false;
    }
}
