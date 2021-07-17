
namespace VolumeControl.Service
{
    partial class VolumeControlServiceInstallerClass
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.VolumeControlServiceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.VolumeControlServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // VolumeControlServiceProcessInstaller
            // 
            this.VolumeControlServiceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.VolumeControlServiceProcessInstaller.Password = null;
            this.VolumeControlServiceProcessInstaller.Username = null;
            // 
            // VolumeControlServiceInstaller
            // 
            this.VolumeControlServiceInstaller.ServiceName = "Schlechtums.VolumeControl";
            this.VolumeControlServiceInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.VolumeControlServiceProcessInstaller,
            this.VolumeControlServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller VolumeControlServiceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller VolumeControlServiceInstaller;
    }
}