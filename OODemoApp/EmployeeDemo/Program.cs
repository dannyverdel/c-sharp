global using System;

namespace EmployeeDemo;

class Program
{
    static void Main(string[] args) {
        SeniorSoftwareEngineer sse = new SeniorSoftwareEngineer { EmployeeId = 1 };
        JuniorSoftwareEngineer jse = new JuniorSoftwareEngineer { EmployeeId = 2 };

        sse.AssignProject(jse, "Dobbe Transport API");

        jse.ListProjects();

        Console.ReadLine();
    }
}
