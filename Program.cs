using System;
using System.ComponentModel;

var userModel = new UserModel() {Name = "test"};
dynamic dm = new DynamicModel<UserModel>() {Target = userModel};
System.Console.WriteLine(dm.Name);
INotifyPropertyChanged dmChanged = dm;
dmChanged.PropertyChanged += (sender, args) => {
  System.Console.WriteLine(args.PropertyName);
};
dm.Name = "New Value 1";
dm.Name = "New Value 2";