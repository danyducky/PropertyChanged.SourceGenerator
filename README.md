# PropertyChanged.SourceGenerator
Source code generator for backing fields to auto implement the `property changed` and `property changing` events.

Auto implement the properties by backing fields with raising `INotifyPropertyChanged.PropertyChanged` and `INotifyPropertyChanging.PropertyChanging` events.
Backing fields supports different naming conventions, the behavior can be changed via `editorconfig`:
+ **property_changed_backing_fields_convention** = :camelcase or :pascalcase - backing fields naming convention.
  
  > [!NOTE]
  > PascalCase requires underscore.
+ **property_changed_backing_fields_underscore** = :boolean - should backing fields use underscore, `false` by default.
+ **property_changed_raise_method_names** = :string[] - method names to raise the `INotifyPropertyChanged.PropertyChanged` event.
+ **property_changing_raise_method_names** = :string[] - method names to raise the `INotifyPropertyChanging.PropertyChanging` event.
