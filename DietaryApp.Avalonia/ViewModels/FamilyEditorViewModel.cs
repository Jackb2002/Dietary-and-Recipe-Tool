using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WinFormsInfoApp.Family;

namespace DietaryApp.Avalonia.ViewModels;

public partial class FamilyEditorViewModel : ObservableObject
{
    [ObservableProperty] private Person? _selectedMember;

    public ObservableCollection<Person> FamilyMembers { get; } = [];

    public FamilyEditorViewModel(Family family)
    {
        foreach (var p in family.People)
            FamilyMembers.Add(p);
    }

    [RelayCommand] void AddChildMale() => FamilyMembers.Add(new ChildMale());
    [RelayCommand] void AddTeenMale() => FamilyMembers.Add(new TeenMale());
    [RelayCommand] void AddAdultMale() => FamilyMembers.Add(new AdultMale());
    [RelayCommand] void AddElderMale() => FamilyMembers.Add(new ElderlyMale());
    [RelayCommand] void AddChildFemale() => FamilyMembers.Add(new ChildFemale());
    [RelayCommand] void AddTeenFemale() => FamilyMembers.Add(new TeenFemale());
    [RelayCommand] void AddAdultFemale() => FamilyMembers.Add(new AdultFemale());
    [RelayCommand] void AddElderFemale() => FamilyMembers.Add(new ElderlyFemale());

    [RelayCommand]
    void RemoveSelected()
    {
        if (SelectedMember != null)
            FamilyMembers.Remove(SelectedMember);
    }

    public Family BuildFamily() => new() { People = [.. FamilyMembers] };
}
