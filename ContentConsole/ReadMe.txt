- Solution was developed using Visual Studio 2013; it took me more than an hour to complete the test.
- Business logic has been implmented in a separate dll - Content.Bll (Business Logic Layer)
- Data acccess would be in a seprate dll - Content.Dal (Data Access Layer); Dal would implement IContentRepository. The definition of IContentRepository would be moved to a 
  separate dll, that both Bll and Dll would reference.
- The console project is the UI layer.
- I would also rename the unit test project to Content.Bll.Test.Unit
- No exception handling or logging has been implemented
- Code hasn't been properly commented
