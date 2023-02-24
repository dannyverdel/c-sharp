namespace DemoLibrary.Tests;

public class DataAccessTests
{
    [Fact]
    public void AddPersonToPeopleListShouldWork() {
        PersonModel new_person = new PersonModel() { FirstName = "Danny", LastName = "Verdel" };
        List<PersonModel> people = new List<PersonModel>();

        DataAccess.AddPersonToPeopleList(people, new_person);

        Assert.True(people.Count == 1);
        Assert.Contains<PersonModel>(new_person, people);
    }

    [Theory]
    [InlineData("Danny", "", "LastName")]
    [InlineData("", "Verdel", "FirstName")]
    public void AddPersonToPeopleListShouldFail(string first_name, string last_name, string param) {
        PersonModel new_person = new PersonModel() { FirstName = first_name, LastName = last_name };
        List<PersonModel> people = new List<PersonModel>();

        Assert.Throws<ArgumentException>(param, () => DataAccess.AddPersonToPeopleList(people, new_person));
    }

    [Fact]
    public void ConvertCSVToModelsShouldWork() {
        string[] content = { "Danny,Verdel", "Vera,Pouwels", "Max,Zoetendaal" };
        List<PersonModel> people = DataAccess.ConvertCSVToModels(content);

        Assert.True(people.Count == 3);
        Assert.NotNull(people.Where(x => x.FirstName == "Max").FirstOrDefault());
    }

    [Theory]
    [InlineData("Danny,", "Max,Zoetendaal", "Vera,Pouwels", "data[1]")]
    [InlineData("Danny,Verdel", ",Zoetendaal", "Vera,Pouwels", "data[0]")]
    [InlineData(",", "Max,Zoetendaal", "Vera,Pouwels", "data[0]")]
    public void ConvertCSVToModelShouldFail(string name1, string name2, string name3, string param) {
        string[] content = { name1, name2, name3 };
        Assert.Throws<ArgumentException>(param, () => DataAccess.ConvertCSVToModels(content));
    }
}

