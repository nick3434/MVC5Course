using MVC5Course.Hubs;
using NModbus;
using System;
using System.Net.Sockets;
using System.Threading;

namespace MVC5Course.Service
{
    public class ModbusService
    {

        private readonly string _ipAddress = "192.168.1.129";
        private readonly int _port = 502;
        private bool _isRunning = true;

        public void StartReading()
        {
            Thread thread = new Thread(() =>
            {
                while (_isRunning)
                {
                    try
                    {
                        using (TcpClient client = new TcpClient(_ipAddress, _port))
                        {
                            var factory = new ModbusFactory();
                            var master = factory.CreateMaster(client);

                            ushort startAddress = 0;
                            ushort numRegisters = 10;
                            ushort[] registers = master.ReadHoldingRegisters(1, startAddress, numRegisters);

                            Console.WriteLine($"Modbus 測試數據: {registers[0]}");
                            ModbusHub.BroadcastData($"Address {startAddress}", registers[0]);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ Modbus 錯誤: {ex.Message}");
                    }

                    Thread.Sleep(1000); // 每秒讀取一次
                }
            });

            thread.IsBackground = true;
            thread.Start();
        }
    }
}