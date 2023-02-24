using DemoLibrary.Models;
namespace DemoLibrary;

public static class DataAccess
{
    private static string _person_text_file = "PersonText.txt";
    public static void AddNewPerson(PersonModel person) {
        List<PersonModel> people = GetAllPeople();

        AddPersonToPeopleList(people, person);

        List<string> lines = ConvertModelsToCSV(people);

        File.WriteAllLines(_person_text_file, lines);
    }

    public static void AddPersonToPeopleList(List<PersonModel> people, PersonModel person) {
        if ( person.FirstName.IsNullEmptyOrWhiteSpace() )
            throw new ArgumentException("You passed in an invalid parameter", "FirstName");
        else if ( person.LastName.IsNullEmptyOrWhiteSpace() )
            throw new ArgumentException("You passed in an invalid parameter", "LastName");
        people.Add(person);
    }

    public static List<string> ConvertModelsToCSV(List<PersonModel> people) {
        List<string> output = new List<string>();
        foreach ( PersonModel user in people )
            output.Add($"{user.FirstName},{user.LastName}");

        return output;
    }

    public static List<PersonModel> GetAllPeople() {
        string[] content = File.ReadAllLines(_person_text_file);

        List<PersonModel> output = ConvertCSVToModels(content);

        return output;
    }

    public static List<PersonModel> ConvertCSVToModels(string[] csv) {
        List<PersonModel> output = new List<PersonModel>();
        foreach ( string line in csv ) {
            string[] data = line.Split(",");
            if ( data[0].IsNullEmptyOrWhiteSpace() || data[1].IsNullEmptyOrWhiteSpace() )
                throw new ArgumentException("You passed in an invalid parameter", data[0].IsNullEmptyOrWhiteSpace() ? "data[0]" : "data[1]");
            output.Add(new PersonModel { FirstName = data[0], LastName = data[1] });
        }

        return output;
    }
}

