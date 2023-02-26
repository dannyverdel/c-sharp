namespace EmployeeDemo;

public class JuniorSoftwareEngineer : SoftwareEngineer, IEmployee
{
    public int EmployeeId { get; set; }
    public Address? Address { get; set; }
    public void GetProjectAssigned(string project) => Projects.Add(project);
    public void ListProjects() {
        foreach ( string project in Projects )
            Console.WriteLine(project);
    }
}
