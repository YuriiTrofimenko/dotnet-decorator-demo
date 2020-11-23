using System.Dynamic;
using System.ComponentModel;

public class DynamicModel<T> : DynamicObject, INotifyPropertyChanged {
  public T Target { get; init; }
  public event PropertyChangedEventHandler PropertyChanged;
  
  public override bool TryGetMember(GetMemberBinder binder, out object result)
    {
        string name = binder.Name;
        // System.Console.WriteLine(name);
        result = Target.GetType().GetProperty(name)?.GetValue(Target);
        // System.Console.WriteLine(Target.GetType().GetProperty(name));
        // System.Console.WriteLine(result);
        return result != null;//Target.TryGetValue(name, out result);
    }
    public override bool TrySetMember(SetMemberBinder binder, object value)
    {
        // Target[binder.Name.ToLower()] = value;
        var oldValue = Target.GetType().GetProperty(binder.Name)?.GetValue(Target);
        if (value != oldValue)  
        {  
            Target.GetType().GetProperty(binder.Name)?.SetValue(Target, value);  
            NotifyPropertyChanged(binder.Name);  
        }
        return true;
    }

    private void NotifyPropertyChanged(string propertyName)  
    {  
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}