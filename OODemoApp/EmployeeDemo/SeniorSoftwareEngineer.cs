namespace EmployeeDemo;

public class SeniorSoftwareEngineer : SoftwareEngineer, IEmployee
{
    public int EmployeeId { get; set; }
    public Address? Address { get; set; }
    public void AssignProject(JuniorSoftwareEngineer engineer, string project) => engineer.GetProjectAssigned(project);
}
