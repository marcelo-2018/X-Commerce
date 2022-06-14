﻿using System;
using System.Globalization;
using System.Windows.Forms;
using Aplicacion.IoC;
using Presentacion.Core.Login;
using StructureMap;

namespace CommerceApp
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            CultureInfo.CurrentCulture = new CultureInfo("es-AR");

            // Configuracion del Inyector (StructureMap)
            new StructureMapContainer().Configure();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(ObjectFactory.GetInstance<Principal>());

            var fLogin = ObjectFactory.GetInstance<Login>();
            fLogin.ShowDialog();

            if (fLogin.PuedeAcceder)
            {
                Application.Run(ObjectFactory.GetInstance<Principal>());
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
