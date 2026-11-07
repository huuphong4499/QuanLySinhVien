using System;
using System.Windows.Forms;

namespace QLSinhVien
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Tự động dò tìm SQL Server và tự động tạo Database + Bảng dữ liệu mẫu nếu chưa có
            // Giúp bất kỳ ai tải project về chỉ cần bấm F5 là chạy được ngay!
            Database.Initialize();

            Application.Run(new FormDangNhap());
        }
    }
}
