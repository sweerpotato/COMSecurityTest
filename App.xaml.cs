using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace COMSecurityTest
{
    internal static class NativeMethods
    {
        private enum RpcAuthnLevel
        {
            Default = 0,
            None = 1,
            Connect = 2,
            Call = 3,
            Pkt = 4,
            PktIntegrity = 5,
            PktPrivacy = 6
        }

        private enum RpcImpLevel
        {
            Default = 0,
            Anonymous = 1,
            Identify = 2,
            Impersonate = 3,
            Delegate = 4
        }

        private enum EoAuthnCap
        {
            None = 0x0000,
            MutualAuth = 0x0001,
            StaticCloaking = 0x0020,
            DynamicCloaking = 0x0040,
            AnyAuthority = 0x0080,
            MakeFullSIC = 0x0100,
            Default = 0x0800,
            SecureRefs = 0x0002,
            AccessControl = 0x0004,
            AppID = 0x0008,
            Dynamic = 0x0010,
            RequireFullSIC = 0x0200,
            AutoImpersonate = 0x0400,
            NoCustomMarshal = 0x2000,
            DisableAAA = 0x1000
        }

        [DllImport("Ole32.dll",
            ExactSpelling = true,
            EntryPoint = "CoInitializeSecurity",
            CallingConvention = CallingConvention.StdCall,
            SetLastError = false,
            PreserveSig = false)]
        private static extern void CoInitializeSecurity(
            IntPtr pVoid,
            int cAuthSvc,
            IntPtr asAuthSvc,
            IntPtr pReserved1,
            uint dwAuthnLevel,
            uint dwImpLevel,
            IntPtr pAuthList,
            uint dwCapabilities,
            IntPtr pReserved3);

        public static void Initialize()
        {
            CoInitializeSecurity(IntPtr.Zero,
                -1,
                IntPtr.Zero,
                IntPtr.Zero,
                (uint)RpcAuthnLevel.PktPrivacy,
                (uint)RpcImpLevel.Impersonate,
                IntPtr.Zero,
                (uint)EoAuthnCap.DynamicCloaking,
                IntPtr.Zero);
        }
    }

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            NativeMethods.Initialize();
        }
    }
}
